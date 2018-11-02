using UnityEngine;
using System.Collections;

public class Envenenado : StatusTemporarioBase
{
    private float tempoAcumulado = 0;
    private EstadoDaqui estado = EstadoDaqui.tempoCorrente;
    private ApresentaDerrota apresentaDerrota;

    private enum EstadoDaqui
    {
        tempoCorrente,
        derrotadoAtivo,
        derrotadoInterno,
        morreuEnvenenadoAtivo,
        emEspera
    }

    // Use this for initialization
    public override void Start()
    {
        if(CDoAfetado!=null)
            ColocaAParticulaEAdicionaEsseStatus(DoJogo.particulasEnvenenado.ToString(), CDoAfetado.transform);

    }

    // Update is called once per frame
    public override void Update()
    {
        switch(estado)
        {
            case EstadoDaqui.tempoCorrente:

                if(PodeContarTempo())
                    tempoAcumulado += Time.deltaTime;

                if (tempoAcumulado >= Dados.TempoSignificativo && OAfetado.CaracCriature.meusAtributos.PV.Corrente > 0)
                {
                    Debug.Log(CDoAfetado);
                    if (CDoAfetado != null)
                    {
                        Animator A = CDoAfetado.GetComponent<Animator>();

                        Dano.EmEstadoDeDano(A, CDoAfetado);
                        Dano.InsereEstouEmDano(CDoAfetado, A, new GolpeBase(new ContainerDeCaracteristicasDeGolpe() { }));
                        Dano.AplicaVisaoDeDano(CDoAfetado, (int)Dados.Quantificador);

                        if (CDoAfetado.name == "CriatureAtivo")
                            GameController.g.HudM.Painel.AtivarNovaMens(
                                string.Format(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.status)[1],
                                OAfetado.NomeEmLinguas, (int)Dados.Quantificador), 20, 2
                                );
                        else
                            GameController.g.HudM.Painel.AtivarNovaMens(
                                string.Format(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.status)[2],
                                (int)Dados.Quantificador), 20, 2
                                );
                    }
                    else
                    {
                        GameController.g.HudM.Painel.AtivarNovaMens(
                                string.Format(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.status)[1],
                                OAfetado.NomeEmLinguas, (int)Dados.Quantificador), 20, 2
                                );
                    }

                    Dano.AplicaCalculoDoDano(OAfetado.CaracCriature.meusAtributos, (int)Dados.Quantificador);


                    GameController.g.HudM.AtualizaDadosDaHudVida(false);

                    if (GameController.g.InimigoAtivo != null)
                        GameController.g.HudM.AtualizaDadosDaHudVida(true);

                    


                    VerificaVida();
                    tempoAcumulado = 0;
                }
                else if (OAfetado.CaracCriature.meusAtributos.PV.Corrente <= 0)
                {
                    RetiraComponenteStatus();

                    if (CDoAfetado != null)
                        MudaParaEstadoMorto();
                }
            break;
            case EstadoDaqui.derrotadoAtivo:
                tempoAcumulado += Time.deltaTime;
                if (tempoAcumulado > 2 || CommandReader.ButtonDown(0, GameController.g.Manager.Control))
                {
                    Debug.Log("disparaacao");
                    apresentaDerrota = new ApresentaDerrota(GameController.g.Manager, CDoAfetado);
                    estado = EstadoDaqui.morreuEnvenenadoAtivo;
                }

            break;
            case EstadoDaqui.morreuEnvenenadoAtivo:
                ApresentaDerrota.RetornoDaDerrota R = apresentaDerrota.Update();
                if (R != ApresentaDerrota.RetornoDaDerrota.atualizando)
                {
                    if (R == ApresentaDerrota.RetornoDaDerrota.voltarParaPasseio)
                    {
                        GameController.g.Manager.AoHeroi();
                        RetiraComponenteStatus();
                        estado = EstadoDaqui.emEspera;
                    }
                    else
                    if (R == ApresentaDerrota.RetornoDaDerrota.deVoltaAoArmagedom)
                    {
                        
                    }

                    
                    
                }
                
            break;
        }
    }

    bool PodeContarTempo()
    {
        bool retorno = true;
        if (CDoAfetado != null)
        {
            if (CDoAfetado.Estado == CreatureManager.CreatureState.parado
                ||
                CDoAfetado.Estado == CreatureManager.CreatureState.emDano
                ||
                CDoAfetado.Estado == CreatureManager.CreatureState.morto
                ||
                CDoAfetado.Estado == CreatureManager.CreatureState.parado
                ||
                CDoAfetado.Estado == CreatureManager.CreatureState.aplicandoGolpe
                )
                retorno = false;
        }

        return retorno;
    }

    void VerificaVida()
    {
        if (!GameController.g.estaEmLuta)
        {
            if (OAfetado.CaracCriature.meusAtributos.PV.Corrente <= 0)
            {
                if (CDoAfetado != null)
                {
                    GameController.g.Manager.Estado = EstadoDePersonagem.parado;
                    estado = EstadoDaqui.derrotadoAtivo;

                    MudaParaEstadoMorto();

                    MonoBehaviour.Destroy(Particula.gameObject);
                }
                else
                {
                    estado = EstadoDaqui.derrotadoInterno;
                }

                GameController.g.HudM.Painel.AtivarNovaMens(
                                string.Format(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.status)[0],
                                OAfetado.NomeEmLinguas), 20, 5
                                );              

            }
        }
    }

    void MudaParaEstadoMorto()
    {
        CDoAfetado.GetComponent<Animator>().SetBool("cair", true);
        CDoAfetado.Estado = CreatureManager.CreatureState.morto;
    }
}
