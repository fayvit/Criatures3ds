using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GerenciadorDeGolpes
{
    public int golpeEscolhido = 0;
    [SerializeField]public List<GolpePersonagem> listaDeGolpes;
    [SerializeField]public List<GolpeBase> meusGolpes;


    List<GolpePersonagem> ListaDeGolpesAtualizada(nomesCriatures nome)
    {
        return personagemG2.RetornaUmCriature(nome).GerenteDeGolpes.listaDeGolpes;
    }

    public GolpePersonagem ProcuraGolpeNaLista(nomesCriatures nome,nomesGolpes esseGolpe)
    {
        GolpePersonagem retorno = new GolpePersonagem();
        listaDeGolpes = ListaDeGolpesAtualizada(nome);

        for (int i = 0; i < listaDeGolpes.Count; i++)
            if (listaDeGolpes[i].Nome == esseGolpe)
                retorno = listaDeGolpes[i];

        return retorno;
    }

    public GolpePersonagem VerificaGolpeDoNivel(nomesCriatures nome, int nivel)
    {
        GolpePersonagem retorno = new GolpePersonagem();
        listaDeGolpes = ListaDeGolpesAtualizada(nome);

        for (int i = 0; i < listaDeGolpes.Count; i++)
            if (listaDeGolpes[i].NivelDoGolpe == nivel)
                retorno = listaDeGolpes[i];

        return retorno;
    }

    public bool TemEsseGolpe(nomesGolpes nome)
    {
        for (int i = 0; i < meusGolpes.Count; i++)
        {
            if (meusGolpes[i].Nome == nome)
                return true;
        }

        return false;
    }
}
