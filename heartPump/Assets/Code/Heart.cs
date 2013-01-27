using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour {
	private int _maxHealth = 200;
	private int _curHealth = 0;
	private int _multiplyer = 37;
	private int _playersPumping = 0;
	
	
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public int Pump()
	{
		_curHealth++;
		if(_curHealth > _maxHealth)
		{
			Death();
		}
		return _multiplyer;
	}
	
	public int Pump(int dmg)
	{
		_curHealth += dmg;
		if(_curHealth >  _maxHealth)
		{
			Death ();
		}
		return dmg * _multiplyer;
	}
	
	private void Death()
	{
		//TODO death things
	}
	
	private void OnCollisionEnter(Collision collisionInfo)
	{
		if(Equals(collisionInfo.gameObject.tag, "Player"))
		{
			_playersPumping++;
		}
	}
	
	private void OnCollisionExit(Collision collisionInfo)
	{
		if(Equals(collisionInfo.gameObject.tag, "Player"))
		{
			_playersPumping--;
		}
	}
	
}
