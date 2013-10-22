using UnityEngine;
using System.Collections;

// static class courtesy of Michael Todd http://twitter.com/thegamedesigner
// This script is part of the tutorial series "Making a 2D game with Unity3D using only free tools"
// http://www.rocket5studios.com/tutorials/make-a-2d-game-in-unity3d-using-only-free-tools-part-1

public class xa : MonoBehaviour {

	//static scripts
	public static KillCount sc;
	//public static DungeonGenerator dungen;//dungeon generator
	
	public static float orthSize;
	public static float orthSizeX;
	public static float orthSizeY;
	public static float camRatio;

	public static bool blockedRight = false;
	public static bool blockedLeft = false;
	public static bool blockedUp = false;
	public static bool blockedDown = false;

	public static float playerHitboxX = 0.225f; // player x = 0.45
	public static float playerHitboxY = 0.5f; // 0.5 is correct for ladders while player actual y = 0.6

	public static bool isLeft;
	public static bool isRight;
	public static bool isUp;
	public static bool isDown;
	public static bool isUpLeft;
	public static bool isUpRight;
	public static bool isDownLeft;
	public static bool isDownRight;
	
	public static bool isShoot;

	public static bool alive;
	public static bool onLadder;
	public static bool onRope;
	public static bool falling;
	public static bool shooting;
	
	public static Vector3 playerPosition = new Vector3(0,0,0);
	private GameObject[] players;
	//private List<GameObject> players;
	//public static bool playerdead = false;

	public static int facingDir = 1;//1 = up, 2 = upright, 3 = right, 4 = downright, 5 = down, 6 = downleft, 7 = left, 8 = upleft
	
	//public enum anim { None, WalkLeft, WalkRight, RopeLeft, RopeRight, Climb, ClimbStop, StandLeft, StandRight, HangLeft, HangRight, FallLeft, FallRight , ShootLeft, ShootRight }
	public enum anim {None, WalkUp, StandUp, WalkUpRight, StandUpRight, WalkRight, StandRight, WalkDownRight, StandDownRight, WalkDown, StandDown, WalkDownLeft, StandDownLeft, WalkLeft, StandLeft, WalkUpLeft, StandUpLeft}
	public static Vector3 glx;
	
	//dungeon layout stuff
	public static DungeonPopulator dunpop;
	public static int currentposi = -1;
	public static int currentposj = -1;
	public static bool initialload = true;
	public static int[,] mapgrid = new int[16,16];
	public static int[,] visitgrid = new int[16,16];//will be used to determine which rooms have been visited and unlocked
	public static int playerexitdirection = 0;//will determine which way the player exits
	public static int northroom = -1;
	public static int eastroom = -1;
	public static int southroom = -1;
	public static int westroom = -1;
	public bool isstartscene;
	
	//for PlayerRoomSideDecider
	public static int playerstartside = 0;
	public PlayerRoomSideDecider playerroomsidedecider;
	public bool playerexists = false;

	public void Start()
	{
		sc = (KillCount) (this.gameObject.GetComponent("KillCount"));
		playerroomsidedecider = (PlayerRoomSideDecider)(this.gameObject.GetComponent("PlayerRoomSideDecider"));
		if(!playerexists)
		{
			playerroomsidedecider.spawnPlayer();
			players = GameObject.FindGameObjectsWithTag("Player");
			playerexists = true;
		}
		
		
		//Debug.Log("length: " + players.Length);
		
		if(isstartscene)//start scene check, will probably change this to loading scene (good for testing a single scene without initial loading stuff)
		{
		//do this stuff once in the beginning
		if(initialload)
		{
			dunpop = (DungeonPopulator) (this.gameObject.GetComponent("DungeonPopulator"));
			dunpop.assignRooms();
			
			//fill map with zeroes first
			/*for(int i=0;i<17;i++)
			{
				for(int j=0;j<17;j++)
				{
					mapgrid[i,j]=0;
				}
			}*/
			
			for(int i=0;i<16;i++)
			{
				for(int j=0;j<16;j++)
				{
					mapgrid[i,j] = dunpop.getValueInIndexOfRooms(i,j);
					visitgrid[i,j] = -1;//marks them as unvisited
					
					if(mapgrid[i,j] == -10)
					{
						currentposi = i;
						currentposj = j;
						visitgrid[i,j] = 1;//marks the start area as visited already
					}
				}
			}//end for
			
			initialload = false;
		}//end initialload check
		}//end isstartscene check
		Debug.Log("current position: " + currentposi + ", " + currentposj);
		
		northroom = -1;
		eastroom = -1;
		southroom = -1;
		westroom = -1;
		
		if(currentposi>0)
		{
			if(mapgrid[currentposi-1,currentposj]!=0)//check north
			{
				northroom = mapgrid[currentposi-1,currentposj];
			}
		}
		if(currentposj>0)
		{
			if(mapgrid[currentposi,currentposj-1]!=0)//check west
			{
				westroom = mapgrid[currentposi,currentposj-1];
			}
		}
		if(currentposi<15 && currentposi!=-1)
		{
			if(mapgrid[currentposi+1,currentposj]!=0)//check south
			{
				southroom = mapgrid[currentposi+1,currentposj];
			}
		}
		if(currentposj<15 && currentposj!=-1)
		{
			if(mapgrid[currentposi,currentposj+1]!=0)//check east
			{
				eastroom = mapgrid[currentposi,currentposj+1];
			}
		}
		
		
		// gather information from the camera to find the screen size
		xa.camRatio = 1.333f; // 4:3 is 1.333f (800x600) 
		xa.orthSize = Camera.mainCamera.camera.orthographicSize;
		xa.orthSizeX = xa.orthSize * xa.camRatio;
		
		//stuff that must default controls to at the load of each scene
		isLeft = false;
		isRight = false;
		isUp = false;
		isDown = false;
		isShoot = false;
		shooting = false;
	}

