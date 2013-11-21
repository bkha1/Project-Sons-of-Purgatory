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
		
		
		/*
		// move left
		if(xa.isLeft && !xa.blockedLeft && !xa.shooting) 
		{
			moveDirX = -1;
			xa.facingDir = 7;
		}
		
		
		
		
		// move right
		if(xa.isRight && !xa.blockedRight && !xa.shooting) 
		{
			moveDirX = 1;
			xa.facingDir = 3;
		}
		
		// move up on ladder
		if(xa.isUp && !xa.blockedUp && xa.onLadder)
		{
			moveDirY = 1;
			xa.facingDir = 1;
		}
		
		// move down on ladder
		if(xa.isDown && !xa.blockedDown && xa.onLadder) 
		{
			moveDirY = -1;
			xa.facingDir = 5;
		}
		*/
		/*
		// drop from rope
		if(xa.isDown && xa.onRope) 
		{
			xa.onRope = false;
			dropFromRope = true;
		}
		
		// shoot
		if (xa.isShoot && !xa.shooting && !xa.onRope && !xa.falling && !shotBlockedLeft && !shotBlockedRight) 
		{
			StartCoroutine(Shoot());
		}
		*/
		
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
					//BETTER REWARD FOR TIGHTER TIMING
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
		
		/*
		// player is not falling so move normally
		//if(!xa.falling || xa.onLadder) 
		//{
			movement = new Vector3(moveDirX, moveDirY,0f);
			movement *= Time.deltaTime*moveSpeed;
			//thisTransform.Translate(movement.x,movement.y, 0f); //doesnt work
		GetComponent<OTSprite>().position+=(Vector2)movement;
		
		//}*/
		/*
		// player is falling so apply gravity
		else 
		{
			movement = new Vector3(0f,-1f,0f);
			movement *= Time.deltaTime*moveSpeed;
			thisTransform.Translate(0f,movement.y, 0f);
		}
		*/
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
		
		// is the player is standing on the ground?
		// cast 2 rays, one on each side of the character
		/*if (Physics.Raycast(new Vector3(thisTransform.position.x-0.3f,thisTransform.position.y,thisTransform.position.z+1f), -Vector3.up, out hit, 0.7f, groundMask) || Physics.Raycast(new Vector3(thisTransform.position.x+0.3f,thisTransform.position.y,thisTransform.position.z+1f), -Vector3.up, out hit, 0.7f, groundMask))
		{	
			xa.falling = false;
			
			// snap the player to the top of a ground tile if she's not on a ladder
			if(!xa.onLadder)
			{
				thisTransform.position = new Vector3(thisTransform.position.x, hit.point.y + xa.playerHitboxY, 0f);
			}
		}
		
		// then maybe she's falling
		else
		{
			if(!xa.onRope && !xa.falling && !xa.onLadder) {
				xa.falling = true;
			}
		}*/
		
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
	
	/* ============================== SHOOT ====================================================================== */
	
	
