using UnityEngine;
using System.Collections;

public class ChaoQueimando : MonoBehaviour
{
    private float tempoQueimando;

    public float tempoMin = 5;
    public float tempoMax = 10;

    public nomeTipos tipoImune = nomeTipos.Fogo;
    public string noImpacto = "impactoDeFogo";

    void Start()
    {
        tempoQueimando = Random.Range(5, 10);

        Destroy(gameObject, tempoQueimando);
    }

    void OnTriggerEnter(Collider emQ)
    {
        if (emQ.tag == "Criature" || emQ.tag == "Player")
        {
            bool dano = true;
            bool deFogo = false;
            if (emQ.tag == "Criature")
            {
                nomeTipos[] Tipos = emQ.transform.GetComponent<CreatureManager>().MeuCriatureBase.CaracCriature.meusTipos;
                for (int i = 0; i < Tipos.Length; i++)
                {
                    if (Tipos[i] == tipoImune)
                    {
                        dano = false;
                        deFogo = true;
                    }
                }
            }
            else
                dano = false;

            if (!deFogo)
            {
                GameObject G = GameController.g.El.retorna(noImpacto);
                G = (GameObject)Instantiate(G, emQ.transform.position, Quaternion.identity);

                Destroy(G, 0.75f);
            }

            if (dano)
            {
                Dano.VerificaDano(emQ.gameObject, emQ.gameObject, new GolpeBase(new ContainerDeCaracteristicasDeGolpe()));
            }

            if (!deFogo)
                Destroy(gameObject);
        }
    }
}