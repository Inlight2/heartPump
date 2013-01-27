using UnityEngine;
using System.Collections;

public class HealthUp : PowerUp {

	public override void use(string player)
	{
		owner = GameObject.FindWithTag(player);
	}
}
