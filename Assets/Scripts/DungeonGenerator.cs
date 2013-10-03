using UnityEngine;
using System.Collections;

public class DungeonGenerator : MonoBehaviour {
	
	private int[,] floor = new int[16,16];
	//private int count;
	private string grid;
	private int numofblocks;
	
	private int orientation;

	// Use this for initialization
	void Start () {
		
		numofblocks = 0;
		//count = 0;
		grid = "";
		
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				//floor[i,j] = count;
				//count++;
				floor[i,j] = 0;
			}//end for
		}//end for
		
		
		//createLine ();
		//createSquare();
		//createL();
		//createT();
		//createS();
		//createZ();
		//createJ();
		//Debug.Log ("placed block " + numofblocks);
		
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				//Debug.Log(floor[i,j]);
				grid += " " + floor[i,j];
			}//end for
			Debug.Log(grid);
			grid = "";
		}//end for
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void createLine()
	{
		orientation = Random.Range(0,2);
		if(numofblocks == 0)
		{
			if(orientation == 0)//upright
			{
				floor[15,0] = 1;
				floor[14,0] = 1;
				floor[13,0] = 1;
				floor[12,0] = 1;
			}
			else//on its side
			{
				floor[15,0] = 1;
				floor[15,1] = 1;
				floor[15,2] = 1;
				floor[15,3] = 1;
			}	
		}
		numofblocks++;
		Debug.Log ("placed Line " + numofblocks);
	}//end createLine
	
	void createSquare()
	{
		orientation = 0;//orientation doesnt matter for this one
		if(numofblocks == 0)
		{
			floor[15,0] = 1;
			floor[15,1] = 1;
			floor[14,0] = 1;
			floor[14,1] = 1;
		}
		numofblocks++;
		Debug.Log ("placed Square " + numofblocks);
	}//end createSquare
	
	void createL()
	{
		orientation = Random.Range(0,4);
		if(numofblocks == 0)
		{
			if(orientation == 0)//upright
			{
				floor[15,0] = 1;
				floor[15,1] = 1;
				floor[14,0] = 1;
				floor[13,0] = 1;
			}
			else if(orientation == 1)//rightside
			{
				floor[15,0] = 1;
				floor[14,0] = 1;
				floor[14,1] = 1;
				floor[14,2] = 1;
			}
			else if(orientation == 2)//upsidedown
			{
				floor[13,0] = 1;
				floor[15,1] = 1;
				floor[13,1] = 1;
				floor[14,1] = 1;
			}
			else//leftside
			{
				floor[15,0] = 1;
				floor[15,1] = 1;
				floor[15,2] = 1;
				floor[14,2] = 1;
			}
		}
		numofblocks++;
		Debug.Log ("placed L " + numofblocks);
	}//end createL
	
	void createT()
	{
		orientation = Random.Range(0,4);
		if(numofblocks == 0)
		{
			if(orientation == 0)//upright
			{
				floor[15,0] = 1;
				floor[15,1] = 1;
				floor[15,2] = 1;
				floor[14,1] = 1;
			}
			else if(orientation == 1)//rightside
			{
				floor[15,0] = 1;
				floor[14,0] = 1;
				floor[14,1] = 1;
				floor[13,0] = 1;
			}
			else if(orientation == 2)//upsidedown
			{
				floor[15,1] = 1;
				floor[14,0] = 1;
				floor[14,1] = 1;
				floor[14,2] = 1;
			}
			else//leftside
			{
				floor[14,0] = 1;
				floor[15,1] = 1;
				floor[14,1] = 1;
				floor[13,1] = 1;
			}
		}
		numofblocks++;
		Debug.Log ("placed T " + numofblocks);
	}//end createT
	
	void createS()
	{
		orientation = Random.Range (0,2);
		if(numofblocks == 0)
		{
			if(orientation == 0)//upright
			{
				floor[15,0] = 1;
				floor[15,1] = 1;
				floor[14,1] = 1;
				floor[14,2] = 1;
			}
			else//side
			{
				floor[15,1] = 1;
				floor[14,0] = 1;
				floor[14,1] = 1;
				floor[13,0] = 1;
			}
		}
		numofblocks++;
		Debug.Log ("placed S " + numofblocks);
	}//end createS
	
	void createZ()
	{
		orientation = Random.Range(0,2);
		if(numofblocks == 0)
		{
			if(orientation == 0)//upright
			{
				floor[14,0] = 1;
				floor[14,1] = 1;
				floor[15,1] = 1;
				floor[15,2] = 1;
			}
			else//side
			{
				floor[15,0] = 1;
				floor[14,0] = 1;
				floor[14,1] = 1;
				floor[13,1] = 1;
			}
		}
		numofblocks++;
		Debug.Log ("placed Z " + numofblocks);
	}//end createZ
	
	void createJ()
	{
		orientation = Random.Range (0,4);
		if(numofblocks == 0)
		{
			if(orientation == 0)//upright
			{
				floor[15,0] = 1;
				floor[15,1] = 1;
				floor[14,1] = 1;
				floor[13,1] = 1;
			}
			else if(orientation == 1)//rightside
			{
				floor[15,0] = 1;
				floor[14,0] = 1;
				floor[15,1] = 1;
				floor[15,2] = 1;
			}
			else if(orientation == 2)//upsidedown
			{
				floor[15,0] = 1;
				floor[14,0] = 1;
				floor[13,0] = 1;
				floor[13,1] = 1;
			}
			else//leftside
			{
				floor[14,0] = 1;
				floor[14,1] = 1;
				floor[14,2] = 1;
				floor[15,2] = 1;
			}
		}
		numofblocks++;
		Debug.Log ("placed J " + numofblocks);
	}//end createJ
	
	void findTopmost()
	{
	}//end findTopmost
	
	void findRightmost()
	{
	}//end findRightmost
}
