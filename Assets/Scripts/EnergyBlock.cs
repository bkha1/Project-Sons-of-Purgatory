using UnityEngine;
using System.Collections;

public class EnergyBlock : MonoBehaviour {

	public int blockid;

	// Use this for initialization
	void Start () {
	
	}

	Color texturecolor;
	int blocklevel;

	// Update is called once per frame
	void Update () {

		blocklevel = xa.experiencepoints/500;

		texturecolor = guiTexture.color;
		if(blocklevel>=blockid)
		{
			texturecolor.a = .5f;//.5 is the max alpha
		}
		else if(blocklevel+1==blockid)
		{
			texturecolor.a = (xa.experiencepoints - blocklevel*500)*.001f;
		}
		else
		{
			texturecolor.a = 0f;
		}
		guiTexture.color = texturecolor;
	}
}
