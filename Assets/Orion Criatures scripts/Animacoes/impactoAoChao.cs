using UnityEngine;
using System.Collections;

public class impactoAoChao : MonoBehaviour {

	public float tempoDeDestruicao = 1;
	public string aoChao = "impactoAoChao";
	private CharacterController controller;

	// Use this for initialization
	void Start () {
		Destroy(this,tempoDeDestruicao);
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {


		if (controller.collisionFlags == CollisionFlags.Below){

			GameObject G = GameController.g.El.retorna(aoChao);
			G = Instantiate(G,transform.position+transform.forward,Quaternion.identity)as GameObject;
			Destroy(G,1);
			Destroy(this);
		}



	}
}
