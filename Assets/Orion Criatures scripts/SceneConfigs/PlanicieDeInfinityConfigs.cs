using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanicieDeInfinityConfigs : SceneConfigs {

    public PlanicieDeInfinityConfigs()
    {
        SceneMusic = NameMusic.Mushrooms;
    }

    public override bool LocaisSeguros()
    {
        bool retorno = false;
        Vector3 pos = GameController.g.Manager.transform.position;
        if ((pos.x > 248 && pos.z > 111 && pos.x < 390 && pos.z < 198)
            ||
            (pos.x > 280 && pos.z > 190 && pos.x < 350 && pos.z < 220)
            //||
            //(pos.x > 73 && pos.z > 168 && pos.x < 123 && pos.z < 118)
                )
            retorno = true;
        return retorno;
    }
}
