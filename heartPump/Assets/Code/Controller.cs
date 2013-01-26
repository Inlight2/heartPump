using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public enum State
    {
        Dead,
        Alive
    };

    public State myState;
    public GameObject myHpBar;
    public GameObject myMpBar;

    private GameObject _theCollided;
    private Transform _myTransform;
    private string _myID;
    private float _speed = 3.0f;
    private float _translateX;
    private float _translateY;
    private float _myPosX;
    private float _myPosY;
    private float _myPosZ;
    
    const float RIGHT_X_CONSTRAINT = 2.5f;
    const float LEFT_X_CONSTRAINT = -2.4f;
    const float UP_Y_CONSTRAINT = 2.7f;
    const float DOWN_Y_CONSTRAINT = -0.6f;
    

	// Use this for initialization
	void Start () 
    {
        myState = State.Alive;
        _myID = this.gameObject.name;
        _myTransform = this.transform;
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

        DoHUD();

        if (_myID == "Player 1")
        Debug.Log(_myID + " is colliding with " + _theCollided);
	}

    private void DoMovement()
    {
        _translateX = Input.GetAxis(_myID + ": Horizontal") * _speed * Time.deltaTime;
        _translateY = Input.GetAxis(_myID + ": Vertical") * _speed * Time.deltaTime;

        _myTransform.Translate(_translateX, _translateY, 0);

        #region Movement Constraints
        if (_myTransform.position.x >= RIGHT_X_CONSTRAINT)
        {
            _myTransform.position = new Vector3(RIGHT_X_CONSTRAINT, _myTransform.position.y, _myTransform.position.z);
        }

        if (_myTransform.position.x <= LEFT_X_CONSTRAINT)
        {
            _myTransform.position = new Vector3(LEFT_X_CONSTRAINT, _myTransform.position.y, _myTransform.position.z);
        }

        if (_myTransform.position.y >= UP_Y_CONSTRAINT)
        {
            _myTransform.position = new Vector3(_myTransform.position.x, UP_Y_CONSTRAINT, _myTransform.position.z);
        }

        if (_myTransform.position.y <= DOWN_Y_CONSTRAINT)
        {
            _myTransform.position = new Vector3(_myTransform.position.x, DOWN_Y_CONSTRAINT, _myTransform.position.z);
        }
        #endregion
    }

    private void DoAttack()
    {

    }

    private void DoPush()
    {

    }

    private void DoHUD()
    {
        myHpBar.transform.position = new Vector3(_myTransform.position.x - .27f, _myTransform.position.y - .35f, _myTransform.position.z);
        myMpBar.transform.position = new Vector3(_myTransform.position.x - .14f, _myTransform.position.y - .46f, _myTransform.position.z);
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
