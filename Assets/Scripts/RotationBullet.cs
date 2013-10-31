using UnityEngine;
using System.Collections;

public class RotationBullet : MonoBehaviour {
	
	public float rotation = 0;
	public int speed = 1;
	public int damage = 100;
	//public bool isenemybullet = false;
	
	private Vector2 movement;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		gameObject.GetComponent<OTSprite>().rotation = rotation;
		movement = new Vector2(Mathf.Cos(Mathf.Deg2Rad*rotation),Mathf.Sin(Mathf.Deg2Rad*rotation));
		movement *= Time.deltaTime*speed;
		//movement = transform.forward*Input.GetAxis("Vertical")*speed;
		GetComponent<OTSprite>().position+=(Vector2)movement;
	}
	
	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}
		
		/*if(isenemybullet)
		{
			if(other.gameObject.CompareTag("Player"))
			{
				if(!other.gameObject.GetComponent<Player>().isDead())
				{
					other.gameObject.GetComponent<Player>().killPlayer();
					Destroy (gameObject);
				}
			}
		}
		else*/
		if(gameObject.CompareTag("Bullet"))
		{
			if(other.gameObject.CompareTag("Enemy"))
			{
				/*gameObject.GetComponent<EnemyProperties>().health -=other.gameObject.GetComponent<Bullet>().getDamage();
				Destroy(other.gameObject);//destroy the bullet
				StartCoroutine(hitFlash()*/
				//other.gameObject.GetComponent<EnemyProperties>().health -= damage;
				if(!other.gameObject.GetComponent<EnemyProperties>().dead)
				{
					other.gameObject.GetComponent<EnemyProperties>().hitEnemy(damage);
					Destroy (gameObject);
				}
			}
		}
	}
}
