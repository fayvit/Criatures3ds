using UnityEngine;
using System.Collections;

public class InicializadorDoJogo
{
    public static GameObject InstanciaCriature(Transform transform, CriatureBase criature)
    {

        GameObject CA = GameController.g.El.retorna(criature.NomeID.ToString(),"criature");
        CA = MonoBehaviour.Instantiate(CA, transform.position - 3 * transform.forward, Quaternion.identity)
            as GameObject;
        
        UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(CA,
            UnityEngine.SceneManagement.SceneManager.GetSceneByName("comunsDeFase")
            );
        return CA;
    }

    public static void InsereCriatureEmJogo(GameObject G, CharacterManager este, bool aplicarComandos = false)
    {
        G.name = "CriatureAtivo";
        CreatureManager C = G.GetComponent<CreatureManager>();
        C.TDono = este.transform;
        C.MeuCriatureBase = este.Dados.CriaturesAtivos[0];

        RecolocadorDeStatus.VerificaInsereParticulaDeStatus(C);
    }

    /*
    public static void EditaGerenciadorDeCriature(GameObject G, CharacterManager gerente)
    {
        CreatureManager GC = G.GetComponent<CreatureManager>();
        /*
        GC.estado = EstadoCriature.seguindo;
        GC.MeuCriatureBase = gerente.Dados.criaturesAtivos[0];
        GC.gerenteCri = gerente;
        
    } */

    /*
public static void AdicionaSigaOLider(GameObject G, Transform T)
{
     sigaOLider S = G.AddComponent<sigaOLider>();
     S.lider = T;
     S.caminhos = G.AddComponent<UnityEngine.AI.NavMeshAgent>();
     S.caminhos.stoppingDistance = S.caminhos.radius < 1 ? 7 : 5 * S.caminhos.radius;
     S.caminhos.baseOffset = -0.12f;
     S.caminhos.speed = G.GetComponent<GerenciadorDeCriature>().MeuCriatureBase.Mov.velocidadeCorrendo;

 }*/
}
