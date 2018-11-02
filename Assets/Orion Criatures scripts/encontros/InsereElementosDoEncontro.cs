using UnityEngine;
using System.Collections;

public class InsereElementosDoEncontro
{
    
    public static void encontroPadrao(CharacterManager manager)
    {
        animacaoDeEncontro(manager.transform.position);
        adicionaCilindroEncontro(manager.transform.position);
        alternanciaParaCriature(manager);
        impedeMovimentoDoCriature(manager.CriatureAtivo);
        alteraPosDoCriature(manager);    
        colocaOHeroiNaPOsicaoDeEncontro(manager);        

    }

    public static void EncontroDeTreinador(CharacterManager manager, Transform doTreinador)
    {
        /*
        animacaoDeEncontro(manager.transform.position);
        adicionaCilindroEncontro(manager.transform.position);
        alternanciaParaCriature(manager);
        impedeMovimentoDoCriature(manager.CriatureAtivo);
        alteraPosDoCriature(manager);
        colocaOHeroiNaPOsicaoDeEncontro(manager);*/
        encontroPadrao(manager);
        doTreinador.position = novaPos(doTreinador.position, 2, doTreinador.position + 40 * manager.transform.forward);
    }

    protected static void animacaoDeEncontro(Vector3 posHeroi)
    {
        //heroi.emLuta = true;
        GameObject anima = GameController.g.El.retorna("encontro");

        MonoBehaviour.Destroy(MonoBehaviour.Instantiate(anima, posHeroi, Quaternion.identity),2);
    }

    protected static void adicionaCilindroEncontro(Vector3 posHeroi)
    {
        GameObject cilindro = GameController.g.El.retorna("cilindroEncontro");
        Object cilindro2 = MonoBehaviour.Instantiate(cilindro, posHeroi, Quaternion.identity);
        cilindro2.name = "cilindroEncontro";
    }

    protected static void alteraPosDoCriature(CharacterManager manager)
    {
        GameObject  X = GameObject.Find("CriatureAtivo");

        X.transform.position = manager.transform.position;//new melhoraPos().novaPos(posHeroi,X.transform.lossyScale.y);
        X.transform.rotation = manager.transform.rotation;
    }


    protected static void alternanciaParaCriature(CharacterManager manager)
    {
        manager.AoCriature();
    }

    protected static void impedeMovimentoDoCriature(CreatureManager C)
    {
        
        C.Estado = CreatureManager.CreatureState.parado;
    }

    protected static void colocaOHeroiNaPOsicaoDeEncontro(CharacterManager manager)
    {
        manager.transform.position = novaPos(manager.transform.position,manager.Mov.Controle.height,
            manager.transform.position - 40f * manager.transform.forward
            );//40f * tHeroi.forward;


        //manager.gameObject.AddComponent<gravidadeGambiarra>();
    }

    static Vector3 novaPos(Vector3 posInicial,float altura,Vector3 posAlvo)
    {
        RaycastHit hit = new RaycastHit();
        string nomeDoTerreno = "Terrain";
        if (Physics.Raycast(posInicial+Vector3.up , Vector3.down, out hit))
        {
            nomeDoTerreno = hit.collider.name;
        }
        Vector3 retorno = new melhoraPos().posEmparedado(posAlvo, posInicial);
        
        retorno = emBuscaDeUmaBoaPosicao(retorno, altura, nomeDoTerreno);

        return retorno;
    }

    public static Vector3 emBuscaDeUmaBoaPosicao(Vector3 pontoInicial, float PQP, string terreno = "Terrain")
    {
        Vector3 retorno = pontoInicial;
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(pontoInicial + PQP * Vector3.up, -Vector3.up, out hit))
        {
            Debug.Log(hit.transform.name + " : " + hit.transform.tag);
            if (hit.transform.name == terreno || hit.transform.tag=="cenario")
            {

                Debug.Log("raio abaixo");
                retorno = hit.point + 2f * Vector3.up;
                
            }
        }
        /*
        else if (Physics.Raycast(pontoInicial - PQP * Vector3.up, Vector3.up, out hit))
            if (hit.transform.name == terreno || hit.transform.tag == "cenario")
            {
                Debug.Log("raio acima");
                retorno = hit.point + 2f * Vector3.up;
                
            }*/

        if(hit.transform!=null)
            Debug.Log(hit.transform.name + " : " + hit.transform.tag);

        return retorno;
    }
}
