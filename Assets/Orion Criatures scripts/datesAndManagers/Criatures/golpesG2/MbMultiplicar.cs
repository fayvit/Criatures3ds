using UnityEngine;
using System.Collections;

[System.Serializable]
public class MbMultiplicar : GolpeBase
{
    [System.NonSerialized]private CharacterController controle;
    private CaracteristicasDeImpacto carac;
    private GolpePersonagem gP;
    private float tempoDecorrido = 0;
    private bool addView = false;
    

    public MbMultiplicar() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.multiplicar,
        tipo = nomeTipos.Inseto,
        carac = caracGolpe.projetil,
        custoPE = 3,
        potenciaCorrente = 2,
        potenciaMaxima = 4,
        potenciaMinima = 1,
        tempoDeReuso = 10f,
        tempoDeMoveMax = 1,
        tempoDeMoveMin = 0.1f,
        tempoDeDestroy = 12,
        TempoNoDano = 0.75f,
        velocidadeDeGolpe = 18,
        podeNoAr = true
    }
        )
    {
        carac = new CaracteristicasDeImpacto()
        {
            noImpacto = NoImpacto.impactoDeGosma.ToString()
        };
    }

    public override void IniciaGolpe(GameObject G)
    {
        tempoDecorrido = 0;
        addView = false;
        
        gP = GolpePersonagem.RetornaGolpePersonagem(G, Nome);
    }

    public override void UpdateGolpe(GameObject G)
    {
        tempoDecorrido += Time.deltaTime;
        MultiplicarInsetos(G);
    }

    void MultiplicarInsetos(GameObject G)
    {
        if (!addView && tempoDecorrido > gP.TempoDeInstancia)
        {
            CreatureManager C = G.GetComponent<CreatureManager>();
            GameObject G2 = GameController.g.El.retorna(C.MeuCriatureBase.NomeID);
            Vector3 pos = Vector3.zero;
            Transform alvo = CriaturesPerto.procureUmBomAlvo(G, 450);

            if (alvo)
                G.transform.rotation = Quaternion.LookRotation(
                    Vector3.ProjectOnPlane(alvo.position-G.transform.position,Vector3.up)
                    );

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        pos = G.transform.position + G.transform.forward + 2 * G.transform.right;

                    break;
                    case 1:

                        pos = G.transform.position + G.transform.forward - 2 *G.transform.right;

                    break;
                    case 2:
                        pos = G.transform.position - G.transform.forward + 3 * G.transform.right;

                    break;
                    case 3:
                        pos = G.transform.position - G.transform.forward - 3 * G.transform.right;
                    break;
                }

                G2 = MonoBehaviour.Instantiate(G2, new melhoraPos().novaPos(pos), G.transform.rotation) as GameObject;
                if (i == 0)
                {
                    G2.layer = 10;
                    G2.tag = "Untagged";
                    MbComportamentoDoMultiplicado c = G2.AddComponent<MbComportamentoDoMultiplicado>();
                    c.direcaoMovimento = pos - G.transform.position;
                    c.velocidadeProjetil = C.MeuCriatureBase.Mov.velocidadeAndando;
                    c.dono = G;
                    c.esseGolpe = this;
                    c.tempoDestroy = Mathf.Max(0.95f* TempoDeDestroy - TempoDeMoveMin,1);
                    CapsuleCollider caps = G2.AddComponent<CapsuleCollider>();
                    G2.AddComponent<Rigidbody>();
                    caps.isTrigger = true;
                    if (!controle)
                        controle = G.GetComponent<CharacterController>();
                    caps.radius = 2 * controle.radius;
                    caps.height = controle.height;
                    //					print(procureUmBomAlvo(450));
                    c.alvo = alvo;
                }
                else
                {
                    MbComportamentoDoMultiplicado mC = G2.GetComponent<MbComportamentoDoMultiplicado>();
                    mC.direcaoMovimento = pos - G.transform.position;
                    mC.esseGolpe = this;
                }
                MonoBehaviour.Destroy(
                    MonoBehaviour.Instantiate(GameController.g.El.retorna(carac.noImpacto), pos, Quaternion.identity),
                    2);

            }
            addView = true;
        }
    }

    }
