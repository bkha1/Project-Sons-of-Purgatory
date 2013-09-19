using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public int moveDirX = 1;
	public int moveDirY = 1;
	private Vector3 movement;
	public int moveSpeed = 15;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		movement = new Vector3(moveDirX, moveDirY,0f);
		movement *= Time.deltaTime*moveSpeed;
		GetComponent<OTSprite>().position+=(Vector2)movement;
	
	}
}
