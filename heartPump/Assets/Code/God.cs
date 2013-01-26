using UnityEngine;
using System.Collections;

public class God : MonoBehaviour
{
	static public Player player1;
	static public Player player2;
	static public Player player3;
	static public Player player4;
	
	void Start()
	{
		player1 = GameObject.FindWithTag("Player1").GetComponent<Player>();
		player2 = GameObject.FindWithTag("Player2").GetComponent<Player>();
		player3 = GameObject.FindWithTag("Player3").GetComponent<Player>();
		player4 = GameObject.FindWithTag("Player4").GetComponent<Player>();
	}
	
	void Update ()
	{
		
	}
	
	static public void giveScore(int playerNumber, int score)
	{
		switch(playerNumber)
		{
		case 1:
			player1.score += score;
			break;
		case 2:
			player2.score += score;
			break;
		case 3:
			player3.score += score;
			break;
		case 4:
			player4.score += score;
			break;
		}
	}
	
	static public int getScore(int playerNumber)
	{
		switch(playerNumber)
		{
			case 1:
				return player1.score;
			case 2:
				return player2.score;
			case 3:
				return player3.score;
			case 4:
				return player4.score;
			default: 
				return 0;
		}
	}
	
	static public int getHealth(int playerNumber)
	{
		switch(playerNumber)
		{
			case 1:
				return player1.health;
			case 2:
				return player2.health;
			case 3:
				return player3.health;
			case 4:
				return player4.health;
			default:
				return 0;
		}
	}
	
	static public int getMagic(int playerNumber)
	{
		switch(playerNumber)
		{
			case 1:
				return player1.magic;
			case 2:
				return player2.magic;
			case 3:
				return player3.magic;
			case 4:
				return player4.magic;
			default:
				return 0;
		}
	}
}
