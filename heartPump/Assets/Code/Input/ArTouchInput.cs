using UnityEngine;
using System.Collections.Generic;

// Singleton class for managing touch events
// Must be updated each step with ArTouchInput.GetInstance().Update()
// Assign event callbacks to OnTouch____ delegates
public class ArTouchInput 
{
	#region Consts
	// The identifier passed to Unity's Input class to specify the left mouse button
	private const int LEFT_MOUSE = 0;
	
	// Maximum age of a touch to throw a tap event when released
	private const float TAP_MAXTIME = 0.3f;
	// Maximum time to throw a double tap event when second touch released
	// Time is measured as between the placing of the first touch and the removal
	// of the second for code simplicity
	private const float DOUBLE_TAP_MAXTIME = 0.8f;
	// Maximum age of a touch to throw a flick event when released
	private const float FLICK_MAXTIME = 0.2f;
	// Minimum age of a touch to start throwing press events while held
	// Should be greater than or equal to TAP_MAXTIME
	private const float PRESS_MINTIME = 0.4f;
	// Minimum age of a touch to start throwing drag events while moving
	// Should be greater than or equal to FLICK_MAXTIME
	private const float DRAG_MINTIME = 0.2f;
	
	// Minimum distance in inches a touch must travel from its startPosition
	// to be considered a moving touch and not a stationary one
	private const float START_MOVE_DIST_IN = 0.022f;
	// Minimum distance in inches a touch must travel from its last position
	// to register as a move. Used as a simple low-pass filter for unsteady
	// fingers.
	private const float REGISTER_MOVE_DIST_IN = 0.01f;
	// Maximum distance in inches between two consecutive taps
	// for a tap to count as a double tap event.
	private const float DOUBLE_TAP_MAX_DIST_IN = 0.1f;
	
	private const float DEFAULT_DPI = 200;
	#endregion
	
	#region Delegates
	// Delegate type
	public delegate void TouchEventDelegate (ref ArTouch touch);
	
	// Called when a touch is first placed on the screen
	public static TouchEventDelegate OnTouchDown = null;
	// Called whenever a touch moves a registered distance on the screen
	// We should generally use OnTouchDrag instead, which waits for a moment
	// to see if the touch is a flick or not.
	public static TouchEventDelegate OnTouchMove = null;
	// Called when a touch is removed from the screen
	public static TouchEventDelegate OnTouchUp = null;
	// Called when a touch has been held at its starting location for a time (long press)
	public static TouchEventDelegate OnPress = null;
	// Called when a touch is moved away from its starting location
	public static TouchEventDelegate OnDrag = null;
	// Called when a touch is placed down and quickly released without moving
	public static TouchEventDelegate OnTap = null;
	// Called when a touch is tapped near where another tap recently occurred
	public static TouchEventDelegate OnDoubleTap = null;
	// Called when a touch is released shortly after being placed having moved
	// a significant distance
	public static TouchEventDelegate OnFlick = null;
	#endregion
	
	#region Members
	// The current ArTouches on the screen
	private List<ArTouch> _touches;
	// A separate ArTouch reserved for the mouse
	private ArTouch _mouseTouch;
	// The most recent released taps on the screen
	// Checked for double tap events
	private List<ArTouch> _recentTaps;
	
	// The calculated minimum squared distance in pixels a touch must travel from its startPosition
	// to be considered a moving touch and not a stationary one
	private float _startMoveSqdist;
	// The calculated minimum squared distance in pixels a touch must travel from its last position
	// to register as a move. Used as a simple low-pass filter for unsteady
	// fingers.
	private float _registerMoveSqdist;
	// The calculated maximum squared distance in pixels between two consecutive taps
	// for a tap to count as a double tap event.
	private float _doubleTapMaxSqdist;
	
	
	#endregion
	
	#region Singleton
	private static ArTouchInput instance = null;
	
	// Singleton accessor
	public static ArTouchInput GetInstance()
	{
		if (instance == null)
		{
			instance = new ArTouchInput();
		}
		return instance;
	}
	
	// Private constructor for singleton
	private ArTouchInput () 
	{
		// TODO jg Set initial capacity?
		_touches = new List<ArTouch>();
		_mouseTouch = null;
		_recentTaps = new List<ArTouch>();
		
		// Calculate squared distances for gesture detection
		float dpi = Screen.dpi;
		// Unity could not determine dpi
		if (dpi == 0)
		{
			dpi = DEFAULT_DPI;
		}
		_startMoveSqdist = (START_MOVE_DIST_IN * START_MOVE_DIST_IN * dpi * dpi);
		_registerMoveSqdist = (REGISTER_MOVE_DIST_IN * REGISTER_MOVE_DIST_IN * dpi * dpi);
		_doubleTapMaxSqdist = (DOUBLE_TAP_MAX_DIST_IN * DOUBLE_TAP_MAX_DIST_IN * dpi * dpi);
		
		// Add all existing touches as if they'd just been placed down
		if (Input.GetMouseButton(LEFT_MOUSE))
		{
			ProcessTouchDown(ArTouch.MOUSE_ID, Input.mousePosition);		
		}
		// NOTE jg Input.touchCount appears to be zero here, but I'll
		// leave the code in for safety's sake
		foreach (Touch touch in Input.touches)
		{
			ProcessTouchDown(touch.fingerId, touch.position);	
		}
	}
	#endregion
	
