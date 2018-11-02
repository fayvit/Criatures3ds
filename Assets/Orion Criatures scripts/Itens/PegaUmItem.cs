using UnityEngine;
using System.Collections;

public class PegaUmItem
{
    public static MbItens Retorna(nomeIDitem nomeItem, int estoque = 1)
    {
        MbItens retorno = new MbItens(new ContainerDeCaracteristicasDeItem());
        switch (nomeItem)
        {
            case nomeIDitem.maca:
                retorno = new MbMaca(estoque);
            break;
            case nomeIDitem.cartaLuva:
                retorno = new MbCartaLuva(estoque);
            break;
            case nomeIDitem.gasolina:
                retorno = new MbGasolina(estoque);
            break;
            case nomeIDitem.aguaTonica:
                retorno = new MbAguaTonica(estoque);
            break;
            case nomeIDitem.aura:
                retorno = new MbAura(estoque);
            break;
            case nomeIDitem.regador:
                retorno = new MbRegador(estoque);
            break;
            case nomeIDitem.ventilador:
                retorno = new MbVentilador(estoque);
            break;
            case nomeIDitem.inseticida:
                retorno = new MbInseticida(estoque);
            break;
            case nomeIDitem.pilha:
                retorno = new MbPilha(estoque);
            break;
            case nomeIDitem.estrela:
                retorno = new MbEstrela(estoque);
            break;
            case nomeIDitem.seiva:
                retorno = new MbSeiva(estoque);
            break;
            case nomeIDitem.quartzo:
                retorno = new MbQuartzo(estoque);
            break;
            case nomeIDitem.adubo:
                retorno = new MbAdubo(estoque);
            break;
            case nomeIDitem.repolhoComOvo:
                retorno = new MbRepolhoComOvo(estoque);
            break;
            case nomeIDitem.pergArmagedom:
                retorno = new MbPergaminhoDeArmagedom(estoque);
            break;
            case nomeIDitem.pergaminhoDePerfeicao:
                retorno = new MbPergaminhoDePerfeicao(estoque);
            break;
            case nomeIDitem.pergaminhoDeFuga:
                retorno = new MbPergaminhoDeFuga(estoque);
            break;
            case nomeIDitem.tinteiroSagradoDeLog:
                retorno = new TinteiroSagradaDeLog(estoque);
            break;
            case nomeIDitem.pergaminhoDeLaurense:
                retorno = new PergaminhoDeLaurense(estoque);
            break;
            case nomeIDitem.pergaminhoDeAnanda:
                retorno = new PergaminhoDeAnanda(estoque);
            break;
            case nomeIDitem.pergaminhoDeBoutjoi:
                retorno = new PergaminhoDeBoutjoi(estoque);
            break;
            case nomeIDitem.canetaSagradaDeLog:
                retorno = new CanetaSagradaDeLog(estoque);
            break;
            case nomeIDitem.pergSinara:
                retorno = new PergaminhoDeSinara(estoque);
            break;
            case nomeIDitem.pergAlana:
                retorno = new PergaminhoDeAlana(estoque);
            break;
            case nomeIDitem.pergSabre:
                retorno = new PergDeSabre(estoque);
            break;
            case nomeIDitem.pergMultiplicar:
                retorno = new PergDoMultiplicar(estoque);
            break;
            case nomeIDitem.antidoto:
                retorno = new Antidoto(estoque);
            break;
            case nomeIDitem.amuletoDaCoragem:
                retorno = new AmuletoDaCoragem(estoque);
            break;
            case nomeIDitem.tonico:
                retorno = new Tonico(estoque);
            break;
            case nomeIDitem.pergOlharEnfraquecedor:
                retorno = new PergOlharEnfraquecedor(estoque);
            break;
            case nomeIDitem.pergOlharMal:
                retorno = new PergOlharMal(estoque);
            break;
            case nomeIDitem.pergFuracaoDeFolhas:
                retorno = new PergFuracaoDeFolhas(estoque);
            break;
            case nomeIDitem.pergVentosCortantes:
                retorno = new PergVentosCortantes(estoque);
            break;
            case nomeIDitem.pergGosmaAcida:
                retorno = new PergGosmaAcida(estoque);
            break;
        }
        return retorno;
    }
}
