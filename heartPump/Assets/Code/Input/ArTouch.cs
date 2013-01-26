using UnityEngine;

// Asterogue's class representing a touch on the screen
public class ArTouch
{
	// The touch id used for ArTouches spawned by the mouse
	public const int MOUSE_ID = 0;
	
	// The touch id for null ArTouches
	public const int NULL_ID = -1;
	
	// A unique identifier to track this ArTouch over multiple callbacks
	public int id;
	// The ArTouch's current pixel coordinates on the game window
	public Vector2 position;
	// How far the ArTouch has moved since the last update
	public Vector2 deltaPosition;
	// The coordinates that the ArTouch began at
	public Vector2 startPosition;
	// The system time when this ArTouch began
	public float startTime;
	
	// Whether the ArTouch has moved far enough from its startPosition to
	// throw movement events instead of stationary events
	public bool hasMoved;
	// Stops this ArTouch from throwing events
	public bool isDead;
	
	// Basic constructor
	public ArTouch (int id, Vector2 pos)
	{
		this.id = id;
		position = pos;
		deltaPosition = Vector2.zero;
		startPosition = pos;
		startTime = Time.time;
		hasMoved = false;
		isDead = false;
	}
	
	public float GetAge()
	{
		return (Time.time - startTime);	
	}
}