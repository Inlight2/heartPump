using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	public GameObject item;
	public PowerUp[] itemList;
	private int _length;
	private bool _spawnNow = false;
	private int _coolDown = 10;
	public GameObject theCage;
	
	
	// Use this for initialization
	void Start () {
		
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
		Vector3 randomSpot = new Vector3((Random.value * 4.9f) - 2.45f,(Random.value * 3.3f) - 1.65f,-3f);
		Instantiate(item, randomSpot, Quaternion.identity);
	}
	
	private IEnumerator spawntimer()
	{
		yield return new WaitForSeconds(_coolDown);
		_spawnNow = true;
	}
	
	public PowerUp randomItem()
	{
		int random = (int)Mathf.Round ((itemList.Length - 1) * Random.value);
		return itemList[random];
	}
}
