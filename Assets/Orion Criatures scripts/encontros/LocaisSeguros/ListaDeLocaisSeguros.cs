using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ListaDeLocaisSeguros
{
    public static bool LocalSeguro()
    {
        bool retorno = false;
        NomesCenas nomeDaCena = NomesCenas.cavernaIntro;

        try
        {
            nomeDaCena = (NomesCenas)System.Enum.Parse(typeof(NomesCenas), SceneManager.GetActiveScene().name);
        } catch 
        {
            //Debug.Log("cena indisponivel");
        }

        switch (nomeDaCena)
        {
            case NomesCenas.katidsTerrain:
                retorno = LocaisSegurosDeKatidesTerrain.LocalSeguro();
            break;
            case NomesCenas.TempleZone:
                retorno = LocaisSegurosDeTempleZone.LocalSeguro();
            break;
            case NomesCenas.Marjan:
                retorno = LocaisSegurosDeMarjan.LocalSeguro();
            break;
        }
        return retorno;
    }
}
