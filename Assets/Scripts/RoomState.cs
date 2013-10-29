using UnityEngine;
using System.Collections;

public class RoomState : MonoBehaviour {
	
	private bool childrenenabled = true;
	private GameObject[] enemies;
	private Vector2 location = new Vector2(-1,-1);
	
	// Use this for initialization
	void Start () {
		if(xa.currentposi!=-1)
		{
			if(xa.visitgrid[xa.currentposi,xa.currentposj]==1)//(visited)
			{
				Destroy(gameObject);
			}
			else
			{
				DontDestroyOnLoad(gameObject);
				location.x = xa.currentposi;
				location.y = xa.currentposj;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(xa.currentposi==location.x && xa.currentposj==location.y)
		{
			if(!childrenenabled)
			{
				for(int i=0;i<transform.childCount;i++)
				{
					transform.GetChild(i).gameObject.SetActive(true);
				}
				childrenenabled = true;
			}
			checkCleared();
		}
		else
		{
			if(childrenenabled)
			{
				for(int i=0;i<transform.childCount;i++)
				{
					transform.GetChild(i).gameObject.SetActive(false);
				}
				childrenenabled=false;
			}
		}
		
		
	}
	
	void Awake()
	{	
		/*if(xa.currentposi!=-1)
		{
			if(xa.visitgrid[xa.currentposi,xa.currentposj]==1)//(visited)
			{
				Destroy(gameObject);
			}
			else
			{
				DontDestroyOnLoad(gameObject);
				location.x = xa.currentposi;
				location.y = xa.currentposj;
			}
		}*/
	}
	
	
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
			if(xa.currentposi!=-1)
			{
				xa.visitgrid[xa.currentposi,xa.currentposj] = 1;
			}
		}
	}//end checkCleared
}
