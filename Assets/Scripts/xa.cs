using UnityEngine;
using System.Collections;

// static class courtesy of Michael Todd http://twitter.com/thegamedesigner
// This script is part of the tutorial series "Making a 2D game with Unity3D using only free tools"
// http://www.rocket5studios.com/tutorials/make-a-2d-game-in-unity3d-using-only-free-tools-part-1

public class xa : MonoBehaviour {

	//static scripts
	public static KillCount sc;
	//public static DungeonGenerator dungen;//dungeon generator
	public static DungeonPopulator dunpop;
	public static int[,] mapgrid = new int[16,16];
	
	public static int currentposi = -1;
	public static int currentposj = -1;
	
	public static bool initialload = true;

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
	
	public static Vector3 playerPosition;
	private GameObject[] players;
	public static bool playerdead = false;

	public static int facingDir = 1;//1 = up, 2 = upright, 3 = right, 4 = downright, 5 = down, 6 = downleft, 7 = left, 8 = upleft
	public static int playerexitdirection = 0;//will determine which way the player exits
	
	//public enum anim { None, WalkLeft, WalkRight, RopeLeft, RopeRight, Climb, ClimbStop, StandLeft, StandRight, HangLeft, HangRight, FallLeft, FallRight , ShootLeft, ShootRight }
	public enum anim {None, WalkUp, StandUp, WalkUpRight, StandUpRight, WalkRight, StandRight, WalkDownRight, StandDownRight, WalkDown, StandDown, WalkDownLeft, StandDownLeft, WalkLeft, StandLeft, WalkUpLeft, StandUpLeft}
	public static Vector3 glx;

	public void Start()
	{
		players = GameObject.FindGameObjectsWithTag("Player");
		sc = (KillCount) (this.gameObject.GetComponent("KillCount"));
		
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
					
					if(mapgrid[i,j] == -10)
					{
						currentposi = i;
						currentposj = j;
					}
				}
			}//end for
			
			initialload = false;
		}//end initialload check
		
		Debug.Log("current position: " + currentposi + ", " + currentposj);
		
		if(currentposi!=0)
		{
			if(mapgrid[currentposi-1,currentposj]!=0)//check north
			{}
		}
		if(currentposj!=0)
		{
			if(mapgrid[currentposi,currentposj-1]!=0)//check west
			{}
		}
		if(currentposi!=15)
		{
			if(mapgrid[currentposi+1,currentposj]!=0)//check south
			{}
		}
		if(currentposj!=15)
		{
			if(mapgrid[currentposi,currentposj+1]!=0)//check east
			{}
		}
		
		// gather information from the camera to find the screen size
		xa.camRatio = 1.333f; // 4:3 is 1.333f (800x600) 
		xa.orthSize = Camera.mainCamera.camera.orthographicSize;
		xa.orthSizeX = xa.orthSize * xa.camRatio;
	}

	public void Update() 
	{
		//find player's position
		playerPosition = players[0].transform.position;
		playerdead = players[0].GetComponent<Player>().isDead();
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
		if(Input.GetMouseButtonUp(0))
		{ //Debug.Log("Left mouse button up!");
			isShoot = false;
		}
		
		/*
		//TODO:Make sure that the user cant hold down more than 2 directional keys
		if(isRight && isLeft)
		{ isRight= true; isLeft = false; Debug.Log ("Right and Left pressed at the same time!");}
		if(isUp && isDown)
		{ isUp = true; isDown =false; Debug.Log("Up and Down pressed at the same time!");}
		*/
		
		//Debug.Log("test");
		if(playerexitdirection==1)
		{
			Debug.Log("exit north");
		}
		else if(playerexitdirection==2)
		{
			Debug.Log("exit east");
		}
		else if(playerexitdirection==3)
		{
			Debug.Log("exit south");
		}
		else if(playerexitdirection==4)
		{
			Debug.Log("exit west");
		}
		playerexitdirection=0;
	}
}
