using UnityEngine;
using System.Collections;

public class poeiraAoVento : MonoBehaviour {
	public float tempoDeRepeticao = 0.25f;
	public string particula = "poeiraAoVento";

	// Use this for initialization
	void Start () {
		
		Invoke("poeira",0.5f);
	}

	void poeira()
	{
        
        GameObject G = GameController.g.El.retorna(particula);
		Vector3 pos = Vector3.zero;
		RaycastHit ray = new RaycastHit();
		if(Physics.Raycast(transform.position,Vector3.down,out ray))
		{
			pos = ray.point;
		}else if(Physics.Raycast(transform.position,Vector3.up,out ray))
		{
			pos = ray.point;
		}
		G = Instantiate(G,pos,Quaternion.identity) as GameObject;
		Destroy(G,1.75f);
		Invoke("poeira",tempoDeRepeticao);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
