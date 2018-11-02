using UnityEngine;
using System.Collections;

public class LocaisSegurosDeMarjan
{
    public static bool LocalSeguro()
    {
        bool retorno = false;
        Vector3 pos = GameController.g.Manager.transform.position;
        if ((pos.x > 538 && pos.z > 3259 && pos.x < 690 && pos.z < 3383)
            || (pos.x > 843 && pos.z > 3406 && pos.x < 868 && pos.z < 3451))
            retorno = true;
        return retorno;
    }
}
