using UnityEngine;
using System.Collections;

public class DungeonPopulator : MonoBehaviour {
	
	private int[,] tempgrid = new int[16,16];
	private int[,] roomsgrid = new int[16,16];
	private int[,] doorsgrid = new int[16,16];
		
	public DungeonGenerator dungen;//dungeon generator
	public int numoflevels;
	private int numofspace = 0;
	
	private string lvlname;
	//private string tempname;
	
	private ArrayList lvlbank = new ArrayList();//bank of all the levels to be used
	private ArrayList usedlvls = new ArrayList();//list of all the used levels within this dungeon
	private bool levelsloadedintobank = false;
	
	// Use this for initialization
	void Start () {	
		//assignRooms();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void assignRooms()
	{	
		dungen = (DungeonGenerator) (this.gameObject.GetComponent("DungeonGenerator"));
		dungen.testPlaceBlocks();
		
		string grid = "";
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				//Debug.Log(floor[i,j]);
				tempgrid[i,j] = dungen.getValueInIndex(i,j);
				grid += " " + tempgrid[i,j];
				if(tempgrid[i,j]!=0)
				{
					numofspace++;
				}
			}//end for
			Debug.Log(grid);
			grid = "";
		}//end for
		
		if(!levelsloadedintobank)
		{
			for(int i = 1; i<numoflevels + 1;i++)
			{
				lvlbank.Add(i);
				Debug.Log("level"+ i + " loaded into lvlbank");
			}
			
			levelsloadedintobank = true;
		}
		
		int randomlvlindex;
		
		for(int i=0;i<16;i++)
		{
			for(int j=0;j<16;j++)
			{
				if(lvlbank.ToArray().Length == 0)
				{
					Debug.Log("level bank is empty");
				}
				else if(tempgrid[i,j]!=0)
				{
					if(tempgrid[i,j]>0)
					{
						randomlvlindex = Random.Range (0,lvlbank.ToArray().Length);//randomly chooses lvl
						roomsgrid[i,j] = (int)lvlbank[randomlvlindex];
						//lvlbank.RemoveAt(randomlvlindex);//removes lvl from the bank
					}
					else if(tempgrid[i,j]==-10)
					{
						roomsgrid[i,j]=-10;
					}
					else if(tempgrid[i,j]==-20)
					{
						roomsgrid[i,j]=-20;
					}
				}
				else
				{
					roomsgrid[i,j]=0;
				}
				
				//an empty levelbank skips this section of the code above, so ive duplicated it down here
				if(tempgrid[i,j]==-10)
				{
					roomsgrid[i,j]=-10;
				}
				if(tempgrid[i,j]==-20)
				{
					roomsgrid[i,j]=-20;
				}
			}
		}
		
		//for testing purposes
		grid = "";
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				grid += " " + roomsgrid[i,j];
			}//end for
			Debug.Log(grid);
			grid = "";
		}//end for
	}//end assignRooms
	
	public int getValueInIndexOfRooms(int i, int j)
	{
		return roomsgrid[i,j];
	}
}
