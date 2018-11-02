using UnityEngine;
using System.Collections.Generic;

public class NomeDeArmagedomEN_G : MonoBehaviour
{

    public static Dictionary<IndiceDeArmagedoms, string> n = new Dictionary<IndiceDeArmagedoms, string>()
    {
        {
            IndiceDeArmagedoms.cavernaIntro,"Resistence cave"
        },
        {
            IndiceDeArmagedoms.saidaDaCaverna,"Exit of Cave"
        },
        {
            IndiceDeArmagedoms.deKatids,"Katids"
        },
        {
            IndiceDeArmagedoms.miniKatidsVsTemple,"Mini armagedom in temple zone"
        },
        {
            IndiceDeArmagedoms.Marjan,"Marjan"
        }
    };
}