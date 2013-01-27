using UnityEngine;
using System.Collections;

public class SwordFish : Weapon {
	
	void Start ()
	{
		_transfrom = transform;
		_transfrom.Rotate(new Vector3(0f,0f,45f));
	}
	
	void Update ()
	{
		Spin();
		if(_angleTurned > 180 || _angleTurned < -180)
		{
			Destroy(this.gameObject);
		}
		_transfrom.position = _playerTransform.position;
	}
	
	public override void SetLeft()
	{
		//_transfrom.Translate(new Vector3(0.15f,0f,0f));
	}
	
	public override void SetRight()
	{
		_rotationSpeed *= -1;
		//_transfrom.Translate(new Vector3(-0.3f,0f,0f));
	}
	
	void OnTriggerEnter(Collider other) {
       	if(!string.Equals(other.name, God.player1.name) && string.Equals(other.tag, "Player"))
		{
			God.DamagePlayer(other.name.Substring(other.name.Length - 1), 1);
		}
    }
}
