using UnityEngine;
using System.Collections;

public class TheCage : MonoBehaviour {
	private float _scaleSpeed = 1.01f;
	private Transform _transform;
	private Vector3 _rotating = new Vector3(0f,0f,5f);
	
	private bool spin = false;
	
	public float stageOne = 4;
	public float stageTwo = 5;
	public float stageThree = 1;
	
	void Start()
	{
		StartCoroutine(waitA());
		_transform = transform;
		
	}
	
	void Update ()
	{
		transform.localScale *= _scaleSpeed;
		if(spin)
		{
			_transform.Rotate(_rotating);
		}
	}
	
	IEnumerator waitA()
	{
		yield return new WaitForSeconds(stageOne);
		spin = true;
		StartCoroutine(waitB());
	}
	
	IEnumerator waitB()
	{
		yield return new WaitForSeconds(stageTwo);
		God.player1.gameObject.GetComponent<Controller>().setId("Player 2");
		God.player2.gameObject.GetComponent<Controller>().setId("Player 3");
		God.player3.gameObject.GetComponent<Controller>().setId("Player 4");
		God.player4.gameObject.GetComponent<Controller>().setId("Player 1");
		Debug.Log("THE CAGGGGGGEEEEEEEEEEEEE");
		StartCoroutine(waitC());
	}
	
	IEnumerator waitC()
	{
		yield return new WaitForSeconds(stageThree);
		Destroy(this.gameObject);
	}
}
