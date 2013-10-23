using UnityEngine;
using System.Collections;

public class Level1RoomClear : MonoBehaviour {
	
	private bool cleared = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectWithTag("Enemy")==null)
		{
			//Debug.Log("ROOM CLEARED");
			xa.visitgrid[xa.currentposi,xa.currentposj] = 1;
		}
	}
}
