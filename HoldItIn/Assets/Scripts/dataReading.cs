using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class dataReading : MonoBehaviour {
	SerialPort floraSP = new SerialPort ("COM9", 9600);
	SerialPort floraSP2 = new SerialPort ("COM9", 4800);

	private Text switchText;
	private Text bendText;
	// Use this for initialization
	void Start () {
		floraSP.Open ();
		floraSP.ReadTimeout = 2;

		floraSP2.Open ();
		floraSP2.ReadTimeout = 2;

		switchText = GameObject.Find ("PressureSwitch").GetComponent<Text> ();
		bendText = GameObject.Find ("PressureBend").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		try{
			print (floraSP.ReadLine());
		}
		catch(System.Exception){
		}

		switchText.text = "Pressure Switch Value: " + floraSP.ReadLine ();
		bendText.text = "Pressure Bend Value: " + floraSP2.ReadLine ();
	}
}
