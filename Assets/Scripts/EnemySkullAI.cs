using UnityEngine;
using System.Collections;

public class EnemySkullAI : MonoBehaviour {
	
	public GameObject bullet;
	
	private int state = 0;//state of the skull ai; 0 = wandering; 1 = attacking; 2 = dead
	//private enum stat{Wandering, Attack}
	
	private Vector3 movement;
	private float moveSpeed = 3;
	private int moveDirX;
	private int moveDirY;
	
	private int bobcount=0;
	
	//public int health = 1000;//health!
	//private bool dead = false;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<EnemyProperties>().health = 1000;
		//gameObject.GetComponent<EnemyProperties>().id = 1;//enemy id is 1
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<OTSprite>().spriteContainer = OT.ContainerByName("doge");//so that the container is constantly updated, should migrate to animation script later
		
		if(!gameObject.GetComponent<EnemyProperties>().dead)
		{
			updateRaycasts();
			
			bob ();
			wander ();
			//attacking();
			//healthCheck();
		}
		else
		{
			gameObject.GetComponent<OTSprite>().tintColor = Color.black;
		}
	}
	
	private bool blockednorth = false;
	private bool blockedeast = false;
	private bool blockedsouth = false;
	private bool blockedwest = false;
	private float rayBlockedDistX = 1f;
	private int groundMask = 1 << 8; // layer = Ground/wall
	
	void updateRaycasts()
	{
		blockednorth = false;
		blockedeast = false;
		blockedsouth = false;
		blockedwest = false;
		
		if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y+0.3f,transform.position.z+2f), Vector3.right, rayBlockedDistX, groundMask) || Physics.Raycast(new Vector3(transform.position.x,transform.position.y-0.4f,transform.position.z+2f), Vector3.right, rayBlockedDistX, groundMask))
		{
			blockedeast = true;
		}
		
		// player is blocked by something on the left
		// cast out 2 rays, one from the head and one from the feet
		if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y+0.3f,transform.position.z+2f), -Vector3.right, rayBlockedDistX, groundMask) || Physics.Raycast(new Vector3(transform.position.x,transform.position.y-0.4f,transform.position.z+2f), -Vector3.right, rayBlockedDistX, groundMask))
		{
			blockedwest = true;
		}
		
		//player is blocked by something on top
		//casts out 2 rays, one from the left side and one from the right side
		if(Physics.Raycast(new Vector3(transform.position.x+0.3f,transform.position.y,transform.position.z+2f), Vector3.up, rayBlockedDistX, groundMask) || Physics.Raycast(new Vector3(transform.position.x-0.4f, transform.position.y,transform.position.z+2f), Vector3.up, rayBlockedDistX, groundMask))
		{
			blockednorth = true;
			//Debug.Log("BLOCKED NORTH");
		}
		
		if(Physics.Raycast(new Vector3(transform.position.x+0.3f,transform.position.y,transform.position.z+2f), -Vector3.up, rayBlockedDistX, groundMask) || Physics.Raycast(new Vector3(transform.position.x-0.4f, transform.position.y,transform.position.z+2f), -Vector3.up, rayBlockedDistX, groundMask))
		{
			blockedsouth = true;
		}
		
	}//end updateRaycasts
	
	void bob()
	{
		
		if(bobcount<25)
		{
			movement = new Vector3(0, 1,0f);
			bobcount++;
		}
		else if(bobcount < 30)
		{
			movement = new Vector3(0,0,0f);
			bobcount++;
		}
		else if(bobcount<55)
		{
			movement = new Vector3(0, -1,0f);
			bobcount++;
		}
		else if(bobcount<60)
		{
			movement = new Vector3(0,0,0f);
			bobcount++;
		}
		else
		{
			bobcount = 0;
		}
		movement *= Time.deltaTime*1f;
		GetComponent<OTSprite>().position+=(Vector2)movement;

	}//end bob
	
	private int wanderrand = -1;
	private int wanderdir;
	private int wanderduration;
	private float wandertimer = 0;
	void wander()
	{
		movement = new Vector3(0,0,0f);
		
		if(wanderrand==-1)
		{
			wanderrand = Random.Range (0,6);
			wanderdir = Random.Range (0,4);
			wanderduration=Random.Range(1,4);
			wandertimer=0;
		}
		
		if(wanderrand==0)//wander left and right
		{
			if(wanderdir<2)// && !blockedeast)//left
			{
				//movement = new Vector3(-1, 0,0f);
				moveLeft();
			}
			else// if(!blockedwest)//right
			{
				//movement=new Vector3(1,0,0f);	
				moveRight();
			}
		}
		else if(wanderrand==1)//wander up and down
		{
			if(wanderdir<2)// && !blockednorth)//up
			{
				//movement = new Vector3(0,1,0f);
				moveUp();
			}
			else// if(!blockedsouth)//down
			{
				//movement = new Vector3(0,-1,0f);
				moveDown();
			}
		}
		else if(wanderrand==2) //wander diagonally
		{
			if(wanderdir==0)// && !blockednorth && !blockedeast)//upright
			{
				//movement = new Vector3(1,1,0f);
				moveUpRight();
			}
			else if(wanderdir==1)// && !blockedsouth && !blockedeast)//downright
			{
				//movement = new Vector3(1,-1,0f);
				moveDownRight();
			}
			else if(wanderdir==2)// && !blockednorth && !blockedwest)//upleft
			{
				//movement = new Vector3(-1,1,0f);
				moveUpLeft();
			}
			else// if(!blockedsouth && !blockedwest)//downleft
			{
				//movement = new Vector3(-1,-1,0f);
				moveDownLeft();
			}
		}
		else if(wanderrand==3)
		{
			attacking ();
		}
		else//do nothing
		{
			movement = new Vector3(0,0,0f);
		}
		
		wandertimer+=Time.deltaTime;
		
		if(wandertimer>wanderduration)
		{
			wanderrand=-1;
		}
		
		movement *= Time.deltaTime*2f;
		GetComponent<OTSprite>().position+=(Vector2)movement;
	}//end wander
	
	private int attackingtimer = 0;
	private int attackinglimit = 15;
	
	void attacking()
	{
		//Debug.Log("ATTACKING");
		if(attackingtimer>attackinglimit)
		{
		Vector2 thisskull = new Vector2(transform.position.x,transform.position.y);
		GameObject newbullet = (GameObject)Instantiate(bullet);
		GameObject newbullet2 = (GameObject)Instantiate(bullet);
		GameObject newbullet3 = (GameObject)Instantiate(bullet);
		Destroy (newbullet,5);
		Destroy (newbullet2,5);
		Destroy (newbullet3,5);
		newbullet.GetComponent<OTSprite>().position = thisskull;
		newbullet2.GetComponent<OTSprite>().position = thisskull;
		newbullet3.GetComponent<OTSprite>().position = thisskull;
		
		Vector2 bulletpoint = (Vector2)xa.playerPosition;
		Vector2 differencepos = (Vector2)xa.playerPosition - thisskull;
		float deg = Mathf.Rad2Deg*Mathf.Atan(differencepos.y/differencepos.x);
		
		
		if(bulletpoint.x > thisskull.x && bulletpoint.y >= thisskull.y)//quadrant 1
		{
			//Debug.Log("q1");
			if(deg <= 11.25)//target right
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=0;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=.5f;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=-.5f;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 2 o clock
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX = 1;
				newbullet.GetComponent<EnemyBullet>().moveDirY= .5f;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=0;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=1;
			}
			else if(deg <=56.25 && deg > 33.75)//target upright
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=1;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX = 1;
				newbullet2.GetComponent<EnemyBullet>().moveDirY= .5f;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX = .5f;
				newbullet3.GetComponent<EnemyBullet>().moveDirY= 1;
			}
			else if(deg <= 78.75 && deg > 56.25)//target 1 o clock
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX = .5f;
				newbullet.GetComponent<EnemyBullet>().moveDirY= 1;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=1;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX = 0;
				newbullet3.GetComponent<EnemyBullet>().moveDirY= 1;
			}
			else if(deg > 78.75)//target up
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX = 0;
				newbullet.GetComponent<EnemyBullet>().moveDirY= 1;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX = .5f;
				newbullet2.GetComponent<EnemyBullet>().moveDirY= 1;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=-.5f;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=1;
			}
			/*else
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=1;
			}*/
		}
		else if(bulletpoint.x <= thisskull.x && bulletpoint.y > thisskull.y)//quadrant 2
		{
			//Debug.Log("q2");
			if(deg <= -78.75)//up
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=0;
				newbullet.GetComponent<EnemyBullet>().moveDirY=1;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX = .5f;
				newbullet2.GetComponent<EnemyBullet>().moveDirY= 1;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=-.5f;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=1;
			}
			else if(deg <= -56.25 && deg > -78.75)//11
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=-.5f;
				newbullet.GetComponent<EnemyBullet>().moveDirY=1;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=0;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=1;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=1;
			}
			else if(deg <= -33.75 && deg > -56.25)//upleft
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=1;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=-.5f;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=1;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=.5f;
			}
			else if(deg <= -11.25 && deg > -33.75)//10
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=.5f;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=1;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=0;
			}
			else if(deg > -11.25)//left
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=0;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=.5f;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX = -1;
				newbullet3.GetComponent<EnemyBullet>().moveDirY= -.5f;
			}
			/*else
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=1;
			}*/
		}
		else if(bulletpoint.x < thisskull.x && bulletpoint.y <= thisskull.y)//quadrant 3
		{
			//Debug.Log("q3");
			if(deg <= 11.25)//target left
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=0;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=.5f;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX = -1;
				newbullet3.GetComponent<EnemyBullet>().moveDirY= -.5f;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 8 o clock
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX = -1;
				newbullet.GetComponent<EnemyBullet>().moveDirY= -.5f;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=0;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=-1;
			}
			else if(deg <=56.25 && deg > 33.75)//target downleft
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=-1;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX = -1;
				newbullet2.GetComponent<EnemyBullet>().moveDirY= -.5f;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX = -.5f;
				newbullet3.GetComponent<EnemyBullet>().moveDirY= -1;
			}
			else if(deg <= 78.75 && deg > 56.25)//target 7 o clock
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX = -.5f;
				newbullet.GetComponent<EnemyBullet>().moveDirY= -1;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=-1;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX = 0;
				newbullet3.GetComponent<EnemyBullet>().moveDirY= -1;
			}
			else if(deg > 78.75)//target down
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX = 0;
				newbullet.GetComponent<EnemyBullet>().moveDirY= -1;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX = -.5f;
				newbullet2.GetComponent<EnemyBullet>().moveDirY= -1;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=.5f;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=-1;
			}
			/*else
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=-1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=-1;
			}*/
		}
		else if(bulletpoint.x >= thisskull.x && bulletpoint.y < thisskull.y)//quadrant 4
		{
			//Debug.Log("q4");
			if(deg <= -78.75)//down
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=0;
				newbullet.GetComponent<EnemyBullet>().moveDirY=-1;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX = -.5f;
				newbullet2.GetComponent<EnemyBullet>().moveDirY= -1;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=.5f;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=-1;
			}
			else if(deg <= -56.25 && deg > -78.75)//5
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=.5f;
				newbullet.GetComponent<EnemyBullet>().moveDirY=-1;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=0;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=-1;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=-1;
			}
			else if(deg <= -33.75 && deg > -56.25)//downright
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=-1;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=.5f;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=-1;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=-.5f;
			}
			else if(deg <= -11.25 && deg > -33.75)//4
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=-.5f;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=-1;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet3.GetComponent<EnemyBullet>().moveDirY=0;
			}
			else if(deg > -11.25)//right
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=0;
				
				newbullet2.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet2.GetComponent<EnemyBullet>().moveDirY=-.5f;
				
				newbullet3.GetComponent<EnemyBullet>().moveDirX = 1;
				newbullet3.GetComponent<EnemyBullet>().moveDirY= .5f;
			}
			/*else
			{
				newbullet.GetComponent<EnemyBullet>().moveDirX=1;
				newbullet.GetComponent<EnemyBullet>().moveDirY=-1;
			}*/
			
		}
		}
		
		attackingtimer++;
		
		if(attackingtimer>attackinglimit + 1)
		{
			attackingtimer=0;
		}
		
	}//end attacking
	
	void moveUp()
	{
		if(!blockednorth)
		{movement = new Vector3(0,1,0f);}
		else
		{movement = new Vector3(0,0,0f);}
	}
	
	void moveDown()
	{
		if(!blockedsouth)
		{movement = new Vector3(0,-1,0f);}
		else
		{movement = new Vector3(0,0,0f);}
	}
	
	void moveRight()
	{
		if(!blockedeast)
		{movement = new Vector3(1,0,0f);}
		else
		{movement = new Vector3(0,0,0f);}
	}
	
	void moveLeft()
	{
		if(!blockedwest)
		{movement = new Vector3(-1,0,0f);}
		else
		{movement = new Vector3(0,0,0f);}
	}
	
	void moveUpRight()
	{
		int x = 0;
		int y = 0;
		if(!blockednorth)
		{y=1;}
		if(!blockedeast)
		{x=1;}
		movement = new Vector3(x,y,0f);
	}
	
	void moveUpLeft()
	{
		int x = 0;
		int y = 0;
		if(!blockednorth)
		{y=1;}
		if(!blockedwest)
		{x=-1;}
		movement = new Vector3(x,y,0f);
	}
	
	void moveDownRight()
	{
		int x = 0;
		int y = 0;
		if(!blockedsouth)
		{y=-1;}
		if(!blockedeast)
		{x=1;}
		movement = new Vector3(x,y,0f);
	}
	
	void moveDownLeft()
	{
		int x = 0;
		int y = 0;
		if(!blockedsouth)
		{y=-1;}
		if(!blockedwest)
		{x=-1;}
		movement = new Vector3(x,y,0f);
	}
	
	public bool isDead()
	{
		if(state==2)
		{
			return true;
		}
		else
			return false;
	}
	
	/*void healthCheck()
	{
		if(health<0)
		{
			//Destroy(gameObject);
			gameObject.GetComponent<OTSprite>().tintColor = Color.black;
			//gameObject.GetComponent<OTSprite>().depth = -1;
			dead = true;
		}
	}//end healthCheck*/
	
	IEnumerator hitFlash()
	{
		//gameObject.renderer.material.color = Color.white;
		gameObject.GetComponent<OTSprite>().materialReference="additive";
		yield return new WaitForSeconds(.2f);
		gameObject.GetComponent<OTSprite>().materialReference="transparent";

	}//end hitFlash
	
	void OnTriggerEnter(Collider other)
	{
		if(!gameObject.GetComponent<EnemyProperties>().dead)
		{
			if(other.gameObject.CompareTag("Bullet"))
			{
				//health -=other.gameObject.GetComponent<Bullet>().getDamage();
				gameObject.GetComponent<EnemyProperties>().health -=other.gameObject.GetComponent<Bullet>().getDamage();
				Destroy(other.gameObject);//destroy the bullet
				StartCoroutine(hitFlash());
			}
			else if(other.gameObject.CompareTag("Player"))
			{
				other.gameObject.GetComponent<Player>().killPlayer();
			}
		}
	}
}
