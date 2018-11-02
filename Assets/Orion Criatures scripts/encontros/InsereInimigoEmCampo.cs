using UnityEngine;
using System.Collections;

public class InsereInimigoEmCampo
{
    public static CreatureManager RetornaInimigoEmCampo(CriatureBase C)
    {
        CreatureManager retorno = ColocaCriatureEmCampo(C.NomeID.ToString());
        retorno.MeuCriatureBase = new CriatureBase(C.NomeID,C.CaracCriature.mNivel.Nivel);
        
        InsereIaAgressiva(retorno);

        return retorno;

    }

    public static CreatureManager RetornaInimigoEmCampo(Encontravel encontrado,CharacterManager manager)
    {        
       
        if (manager.CriatureAtivo && encontrado.nivelMax > 0)
        {

            CreatureManager retorno = ColocaCriatureEmCampo(encontrado.nome.ToString());
            
            int nivel = Random.Range(encontrado.nivelMin, encontrado.nivelMax);
            retorno.MeuCriatureBase
                = new CriatureBase(encontrado.nome, nivel);

            InsereIaAgressiva(retorno);            

            return retorno;
        }
        else
            return null;
    }

    static void InsereIaAgressiva(CreatureManager retorno)
    {
        retorno.IA = new IA_Agressiva() { PodeAtualizar = false };
        retorno.IA.Start(retorno);
        retorno.Estado = CreatureManager.CreatureState.selvagem;
    }

    static CreatureManager ColocaCriatureEmCampo(string nome)
    {
        GameObject M = GameController.g.El.criature(nome);
        CharacterManager manager = GameController.g.Manager;
        Transform doCriatureAtivo = manager.transform;// o inimigo é colocado em campo antes do heroi trocar de posição com o criature
        Vector3 instancia = doCriatureAtivo.position + 10 * doCriatureAtivo.forward;
        Debug.Log(M);
        /*
        RaycastHit hit = new RaycastHit ();
            if(Physics.Linecast(posHeroi,posHeroi+10*tHeroi.forward,out hit))
        {
            instancia = hit.point+Vector3.up;
        }
        */
        melhoraPos melhoraPF = new melhoraPos();

        instancia = melhoraPF.posEmparedado(instancia, doCriatureAtivo.position);

        instancia = InsereElementosDoEncontro.emBuscaDeUmaBoaPosicao(instancia, M.transform.lossyScale.y);//melhoraPF.novaPos(instancia, M.transform.lossyScale.y);

        GameObject InimigoX = MonoBehaviour.Instantiate(M, instancia, Quaternion.identity) as GameObject;


        return InimigoX.GetComponent<CreatureManager>();
    }
}
