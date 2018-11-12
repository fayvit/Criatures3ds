using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSceneConfigs
{

    public static SceneConfigs Get(NomesCenas nomeCena)
    {
        SceneConfigs S = new SceneConfigs();
        switch (nomeCena)
        {
            case NomesCenas.cavernaIntro:
                S = new CavernaIntroConfigs();
            break;
            case NomesCenas.planicieDeInfinity:
            case NomesCenas.planicieDeInfinity_b:
            case NomesCenas.outraModularScene:
            case NomesCenas.modularScene:
                S = new PlanicieDeInfinityConfigs();
            break;
            case NomesCenas.represa:
            case NomesCenas.represa_c:
                S = new RepresaConfigs();
            break;
            default:
                Debug.Log("Scenes Config retornou o valor default");
                Debug.LogWarning("Scenes Config retornou o valor default");
            break;
        }
        return S;
    }
}
