using UnityEngine;
using System.Collections;

public class AuxiliarDeInstancia
{

    public static GameObject InstancieEDestrua(nomesGolpes nomeGolpe,
                                                Vector3 posInicial,
                                                Vector3 forwardInicial,
                                                float tempoDeGolpe)
    {
        return InstancieEDestrua(nomeGolpe.ToString(), posInicial, forwardInicial, tempoDeGolpe);
    }

    private static GameObject InstancieEDestrua(string nome,
                                                Vector3 posInicial,
                                                Vector3 forwardInicial,
                                                float tempoDeGolpe)
    {
        GameObject golpeX = GameController.g.El.retorna(nome);

        golpeX = (GameObject)MonoBehaviour.Instantiate(golpeX, posInicial, Quaternion.LookRotation(forwardInicial));

        MonoBehaviour.Destroy(golpeX, tempoDeGolpe);

        return golpeX;
    }

    private static GameObject InstancieEDestrua(string nome,Vector3 posInicial,float tempoDeGolpe)
    {
        return InstancieEDestrua(nome,posInicial,Vector3.forward,tempoDeGolpe);
    }

    public static GameObject InstancieEDestrua(DoJogo nome, Vector3 posInicial, float tempoDeGolpe)
    {
        return InstancieEDestrua(nome.ToString(), posInicial, tempoDeGolpe);
    }

    public static GameObject InstancieEDestrua(DoJogo nome, Vector3 posInicial,Vector3 forwardInicial, float tempoDeGolpe)
    {
        return InstancieEDestrua(nome.ToString(), posInicial, forwardInicial, tempoDeGolpe);
    }
}
