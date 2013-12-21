using UnityEngine;
using System.Collections;

public class EnergyBlock : MonoBehaviour {

	public int blockid;

	// Use this for initialization
	void Start () {
	
	}

	Color texturecolor;


	// Update is called once per frame
	void Update () {

		texturecolor = guiTexture.color;
		if(PowerUpManager.numofblocks>=blockid)
		{
			if(PowerUpManager.blocksfilled>=blockid)
			{
				texturecolor.a = .5f;//.5 is the max alpha
			}
			else if(PowerUpManager.blocksfilled+1==blockid)
			{
				texturecolor.a = (PowerUpManager.meterfilled - PowerUpManager.blocksfilled*500)*.001f;
			}
			else
			{
				texturecolor.a = 0f;
			}
		}
		else
		{
			texturecolor.a = 0f;
		}
		guiTexture.color = texturecolor;
	}
}
