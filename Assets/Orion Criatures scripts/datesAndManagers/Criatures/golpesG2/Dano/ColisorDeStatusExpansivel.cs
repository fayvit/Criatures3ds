using UnityEngine;
using System.Collections;

public class ColisorDeStatusExpansivel : ColisorDeDanoBase
{
    private float Yinicial;
    private float alturaInicial;

    void Start()
    {
        Yinicial = transform.position.y;
        quaternionDeImpacto();
        alturaInicial = GetComponent<BoxCollider>().size.y;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += velocidadeProjetil * transform.forward * Time.deltaTime;
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(10, 10, 1), 3 * Time.deltaTime);
        transform.position = new Vector3(transform.position.x,
                                         Yinicial + (transform.localScale.y - 1) * alturaInicial / 4,
                                         transform.position.z);
    }

    void OnTriggerEnter(Collider emQ)
    {
        funcaoTrigger(emQ);
    }

}
