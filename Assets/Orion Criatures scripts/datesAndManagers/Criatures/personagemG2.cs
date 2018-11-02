using UnityEngine;
using System.Collections.Generic;

public static class personagemG2
{
    public static CriatureBase RetornaUmCriature(nomesCriatures nome)
    {
        CriatureBase retorno;
        switch (nome)
        {
            case nomesCriatures.Xuash:
                retorno = XuashG2.Criature;
            break;
            case nomesCriatures.Florest:
                retorno = FlorestG2.Criature;
            break;
            case nomesCriatures.PolyCharm:
                retorno = PolyCharmG2.Criature;
            break;
            case nomesCriatures.Arpia:
                retorno = ArpiaG2.Criature;
            break;
            case nomesCriatures.Iruin:
                retorno = IruinG2.Criature;
            break;
            case nomesCriatures.Urkan:
                retorno = UrkanG2.Criature;
            break;
            case nomesCriatures.Babaucu:
                retorno = MbBabaucu.Criature;
            break;
            case nomesCriatures.Serpente:
                retorno = MbSerpente.Criature;
            break;
            case nomesCriatures.Nessei:
                retorno = MbNessei.Criature;
            break;
            case nomesCriatures.Cracler:
                retorno = MbCracler.Criature;
            break;
            case nomesCriatures.Flam:
                retorno = MbFlam.Criature;
            break;
            case nomesCriatures.Rocketler:
                retorno = MbRocketler.Criature;
            break;
            case nomesCriatures.Baratarab:
                retorno = MbBaratarab.Criature;
            break;
            case nomesCriatures.Aladegg:
                retorno = MbAladegg.Criature;
            break;
            case nomesCriatures.Onarac:
                retorno = MbOnarac.Criature;
            break;
            case nomesCriatures.Marak:
                retorno = MbMarak.Criature;
            break;
            case nomesCriatures.Steal:
                retorno = MbSteal.Criature;
            break;
            case nomesCriatures.Escorpion:
                retorno = MbEscorpion.Criature;
            break;
            case nomesCriatures.Cabecu:
                retorno = MbCabecu.Criature;
            break;
            case nomesCriatures.DogMour:            
                retorno = MbDogMour.Criature;
            break;
            case nomesCriatures.Batler:
                retorno = Batler.Criature;
            break;
            case nomesCriatures.Wisks:
                retorno = Wisks.Criature;
            break;
            case nomesCriatures.Izicuolo:
                retorno = Izicuolo.Criature;
            break;
            default:
                retorno = new CriatureBase();
            break;
        }

        return retorno;
    }
   
}

public enum nomesCriatures
{
    Xuash,
    Florest,
    Marak,
    PolyCharm,
    FelixCat,
    Babaucu,
    Arpia,
    Iruin,
    Steal,
    Escorpion,
    Escorpirey,
    Aladegg,
    Onarac,
    Wisks,
    Serpente,
    Nessei,
    Rabitler,
    Rocketler,
    Cabecu,
    Vampire,
    Fajin,
    Flam,
    Croc,
    Oderc,
    Abutre,
    Urkan,
    Baratarab,
    DogMour,
    Izicuolo,
    Cracler,
    Nedak,
    Estrep,
    Batler
}
