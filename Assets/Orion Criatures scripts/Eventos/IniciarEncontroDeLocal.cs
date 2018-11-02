using UnityEngine;
using System.Collections;

public class IniciarEncontroDeLocal : MonoBehaviour
{
    [SerializeField]private CriatureBase C;
    [SerializeField]private string chave;

    private bool iniciouLuta;
    private CreatureManager cm;

    void Update()
    {
        if (iniciouLuta)
        {
            if (cm.MeuCriatureBase.CaracCriature.meusAtributos.PV.Corrente <= 0 || cm == null)
            {
                GameController.g.MyKeys.MudaAutoShift(chave, true);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !GameController.g.MyKeys.VerificaAutoShift(chave))
        {
            cm = InsereInimigoEmCampo.RetornaInimigoEmCampo(C);
            GameController.g.EncontroAgoraCom(cm);
            iniciouLuta = true;
        }
        else if (GameController.g.MyKeys.VerificaAutoShift(chave))
            Destroy(gameObject);
    }
}
