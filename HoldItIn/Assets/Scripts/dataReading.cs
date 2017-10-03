using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class dataReading : MonoBehaviour {
	SerialPort floraSP = new SerialPort ("COM9", 9600);
	//SerialPort floraSP2 = new SerialPort ("COM9", 4800);

	public Color blue;
	public Color green;
	public Color red;

	public Text switchText;
	public Text bendText;
	private Text rectalPressureText;

	private Camera cam;
	private float bendSqueezePressure;
	private float rectalPressureValue = 1000;
	private float bendSqueezeValue;
	public float leakSpeed = 2;
	public float leakThreshold = 0;
	public float overPressureThreshold = 2000;
	public float colorLerpDuration = 3f;


	// Use this for initialization
	void Start () {

		floraSP.Open ();
		floraSP.ReadTimeout = 1;

		//floraSP2.Open ();
		//floraSP2.ReadTimeout = 2;

		//switchText = GameObject.Find ("PressureSwitch").GetComponent<Text> ();
		//bendText = GameObject.Find ("PressureBend").GetComponent<Text> ();
		//switchText.text = "Pressure Switch Value: ";
		//Debug.Log (switchText.text);
		cam = GameObject.Find("Main Camera").GetComponent<Camera> ();
		rectalPressureText = GameObject.Find ("RectalPressure").GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () {
		//print (Mathf.Round (Time.deltaTime));
		bendSqueezePressure = float.Parse(floraSP.ReadLine ());
		bendSqueezeValue = 900f - bendSqueezePressure;
		rectalPressureValue = rectalPressureValue - Time.deltaTime*leakSpeed + bendSqueezeValue;

		try{
			//print (floraSP.ReadLine());
		}
		catch(System.Exception){
		}

		switchText.text = "Pressure Switch Value: " + bendSqueezeValue;
		rectalPressureText.text = "Rectal Pressure: " + (Mathf.Round(rectalPressureValue));
		//bendText.text = "Pressure Bend Value: "; //+ floraSP2.ReadLine ();
		float t = Mathf.PingPong(Time.time, colorLerpDuration)/colorLerpDuration;
		if (rectalPressureValue < leakThreshold) {
			cam.backgroundColor = Color.Lerp (cam.backgroundColor, green, t);
		} else if (rectalPressureValue > overPressureThreshold) {
			cam.backgroundColor = Color.Lerp (cam.backgroundColor, red, t);
		} else {
			cam.backgroundColor = Color.Lerp (cam.backgroundColor, blue, t);
		}
	}
}
