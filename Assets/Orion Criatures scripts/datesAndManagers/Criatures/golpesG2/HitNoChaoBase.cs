using UnityEngine;
using System.Collections;

[System.Serializable]
public class HitNoChaoBase : GolpeBase
{
    private bool addView = false;
    private float tempoDecorrido = 0;
    protected NoImpacto noImpacto = NoImpacto.impactoComum;
    [System.NonSerialized]private CharacterController controle;

    public HitNoChaoBase(ContainerDeCaracteristicasDeGolpe C) : base(C){ }

    public override void IniciaGolpe(GameObject G)
    {
        addView = false;
        tempoDecorrido = 0;
        DirDeREpulsao = G.transform.forward;

        ApareceDesaparece(false, G);

        MonoBehaviour.Destroy(
            MonoBehaviour.Instantiate(
            GameController.g.El.retorna(Nome.ToString()),
            G.transform.position,
            Quaternion.identity),
            10);
        
    }

    public override void UpdateGolpe(GameObject G)
    {
        tempoDecorrido += Time.deltaTime;
        ApareceComHitNoChao(G);
    }

    
    void ApareceDesaparece(bool aparecer, GameObject G)
    {
        SkinnedMeshRenderer[] skinned = G.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer sk in skinned)
        {
            sk.enabled = aparecer;
        }

        if (!controle)
            controle = G.GetComponent<CharacterController>();

        controle.enabled = aparecer;
    }    

    void ApareceComHitNoChao(GameObject gameObject)
    {
        if (!addView)
        {
            addView = true;
            CreatureManager aAlvo = (gameObject.name == "CriatureAtivo")
                ?
                GameController.g.InimigoAtivo
                :
                GameController.g.Manager.CriatureAtivo;

            Transform alvo = (aAlvo != null) ? aAlvo.transform : null;

            Vector3 volta = gameObject.transform.position;

            if (alvo != null)
            {
                volta = alvo.position;
                DirDeREpulsao = Vector3.ProjectOnPlane(alvo.position - gameObject.transform.position, Vector3.up).normalized;
                
                bool b = aAlvo.IA.NavAtiva() || aAlvo.Mov.NoChao(aAlvo.MeuCriatureBase.CaracCriature.distanciaFundamentadora); //alvo.GetComponent<Animator>().GetBool("noChao");
                bool c = alvo.GetComponent<CharacterController>().enabled;

                MonoBehaviour.Destroy(
                    MonoBehaviour.Instantiate(
                    GameController.g.El.retorna(noImpacto.ToString()),
                    alvo.position,
                    Quaternion.identity),
                    10);

                Debug.Log("hitNoChao: "+b + " : " + c);
                if (b && c)
                    Dano.VerificaDano(aAlvo.gameObject, gameObject, this);

            }

            MonoBehaviour.Destroy(
                MonoBehaviour.Instantiate(
                GameController.g.El.retorna(Nome.ToString()),
                volta,
                Quaternion.identity),
                10);

            gameObject.transform.position = new melhoraPos().novaPos(volta);
            ApareceDesaparece(true, gameObject);
        }
    }
}
