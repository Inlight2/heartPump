using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	private int health;
	private int magic;
	private int score;
	private spawner spawn;
	private Controller control;
	//private Controller control;
	
	void Start ()
	{
		health = 1000;
		magic = 100;
		score = 0;
		spawn = GetComponent<spawner>();
		spawn.SetUp();
		control = GetComponent<Controller>();
	}
	
	void Update ()
	{
		if(health < 0)//TODO && not dead
		{
			health = 0;
			Death();
		}
	}
	
	private void Death()
	{
		//TODO: figure out what happens when the player dies
	}
	
	//takes no more damage when dead, also can't be pushed
}
