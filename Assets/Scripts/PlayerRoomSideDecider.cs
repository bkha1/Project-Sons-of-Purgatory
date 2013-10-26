using UnityEngine;
using System.Collections;

public class PlayerRoomSideDecider : MonoBehaviour {
	
	public GameObject player;

	// Use this for initialization
	void Start () {
		/*
		Vector2 playerpos;// = new Vector2(GetComponent<OTSprite>().position.x,GetComponent<OTSprite>().position.y);
		GameObject playerspawn = (GameObject)Instantiate(player);
		
		if(xa.playerstartside==1)//start north
		{
			playerpos = new Vector2(-0.5f,5.7f);		
		}
		else if(xa.playerstartside==2)//start east
		{
			playerpos = new Vector2(8.5f,.7f);
		}
		else if(xa.playerstartside==3)//start south
		{
			playerpos = new Vector2(0.5f,-5.3f);
		}
		else if(xa.playerstartside==4)//start west
		{
			playerpos = new Vector2(-8.5f,.7f);	
		}
		else
		{
			playerpos = new Vector2(0f,0f);
		}
		playerspawn.GetComponent<OTSprite>().position=playerpos;
		Debug.Log(playerspawn.GetComponent<OTSprite>().position.x + " " + playerspawn.GetComponent<OTSprite>().position.y);
		xa.playerstartside=0;
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void spawnPlayer()
	{
		Vector2 playerpos;
		GameObject playerspawn = (GameObject)Instantiate(player);
		
		if(xa.playerstartside==1)//start north
		{
			playerpos = new Vector2(-0.5f,5.7f);		
		}
		else if(xa.playerstartside==2)//start east
		{
			playerpos = new Vector2(8.5f,.7f);
		}
		else if(xa.playerstartside==3)//start south
		{
			playerpos = new Vector2(0.5f,-5.3f);
		}
		else if(xa.playerstartside==4)//start west
		{
			playerpos = new Vector2(-8.5f,.7f);	
		}
		else
		{
			//playerpos = new Vector2(-0.5f,5.7f);
			playerpos = new Vector2(0,0);
		}
		playerspawn.GetComponent<OTSprite>().position=playerpos;
		//xa.playerstartside=0;
	}
	
	/*public void respawnPlayer()
	{
		//TODO: Trigger temp invincibility for player
		
		Vector2 playerpos;
		GameObject playerspawn = (GameObject)Instantiate(player);
		
		if(xa.playerstartside==1)//start north
		{
			playerpos = new Vector2(-0.5f,5.7f);		
		}
		else if(xa.playerstartside==2)//start east
		{
			playerpos = new Vector2(8.5f,.7f);
		}
		else if(xa.playerstartside==3)//start south
		{
			playerpos = new Vector2(0.5f,-5.3f);
		}
		else if(xa.playerstartside==4)//start west
		{
			playerpos = new Vector2(-8.5f,.7f);	
		}
		else
		{
			playerpos = new Vector2(-0.5f,5.7f);
		}
		playerspawn.GetComponent<OTSprite>().position=playerpos;
		//xa.playerstartside=0;
	}*/
}
