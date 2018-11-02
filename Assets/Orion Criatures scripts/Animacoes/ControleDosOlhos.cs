using UnityEngine;
using System.Collections;

public class ControleDosOlhos : MonoBehaviour
{

    public Vector2[] piscar;
    public Vector2[] olhosDeImpacto;
    public Vector2[] olhosDeMorto;
    public float tempoDePiscada = 0.15f;
    public float tempoFechadoNoPiscar = 0.2f;
    public int materialDosOlhos = 0;

    private estadoOlho meuOlho;
    private float tempoDosOlhos;
    private SkinnedMeshRenderer skinned;
    private Animator animator;
    // Use this for initialization

    private enum estadoOlho
    {
        aberto,
        piscar,
        impacto,
        morto,
        fechar
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        skinned = GetComponentInChildren<SkinnedMeshRenderer>();

        if (olhosDeMorto.Length == 0)
            olhosDeMorto = piscar;
    }

    void olhoAberto()
    {
        skinned.materials[materialDosOlhos].mainTextureOffset = piscar[0];
    }
    void fecharOlho()
    {
        meuOlho = estadoOlho.fechar;
        tempoDosOlhos = 0;
    }

    void abrirOlho()
    {
        meuOlho = estadoOlho.piscar;

        /*
		   Esse tempo aparentemente maluco e para aproveitar o procedimento de Piscar
			nesse tempo os olhos começam a abrir no procedimento de Piscar
		 */
        tempoDosOlhos = piscar.Length * tempoDePiscada + tempoFechadoNoPiscar;
    }

    void olhoDeMorto()
    {
        meuOlho = estadoOlho.morto;
        tempoDosOlhos = 0;
    }

    void olhoImpacto()
    {
        meuOlho = estadoOlho.impacto;
        tempoDosOlhos = 0;
    }

    void piscadelas()
    {
        meuOlho = estadoOlho.piscar;
        tempoDosOlhos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetFloat("velocidade") > 0)
            meuOlho = estadoOlho.aberto;
        switch (meuOlho)
        {
            case estadoOlho.fechar:
                procedimentoFechar(piscar);
                break;
            case estadoOlho.impacto:
                procedimentoFechar(olhosDeImpacto);
                break;
            case estadoOlho.piscar:
                procedimentoPiscar();
                break;
            case estadoOlho.morto:
                procedimentoFechar(olhosDeMorto);
                break;
            case estadoOlho.aberto:
                if (skinned.materials[materialDosOlhos].mainTextureOffset != piscar[0])
                    skinned.materials[materialDosOlhos].mainTextureOffset = piscar[0];
                break;
        }

    }

    void procedimentoPiscar()
    {
        if (tempoDosOlhos <= piscar.Length * tempoDePiscada + tempoFechadoNoPiscar)
        {
            procedimentoFechar(piscar);
        }
        else if (tempoDosOlhos >= piscar.Length * tempoDePiscada + tempoFechadoNoPiscar
                &&
                tempoDosOlhos < 2 * piscar.Length + tempoFechadoNoPiscar)
        {
            procedimentoAbrir();
        }
        else
        {
            meuOlho = estadoOlho.aberto;
        }
    }
    void procedimentoAbrir()
    {
        tempoDosOlhos += Time.deltaTime;

        for (int i = 0; i < piscar.Length; i++)
        {
            if (tempoDosOlhos > i * tempoDePiscada && tempoDosOlhos < (i + 4) * tempoDePiscada + tempoFechadoNoPiscar)
            {
                skinned.materials[materialDosOlhos].mainTextureOffset = piscar[piscar.Length - i - 1];
            }
        }
    }

    void procedimentoFechar(Vector2[] mov)
    {
        tempoDosOlhos += Time.deltaTime;

        for (int i = 0; i < mov.Length; i++)
        {
            if (tempoDosOlhos > i * tempoDePiscada && tempoDosOlhos < (i + 1) * tempoDePiscada)
            {
                skinned.materials[materialDosOlhos].mainTextureOffset = mov[i];
            }
        }
    }
}
