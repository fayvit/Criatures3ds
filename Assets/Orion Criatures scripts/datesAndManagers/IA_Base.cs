using UnityEngine;
using System.Collections;

[System.Serializable]
public class IA_Base
{
    [Header("Variaveis de edição no inspector")]
    [SerializeField]protected float distanciaDeAgressao = 40;
    [SerializeField]protected float distanciaDoGuardiao= 400;

   // [SerializeField]protected EstadoDeIA estado = EstadoDeIA.naoIniciado;
    //[SerializeField]protected ComportamentoDeIA comportamento = ComportamentoDeIA.agressivo;

    [Header("Variaveis de edição interna")]
    [SerializeField]protected CreatureManager criatureDoJogador;
    //[SerializeField]protected Vector3 posInicial = Vector3.zero;

    [SerializeField]protected CreatureManager meuCriature;
    [SerializeField]protected SigaOLider siga;

    protected bool procurando = false;

    public void Start(CreatureManager meuCriature) {
        this.meuCriature = meuCriature;
        ProcuraCriatureDoJogador();
        if (siga==null)
            siga = new SigaOLider();
       // posInicial = meuCriature.transform.position;
        siga.Start(meuCriature);
    }
    public void Update() {
        if (criatureDoJogador != null)
        {
            if (meuCriature.name == "CriatureAtivo")
            {
                siga.Update(criatureDoJogador.TDono.position);
            }
            else
            {
                AplicaIaDeAtaque();
                // GerenciadorDeEstado();
                //GerenciadorDeAcoes();
            }
        }
        else
        {
            if (!procurando)
                ProcuraCriatureDoJogador();
        }
    }

    protected virtual void AplicaIaDeAtaque()
    {
        //sobrecarregado pela IA agressiva
    }

    IEnumerator ProcuraCriatureDoJogadorCo()
    {
        yield return new WaitForSeconds(0.5f);
        ProcuraCriatureDoJogador();
    }

    public void ProcuraCriatureDoJogador()
    {
        
        GameObject G = GameObject.Find("CriatureAtivo");
        
        if (G)
            criatureDoJogador = G.GetComponent<CreatureManager>();

        if (criatureDoJogador == null)
        {
            Debug.Log("Reprocurando");
            GameController.g.StartCoroutine(ProcuraCriatureDoJogadorCo());
            procurando = true;
        }
        else
            procurando = false;

    }

    public void SuspendeNav()
    {
        siga.PareAgora();
    }

    public bool NavAtiva()
    {
        return siga.Nav.isActiveAndEnabled;
    }

}

