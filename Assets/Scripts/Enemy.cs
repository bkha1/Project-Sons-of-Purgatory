using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	private bool dead;
	public float moveSpeed = 3;
	private Vector3 movement;
	private int moveDirX;
	private int moveDirY;
	private int randomnum;

	// Use this for initialization
	void Start () {
		dead = false;
		moveDirX = 0;
		moveDirY = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!dead)
		{
			UpdateMovement();
		}
	
	}
	
	void UpdateMovement() 
	{
		randomnum = Random.Range(0,100);
		
		if((xa.playerPosition.x - transform.position.x) > 0)//gameObject.transform.position.x) > 0)
		{
			moveDirX = 1;
			if(randomnum>70)
			{
				if(randomnum > 85)
					moveDirX = 0;
				else
					moveDirX = -1;
			}
		}
		else if((xa.playerPosition.x - transform.position.x) < 0)//gameObject.transform.position.x) < 0)
		{
			moveDirX = -1;
			if(randomnum>70)
			{
				if(randomnum > 85)
					moveDirX = 0;
				else
					moveDirX = 1;
			}
		}
		else {moveDirX = 0;}
		
		randomnum = Random.Range(0,100);
		
		if((xa.playerPosition.y - transform.position.y) > 0)//gameObject.transform.position.y) > 0)
		{
			moveDirY = 1;
			if(randomnum>70)
			{
				if(randomnum > 85)
					moveDirY = 0;
				else
					moveDirY = -1;
			}
		}
		else if((xa.playerPosition.y - transform.position.y) < 0)//gameObject.transform.position.y) < 0)
		{
			moveDirY = -1;
			if(randomnum>70)
			{
				if(randomnum > 85)
					moveDirY = 0;
				else
					moveDirY = 1;
			}
		}
		else {moveDirY = 0;}
		
		movement = new Vector3(moveDirX, moveDirY,0f);
		movement *= Time.deltaTime*moveSpeed;
		GetComponent<OTSprite>().position+=(Vector2)movement;
	}
	
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
		if(!dead)
		{
			if(other.gameObject.CompareTag("Bullet"))
			{
				//if(other.GetComponent<Bullet>())
				//{
					//Debug.Log("Bullet touched enemy!");
					other.GetComponent<Bullet>().destroyMe();
					//DestroyObject(other);
					
					killEnemy();
				//}
			}
		}
	}
	
	public void killEnemy()
	{
		//KillCount.killIncrease();
		xa.sc.killIncrease();
		
		gameObject.GetComponent<OTAnimatingSprite>().tintColor = Color.red;
		//gameObject.GetComponent<OTAnimatingSprite>().looping = false;
		gameObject.GetComponent<OTAnimatingSprite>().depth = -1;
		gameObject.GetComponent<OTAnimatingSprite>().Pauze();
		dead = true;
	}
	
	public bool isDead()
	{
		return dead;
	}
}
