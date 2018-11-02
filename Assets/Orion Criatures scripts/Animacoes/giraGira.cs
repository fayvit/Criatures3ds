using UnityEngine;
using System.Collections;

public class giraGira : MonoBehaviour {
	private  float X = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		X += 900*Time.deltaTime*Random. Range(0.75f,1f); 
		
		transform.rotation = Quaternion.Euler (90,0,X);
		if (X > 360) 
			X=0;
	}
}
