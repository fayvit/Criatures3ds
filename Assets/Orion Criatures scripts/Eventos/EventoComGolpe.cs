using UnityEngine;
using System.Collections;

public abstract class EventoComGolpe : AtivadorDeBotao
{
    [SerializeField]protected string ID;
    //[SerializeField]private KeyShift chaveEspecial = KeyShift.nula;
    [SerializeField]private nomesGolpes[] ativaveis;
    [Space(5)]
    [SerializeField]private bool todoDoTipo = false;
    [SerializeField]private nomeTipos tipoParaAtivar = nomeTipos.nulo;
    
    /*
    protected KeyShift ChaveEspecial
    {
        get { return chaveEspecial; }
    }*/

    private bool VerificaGolpeNaLista(nomesGolpes nomeDoGolpe)
    {
        for (int i = 0; i < ativaveis.Length; i++)
            if (ativaveis[i] == nomeDoGolpe)
                return true;

        return false;
    }

    private void OnValidate()
    {
        BuscadorDeID.Validate(ref ID,this);
    }

    protected bool EsseGolpeAtiva(nomesGolpes nomeDoGolpe)
    {
        Debug.Log(nomeDoGolpe);
        bool ativa = false;
        if (todoDoTipo)
            if (PegaUmGolpeG2.RetornaGolpe(nomeDoGolpe).Tipo == tipoParaAtivar)
                ativa = true;
        if (VerificaGolpeNaLista(nomeDoGolpe))
            ativa = true;

        return ativa;
    }

    public abstract void DisparaEvento(nomesGolpes nomeDoGolpe);
}
