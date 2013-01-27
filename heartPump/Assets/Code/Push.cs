using UnityEngine;
using System.Collections;

public class Push : MonoBehaviour {

    private Transform _myTransform;

    const float RIGHT_X_CONSTRAINT = 3f;
    const float LEFT_X_CONSTRAINT = -3f;
    const float UP_Y_CONSTRAINT = 3f;
    const float DOWN_Y_CONSTRAINT = -1f;

    const float speed = 3f;

	void Start () 
    {
        _myTransform = transform;
	}
	
	void Update () 
    {
        _myTransform.Translate(Vector3.up * speed * Time.deltaTime);

        #region Movement Constraints
        if (_myTransform.position.x >= RIGHT_X_CONSTRAINT)
        {
            Destroy(this.gameObject);
        }

        if (_myTransform.position.x <= LEFT_X_CONSTRAINT)
        {
            Destroy(this.gameObject);
        }

        if (_myTransform.position.y >= UP_Y_CONSTRAINT)
        {
            Destroy(this.gameObject);
        }

        if (_myTransform.position.y <= DOWN_Y_CONSTRAINT)
        {
            Destroy(this.gameObject);
        }
        #endregion
	}

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.name == "Heart")
        {
            Destroy(this.gameObject);
        }
    }
}
