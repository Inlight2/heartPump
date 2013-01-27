using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour {
	protected GameObject owner;
	
	public abstract void use(string player);
}
