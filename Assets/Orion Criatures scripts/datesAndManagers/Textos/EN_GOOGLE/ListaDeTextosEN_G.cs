using UnityEngine;
using System.Collections.Generic;

public class ListaDeTextosEN_G
{
    static Dictionary<ChaveDeTexto, List<string>> txt;
    public static Dictionary<ChaveDeTexto, List<string>> Txt
    {
        get
        {
            if (txt == null)
            {
                txt = new Dictionary<ChaveDeTexto, List<string>>();

                ColocaTextos(ref txt, TextosChaveEmEN_G.txt);
                ColocaTextos(ref txt, TextosDeBarreirasEN_G.txt);
                ColocaTextos(ref txt, TextosDaCavernaInicialEN_G.txt);
                ColocaTextos(ref txt, TextosDeKatidsEN_G.txt);
                ColocaTextos(ref txt, TextosDeMarjanEN_G.txt);
                ColocaTextos(ref txt, TextosDeInfoEN_G.txt);
            }

            return txt;
        }
    }

    static void ColocaTextos(ref Dictionary<ChaveDeTexto, List<string>> retorno, Dictionary<ChaveDeTexto, List<string>> inserir)
    {
        foreach (ChaveDeTexto k in inserir.Keys)
        {
            retorno[k] = inserir[k];
        }
    }

}