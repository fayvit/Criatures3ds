using UnityEngine;
using System.Collections.Generic;

public class CriaturesPerto
{
    public static Transform procureUmBomAlvo(GameObject esseObjeto, float distancia = 40)
    {
        GameObject[] criatures = GameObject.FindGameObjectsWithTag("Criature");
        GameObject alvo = null;
        Vector3 vendo = Vector3.zero;
        Vector3 c = Vector3.zero;
        List<GameObject> inimigosPerto = new List<GameObject>();
        Transform T = esseObjeto.transform;



        foreach (GameObject criature in criatures)
        {
            if (criature != esseObjeto)
            {
                c = criature.transform.position;
                vendo = c - T.position;



                if (vendo.sqrMagnitude < Mathf.Pow(distancia, 2))
                    inimigosPerto.Add(criature);
            }
        }



        if (inimigosPerto.Count != 0)
        {
            GameObject deMelhorVisao = null;
            GameObject maisPerto = null;

            foreach (GameObject criature in inimigosPerto)
            {
                c = criature.transform.position;
                maisPerto = maisPerto != null
                    ?
                        (c - T.position).sqrMagnitude
                        <
                        (maisPerto.transform.position - T.position).sqrMagnitude
                        ?
                        criature
                        :
                        maisPerto
                        : criature;

                deMelhorVisao = deMelhorVisao == null
                    ?
                        criature
                        :
                        Vector3.Dot((c - T.position).normalized,
                                     T.forward)
                        >
                        Vector3.Dot(
                            (deMelhorVisao.transform.position - T.position)
                            .normalized,
                            T.forward
                            )
                        ?
                        criature
                        :
                        deMelhorVisao;
            }



            if (deMelhorVisao == maisPerto
               &&
               Vector3.Dot(
                (deMelhorVisao.transform.position - T.position).normalized,
                T.forward) > 0)
            {
                alvo = Vector3.Dot((maisPerto.transform.position -
                                    T.position).normalized,
                                   T.forward) > -0.5
                    ? deMelhorVisao : null;
            }
            else
            {
                if ((maisPerto.transform.position - T.position)
                   .sqrMagnitude < 25 &&
                   Vector3.Dot((maisPerto.transform.position -
                             T.position).normalized,
                            T.forward) > -0.5
                   )
                    alvo = maisPerto;
                else
                    alvo = Vector3.Dot((deMelhorVisao.transform.position -
                                        T.position).normalized,
                                       T.forward) > -0.5
                        ? deMelhorVisao : null;
            }
        }

        //procurouAlvo = true;


        ajudaAtaque(alvo, T);

        return alvo != null ? alvo.transform : null;
    }

    static void ajudaAtaque(GameObject alvo, Transform T)
    {

        Vector3 gira = Vector3.zero;
        if (alvo != null)
        {
            gira = alvo.transform.position - T.position;

            gira.y = 0;
            T.rotation = Quaternion.LookRotation(gira);

        }
    }

    public static List<GameObject> ProximosDoPonto(Vector3 pontoDeProximidade, float distancia = 40)
    {
        List<GameObject> retorno = new List<GameObject>();
        GameObject[] Gs = GameObject.FindGameObjectsWithTag("Criature");

        foreach (GameObject G in Gs)
        {
            if (Vector3.Distance(G.transform.position, pontoDeProximidade) < distancia &&
               Vector3.Distance(G.transform.position, pontoDeProximidade) > 0 &&
               G.GetComponent<CreatureManager>().MeuCriatureBase.CaracCriature.meusAtributos.PV.Corrente > 0)
            {
                retorno.Add(G);
            }
        }
        return retorno;
    }

    public static List<GameObject> RemoveEu(List<GameObject> osPerto, GameObject eu)
    {
        bool remove = false;
        
        foreach (GameObject G in osPerto)
        {
            
            if (G == eu)
                remove = true;
        }

        if(remove)
            osPerto.Remove(eu);

        return osPerto;
    }

    public static List<GameObject> ProximosDeMim(GameObject eu,float distancia = 40)
    {
        return RemoveEu(ProximosDoPonto(eu.transform.position), eu);
    }
}
