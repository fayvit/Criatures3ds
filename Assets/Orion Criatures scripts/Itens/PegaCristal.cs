using UnityEngine;
using System.Collections;

public class PegaCristal : MonoBehaviour
{
    [SerializeField] private int valor = 1;
    [SerializeField] private string ID = "0";

    // Use this for initialization
    void Start()
    {
        if (Application.isPlaying)
        {
            if (ExistenciaDoController.AgendaExiste(Start, this))
            {


                if (GameController.g.MyKeys.VerificaCristalShift(ID))
                    Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnValidate()
    {
        BuscadorDeID.Validate(ref ID, this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Application.isPlaying)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Criature")
            {
                EventAgregator.Publish(new StandardSendStringEvent(gameObject, SoundEffectID.XP_Heal01.ToString(), EventKey.disparaSom));
                GameController g = GameController.g;
                GameObject G = g.El.retorna(DoJogo.pegueiCristal);

                Destroy(Instantiate(G, transform.position + 1f * Vector3.up, G.transform.rotation), 5);

                g.Manager.Dados.Cristais += valor;
                g.MyKeys.MudaCristalShift(ID);

                if (g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto))
                    g.HudM.AtualizeImagemDeAtivos();

                Destroy(gameObject);
            }
        }
    }
}
