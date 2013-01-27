using UnityEngine;
using System.Collections;

public class Watch : Weapon {

	void Update ()
	{
		Spin();
		if(_angleTurned > 180)
		{
			Destroy(this.gameObject);
		}
	}
	
	new public void SetLeft()
	{
		_transfrom.position += new Vector3(0.25f,0f,0f);
	}
	
	new public void SetRight()
	{
		_rotationSpeed *= -1;
		_transfrom.position += new Vector3(-0.15f,0f,0f);
	}
	
	void OnTriggerEnter(Collider other) {
       	if(!string.Equals(other.name, "Player 2") && string.Equals(other.tag, "Player"))
		{
			God.DamagePlayer(other.name.Substring(other.name.Length - 1), 1);
		}
    }
}
