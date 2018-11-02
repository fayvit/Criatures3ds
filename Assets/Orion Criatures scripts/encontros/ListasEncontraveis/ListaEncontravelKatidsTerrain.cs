using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ListaEncontravelKatidsTerrain
{
    public static List<Encontravel> EncontraveisDaqui
    {
        get
        {
            List<Encontravel> retorno = new List<Encontravel>();
            Vector3 pos = GameController.g.Manager.transform.position;
            if (pos.x > 912 && pos.z > 2040 && pos.x < 1090 && pos.z < 2185)
            {
                retorno = new List<Encontravel>()
                    {
                        new Encontravel(nomesCriatures.Marak,18,1,2),
                        new Encontravel(nomesCriatures.Arpia,18,1,2),
                        new Encontravel(nomesCriatures.Escorpion,13,1,2),
                        new Encontravel(nomesCriatures.Aladegg,15,1,2),
                        new Encontravel(nomesCriatures.Wisks,6,1,2),
                        new Encontravel(nomesCriatures.Iruin,24,1,2),
                        new Encontravel(nomesCriatures.Onarac,6,1,2),
                    };
            }else if (pos.x > 625 && pos.z > 2088 && pos.x < 714 && pos.z < 2222)
            {
                retorno = new List<Encontravel>()
                    {
                        new Encontravel(nomesCriatures.Marak,22,1,2),
                        new Encontravel(nomesCriatures.Arpia,23,1,2),
                        new Encontravel(nomesCriatures.Escorpion,18,1,2),
                        new Encontravel(nomesCriatures.Steal,2,1,2),
                        new Encontravel(nomesCriatures.Iruin,24,1,2),
                        new Encontravel(nomesCriatures.Onarac,11,1,2),
                    };
            } else
                retorno = ListaDeEncontraveis.Default;
            return retorno;
        }
    }
}
