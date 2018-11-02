using UnityEngine;
using System.Collections;

public class ColisorDeDano : ColisorDeDanoBase
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

    void OnTriggerEnter(Collider emQ)
    {
        //Debug.Log(emQ.name + " : " + emQ.gameObject.name + " : " + dono+"trigger");
        
        funcaoTrigger(emQ);
    }
}
