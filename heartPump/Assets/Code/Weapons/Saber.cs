using UnityEngine;
using System.Collections;

public class Saber : Weapon {

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
		
	}
	
	public override void SetRight()
	{
		_rotationSpeed *= -1;
	}
	
	void OnTriggerEnter(Collider other) {
       	if(!string.Equals(other.name, "Player 4") && string.Equals(other.tag, "Player"))
		{
			God.DamagePlayer(other.name.Substring(other.name.Length - 1), 1);
		}
    }
}
