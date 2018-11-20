using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanicieDeInfinityConfigs : SceneConfigs {

    public PlanicieDeInfinityConfigs()
    {
        SceneMusic = NameMusic.Field2;
    }

    public override bool LocaisSeguros()
    {
        bool retorno = false;
        Vector3 pos = GameController.g.Manager.transform.position;
        if ((pos.x > 117 && pos.z > 250 && pos.x < 259 && pos.z < 337)
            ||
            (pos.x > 160 && pos.z > 230 && pos.x < 220 && pos.z < 252)
            //||
            //(pos.x > 73 && pos.z > 168 && pos.x < 123 && pos.z < 118)
                )
            retorno = true;
        return retorno;
    }
}
