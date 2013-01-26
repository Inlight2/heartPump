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
			Spawn();
		}
		if (Input.GetButtonDown("Player 1" + ": A Button"))
        {
           StartCoroutine(Do());
        }
	}
	
	public void SetUp()
	{
		_transform = transform;
		_spawnLocation = _transform.position;
	}
	
	private void Spawn()
	{
		_transform.position = _spawnLocation;
	}
	
	public void lockUp()
	{
		_locked = true;
		
	}
	
	IEnumerator Do()
	{
		yield return new WaitForSeconds(2);
		_transform.position = _spawnLocation;
	}
	
}
