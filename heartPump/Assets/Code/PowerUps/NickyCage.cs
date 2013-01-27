using UnityEngine;
using System.Collections;

public class NickyCage : MonoBehaviour {
	
	void OnCollisionEnter(Collision collisionInfo)
	{
		if(string.Equals(collisionInfo.gameObject.tag, "Player"))
		{
			God.giveItem(collisionInfo.gameObject.name.Substring(collisionInfo.gameObject.name.Length - 1));
			Destroy(this.gameObject);
		}
	}
}
