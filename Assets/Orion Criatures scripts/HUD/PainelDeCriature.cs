using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class PainelDeCriature : MonoBehaviour
{
    [SerializeField]private RawImage imgDoPersonagem;
    [SerializeField]private Text numPV;
    [SerializeField]private Text numPE;
    [SerializeField]private Text numAtk;
    [SerializeField]private Text numDef;
    [SerializeField]private Text numPod;
    [SerializeField]private Text numXp;
    [SerializeField]private Text txtMeusTipos;
    [SerializeField]private Text txtNomeC;
    [SerializeField]private Text numNivel;    

    public void InserirDadosNoPainelPrincipal(CriatureBase C)
    {
        Atributos A = C.CaracCriature.meusAtributos;
        IGerenciadorDeExperiencia g_XP = C.CaracCriature.mNivel;

        imgDoPersonagem.texture = GameController.g.El.RetornaMini(C.NomeID);
        txtNomeC.text = C.NomeEmLinguas;
        numNivel.text = g_XP.Nivel.ToString();
        numPV.text = A.PV.Corrente + "\t/\t" + A.PV.Maximo;
        numPE.text = A.PE.Corrente + "\t/\t" + A.PE.Maximo;
        numXp.text = g_XP.XP + "\t/\t" + g_XP.ParaProxNivel;
        numAtk.text = A.Ataque.Corrente.ToString();
        numDef.text = A.Defesa.Corrente.ToString();
        numPod.text = A.Poder.Corrente.ToString();
        string paraTipos = "";

        for (int i = 0; i < C.CaracCriature.meusTipos.Length; i++)
        {
            paraTipos += tipos.NomeEmLinguas(C.CaracCriature.meusTipos[i]) + ", ";
        }

        txtMeusTipos.text = paraTipos.Substring(0, paraTipos.Length - 2);

    }

}
