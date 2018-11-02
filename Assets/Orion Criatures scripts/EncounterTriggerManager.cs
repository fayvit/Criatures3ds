using UnityEngine;
using System.Collections;

public class EncounterTriggerManager : MonoBehaviour
{
    [SerializeField] private IniciarEncontroComGerente[] encontrosLocais;

    private bool iniciouLuta = false;
    private int indiceDoEncontro = -1;
    private CreatureManager cm;

    public void IniciouLuta(CreatureManager cm, IniciarEncontroComGerente ini)
    {
        iniciouLuta = true;
        this.cm = cm;

        for (int i = 0; i < encontrosLocais.Length; i++)
        {
            if (encontrosLocais[i] == ini)
                indiceDoEncontro = i;
        }
    }

    void Start()
    {
        for (int i = 0; i < encontrosLocais.Length; i++)
        {
            encontrosLocais[i].TutorManager = this;
        }
    }

    void Update()
    {
        if (iniciouLuta)
        {
            if (cm.MeuCriatureBase.CaracCriature.meusAtributos.PV.Corrente <= 0 || cm == null)
            {
                for (int i = 0; i < encontrosLocais.Length; i++)
                {
                    GameController.g.MyKeys.MudaAutoShift(encontrosLocais[i].Chave, false);
                    encontrosLocais[i].gameObject.SetActive(true);
                }

                GameController.g.MyKeys.MudaAutoShift(encontrosLocais[indiceDoEncontro].Chave, true);
                encontrosLocais[indiceDoEncontro].gameObject.SetActive(false);
            }
        }
    }
}
