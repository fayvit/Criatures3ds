using UnityEngine;
using System.Collections;

public class ColisorDeDanoBase : MonoBehaviour
{

    public float velocidadeProjetil = 6f;
    public GameObject dono;
    public string noImpacto;
    public IGolpeBase esseGolpe;

    protected Quaternion Qparticles;

    protected void funcaoTrigger(Collider emQ)
    {
        
        if (emQ.gameObject != dono
           &&
           ((emQ.tag != "cenario"&& emQ.tag != "gatilhoDePuzzle" && emQ.gameObject.layer != 2) /*|| velocidadeProjetil > 0*/)
           &&
           emQ.tag != "desvieCamera")
        {
            facaImpacto(emQ.gameObject);

        }


    }

    protected void quaternionDeImpacto()
    {

        switch (noImpacto)
        {
            case "impactoComum":           
                Qparticles = Quaternion.LookRotation(dono.transform.forward);
            break;
            default:
                GameObject impacto = GameController.g.El.retorna(noImpacto);
                Qparticles = impacto.transform.rotation;
            break;
        }


    }

    protected void facaImpacto(GameObject emQ, bool destroiAqui = true, bool noTransform = false)
    {
        /*
        
        if (emQ.gameObject.tag == "eventoComGolpe" && !GameController.g.estaEmLuta)
        {
            emQ.GetComponent<EventoComGolpe>().DisparaEvento(esseGolpe.Nome);
        }*/

        GameObject impacto = GameController.g.El.retorna(noImpacto);
        

        if (!noTransform)
            impacto = (GameObject)Instantiate(impacto, transform.position, Qparticles);

       // if (emQ.tag == "Criature")
        //{
           // Atributos A = emQ.GetComponent<CreatureManager>().MeuCriatureBase.CaracCriature.meusAtributos;
            //if (A!=null)
              //  if (A.PV.Corrente > 0)

                    Dano.VerificaDano(emQ, dono, esseGolpe);

            if (noTransform)
                impacto = (GameObject)Instantiate(impacto, emQ.transform.position, Qparticles);

                /*
                if (colocaImpactos)
                    aG.impactos++;
                    */
                    
       // }

        if (impacto)
            Destroy(impacto, 1.5f);
        if (destroiAqui)
            Destroy(gameObject);
    }
}
