using UnityEngine;
using System.Collections;

public class AplicadorDeGolpe :MonoBehaviour
{
    public IGolpeBase esseGolpe;
    public float tempoDecorrido = 0;

    private CreatureManager gerente;
    private bool retornou = false;
    
    void Start()
    {
        gerente = GetComponent<CreatureManager>();
        ParaliseNoTempo();
        esseGolpe.IniciaGolpe(gameObject);        
        tempoDecorrido -= GolpePersonagem.RetornaGolpePersonagem(gameObject,esseGolpe.Nome).TempoDeInstancia;
    }

    void Update()
    {
        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido>esseGolpe.TempoDeMoveMin && gerente.Estado == CreatureManager.CreatureState.aplicandoGolpe)
        {
            esseGolpe.UpdateGolpe(gameObject);
        }
        else if (gerente.Estado == CreatureManager.CreatureState.emDano)
        {
            FinalizaGolpe();
        }

        if (tempoDecorrido > esseGolpe.TempoDeMoveMax && !retornou)
        {
            if (esseGolpe.Caracteristica == caracGolpe.projetil)
            {
                LiberaDoAtacando();
                Destroy(this, 2);
            }
            else if (tempoDecorrido > esseGolpe.TempoDeDestroy)
            {
                FinalizaGolpe();
            }
        }
    }

    private void FinalizaGolpe()
    {
        esseGolpe.FinalizaEspecificoDoGolpe();
        LiberaDoAtacando();
        Destroy(this);
    }

    private void LiberaDoAtacando()
    {
        gerente.LiberaMovimento(CreatureManager.CreatureState.aplicandoGolpe);
        GetComponent<Animator>().SetBool("atacando", false);
        retornou = true;
    }

    void ParaliseNoTempo()
    {
        tempoDecorrido = 0.0f;
        if (!gerente.MudaAplicaGolpe())
            Destroy(this);            
    }
}
