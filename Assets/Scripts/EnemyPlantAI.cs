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
			if(!isattacking)
			{
				attacking();
			}
		}
		else
		{
			gameObject.GetComponent<OTSprite>().tintColor = Color.black;
		}
	}
	
	private bool isattacking = false;
			
	IEnumerator attacking()
	{
		isattacking = true;
		yield return new WaitForSeconds(.5f);
		isattacking = false;
	}//end attacking
	
	IEnumerator hitFlash()
	{
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
