using UnityEngine;
using System.Collections;

public class PlayerHitbox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<MeshRenderer>().enabled =false;
		if(xa.isShift)
		{
			gameObject.GetComponent<MeshRenderer>().enabled =true;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		//gameObject.GetComponent<Player>().isDead()
		if(!transform.parent.gameObject.GetComponent<Player>().isDead())
		{
			if(other.gameObject.CompareTag("Enemy"))
			{
				if(!other.GetComponent<EnemyProperties>().dead)
				{
					transform.parent.gameObject.GetComponent<Player>().killPlayer();
				}
				
			}
			else if(other.gameObject.CompareTag("EnemyBullet"))
			{
				//gameObject.GetComponent<Player>().killPlayer ();
				transform.parent.gameObject.GetComponent<Player>().killPlayer();
				Destroy(other.gameObject);
			}
		}
	}
}
