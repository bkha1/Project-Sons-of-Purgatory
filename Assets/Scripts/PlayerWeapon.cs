﻿using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {
	
	private float rateoffire = 0.1f;
	private bool shooting = false;
	public GameObject bullet;
	public GameObject rotatingbullet;
	public GameObject multibullet;
	public GameObject rotatingmultibullet;
	Vector2 playerpos;
	// Use this for initialization
	void Start () {
		shooting = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		playerpos = new Vector2(GetComponent<OTSprite>().position.x,GetComponent<OTSprite>().position.y);
		
		//shooting
		if(xa.isShoot && !shooting)
		{	
			if(xa.experiencepoints>=1200)
			{
				if(xa.isShift)
				{
					StartCoroutine (spreadShot2(playerpos, rotatingmultibullet));
				}
				else
				{
					StartCoroutine (spreadShot(playerpos, multibullet));
				}
			}
			else if(xa.experiencepoints>=500)
			{
				if(xa.isShift)
				{
					StartCoroutine (Shoot2(playerpos, rotatingmultibullet));
					//StartCoroutine (Shoot2(playerpos, rotatingbullet));
					//StartCoroutine (Shoot2(new Vector2(playerpos.x,playerpos.y-1), rotatingbullet));
					//StartCoroutine (Shoot2(new Vector2(playerpos.x,playerpos.y+1), rotatingbullet));
				}
				else
				{
					StartCoroutine (Shoot(playerpos, multibullet));
				}
			}
			else
			{
				if(xa.isShift)
				{
					StartCoroutine (Shoot2 (playerpos, rotatingbullet));
				}
				else
				{
					StartCoroutine (Shoot (playerpos, bullet));
				}
			}
		}
	
	}
	
	IEnumerator Shoot(Vector2 bulletpos, GameObject bullet)
	{
		shooting = true;
		xa.sc.bulletIncrease();
		
		//Vector2 bulletpos = new Vector2(GetComponent<OTSprite>().position.x,GetComponent<OTSprite>().position.y);
		
		GameObject newbullet = (GameObject)Instantiate(bullet);//, bulletpos, Quaternion.identity); //new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);//, GetComponent<OTSprite>().transform.position, Quaternion.identity);//thisTransform.transform.position, thisTransform.transform.rotation); //transform.position, transform.rotation);//create a new bullet object
		Destroy (newbullet,5);//destroys the newly created object in 3 seconds
		
		newbullet.GetComponent<OTSprite>().position = bulletpos;//OH MY FUCKING GOD, ORTHELLO YOU BASTARD, THIS IS HOW TO DECIDE POSITIONS FOR ORTHELLO SPRITES
		//newbullet.transform.position = bulletpos;
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    	Vector3 bulletpoint = ray.origin + (ray.direction * 1000);
		//Debug.Log("mouse:" + bulletpoint.x + " " + bulletpoint.y);
		
		Vector2 differencepos = (Vector2)bulletpoint - bulletpos;
		float deg = Mathf.Rad2Deg*Mathf.Atan(differencepos.y/differencepos.x);
		
		
		int quadr = 0;//records which quadrant was clicked
		
		//checking where player is clicking and adjusting it for 8way using the deg variable
		if(bulletpoint.x > bulletpos.x && bulletpoint.y >= bulletpos.y)//quadrant 1
		{
			quadr = 1;
			//Debug.Log("QUADRANT 1");
			
			if(deg <= 11.25)//target right
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
				//newbullet.GetComponent<OTSprite>().rotation = 0;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 2 o clock
			{
				newbullet.GetComponent<Bullet>().moveDirX = 1;
				newbullet.GetComponent<Bullet>().moveDirY= .5f;
				newbullet.GetComponent<OTSprite>().rotation = 22.5f;
			}
			else if(deg <=56.25 && deg > 33.75)//target upright
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=1;
				newbullet.GetComponent<OTSprite>().rotation = 45;
			}
			else if(deg <= 78.75 && deg > 56.25)//target 1 o clock
			{
				newbullet.GetComponent<Bullet>().moveDirX = .5f;
				newbullet.GetComponent<Bullet>().moveDirY= 1;
				newbullet.GetComponent<OTSprite>().rotation = 67.5f;
			}
			else if(deg > 78.75)//target up
			{
				newbullet.GetComponent<Bullet>().moveDirX = 0;
				newbullet.GetComponent<Bullet>().moveDirY= 1;
				newbullet.GetComponent<OTSprite>().rotation = 90;
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
			
			if(deg <= -78.75)//up
			{
				newbullet.GetComponent<Bullet>().moveDirX=0;
				newbullet.GetComponent<Bullet>().moveDirY=1;
				newbullet.GetComponent<OTSprite>().rotation = 90;
			}
			else if(deg <= -56.25 && deg > -78.75)//11
			{
				newbullet.GetComponent<Bullet>().moveDirX=-.5f;
				newbullet.GetComponent<Bullet>().moveDirY=1;
				newbullet.GetComponent<OTSprite>().rotation = 112.5f;
			}
			else if(deg <= -33.75 && deg > -56.25)//upleft
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=1;
				newbullet.GetComponent<OTSprite>().rotation = 135;
			}
			else if(deg <= -11.25 && deg > -33.75)//10
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=.5f;
				newbullet.GetComponent<OTSprite>().rotation = 157.5f;
			}
			else if(deg > -11.25)//left
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
				newbullet.GetComponent<OTSprite>().rotation = 180;
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
			
			if(deg <= 11.25)//target left
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
				newbullet.GetComponent<OTSprite>().rotation = 180;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 8 o clock
			{
				newbullet.GetComponent<Bullet>().moveDirX = -1;
				newbullet.GetComponent<Bullet>().moveDirY= -.5f;
				newbullet.GetComponent<OTSprite>().rotation = 202.5f;
			}
			else if(deg <=56.25 && deg > 33.75)//target downleft
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
				newbullet.GetComponent<OTSprite>().rotation = 225;
			}
			else if(deg <= 78.75 && deg > 56.25)//target 7 o clock
			{
				newbullet.GetComponent<Bullet>().moveDirX = -.5f;
				newbullet.GetComponent<Bullet>().moveDirY= -1;
				newbullet.GetComponent<OTSprite>().rotation = 247.5f;
			}
			else if(deg > 78.75)//target down
			{
				newbullet.GetComponent<Bullet>().moveDirX = 0;
				newbullet.GetComponent<Bullet>().moveDirY= -1;
				newbullet.GetComponent<OTSprite>().rotation = 270;
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
			
			if(deg <= -78.75)//down
			{
				newbullet.GetComponent<Bullet>().moveDirX=0;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
				newbullet.GetComponent<OTSprite>().rotation = 270;
			}
			else if(deg <= -56.25 && deg > -78.75)//5
			{
				newbullet.GetComponent<Bullet>().moveDirX=.5f;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
				newbullet.GetComponent<OTSprite>().rotation = 292.5f;
			}
			else if(deg <= -33.75 && deg > -56.25)//downright
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
				newbullet.GetComponent<OTSprite>().rotation = 315;
			}
			else if(deg <= -11.25 && deg > -33.75)//4
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=-.5f;
				newbullet.GetComponent<OTSprite>().rotation = 337.5f;
			}
			else if(deg > -11.25)//right
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
				newbullet.GetComponent<OTSprite>().rotation = 0;
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
		
		yield return new WaitForSeconds(rateoffire);
		
		shooting = false;
	}//end shoot()
	
	
	IEnumerator Shoot2(Vector2 bulletpos, GameObject bullet)
	{
		shooting = true;
		xa.sc.bulletIncrease();
		
		//Vector2 bulletpos = new Vector2(GetComponent<OTSprite>().position.x,GetComponent<OTSprite>().position.y);
		
		GameObject newbullet = (GameObject)Instantiate(bullet);//, bulletpos, Quaternion.identity); //new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);//, GetComponent<OTSprite>().transform.position, Quaternion.identity);//thisTransform.transform.position, thisTransform.transform.rotation); //transform.position, transform.rotation);//create a new bullet object
		Destroy (newbullet,5);//destroys the newly created object in 3 seconds
		
		newbullet.GetComponent<OTSprite>().position = bulletpos;//OH MY FUCKING GOD, ORTHELLO YOU BASTARD, THIS IS HOW TO DECIDE POSITIONS FOR ORTHELLO SPRITES
		//newbullet.transform.position = bulletpos;
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    	Vector3 bulletpoint = ray.origin + (ray.direction * 1000);
		//Debug.Log("mouse:" + bulletpoint.x + " " + bulletpoint.y);
		
		Vector2 differencepos = (Vector2)bulletpoint - bulletpos;
		float deg = Mathf.Rad2Deg*Mathf.Atan(differencepos.y/differencepos.x);
		
		
		int quadr = 0;//records which quadrant was clicked
		
		//checking where player is clicking and adjusting it for 8way using the deg variable
		if(bulletpoint.x > bulletpos.x && bulletpoint.y >= bulletpos.y)//quadrant 1
		{
			quadr = 1;
			//Debug.Log("QUADRANT 1");
			
			if(deg <= 11.25)//target right
			{
				newbullet.GetComponent<RotationBullet>().rotation = 0;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 2 o clock
			{
				newbullet.GetComponent<RotationBullet>().rotation = 22.5f;
			}
			else if(deg <=56.25 && deg > 33.75)//target upright
			{
				newbullet.GetComponent<RotationBullet>().rotation = 45;
			}
			else if(deg <= 78.75 && deg > 56.25)//target 1 o clock
			{
				newbullet.GetComponent<RotationBullet>().rotation =67.5f;
			}
			else if(deg > 78.75)//target up
			{
				newbullet.GetComponent<RotationBullet>().rotation =90;
			}
		}
		else if(bulletpoint.x <= bulletpos.x && bulletpoint.y > bulletpos.y)//quadrant 2
		{
			quadr = 2;
			
			if(deg <= -78.75)//up
			{
				newbullet.GetComponent<RotationBullet>().rotation =90;
			}
			else if(deg <= -56.25 && deg > -78.75)//11
			{
				newbullet.GetComponent<RotationBullet>().rotation = 112.5f;
			}
			else if(deg <= -33.75 && deg > -56.25)//upleft
			{
				newbullet.GetComponent<RotationBullet>().rotation =135;
			}
			else if(deg <= -11.25 && deg > -33.75)//10
			{
				newbullet.GetComponent<RotationBullet>().rotation =157.5f;
			}
			else if(deg > -11.25)//left
			{
				newbullet.GetComponent<RotationBullet>().rotation =180;
			}
		}
		else if(bulletpoint.x < bulletpos.x && bulletpoint.y <= bulletpos.y)//quadrant 3
		{
			quadr = 3;
			
			if(deg <= 11.25)//target left
			{
				newbullet.GetComponent<RotationBullet>().rotation =180;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 8 o clock
			{
				newbullet.GetComponent<RotationBullet>().rotation =202.5f;
			}
			else if(deg <=56.25 && deg > 33.75)//target downleft
			{
				newbullet.GetComponent<RotationBullet>().rotation =225;
			}
			else if(deg <= 78.75 && deg > 56.25)//target 7 o clock
			{
				newbullet.GetComponent<RotationBullet>().rotation =247.5f;
			}
			else if(deg > 78.75)//target down
			{
				newbullet.GetComponent<RotationBullet>().rotation =270;
			}
			
		}
		else if(bulletpoint.x >= bulletpos.x && bulletpoint.y < bulletpos.y)//quadrant 4
		{
			quadr = 4;
			
			if(deg <= -78.75)//down
			{
				newbullet.GetComponent<RotationBullet>().rotation =270;
			}
			else if(deg <= -56.25 && deg > -78.75)//5
			{
				newbullet.GetComponent<RotationBullet>().rotation =292.5f;
			}
			else if(deg <= -33.75 && deg > -56.25)//downright
			{
				newbullet.GetComponent<RotationBullet>().rotation =315;
			}
			else if(deg <= -11.25 && deg > -33.75)//4
			{
				newbullet.GetComponent<RotationBullet>().rotation =337.5f;
			}
			else if(deg > -11.25)//right
			{

				newbullet.GetComponent<RotationBullet>().rotation =0;
			}
		}
		
		yield return new WaitForSeconds(rateoffire);
		
		shooting = false;
	}//end shoot()
	
	IEnumerator spreadShot(Vector2 bulletpos, GameObject bullet)
	{
		xa.sc.bulletIncrease();
		xa.sc.bulletIncrease();
		xa.sc.bulletIncrease();
		
		
		//Vector2 bulletpos = playerpos;
		
		GameObject newbullet = (GameObject)Instantiate(bullet);//create a new bullet object
		GameObject newbullet2 = (GameObject)Instantiate(bullet);
		GameObject newbullet3 = (GameObject)Instantiate(bullet);
		
		Destroy (newbullet,5);//destroys the newly created object in 3 seconds
		Destroy (newbullet2,5);
		Destroy (newbullet3,5);
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    	Vector3 bulletpoint = ray.origin + (ray.direction * 1000);
		//Debug.Log("mouse:" + bulletpoint.x + " " + bulletpoint.y);
		
		Vector2 differencepos = (Vector2)bulletpoint - bulletpos;
		float deg = Mathf.Rad2Deg*Mathf.Atan(differencepos.y/differencepos.x);
		
		int quadr = 0;//records which quadrant was clicked
		
		//checking where player is clicking and adjusting it for 8way using the deg variable
		if(bulletpoint.x > bulletpos.x && bulletpoint.y >= bulletpos.y)//quadrant 1
		{
			quadr = 1;
			//Debug.Log("QUADRANT 1");
			
			if(deg <= 11.25)//target right
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
				//newbullet.GetComponent<OTSprite>().rotation = 0;
				
				newbullet2.GetComponent<Bullet>().moveDirX = 1;
				newbullet2.GetComponent<Bullet>().moveDirY= .5f;
				newbullet2.GetComponent<OTSprite>().rotation = 22.5f;
				
				newbullet3.GetComponent<Bullet>().moveDirX=1;
				newbullet3.GetComponent<Bullet>().moveDirY=-.5f;
				newbullet3.GetComponent<OTSprite>().rotation = 337.5f;
				
				//bulletpos.x+=1;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 2 o clock
			{
				newbullet.GetComponent<Bullet>().moveDirX = 1;
				newbullet.GetComponent<Bullet>().moveDirY= .5f;
				newbullet.GetComponent<OTSprite>().rotation = 22.5f;
				
				newbullet2.GetComponent<Bullet>().moveDirX=1;
				newbullet2.GetComponent<Bullet>().moveDirY=0;
				
				newbullet3.GetComponent<Bullet>().moveDirX=1;
				newbullet3.GetComponent<Bullet>().moveDirY=1;
				newbullet3.GetComponent<OTSprite>().rotation = 45;
				
				/*bulletpos.x += 1;
				bulletpos.y += .5f;*/
			}
			else if(deg <=56.25 && deg > 33.75)//target upright
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=1;
				newbullet.GetComponent<OTSprite>().rotation = 45;
				
				newbullet2.GetComponent<Bullet>().moveDirX = 1;
				newbullet2.GetComponent<Bullet>().moveDirY= .5f;
				newbullet2.GetComponent<OTSprite>().rotation = 22.5f;
				
				newbullet3.GetComponent<Bullet>().moveDirX = .5f;
				newbullet3.GetComponent<Bullet>().moveDirY= 1;
				newbullet3.GetComponent<OTSprite>().rotation = 67.5f;
				
				/*bulletpos.x += 1;
				bulletpos.y += 1;*/
			}
			else if(deg <= 78.75 && deg > 56.25)//target 1 o clock
			{
				newbullet.GetComponent<Bullet>().moveDirX = .5f;
				newbullet.GetComponent<Bullet>().moveDirY= 1;
				newbullet.GetComponent<OTSprite>().rotation = 67.5f;
				
				newbullet2.GetComponent<Bullet>().moveDirX=1;
				newbullet2.GetComponent<Bullet>().moveDirY=1;
				newbullet2.GetComponent<OTSprite>().rotation = 45;
				
				newbullet3.GetComponent<Bullet>().moveDirX = 0;
				newbullet3.GetComponent<Bullet>().moveDirY= 1;
				newbullet3.GetComponent<OTSprite>().rotation = 90;
				
				/*bulletpos.x += .5f;
				bulletpos.y += 1;*/
			}
			else if(deg > 78.75)//target up
			{
				newbullet.GetComponent<Bullet>().moveDirX = 0;
				newbullet.GetComponent<Bullet>().moveDirY= 1;
				newbullet.GetComponent<OTSprite>().rotation = 90;
				
				newbullet2.GetComponent<Bullet>().moveDirX = .5f;
				newbullet2.GetComponent<Bullet>().moveDirY= 1;
				newbullet2.GetComponent<OTSprite>().rotation = 67.5f;
				
				newbullet3.GetComponent<Bullet>().moveDirX=-.5f;
				newbullet3.GetComponent<Bullet>().moveDirY=1;
				newbullet3.GetComponent<OTSprite>().rotation = 112.5f;
				
				/*bulletpos.x += 0;
				bulletpos.y += 1;*/
			}
		}
		else if(bulletpoint.x <= bulletpos.x && bulletpoint.y > bulletpos.y)//quadrant 2
		{
			quadr = 2;
			
			if(deg <= -78.75)//up
			{
				newbullet.GetComponent<Bullet>().moveDirX=0;
				newbullet.GetComponent<Bullet>().moveDirY=1;
				newbullet.GetComponent<OTSprite>().rotation = 90;
				
				newbullet2.GetComponent<Bullet>().moveDirX = .5f;
				newbullet2.GetComponent<Bullet>().moveDirY= 1;
				newbullet2.GetComponent<OTSprite>().rotation = 67.5f;
				
				newbullet3.GetComponent<Bullet>().moveDirX=-.5f;
				newbullet3.GetComponent<Bullet>().moveDirY=1;
				newbullet3.GetComponent<OTSprite>().rotation = 112.5f;
				
				/*bulletpos.x += 0;
				bulletpos.y += 1;*/
			}
			else if(deg <= -56.25 && deg > -78.75)//11
			{
				newbullet.GetComponent<Bullet>().moveDirX=-.5f;
				newbullet.GetComponent<Bullet>().moveDirY=1;
				newbullet.GetComponent<OTSprite>().rotation = 112.5f;
				
				newbullet2.GetComponent<Bullet>().moveDirX=0;
				newbullet2.GetComponent<Bullet>().moveDirY=1;
				newbullet2.GetComponent<OTSprite>().rotation = 90;
				
				newbullet3.GetComponent<Bullet>().moveDirX=-1;
				newbullet3.GetComponent<Bullet>().moveDirY=1;
				newbullet3.GetComponent<OTSprite>().rotation = 135;
				
				/*bulletpos.x += -.5f;
				bulletpos.y += 1;*/
			}
			else if(deg <= -33.75 && deg > -56.25)//upleft
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=1;
				newbullet.GetComponent<OTSprite>().rotation = 135;
				
				newbullet2.GetComponent<Bullet>().moveDirX=-.5f;
				newbullet2.GetComponent<Bullet>().moveDirY=1;
				newbullet2.GetComponent<OTSprite>().rotation = 112.5f;
				
				newbullet3.GetComponent<Bullet>().moveDirX=-1;
				newbullet3.GetComponent<Bullet>().moveDirY=.5f;
				newbullet3.GetComponent<OTSprite>().rotation = 157.5f;
				
				/*bulletpos.x += -1;
				bulletpos.y += 1;*/
			}
			else if(deg <= -11.25 && deg > -33.75)//10
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=.5f;
				newbullet.GetComponent<OTSprite>().rotation = 157.5f;
				
				newbullet2.GetComponent<Bullet>().moveDirX=-1;
				newbullet2.GetComponent<Bullet>().moveDirY=1;
				newbullet2.GetComponent<OTSprite>().rotation = 135;
				
				newbullet3.GetComponent<Bullet>().moveDirX=-1;
				newbullet3.GetComponent<Bullet>().moveDirY=0;
				newbullet3.GetComponent<OTSprite>().rotation = 180;
				
				/*bulletpos.x += -1;
				bulletpos.y += .5f;*/
			}
			else if(deg > -11.25)//left
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
				newbullet.GetComponent<OTSprite>().rotation = 180;
				
				newbullet2.GetComponent<Bullet>().moveDirX=-1;
				newbullet2.GetComponent<Bullet>().moveDirY=.5f;
				newbullet2.GetComponent<OTSprite>().rotation = 157.5f;
				
				newbullet3.GetComponent<Bullet>().moveDirX = -1;
				newbullet3.GetComponent<Bullet>().moveDirY= -.5f;
				newbullet3.GetComponent<OTSprite>().rotation = 202.5f;
				
				/*bulletpos.x += -1;
				bulletpos.y += 0;*/
			}
		}
		else if(bulletpoint.x < bulletpos.x && bulletpoint.y <= bulletpos.y)//quadrant 3
		{
			quadr = 3;
			
			if(deg <= 11.25)//target left
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
				newbullet.GetComponent<OTSprite>().rotation = 180;
				
				newbullet2.GetComponent<Bullet>().moveDirX=-1;
				newbullet2.GetComponent<Bullet>().moveDirY=.5f;
				newbullet2.GetComponent<OTSprite>().rotation = 157.5f;
				
				newbullet3.GetComponent<Bullet>().moveDirX = -1;
				newbullet3.GetComponent<Bullet>().moveDirY= -.5f;
				newbullet3.GetComponent<OTSprite>().rotation = 202.5f;
				
				/*bulletpos.x += -1;
				bulletpos.y += 0;*/
			}
			else if(deg <= 33.75 && deg > 11.25)//target 8 o clock
			{
				newbullet.GetComponent<Bullet>().moveDirX = -1;
				newbullet.GetComponent<Bullet>().moveDirY= -.5f;
				newbullet.GetComponent<OTSprite>().rotation = 202.5f;
				
				newbullet2.GetComponent<Bullet>().moveDirX=-1;
				newbullet2.GetComponent<Bullet>().moveDirY=0;
				newbullet2.GetComponent<OTSprite>().rotation = 180;
				
				newbullet3.GetComponent<Bullet>().moveDirX=-1;
				newbullet3.GetComponent<Bullet>().moveDirY=-1;
				newbullet3.GetComponent<OTSprite>().rotation = 225;
				
				/*bulletpos.x += -1;
				bulletpos.y += -.5f;*/
			}
			else if(deg <=56.25 && deg > 33.75)//target downleft
			{
				newbullet.GetComponent<Bullet>().moveDirX=-1;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
				newbullet.GetComponent<OTSprite>().rotation = 225;
				
				newbullet2.GetComponent<Bullet>().moveDirX = -1;
				newbullet2.GetComponent<Bullet>().moveDirY= -.5f;
				newbullet2.GetComponent<OTSprite>().rotation = 202.5f;
				
				newbullet3.GetComponent<Bullet>().moveDirX = -.5f;
				newbullet3.GetComponent<Bullet>().moveDirY= -1;
				newbullet3.GetComponent<OTSprite>().rotation = 247.5f;
				
				/*bulletpos.x += -1;
				bulletpos.y += -1;*/
			}
			else if(deg <= 78.75 && deg > 56.25)//target 7 o clock
			{
				newbullet.GetComponent<Bullet>().moveDirX = -.5f;
				newbullet.GetComponent<Bullet>().moveDirY= -1;
				newbullet.GetComponent<OTSprite>().rotation = 247.5f;
				
				newbullet2.GetComponent<Bullet>().moveDirX=-1;
				newbullet2.GetComponent<Bullet>().moveDirY=-1;
				newbullet2.GetComponent<OTSprite>().rotation = 225;
				
				newbullet3.GetComponent<Bullet>().moveDirX = 0;
				newbullet3.GetComponent<Bullet>().moveDirY= -1;
				newbullet3.GetComponent<OTSprite>().rotation = 270;
				
				/*bulletpos.x += -.5f;
				bulletpos.y += -1;*/
			}
			else if(deg > 78.75)//target down
			{
				newbullet.GetComponent<Bullet>().moveDirX = 0;
				newbullet.GetComponent<Bullet>().moveDirY= -1;
				newbullet.GetComponent<OTSprite>().rotation = 270;
				
				newbullet2.GetComponent<Bullet>().moveDirX = -.5f;
				newbullet2.GetComponent<Bullet>().moveDirY= -1;
				newbullet2.GetComponent<OTSprite>().rotation = 247.5f;
				
				newbullet3.GetComponent<Bullet>().moveDirX=.5f;
				newbullet3.GetComponent<Bullet>().moveDirY=-1;
				newbullet3.GetComponent<OTSprite>().rotation = 292.5f;
				
				/*bulletpos.x += 0;
				bulletpos.y += -1;*/
			}
			
		}
		else if(bulletpoint.x >= bulletpos.x && bulletpoint.y < bulletpos.y)//quadrant 4
		{
			quadr = 4;
			
			if(deg <= -78.75)//down
			{
				newbullet.GetComponent<Bullet>().moveDirX=0;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
				newbullet.GetComponent<OTSprite>().rotation = 270;
				
				newbullet2.GetComponent<Bullet>().moveDirX = -.5f;
				newbullet2.GetComponent<Bullet>().moveDirY= -1;
				newbullet2.GetComponent<OTSprite>().rotation = 247.5f;
				
				newbullet3.GetComponent<Bullet>().moveDirX=.5f;
				newbullet3.GetComponent<Bullet>().moveDirY=-1;
				newbullet3.GetComponent<OTSprite>().rotation = 292.5f;
				
				/*bulletpos.x += 0;
				bulletpos.y += -1;*/
			}
			else if(deg <= -56.25 && deg > -78.75)//5
			{
				newbullet.GetComponent<Bullet>().moveDirX=.5f;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
				newbullet.GetComponent<OTSprite>().rotation = 292.5f;
				
				newbullet2.GetComponent<Bullet>().moveDirX=0;
				newbullet2.GetComponent<Bullet>().moveDirY=-1;
				newbullet2.GetComponent<OTSprite>().rotation = 270;
				
				newbullet3.GetComponent<Bullet>().moveDirX=1;
				newbullet3.GetComponent<Bullet>().moveDirY=-1;
				newbullet3.GetComponent<OTSprite>().rotation = 315;
				
				/*bulletpos.x += .5f;
				bulletpos.y += -1;*/
			}
			else if(deg <= -33.75 && deg > -56.25)//downright
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=-1;
				newbullet.GetComponent<OTSprite>().rotation = 315;
				
				newbullet2.GetComponent<Bullet>().moveDirX=.5f;
				newbullet2.GetComponent<Bullet>().moveDirY=-1;
				newbullet2.GetComponent<OTSprite>().rotation = 292.5f;
				
				newbullet3.GetComponent<Bullet>().moveDirX=1;
				newbullet3.GetComponent<Bullet>().moveDirY=-.5f;
				newbullet3.GetComponent<OTSprite>().rotation = 337.5f;
				
				/*bulletpos.x += 1;
				bulletpos.y += -1;*/
			}
			else if(deg <= -11.25 && deg > -33.75)//4
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=-.5f;
				newbullet.GetComponent<OTSprite>().rotation = 337.5f;
				
				newbullet2.GetComponent<Bullet>().moveDirX=1;
				newbullet2.GetComponent<Bullet>().moveDirY=-1;
				newbullet2.GetComponent<OTSprite>().rotation = 315;
				
				newbullet3.GetComponent<Bullet>().moveDirX=1;
				newbullet3.GetComponent<Bullet>().moveDirY=0;
				newbullet3.GetComponent<OTSprite>().rotation = 0;
				
				/*bulletpos.x += 1;
				bulletpos.y += -.5f;*/
			}
			else if(deg > -11.25)//right
			{
				newbullet.GetComponent<Bullet>().moveDirX=1;
				newbullet.GetComponent<Bullet>().moveDirY=0;
				newbullet.GetComponent<OTSprite>().rotation = 0;
				
				newbullet2.GetComponent<Bullet>().moveDirX=1;
				newbullet2.GetComponent<Bullet>().moveDirY=-.5f;
				newbullet2.GetComponent<OTSprite>().rotation = 337.5f;
				
				newbullet3.GetComponent<Bullet>().moveDirX = 1;
				newbullet3.GetComponent<Bullet>().moveDirY= .5f;
				newbullet3.GetComponent<OTSprite>().rotation = 22.5f;
				
				/*bulletpos.x += 1;
				bulletpos.y += 0;*/
			}
		}
		else
		{
			quadr = 0;
			//Debug.Log("LACK OF QUADRANT?");
		}
		
		newbullet.GetComponent<OTSprite>().position = bulletpos;
		newbullet2.GetComponent<OTSprite>().position = bulletpos;
		newbullet3.GetComponent<OTSprite>().position = bulletpos;
		
		shooting = true;
		yield return new WaitForSeconds(rateoffire);
		shooting = false;
	}//end spreadShot
	
	IEnumerator spreadShot2(Vector2 bulletpos, GameObject bullet)
	{
		xa.sc.bulletIncrease();
		xa.sc.bulletIncrease();
		xa.sc.bulletIncrease();
		
		
		//Vector2 bulletpos = playerpos;
		
		GameObject centerbullet = (GameObject)Instantiate(bullet);//create a new bullet object
		GameObject leftbullet = (GameObject)Instantiate(bullet);
		GameObject rightbullet = (GameObject)Instantiate(bullet);
		
		Destroy (centerbullet,5);//destroys the newly created object in 3 seconds
		Destroy (leftbullet,5);
		Destroy (rightbullet,5);
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    	Vector3 bulletpoint = ray.origin + (ray.direction * 1000);
		//Debug.Log("mouse:" + bulletpoint.x + " " + bulletpoint.y);
		
		Vector2 differencepos = (Vector2)bulletpoint - bulletpos;
		float deg = Mathf.Rad2Deg*Mathf.Atan(differencepos.y/differencepos.x);
		
		int quadr = 0;//records which quadrant was clicked
		
		//checking where player is clicking and adjusting it for 8way using the deg variable
		if(bulletpoint.x > bulletpos.x && bulletpoint.y >= bulletpos.y)//quadrant 1
		{
			quadr = 1;
			//Debug.Log("QUADRANT 1");
			
			if(deg <= 11.25)//target right
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 0;
				leftbullet.GetComponent<RotationBullet>().rotation = 22.5f;
				rightbullet.GetComponent<RotationBullet>().rotation = 337.5f;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 2 o clock
			{

				centerbullet.GetComponent<RotationBullet>().rotation = 22.5f;
				leftbullet.GetComponent<RotationBullet>().rotation = 0;
				rightbullet.GetComponent<RotationBullet>().rotation = 45;
			}
			else if(deg <=56.25 && deg > 33.75)//target upright
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 45;
				leftbullet.GetComponent<RotationBullet>().rotation = 22.5f;
				rightbullet.GetComponent<RotationBullet>().rotation = 67.5f;
			}
			else if(deg <= 78.75 && deg > 56.25)//target 1 o clock
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 67.5f;
				leftbullet.GetComponent<RotationBullet>().rotation = 45;
				rightbullet.GetComponent<RotationBullet>().rotation = 90;
			}
			else if(deg > 78.75)//target up
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 90;
				leftbullet.GetComponent<RotationBullet>().rotation = 67.5f;
				rightbullet.GetComponent<RotationBullet>().rotation = 112.5f;
			}
		}
		else if(bulletpoint.x <= bulletpos.x && bulletpoint.y > bulletpos.y)//quadrant 2
		{
			quadr = 2;
			
			if(deg <= -78.75)//up
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 90;
				leftbullet.GetComponent<RotationBullet>().rotation = 67.5f;
				rightbullet.GetComponent<RotationBullet>().rotation = 112.5f;
			}
			else if(deg <= -56.25 && deg > -78.75)//11
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 112.5f;
				leftbullet.GetComponent<RotationBullet>().rotation = 90;
				rightbullet.GetComponent<RotationBullet>().rotation = 135;
			}
			else if(deg <= -33.75 && deg > -56.25)//upleft
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 135;
				leftbullet.GetComponent<RotationBullet>().rotation = 112.5f;
				rightbullet.GetComponent<RotationBullet>().rotation = 157.5f;
			}
			else if(deg <= -11.25 && deg > -33.75)//10
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 157.5f;
				leftbullet.GetComponent<RotationBullet>().rotation = 135;
				rightbullet.GetComponent<RotationBullet>().rotation = 180;
			}
			else if(deg > -11.25)//left
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 180;
				leftbullet.GetComponent<RotationBullet>().rotation = 157.5f;
				rightbullet.GetComponent<RotationBullet>().rotation = 202.5f;				
			}
		}
		else if(bulletpoint.x < bulletpos.x && bulletpoint.y <= bulletpos.y)//quadrant 3
		{
			quadr = 3;
			
			if(deg <= 11.25)//target left
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 180;
				leftbullet.GetComponent<RotationBullet>().rotation = 157.5f;
				rightbullet.GetComponent<RotationBullet>().rotation = 202.5f;
			}
			else if(deg <= 33.75 && deg > 11.25)//target 8 o clock
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 202.5f;
				leftbullet.GetComponent<RotationBullet>().rotation = 180;
				rightbullet.GetComponent<RotationBullet>().rotation = 225;
			}
			else if(deg <=56.25 && deg > 33.75)//target downleft
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 225;
				leftbullet.GetComponent<RotationBullet>().rotation = 202.5f;
				rightbullet.GetComponent<RotationBullet>().rotation = 247.5f;
			}
			else if(deg <= 78.75 && deg > 56.25)//target 7 o clock
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 247.5f;
				leftbullet.GetComponent<RotationBullet>().rotation = 225;
				rightbullet.GetComponent<RotationBullet>().rotation = 270;
			}
			else if(deg > 78.75)//target down
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 270;
				leftbullet.GetComponent<RotationBullet>().rotation = 247.5f;
				rightbullet.GetComponent<RotationBullet>().rotation = 292.5f;
			}
			
		}
		else if(bulletpoint.x >= bulletpos.x && bulletpoint.y < bulletpos.y)//quadrant 4
		{
			quadr = 4;
			
			if(deg <= -78.75)//down
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 270;
				leftbullet.GetComponent<RotationBullet>().rotation = 247.5f;
				rightbullet.GetComponent<RotationBullet>().rotation = 292.5f;
			}
			else if(deg <= -56.25 && deg > -78.75)//5
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 292.5f;
				leftbullet.GetComponent<RotationBullet>().rotation = 270;
				rightbullet.GetComponent<RotationBullet>().rotation = 315;
			}
			else if(deg <= -33.75 && deg > -56.25)//downright
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 315;
				leftbullet.GetComponent<RotationBullet>().rotation = 292.5f;
				rightbullet.GetComponent<RotationBullet>().rotation = 337.5f;
			}
			else if(deg <= -11.25 && deg > -33.75)//4
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 337.5f;
				leftbullet.GetComponent<RotationBullet>().rotation = 315;
				rightbullet.GetComponent<RotationBullet>().rotation = 0;
			}
			else if(deg > -11.25)//right
			{
				centerbullet.GetComponent<RotationBullet>().rotation = 0;
				leftbullet.GetComponent<RotationBullet>().rotation = 337.5f;
				rightbullet.GetComponent<RotationBullet>().rotation = 22.5f;
			}
		}
		
		centerbullet.GetComponent<OTSprite>().position = bulletpos;
		leftbullet.GetComponent<OTSprite>().position = bulletpos;
		rightbullet.GetComponent<OTSprite>().position = bulletpos;
		
		//centerbullet.GetComponent<RotationBullet>().damage *=.5f;
		leftbullet.GetComponent<RotationBullet>().damage *=.5f;
		rightbullet.GetComponent<RotationBullet>().damage *=.5f;
		
		shooting = true;
		yield return new WaitForSeconds(rateoffire);
		shooting = false;
	}//end spreadShot
}//end class