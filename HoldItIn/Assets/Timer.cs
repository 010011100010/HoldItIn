using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer :MonoBehaviour {
	public Text timerText;
	//public float myCoolTimer = 99
	float timer = 0f;
	int seconds;



	// Use this for initialization
	void Start () {
		//timerText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		/*myCoolTimer-= Time.deltaTime;*/

		timer += Time.deltaTime;
		seconds = Mathf.RoundToInt (timer);
		timerText.text = "Rectal Pressure Regulated: " + seconds;

	}
}
