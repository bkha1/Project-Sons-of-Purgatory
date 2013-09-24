using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	private bool isSpawning = false;
	public GameObject enemyToSpawn;
	private Vector2 spawnlocation = new Vector2(0,0);
	
	private double spawnrate = 1.5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(xa.sc.getKillValue());
		
		if(xa.sc.getKillValue() > 200)
		{
			spawnrate = .6;
			Debug.Log ("spawnrate .6");
		}
		else if(xa.sc.getKillValue() > 150)
		{
			spawnrate = .8;
		}
		else if(xa.sc.getKillValue() > 100)
		{
			spawnrate = 1;
		}
		else if(xa.sc.getKillValue() > 50)
		{
			spawnrate = 1.2;
		}
		
		if(!isSpawning)
		{
			StartCoroutine(spawnEnemy(1));
			StartCoroutine(spawnEnemy(2));
			StartCoroutine(spawnEnemy(3));
			StartCoroutine(spawnEnemy(4));
		}
	
	}
	
	IEnumerator spawnEnemy(int side)
	{
		isSpawning = true;
		GameObject newEnemy = (GameObject)Instantiate(enemyToSpawn);
		
		if(side == 1)
		{
			spawnlocation = new Vector2(0,9);
		}
		else if(side == 2)
		{
			spawnlocation = new Vector2(12,0);
		}
		else if(side == 3)
		{
			spawnlocation = new Vector2(0,-9);
		}
		else if(side == 4)
		{
			spawnlocation = new Vector2(-12,0);
		}
		
		newEnemy.GetComponent<OTAnimatingSprite>().position = spawnlocation;
			
		yield return new WaitForSeconds((float)spawnrate);
		
		isSpawning = false;
	}
}
