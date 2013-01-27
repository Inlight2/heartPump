using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public int health;
	public int magic;
	public int score;
	public bool item;
	
	public PowerUp nickyKage;
	
	private Spawner _spawn;
	private Controller _control;
	private bool _dead = false;
	private int _maxHealth = 3;
	private int _maxMagic = 3;
	
	//private Controller control;
	
	void Start ()
	{
		health = _maxHealth;
		magic = _maxMagic;
		score = 0;
		_spawn = GetComponent<Spawner>();
		_spawn.SetUp();
		_control = GetComponent<Controller>();
	}
	
	void Update ()
	{
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
		health = _maxHealth;
		magic = _maxMagic;
		_control.myState = Controller.State.Alive;
		_dead = false;
	}
	
	public void useItem()
	{
		if(item)
		{
			nickyKage.Use();
			item = false;
		}
	}
	
	public void Pump()
	{
		score += God.Pump();
	}
	
	public void Damage(int dmg)
	{
		health -= dmg;
	}
	
	//takes no more damage when dead, also can't be pushed
}