	#region Public Member Functions
	// Returns the ArTouch with the given touch id or null if ArTouch touch does not exist
	public ArTouch GetTouch(int touchId)
	{
		if (touchId == ArTouch.MOUSE_ID)
		{
			return _mouseTouch;	
		}
		else
		{
			// Lambda!
			return _touches.Find(t => t.id == touchId);
		}
	}
	
	// Returns how many currently held-down touches there are
	public int GetNumTouches()
	{
		int count = _touches.Count;
		if (_mouseTouch != null)
		{
			++count;	
		}
		return count;
	}
	
	// Call once per frame
	// Processes touch information and potentially calls event delegates
	public void Update () 
	{
		// Used to calculate how many touches we should have at the end of processing
		// because Unity's list of touches includes those where the touchPhase says the
		// touch is ended or canceled.
		int finalTouchNum = Input.touches.Length;
		
		// Process Unity's reported touches
		foreach (Touch utouch in Input.touches)
		{
				
			// A new touch has been detected, so add it
			if (utouch.phase == TouchPhase.Began)
			{
				ProcessTouchDown(utouch.fingerId, utouch.position);		
			}
			// A touch has been removed from the screen, so remove it from our references
			else if (utouch.phase == TouchPhase.Ended || utouch.phase == TouchPhase.Canceled)
			{
				ProcessTouchUp(utouch.fingerId, utouch.position);
				--finalTouchNum;
			}
			// This touch already existed, so update it
			else
			{
				ProcessTouchUpdate(utouch.fingerId, utouch.position);
			}
		}
		
		// Process the mouse as its own special touch
		ProcessMouse();
		
		// Remove all the recent taps that are now too old to help
		// throw double tap events.
		_recentTaps.RemoveAll(t => t.GetAge() > DOUBLE_TAP_MAXTIME);
		
		// Make sure our touch representations haven't fallen out
		// of phase with Unity's
		CleanupTouches(finalTouchNum);
	}
	#endregion
	
	#region Private Processing Functions
	private void ProcessTouchDown(int id, Vector2 position)
	{
		// Update instead of add if this touch has already been added
		ArTouch checkTouch = GetTouch(id);
		if (checkTouch != null)
		{
			ArDebug.LogWarning("Tried to add touch " + id + " multiple times.");
			ProcessTouchUpdate(id, position);
			return;
		}
		
		// If the mouse is already held down, force it out
		// of touches, because Unity's going to overwrite
		// its data with this new touch anyway
		ArTouch mouse = GetTouch(ArTouch.MOUSE_ID);
		if (mouse != null)
		{
			// NOTE: Don't pass through Input.mousePosition,
			// because the new touch has overwritten the mouse data
			ProcessTouchUp(ArTouch.MOUSE_ID, mouse.position);
		}
		
		// Create and store new ArTouch from the Unity touch
		ArTouch newTouch = new ArTouch(id, position);
		if (id == ArTouch.MOUSE_ID)
		{
			_mouseTouch = newTouch;
		}
		else
		{
			_touches.Add(newTouch);
		}
		
		// Call any assigned functions for TouchDown events
		if (OnTouchDown != null && !newTouch.isDead)
		{
			OnTouchDown(ref newTouch);
		}
	}
	
	private void ProcessTouchUpdate(int id, Vector2 position)
	{	
		ArTouch touch = GetTouch(id);
		
		// If this touch has not been added yet, add it instead of updating
		if (touch == null)
		{
			Debug.LogWarning("Tried to update touch " + id + " before adding it.");
			ProcessTouchDown(id, position);
			return;
		}
		
		// Only update the touch's position if it has moved a significant distance
		Vector2 moveDelta = position - touch.position;
		bool moved = (moveDelta.sqrMagnitude >= _registerMoveSqdist);
		if (moved)
		{
			touch.deltaPosition = moveDelta;
			touch.position = position;
		}
		
		// If touch hasn't moved far enough to throw move events yet, check if it has now
		if (!touch.hasMoved)
		{
			if ((touch.position - touch.startPosition).sqrMagnitude >= _startMoveSqdist)
			{
				touch.hasMoved = true;
			}
		}
		
		// Store age for event checks
		float touchAge =  touch.GetAge();
		
		// If it still hasn't moved, see if it should throw any assigned press events
		if (!touch.hasMoved && touchAge > PRESS_MINTIME)
		{
			if (OnPress != null && !touch.isDead)
			{
				OnPress(ref touch);
			}
		}
		// If it has moved far enough to throw move events, process that
		else if (touch.hasMoved && moved)
		{
			// If we're not still waiting for flicks, throw any assigned drag events
			if (touchAge > DRAG_MINTIME)
			{
				if (OnDrag != null && !touch.isDead)
				{
					OnDrag(ref touch);
				}
			}
			
			// Throw any assigned generic touch move events
			if (OnTouchMove != null && !touch.isDead)
			{
				OnTouchMove(ref touch);	
			}
		}
	}
	
