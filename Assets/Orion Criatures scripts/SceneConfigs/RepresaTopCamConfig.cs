using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepresaTopCamConfig : SceneConfigs
{

    public RepresaTopCamConfig()
    {
        SceneMusic = NameMusic.Field2;
    }

    public override bool LocaisSeguros()
    {
        bool retorno = false;
        Vector3 pos = GameController.g.Manager.transform.position;
        if ((pos.x > 725 && pos.z > 165 && pos.x < 790 && pos.z < 205)
            ||
            (pos.x > 573 && pos.z > 63 && pos.x < 626 && pos.z < 100)
            ||
            (pos.x > 740 && pos.z > 18 && pos.x < 795 && pos.z < 75)
                )
            retorno = true;
        return retorno;
    }

    public override List<Encontravel> ListaEncontravel
    {
        get
        {
            Vector3 pos = GameController.g.Manager.transform.position;
            if (pos.x > 570 && pos.z > 100 && pos.x < 630 && pos.z < 210)
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
            else if (pos.x > 800 && pos.z > 60 && pos.x < 880 && pos.z < 100)
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
