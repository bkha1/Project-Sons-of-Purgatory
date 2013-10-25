using UnityEngine;
using System.Collections;

public class EnemyProperties : MonoBehaviour {
	
	public int health = 100;
	public bool dead = false;
	public bool frenzy = false;
	public int id = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		healthCheck();
	
	}
	
	void healthCheck()
	{
		if(health<0)
		{
			dead = true;
		}
	}
}
