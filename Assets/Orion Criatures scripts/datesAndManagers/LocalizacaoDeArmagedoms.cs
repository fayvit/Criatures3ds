using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LocalizacaoDeArmagedoms
{
    private static Dictionary<IndiceDeArmagedoms, VisitasParaArmagedom> l = new Dictionary<IndiceDeArmagedoms, VisitasParaArmagedom>()
    {

        { IndiceDeArmagedoms.cavernaIntro, new VisitasParaArmagedom() {
            Endereco = new Vector3(-184f, 11, 150),
            nomeDasCenas = new NomesCenas[1]{NomesCenas.cavernaIntro},
            DirRotation = "s"
        } },
        { IndiceDeArmagedoms.deKatids, new VisitasParaArmagedom() {
            Endereco = new Vector3(761, 1.2f, 1872),
            nomeDasCenas = new NomesCenas[2]{NomesCenas.katidsTerrain,NomesCenas.katidsVsTempleZone }
        } },
        { IndiceDeArmagedoms.miniKatidsVsTemple, new VisitasParaArmagedom() {
            Endereco = new Vector3(530,1f,2540),
            nomeDasCenas = new NomesCenas[3]{
                NomesCenas.TempleZone,
                NomesCenas.katidsVsTempleZone,
                NomesCenas.TempleZoneVsMarjan }
        } },
        { IndiceDeArmagedoms.Marjan, new VisitasParaArmagedom() {
            Endereco = new Vector3(580,-49f,3360),
            nomeDasCenas = new NomesCenas[2]{
                NomesCenas.Marjan,
                NomesCenas.TempleZoneVsMarjan }
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
            return new Vector3(endX,endY,endZ);
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
        get {
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
            return retorno; }
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
    deKatids,
    saidaDaCaverna,
    miniKatidsVsTemple,
    Marjan
}
