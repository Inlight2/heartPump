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

    private tk2dBaseSprite theSprite;
    private string myID;
    private float speed = 3.0f;
    
    const float RIGHT_X_CONSTRAINT = 1.2f;
    const float LEFT_X_CONSTRAINT = -1.2f;
    const float UP_Y_CONSTRAINT = 1.8f;
    const float DOWN_Y_CONSTRAINT = 0.2f;
    

	// Use this for initialization
	void Start () 
    {
        myState = State.Alive;
        theSprite = this.gameObject.GetComponent<tk2dSprite>();
        myID = this.gameObject.name;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (myState == State.Alive)
        {
            doMovement();
            if (Input.GetButtonDown(myID + ": A Button"))
            {
                doAttack();
            }
            else if (Input.GetButtonDown(myID + ": B Button"))
            {
                doPush();
            }
        }
        else
        {
            Debug.Log(myID + " is Dead.");
        }
	}

    private void doMovement()
    {
        float translateX = Input.GetAxis(myID + ": Horizontal") * speed * Time.deltaTime;
        float translateY = Input.GetAxis(myID + ": Vertical") * speed * Time.deltaTime;

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

    private void doAttack()
    {
    }

    private void doPush()
    {
    }
}
