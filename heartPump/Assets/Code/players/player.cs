using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	private int health;
	private int magic;
	private int score;
	private spawner spawn;
	//private Controller control;
	
	void Start ()
	{
		health = 1000;
		magic = 100;
		score = 0;
		spawn = GetComponent<spawner>();
	}
	
	void Update ()
	{
		if(health < 0)
		{
			Death();
		}
	}
	
	private void Death()
	{
		//TODO: figure out what happens when the player dies
		spawn.lockUp();
	}
}
