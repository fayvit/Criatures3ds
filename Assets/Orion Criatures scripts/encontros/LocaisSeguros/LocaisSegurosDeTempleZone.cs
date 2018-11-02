using UnityEngine;
using System.Collections;

public class LocaisSegurosDeTempleZone
{
    public static bool LocalSeguro()
    {
        bool retorno = false;
        Vector3 pos = GameController.g.Manager.transform.position;
        if ((pos.x > 707 && pos.z > 2821 && pos.x < 764 && pos.z < 2987)
            || (pos.x > 669 && pos.z > 2906 && pos.x < 802 && pos.z < 2938)
            || (pos.x > 487 && pos.z > 2514 && pos.x < 562 && pos.z < 2552))
            retorno = true;
        return retorno;
    }
}