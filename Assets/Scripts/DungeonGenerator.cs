using UnityEngine;
using System.Collections;

public class DungeonGenerator : MonoBehaviour {
	
	private int[,] floor = new int[16,16];
	//private int count;
	private string grid;
	private int numofblocks;
	
	private int orientation;
	
	private int topmosti;
	private int topmostj;
	
	private int rightmosti;
	private int rightmostj;
	
	private int leftmosti;
	private int leftmostj;
	
	private int bottommosti;
	private int bottommostj;

	// Use this for initialization
	void Start () {
		topmosti = 16;
		topmostj = 16;
		
		rightmosti = 16;
		rightmostj = 16;
		
		leftmosti = 16;
		leftmostj = 16;
		
		bottommosti = 16;
		bottommostj = 16;
		
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
		createT();
		//createS();
		//createZ();
		//createJ();
		
		findTopmost();
		Debug.Log ("topmost " + topmosti + " " + topmostj);
		findRightmost();
		Debug.Log ("rightmost " + rightmosti + " " + rightmostj);
		findLeftmost();
		Debug.Log ("leftmost " + leftmosti + " " + leftmostj);
		findBottommost();
		Debug.Log ("bottommost " + bottommosti + " " + bottommostj);
		Debug.Log("isWider " + isWider());
		
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
		else
		{
			if(orientation == 0)//upright
			{
				
			}
			else//side
			{
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
		topmosti = 16;
		topmostj = 16;
		
		for(int i = 0; i < 16; i++)
		{
			for(int j = 0; j < 16; j++)
			{
				
				if(floor[i,j] == 1)
				{	
					topmosti = i;
					topmostj = j;
					break;
				}
			}
			
			if(topmosti != 16 && topmostj != 16)
			{
				break;
			}
		}
	}//end findTopmost
	
	void findRightmost()
	{
		rightmosti = 16;
		rightmostj = 16;
		
		for(int j = 15; j > -1; j--)
		{
			for(int i = 0; i < 16; i++)
			{
				if(floor[i,j] == 1)
				{
					rightmosti = i;
					rightmostj = j;
					break;
				}
			}
			
			if(rightmosti != 16 && rightmostj != 16)
			{
				break;
			}
		}
	}//end findRightmost
	
	void findLeftmost()
	{
		leftmosti = 16;
		leftmostj = 16;
		
		for(int j = 0; j < 16; j++)
		{
			for(int i = 15; i > -1; i--)
			{
				if(floor[i,j] == 1)
				{
					leftmosti = i;
					leftmostj = j;
					break;
				}
			}
			
			if(leftmosti != 16 && leftmostj != 16)
			{
				break;
			}
		}
	}//end findLeftmost
	
	void findBottommost()
	{
		bottommosti = 16;
		bottommostj = 16;
		
		for(int i = 15; i > -1; i--)
		{
			for(int j = 15; j > -1; j--)
			{
				if(floor[i,j] == 1)
				{
					bottommosti = i;
					bottommostj = j;
					break;
				}
			}
			
			if(bottommosti != 16 && bottommostj != 16)
			{
				break;
			}
		}
	}//end findBottommost
	
	bool checkTraversable()
	{
		if(numofblocks != 0)
		{
		}
		return false;
	}//end checkTraversable
	
	bool isWider()
	{
		findRightmost();
		findTopmost();
		
		if(rightmostj > (15 - topmosti))
		{
			return true;
		}
		
		return false;
	}
	
	void decideSurface()//goes over available surfaces and randomly chooses one (includes an empty surface on the right hand side if there is one available)
	{
	}//end decideSurface
	
	void selectStart()
	{
		
	}//selectStart
	
	void selectEnd()
	{
	}//end selectEnd
}