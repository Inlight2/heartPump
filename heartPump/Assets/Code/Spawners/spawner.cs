using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
	private float _spawnTimer;
	private Vector3 _spawnLocation;
	private bool _isDead = false;
	private Transform _transform;
	private bool _locked = false;

	void Start ()
	{
		_transform = transform;
	}
	
	void Update ()
	{
		if(_locked)
		{
			//TODO: decide how this is going to work
		}
	}
	
	public void SetUp(Vector3 start)
	{
		_spawnLocation = start;
		_transform = transform;
	}
	
	private void Spawn()
	{
		_transform.position = _spawnLocation;
	}
	
	public void lockUp()
	{
		_locked = true;
	}
}
