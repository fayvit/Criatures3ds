using UnityEngine;
using System.Collections;

[System.Serializable]
public class OlharEnfraquecedor : ProjetilBase
{
    public OlharEnfraquecedor() : base(
        new ContainerDeCaracteristicasDeGolpe()
        {
            nome = nomesGolpes.olharEnfraquecedor,
            tipo = nomeTipos.Normal,
            carac = caracGolpe.projetil,
            custoPE = 4,
            potenciaCorrente = 1,
            potenciaMaxima = 8,
            potenciaMinima = 1,
            tempoDeReuso = 30f,
            tempoDeMoveMax = 1,
            tempoDeMoveMin = 0.3f,
            tempoDeDestroy = 4,
            TempoNoDano = 0.75f,
            velocidadeDeGolpe = 10,//18
        }
        )
    {
        carac = new CaracteristicasDeProjetil()
        {
            noImpacto = NoImpacto.impactoComum,
            tipo = TipoDoProjetil.statusExpansivel
        };
    }

    public override void VerificaAplicaStatus(CriatureBase atacante, CreatureManager cDoAtacado)
    {
        StatusTemporarioBase S = new Fraco()
        {
            Dados = new DatesForTemporaryStatus()
            {
                Quantificador = 2,
                TempoSignificativo = 250,
                Tipo = TipoStatus.fraco //trocavel
            },
            CDoAfetado = cDoAtacado,
            OAfetado = cDoAtacado.MeuCriatureBase
        };

        int num = StatusTemporarioBase.ContemStatus(TipoStatus.fraco/*trocavel*/, cDoAtacado.MeuCriatureBase);

        AdicionaStatusSimples.InsereStatusSimples(cDoAtacado, S, num);

        Debug.Log("enfraqueceu");
    }
}
