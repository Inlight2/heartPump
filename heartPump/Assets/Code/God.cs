using UnityEngine;
using System.Collections;

public class God : MonoBehaviour
{
	static public Player player1;
	static public Player player2;
	static public Player player3;
	static public Player player4;
	static public Heart heart;
	static public Manager manager;
	
	void Start()
	{
		player1 = GameObject.Find("Player 1").GetComponent<Player>();
		player2 = GameObject.Find("Player 2").GetComponent<Player>();
		player3 = GameObject.Find("Player 3").GetComponent<Player>();
		player4 = GameObject.Find("Player 4").GetComponent<Player>();
		heart = GameObject.Find("Heart").GetComponent<Heart>();
		manager = GetComponent<Manager>();
	}
	
	void Update ()
	{
		
	}
	
	static public void giveItem(string playerNumber)
	{
		switch(playerNumber)
		{
		case "1":
			player1.item = true;
			break;
		case "2":
			player2.item = true;
			break;
		case "3":
			player3.item = true;
			break;
		case "4":
			player4.item = true;
			break;
		default:
			break;
		}
	}
	
	static public int Pump()
	{
		return heart.Pump();
	}
	
	static public int Pump(int dmg)
	{
		return heart.Pump(dmg);
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
	
	static public void DamagePlayer(int playerNumber, int dmg)
	{
		switch(playerNumber)
		{
		case 1:
			player1.Damage(dmg);
			break;
		case 2:
			player2.Damage(dmg);
			break;
		case 3:
			player3.Damage(dmg);
			break;
		case 4:
			player4.Damage(dmg);
			break;
		default:
			break;
		}
	}
	
	static public void DamagePlayer(string playerNumber, int dmg)
	{
		switch(playerNumber)
		{
		case "1":
			player1.Damage(dmg);
			break;
		case "2":
			player2.Damage(dmg);
			break;
		case "3":
			player3.Damage(dmg);
			break;
		case "4":
			player4.Damage(dmg);
			break;
		default:
			break;
		}
	}
}
