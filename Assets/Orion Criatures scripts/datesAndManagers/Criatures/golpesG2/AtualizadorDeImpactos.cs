using UnityEngine;
using System.Collections;

public class AtualizadorDeImpactos
{
    private bool procurouAlvo = false;
    private bool addView = false;
    private float tempoDecorrido = 0;
    private Transform alvoProcurado;
    private CharacterController controle;

    public void ReiniciaAtualizadorDeImpactos()
    {
        tempoDecorrido = 0;
        addView = false;
        procurouAlvo = false;
    }

    public static void ajudaAtaque(Transform alvo,Transform T)
    {
        Vector3 forwardInicial = T.forward;
        if (alvo != null)
        {
            if (Vector3.Dot(alvo.position - T.position, T.forward) > -0.25f)
                forwardInicial = alvo.position - T.position;
            

            forwardInicial.y = 0;
            T.rotation = Quaternion.LookRotation(forwardInicial);

        }
    }

    public void ImpatoAtivo(GameObject G,IGolpeBase ativa,CaracteristicasDeImpacto caracteristica)
    {
        tempoDecorrido += Time.deltaTime;
        if (!procurouAlvo)
        {
            alvoProcurado = CriaturesPerto.procureUmBomAlvo(G);
            procurouAlvo = true;
            Debug.Log(alvoProcurado + "  esse é o alvo");
            ajudaAtaque(alvoProcurado, G.transform);
        }

        if (!addView)
        {
            tempoDecorrido += ativa.TempoDeMoveMin;
            ColisorDeGolpe.AdicionaOColisor(G,ativa,caracteristica,tempoDecorrido);

            addView = true;

        }

        if (tempoDecorrido < ativa.TempoDeMoveMax)
        {
            if (((int)(tempoDecorrido * 10)) % 2 == 0 && alvoProcurado)
                ajudaAtaque(alvoProcurado,G.transform);

            ativa.DirDeREpulsao = G.transform.forward;
            
            if (!controle)
                controle = G.GetComponent<CharacterController>();
            controle.Move(ativa.VelocidadeDeGolpe * G.transform.forward * Time.deltaTime + Vector3.down * 9.8f);
        }
    }
}

[System.Serializable]
public struct CaracteristicasDeImpacto
{
    public string nomeTrail;
    public string noImpacto;
    public bool parentearNoOsso;

    public CaracteristicasDeImpacto(Trails trail,NoImpacto noImpacto,bool parentearOsso)
    {
        nomeTrail = trail.ToString();
        this.noImpacto = noImpacto.ToString();
        this.parentearNoOsso = parentearOsso;
    }
}

public enum Trails
{
    umCuboETrail,
    tresCubos,
    doisCubos,
    dentada,
    tempestadeDeFolhas,
    tempestadeEletrica,
    hidroBomba,
    tosteAtaque,
    chuvaVenenosa,
    avalanche
}

public enum NoImpacto
{
    impactoComum,
    impactoDeFolhas,
    impactoDeAgua,
    impactoDeFogo,
    impactoDeVento,
    impactoDeGosma,
    impactoDeGosmaAcida,
    impactoVenenoso,
    impactoDePedra,
    impactoEletrico,
    impactoDeTerra,
    impactoDeGas
}
