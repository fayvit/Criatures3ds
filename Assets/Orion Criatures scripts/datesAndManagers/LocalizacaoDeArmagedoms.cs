using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LocalizacaoDeArmagedoms
{
    private static Dictionary<IndiceDeArmagedoms, VisitasParaArmagedom> l = new Dictionary<IndiceDeArmagedoms, VisitasParaArmagedom>()
    {
        
        { IndiceDeArmagedoms.cavernaIntro, new VisitasParaArmagedom() {
            Endereco = new Vector3(145, 5.5f, 180),
            nomeDasCenas = new NomesCenas[1]{NomesCenas.cavernaIntro}
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
