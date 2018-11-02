using System;
using UnityEngine;

[Serializable]
public class LanguageMenu : UiDeOpcoes
{
    private Action<int> acaoDeOpcao;
    [SerializeField]private OpcaoDeLinguagem[] minhasOpcoes;

    [Serializable]
    private struct OpcaoDeLinguagem
    {
        public idioma key;
        public string labelDoIdioma;
        public Sprite imgDoIdioma;
    }

    public Sprite BandeirinhaAtualSelecionada()
    {
        for (int i = 0; i < minhasOpcoes.Length; i++)
        {
            if (minhasOpcoes[i].key == BancoDeTextos.linguaChave)
                return minhasOpcoes[i].imgDoIdioma;
        }

        return null;
    }

    public idioma IdiomaNoIndice(int indice)
    {
        return minhasOpcoes[indice].key;
    }

    public Sprite BandeirinhaNoIndice(int indice)
    {
        return minhasOpcoes[indice].imgDoIdioma;
    }

    public void IniciarHud(Action<int> acao)
    {
        acaoDeOpcao += acao;
        IniciarHUD(minhasOpcoes.Length);
        
    }

    public override void SetarComponenteAdaptavel(GameObject G, int indice)
    {
        G.GetComponent<UmaOpcaoDeLinguagem>().SetarOpcao(acaoDeOpcao, 
            minhasOpcoes[indice].labelDoIdioma, 
            minhasOpcoes[indice].imgDoIdioma);
    }

    protected override void FinalizarEspecifico()
    {
        acaoDeOpcao = null;
    }
}

