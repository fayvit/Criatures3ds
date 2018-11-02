using UnityEngine;
using System.Collections;

public class escalaParaSabre : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(transform.localScale.sqrMagnitude<2.9f)
			transform.localScale = Vector3.Lerp(
				transform.localScale,
				new Vector3(1,1,1),
				Time.deltaTime*10
				);
		else
			transform.localScale = new Vector3(0.1f,0.1f,0.1f);
	}
}
