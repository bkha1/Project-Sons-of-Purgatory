using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	
	//private string lvlname;
	//private string tempname;
	
	//public int numoflevels;
	public int whichside;
	
	//static ArrayList lvlbank = new ArrayList();//bank of all the levels to be used
	//static ArrayList usedlvls = new ArrayList();//list of all the used levels within this dungeon
	//static bool levelsloadedintobank = false;

	// Use this for initialization
	void Start ()
	{
		/*
		lvlname = "EMPTY";
		while(lvlname.Equals("EMPTY"))
		{
			tempname = "level" + Random.Range(1,numoflevels + 1);
			if(usedlvls.ToArray().Length == 0)
			{
				lvlname = tempname;
				usedlvls.Add(tempname);
			}
			else
			{
				
			}
			
		}
		*/
		//lvlname = "level" + Random.Range(1,5);
		//Debug.Log(lvlname);
		
		//Debug.Log (levelsloadedintobank);
		
		/*if(!levelsloadedintobank)
		{
			for(int i = 1; i<numoflevels + 1;i++)
			{
				tempname = "level" + i;
				lvlbank.Add(tempname);
				Debug.Log(tempname + " loaded into lvlbank");
			}
			
			levelsloadedintobank = true;
		}
		
		
		if(lvlbank.ToArray().Length == 0)
		{
			Debug.Log("level bank is empty");
		}
		else
		{
			int randomlvlindex = Random.Range (0,lvlbank.ToArray().Length);//randomly chooses lvl
			lvlname = (string)lvlbank[randomlvlindex];
			lvlbank.RemoveAt(randomlvlindex);//removes lvl from the bank
			usedlvls.Add(lvlname);//adds the used level to the usedlvls list
		}*/
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//check which sides are available to show doors
		if(whichside==1)
		{
			if(xa.northroom==-1)
			{this.gameObject.SetActive(false);}
		}
		else if(whichside==2)
		{
			if(xa.eastroom==-1)
			{this.gameObject.SetActive(false);}
		}
		else if(whichside==3)
		{
			if(xa.southroom==-1)
			{this.gameObject.SetActive(false);}
		}
		else if(whichside==4)
		{
			if(xa.westroom==-1)
			{this.gameObject.SetActive(false);}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		//bug:holding mouse0 while loading doesnt let you shoot in the later scenes
		/*if(other.gameObject.CompareTag("Player"))
		{
			if(lvlname.Equals("EMPTY") == false)
			{
				Debug.Log(lvlname);
				Application.LoadLevel(lvlname);
			}
		}*/
		
		if(other.gameObject.CompareTag("Player"))
		{
			if(whichside==1)//north
			{xa.playerexitdirection=1;}
			else if(whichside==2)//east
			{xa.playerexitdirection=2;}
			else if(whichside==3)//south
			{xa.playerexitdirection=3;}
			else if(whichside==4)//west
			{xa.playerexitdirection=4;}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			xa.playerexitdirection=0;
		}
	}
}
