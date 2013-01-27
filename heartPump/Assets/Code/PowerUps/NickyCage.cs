using UnityEngine;
using System.Collections;

public class NickyCage : PowerUp {
	
	private Vector3 _origin = new Vector3(0.05f,1.05f,-3.1f);
	
	public override void Use ()
	{
		Instantiate(God.manager.theCage, _origin, Quaternion.identity);
	}
}
