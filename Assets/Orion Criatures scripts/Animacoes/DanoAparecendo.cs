using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DanoAparecendo : MonoBehaviour {

	public int dano = 0;
    public Transform atacado;

	//private Text oTexto;
	private Vector3 posInicial = Vector3.zero;
	private bool aumenta = true;
	private Transform cam;
	// Use this for initialization
	void Start () {
		transform.localScale = Vector3.zero;
        transform.SetParent(atacado);
		cam = Camera.main.transform;
		GetComponentInChildren<Text>().text = dano.ToString();
	}
	
	// Update is called once per frame
	void Update () {
//		print(cam.position);
		transform.LookAt(cam);

		if(aumenta){
		transform.localScale =  
			Vector3.Lerp(transform.localScale,new Vector3(0.005f,0.005f,0.005f),2*Time.deltaTime);


		transform.localPosition = 
			Vector3.Lerp(transform.localPosition,2*Vector3.up,2*Time.deltaTime);

			if((transform.localScale-new Vector3(0.005f,0.005f,0.005f)).magnitude<0.001f)
				aumenta = false;
		}
		else
		{
			transform.localScale =  
				Vector3.Lerp(transform.localScale,Vector3.zero,4*Time.deltaTime);

			transform.localPosition = 
				Vector3.Lerp(transform.localPosition,posInicial+2*Vector3.up,2*Time.deltaTime);

			if(transform.localScale.magnitude<0.0001f)
				Destroy(gameObject);
		}

	}
}
