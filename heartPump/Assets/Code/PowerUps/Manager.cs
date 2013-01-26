using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	public PowerUp[] powersList;
	private int _length;
	private bool _spawnNow = false;
	private int _coolDown = 5;
	
	
	// Use this for initialization
	void Start () {
		_length = powersList.Length;
	}
	
	// Update is called once per frame
	void Update () {
		if(_spawnNow)
		{
			drop ();
			_spawnNow = false;
			StartCoroutine(spawntimer());
		}
	}
	
	void drop(){
		int random = (int)Mathf.Round(Random.value * _length);
		Vector3 randomSpot = new Vector3((Random.value * 4.9f) - 2.45f,(Random.value * 3.3f) - 1.65f,-3f);
		Instantiate(powersList[random], randomSpot, Quaternion.identity);
	}
	
	private IEnumerator spawntimer()
	{
		yield return new WaitForSeconds(_coolDown);
		_spawnNow = true;
	}
}
