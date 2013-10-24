using UnityEngine;
using System.Collections;

public class Level1RoomClear : MonoBehaviour {
	
	private static bool visited = false;
	private GameObject[] enemies;
	private GameObject[] items;
	private GameObject[] specialtiles;
	
	// Use this for initialization
	void Start () {
		if(visited)
		{
			/*if(enemies!=null)
			{
				for(int i=0; i<enemies.Length;i++)
				{
					Instantiate(enemies[i]);
				}
			}*/
			//if(GameObject.FindGameObjectWithTag("Enemy")!=null)
			//{
				//for(int i
			//}
		}
		else
		{
			visited = true;
		}
		
		/*if(!visited)
		{
			enemies = GameObject.FindGameObjectsWithTag("Enemy");
			recordObjects();
			visited = true;
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		checkCleared();
		//recordObjects();
		
		/*if(GameObject.FindGameObjectWithTag("Enemy")==null)
		{
			//Debug.Log("ROOM CLEARED");
			if(xa.visitgrid != null)
			{
				xa.visitgrid[xa.currentposi,xa.currentposj] = 1;
			}
		}*/
	}
	
	void recordObjects()
	{
		if(enemies!=null)
		{
			for(int i=0;i<enemies.Length;i++)
			{
				DontDestroyOnLoad(enemies[i]);
			}
		}
	}//end recordObjects
	
	void checkCleared()
	{
		bool ch = true;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		if(enemies!=null)
		{
			for(int i=0;i<enemies.Length;i++)
			{
				if(!enemies[i].GetComponent<EnemyProperties>().dead)
				{
					ch = false;
				}
			}
		}
		
		if(ch)
		{
			if(xa.visitgrid != null)
			{xa.visitgrid[xa.currentposi,xa.currentposj] = 1;}
		}
	}//end checkCleared
}
