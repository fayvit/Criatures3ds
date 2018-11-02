using UnityEngine;
using System.Collections;

public class ColisorDeDanoRigido : ColisorDeDanoBase
{
    Vector3 forwardInicial;
    
    void Start()
    {
        forwardInicial = transform.forward;
        quaternionDeImpacto();

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += velocidadeProjetil * forwardInicial * Time.deltaTime;
    }

    void OnCollisionEnter(Collision emQ)
    {
        Debug.Log(emQ.collider.name + " : " + emQ.gameObject.name + " : " + dono + "collision");
        funcaoTrigger(emQ.collider);        
    }
}
