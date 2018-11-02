using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntervaloDeItens : MonoBehaviour
{

    [SerializeField] private Text texto;
    [SerializeField] private Image imgDoTexto;
    // Update is called once per frame
    void Update()
    {
        if (GameController.g)
        {
            if (GameController.g.Manager.Dados.Itens.Count > 0)
            {
                if (Time.time > GameController.g.Manager.Dados.TempoDoUltimoUsoDeItem + MbItens.INTERVALO_DO_USO_DE_ITEM)
                {
                    imgDoTexto.gameObject.SetActive(false);
                }
                else
                {
                    imgDoTexto.gameObject.SetActive(true);
                    texto.text = CreatureManager.MostradorDeTempo(-Time.time + GameController.g.Manager.Dados.TempoDoUltimoUsoDeItem + MbItens.INTERVALO_DO_USO_DE_ITEM, "s", false);
                    texto.text = (texto.text == "0") ? "0." : texto.text;
                }
            }
        }
    }
}
