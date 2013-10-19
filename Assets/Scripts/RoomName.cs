using UnityEngine;
using System.Collections;

public class RoomName : MonoBehaviour {
	
	public GameObject roomname;
	public string title;

	// Use this for initialization
	void Start () {
		
		GameObject newroomname = (GameObject)Instantiate(roomname);//, bulletpos, Quaternion.identity); //new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);//, GetComponent<OTSprite>().transform.position, Quaternion.identity);//thisTransform.transform.position, thisTransform.transform.rotation); //transform.position, transform.rotation);//create a new bullet object
		newroomname.GetComponent<TextMesh>().text = title;
		
		Destroy (newroomname,1.5f);//destroys the newly created object in 3 seconds
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
