using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

	public class PowerUp
	{
		public int size;
		public int powerid;

		public PowerUp(int s, int p)
		{
			size = s;
			powerid = p;
		}//end constructor powerup
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

	public void addPowerUp(int size, int powerid)
	{
		PowerUp newpower = new PowerUp(size, powerid);
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

	public void increaseMeterSize(int i)
	{
		numofblocks += i;
	}//end increaseMeterSize

	int spillover = 0;
	void updateMeterfilled()
	{
		meterfilled += xa.experiencepoints;
		xa.experiencepoints = 0;

		//meterceiling check
		if(meterfilled/500>=numofblocks)
		{
			spillover = meterfilled - numofblocks*500;//calculates xp spillover
			meterfilled=numofblocks*500;//makes it so that meterfilled doesnt go over
		}
		//Debug.Log(meterfilled);
	}//end updateMeterfilled

	void updateBlocksFilled()
	{
		blocksfilled = meterfilled/500;
	}//end updateBlocksFilled

}//end class
