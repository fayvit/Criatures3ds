using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavernaIntroConfigs:SceneConfigs
{
    public CavernaIntroConfigs()
    {
        ListaEncontravel = new List<Encontravel>();
        SceneMusic = NameMusic.Mushrooms;
        CamColor = new Color(53f/255,32f/255,22f/255,1);   
        
    }
}
