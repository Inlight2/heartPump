using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
	private float _spawnTimer = 2f;
	private Vector3 _spawnLocation;
	public bool isDead = false;
	private Transform _transform;

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
	}
	
	IEnumerator Do()
	{
		yield return new WaitForSeconds(_spawnTimer);
		Spawn();
		isDead = false;
	}
}
