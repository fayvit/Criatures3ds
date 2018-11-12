using UnityEngine;
using System.Collections.Generic;

public static class TextosDeInfoPT_BR
{
    public static Dictionary<ChaveDeTexto, List<string>> txt = new Dictionary<ChaveDeTexto, List<string>>()
    {
        {ChaveDeTexto.ensinaTrocarCriature, new List<string>()
        {
            "Use <color=cyan>X</color> para alternar para o controle do criature"
        }
        },
        {ChaveDeTexto.ensinaBotaoDeAtacar, new List<string>()
        {
            "Controlando o criature, use <color=cyan>A</color> para atacar"
        }
        },
        {ChaveDeTexto.ensinaMudarDeAtaque, new List<string>()
        {
            "Controlando o criature, segure <color=cyan>R</color> e pression <color=cyan>B</color> para trocar o golpe do criature"
        }
        },
        {ChaveDeTexto.ensinaUsarItem, new List<string>()
        {
            "Com o criature ou com o heroi, segure <color=cyan>L</color> e pressione <color=cyan>A</color> para usar um item"
        }
        },
        {ChaveDeTexto.ensinaTrocarItem, new List<string>()
        {
            "Segure<color=cyan>R</color> e pressione <color=cyan>Y</color>ou <color=magenta>A</color>,  para alterar o item selecionado para uso"
        }
        },
        {ChaveDeTexto.mudarDeCriature, new List<string>()
        {
            "Com mais de um criature, segure <color=cyan>L</color> e pressione <color=cyan>X</color> para substituir o criature ativo"
        }
        },
        {ChaveDeTexto.oCriatureSelecionadoParaMudanca, new List<string>()
        {
            "Com mais de dois criatures...",
            "... segure <color=cyan>R</color> e pressione <color=Cyan>X</color> para escolher entre os criature que você carega"
        }
        },
        {ChaveDeTexto.gradeDeEsgoto, new List<string>()
        {
            "É notável pelo tamanho da tubulação que alguem pode caminhar ai dentro. Se não fossem as grades poderiamos entrar."
        }
        },
         {ChaveDeTexto.ensinaAndarE_Correr, new List<string>()
        {
            "Use <color=cyan>Y</color> para correr"
        }
        },
          {ChaveDeTexto.ensinaPular, new List<string>()
        {
            "Use <color=cyan>B</color> para pular"
        }
        },
           {ChaveDeTexto.ensinaCamera, new List<string>()
        {
            "Use <color=cyan>C-Stick</color> para mover a camera ou segure <color=magenta>L+X</color> para mover a camera para mover a camera com o analogico",
            "Use <color=cyan> ZL/ZR</color> ou segure <color=cyan>L</color> e pressione <color=cyan>B</color> para centralizar a camera",
        }
        },
        {ChaveDeTexto.pedraDiferente, new List<string>()
        {
            "Parece que há algo diferente com essa pedra!!"
        }
        },


    };
}