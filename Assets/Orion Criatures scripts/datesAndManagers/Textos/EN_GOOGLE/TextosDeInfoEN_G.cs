using UnityEngine;
using System.Collections.Generic;

public static class TextosDeInfoEN_G
{
    public static Dictionary<ChaveDeTexto, List<string>> txt = new Dictionary<ChaveDeTexto, List<string>>()
    {
        {ChaveDeTexto.ensinaTrocarCriature, new List<string>()
        {
           "Use <color=cyan> Q </color> on the keyboard or <color=magenta> LS </color> on the XBOX control to switch to the Criature"
        }
        },
        {ChaveDeTexto.ensinaBotaoDeAtacar, new List<string>()
        {
            "Controlling the criature, use <color=cyan> L-Click [numpad0] </color> on the keyboard or <color=#00FF00> A </color> [<color=magenta> RT </color>] XBOX to attack "
        }
        },
        {ChaveDeTexto.ensinaMudarDeAtaque, new List<string>()
        {
            "Controlling the criature, use <color=cyan> Tab </color> on the keyboard or <color=magenta> LB [DPAD Down] </color> on the XBOX control to swap the criature's hit"
        }
        },
        {ChaveDeTexto.ensinaUsarItem, new List<string>()
        {
           "With the creature or hero, use <color=cyan> F </color> on the keyboard or <color=#0099FF> X </color> in the XBOX control to use an item"
        }
        },
        {ChaveDeTexto.ensinaTrocarItem, new List<string>()
        {
            "Use <color=cyan> number1 / (number2) </color> on the keyboard or <color=magenta> (DPAD Left)/(DPAD Right) </color>, on the XBOX control to change ... " ,
            "... the item selected for use"
        }
        },
        {ChaveDeTexto.mudarDeCriature, new List<string>()
        {
            "With more than one criature, use <color=cyan> R </color> on the keyboard or <color=orange> Y </color> in the XBOX control to replace the active criature"
        }
        },
        {ChaveDeTexto.oCriatureSelecionadoParaMudanca, new List<string>()
        {
            "With more than two criatures ...",
            "... use <color=cyan> C </color> on the keyboard or <color=magenta> LT [DPAD Up] </color>, in the XBOX control to choose among the criatures"
        }
        },
        {ChaveDeTexto.gradeDeEsgoto, new List<string>()
        {
            "It's remarkable for the size of the pipe that someone can walk in. If it were not for the grids we could get in."
        }
        },
         {ChaveDeTexto.ensinaAndarE_Correr, new List<string>()
        {
            "Use <color=cyan> A, W, S, D [Left, Up, Right Arrows]</color> on the keyboard or <color=magenta> Left Stick </color> on the XBOX control to walk",
            "Use <color=cyan> Shift </color> on keyboard or <color=magenta> LB </color> in XBOX control to run"
        }
        },
          {ChaveDeTexto.ensinaPular, new List<string>()
        {
            "Use <color=cyan> Space </color> on the keyboard or <color=red> B </color> on the XBOX control to jump"
        }
        },
           {ChaveDeTexto.ensinaCamera, new List<string>()
        {
           "Use <color=cyan> Mouse move </color> on the pc or <color=magenta> Right Stick </color> on the XBOX control to move the camera",
            "Use <color=cyan> X [Right Click, Left Alt] </color> on the keyboard or <color=magenta> RS </color> on the XBOX control to center the camera"
        }
        },


    };
}