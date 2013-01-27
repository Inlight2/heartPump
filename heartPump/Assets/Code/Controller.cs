using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public enum State
    {
        Dead,
        Alive
    };

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };

    public enum DirectionAttack
    {
        Left,
        Right
    };

    public float xOffset, yOffset;

    public State myState;
    public Direction myDirection;
    public DirectionAttack myDirectAttack;
    public GameObject myHpBar;
    public GameObject myMpBar;
    public GameObject myPwrUp;
    public GameObject myPump;
    public GameObject myWeapon;
    public GameObject pushPrefab;
    public UILabel myScore;

    private GameObject _theCollided;
    private Player _myPlayer;
    private Transform _myTransform;
    private tk2dSprite _myPumpSprite;

    private string _myID;
    private float _speed = 3.0f;
    private float _translateX;
    private float _translateY;
    private bool _isPumping = false;

    const float RIGHT_X_CONSTRAINT = 2.5f;
    const float LEFT_X_CONSTRAINT = -2.4f;
    const float UP_Y_CONSTRAINT = 2.7f;
    const float DOWN_Y_CONSTRAINT = -0.6f;
    

	void Start () 
    {
        myState = State.Alive;
        _myID = this.gameObject.name;
        _myTransform = this.transform;
        _myPlayer = this.GetComponent<Player>();
        _myPumpSprite = myPump.GetComponent<tk2dSprite>();

        myHpBar.SetActive(true);
        myMpBar.SetActive(true);
        myPwrUp.SetActive(true);
	}
	
	void Update ()
    {
        DoHUD();
        DoScore();

        if (myState == State.Alive)
        {
            if (_isPumping == false)
            {
                DoMovement();
            }

            if (Input.GetButtonDown(_myID + ": A Button"))
            {
                if (_theCollided != null)
                {
                    if (_theCollided.gameObject.name == "Heart")
                    {
                        if (_isPumping == false)
                        {
                            _isPumping = true;
                            Invoke("CooldownPump", .5f);
                            DoPump();
                        }
                        else
                        {
                            CancelInvoke("CooldownPump");
                            Invoke("CooldownPump", .5f);
                            DoPump();
                        }
                    }
                    else
                    {
                        DoAttack();
                    }
                }
				else
                {
                    DoAttack();
                }
            }
            else if (Input.GetButtonDown(_myID + ": B Button") && _isPumping == false)
            {
                DoPush();
            }
        }
        else
        {
            Debug.Log(_myID + " is Dead.");
        }
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

        #region Grab Direction
        if (Input.GetAxis(_myID + ": Horizontal") > 0f)
        {
            myDirection = Direction.Right;
            myDirectAttack = DirectionAttack.Right;
        }
        else if (Input.GetAxis(_myID + ": Horizontal") < 0f)
        {
            myDirection = Direction.Left;
            myDirectAttack = DirectionAttack.Left;
        }
        else if (Input.GetAxis(_myID + ": Vertical") > 0f)
        {
            myDirection = Direction.Up;
        }
        else if (Input.GetAxis(_myID + ": Vertical") < 0f)
        {
            myDirection = Direction.Down;
        }
        #endregion

    }

    private void DoPump()
    {
        _myPlayer.Pump();
        if (_myPumpSprite.spriteId == _myPumpSprite.GetSpriteIdByName("pumpUp"))
        {
            _myPumpSprite.spriteId = _myPumpSprite.GetSpriteIdByName("pumpDown");
        }
        else
        {
            _myPumpSprite.spriteId = _myPumpSprite.GetSpriteIdByName("pumpUp");
        }
    }

    private void CooldownPump()
    {
        _isPumping = false;
    }

    private void DoAttack()
    {
		GameObject clone;
		clone = (GameObject)Instantiate(myWeapon, _myTransform.position, Quaternion.identity);
		clone.GetComponent<Weapon>().setTransform(_myPlayer.transform);
		if(myDirectAttack == DirectionAttack.Left)
		{
			clone.GetComponent<Weapon>().SetLeft();
		}
		else clone.GetComponent<Weapon>().SetRight();
    }

    private void DoPush()
    {
        switch (myDirection)
        {
            case Direction.Up:
                Instantiate(pushPrefab, new Vector3(_myTransform.position.x, _myTransform.position.y + .5f, _myTransform.position.z), Quaternion.Euler(0, 0, 0));
                break;
            case Direction.Down:
                Instantiate(pushPrefab, new Vector3(_myTransform.position.x, _myTransform.position.y - .5f, _myTransform.position.z), Quaternion.Euler(0, 0, 180));
                break;
            case Direction.Left:
                Instantiate(pushPrefab, new Vector3(_myTransform.position.x - .5f, _myTransform.position.y, _myTransform.position.z), Quaternion.Euler(0, 0, 90));
                break;
            case Direction.Right:
                Instantiate(pushPrefab, new Vector3(_myTransform.position.x + .5f, _myTransform.position.y, _myTransform.position.z), Quaternion.Euler(0, 0, 270));
                break;
            default:
                break;

        }
    }

    private void DoHUD()
    {
        TriggerHUD();
        myHpBar.transform.position = new Vector3(_myTransform.position.x - .27f, _myTransform.position.y - .35f, _myTransform.position.z);
        myMpBar.transform.position = new Vector3(_myTransform.position.x - .14f, _myTransform.position.y - .46f, _myTransform.position.z);
        myPwrUp.transform.position = new Vector3(_myTransform.position.x - .15f, _myTransform.position.y + .29f, _myTransform.position.z);
        myPump.transform.position = new Vector3(_myTransform.position.x - .07f, _myTransform.position.y - .11f, _myTransform.position.z);
        myHpBar.GetComponent<UISlider>().sliderValue = (float)(_myPlayer.health / 3f);
    }

    private void TriggerHUD()
    {
        if (myState == State.Alive)
        {
            myHpBar.SetActive(true);
            myMpBar.SetActive(true);
            myPwrUp.SetActive(true);
        }
        if (myState == State.Dead)
        {
            myHpBar.SetActive(false);
            myMpBar.SetActive(false);
            myPwrUp.SetActive(false);
        }
    }

    private void DoScore()
    {
        switch (_myID)
        {
            case "Player 1":
                myScore.text = God.getScore(1).ToString();
                break;
            case "Player 2":
                myScore.text = God.getScore(2).ToString();
                break;
            case "Player 3":
                myScore.text = God.getScore(3).ToString();
                break;
            case "Player 4":
                myScore.text = God.getScore(4).ToString();
                break;
            default:
                Debug.Log("DoScore() on a DEFAULT!!!");
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _theCollided = collision.gameObject;

        if (collision.gameObject.name == "Heart")
        {
            myPump.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Heart")
        {
            myPump.SetActive(false);
        }
        _theCollided = null;
    }
}
