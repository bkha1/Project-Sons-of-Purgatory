using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

	public class PowerUp
	{
		public int size;
		//public int powerid;
		public powers pow;

		//enums? to see which powerup it is?
		public enum powers
		{
			None,
			ExtraGun,
			SpreadShot
		}

		public PowerUp(int s, powers p)
		{
			size = s;
			//powerid = p;
			pow = p;
		}//end constructor powerup

		//constructor for just specifying the power, size autodecided
		public PowerUp(powers p)
		{
			pow = p;
			size = 1;
		}

	}//end class PowerUp

	public static int numofblocks = 4;//number of energy blocks in the meter
	public static int blocksused = 0;//meter occupied by the powerups
	public static int meterfilled = 0;//meter filled by player's triscuit chips aka xp
	public static int blocksfilled = 0;//numberofblocks fully filled by triscuits

	public static ArrayList powermeter = new ArrayList();//holds all the player's powerups

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		updateMeterfilled();
		updateBlocksFilled();
	}

	public void addPowerUp(int size, PowerUp.powers power)//int powerid)
	{
		PowerUp newpower = new PowerUp(size, power);//powerid);
		int spaceleft = numofblocks - blocksused;

		if(spaceleft >= size)
		{
			powermeter.Add(newpower);
			blocksused +=size;
		}
		else
		{
			Debug.Log("NOT ENOUGH METER SPACE!");
		}
	}//end addPowerUp

	//for second PowerUp constructor
	public void addPowerUp(PowerUp.powers power)
	{
		PowerUp newpower = new PowerUp(power);
		int spaceleft = numofblocks - blocksused;
		
		if(spaceleft >= 1)
		{
			powermeter.Add(newpower);
			blocksused +=1;
		}
		else
		{
			Debug.Log("NOT ENOUGH METER SPACE!");
		}
	}//end addPowerUp

	public void increaseMeterSize(int i)
	{
		numofblocks += i;
	}//end increaseMeterSize

	int spillover = 0;
	void updateMeterfilled()
	{
		meterfilled = xa.experiencepoints;
		//xa.experiencepoints = 0;

		//meterceiling check
		if(meterfilled/500>=numofblocks)
		{
			spillover = meterfilled - numofblocks*500;//calculates xp spillover
			meterfilled=numofblocks*500;//makes it so that meterfilled doesnt go over

			xa.experiencepoints = meterfilled;//cap xp
			//Debug.Log("spillover: " + spillover);
			spillover = 0;//just in case
		}
		//Debug.Log(meterfilled);
	}//end updateMeterfilled

	void updateBlocksFilled()
	{
		blocksfilled = meterfilled/500;
	}//end updateBlocksFilled

}//end class
