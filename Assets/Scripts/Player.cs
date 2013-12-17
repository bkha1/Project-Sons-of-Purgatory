using UnityEngine;
using System.Collections;

// This script is part of the tutorial series "Making a 2D game with Unity3D using only free tools"
// http://www.rocket5studios.com/tutorials/make-a-2d-game-in-unity3d-using-only-free-tools-part-1

public class Player : MonoBehaviour {

	// shoot objects
	private Transform shootParent;
	private Renderer shootRenderer;
	private OTAnimatingSprite shootSprite;
	
	//testing stuff
	//public GameObject bullet;

	// movement
	private float moveSpeed = 5;
	private float moveDirX;
	private float moveDirY;
	private Vector3 movement;
	private Transform thisTransform;
			
	// raycasts
	private float rayBlockedDistX = 0.6f;
	private RaycastHit hit;
	
	// layer masks	
	private int groundMask = 1 << 8; // layer = Ground/wall
	private int shootMask = 1 << 8 | 1 << 9; // layers = Ground, Ladder
		
	private bool dropFromRope = false;
	private bool shotBlockedLeft;
	private bool shotBlockedRight;
	
	private Vector3 spawnPoint;
	private Vector3 ladderHitbox;
	
	private bool dead;
	
	void Awake() 
	{
		thisTransform = transform;
	}
	
	void Start()
    {
		xa.alive = true;
		spawnPoint = thisTransform.position; // player will respawn at initial starting point
		dead = false;
		//xa.shooting = false;
		
		// connect external objects
		//shootParent = transform.Find("shoot parent");
		//shootRenderer = GameObject.Find("shoot").renderer;
		//shootSprite = GameObject.Find("shoot").GetComponent<OTAnimatingSprite>();
    }
	
	/* ============================== CONTROLS ============================== */
	
	public void Update ()
	{		
		UpdateRaycasts();
		blinkCheck();
		
		moveDirX = 0;
		moveDirY = 0;
		
		if(xa.isLeft && !xa.blockedLeft)
		{
			moveDirX = -1;
			
			if(xa.isShift)
			{
				moveDirX = -.6f;
			}
		}
		
		if(xa.isRight && !xa.blockedRight)
		{
			moveDirX = 1;
			
			if(xa.isShift)
			{
				moveDirX = .6f;
			}
		}
		
		if(xa.isUp && !xa.blockedUp)
		{
			moveDirY = 1;
			
			if(xa.isShift)
			{
				moveDirY = .6f;
			}
		}
		
		if(xa.isDown && !xa.blockedDown)
		{
			moveDirY = -1;
			
			if(xa.isShift)
			{
				moveDirY = -.6f;
			}
		}
		
		UpdateMovement();
		
		checkJustDefense();
	}
	
	//JUST DEFENSE TEST VARIABLES
	int justdefcooldown = 0;
	int justdefshiftcooldown = 0;
	bool justdef = false;
	
	void checkJustDefense()
	{
		if(xa.isShift && xa.isShoot)
		{
			if(justdefcooldown==0)
			{
				justdefcooldown = 25;//governs the window; has to be over 20; number - 20 = frame window
			}
			else if(justdefcooldown<=20)//so that the player cant just hold the buttons and hope it hits during the correct window
			{
				justdefcooldown=20;
			}
		}
		
		//simply hitting shift will also do a just defense check, this one has a quicker cooldown; maybe make the window smaller and award players with energy
		if(xa.isShift)
		{
			if(justdefshiftcooldown==0)
			{
				justdefshiftcooldown = 13;//smaller window
			}
			else if(justdefshiftcooldown<=10)
			{
				justdefshiftcooldown = 10;
			}
		}
		
		if(justdefcooldown>0 || justdefshiftcooldown>0)
		{
			if(justdefcooldown>20 || justdefshiftcooldown>10)
			{
				justdef = true;
				
				if(justdefshiftcooldown==11)
				{
					//BETTER REWARD FOR TIGHTER TIMING?
				}
			}
			else
			{
				justdef = false;
			}
			
			if(justdefcooldown>0)
				justdefcooldown--;
			if(justdefshiftcooldown>0)
				justdefshiftcooldown--;
		}
	}//end checkJustDefense
	
