using UnityEngine;
using System.Collections;

public class EnemySkullRand : MonoBehaviour {
	
	public int chance = 70;
	public int frenzychance = 5;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Awake()
	{
		if(Random.Range(0,100)<frenzychance)
		{
			gameObject.GetComponent<EnemyProperties>().frenzy=true;
		}
		
		if(Random.Range(0,100)<chance)
		{
			Destroy(gameObject);
		}	
	}
}
