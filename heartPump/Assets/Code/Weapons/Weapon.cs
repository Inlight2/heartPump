using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
	protected float _angleTurned;
	protected float _rotationSpeed = 5f;
	protected Transform _transfrom;
	protected Transform _playerTransform;
	
	public void Spin()
	{
		_transfrom.Rotate(new Vector3(0f,0f,_rotationSpeed));
		_angleTurned += _rotationSpeed;
	}
	
	public void setTransform(Transform tmpTransform)
	{
		_playerTransform = tmpTransform;
	}
	
	public abstract void SetLeft();
	
	public abstract void SetRight();
	
}
