using UnityEngine;
using System.Collections;

public class KillCount : MonoBehaviour {
	
	public TextMesh killValueText;
	public TextMesh bulletValueText;
	public TextMesh timeValueText;
	
	private int killValue = 0;
	private int bulletValue = 0;
	private float timeValue = 0;

	// Use this for initialization
	void Start () {
		
		killValueText.text = killValue.ToString("D7");
		bulletValueText.text = bulletValue.ToString("D7");
		//timeValueText.text = timeValue.ToString("D7");
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!xa.playerdead)
		{
			timeIncrease();
		}
	
	}
	
	public void killIncrease()
	{
		killValue += 1;
		killValueText.text = killValue.ToString ("D7");
	}
	
	public void bulletIncrease()
	{
		bulletValue += 1;
		bulletValueText.text = bulletValue.ToString("D7");
	}
	
	public int getKillValue()
	{
		return killValue;
	}
	
	private void timeIncrease()
	{
		timeValue += Time.deltaTime;
		timeValueText.text = timeValue.ToString();
		
	}
}
