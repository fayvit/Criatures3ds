using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class PainelDeAcoes
{
    [SerializeField] private Text textoDoPainel;

    public bool ActiveSelf
    {
        get { return textoDoPainel.transform.parent.gameObject.activeSelf; }
    }

    public void SetActive(bool x, string texto = "")
    {
        textoDoPainel.transform.parent.gameObject.SetActive(x);
        textoDoPainel.text = texto;
    }
}