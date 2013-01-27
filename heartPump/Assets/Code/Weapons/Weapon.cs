using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	protected float _angleTurned;
	protected float _rotationSpeed = 15f;
	protected Transform _transfrom;
	
	void Start ()
	{
		_transfrom = transform;
	}
	
	public void Spin()
	{
		_transfrom.Rotate(new Vector3(0f,0f,_rotationSpeed));
		_angleTurned += _rotationSpeed;
	}
	
	public void SetLeft()
	{
		_transfrom.Rotate(new Vector3(0f,0f,270f));
	}
	
	public void SetRight()
	{
		_transfrom.Rotate(new Vector3(0f,0f,90f));
	}
	
}
