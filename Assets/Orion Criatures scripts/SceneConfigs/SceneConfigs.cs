using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneConfigs {

    private float volume = 1;
    private NameMusic sceneMusic = NameMusic.nula;

    private Color camColor = new Color(88f / 255, 155f / 255, 188f / 255, 1);
    private List<Encontravel> lEncontravel = new List<Encontravel>()
                    {
                        new Encontravel(nomesCriatures.Marak,25,1,2),
                        new Encontravel(nomesCriatures.Arpia,25,1,2),
                        new Encontravel(nomesCriatures.Escorpion,15,1,2),
                        new Encontravel(nomesCriatures.Wisks,7,1,2),
                        new Encontravel(nomesCriatures.Iruin,20,1,2),
                        new Encontravel(nomesCriatures.Onarac,8,1,2),
                    };


    public List<Encontravel> ListaEncontravel
    {
        get
        {
            //Debug.Log("Foi Utilizada a Lista de encontros Default");
            return lEncontravel;
        }
        protected set { lEncontravel = value; }
    }

    public Color CamColor
    {
        get { return camColor; }
        protected set{ camColor = value; }
    }

    public NameMusic SceneMusic
    {
        get { return sceneMusic; }
        protected set { sceneMusic = value; }
    }

    public float MusicVolume
    {
        get { return volume;}
        protected set { volume = value; }
    }

    public virtual bool LocaisSeguros()
    {
        return true;
    }


}
