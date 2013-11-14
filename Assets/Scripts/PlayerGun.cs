using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour {
	
	int quadr = 0;
	float deg = 0;
	float rotation = 0;
	bool shooting = false;
	float rateoffire = 0.1f;
	Vector2 gunpos;
	
	public GameObject bullet;

	// Use this for initialization
	void Start () {
		shooting = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		gunpos = new Vector2(GetComponent<OTSprite>().position.x,GetComponent<OTSprite>().position.y);
		
		quadr = gameObject.transform.parent.gameObject.GetComponent<PlayerWeapon>().getQuadr();
		deg = gameObject.transform.parent.gameObject.GetComponent<PlayerWeapon>().getDeg();
		getRotation();
		
		gameObject.GetComponent<OTSprite>().rotation+=5;
		
		if(xa.isShoot && !shooting)
		{
			StartCoroutine (shoot(bullet));
		}
	}
	
	void getRotation()
	{
		if(quadr==1)
		{
			if(deg <= 11.25)//target right
			{
				rotation = 0;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 2 o clock
			{
				rotation = 22.5f;
			}
			else if(deg <=56.25 && deg > 33.75)//target upright
			{
				rotation = 45;
			}
			else if(deg <= 78.75 && deg > 56.25)//target 1 o clock
			{
				rotation = 67.5f;
			}
			else if(deg > 78.75)//target up
			{
				rotation = 90;
			}
		}
		else if(quadr==2)
		{
			
			if(deg <= -78.75)//up
			{
				rotation = 90;
			}
			else if(deg <= -56.25 && deg > -78.75)//11
			{
				rotation = 112.5f;
			}
			else if(deg <= -33.75 && deg > -56.25)//upleft
			{
				rotation = 135;
			}
			else if(deg <= -11.25 && deg > -33.75)//10
			{
				rotation = 157.5f;
			}
			else if(deg > -11.25)//left
			{
				rotation = 180;
			}
		}
		else if(quadr == 3)
		{
			if(deg <= 11.25)//target left
			{
				rotation = 180;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 8 o clock
			{
				rotation = 202.5f;
			}
			else if(deg <=56.25 && deg > 33.75)//target downleft
			{
				rotation = 225;
			}
			else if(deg <= 78.75 && deg > 56.25)//target 7 o clock
			{
				rotation = 247.5f;
			}
			else if(deg > 78.75)//target down
			{
				rotation = 270;
			}
			
		}
		else if(quadr==4)
		{
			
			if(deg <= -78.75)//down
			{
				rotation = 270;
			}
			else if(deg <= -56.25 && deg > -78.75)//5
			{
				rotation = 292.5f;
			}
			else if(deg <= -33.75 && deg > -56.25)//downright
			{
				rotation = 315;
			}
			else if(deg <= -11.25 && deg > -33.75)//4
			{
				rotation = 337.5f;
			}
			else if(deg > -11.25)//right
			{
				rotation = 0;
			}
		}
	}//end changeRotation
	
	IEnumerator shoot(GameObject bullet)
	{
		shooting = true;
		xa.sc.bulletIncrease();
		
		GameObject newbullet = (GameObject)Instantiate(bullet);//, bulletpos, Quaternion.identity); //new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);//, GetComponent<OTSprite>().transform.position, Quaternion.identity);//thisTransform.transform.position, thisTransform.transform.rotation); //transform.position, transform.rotation);//create a new bullet object
		Destroy (newbullet,5);//destroys the newly created object in 3 seconds
		
		newbullet.GetComponent<OTSprite>().position = gunpos;//OH MY FUCKING GOD, ORTHELLO YOU BASTARD, THIS IS HOW TO DECIDE POSITIONS FOR ORTHELLO SPRITES

		newbullet.GetComponent<RotationBullet>().rotation = rotation;
		
		yield return new WaitForSeconds(rateoffire);
		
		shooting = false;
	}//end shoot()
}
