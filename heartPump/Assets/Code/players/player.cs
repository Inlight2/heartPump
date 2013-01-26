using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public int health;
	public int magic;
	public int score;
	private spawner _spawn;
	private Controller _control;
	private bool _dead = false;
	//private Controller control;
	
	void Start ()
	{
		health = 1000;
		magic = 100;
		score = 0;
		_spawn = GetComponent<spawner>();
		_spawn.SetUp();
		_control = GetComponent<Controller>();
	}
	
	void Update ()
	{
		if (Input.GetButtonDown("Player 1" + ": A Button"))
            {
                health  = 0;
            }
		
		if(health <= 0 && !_dead)//TODO && not dead
		{
			health = 0;
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
		health = 1000;
		_control.myState = Controller.State.Alive;
		_dead = false;
	}
	
	//takes no more damage when dead, also can't be pushed
}
