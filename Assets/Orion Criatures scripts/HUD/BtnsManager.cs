using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BtnsManager : MonoBehaviour
{

    /// <summary>
    /// Desliga os Botoes do gameObject paramentro
    /// </summary>
    /// <param name="container">GameObject que terá todos os seus botoes desligados</param>
    public static void DesligarBotoes(GameObject container)
    {
        Button[] Bs = container.GetComponentsInChildren<Button>();

        foreach (Button B in Bs)
            B.interactable = false;
    }

    /// <summary>
    /// Religa os botoes do gameObject parametro
    /// </summary>
    /// <param name="G">GameObject que tera os botoes religados</param>
    public static void ReligarBotoes(GameObject G)
    {

        Button[] Bs = G.GetComponentsInChildren<Button>();

        foreach (Button B in Bs)
            B.interactable = true;
    }
}
