using UnityEngine;
using System.Collections.Generic;

public static class TextosDeInfoPT_BR
{
    public static Dictionary<ChaveDeTexto, List<string>> txt = new Dictionary<ChaveDeTexto, List<string>>()
    {
        {ChaveDeTexto.ensinaTrocarCriature, new List<string>()
        {
            "Use <color=cyan>Q</color> no teclado ou <color=magenta>LS</color> no controle de XBOX para alternar para o controle do criature"
        }
        },
        {ChaveDeTexto.ensinaBotaoDeAtacar, new List<string>()
        {
            "Controlando o criature, use <color=cyan>L-Click[numpad0]</color> no teclado ou <color=#00FF00>A</color>[<color=magenta>RT</color>] no controle de XBOX para atacar"
        }
        },
        {ChaveDeTexto.ensinaMudarDeAtaque, new List<string>()
        {
            "Controlando o criature, use <color=cyan>Tab</color> no teclado ou <color=magenta>LB[DPAD Down]</color> no controle de XBOX para trocar o golpe do criature"
        }
        },
        {ChaveDeTexto.ensinaUsarItem, new List<string>()
        {
            "Com o criature ou com o heroi, use <color=cyan>F</color> no teclado ou <color=#0099FF>X</color> no controle de XBOX para usar um item"
        }
        },
        {ChaveDeTexto.ensinaTrocarItem, new List<string>()
        {
            "Use <color=cyan>numero 1/(numero 2)</color>, no teclado ou <color=magenta>(DPAD Left)/(DPAD Right)</color>, no controle de XBOX para alterar...",
            "... o item selecionado para uso"
        }
        },
        {ChaveDeTexto.mudarDeCriature, new List<string>()
        {
            "Com mais de um criature, use <color=cyan>R</color>, no teclado ou <color=orange>Y</color>, no controle de XBOX para substituir o criature ativo"
        }
        },
        {ChaveDeTexto.oCriatureSelecionadoParaMudanca, new List<string>()
        {
            "Com mais de dois criatures...",
            "... use <color=cyan>C</color>, no teclado ou <color=magenta>LT[DPAD Up]</color>, no controle de XBOX para escolher entre os criature que você carega"
        }
        },
        {ChaveDeTexto.gradeDeEsgoto, new List<string>()
        {
            "É notável pelo tamanho da tubulação que alguem pode caminhar ai dentro. Se não fossem as grades poderiamos entrar."
        }
        },
         {ChaveDeTexto.ensinaAndarE_Correr, new List<string>()
        {
            "Use <color=cyan>A,W,S,D[Left,Up,Down,Right Arrows]</color> no teclado ou <color=magenta>Left Stick</color> no controle de XBOX para andar",
            "Use <color=cyan>Shift</color> no teclado ou <color=magenta>LB</color> no controle de XBOX para correr"
        }
        },
          {ChaveDeTexto.ensinaPular, new List<string>()
        {
            "Use <color=cyan>Space</color> no teclado ou <color=red>B</color> no controle de XBOX para pular"
        }
        },
           {ChaveDeTexto.ensinaCamera, new List<string>()
        {
            "Use <color=cyan>Mouse move</color> no pc ou <color=magenta>Right Stick</color> no controle de XBOX para mover a camera",
            "Use <color=cyan> X [Left Alt,Right Click]</color> no teclado ou <color=magenta>RS</color> no controle de XBOX para centralizar a camera",
        }
        },


    };
}