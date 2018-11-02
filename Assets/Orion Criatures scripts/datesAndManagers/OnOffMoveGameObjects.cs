using UnityEngine;
using System.Collections;

public class OnOffMoveGameObjects : MonoBehaviour
{

    [SerializeField] private Transform mudavel;
    [SerializeField] private MoverEntre[] mover;
    [SerializeField] private AcaoEntre[] desligarEntre;
    [SerializeField] private AcaoEntre[] ligarEntre;

    // Use this for initialization
    void Start()
    {
        if (mudavel == null)
            mudavel = transform;

        if (ExistenciaDoController.AgendaExiste(Start, this))
        {

            for (int i = 0; i < mover.Length; i++)
                mover[i].VerificaMudarPos(mudavel);

            for (int i = 0; i < desligarEntre.Length; i++)
            {
                if (desligarEntre[i].Pode())
                    gameObject.SetActive(false);
            }

            for (int i = 0; i < ligarEntre.Length; i++)
            {
                if (ligarEntre[i].Pode())
                    gameObject.SetActive(true);
            }
        }
    }
}
[System.Serializable]
public struct MoverEntre
{
    [SerializeField] private Transform pos;
    [SerializeField] private AcaoEntre essaAcao;

    public void VerificaMudarPos(Transform T)
    {
        if (essaAcao.Pode())
            T.position = pos.position;
    }
}

[System.Serializable]
public struct AcaoEntre
{
    [SerializeField] private bool aposShift;
    [SerializeField] private KeyShift shiftDeApos;
    [SerializeField] private bool antesDeShift;
    [SerializeField] private KeyShift shiftDeAntes;

    [SerializeField] private bool aposAutoShift;
    [SerializeField] private string autoDeApos;
    [SerializeField] private bool antesDeAutoShift;
    [SerializeField] private string autoDeAntes;

    public bool Pode()
    {
        bool retorno = false;
        KeyVar keyVar = GameController.g.MyKeys;
        if (aposShift)
            retorno = keyVar.VerificaAutoShift(shiftDeApos);

        if (aposAutoShift)
            retorno = keyVar.VerificaAutoShift(autoDeApos);

        if (!aposShift && !aposAutoShift && (antesDeShift || antesDeAutoShift))
            retorno = true;

        if (antesDeShift)
            retorno &= !keyVar.VerificaAutoShift(shiftDeAntes);

        if (antesDeAutoShift)
            retorno &= !keyVar.VerificaAutoShift(autoDeAntes);


        return retorno;
    }
}
