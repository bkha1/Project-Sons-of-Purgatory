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

	public static int metersize = 0;//total size of the meter
	public static int meterused = 0;//meter occupied by the powerups
	public static int meterfilled = 0;//meter filled by player's triscuit chips

	public static ArrayList powermeter = new ArrayList();//holds all the player's powerups

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addPowerUp(int size, int powerid)
	{
		PowerUp newpower = new PowerUp(size, powerid);
		int spaceleft = metersize - meterused;

		if(spaceleft >= size)
		{
			powermeter.Add(newpower);
			meterused +=size;
		}
		else
		{
			Debug.Log("NOT ENOUGH METER SPACE!");
		}
	}//end addPowerUp

	public void increaseMeterSize(int i)
	{
		metersize += i;
	}//end increaseMeterSize

	void updateMeterfilled()
	{
		meterfilled = xa.experiencepoints;
	}//end updateMeterfilled

}//end class
