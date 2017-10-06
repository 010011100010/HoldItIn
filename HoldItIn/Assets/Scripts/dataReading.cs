using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class dataReading : MonoBehaviour {
	SerialPort floraSP = new SerialPort ("COM9", 9600);
	//SerialPort floraSP2 = new SerialPort ("COM9", 4800);

	public Color white;
	public Color green;
	public Color red;

	public GameObject mrLeakNormalExpression;
	public GameObject mrLeakAbnormalExpression;
	public GameObject mrLeakFace;
	public GameObject needle;
	public Text switchText;
	public Text bendText;
	private Text rectalPressureText;
	private SpriteRenderer mrLeakFaceSR;

	private Camera cam;
	private float bendSqueezePressure;
	private float rectalPressureValue = 1000;
	private float bendSqueezeValue;
	public float leakSpeed = 2;
	public float leakThreshold = 0;
	public float overPressureThreshold = 2000;
	public float colorLerpDuration = 3f;
	public float dangerZone = 1000;
	public float needleTuringSpeed = 1f;


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
		//cam = GameObject.Find("Main Camera").GetComponent<Camera> ();
		rectalPressureText = GameObject.Find ("RectalPressure").GetComponent<Text> ();
		mrLeakFaceSR = mrLeakFace.GetComponent<SpriteRenderer> ();
		mrLeakAbnormalExpression.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		//print (Mathf.Round (Time.deltaTime));
		bendSqueezePressure = float.Parse(floraSP.ReadLine ());
		bendSqueezeValue = 1500f - bendSqueezePressure;
		rectalPressureValue = rectalPressureValue - Time.deltaTime*leakSpeed + bendSqueezeValue;
		print (bendSqueezeValue);

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
			mrLeakFaceSR.color = Color.Lerp (mrLeakFaceSR.color, green, t);
			mrLeakAbnormalExpression.SetActive (true);
		} else if (rectalPressureValue > overPressureThreshold) {
			mrLeakFaceSR.color = Color.Lerp (mrLeakFaceSR.color, red, t);
			mrLeakAbnormalExpression.SetActive (true);
		} else {
			mrLeakFaceSR.color = Color.Lerp (mrLeakFaceSR.color, white, t);
			mrLeakNormalExpression.SetActive (true);
			mrLeakAbnormalExpression.SetActive (false);
		}

		if (rectalPressureValue < (leakThreshold-dangerZone)) {
			LoadLeakScene ();
		}else if (rectalPressureValue > (overPressureThreshold+dangerZone))
		{
			LoadPoopScene ();
		}

		if (rectalPressureValue<700f) {
			//needle.transform.rotation = Quaternion.Euler (0, 0, needle.transform.rotation.z + needleTuringSpeed);
			needle.transform.Rotate(0,0,Time.deltaTime*needleTuringSpeed);
		} else if (rectalPressureValue>700f) {
			//needle.transform.rotation = Quaternion.Euler (0, 0, needle.transform.rotation.z - needleTuringSpeed);
			needle.transform.Rotate(0,0,-Time.deltaTime*needleTuringSpeed);
		}
	}

	void LoadLeakScene () {
		SceneManager.LoadScene ("GameOver1", LoadSceneMode.Single);
	}

	void LoadPoopScene () {
		SceneManager.LoadScene ("GameOver2", LoadSceneMode.Single);
	}
}
