using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour {
	protected GameObject owner;
	
	public abstract void Use();
	
	void OnCollisionEnter(Collision collisionInfo)
	{
		if(string.Equals(collisionInfo.gameObject.tag, "Player"))
		{
			God.giveItem(collisionInfo.gameObject.name);
		}
	}
}
