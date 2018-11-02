using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InformacoesDeCarregamento
{
    public static void FacaModificacoes()
    {
        NomesCenas nomeDaCena = (NomesCenas)System.Enum.Parse(typeof(NomesCenas),
            SceneManager.GetActiveScene().name
            );

        switch (nomeDaCena)
        {
            case NomesCenas.petrolifera:
            case NomesCenas.esgoto:
                GameObject.Find("Directional Light").GetComponent<Light>().intensity = 0;
            break;
        }
    }
}
