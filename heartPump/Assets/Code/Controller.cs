using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {


    //Calvin Harris 18 months
    private tk2dBaseSprite theSprite;
    private string myID;
    private float speed = 3.0f;
    

	// Use this for initialization
	void Start () 
    {
        theSprite = this.GetComponent<tk2dBaseSprite>();
        myID = this.gameObject.name;
	}
	
	// Update is called once per frame
	void Update () 
    {
        float translateX = Input.GetAxis(myID + ": Horizontal") * speed * Time.deltaTime;
        float translateY = Input.GetAxis(myID + ": Vertical") * speed * Time.deltaTime;

        transform.Translate(translateX, translateY, 0);

        if (Input.GetButtonDown(myID + ": A Button"))
        {
            theSprite.spriteId = 2;
        }

        else if (Input.GetButtonDown(myID + ": B Button"))
        {
            theSprite.spriteId = 1;
        }

        else
        {
            theSprite.spriteId = 0;
        }
	}
}
