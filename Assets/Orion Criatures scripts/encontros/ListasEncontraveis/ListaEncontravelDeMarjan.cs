using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ListaEncontravelDeMarjan
{
    public static List<Encontravel> EncontraveisDaqui
    {
        get
        {
            List<Encontravel> retorno = new List<Encontravel>();
            Vector3 pos = GameController.g.Manager.transform.position;
            if (pos.x > 690 && pos.z > 3295 && pos.x < 740 && pos.z < 3335)
            {
                retorno = new List<Encontravel>()
                    {
                        new Encontravel(nomesCriatures.Marak,15,3,4),
                        new Encontravel(nomesCriatures.Arpia,15,3,4),
                        new Encontravel(nomesCriatures.Escorpion,10,3,4),
                        new Encontravel(nomesCriatures.Babaucu,15,3,4),
                        new Encontravel(nomesCriatures.Serpente,10,3,4),
                        new Encontravel(nomesCriatures.Iruin,15,3,4),
                        new Encontravel(nomesCriatures.Onarac,10,3,4),
                        new Encontravel(nomesCriatures.Aladegg,10,3,4),
                    };
            }
            else
                retorno  = new List<Encontravel>()
                    {
                        new Encontravel(nomesCriatures.Marak,15,3,4),
                        new Encontravel(nomesCriatures.Arpia,15,3,4),
                        new Encontravel(nomesCriatures.Escorpion,15,3,4),
                        new Encontravel(nomesCriatures.Babaucu,20,3,4),
                        new Encontravel(nomesCriatures.Iruin,15,3,4),
                        new Encontravel(nomesCriatures.Onarac,10,3,4),
                        new Encontravel(nomesCriatures.Aladegg,10,3,4),
                    };
            return retorno;
        }
    }
}
