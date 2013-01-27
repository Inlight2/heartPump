using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour {
	private int _maxHealth = 200;
	private int _curHealth = 0;
	private int _multiplyer = 37;
	private int _playersPumping = 0;
	private float _scaleUp = 1.009f;
	private float _scaleDown = 0.9995f;
	private Transform _transform;
	private float _startScale;
	//max scale is at 2.4f
	
	
	void Start ()
	{
		_transform = transform;
		_startScale = _transform.localScale.magnitude;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_transform.localScale.magnitude > _startScale)
			_transform.localScale *= _scaleDown;
	}
	
	public int Pump()
	{
		float tmpScale;
		_curHealth++;
		tmpScale =_scaleUp; // _playersPumping;
		_transform.localScale *= tmpScale;
		if(_curHealth > _maxHealth)
		{
			Death();
		}
		return _multiplyer;
	}
	
	public int Pump(int dmg)
	{
		Vector3 tmpScale;
		_curHealth += dmg;
		tmpScale = _transform.localScale * (_scaleUp * dmg); // _playersPumping;
		_transform.localScale = tmpScale;
		if(_curHealth >  _maxHealth)
		{
			Death ();
			_multiplyer = 0;
		}
		return dmg * _multiplyer;
	}
	
	private void Death()
	{
		gameObject.SetActive (false);
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
