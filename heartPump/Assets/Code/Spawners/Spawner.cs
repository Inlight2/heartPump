using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	private float _spawnTimer = 2f;
	private Vector3 _spawnLocation;
	public bool isDead = false;
	private Transform _transform;
	private Vector3 _deathSpot = new Vector3(-2f,2f);

	void Start ()
	{
		_transform = transform;
	}
	
	void Update ()
	{
		if(isDead)
		{
			
		}
		else
		{
			
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
	
	public void Death()
	{
		isDead = true;
		StartCoroutine(Do());
		_transform.position = _deathSpot;
	}
	
	IEnumerator Do()
	{
		yield return new WaitForSeconds(_spawnTimer);
		Spawn();
		isDead = false;
	}
}
