﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public float moveDirX = 1;
	public float moveDirY = 1;
	private Vector3 movement;
	public int moveSpeed = 15;
	
	private int damage = 100;
	
	private GameObject thisGameObject;
	
	// Use this for initialization
	void Start () {
		
		thisGameObject = gameObject;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		movement = new Vector3(moveDirX, moveDirY,0f);
		movement *= Time.deltaTime*moveSpeed;
		GetComponent<OTSprite>().position+=(Vector2)movement;
	
	}
	
	public void destroyMe()
	{
		Destroy(thisGameObject);
	}
	
	public int getDamage()
	{
		return damage;
	}
	
	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.CompareTag("Wall"))
		{
			destroyMe();
		}
		
		if(other.gameObject.CompareTag("Enemy"))
		{
			if(!other.gameObject.GetComponent<EnemyProperties>().dead)
			{
				other.gameObject.GetComponent<EnemyProperties>().hitEnemy(damage);
				Destroy (gameObject);
			}
		}
	}
}
