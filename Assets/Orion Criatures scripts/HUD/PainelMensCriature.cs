using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class PainelMensCriature
{
    [SerializeField] private Text textoDaMensagem;

    private float tempoDeFuga = 0;

    // Use this for initialization

    void Start()
    {
        // p = this;
        //gameObject.SetActive(false);
    }

    public void AtivarNovaMens(string mensagem, int fontSize, float tempoDeFuga = 0)
    {
        textoDaMensagem.transform.parent.gameObject.SetActive(true);
        textoDaMensagem.text = mensagem;
        textoDaMensagem.fontSize = fontSize;
        this.tempoDeFuga = tempoDeFuga;

        GameController.g.Manager.StopAllCoroutines();
        if (tempoDeFuga > 0)
            GameController.g.Manager.StartCoroutine(DesligueIsso(tempoDeFuga));
    }

    IEnumerator DesligueIsso(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        if(tempoDeFuga>0)
            textoDaMensagem.transform.parent.gameObject.SetActive(false);
    }

    public void EsconderMensagem()
    {
        textoDaMensagem.transform.parent.gameObject.SetActive(false);
    }
}
