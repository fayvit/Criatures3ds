using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LocalizacaoDeArmagedoms
{
    private static Dictionary<IndiceDeArmagedoms, VisitasParaArmagedom> l = new Dictionary<IndiceDeArmagedoms, VisitasParaArmagedom>()
    {

        { IndiceDeArmagedoms.cavernaIntro, new VisitasParaArmagedom() {
            Endereco = new Vector3(-87f, 11, 325),
            nomeDasCenas = new NomesCenas[1]{NomesCenas.cavernaIntro}
        } },
        { IndiceDeArmagedoms.planicieDeInfinity_b, new VisitasParaArmagedom() {
            Endereco = new Vector3(166, 1.2f, 322),
            nomeDasCenas = new NomesCenas[11]{
                NomesCenas.planicieDeInfinity_b,
                NomesCenas.infinity_A,
                NomesCenas.infinity_B,
                NomesCenas.infinity_C,
                NomesCenas.infinity_D,
                NomesCenas.infinity_EF,
                NomesCenas.infinity_G,
                NomesCenas.infinity_H,
                NomesCenas.infinity_I,
                NomesCenas.infinity_J,
                NomesCenas.infinityVsRepresa
            }
        } },
        { IndiceDeArmagedoms.secretoDaRepresa, new VisitasParaArmagedom() {
            Endereco = new Vector3(590, 2f, 97),
            nomeDasCenas = new NomesCenas[6]{
                NomesCenas.represaTopCam,
                NomesCenas.represaTopCam_C,
                NomesCenas.represaTopCam_B,
                NomesCenas.represaTopCam_A,
                NomesCenas.infinityVsRepresa,
                NomesCenas.represaVsFortaleza
            }
        } },
        { IndiceDeArmagedoms.Marjan, new VisitasParaArmagedom() {
            Endereco = new Vector3(580,-49f,3360),
            nomeDasCenas = new NomesCenas[2]{
                NomesCenas.Marjan,
                NomesCenas.TempleZoneVsMarjan
            }
        } }
    };

    public static Dictionary<IndiceDeArmagedoms, VisitasParaArmagedom> L
    {
        get { return l; }
    }
}

public class VisitasParaArmagedom
{
    private float endX = 0;
    private float endY = 0;
    private float endZ = 0;
    private string dir = "n";
    public NomesCenas[] nomeDasCenas = new NomesCenas[1] { NomesCenas.cavernaIntro };

    public Vector3 Endereco
    {
        get
        {
            return new Vector3(endX, endY, endZ);
        }

        set
        {
            Vector3 V = value;
            endX = V.x;
            endY = V.y;
            endZ = V.z;
        }
    }

    public Quaternion Rot
    {
        get
        {
            Debug.Log(dir);
            Quaternion retorno = Quaternion.identity;
            switch (dir)
            {
                case "s":
                    retorno = Quaternion.LookRotation(new Vector3(0, 0, -1));
                    break;
                case "n":
                    retorno = Quaternion.LookRotation(new Vector3(0, 0, 1));
                    break;
                case "l":
                    retorno = Quaternion.LookRotation(new Vector3(1, 0, 0));
                    break;
                case "o":
                    retorno = Quaternion.LookRotation(new Vector3(-1, 0, 0));
                    break;
            }
            return retorno;
        }
    }

    public string DirRotation
    {
        get { return dir; }
        set { dir = value; }
    }


    public static string NomeEmLinguas(IndiceDeArmagedoms i)
    {
        return BancoDeTextos.nomesArmagedoms[BancoDeTextos.linguaChave][i];
    }
}


public enum IndiceDeArmagedoms
{
    // Registrar no nome em linguas
    cavernaIntro,
    daCavernaInicial,
    planicieDeInfinity_b,
    secretoDaRepresa,
    miniKatidsVsTemple,
    Marjan
}
