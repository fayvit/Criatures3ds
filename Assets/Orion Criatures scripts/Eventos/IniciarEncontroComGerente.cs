using UnityEngine;
using System.Collections;

public class IniciarEncontroComGerente : MonoBehaviour
{
    
    [SerializeField]    private string chave;
    [SerializeField]    private CriatureBase C;
    [SerializeField]    private bool golpeDeInspector = false;
    [SerializeField]    private bool pvDeInspector = false;

    private bool iniciouLuta;
    private CreatureManager cm;
    private EncounterTriggerManager tutorManager;

    public EncounterTriggerManager TutorManager
    {
        get { return tutorManager; }
        set { tutorManager = value; }
    }

    public string Chave
    {
        get { return chave; }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !GameController.g.MyKeys.VerificaAutoShift(chave))
        {
            cm = InsereInimigoEmCampo.RetornaInimigoEmCampo(C);

            if (golpeDeInspector)
                for(int i=0;i<C.GerenteDeGolpes.meusGolpes.Count;i++)
                cm.MeuCriatureBase.GerenteDeGolpes.meusGolpes[i] = PegaUmGolpeG2.RetornaGolpe(C.GerenteDeGolpes.meusGolpes[i].Nome);

            if (pvDeInspector)
            {
                cm.MeuCriatureBase.CaracCriature.meusAtributos.PV.Maximo = C.CaracCriature.meusAtributos.PV.Maximo;
                cm.MeuCriatureBase.CaracCriature.meusAtributos.PV.Corrente = C.CaracCriature.meusAtributos.PV.Corrente;
            }

            GameController.g.EncontroAgoraCom(cm);
            tutorManager.IniciouLuta(cm, this);
        }
        else if (GameController.g.MyKeys.VerificaAutoShift(chave))
            gameObject.SetActive(false);
    }
}
