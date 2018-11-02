using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ListaEncontravelDeTempleZone
{
    public static List<Encontravel> EncontraveisDaqui
    {
        get
        {
            List<Encontravel> retorno = new List<Encontravel>();
            Vector3 pos = GameController.g.Manager.transform.position;
            if (pos.x > 228 && pos.z > 2995 && pos.x < 329 && pos.z < 3095)
            {
                retorno = new List<Encontravel>()
                    {
                        new Encontravel(nomesCriatures.Marak,20,2,3),
                        new Encontravel(nomesCriatures.Arpia,20,2,3),
                        new Encontravel(nomesCriatures.Escorpion,10,2,3),
                        new Encontravel(nomesCriatures.Florest,5,2,3),
                        new Encontravel(nomesCriatures.Iruin,15,2,3),
                        new Encontravel(nomesCriatures.Onarac,15,2,3),
                        new Encontravel(nomesCriatures.Aladegg,15,2,3),
                    };
            }
            else if (pos.x > 266 && pos.z > 2636 && pos.x < 470 && pos.z < 2791 &&
                RetaDelimitadora.RelativoAReta(451, 2733, 353, 2783, false, pos.x, pos.z)
                )
            {
                retorno = new List<Encontravel>()
                    {
                        new Encontravel(nomesCriatures.Marak,20,2,3),
                        new Encontravel(nomesCriatures.Arpia,20,2,3),
                        new Encontravel(nomesCriatures.Escorpion,10,2,3),
                        new Encontravel(nomesCriatures.PolyCharm,5,2,3),
                        new Encontravel(nomesCriatures.Iruin,15,2,3),
                        new Encontravel(nomesCriatures.Onarac,15,2,3),
                        new Encontravel(nomesCriatures.Aladegg,15,2,3),
                    };
            }
            else if (pos.x > 954 && pos.z > 2775 && pos.x < 1088 && pos.z < 2867)
            {
                retorno = new List<Encontravel>()
                    {
                        new Encontravel(nomesCriatures.Marak,20,2,3),
                        new Encontravel(nomesCriatures.Arpia,20,2,3),
                        new Encontravel(nomesCriatures.Escorpion,10,2,3),
                        new Encontravel(nomesCriatures.Xuash,5,2,3),
                        new Encontravel(nomesCriatures.Iruin,15,2,3),
                        new Encontravel(nomesCriatures.Onarac,15,2,3),
                        new Encontravel(nomesCriatures.Aladegg,15,2,3),
                    };
            }
            else if ((pos.x > 799 && pos.z > 2821 && pos.x < 843 && pos.z < 2882)||
                (pos.x > 641 && pos.z > 2970 && pos.x < 682 && pos.z < 3022))
            {
                retorno = new List<Encontravel>()
                    {
                        new Encontravel(nomesCriatures.Marak,19,2,3),
                        new Encontravel(nomesCriatures.Arpia,19,2,3),
                        new Encontravel(nomesCriatures.Escorpion,10,2,3),
                        new Encontravel(nomesCriatures.Serpente,10,2,3),
                        new Encontravel(nomesCriatures.Iruin,14,2,3),
                        new Encontravel(nomesCriatures.Onarac,14,2,3),
                        new Encontravel(nomesCriatures.Wisks,14,2,3),
                    };
            }
            else {
                retorno = new List<Encontravel>()
                    {
                        new Encontravel(nomesCriatures.Marak,20,2,3),
                        new Encontravel(nomesCriatures.Arpia,10,2,3),
                        new Encontravel(nomesCriatures.Escorpion,10,2,3),
                        new Encontravel(nomesCriatures.Iruin,15,2,3),
                        new Encontravel(nomesCriatures.Wisks,15,2,3),
                        new Encontravel(nomesCriatures.Onarac,15,2,3),
                        new Encontravel(nomesCriatures.Aladegg,15,2,3),
                    };
            }

            return retorno;
        }
    }
}
