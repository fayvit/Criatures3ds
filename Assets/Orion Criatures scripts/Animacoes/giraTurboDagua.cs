using UnityEngine;
using System.Collections;

public class giraTurboDagua : MonoBehaviour {
	float tempo = 0;
	bool sinal;
	bool naoAfastou = true;
	// Use this for initialization
	void Start () {
		sinal = (transform.localPosition.x>0)?true:false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.parent.position,transform.parent.forward,400*Time.deltaTime);
		tempo+=Time.deltaTime;

		if(tempo>0.1f&&naoAfastou)
		{
			naoAfastou = false;
			float novoX  = sinal ? 0.5f:-0.5f;
			float novoY = 0;

			transform.localPosition = new Vector3(novoX,novoY,transform.localPosition.z);
		}
	}
}
