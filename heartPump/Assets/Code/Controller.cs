using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public enum State
    {
        Dead,
        Alive
    };

    //Calvin Harris 18 months

    public State myState;

    private tk2dBaseSprite _theSprite;
    private GameObject _theCollided;
    private string _myID;
    private float _speed = 3.0f;
    
    const float RIGHT_X_CONSTRAINT = 2.5f;
    const float LEFT_X_CONSTRAINT = -2.4f;
    const float UP_Y_CONSTRAINT = 2.7f;
    const float DOWN_Y_CONSTRAINT = -0.6f;
    

	// Use this for initialization
	void Start () 
    {
        myState = State.Alive;
        _theSprite = this.gameObject.GetComponent<tk2dSprite>();
        _myID = this.gameObject.name;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (myState == State.Alive)
        {
            DoMovement();
            if (Input.GetButtonDown(_myID + ": A Button"))
            {
                DoAttack();
            }
            else if (Input.GetButtonDown(_myID + ": B Button"))
            {
                DoPush();
            }
        }
        else
        {
            Debug.Log(_myID + " is Dead.");
        }

        if (_myID == "Player 1")
        Debug.Log(_myID + " is colliding with " + _theCollided);
	}

    private void DoMovement()
    {
        float translateX = Input.GetAxis(_myID + ": Horizontal") * _speed * Time.deltaTime;
        float translateY = Input.GetAxis(_myID + ": Vertical") * _speed * Time.deltaTime;

        transform.Translate(translateX, translateY, 0);

        #region Movement Constraints
        if (transform.position.x >= RIGHT_X_CONSTRAINT)
        {
            transform.position = new Vector3(RIGHT_X_CONSTRAINT, transform.position.y, transform.position.z);
        }

        if (transform.position.x <= LEFT_X_CONSTRAINT)
        {
            transform.position = new Vector3(LEFT_X_CONSTRAINT, transform.position.y, transform.position.z);
        }

        if (transform.position.y >= UP_Y_CONSTRAINT)
        {
            transform.position = new Vector3(transform.position.x, UP_Y_CONSTRAINT, transform.position.z);
        }

        if (transform.position.y <= DOWN_Y_CONSTRAINT)
        {
            transform.position = new Vector3(transform.position.x, DOWN_Y_CONSTRAINT, transform.position.z);
        }
        #endregion
    }

    private void DoAttack()
    {

    }

    private void DoPush()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        _theCollided = collision.gameObject;
    }

    private void OnCollisionExit(Collision collision)
    {
        _theCollided = null;
    }
}
