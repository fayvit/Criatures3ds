using UnityEngine;
using System.Collections;

public class MbProjetilEletrico : ColisorDeDanoBase
{
    public Transform KXY;
    public Transform criatureAlvo;

    public Vector3 forcaInicial = Vector3.zero;
    private Vector3 forcaDecorrente;
    public float tempoMax = 10;    
    private float tempoDecorrido = 0f;

    private Rigidbody R;

    // Use this for initialization
    void Start()
    {
        R = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido < 0.05f)
        {
            R.AddForce(esseGolpe.VelocidadeDeGolpe*10* forcaInicial);
        }
        else if (tempoDecorrido > 0.05 && tempoDecorrido < 0.1)
        {
            bool vai = true;
            if (criatureAlvo != null)
                if (Vector3.Distance(transform.position, criatureAlvo.position) < 1)
                    vai = false;

            if(vai)
            R.AddForce(-R.velocity);
        }
        else if (tempoDecorrido < tempoMax)
        {
            if (criatureAlvo != null)
            {
                if (Vector3.Distance(criatureAlvo.position, transform.position) < 3)
                    forcaDecorrente = (criatureAlvo.position - transform.position).normalized;
                else
                    forcaDecorrente = (criatureAlvo.position - transform.position + 5 * Vector3.down).normalized;
            }
            else
            {
                forcaDecorrente = (dono.transform.forward + Vector3.down).normalized;
            }

            transform.rotation = Quaternion.LookRotation(forcaDecorrente);
            R.AddForce(forcaDecorrente * esseGolpe.VelocidadeDeGolpe);
        }
        else
        {
            destroiIsso();
        }

       // transform.position += velocidadeProjetil * transform.forward * Time.deltaTime;
    }

    void destroiIsso()
    {
        if (KXY)
        {
            if (KXY.GetComponent<LightningBolt>())
                Destroy(KXY.GetComponent<LightningBolt>());

            Destroy(KXY.gameObject);
        }

        Destroy(transform.parent.gameObject);
    }

    void OnCollisionEnter(Collision emQ)
    {
        //print(emQ.gameObject+" : "+dono+" o bool"+(emQ.gameObject!=dono));
        if (emQ.gameObject != dono
           &&
           (emQ.gameObject.tag == "Criature" || criatureAlvo == null)
           )
        {

            facaImpacto(emQ.gameObject,false);



            destroiIsso();

            /*
            if (emQ.gameObject.tag == "Criature")
                if (dono.GetComponent<acaoDeGolpe>())
                    dono.GetComponent<acaoDeGolpe>().aplicaFimEletrico();
                    */

        }
    }
}
