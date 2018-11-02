using UnityEngine;
using System.Collections;

public class tiraPoeFlare : MonoBehaviour {


	public Flare F;

	private Light L;
	private Transform tHeroi;
	// Use this for initialization
	void Start () {
		L = GetComponent<Light>();

        if(GameObject.FindWithTag("Player"))
		    tHeroi = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (tHeroi)
        {
            if (Vector3.Distance(tHeroi.position, transform.position) < 300
               &&
               Mathf.Abs(tHeroi.position.y - transform.position.y) < 15)
                L.enabled = true;
            else
                L.enabled = false;
        }
	}
}
