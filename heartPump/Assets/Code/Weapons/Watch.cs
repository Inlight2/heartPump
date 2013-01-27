using UnityEngine;
using System.Collections;

public class Watch : Weapon {
	
	void Start ()
	{
		_transfrom = transform;
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
		_rotationSpeed *= -1;
		//_transfrom.Translate(new Vector3(0.15f,0f,0f));
	}
	
	public override void SetRight()
	{
		//_transfrom.Translate(new Vector3(-0.3f,0f,0f));
	}
	
	void OnTriggerEnter(Collider other) {
       	if(!string.Equals(other.name, "Player 2") && string.Equals(other.tag, "Player"))
		{
			God.DamagePlayer(other.name.Substring(other.name.Length - 1), 1);
		}
    }
}
