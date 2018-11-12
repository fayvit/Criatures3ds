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
            Debug.Log("Algo errado ao converter nome da cena[lista de locais seguros]");
            //Debug.Log("cena indisponivel");
        }

        retorno = GetSceneConfigs.Get(nomeDaCena).LocaisSeguros();

        /*
        switch (nomeDaCena)
        {
            case NomesCenas.planicieDeInfinity:
                retorno = GetSceneConfigs.Get(NomesCenas.planicieDeInfinity).LocaisSeguros();
            break;
            case NomesCenas.TempleZone:
                retorno = LocaisSegurosDeTempleZone.LocalSeguro();
            break;
            case NomesCenas.Marjan:
                retorno = LocaisSegurosDeMarjan.LocalSeguro();
            break;
        }*/
        return retorno;
    }
}
