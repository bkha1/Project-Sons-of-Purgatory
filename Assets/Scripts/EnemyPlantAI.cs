using UnityEngine;
using System.Collections;

public class EnemyPlantAI : MonoBehaviour {
	
	public GameObject bullet;
	private bool dead = false;
	// Use this for initialization
	void Start () {
		
		gameObject.GetComponent<EnemyProperties>().health = 2000;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		gameObject.GetComponent<OTSprite>().spriteContainer = OT.ContainerByName("doge2");
		
		if(!gameObject.GetComponent<EnemyProperties>().dead)
		{	
			waiting ();
			
			
			if(gameObject.GetComponent<EnemyProperties>().hurt)
			{
				StartCoroutine(hitFlash());
				gameObject.GetComponent<EnemyProperties>().hurt = false;
			}
		}
		else
		{
			gameObject.GetComponent<OTSprite>().tintColor = Color.black;
		}
	}
	
	//int startaim = Random.Range(0,360);
	//float attackinterval = 0;
	int attackingduration = 6;
	int attackingtimer = 0;
	int startaim = Random.Range(0,360);
	void attacking()
	{
		//isattacking = true;
		
		if(attackingtimer>attackingduration)
		{
			shootAngle(startaim);
			startaim+=12;
			if(startaim>=360)
			{
				startaim = 360 - startaim;
			}
		}
		
		//yield return new WaitForSeconds(.05f);
		//isattacking = false;
		
		attackingtimer++;
		if(attackingtimer > attackingduration+1)
		{
			attackingtimer=0;
		}

	}//end attacking
	
	int waitingrand = -1;
	int waitingduration;
	float waitingtimer = 0;
	void waiting()
	{
		if(waitingrand==-1)
		{
			waitingrand=Random.Range(0,3);
			waitingduration = Random.Range(1,4);
			waitingtimer = 0;
			//startaim = Random.Range(0,360);
		}
		
		if(waitingrand<2)
		{
			//if(!isattacking)
			//{
				//StartCoroutine(attacking());
				attacking();
			//}
		}
		
		waitingtimer+=Time.deltaTime;
		if(waitingtimer>waitingduration)
		{
			waitingrand = -1;
			//waitingtimer=0;
		}
	}
	
	void shootAngle(int degrees)
	{
		GameObject newbullet = (GameObject)Instantiate(bullet);
		Destroy (newbullet,5);
		newbullet.GetComponent<OTSprite>().position = new Vector2(transform.position.x,transform.position.y);
		newbullet.GetComponent<RotationBullet>().rotation = degrees;
	}//end shootAngle
	
	IEnumerator hitFlash()
	{
		gameObject.GetComponent<OTSprite>().materialReference="additive";
		yield return new WaitForSeconds(.2f);
		gameObject.GetComponent<OTSprite>().materialReference="transparent";

	}//end hitFlash
	
	void OnTriggerEnter(Collider other)
	{
		/*if(!gameObject.GetComponent<EnemyProperties>().dead)
		{
			if(other.gameObject.CompareTag("Bullet"))
			{
				//health -=other.gameObject.GetComponent<Bullet>().getDamage();
				gameObject.GetComponent<EnemyProperties>().health -=other.gameObject.GetComponent<Bullet>().getDamage();
				Destroy(other.gameObject);//destroy the bullet
				StartCoroutine(hitFlash());
			}
			if(other.gameObject.CompareTag("Player"))
			{
				other.gameObject.GetComponent<Player>().killPlayer();
			}
		}*/
	}
}
