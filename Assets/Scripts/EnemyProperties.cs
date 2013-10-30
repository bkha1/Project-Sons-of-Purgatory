using UnityEngine;
using System.Collections;

public class EnemyProperties : MonoBehaviour {
	
	public int health = 100;
	public bool dead = false;
	public bool frenzy = false;
	public bool hurt = false;
	//public int id = 0;
	
	public GameObject triscuit;
	public int triscuitsdropped = 5;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		healthCheck();
	
	}
	
	void healthCheck()
	{
		if(health<0 && !dead)
		{
			int a = (int)triscuitsdropped/4;
			if(Random.Range(0,2)==0)
			{
				triscuitsdropped+=a;
			}
			else
			{
				triscuitsdropped-=a;
			}
			
			for(int i=0;i<triscuitsdropped;i++)
			{
				GameObject clonetriscuit = (GameObject) Instantiate(triscuit);
				Vector2 p = new Vector2(gameObject.transform.localPosition.x + Random.Range(-1f,1f),gameObject.transform.localPosition.y + Random.Range(-1f,1f));
				clonetriscuit.GetComponent<OTSprite>().position = p;
				
				clonetriscuit.transform.parent = transform.parent;
				
				if(Random.Range(0,10)==0)
				{
					//Vector3 tempscale = new Vector3(5,5,0);
					//clonetriscuit.transform.localScale.Set(clonetriscuit.transform.localScale.x*5,clonetriscuit.transform.localScale.y*5,1);
					clonetriscuit.GetComponent<OTSprite>().size *= 1.5f;
					clonetriscuit.GetComponent<Triscuits>().xp*=5;
				}
				
			}
			dead = true;
			//xa.sc.killIncrease();
		}
	}//end checkHealth
	
	public void hitEnemy(int damage)
	{
		if(!dead)
		{
			health -= damage;
			hurt = true;
		}
	}
}
