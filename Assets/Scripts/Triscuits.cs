using UnityEngine;
using System.Collections;

public class Triscuits : MonoBehaviour {
	
	public int xp = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<OTSprite>().spriteContainer = OT.ContainerByName("triscuit");
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			//Debug.Log("XP!");
			xa.experiencepoints += xp;
			Destroy (this.gameObject);
		}
	}
}
