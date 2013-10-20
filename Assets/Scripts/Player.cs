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
	public GameObject bullet;

	// movement
	private float moveSpeed = 5;
	private int moveDirX;
	private int moveDirY;
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
		
		moveDirX = 0;
		moveDirY = 0;
		
		if(xa.isLeft && !xa.blockedLeft)
		{
			moveDirX = -1;
		}
		
		if(xa.isRight && !xa.blockedRight)
		{
			moveDirX = 1;
		}
		
		if(xa.isUp && !xa.blockedUp)
		{
			moveDirY = 1;
		}
		
		if(xa.isDown && !xa.blockedDown)
		{
			moveDirY = -1;
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
		
		//shooting
		if(xa.isShoot && !xa.shooting)
		{
			//Debug.Log ("SHOOTING");
			StartCoroutine (Shoot ());
		}

		UpdateMovement();
	}
	
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
	
	IEnumerator Shoot()
	{
		xa.shooting = true;
		xa.sc.bulletIncrease();
		
		Vector2 bulletpos = new Vector2(GetComponent<OTSprite>().position.x,GetComponent<OTSprite>().position.y);
		
		GameObject newbullet = (GameObject)Instantiate(bullet);//, bulletpos, Quaternion.identity); //new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);//, GetComponent<OTSprite>().transform.position, Quaternion.identity);//thisTransform.transform.position, thisTransform.transform.rotation); //transform.position, transform.rotation);//create a new bullet object
		Destroy (newbullet,3);//destroys the newly created object in 3 seconds
		
		newbullet.GetComponent<OTSprite>().position = bulletpos;//OH MY FUCKING GOD, ORTHELLO YOU BASTARD, THIS IS HOW TO DECIDE POSITIONS FOR ORTHELLO SPRITES
		
		//Debug.Log("player:" + bulletpos.x + " " + bulletpos.y);
		
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    	Vector3 bulletpoint = ray.origin + (ray.direction * 1000);
		//Debug.Log("mouse:" + bulletpoint.x + " " + bulletpoint.y);
		
		Vector2 differencepos = (Vector2)bulletpoint - bulletpos;
		float deg = Mathf.Rad2Deg*Mathf.Atan(differencepos.y/differencepos.x);
		
		//Debug.Log("Degree:" + deg);
		
		int quadr = 0;//records which quadrant was clicked
		
		//checking where player is clicking and adjusting it for 8way using the deg variable
		if(bulletpoint.x > bulletpos.x && bulletpoint.y >= bulletpos.y)//quadrant 1
		{
			quadr = 1;
			//Debug.Log("QUADRANT 1");
			/*if(deg < 22.5)//target right
			{
				//Debug.Log("TARGET RIGHT");
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
			}
			else if(deg > 67.5) //target up
			{
				//Debug.Log("TARGET UP");
				newbullet.GetComponent<Bullet>().moveDirX=0;
				newbullet.GetComponent<Bullet>().moveDirY=1;
			}
			else //target upright
			{
				//Debug.Log("TARGET UPRIGHT");
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=1;
			}*/
			
			if(deg <= 11.25)//target right
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 2 o clock
			{
				newbullet.GetComponent<Bullet>().moveDirX = 1;
				newbullet.GetComponent<Bullet>().moveDirY= .5f;
			}
			else if(deg <=56.25 && deg > 33.75)//target upright
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=1;
			}
			else if(deg <= 78.75 && deg > 56.25)//target 1 o clock
			{
				newbullet.GetComponent<Bullet>().moveDirX = .5f;
				newbullet.GetComponent<Bullet>().moveDirY= 1;
			}
			else if(deg > 78.75)//target up
			{
				newbullet.GetComponent<Bullet>().moveDirX = 0;
				newbullet.GetComponent<Bullet>().moveDirY= 1;
			}
			else
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=1;
			}
		}
		else if(bulletpoint.x <= bulletpos.x && bulletpoint.y > bulletpos.y)//quadrant 2
		{
			quadr = 2;
			//Debug.Log("QUADRANT 2");
			/*
			if(deg < -67.5) //target up
			{
				//Debug.Log("TARGET UP");
				newbullet.GetComponent<Bullet>().moveDirX=0;
				newbullet.GetComponent<Bullet>().moveDirY=1;
			}
			else if(deg > -22.5)//target left
			{
				//Debug.Log("TARGET LEFT");
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
			}
			else //target upleft
			{
				//Debug.Log("TARGET UPLEFT");
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=1;
			}
			*/
			
			if(deg <= -78.75)//up
			{
				newbullet.GetComponent<Bullet>().moveDirX=0;
				newbullet.GetComponent<Bullet>().moveDirY=1;
			}
			else if(deg <= -56.25 && deg > -78.75)//11
			{
				newbullet.GetComponent<Bullet>().moveDirX=-.5f;
				newbullet.GetComponent<Bullet>().moveDirY=1;
			}
			else if(deg <= -33.75 && deg > -56.25)//upleft
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=1;
			}
			else if(deg <= -11.25 && deg > -33.75)//10
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=.5f;
			}
			else if(deg > -11.25)//left
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
			}
			else
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=1;
			}
		}
		else if(bulletpoint.x < bulletpos.x && bulletpoint.y <= bulletpos.y)//quadrant 3
		{
			quadr = 3;
			//Debug.Log("QUADRANT 3");
			/*
			if(deg < 22.5)//target left
			{
				//Debug.Log("TARGET LEFT");
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
			}
			else if(deg > 67.5)//target down
			{
				//Debug.Log("TARGET DOWN");
				newbullet.GetComponent<Bullet>().moveDirX=0;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
			}
			else//target downleft
			{
				//Debug.Log("TARGET DOWNLEFT");
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
			}*/
			
			if(deg <= 11.25)//target left
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 8 o clock
			{
				newbullet.GetComponent<Bullet>().moveDirX = -1;
				newbullet.GetComponent<Bullet>().moveDirY= -.5f;
			}
			else if(deg <=56.25 && deg > 33.75)//target downleft
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
			}
			else if(deg <= 78.75 && deg > 56.25)//target 7 o clock
			{
				newbullet.GetComponent<Bullet>().moveDirX = -.5f;
				newbullet.GetComponent<Bullet>().moveDirY= -1;
			}
			else if(deg > 78.75)//target down
			{
				newbullet.GetComponent<Bullet>().moveDirX = 0;
				newbullet.GetComponent<Bullet>().moveDirY= -1;
			}
			else
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
			}
			
		}
		else if(bulletpoint.x >= bulletpos.x && bulletpoint.y < bulletpos.y)//quadrant 4
		{
			quadr = 4;
			//Debug.Log("QUADRANT 4");
			/*
			if(deg < -67.5)//target down
			{
				//Debug.Log("TARGET DOWN");
				newbullet.GetComponent<Bullet>().moveDirX=0;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
			}
			else if(deg > -22.5)//target right
			{
				//Debug.Log("TARGET RIGHT");
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
			}
			else//target downright
			{
				//Debug.Log("TARGET DOWNRIGHT");
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
			}*/
			
			if(deg <= -78.75)//down
			{
				newbullet.GetComponent<Bullet>().moveDirX=0;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
			}
			else if(deg <= -56.25 && deg > -78.75)//5
			{
				newbullet.GetComponent<Bullet>().moveDirX=.5f;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
			}
			else if(deg <= -33.75 && deg > -56.25)//downright
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
			}
			else if(deg <= -11.25 && deg > -33.75)//4
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=-.5f;
			}
			else if(deg > -11.25)//right
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
			}
			else
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
			}
		}
		else
		{
			quadr = 0;
			//Debug.Log("LACK OF QUADRANT?");
		}
		
		
		/*
		movement = new Vector3(moveDirX, moveDirY,0f);
		movement *= Time.deltaTime*moveSpeed;
		GetComponent<OTSprite>().position+=(Vector2)movement;*/
		
		/*
		// show the shoot sprite and play the animation
		shootRenderer.enabled = true;
		shootSprite.Play("shoot");
		
		// check facing direction and flip the shoot parent to the correct side
		if(xa.facingDir == 1)
		{
			shootParent.localScale = new Vector3(1,1,1); // left side
		}
		if(xa.facingDir == 2)
		{
			shootParent.localScale = new Vector3(-1,1,1); // right side
		}
		*/
		
		yield return new WaitForSeconds(0.05f);
		
		
		// hide the sprite
		//shootRenderer.enabled = false;
		
		xa.shooting = false;
	}//end shoot()
	
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
		// did the player collide with a pickup?
		// pickups and scoring will be added in an upcomming tutorial
		/*if (other.gameObject.CompareTag("Pickup"))
		{
			if (other.GetComponent<Pickup>())
			{
				other.GetComponent<Pickup>().PickMeUp();
				xa.sc.Pickup();
			}
		}*/
		
		if(!dead)//NOTE: SHOULD PROBABLY MIGRATE THIS TO ENEMY SCRIPTS INSTEAD, THEN I CAN CHECK IF THE PLAYER IS ALIVE AND KILL HIM IF HE IS
		{
			if(other.gameObject.CompareTag("Enemy"))
			{
				/*if(!other.GetComponent<Enemy>().isDead())
				{
					dead = true;
					gameObject.GetComponent<OTAnimatingSprite>().tintColor = Color.yellow;
				}*/
				
				dead = true;
				gameObject.GetComponent<OTAnimatingSprite>().tintColor = Color.yellow;
			}
		}
	}
	
	void OnTriggerStay(Collider other)
	{
		/*
		if(other.gameObject.CompareTag ("Wall"))
		{
			Debug.Log("found a wall!");
		}//end wall
		*/
		
		// has the player been crushed by a block?
		// this will be added in an upcomming tutorial
		/*if (other.gameObject.CompareTag("Crusher"))
		{
			if(xa.alive)
			{
				xa.alive = false;
				RespawnPlayer();
				xa.sc.LifeSubtract();
			}
		}*/
		
		/*
		// is the player overlapping a ladder?
		if(other.gameObject.CompareTag("Ladder"))
		{
			xa.onLadder = false;
			xa.blockedUp = false;
			xa.blockedDown = false;
			
			ladderHitbox.y = other.transform.localScale.y * 0.5f; // get half the ladders Y height
			
			// is the player overlapping the ladder?
			// if player is landing on top of ladder from a fall, let him pass by
			if ((thisTransform.position.y + xa.playerHitboxY) < ((ladderHitbox.y + 0.1f) + other.transform.position.y))
			{
				xa.onLadder = true;
				xa.falling = false;
			}
			
			// if the player is at the top of the ladder, then snap her to the top
			if ((thisTransform.position.y + xa.playerHitboxY) >= (ladderHitbox.y + other.transform.position.y) && xa.isUp)
			{
				xa.blockedUp = true;
				xa.glx = thisTransform.position;
                xa.glx.y = (ladderHitbox.y + other.transform.position.y) - xa.playerHitboxY;
                thisTransform.position = xa.glx;
			}
			
			// if the player is at the bottom of the ladder, then snap her to the bottom
			if ((thisTransform.position.y - xa.playerHitboxY) <= (-ladderHitbox.y + other.transform.position.y))
			{
				xa.blockedDown = true;
				xa.glx = thisTransform.position;
				xa.glx.y = (-ladderHitbox.y + other.transform.position.y) + xa.playerHitboxY;
                thisTransform.position = xa.glx;
			}
		}
		
		// is the player overlapping a rope?
		if(other.gameObject.CompareTag("Rope"))
		{
			xa.onRope = false;
			
			if(!xa.onRope && !dropFromRope) 
			{
				// snap player to center of the rope
				if (thisTransform.position.y < (other.transform.position.y + 0.2f) && thisTransform.position.y > (other.transform.position.y - 0.2f))
                {
					xa.onRope = true;
					xa.falling = false;
					xa.glx = thisTransform.position;
                    xa.glx.y = other.transform.position.y;
                    thisTransform.position = xa.glx;
                }
			}
		}
		*/
	}
	
	void OnTriggerExit(Collider other)
	{
		/*
		// did the player exit a rope trigger?
		if (other.gameObject.CompareTag("Rope"))
		{
			xa.onRope = false;
			dropFromRope = false;
		}
		
		// did the player exit a ladder trigger?
		if (other.gameObject.CompareTag("Ladder")) 
		{
			xa.onLadder = false;
		}
		*/
	}
	
	public bool isDead()
	{
		return dead;
	}
}
