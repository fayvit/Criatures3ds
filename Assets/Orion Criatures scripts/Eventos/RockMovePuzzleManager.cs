using UnityEngine;
using System.Collections;

public class RockMovePuzzleManager : AtivadorDeBotao
{
    [SerializeField]private RockShift[] rockShift;
    [SerializeField]private Vector3 posDeEscondido = Vector3.zero;
    [SerializeField]private GameObject particulaDeFim;
    [SerializeField]private KeyShift chaveEspecial;
    [SerializeField]private string chave;    
    [SerializeField]private float tempoParaEsconder = 3;

    [SerializeField]private bool iniciarVisaoDeFeito = false;
    private Vector3 posInicial;
    private float contadorDeTempo = 0;
    
    // Use this for initialization
    void Start()
    {
        textoDoBotao = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.textoBaseDeAcao)[1];
        
        if (ExistenciaDoController.AgendaExiste(Start, this))
            {
            if (GameController.g.MyKeys.VerificaAutoShift(chave))
            {
                for (int i = 0; i < rockShift.Length; i++)
                {
                    rockShift[i].rock.transform.position = new melhoraPos().novaPos(rockShift[i].target.position) + Vector3.up;
                    gameObject.SetActive(false);
                }
            }
            else
            {
                if (posDeEscondido == Vector3.zero)
                    posDeEscondido = new Vector3(transform.position.x, -2.5f, transform.position.z);

                for (int i = 0; i < rockShift.Length; i++)
                {
                    rockShift[i].posOriginal = rockShift[i].rock.transform.position;
                    rockShift[i].rock.GetComponent<RockMovePuzzle>().RockManager = this;
                }

                posInicial = transform.position;
            }
        }
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (GameController.g.HudM.Menu_Basico.EstaAtivo)
        {
            GameController.g.HudM.Menu_Basico.MudarOpcao();
        }

        if (iniciarVisaoDeFeito)
        {
            contadorDeTempo += Time.deltaTime;
            transform.position = Vector3.Lerp(posInicial,posDeEscondido,contadorDeTempo/tempoParaEsconder);

            if (contadorDeTempo > tempoParaEsconder)
            {

                GameController.g.Manager.AoHeroi();
                //GameController.g.HudM.ligarControladores();
                //AndroidController.a.LigarControlador();
                GameController.g.MyKeys.MudaAutoShift(chave, true);
                GameController.g.MyKeys.MudaShift(chaveEspecial, true);
                FinalizaEspecifico();
                gameObject.SetActive(false);
            }
        }
    }

    protected virtual void FinalizaEspecifico() { }

    public bool VerificaTargetOcupado(Transform target,Transform rock)
    {
        for (int i = 0; i < rockShift.Length; i++)
        {
            if (rockShift[i].target == target)
            {
                int valor = rockShift[i].rockInTheTarget;
                if (valor == -1)
                {
                    int indice = 0;
                    for (int j = 0; j < rockShift.Length; j++)
                    {
                        if (rock == rockShift[i].rock.transform)
                            indice = i;
                    }
                    rockShift[i].rockInTheTarget = indice;
                    return true;
                }
                else
                    return false;
            }
        }
        return true;
    }

    public bool TodosFeitos() {
        bool retorno = true;

        for (int i = 0; i < rockShift.Length; i++)
            retorno &= rockShift[i].rock.Feito;

        if (retorno)
        {
            AplicadorDeCamera.cam.NovoFocoBasico(transform.parent, 10, 10,true,true);
            iniciarVisaoDeFeito = retorno;
            particulaDeFim.SetActive(true);
        }

        return retorno;
    }

    public void Realocar(RockMovePuzzle rock)
    {
        for (int i = 0; i < rockShift.Length; i++)
        {
            if (rock == rockShift[i].rock)
            {
                rock.transform.position = new melhoraPos().novaPos(rockShift[i].target.position) + Vector3.up;
                rockShift[i].rockInTheTarget = i;
            }
        }
    }

    public override void FuncaoDoBotao()
    {
        FluxoDeBotao();GameController.g.HudM.Painel.AtivarNovaMens(
            BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.resetPuzzle),25
            );
        GameController.g.HudM.Menu_Basico.IniciarHud(Respostas,
            BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.simOuNao).ToArray());

        ActionManager.ModificarAcao(transform, 
            () => {
                Respostas(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
            });
    }

    void Respostas(int indice)
    {
        switch (indice)
        {
            case 0:
                RespondeuSim();
            break;
            case 1:
                RetornarAoFluxoDeJogo();
            break;
        }
    }

    void RespondeuSim()
    {
        for (int i = 0; i < rockShift.Length; i++)
        {
            Destroy(
                Instantiate(GameController.g.El.retorna(DoJogo.particulaDasPedraPuzzle),
                rockShift[i].rock.transform.position,
                Quaternion.identity
                ),5);
            Destroy(
                Instantiate(GameController.g.El.retorna(DoJogo.particulaDasPedraPuzzle),
                rockShift[i].posOriginal,
                Quaternion.identity
                ),5);
            rockShift[i].rock.transform.position = rockShift[i].posOriginal;
            rockShift[i].rock.RestauraShift();
            rockShift[i].rockInTheTarget = -1;
        }
        Invoke("RetornarAoFluxoDeJogo",0.75f);

        Destroy(
                Instantiate(GameController.g.El.retorna(DoJogo.particulaDasPedraPuzzle),
                GameController.g.Manager.transform.position,
                Quaternion.identity
                ), 5);

        GameController.g.HudM.Menu_Basico.FinalizarHud();
        ActionManager.ModificarAcao(transform, null);
    }

    void RetornarAoFluxoDeJogo()
    {
        ActionManager.ModificarAcao(transform, null);
        GameController.g.Manager.AoHeroi();
        /*AndroidController.a.LigarControlador();
        GameController.g.HudM.ligarControladores();*/
        GameController.g.HudM.Menu_Basico.FinalizarHud();
        GameController.g.HudM.Painel.EsconderMensagem();
        
    }
}

[System.Serializable]
public class RockShift
{
    [HideInInspector]public Vector3 posOriginal;

    public RockMovePuzzle rock;
    public Transform target;    
    public int rockInTheTarget = -1;
}
