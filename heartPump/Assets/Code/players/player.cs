using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	private int _health;
	private int _magic;
	private int _score;
	private spawner _spawn;
	private Controller _control;
	private bool _dead = false;
	//private Controller control;
	
	void Start ()
	{
		_health = 1000;
		_magic = 100;
		_score = 0;
		_spawn = GetComponent<spawner>();
		_spawn.SetUp();
		_control = GetComponent<Controller>();
	}
	
	void Update ()
	{
		if (Input.GetButtonDown("Player 1" + ": A Button"))
            {
                _health  = 0;
            }
		
		if(_health <= 0 && !_dead)//TODO && not dead
		{
			_health = 0;
			Death();
		}
		
		if(_dead && !_spawn.isDead)
		{
			Live();
		}
	}
	
	private void Death()
	{
		//TODO: figure out what happens when the player dies
		_control.myState = Controller.State.Dead;
		_spawn.Death();
		_dead = true;
	}
	
	private void Live()
	{
		_health = 1000;
		_control.myState = Controller.State.Alive;
	}
	
	//takes no more damage when dead, also can't be pushed
}
