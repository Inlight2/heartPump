using UnityEngine;
using System.Collections;

public class TheCage : MonoBehaviour {
	private float _scaleSpeed = 1.02f;
	private Transform _transform;
	private Vector3 _rotating = new Vector3(0f,0f,5f);
	
	private bool spin = false;
	
	public float stageOne = 4;
	public float stageTwo = 1;
	
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
		StartCoroutine(waitB());
		yield return new WaitForSeconds(stageOne);
		spin = true;
	}
	
	IEnumerator waitB()
	{	
		yield return new WaitForSeconds(stageTwo);
		
		Destroy(this.gameObject);
	}
}
