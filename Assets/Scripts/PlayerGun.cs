using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour {
	
	int quadr = 0;
	float deg = 0;
	float rotation = 0;
	bool shooting = false;
	float rateoffire = 0.1f;
	Vector2 gunpos;
	float rotateby;
	
	public int gunid;
	public bool isActive;
	public GameObject bullet;

	// Use this for initialization
	void Start () {
		shooting = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		gunpos = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y);
		
		if(gunid==0)
		{
			Debug.Log("gunid not assigned!");
		}
		
		isActive=false;
		
		if(PlayerWeapon.numofguns==1)
		{
			if(gunid==3)
				isActive=true;
		}
		else if(PlayerWeapon.numofguns==2)
		{
			if(gunid==2)
				isActive=true;
			if(gunid==4)
				isActive=true;
		}
		else if(PlayerWeapon.numofguns==3)
		{
			if(gunid==2 || gunid==3 || gunid==4)
				isActive=true;
		}
		else if(PlayerWeapon.numofguns==4)
		{
			if(gunid==1 || gunid==5 || gunid==7 || gunid==9)
				isActive=true;
		}
		else if(PlayerWeapon.numofguns==5)
		{
			if(gunid==1 || gunid==5 || gunid==7 || gunid==9 || gunid==3)
				isActive=true;
		}
		
		if(isActive)
		{
			gameObject.GetComponent<MeshRenderer>().enabled = true;
			quadr = PlayerWeapon.quadr;
			deg = PlayerWeapon.deg;
		
			getRotation();
			
			rotateby = Time.deltaTime * 250;
			gameObject.GetComponent<OTSprite>().rotation+= rotateby;
		
			if(xa.isShoot && !shooting && !xa.gamepaused)
			{
				//StartCoroutine (shoot(bullet));
				StartCoroutine (spreadShot(bullet));
			}
		}
		else
		{
			gameObject.GetComponent<MeshRenderer>().enabled = false;
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
		
		GameObject newbullet = (GameObject)Instantiate(bullet);
		Destroy (newbullet,5);//destroys the newly created object in 3 seconds
		
		newbullet.GetComponent<OTSprite>().position = gunpos;

		newbullet.GetComponent<RotationBullet>().rotation = rotation;
		
		yield return new WaitForSeconds(rateoffire);
		
		shooting = false;
	}//end shoot()
	
	IEnumerator spreadShot(GameObject bullet)
	{
		shooting = true;
		xa.sc.bulletIncrease();
		xa.sc.bulletIncrease();
		xa.sc.bulletIncrease();
		
		GameObject newbullet1 = (GameObject)Instantiate (bullet);
		GameObject newbullet2 = (GameObject)Instantiate (bullet);
		GameObject newbullet3 = (GameObject)Instantiate (bullet);
		Destroy (newbullet1,5);
		Destroy (newbullet2,5);
		Destroy (newbullet3,5);
		newbullet1.GetComponent<OTSprite>().position = gunpos;
		newbullet2.GetComponent<OTSprite>().position = gunpos;
		newbullet3.GetComponent<OTSprite>().position = gunpos;
		newbullet1.GetComponent<RotationBullet>().rotation = rotation + 22.5f;
		newbullet2.GetComponent<RotationBullet>().rotation = rotation;
		newbullet3.GetComponent<RotationBullet>().rotation = rotation - 22.5f;
			
		yield return new WaitForSeconds(rateoffire);
		shooting = false;
	}//end spreadShot
}
