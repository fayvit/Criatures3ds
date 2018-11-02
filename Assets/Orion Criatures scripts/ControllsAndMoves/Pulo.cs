using UnityEngine;
using System;

public class Pulo
{
    CaracteristicasDePulo caracteristicas;
    ElementosDeMovimentacao elementos;
    Vector3 movimentoVertical = Vector3.zero;
    float ultimoYFundamentado = 0;
    float tempoDePulo = 0;
    bool EstouSubindo = false;

    public Pulo(CaracteristicasDePulo caracteristicas, ElementosDeMovimentacao elementos)
    {
        this.caracteristicas = caracteristicas;
        this.elementos = elementos;
    }

    public bool EstouPulando
    {
        get { return caracteristicas.estouPulando; }
    }

    public void IniciaAplicaPulo()
    {
        ultimoYFundamentado = elementos.transform.position.y;
        caracteristicas.estouPulando = true;
        elementos.controle.Move(Vector3.up * caracteristicas.impulsoInicial);
        elementos.animador.AnimaIniciaPulo();
    }

    public void VerificaPulo(Vector3 direcaoMovimento)
    {

        if (caracteristicas.estavaPulando == false && caracteristicas.estouPulando == true)
        {
            tempoDePulo = 0;
            EstouSubindo = true;
        }

        caracteristicas.estavaPulando = caracteristicas.estouPulando;

        if (
            EstouSubindo == true
            &&
            (elementos.transform.position.y - ultimoYFundamentado < caracteristicas.alturaDoPulo
         &&
         tempoDePulo < caracteristicas.tempoMaxPulo
         ))
        {

            tempoDePulo += Time.deltaTime;
            movimentoVertical = (direcaoMovimento * caracteristicas.velocidadeDuranteOPulo
                + Vector3.up * caracteristicas.velocidadeSubindo);
            elementos.controle.Move(movimentoVertical * Time.deltaTime);

        }
        else if (
          (elementos.transform.position.y - ultimoYFundamentado >= caracteristicas.alturaDoPulo
       ||
       tempoDePulo >= caracteristicas.tempoMaxPulo
       )
          &&
          EstouSubindo == true)
        {
            EstouSubindo = false;
            elementos.controle.Move(movimentoVertical * Time.deltaTime);
        }
        else if (EstouSubindo == false)
        {

            movimentoVertical = Vector3.Lerp(movimentoVertical,
                                             (direcaoMovimento * caracteristicas.velocidadeDuranteOPulo
                                             + Vector3.down * caracteristicas.velocidadeDescendo),
                                             caracteristicas.amortecimentoNaTransicaoDePulo * Time.deltaTime);
            elementos.controle.Move(movimentoVertical * Time.deltaTime);

        }
    }

    public void NaoEstouPulando()
    {
        
        if (caracteristicas.estouPulando)
            elementos.animador.AnimaDescendoPulo();
            

        //else
            //elementos.animador.ResetaTriggerDeTocandoOChao();
            
        caracteristicas.estouPulando = false;
        caracteristicas.estavaPulando = false;
        movimentoVertical = Vector3.zero;
    }
}