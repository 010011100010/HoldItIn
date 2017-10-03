using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class dataReading : MonoBehaviour {
	SerialPort floraSP = new SerialPort ("COM9", 9600);
	// Use this for initialization
	void Start () {
		floraSP.Open ();
		floraSP.ReadTimeout = 1;
	}
	
	// Update is called once per frame
	void Update () {
		try{
			print (floraSP.ReadLine());
		}
		catch(System.Exception){
		}
	}
}