	private void ProcessTouchUp(int id, Vector2 position)
	{	
		ArTouch touch = GetTouch(id);
		
		// If this touch has not been added yet, 
		// add it then remove it.
		// NOTE jg:  Does adding and removing a touch in the same step
		// ever cause trouble?
		if (touch == null)
		{
			Debug.LogWarning("Tried to remove touch " + id + " before adding it.");
			ProcessTouchDown(id, position);
			touch = GetTouch(id);
		}
		
		// Store age for event processing
		float touchAge = touch.GetAge();
		
		// If a touch hasn't moved and is young enough, throw any assigned tap events
		if (!touch.hasMoved && touchAge <= TAP_MAXTIME)
		{
			// If the touch is near enough to a recent tap's location, throw any 
			// assigned double tap events
			// NOTE: To prevent this touch throwing a basic tap event as well, 
			// set touch.isDead in the double callback function
			bool doubleTap = false;
			foreach (ArTouch oldTap in _recentTaps)
			{
				if ((oldTap.position - touch.position).sqrMagnitude <= _doubleTapMaxSqdist)
				{
					if (OnDoubleTap != null && !touch.isDead)
					{
						OnDoubleTap(ref touch);
						_recentTaps.Remove(oldTap);
						doubleTap = true;
						// Break because we don't want to trigger OnDoubleTap multiple times
						// if it's near multiple old taps
						break;
					}
				}
			}
			
			// Throw any assigned tap events
			if (OnTap != null && !touch.isDead)
			{
				OnTap(ref touch);	
			}
			
			// Add this touch to the list of recent taps
			// if not used on a double tap (otherwise we enter 
			// every tap on that spot will count as a double
			if (!doubleTap)
			{
				_recentTaps.Add(touch);
			}
		}
		// If a touch has moved and is young enough, throw any assigned flick events
		else if (touch.hasMoved && touchAge <= FLICK_MAXTIME)
		{
			if (OnFlick != null && !touch.isDead)
			{
				OnFlick(ref touch);	
			}
		}
		
		// Call any assigned functions for generic TouchUp events
		if (OnTouchUp != null && !touch.isDead)
		{
			OnTouchUp(ref touch);
		}
		
		// Remove the touch from our references
		if (id == ArTouch.MOUSE_ID)
		{
			_mouseTouch = null;	
		}
		else
		{
			_touches.Remove(touch);
		}
	}
	
	// Use left mouse click as surrogate touch
	private void ProcessMouse()
	{
		// Unity automatically overwrites the mouse data
		// with the first touch received, so we can
		// only process mouse by itself if there are no
		// touches down.
		if (Input.touchCount == 0)
		{
			if (Input.GetMouseButtonDown(LEFT_MOUSE))
			{
				ProcessTouchDown(ArTouch.MOUSE_ID, Input.mousePosition);
			}
			else if (Input.GetMouseButtonUp(LEFT_MOUSE))
			{
				ProcessTouchUp(ArTouch.MOUSE_ID, Input.mousePosition);
			}
			else if (Input.GetMouseButton(LEFT_MOUSE))
			{
				ProcessTouchUpdate(ArTouch.MOUSE_ID, Input.mousePosition);
			}
		}
	}
	
	private void CleanupTouches(int correctTouchNum)
	{
		// Make sure that our existing checks to add touches that we've
		// missed their addition touchPhase are working, and we have
		// at least as many touches as Unity is reporting
		ArDebug.Assert(_touches.Count >= correctTouchNum,
						"We're holding fewer touches than Unity reports... How?");
		
		// Make sure that we haven't missed the release of the mouse and
		// held onto mouseTouch for too long
		ArDebug.Assert(!(_mouseTouch != null && !Input.GetMouseButton(LEFT_MOUSE)),
						"We're holding mouseTouch while mouse is up... How?");
		
		// After processing, we might still be holding onto too many touches
		// if we missed Unity's reported removal touchPhase somehow
		// Remove those extra touches now
		if (_touches.Count > correctTouchNum)
		{
			// Create a list of all the touches to remove
			List<ArTouch> removeTouches = new List<ArTouch>(_touches.Count - correctTouchNum);
			
			// For each touch we're holding, see if it has a corresponding Unity touch.
			// If not, add it to the list of touches to remove
			foreach (ArTouch touch in _touches)
			{
				foreach (Touch uTouch in Input.touches)
				{
					if (uTouch.fingerId == touch.id)
					{
						continue;
					}
				}
				ArDebug.Assert(removeTouches.Count != removeTouches.Capacity,
							"List of touches to remove filled too early... how?");
				removeTouches.Add(touch);
			}
			
			// Forcibly remove the touches queued for removal
			foreach (ArTouch removeTouch in removeTouches)
			{
				ProcessTouchUp(removeTouch.id, removeTouch.position);	
			}
			
			ArDebug.Assert(_touches.Count == correctTouchNum,
						"Cleanup did not match our touches to Unity's... why not?");
		}
		
	}
	#endregion
 
}
