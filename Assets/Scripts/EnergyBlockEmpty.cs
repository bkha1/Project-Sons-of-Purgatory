using UnityEngine;
using System.Collections;

public class EnergyBlockEmpty : MonoBehaviour {

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
			texturecolor.a = .5f;//.5 is the max alpha
		}
		else
		{
			texturecolor.a = 0f;
		}
		guiTexture.color = texturecolor;
	}
}
