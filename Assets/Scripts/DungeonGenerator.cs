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
	
	private int decidesurfacei;
	private int decidesurfacej;

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
		
		decidesurfacei = 16;
		decidesurfacej = 16;
		
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
		/*
		createLine ();
		createSquare ();
		createL();
		createT();
		createS();
		createZ ();
		createJ();*/
		placeBlocks();
		placeBlocks ();
		placeBlocks();
		placeBlocks ();
		placeBlocks();
		placeBlocks();
		placeBlocks();
		
		findTopmost();
		Debug.Log ("topmost " + topmosti + " " + topmostj);
		findRightmost();
		Debug.Log ("rightmost " + rightmosti + " " + rightmostj);
		findLeftmost();
		Debug.Log ("leftmost " + leftmosti + " " + leftmostj);
		findBottommost();
		Debug.Log ("bottommost " + bottommosti + " " + bottommostj);
		Debug.Log("isWider " + isWider());
		//decideSurface();
		//Debug.Log("decideSurface " + decidesurfacei + " " + decidesurfacej);
		
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
				floor[15,0] = numofblocks+1;
				floor[14,0] = numofblocks+1;
				floor[13,0] = numofblocks+1;
				floor[12,0] = numofblocks+1;
			}
			else//on its side
			{
				floor[15,0] = numofblocks+1;
				floor[15,1] = numofblocks+1;
				floor[15,2] = numofblocks+1;
				floor[15,3] = numofblocks+1;
			}	
		}
		else
		{
			int i = 1;
			if(orientation == 0)//upright
			{
				while(i == 1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-3,decidesurfacej]==0 && floor[decidesurfacei-4,decidesurfacej]==0)
						{
							floor[decidesurfacei-1,decidesurfacej] = numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej] = numofblocks+1;
							floor[decidesurfacei-3,decidesurfacej] = numofblocks+1;
							floor[decidesurfacei-4,decidesurfacej] = numofblocks+1;
							i = 0;
							break;
						}
					}
					else
					{
						if((floor[15,decidesurfacej-1] != 0 || floor[14,decidesurfacej-1] !=0 || floor[13,decidesurfacej-1] !=0 || floor[12,decidesurfacej-1] !=0) && floor[15,decidesurfacej]==0 && floor[14,decidesurfacej]==0 && floor[13,decidesurfacej]==0 && floor[12,decidesurfacej]==0)
						{
							floor[15,decidesurfacej] = numofblocks+1;
							floor[14,decidesurfacej] = numofblocks+1;
							floor[13,decidesurfacej] = numofblocks+1;
							floor[12,decidesurfacej] = numofblocks+1;
							i = 0;
							break;
						}
					}
				}
			}
			else//side
			{
				while(i == 1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-1,decidesurfacej+2]==0 && floor[decidesurfacei-1,decidesurfacej+3]==0)
						{
							floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
							floor[decidesurfacei-1,decidesurfacej+2]=numofblocks+1;
							floor[decidesurfacei-1,decidesurfacej+3]=numofblocks+1;
							i = 0;
							break;
						}
						else//drop it until it hits an occupied block
						{
							for(int d=1;d<16;d++)
							{
								if(floor[d,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0 || floor[d,decidesurfacej+2]!=0 || floor[d,decidesurfacej+3]!=0)
								{
									d--;
									floor[d,decidesurfacej]=numofblocks+1;
									floor[d,decidesurfacej+1]=numofblocks+1;
									floor[d,decidesurfacej+2]=numofblocks+1;
									floor[d,decidesurfacej+3]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;
						}
					}
					else
					{
						if(floor[15,decidesurfacej-1] != 0 && floor[15,decidesurfacej]==0 && floor[15,decidesurfacej+1]==0 && floor[15,decidesurfacej+2]==0 && floor[15,decidesurfacej+3]==0)
						{
							floor[15,decidesurfacej]=numofblocks+1;
							floor[15,decidesurfacej+1]=numofblocks+1;
							floor[15,decidesurfacej+2]=numofblocks+1;
							floor[15,decidesurfacej+3]=numofblocks+1;
							i = 0;
							break;
						}
					}
				}
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
			floor[15,0] = numofblocks+1;
			floor[15,1] = numofblocks+1;
			floor[14,0] = numofblocks+1;
			floor[14,1] = numofblocks+1;
		}
		else
		{
			int i = 1;
			while(i == 1)
			{
				decideSurface();
				if(decidesurfacei != -1)
				{
					if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0)
					{
						floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
						floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
						floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
						floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
						i=0;
						break;
					}
					else
					{
						for(int d=2;d<16;d++)
						{
							if(floor[d,decidesurfacej]!=0 || floor[d,decidesurfacej+1]!=0)
							{
								d--;
								floor[d,decidesurfacej]=numofblocks+1;
								floor[d,decidesurfacej+1]=numofblocks+1;
								floor[d-1,decidesurfacej]=numofblocks+1;
								floor[d-1,decidesurfacej+1]=numofblocks+1;
								break;
							}
						}
						i=0;
						break;
					}
				}
				else
				{
					if((floor[15,decidesurfacej-1] != 0 || floor[14,decidesurfacej-1] !=0) && floor[15, decidesurfacej]==0 && floor[14,decidesurfacej]==0 && floor[15,decidesurfacej+1]==0 && floor[14,decidesurfacej+1]==0)
					{
						floor[15, decidesurfacej]=numofblocks+1;
						floor[14,decidesurfacej]=numofblocks+1;
						floor[15,decidesurfacej+1]=numofblocks+1;
						floor[14,decidesurfacej+1]=numofblocks+1;
						i=0;
						break;
					}
				}
			}
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
				floor[15,0] = numofblocks+1;
				floor[15,1] = numofblocks+1;
				floor[14,0] = numofblocks+1;
				floor[13,0] = numofblocks+1;
			}
			else if(orientation == 1)//rightside
			{
				floor[15,0] = numofblocks+1;
				floor[14,0] = numofblocks+1;
				floor[14,1] = numofblocks+1;
				floor[14,2] = numofblocks+1;
			}
			else if(orientation == 2)//upsidedown
			{
				floor[13,0] = numofblocks+1;
				floor[15,1] = numofblocks+1;
				floor[13,1] = numofblocks+1;
				floor[14,1] = numofblocks+1;
			}
			else//leftside
			{
				floor[15,0] = numofblocks+1;
				floor[15,1] = numofblocks+1;
				floor[15,2] = numofblocks+1;
				floor[14,2] = numofblocks+1;
			}
		}
		else
		{
			int i = 1;
			if(orientation == 0)//upright
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-3,decidesurfacej]==0)
						{
							floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-3,decidesurfacej]=numofblocks+1;
							i=0;
							break;
						}
						else
						{
							for(int d=3;d<16;d++)
							{
								if(floor[d,decidesurfacej]!=0 || floor[d,decidesurfacej+1]!=0)
								{
									d--;
									floor[d,decidesurfacej]=numofblocks+1;
									floor[d,decidesurfacej+1]=numofblocks+1;
									floor[d-1,decidesurfacej]=numofblocks+1;
									floor[d-2,decidesurfacej]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;
						}
					}
					else
					{
						if((floor[15,decidesurfacej-1] != 0 || floor[14,decidesurfacej-1] !=0 || floor[13,decidesurfacej-1] !=0) && floor[15,decidesurfacej]==0 && floor[15,decidesurfacej+1]==0 && floor[14,decidesurfacej]==0 && floor[13,decidesurfacej]==0)
						{
							floor[15,decidesurfacej]=numofblocks+1;
							floor[15,decidesurfacej+1]=numofblocks+1;
							floor[14,decidesurfacej]=numofblocks+1;
							floor[13,decidesurfacej]=numofblocks+1;
							
							i=0;
							break;
						}
					}
						
				}
			}
			else if(orientation == 1)//rightside
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej+2]==0)
						{
							floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej+2]=numofblocks+1;
							
							i=0;
							break;
						}
						else
						{
							for(int d=2;d<16;d++)
							{
								if(floor[d,decidesurfacej]!=0 || floor[d-1,decidesurfacej+1] !=0 || floor[d-1,decidesurfacej+2]!=0)
								{
									d--;
									floor[d,decidesurfacej]=numofblocks+1;
									floor[d-1,decidesurfacej+1]=numofblocks+1;
									floor[d-1,decidesurfacej+2]=numofblocks+1;
									floor[d-1,decidesurfacej]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;
						}
					}
					else
					{
						if((floor[15,decidesurfacej-1] != 0 || floor[14,decidesurfacej-1] !=0) && floor[15,decidesurfacej]==0 && floor[14,decidesurfacej]==0 && floor[14,decidesurfacej+1]==0 && floor[14,decidesurfacej+2]==0)
						{
							floor[15,decidesurfacej]=numofblocks+1;
							floor[14,decidesurfacej]=numofblocks+1;
							floor[14,decidesurfacej+1]=numofblocks+1;
							floor[14,decidesurfacej+2]=numofblocks+1;
							i=0;
							break;
						}
					}
				}
			}
			else if(orientation == 2)//upsidedown
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)//needs to check if the bottommost block has a foothold, else let it cling to the leftmostblock
					{
						if(floor[decidesurfacei,decidesurfacej+1] !=0)//there is a foothold
						{
							if(floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0 && floor[decidesurfacei-3,decidesurfacej]==0 && floor[decidesurfacei-3,decidesurfacej+1]==0)
							{
								floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-3,decidesurfacej]=numofblocks+1;
								floor[decidesurfacei-3,decidesurfacej+1]=numofblocks+1;
								i=0;
								break;
							}
						}
						else//no foothold, let block fall until it catches
						{
							//int tempi = getSurface(decidesurfacej+1);
							
							if(decidesurfacei != 15)//there is space below that is not oob
							{
								if(floor[decidesurfacei+1,decidesurfacej+1] != 0)//there is a foothold
								{
									if(floor[decidesurfacei,decidesurfacej+1] == 0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0)
									{
										floor[decidesurfacei,decidesurfacej+1] =numofblocks+1;
										floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
										floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
										floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
										i=0;
										break;
										
									}
								}
								else//the block hooks from the left hand side
								{
									if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei,decidesurfacej+1]==0 && floor[decidesurfacei+1,decidesurfacej+1]==0)
									{
										floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
										floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
										floor[decidesurfacei,decidesurfacej+1]=numofblocks+1;
										floor[decidesurfacei+1,decidesurfacej+1]=numofblocks+1;
										i=0;
										break;
									}
								}
							}
							else if(decidesurfacei == 15)//the foothold is out of bounds
							{
								if(floor[decidesurfacei,decidesurfacej+1] == 0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0)
								{
									floor[decidesurfacei,decidesurfacej+1] =numofblocks+1;
									floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
									floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
									floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
									i=0;
									break;
										
								}
							}
							/*
							for(int d=3;d<16;d++)
							{
								if(floor[d,decidesurfacej+1]!=0 || floor[d-2,decidesurfacej] !=0)
								{
									d--;
									floor[d,decidesurfacej+1]=numofblocks+1;
									floor[d-2,decidesurfacej]=numofblocks+1;
									floor[d-2,decidesurfacej+1]=numofblocks+1;
									floor[d-1,decidesurfacej+1]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;*/
						}
						
						for(int d=3;d<16;d++)
						{
							if(floor[d,decidesurfacej+1]!=0 || floor[d-2,decidesurfacej] !=0)
							{
								d--;
								floor[d,decidesurfacej+1]=numofblocks+1;
								floor[d-2,decidesurfacej]=numofblocks+1;
								floor[d-2,decidesurfacej+1]=numofblocks+1;
								floor[d-1,decidesurfacej+1]=numofblocks+1;
								break;
							}
						}
						i=0;
						break;
						
						/*if(floor[decidesurfacei-1,decidesurfacej]==0)
						{
							i=0;
							break;
						}*/
					}
					else
					{
						if(floor[13,decidesurfacej-1] != 0 && floor[13,decidesurfacej]==0 && floor[13,decidesurfacej+1]==0 && floor[14,decidesurfacej+1]==0 && floor[15,decidesurfacej+1]==0)
						{
							floor[13,decidesurfacej]=numofblocks+1;
							floor[13,decidesurfacej+1]=numofblocks+1;
							floor[14,decidesurfacej+1]=numofblocks+1;
							floor[15,decidesurfacej+1]=numofblocks+1;
							i=0;
							break;
						}
					}
				}
			}
			else//leftside
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-1,decidesurfacej+2]==0 && floor[decidesurfacei-2,decidesurfacej+2]==0)
						{
							floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
							floor[decidesurfacei-1,decidesurfacej+2]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej+2]=numofblocks+1;
							i=0;
							break;
						}
						else
						{
							//NEW DROPPING ALGORITHM
							for(int d=2;d<16;d++)
							{
								if(floor[d,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0 || floor[d,decidesurfacej+2]!=0)
								{
									d--;
									floor[d,decidesurfacej]=numofblocks+1;
									floor[d,decidesurfacej+1]=numofblocks+1;
									floor[d,decidesurfacej+2]=numofblocks+1;
									floor[d-1,decidesurfacej+2]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;
						}
					}
					else
					{
						if(floor[15,decidesurfacej-1] != 0 && floor[15,decidesurfacej]==0 && floor[15,decidesurfacej+1]==0 && floor[15,decidesurfacej+2]==0 && floor[14,decidesurfacej+2]==0)
						{
							floor[15,decidesurfacej]=numofblocks+1;
							floor[15,decidesurfacej+1]=numofblocks+1;
							floor[15,decidesurfacej+2]=numofblocks+1;
							floor[14,decidesurfacej+2]=numofblocks+1;
							i=0;
							break;
						}
					}
				}
				
				/*while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0)
						{
							i=0;
							break;
						}
					}
					else
					{
						if(floor[15,decidesurfacej]==0)
						{
							i=0;
							break;
						}
					}
				}*/
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
				floor[15,0] = numofblocks+1;
				floor[15,1] = numofblocks+1;
				floor[15,2] = numofblocks+1;
				floor[14,1] = numofblocks+1;
			}
			else if(orientation == 1)//rightside
			{
				floor[15,0] = numofblocks+1;
				floor[14,0] = numofblocks+1;
				floor[14,1] = numofblocks+1;
				floor[13,0] = numofblocks+1;
			}
			else if(orientation == 2)//upsidedown
			{
				floor[15,1] = numofblocks+1;
				floor[14,0] = numofblocks+1;
				floor[14,1] = numofblocks+1;
				floor[14,2] = numofblocks+1;
			}
			else//leftside
			{
				floor[14,0] = numofblocks+1;
				floor[15,1] = numofblocks+1;
				floor[14,1] = numofblocks+1;
				floor[13,1] = numofblocks+1;
			}
		}
		else
		{
			int i = 1;
			if(orientation==0)//upright
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-1,decidesurfacej+2]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0)
						{
							floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
							floor[decidesurfacei-1,decidesurfacej+2]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
							
							i=0;
							break;
						}
						else
						{
							//NEW DROPPING ALGORITHM
							for(int d=2;d<16;d++)
							{
								if(floor[d,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0 || floor[d,decidesurfacej+2]!=0)
								{
									d--;
									floor[d,decidesurfacej]=numofblocks+1;
									floor[d,decidesurfacej+1]=numofblocks+1;
									floor[d,decidesurfacej+2]=numofblocks+1;
									floor[d-1,decidesurfacej+1]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;
						}
					}
					else
					{
						if(floor[15,decidesurfacej-1] != 0 && floor[15,decidesurfacej]==0 && floor[15,decidesurfacej+1]==0 && floor[15,decidesurfacej+2]==0 && floor[14,decidesurfacej+1]==0)
						{
							floor[15,decidesurfacej]=numofblocks+1;
							floor[15,decidesurfacej+1]=numofblocks+1;
							floor[15,decidesurfacej+2]=numofblocks+1;
							floor[14,decidesurfacej+1]=numofblocks+1;
							
							i=0;
							break;
						}
					}
				}
			}
			else if(orientation==1)//rightside
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0 && floor[decidesurfacei-3,decidesurfacej]==0)
						{
							floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
							floor[decidesurfacei-3,decidesurfacej]=numofblocks+1;
							
							i=0;
							break;
						}
						else
						{
							//NEW DROPPING ALGORITHM
							for(int d=3;d<16;d++)
							{
								if(floor[d,decidesurfacej]!=0 || floor[d-1,decidesurfacej+1] !=0)
								{
									d--;
									floor[d,decidesurfacej]=numofblocks+1;
									floor[d-1,decidesurfacej+1]=numofblocks+1;
									floor[d-1,decidesurfacej]=numofblocks+1;
									floor[d-2,decidesurfacej]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;
						}
					}
					else
					{
						if((floor[15,decidesurfacej-1] != 0 || floor[14,decidesurfacej-1] !=0 || floor[13,decidesurfacej-1] !=0) && floor[15,decidesurfacej] == 0 && floor[14,decidesurfacej]==0 && floor[14,decidesurfacej+1]==0 && floor[13,decidesurfacej]==0)
						{
							floor[15,decidesurfacej] =numofblocks+1;
							floor[14,decidesurfacej]=numofblocks+1;
							floor[14,decidesurfacej+1]=numofblocks+1;
							floor[13,decidesurfacej]=numofblocks+1;
							
							i=0;
							break;
						}
					}
				}
			}
			else if(orientation==2)//upsidedown
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei,decidesurfacej+1] != 0)//there is a foothold
						{
							if(floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej+2]==0)
							{
								floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej+2]=numofblocks+1;
								
								i=0;
								break;
							}
						}
						else//no foothold, drop block until there is
						{
							/*if(decidesurfacei != 15)
							{
							}
							else//hooks on the left
							{
							}*/
							if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei,decidesurfacej+1]==0 && floor[decidesurfacei-1,decidesurfacej+2]==0)
							{
								floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
								floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-1,decidesurfacej+2]=numofblocks+1;
								
								i=0;
								break;
							}
							/*
							//NEW DROPPING ALGORITHM
							for(int d=2;d<16;d++)
							{
								if(floor[d-1,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0 || floor[d-1,decidesurfacej+2]!=0)
								{
									d--;
									floor[d-1,decidesurfacej]=numofblocks+1;
									floor[d,decidesurfacej+1]=numofblocks+1;
									floor[d-1,decidesurfacej+2]=numofblocks+1;
									floor[d-1,decidesurfacej+1]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;*/
						}
						//NEW DROPPING ALGORITHM
						for(int d=2;d<16;d++)
						{
							if(floor[d-1,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0 || floor[d-1,decidesurfacej+2]!=0)
							{
								d--;
								floor[d-1,decidesurfacej]=numofblocks+1;
								floor[d,decidesurfacej+1]=numofblocks+1;
								floor[d-1,decidesurfacej+2]=numofblocks+1;
								floor[d-1,decidesurfacej+1]=numofblocks+1;
								break;
							}
						}
						i=0;
						break;
					}
					else
					{
						if(floor[14,decidesurfacej-1]!=0 && floor[14,decidesurfacej]==0 && floor[14,decidesurfacej+1]==0 && floor[15,decidesurfacej+1]==0 && floor[14,decidesurfacej+2]==0)
						{
							floor[14,decidesurfacej]=numofblocks+1;
							floor[14,decidesurfacej+1]=numofblocks+1;
							floor[15,decidesurfacej+1]=numofblocks+1;
							floor[14,decidesurfacej+2]=numofblocks+1;
							
							i=0;
							break;
						}
					}
				}//end while
			}
			else//leftside
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei,decidesurfacej+1] != 0)//there is a foothold
						{
							if(floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0 && floor[decidesurfacei-3,decidesurfacej+1]==0)
							{
								floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-3,decidesurfacej+1]=numofblocks+1;
								i=0;
								break;
							}
						}
						else//hook
						{
							if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei,decidesurfacej+1]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0)
							{
								floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
								floor[decidesurfacei,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
								
								i=0;
								break;
							}
							/*
							//NEW DROPPING ALGORITHM
							for(int d=3;d<16;d++)
							{
								if(floor[d-1,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0)
								{
									d--;
									floor[d-1,decidesurfacej]=numofblocks+1;
									floor[d,decidesurfacej+1]=numofblocks+1;
									floor[d-1,decidesurfacej+1]=numofblocks+1;
									floor[d-2,decidesurfacej+1]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;*/
						}
						//NEW DROPPING ALGORITHM
						for(int d=3;d<16;d++)
						{
							if(floor[d-1,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0)
							{
								d--;
								floor[d-1,decidesurfacej]=numofblocks+1;
								floor[d,decidesurfacej+1]=numofblocks+1;
								floor[d-1,decidesurfacej+1]=numofblocks+1;
								floor[d-2,decidesurfacej+1]=numofblocks+1;
								break;
							}
						}
						i=0;
						break;
					}
					else
					{
						if(floor[14,decidesurfacej-1]!=0 && floor[15,decidesurfacej+1]==0 && floor[14,decidesurfacej]==0 && floor[14,decidesurfacej+1]==0 && floor[13,decidesurfacej+1]==0)
						{
							floor[15,decidesurfacej+1]=numofblocks+1;
							floor[14,decidesurfacej]=numofblocks+1;
							floor[14,decidesurfacej+1]=numofblocks+1;
							floor[13,decidesurfacej+1]=numofblocks+1;
							
							i=0;
							break;
						}
					}
				}//end while
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
				floor[15,0] = numofblocks+1;
				floor[15,1] = numofblocks+1;
				floor[14,1] = numofblocks+1;
				floor[14,2] = numofblocks+1;
			}
			else//side
			{
				floor[15,1] = numofblocks+1;
				floor[14,0] = numofblocks+1;
				floor[14,1] = numofblocks+1;
				floor[13,0] = numofblocks+1;
			}
		}
		else
		{
			int i = 1;
			if(orientation == 0)//upright
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej+2]==0)
						{
							floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej+2]=numofblocks+1;
							
							i=0;
							break;
						}
						else
						{
							//NEW DROPPING ALGORITHM
							for(int d=2;d<16;d++)
							{
								if(floor[d,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0 || floor[d-1,decidesurfacej+2]!=0)
								{
									d--;
									floor[d,decidesurfacej]=numofblocks+1;
									floor[d,decidesurfacej+1]=numofblocks+1;
									floor[d-1,decidesurfacej+2]=numofblocks+1;
									floor[d-1,decidesurfacej+1]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;
						}
					}
					else
					{
						if(floor[15,decidesurfacej-1] != 0 && floor[15,decidesurfacej]==0 && floor[15,decidesurfacej+1]==0 && floor[14,decidesurfacej+1]==0 && floor[14,decidesurfacej+2]==0)
						{
							floor[15,decidesurfacej]=numofblocks+1;
							floor[15,decidesurfacej+1]=numofblocks+1;
							floor[14,decidesurfacej+1]=numofblocks+1;
							floor[14,decidesurfacej+2]=numofblocks+1;
								
							i=0;
							break;
						}
					}
				}
			}
			else//side
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei,decidesurfacej+1] != 0)//foothold
						{
							if(floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0 && floor[decidesurfacei-3,decidesurfacej]==0)
							{
								floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-3,decidesurfacej]=numofblocks+1;
								
								i=0;
								break;
							}
						}
						else//drop
						{
							if(floor[decidesurfacei,decidesurfacej+1]==0 && floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej]==0)
							{
								floor[decidesurfacei,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
								floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
								
								i=0;
								break;
							}
							/*
							//NEW DROPPING ALGORITHM
							for(int d=3;d<16;d++)
							{
								if(floor[d-1,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0)
								{
									d--;
									floor[d-1,decidesurfacej]=numofblocks+1;
									floor[d,decidesurfacej+1]=numofblocks+1;
									floor[d-1,decidesurfacej+1]=numofblocks+1;
									floor[d-2,decidesurfacej]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;*/
						}
						//NEW DROPPING ALGORITHM
						for(int d=3;d<16;d++)
						{
							if(floor[d-1,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0)
							{
								d--;
								floor[d-1,decidesurfacej]=numofblocks+1;
								floor[d,decidesurfacej+1]=numofblocks+1;
								floor[d-1,decidesurfacej+1]=numofblocks+1;
								floor[d-2,decidesurfacej]=numofblocks+1;
								break;
							}
						}
						i=0;
						break;
					}
					else
					{
						if((floor[14,decidesurfacej-1] != 0 || floor[13,decidesurfacej-1] != 0) && floor[15,decidesurfacej+1]==0 && floor[14,decidesurfacej]==0 && floor[14,decidesurfacej+1]==0 && floor[13,decidesurfacej]==0)
						{
							floor[15,decidesurfacej+1]=numofblocks+1;
							floor[14,decidesurfacej]=numofblocks+1;
							floor[14,decidesurfacej+1]=numofblocks+1;
							floor[13,decidesurfacej]=numofblocks+1;
							
							i=0;
							break;
						}
					}
				}//end while
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
				floor[14,0] = numofblocks+1;
				floor[14,1] = numofblocks+1;
				floor[15,1] = numofblocks+1;
				floor[15,2] = numofblocks+1;
			}
			else//side
			{
				floor[15,0] = numofblocks+1;
				floor[14,0] = numofblocks+1;
				floor[14,1] = numofblocks+1;
				floor[13,1] = numofblocks+1;
			}
		}
		else
		{
			int i = 1;
			if(orientation == 0)//upright
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei,decidesurfacej+1] != 0 || floor[decidesurfacei,decidesurfacej+2] !=0)//foothold
						{
							if(floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-1,decidesurfacej+2]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0)
							{
								floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-1,decidesurfacej+2]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
								
								i=0;
								break;
							}
						}
						else//drop
						{
							if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei,decidesurfacej+1]==0 && floor[decidesurfacei,decidesurfacej+2]==0)
							{
								floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
								floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei,decidesurfacej+2]=numofblocks+1;
								
								i=0;
								break;
							}
							/*
							//NEW DROPPING ALGORITHM
							for(int d=2;d<16;d++)
							{
								if(floor[d-1,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0 || floor[d,decidesurfacej+2]!=0)
								{
									d--;
									floor[d-1,decidesurfacej]=numofblocks+1;
									floor[d,decidesurfacej+1]=numofblocks+1;
									floor[d,decidesurfacej+2]=numofblocks+1;
									floor[d-1,decidesurfacej+1]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;*/
						}
						//NEW DROPPING ALGORITHM
						for(int d=2;d<16;d++)
						{
							if(floor[d-1,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0 || floor[d,decidesurfacej+2]!=0)
							{
								d--;
								floor[d-1,decidesurfacej]=numofblocks+1;
								floor[d,decidesurfacej+1]=numofblocks+1;
								floor[d,decidesurfacej+2]=numofblocks+1;
								floor[d-1,decidesurfacej+1]=numofblocks+1;
								break;
							}
						}
						i=0;
						break;
					}
					else
					{
						if(floor[14,decidesurfacej-1] != 0 && floor[14,decidesurfacej]==0 && floor[14,decidesurfacej+1]==0 && floor[15,decidesurfacej+1]==0 && floor[15,decidesurfacej+2]==0)
						{
							floor[14,decidesurfacej]=numofblocks+1;
							floor[14,decidesurfacej+1]=numofblocks+1;
							floor[15,decidesurfacej+1]=numofblocks+1;
							floor[15,decidesurfacej+2]=numofblocks+1;
							
							i=0;
							break;
						}
					}
				}
			}
			else//sideways
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0 && floor[decidesurfacei-3,decidesurfacej+1]==0)
						{
							floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
							floor[decidesurfacei-3,decidesurfacej+1]=numofblocks+1;
							
							i=0;
							break;
						}
						else
						{
							//NEW DROPPING ALGORITHM
							for(int d=3;d<16;d++)
							{
								if(floor[d,decidesurfacej]!=0 || floor[d-1,decidesurfacej+1] !=0)
								{
									d--;
									floor[d,decidesurfacej]=numofblocks+1;
									floor[d-1,decidesurfacej+1]=numofblocks+1;
									floor[d-1,decidesurfacej]=numofblocks+1;
									floor[d-2,decidesurfacej+1]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;
						}
					}
					else
					{
						if((floor[15,decidesurfacej-1]!=0 || floor[14,decidesurfacej-1]!=0) && floor[15,decidesurfacej]==0 && floor[14,decidesurfacej]==0 && floor[14,decidesurfacej+1]==0 && floor[13,decidesurfacej+1]==0)
						{
							floor[15,decidesurfacej]=numofblocks+1;
							floor[14,decidesurfacej]=numofblocks+1;
							floor[14,decidesurfacej+1]=numofblocks+1;
							floor[13,decidesurfacej+1]=numofblocks+1;
							
							i=0;
							break;
						}
					}
				}
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
				floor[15,0] = numofblocks+1;
				floor[15,1] = numofblocks+1;
				floor[14,1] = numofblocks+1;
				floor[13,1] = numofblocks+1;
			}
			else if(orientation == 1)//rightside
			{
				floor[15,0] = numofblocks+1;
				floor[14,0] = numofblocks+1;
				floor[15,1] = numofblocks+1;
				floor[15,2] = numofblocks+1;
			}
			else if(orientation == 2)//upsidedown
			{
				floor[15,0] = numofblocks+1;
				floor[14,0] = numofblocks+1;
				floor[13,0] = numofblocks+1;
				floor[13,1] = numofblocks+1;
			}
			else//leftside
			{
				floor[14,0] = numofblocks+1;
				floor[14,1] = numofblocks+1;
				floor[14,2] = numofblocks+1;
				floor[15,2] = numofblocks+1;
			}
		}
		else
		{
			int i = 1;
			if(orientation == 0)//upright
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0 && floor[decidesurfacei-3,decidesurfacej+1]==0)
						{
							floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
							floor[decidesurfacei-3,decidesurfacej+1]=numofblocks+1;
							
							i=0;
							break;
						}
						else
						{
							//NEW DROPPING ALGORITHM
							for(int d=3;d<16;d++)
							{
								if(floor[d,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0)
								{
									d--;
									floor[d,decidesurfacej]=numofblocks+1;
									floor[d,decidesurfacej+1]=numofblocks+1;
									floor[d-1,decidesurfacej+1]=numofblocks+1;
									floor[d-2,decidesurfacej+1]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;
						}
					}
					else
					{
						if(floor[15,decidesurfacej-1]!=0 && floor[15,decidesurfacej]==0 && floor[15,decidesurfacej+1]==0 && floor[14,decidesurfacej+1]==0 && floor[13,decidesurfacej+1]==0)
						{
							floor[15,decidesurfacej]=numofblocks+1;
							floor[15,decidesurfacej+1]=numofblocks+1;
							floor[14,decidesurfacej+1]=numofblocks+1;
							floor[13,decidesurfacej+1]=numofblocks+1;
							
							i=0;
							break;
						}
					}
				}
			}
			else if(orientation == 1)//rightside
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-1,decidesurfacej+2]==0)
						{
							floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
							floor[decidesurfacei-1,decidesurfacej+2]=numofblocks+1;
							
							i=0;
							break;
						}
						else
						{
							//NEW DROPPING ALGORITHM
							for(int d=2;d<16;d++)
							{
								if(floor[d,decidesurfacej]!=0 || floor[d,decidesurfacej+1] !=0 || floor[d,decidesurfacej+2]!=0)
								{
									d--;
									floor[d,decidesurfacej]=numofblocks+1;
									floor[d,decidesurfacej+1]=numofblocks+1;
									floor[d,decidesurfacej+2]=numofblocks+1;
									floor[d-1,decidesurfacej]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;
						}
					}
					else
					{
						if((floor[15,decidesurfacej-1]!=0 || floor[14,decidesurfacej-1]!=0) && floor[15,decidesurfacej]==0 && floor[14,decidesurfacej]==0 && floor[15,decidesurfacej+1]==0 && floor[15,decidesurfacej+2]==0)
						{
							floor[15,decidesurfacej]=numofblocks+1;
							floor[14,decidesurfacej]=numofblocks+1;
							floor[15,decidesurfacej+1]=numofblocks+1;
							floor[15,decidesurfacej+2]=numofblocks+1;
							i=0;
							break;
						}
					}
				}
			}
			else if(orientation == 2)//upsidedown
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-3,decidesurfacej]==0 && floor[decidesurfacei-3,decidesurfacej+1]==0)
						{
							floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-3,decidesurfacej]=numofblocks+1;
							floor[decidesurfacei-3,decidesurfacej+1]=numofblocks+1;
							
							i=0;
							break;
						}
						else
						{
							//NEW DROPPING ALGORITHM
							for(int d=3;d<16;d++)
							{
								if(floor[d,decidesurfacej]!=0 || floor[d-2,decidesurfacej+1]!=0)
								{
									d--;
									floor[d,decidesurfacej]=numofblocks+1;
									floor[d-2,decidesurfacej+1]=numofblocks+1;
									floor[d-1,decidesurfacej]=numofblocks+1;
									floor[d-2,decidesurfacej]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;
						}
					}
					else
					{
						if((floor[15,decidesurfacej-1]!=0 || floor[14,decidesurfacej-1]!=0 || floor[13,decidesurfacej-1]!=0) && floor[15,decidesurfacej]==0 && floor[14,decidesurfacej]==0 && floor[13,decidesurfacej]==0 && floor[13,decidesurfacej+1]==0)
						{
							floor[15,decidesurfacej]=numofblocks+1;
							floor[14,decidesurfacej]=numofblocks+1;
							floor[13,decidesurfacej]=numofblocks+1;
							floor[13,decidesurfacej+1]=numofblocks+1;
							
							i=0;
							break;
						}
					}
				}//end while
			}
			else//leftside
			{
				while(i==1)
				{
					decideSurface();
					if(decidesurfacei != -1)
					{
						if(floor[decidesurfacei,decidesurfacej+2] != 0 || floor[decidesurfacei-1,decidesurfacej+1]!=0)//foothold
						{
							if(floor[decidesurfacei-2,decidesurfacej]==0 && floor[decidesurfacei-2,decidesurfacej+1]==0 && floor[decidesurfacei-2,decidesurfacej+2]==0 && floor[decidesurfacei-1,decidesurfacej+2]==0)
							{
								floor[decidesurfacei-2,decidesurfacej]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-2,decidesurfacej+2]=numofblocks+1;
								floor[decidesurfacei-1,decidesurfacej+2]=numofblocks+1;
								
								i=0;
								break;
							}
						}
						else//hanging; bug?
						{
							if(floor[decidesurfacei-1,decidesurfacej]==0 && floor[decidesurfacei-1,decidesurfacej+1]==0 && floor[decidesurfacei-1,decidesurfacej+2]==0 && floor[decidesurfacei,decidesurfacej+2]==0)
							{
								floor[decidesurfacei-1,decidesurfacej]=numofblocks+1;
								floor[decidesurfacei-1,decidesurfacej+1]=numofblocks+1;
								floor[decidesurfacei-1,decidesurfacej+2]=numofblocks+1;
								floor[decidesurfacei,decidesurfacej+2]=numofblocks+1;
								
								i=0;
								break;
							}
							/*
							//NEW DROPPING ALGORITHM
							for(int d=2;d<16;d++)
							{
								if(floor[d-1,decidesurfacej]!=0 || floor[d-1,decidesurfacej+1] !=0 || floor[d,decidesurfacej+2]!=0)
								{
									d--;
									floor[d-1,decidesurfacej]=numofblocks+1;
									floor[d-1,decidesurfacej+1]=numofblocks+1;
									floor[d,decidesurfacej+2]=numofblocks+1;
									floor[d-1,decidesurfacej+2]=numofblocks+1;
									break;
								}
							}
							i=0;
							break;*/
						}
						//NEW DROPPING ALGORITHM
						for(int d=2;d<16;d++)
						{
							if(floor[d-1,decidesurfacej]!=0 || floor[d-1,decidesurfacej+1] !=0 || floor[d,decidesurfacej+2]!=0)
							{
								d--;
								floor[d-1,decidesurfacej]=numofblocks+1;
								floor[d-1,decidesurfacej+1]=numofblocks+1;
								floor[d,decidesurfacej+2]=numofblocks+1;
								floor[d-1,decidesurfacej+2]=numofblocks+1;
								break;
							}
						}
						i=0;
						break;
					}
					else
					{
						if(floor[14,decidesurfacej-1]!=0 && floor[14,decidesurfacej]==0 && floor[14,decidesurfacej+1]==0 && floor[14,decidesurfacej+2]==0 && floor[15,decidesurfacej+2]==0)
						{
							floor[14,decidesurfacej]=numofblocks+1;
							floor[14,decidesurfacej+1]=numofblocks+1;
							floor[14,decidesurfacej+2]=numofblocks+1;
							floor[15,decidesurfacej+2]=numofblocks+1;
							
							i=0;
							break;
						}
					}
				}//end while
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
				
				if(floor[i,j] != 0)
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
				if(floor[i,j] != 0)
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
				if(floor[i,j] != 0)
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
				if(floor[i,j] != 0)
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
	
	/*bool checkTraversable()
	{
		if(numofblocks != 0)
		{
		}
		return false;
	}//end checkTraversable*/
	
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
		decidesurfacei = 16;
		decidesurfacej = 16;
		findRightmost ();
		int j = Random.Range(0,rightmostj + 2);
		
		//TODO: make right hand sides occur more
		if(Random.Range (0,4)>3)//right hand sides occur 75% of the time
		{
			j = rightmostj+1;
		}
		
		if(rightmostj > 11)//quick fix so that out of bound errors on the right hand side wont occur
		{
			j = Random.Range (0,13);
		}
		
		if(numofblocks < 3)//right hand side always occur for the first 3 blocks
		{
			j = rightmostj+1;
		}
		
		decidesurfacej = j;
		/*for(int i = 0; i < 16; i++)
		{
			decidesurfacei = i;
			if(floor[i,j] != 0)
			{		
				break;
			}
			decidesurfacei = -1;//assign i to -1 if there is no surface found on the column
		}*/
		
		decidesurfacei = getSurface(j);
		
		//TODO: fix out of bound errors for north end of the graph too
		if(decidesurfacei < 4 && decidesurfacei != -1)
		{
			decideSurface();
		}
		/*if(decidesurfacei != -1)//testing, enabling this will ensure that the blocks spawn at the edge all the time
		{
			decideSurface();
		}*/
	}//end decideSurface
	
	int getSurface(int j)
	{
		for(int i = 0; i < 16; i++)
		{
			if(floor[i,j] != 0)
			{	
				return i;
			}
		}
		
		return -1;
	}//end getSurface
	
	void selectStartpoint()
	{
		
	}//selectStart
	
	void selectEndpoint()
	{
	}//end selectEnd
	
	void placeBlocks()
	{
		int rand = Random.Range(0,4);
		if(rand ==0)
		{
			createSquare();
		}
		else if(rand==1)
		{
			createT ();
		}
		else if(rand==2)
		{
			createS();
		}
		else
		{
			createZ();
		}
	}
}