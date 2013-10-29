using UnityEngine;
using System.Collections;

public class EnemyRotationBullet : MonoBehaviour {
	
	public int rotation = 0;
	private Vector2 movement;
	public int speed = 1;

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
		
		if(other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<Player>().killPlayer();
			Destroy (gameObject);
		}
	}
}
