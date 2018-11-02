using UnityEngine;
using System.Collections.Generic;

public class NomeDeArmagedomPT_BR : MonoBehaviour
{
    public static Dictionary<IndiceDeArmagedoms, string> n = new Dictionary<IndiceDeArmagedoms, string>()
    {
        {
            IndiceDeArmagedoms.cavernaIntro,"Caverna da resistencia"
        },
        {
            IndiceDeArmagedoms.saidaDaCaverna,"Saída da caverna"
        },
        {
            IndiceDeArmagedoms.deKatids,"Katids"
        },
        {
            IndiceDeArmagedoms.miniKatidsVsTemple,"Mini armagedom da area do templo"
        },
        {
            IndiceDeArmagedoms.Marjan,"Marjan"
        }
    };    
}
