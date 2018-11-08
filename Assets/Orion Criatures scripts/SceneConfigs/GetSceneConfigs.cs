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
        }
        return S;
    }
}
