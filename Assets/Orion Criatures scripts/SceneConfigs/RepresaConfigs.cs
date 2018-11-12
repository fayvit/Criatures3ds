using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepresaConfigs : SceneConfigs
{
    public RepresaConfigs()
    {
        SceneMusic = NameMusic.Mushrooms;
        CamColor = new Color(41f/255,147f/255,182f/255);
    }

    public override bool LocaisSeguros()
    {
        return false;
    }

    public override List<Encontravel> ListaEncontravel
    {
        get
        {
            Vector3 pos = GameController.g.Manager.transform.position;
            if (pos.x > -370 && pos.z > 210 && pos.x < -310 && pos.z < 265)
            {
                base.ListaEncontravel = new List<Encontravel>()
            {
                new Encontravel(nomesCriatures.Marak,15,1,2),
                new Encontravel(nomesCriatures.Babaucu,10,1,2),
                new Encontravel(nomesCriatures.Arpia,15,1,2),
                new Encontravel(nomesCriatures.Serpente,10,1,2),
                new Encontravel(nomesCriatures.Escorpion,15,1,2),
                new Encontravel(nomesCriatures.Wisks,7,1,2),
                new Encontravel(nomesCriatures.Iruin,20,1,2),
                new Encontravel(nomesCriatures.Aladegg,8,1,2),
            };
            }
            else if (pos.x > -468 && pos.z > -8.4 && pos.x < -407 && pos.z < 61)
            {
                base.ListaEncontravel = new List<Encontravel>()
            {
                new Encontravel(nomesCriatures.Marak,10,1,2),
                new Encontravel(nomesCriatures.Marak,5,2,3),
                new Encontravel(nomesCriatures.Babaucu,10,1,2),
                new Encontravel(nomesCriatures.Arpia,10,1,2),
                new Encontravel(nomesCriatures.Arpia,5,2,3),
                new Encontravel(nomesCriatures.Serpente,7,1,2),
                new Encontravel(nomesCriatures.Steal,3,1,2),
                new Encontravel(nomesCriatures.Escorpion,15,1,2),
                new Encontravel(nomesCriatures.Wisks,7,1,2),
                new Encontravel(nomesCriatures.Iruin,10,1,2),
                new Encontravel(nomesCriatures.Onarac,10,1,2),
                new Encontravel(nomesCriatures.Aladegg,8,1,2),
            };
            }
            else
            {
                base.ListaEncontravel = new List<Encontravel>()
            {
                new Encontravel(nomesCriatures.Marak,15,1,2),
                new Encontravel(nomesCriatures.Babaucu,10,1,2),
                new Encontravel(nomesCriatures.Arpia,25,1,2),
                new Encontravel(nomesCriatures.Escorpion,15,1,2),
                new Encontravel(nomesCriatures.Wisks,7,1,2),
                new Encontravel(nomesCriatures.Iruin,20,1,2),
                new Encontravel(nomesCriatures.Aladegg,8,1,2),
            };
            }
                return base.ListaEncontravel;
        }
    }
}
