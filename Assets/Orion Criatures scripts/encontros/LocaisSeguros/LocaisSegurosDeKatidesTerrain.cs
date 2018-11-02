using UnityEngine;
using System.Collections;

public class LocaisSegurosDeKatidesTerrain
{
    public static bool LocalSeguro()
    {
        bool retorno = false;
        Vector3 pos = GameController.g.Manager.transform.position;
        if ((pos.x > 670 && pos.z > 1829 && pos.x < 810 && pos.z < 1980)
            ||
            (pos.x > 340 && pos.z > 2111 && pos.x < 420 && pos.z < 2249)
            ||
            (pos.x > 537 && pos.z > 2169 && pos.x < 591 && pos.z < 2197)
            ||
            (pos.x > 690 && pos.z > 2090 && pos.x < 750 && pos.z < 2190)
                )
            retorno = true;
        return retorno;
    }
}