//	IEnumerator Shoot()
//	{
//		xa.shooting = true;
//		xa.sc.bulletIncrease();
//		
//		Vector2 bulletpos = new Vector2(GetComponent<OTSprite>().position.x,GetComponent<OTSprite>().position.y);
//		
//		GameObject newbullet = (GameObject)Instantiate(bullet);//, bulletpos, Quaternion.identity); //new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);//, GetComponent<OTSprite>().transform.position, Quaternion.identity);//thisTransform.transform.position, thisTransform.transform.rotation); //transform.position, transform.rotation);//create a new bullet object
//		Destroy (newbullet,3);//destroys the newly created object in 3 seconds
//		
//		newbullet.GetComponent<OTSprite>().position = bulletpos;//OH MY FUCKING GOD, ORTHELLO YOU BASTARD, THIS IS HOW TO DECIDE POSITIONS FOR ORTHELLO SPRITES
//		
//		//Debug.Log("player:" + bulletpos.x + " " + bulletpos.y);
//		
//		
//		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//    	Vector3 bulletpoint = ray.origin + (ray.direction * 1000);
//		//Debug.Log("mouse:" + bulletpoint.x + " " + bulletpoint.y);
//		
//		Vector2 differencepos = (Vector2)bulletpoint - bulletpos;
//		float deg = Mathf.Rad2Deg*Mathf.Atan(differencepos.y/differencepos.x);
//		
//		//Debug.Log("Degree:" + deg);
//		
//		int quadr = 0;//records which quadrant was clicked
//		
//		//checking where player is clicking and adjusting it for 8way using the deg variable
//		if(bulletpoint.x > bulletpos.x && bulletpoint.y >= bulletpos.y)//quadrant 1
//		{
//			quadr = 1;
//			//Debug.Log("QUADRANT 1");
//			/*if(deg < 22.5)//target right
//			{
//				//Debug.Log("TARGET RIGHT");
//				newbullet.GetComponent<Bullet>().moveDirX=1;
//				newbullet.GetComponent<Bullet>().moveDirY=0;
//			}
//			else if(deg > 67.5) //target up
//			{
//				//Debug.Log("TARGET UP");
//				newbullet.GetComponent<Bullet>().moveDirX=0;
//				newbullet.GetComponent<Bullet>().moveDirY=1;
//			}
//			else //target upright
//			{
//				//Debug.Log("TARGET UPRIGHT");
//				newbullet.GetComponent<Bullet>().moveDirX=1;
//				newbullet.GetComponent<Bullet>().moveDirY=1;
//			}*/
//			
//			if(deg <= 11.25)//target right
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=1;
//				newbullet.GetComponent<Bullet>().moveDirY=0;
//				//newbullet.GetComponent<OTSprite>().rotation = 0;
//			}
//			else if(deg <= 33.75 && deg > 11.25)//target 2 o clock
//			{
//				newbullet.GetComponent<Bullet>().moveDirX = 1;
//				newbullet.GetComponent<Bullet>().moveDirY= .5f;
//				newbullet.GetComponent<OTSprite>().rotation = 22.5f;
//			}
//			else if(deg <=56.25 && deg > 33.75)//target upright
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=1;
//				newbullet.GetComponent<Bullet>().moveDirY=1;
//				newbullet.GetComponent<OTSprite>().rotation = 45;
//			}
//			else if(deg <= 78.75 && deg > 56.25)//target 1 o clock
//			{
//				newbullet.GetComponent<Bullet>().moveDirX = .5f;
//				newbullet.GetComponent<Bullet>().moveDirY= 1;
//				newbullet.GetComponent<OTSprite>().rotation = 67.5f;
//			}
//			else if(deg > 78.75)//target up
//			{
//				newbullet.GetComponent<Bullet>().moveDirX = 0;
//				newbullet.GetComponent<Bullet>().moveDirY= 1;
//				newbullet.GetComponent<OTSprite>().rotation = 90;
//			}
//			else
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=1;
//				newbullet.GetComponent<Bullet>().moveDirY=1;
//			}
//		}
//		else if(bulletpoint.x <= bulletpos.x && bulletpoint.y > bulletpos.y)//quadrant 2
//		{
//			quadr = 2;
//			//Debug.Log("QUADRANT 2");
//			/*
//			if(deg < -67.5) //target up
//			{
//				//Debug.Log("TARGET UP");
//				newbullet.GetComponent<Bullet>().moveDirX=0;
//				newbullet.GetComponent<Bullet>().moveDirY=1;
//			}
//			else if(deg > -22.5)//target left
//			{
//				//Debug.Log("TARGET LEFT");
//				newbullet.GetComponent<Bullet>().moveDirX=-1;
//				newbullet.GetComponent<Bullet>().moveDirY=0;
//			}
//			else //target upleft
//			{
//				//Debug.Log("TARGET UPLEFT");
//				newbullet.GetComponent<Bullet>().moveDirX=-1;
//				newbullet.GetComponent<Bullet>().moveDirY=1;
//			}
//			*/
//			
//			if(deg <= -78.75)//up
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=0;
//				newbullet.GetComponent<Bullet>().moveDirY=1;
//				newbullet.GetComponent<OTSprite>().rotation = 90;
//			}
//			else if(deg <= -56.25 && deg > -78.75)//11
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=-.5f;
//				newbullet.GetComponent<Bullet>().moveDirY=1;
//				newbullet.GetComponent<OTSprite>().rotation = 112.5f;
//			}
//			else if(deg <= -33.75 && deg > -56.25)//upleft
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=-1;
//				newbullet.GetComponent<Bullet>().moveDirY=1;
//				newbullet.GetComponent<OTSprite>().rotation = 135;
//			}
//			else if(deg <= -11.25 && deg > -33.75)//10
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=-1;
//				newbullet.GetComponent<Bullet>().moveDirY=.5f;
//				newbullet.GetComponent<OTSprite>().rotation = 157.5f;
//			}
//			else if(deg > -11.25)//left
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=-1;
//				newbullet.GetComponent<Bullet>().moveDirY=0;
//				newbullet.GetComponent<OTSprite>().rotation = 180;
//			}
//			else
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=-1;
//				newbullet.GetComponent<Bullet>().moveDirY=1;
//			}
//		}
//		else if(bulletpoint.x < bulletpos.x && bulletpoint.y <= bulletpos.y)//quadrant 3
//		{
//			quadr = 3;
//			//Debug.Log("QUADRANT 3");
//			/*
//			if(deg < 22.5)//target left
//			{
//				//Debug.Log("TARGET LEFT");
//				newbullet.GetComponent<Bullet>().moveDirX=-1;
//				newbullet.GetComponent<Bullet>().moveDirY=0;
//			}
//			else if(deg > 67.5)//target down
//			{
//				//Debug.Log("TARGET DOWN");
//				newbullet.GetComponent<Bullet>().moveDirX=0;
//				newbullet.GetComponent<Bullet>().moveDirY=-1;
//			}
//			else//target downleft
//			{
//				//Debug.Log("TARGET DOWNLEFT");
//				newbullet.GetComponent<Bullet>().moveDirX=-1;
//				newbullet.GetComponent<Bullet>().moveDirY=-1;
//			}*/
//			
//			if(deg <= 11.25)//target left
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=-1;
//				newbullet.GetComponent<Bullet>().moveDirY=0;
//				newbullet.GetComponent<OTSprite>().rotation = 180;
//			}
//			else if(deg <= 33.75 && deg > 11.25)//target 8 o clock
//			{
//				newbullet.GetComponent<Bullet>().moveDirX = -1;
//				newbullet.GetComponent<Bullet>().moveDirY= -.5f;
//				newbullet.GetComponent<OTSprite>().rotation = 202.5f;
//			}
//			else if(deg <=56.25 && deg > 33.75)//target downleft
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=-1;
//				newbullet.GetComponent<Bullet>().moveDirY=-1;
//				newbullet.GetComponent<OTSprite>().rotation = 225;
//			}
//			else if(deg <= 78.75 && deg > 56.25)//target 7 o clock
//			{
//				newbullet.GetComponent<Bullet>().moveDirX = -.5f;
//				newbullet.GetComponent<Bullet>().moveDirY= -1;
//				newbullet.GetComponent<OTSprite>().rotation = 247.5f;
//			}
//			else if(deg > 78.75)//target down
//			{
//				newbullet.GetComponent<Bullet>().moveDirX = 0;
//				newbullet.GetComponent<Bullet>().moveDirY= -1;
//				newbullet.GetComponent<OTSprite>().rotation = 270;
//			}
//			else
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=-1;
//				newbullet.GetComponent<Bullet>().moveDirY=-1;
//			}
//			
//		}
//		else if(bulletpoint.x >= bulletpos.x && bulletpoint.y < bulletpos.y)//quadrant 4
//		{
//			quadr = 4;
//			//Debug.Log("QUADRANT 4");
//			/*
//			if(deg < -67.5)//target down
//			{
//				//Debug.Log("TARGET DOWN");
//				newbullet.GetComponent<Bullet>().moveDirX=0;
//				newbullet.GetComponent<Bullet>().moveDirY=-1;
//			}
//			else if(deg > -22.5)//target right
//			{
//				//Debug.Log("TARGET RIGHT");
//				newbullet.GetComponent<Bullet>().moveDirX=1;
//				newbullet.GetComponent<Bullet>().moveDirY=0;
//			}
//			else//target downright
//			{
//				//Debug.Log("TARGET DOWNRIGHT");
//				newbullet.GetComponent<Bullet>().moveDirX=1;
//				newbullet.GetComponent<Bullet>().moveDirY=-1;
//			}*/
//			
//			if(deg <= -78.75)//down
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=0;
//				newbullet.GetComponent<Bullet>().moveDirY=-1;
//				newbullet.GetComponent<OTSprite>().rotation = 270;
//			}
//			else if(deg <= -56.25 && deg > -78.75)//5
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=.5f;
//				newbullet.GetComponent<Bullet>().moveDirY=-1;
//				newbullet.GetComponent<OTSprite>().rotation = 292.5f;
//			}
//			else if(deg <= -33.75 && deg > -56.25)//downright
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=1;
//				newbullet.GetComponent<Bullet>().moveDirY=-1;
//				newbullet.GetComponent<OTSprite>().rotation = 315;
//			}
//			else if(deg <= -11.25 && deg > -33.75)//4
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=1;
//				newbullet.GetComponent<Bullet>().moveDirY=-.5f;
//				newbullet.GetComponent<OTSprite>().rotation = 337.5f;
//			}
//			else if(deg > -11.25)//right
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=1;
//				newbullet.GetComponent<Bullet>().moveDirY=0;
//				newbullet.GetComponent<OTSprite>().rotation = 0;
//			}
//			else
//			{
//				newbullet.GetComponent<Bullet>().moveDirX=1;
//				newbullet.GetComponent<Bullet>().moveDirY=-1;
//			}
//		}
//		else
//		{
//			quadr = 0;
//			//Debug.Log("LACK OF QUADRANT?");
//		}
//		
//		
//		/*
//		movement = new Vector3(moveDirX, moveDirY,0f);
//		movement *= Time.deltaTime*moveSpeed;
//		GetComponent<OTSprite>().position+=(Vector2)movement;*/
//		
//		/*
//		// show the shoot sprite and play the animation
//		shootRenderer.enabled = true;
//		shootSprite.Play("shoot");
//		
//		// check facing direction and flip the shoot parent to the correct side
//		if(xa.facingDir == 1)
//		{
//			shootParent.localScale = new Vector3(1,1,1); // left side
//		}
//		if(xa.facingDir == 2)
//		{
//			shootParent.localScale = new Vector3(-1,1,1); // right side
//		}
//		*/
//		
//		yield return new WaitForSeconds(0.05f);
//		
//		
//		// hide the sprite
//		//shootRenderer.enabled = false;
//		
//		xa.shooting = false;
//	}//end shoot()
	
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
			Debug.Log("JUST DEFENSE");
			//dead = true;
			//gameObject.GetComponent<OTAnimatingSprite>().tintColor = Color.yellow;
			StartCoroutine(hitPause());
			
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
	
	private bool blinking = false;
	IEnumerator triggerTempInvincibility(float seconds)
	{	
		dead = true;
		blinking = true;
		yield return new WaitForSeconds(seconds);
		dead = false;
		gameObject.GetComponent<MeshRenderer>().enabled = true;
		blinking = false;
	}//end triggerTempInvincibility
	
	IEnumerator hitPause()
	{
		Time.timeScale = 0;
		for(int i=0;i<10;i++)
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
