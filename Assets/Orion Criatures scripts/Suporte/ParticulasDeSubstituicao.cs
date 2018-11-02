using UnityEngine;
using System.Collections;

public class ParticulasDeSubstituicao
{

    public static GameObject InsereParticulaDaLuva(GameObject meuHeroi, bool bracoDireito = true)
    {
        Vector3 instancia = SetarInstancia(meuHeroi, bracoDireito);

        GameObject luz = GameController.g.El.retorna(DoJogo.particulaLuvaDeGuarde);
        luz = (GameObject)MonoBehaviour.Instantiate(luz, instancia, Quaternion.identity);

        luz.name = "luz";
        return luz;
    }


    public static GameObject InsereParticulaDoRaio(Vector3 posCriature, GameObject dono, bool bracoDireito = true)
    {
        
        Vector3 instancia = SetarInstancia(dono, bracoDireito);
        return InsereParticulaDoRaio(instancia,posCriature);
    }

    public static GameObject InsereParticulaDoRaio(Vector3 posSaida, Vector3 posChegada, bool bracoDireito = true)
    {
        GameObject raio = GameController.g.El.retorna(DoJogo.raioDeLuvaDeGuarde);
        
        raio = (GameObject)MonoBehaviour.Instantiate(raio, posSaida, Quaternion.LookRotation(
         posChegada - posSaida
        ));
        raio.name = "raio";
        ParticleSystem P = raio.GetComponent<ParticleSystem>();
        P.startSpeed *= (posChegada - posSaida).magnitude * 2;
        return raio;
    }

    public static void ParticulaSaiDaLuva(Vector3 X, DoJogo oQ = DoJogo.acaoDeCura1)
    {
        GameObject volte = GameController.g.El.retorna(oQ);
        volte = MonoBehaviour.Instantiate(volte, X, Quaternion.identity) as GameObject;


        volte.GetComponent<ParticleSystem>().GetComponent<Renderer>().material
            = GameController.g.El.materiais[0];
        volte.GetComponent<ParticleSystem>().startColor = new Color(1, 0.64f, 0, 1);

        MonoBehaviour.Destroy(volte, 2);
    }

    public static void ReduzVelocidadeDoRaio(GameObject raio)
    {
        ParticleSystem P = raio.GetComponent<ParticleSystem>();
        P.startSpeed = Mathf.Lerp(P.startSpeed, 0, 10 * Time.deltaTime);
        P.startLifetime = Mathf.Lerp(P.startLifetime, 0, 10 * Time.deltaTime);
    }

    static Vector3 SetarInstancia(GameObject meuHeroi, bool direito)
    {
        string nomeEsqueleto = "metarig";

        if (direito)
            return meuHeroi
                .transform.Find(nomeEsqueleto + "/hips/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/palm_02_R")
                    .transform.position;
        else
            return meuHeroi
            .transform.Find(nomeEsqueleto + "/hips/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/palm_02_L")
                .transform.position;

    }
}