	public void Update() 
	{
		//find player's position
		if(playerexists)
		{
			playerPosition = players[0].transform.position;
			//playerdead = players[0].GetComponent<Player>().isDead();
			/*if(players[0].GetComponent<Player>().isDead())
			{
				playerexists=false;
				Destroy(players[0]);
				//Debug.Log("length: " + players[0].transform.position);
				
				//playerroomsidedecider.respawnPlayer();
			}*/
			//Debug.Log("length: " + players.Length);
		}
		
		// these are false unless one of keys is pressed
		/*isLeft = false;
		isRight = false;
		isUp = false;
		isDown = false;
		isShoot = false;*/

		/*
		// keyboard input
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
		{ isLeft = true; }
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
		{ isRight = true; }

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
		{ isUp = true; }
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
		{ isDown = true; }

		if (Input.GetKey(KeyCode.Space)) 
		{ isShoot = true; }
		*/
		
		//new keyboard inputs
		if(Input.GetKeyDown(KeyCode.A))
		{ isLeft = true;}
		if(Input.GetKeyUp(KeyCode.A))
		{ isLeft = false;}
		if(Input.GetKeyDown(KeyCode.D))
		{ isRight = true; }
		if(Input.GetKeyUp(KeyCode.D))
		{ isRight = false; }
		if(Input.GetKeyDown(KeyCode.W))
		{ isUp = true; }
		if(Input.GetKeyUp(KeyCode.W))
		{ isUp = false; }
		if(Input.GetKeyDown(KeyCode.S))
		{ isDown = true; }
		if(Input.GetKeyUp(KeyCode.S))
		{ isDown = false; }
		
		if(isLeft && isUp)
		{ isUpLeft = true; }
		else
		{ isUpLeft = false; }
		
		if(isRight && isUp)
		{ isUpRight = true;}
		else
		{ isUpRight = false;}
		
		if(isLeft && isDown)
		{ isDownLeft = true; }
		else
		{ isDownLeft = false; }
		
		if(isRight && isDown)
		{ isDownRight = true; }
		else
		{ isDownRight = false; }
		
		
		//Input for mouse
		if(Input.GetMouseButtonDown(0))
		{ //Debug.Log("Left mouse button clicked!");
			isShoot = true;
		}
		else if(Input.GetMouseButtonUp(0))
		{ //Debug.Log("Left mouse button up!");
			isShoot = false;
		}
		/*if(Input.GetMouseButton(0))
		{isShoot=true;}*/
		
		if(Input.GetKey(KeyCode.K))
		{
			Debug.Log("manual door override activated");
			visitgrid[currentposi,currentposj] = 1;
		}
		
		/*
		//TODO:Make sure that the user cant hold down more than 2 directional keys
		if(isRight && isLeft)
		{ isRight= true; isLeft = false; Debug.Log ("Right and Left pressed at the same time!");}
		if(isUp && isDown)
		{ isUp = true; isDown =false; Debug.Log("Up and Down pressed at the same time!");}
		*/
		
		if(playerexitdirection==1)
		{
			Debug.Log("exit north");
			if(northroom!=-1)
			{
				playerstartside = 3;
				currentposi--;
				if(northroom==-10)//startpoint
				{Debug.Log("startScene");
					Application.LoadLevel("startScene");
				}
				else if(northroom==-20)//endpoint
				{Debug.Log("endScene");
					Application.LoadLevel("endScene");
				}
				else
				{Debug.Log("level" + northroom);
					Application.LoadLevel("level" + northroom);
				}
			}
		}
		else if(playerexitdirection==2)
		{
			Debug.Log("exit east");
			if(eastroom!=-1)
			{
				playerstartside = 4;
				currentposj++;
				if(eastroom==-10)//startpoint
				{Debug.Log("startScene");
					Application.LoadLevel("startScene");
				}
				else if(eastroom==-20)//endpoint
				{Debug.Log("endScene");
					Application.LoadLevel("endScene");
				}
				else
				{Debug.Log("level" + eastroom);
					Application.LoadLevel("level" + eastroom);
				}
			}
		}
		else if(playerexitdirection==3)
		{
			Debug.Log("exit south");
			if(southroom!=-1)
			{
				playerstartside = 1;
				currentposi++;
				if(southroom==-10)//startpoint
				{Debug.Log("startScene");
					Application.LoadLevel("startScene");
				}
				else if(southroom==-20)//endpoint
				{Debug.Log("endScene");
					Application.LoadLevel("endScene");
				}
				else
				{Debug.Log("level" + southroom);
					Application.LoadLevel("level" + southroom);
				}
			}
		}
		else if(playerexitdirection==4)
		{
			Debug.Log("exit west");
			if(westroom!=-1)
			{
				playerstartside = 2;
				currentposj--;
				if(westroom==-10)//startpoint
				{Debug.Log("startScene");
					Application.LoadLevel("startScene");
				}
				else if(westroom==-20)//endpoint
				{Debug.Log("endScene");
					Application.LoadLevel("endScene");
				}
				else
				{Debug.Log("level" + westroom);
					Application.LoadLevel("level" + westroom);
				}
			}
		}
		playerexitdirection=0;
	}
}