	void UpdateMovement() 
	{
		movement = new Vector3(moveDirX, moveDirY,0f);
		movement *= Time.deltaTime*moveSpeed;
		GetComponent<OTSprite>().position+=(Vector2)movement;
	}
	
	/* ============================== RAYCASTS ============================== */
	
	void UpdateRaycasts() 
	{
		// set these to false unless a condition below is met
		xa.blockedRight = false;
		xa.blockedLeft = false;
		xa.blockedUp = false;
		xa.blockedDown = false;
		
		shotBlockedLeft = false;
		shotBlockedRight = false;
		
		// player is blocked by something on the right
		// cast out 2 rays, one from the head and one from the feet
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y+0.3f,thisTransform.position.z+2f), Vector3.right, rayBlockedDistX, groundMask) || Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y-0.4f,thisTransform.position.z+2f), Vector3.right, rayBlockedDistX, groundMask))
		{
			xa.blockedRight = true;
		}
		
		// player is blocked by something on the left
		// cast out 2 rays, one from the head and one from the feet
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y+0.3f,thisTransform.position.z+2f), -Vector3.right, rayBlockedDistX, groundMask) || Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y-0.4f,thisTransform.position.z+2f), -Vector3.right, rayBlockedDistX, groundMask))
		{
			xa.blockedLeft = true;
		}
		
		//player is blocked by something on top
		//casts out 2 rays, one from the left side and one from the right side
		if(Physics.Raycast(new Vector3(thisTransform.position.x+0.3f,thisTransform.position.y,thisTransform.position.z+2f), Vector3.up, rayBlockedDistX, groundMask) || Physics.Raycast(new Vector3(thisTransform.position.x-0.4f, thisTransform.position.y,thisTransform.position.z+2f), Vector3.up, rayBlockedDistX, groundMask))
		{
			xa.blockedUp = true;
		}
		
		if(Physics.Raycast(new Vector3(thisTransform.position.x+0.3f,thisTransform.position.y,thisTransform.position.z+2f), -Vector3.up, rayBlockedDistX, groundMask) || Physics.Raycast(new Vector3(thisTransform.position.x-0.4f, thisTransform.position.y,thisTransform.position.z+2f), -Vector3.up, rayBlockedDistX, groundMask))
		{
			xa.blockedDown = true;
		}
		
		/*
		// is there something blocking our shot to the right?
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z+1f), Vector3.right, 1f, shootMask))
		{
			shotBlockedRight = true;
		}
		
		// is there something blocking our shot to the left?
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z+1f), -Vector3.right, 1f, shootMask))
		{
			shotBlockedLeft = true;
		}
		
		*/
		// did the shot hit a brick tile to the left?
		//if (Physics.Raycast(new Vector3(thisTransform.position.x-1f,thisTransform.position.y,thisTransform.position.z+1f), -Vector3.up, out hit, 0.6f, groundMask))
		//{
		//	if(!shotBlockedLeft && xa.isShoot && xa.facingDir == 1) {
				// breaking bricks will be added in an upcomming tutorial
				/*if (hit.transform.GetComponent<Brick>())
				{
					StartCoroutine(hit.transform.GetComponent<Brick>().PlayBreakAnim());
				}*/
		//	}
		//}
		
		// did the shot hit a brick tile to the right?
		//if(Physics.Raycast(new Vector3(thisTransform.position.x+1f,thisTransform.position.y,thisTransform.position.z+1f), -Vector3.up, out hit, 0.6f, groundMask))
		//{
		//	if(!shotBlockedRight && xa.isShoot && xa.facingDir == 2) {
				// breaking bricks will be added in an upcomming tutorial
				/*if (hit.transform.GetComponent<Brick>())
				{
					StartCoroutine(hit.transform.GetComponent<Brick>().PlayBreakAnim());
				}*/
		//	}
		//}
		
		// is the player on the far right edge of the screen?
		if (thisTransform.position.x + xa.playerHitboxX > (Camera.mainCamera.transform.position.x + xa.orthSizeX)) 
		{
			xa.blockedRight = true;
		}
		
		// is the player on the far left edge of the screen?
		if (thisTransform.position.x - xa.playerHitboxX < (Camera.mainCamera.transform.position.x - xa.orthSizeX)) 
		{
			xa.blockedLeft = true;
		}
	}	

	/* ============================== DEATH AND RESPAWN ====================================================================== */
	
	void RespawnPlayer()
	{
		// respawn the player at her initial start point
		thisTransform.position = spawnPoint;
		xa.alive = true;
	}
	
	/* ============================== TRIGGER EVENTS ====================================================================== */
	
	void OnTriggerEnter(Collider other)
	{
		
		/*if(!dead)
		{
			if(other.gameObject.CompareTag("Enemy"))
			{
				if(!other.GetComponent<EnemyProperties>().dead)
				{
					killPlayer();
				}
				
			}
			else if(other.gameObject.CompareTag("EnemyBullet"))
			{
				killPlayer ();
				Destroy(other.gameObject);
			}
		}*/
	}
	
	void OnTriggerStay(Collider other)
	{
	}
	
	void OnTriggerExit(Collider other)
	{
	}
	
	public bool isDead()
	{
		return dead;
	}
	
	public void killPlayer()
	{
		if(justdef)
		{
			//Debug.Log("JUST DEFENSE");
			//dead = true;
			//gameObject.GetComponent<OTAnimatingSprite>().tintColor = Color.yellow;
			StartCoroutine(hitPause());
			justdefcolor = true;
			//gameObject.GetComponent<OTAnimatingSprite>().tintColor = Color.white;
			StartCoroutine(triggerTempInvincibility(3));
		}
		else if(dead == false)
		{
			//Debug.Log("JUST DEFENSE FAILED");
			//dead = true;
			//TODO: play a dying animation?
			//TODO: let xa.cs know that player has died, deduct a life and respawn

			respawnPlayer();
			//play respawn animations?
			StartCoroutine(triggerTempInvincibility(3));
			
			xa.experiencepoints-=300;
		}
	}
	
	void respawnPlayer()
	{
		Vector2 playerpos;
		
		if(xa.playerstartside==1)//start north
		{
			playerpos = new Vector2(-0.5f,5.7f);		
		}
		else if(xa.playerstartside==2)//start east
		{
			playerpos = new Vector2(8.5f,0.7f);
		}
		else if(xa.playerstartside==3)//start south
		{
			playerpos = new Vector2(0.5f,-5.3f);
		}
		else if(xa.playerstartside==4)//start west
		{
			playerpos = new Vector2(-8.5f,.7f);	
		}
		else
		{
			//playerpos = new Vector2(-0.5f,5.7f);
			playerpos = new Vector2(0,0);
		}
		gameObject.GetComponent<OTSprite>().position = playerpos;
	}//end respawnPlayer

	private bool justdefcolor = false;
	private bool blinking = false;
	IEnumerator triggerTempInvincibility(float seconds)
	{	
		if(justdefcolor)
		{
			//change player color to indicate JD
			gameObject.GetComponent<OTSprite>().tintColor = Color.yellow;
		}

		dead = true;
		blinking = true;
		yield return new WaitForSeconds(seconds);
		dead = false;
		gameObject.GetComponent<MeshRenderer>().enabled = true;
		blinking = false;
		gameObject.GetComponent<OTSprite>().tintColor = Color.white;
		justdefcolor = false;
	}//end triggerTempInvincibility

	IEnumerator hitPause()
	{
		Time.timeScale = 0;
		for(int i=0;i<10;i++)//wait ten frames
		{
			yield return null;
		}
		Time.timeScale = 1;
	}//end hitPause()
	
	float blinktimer = 0;
	void blinkCheck()
	{
		if(blinking == true)
		{
			if(blinktimer<.05)
			{
				//blinktimer++;
				blinktimer+=Time.deltaTime;
			}
			else
			{
				if(gameObject.GetComponent<MeshRenderer>().enabled)
				{
					gameObject.GetComponent<MeshRenderer>().enabled=false;
				}
				else
				{
					gameObject.GetComponent<MeshRenderer>().enabled=true;
				}
				
				blinktimer=0;
			}
		}
	}//end blink
}
