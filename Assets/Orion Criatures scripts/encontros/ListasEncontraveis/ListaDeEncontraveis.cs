using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ListaDeEncontraveis
{
    public static List<Encontravel> EncontraveisDaqui
    {
        get
        {
            List<Encontravel> retorno = new List<Encontravel>();
            NomesCenas nomeDaCena = (NomesCenas)System.Enum.Parse(typeof(NomesCenas), SceneManager.GetActiveScene().name);
            switch (nomeDaCena)
            {
                case NomesCenas.katidsTerrain:
                    retorno = ListaEncontravelKatidsTerrain.EncontraveisDaqui;
                break;
                case NomesCenas.Marjan:
                    retorno = ListaEncontravelDeMarjan.EncontraveisDaqui;
                break;
                case NomesCenas.TempleZone:
                    retorno = ListaEncontravelDeTempleZone.EncontraveisDaqui;
                break;
                case NomesCenas.petrolifera:
                    retorno = ListaEncontravelDaPetrolifera.EncontraveisDaqui;
                break;
                case NomesCenas.cavernaIntro:
                break;
                case NomesCenas.esgoto:
                    retorno = ListaEncontravelDoEsgoto.EncontraveisDaqui;
                break;
                default:
                    retorno = Default;
                break;
            }

            return retorno;
        }
    }

    public static List<Encontravel> Default
    {
        get {
            Debug.Log("Foi Utilizada a Lista de encontros Default");
            return new List<Encontravel>()
                    {
                        new Encontravel(nomesCriatures.Marak,25,1,2),
                        new Encontravel(nomesCriatures.Arpia,25,1,2),
                        new Encontravel(nomesCriatures.Escorpion,15,1,2),
                        new Encontravel(nomesCriatures.Wisks,7,1,2),
                        new Encontravel(nomesCriatures.Iruin,20,1,2),
                        new Encontravel(nomesCriatures.Onarac,8,1,2),
                    };
        }
    }
}