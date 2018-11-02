using UnityEngine;
using System.Collections;

[System.Serializable]
public class MbPergaminhoDeFuga : MbItens
{
    private float disparado = 0;
    private bool fugiu = true;
    private EstadoDaqui estadoDaqui = EstadoDaqui.particulasAtivas;
    [System.NonSerialized] private Animator animator;
    [System.NonSerialized] private GameObject particula;
    [System.NonSerialized] private CharacterController controle;

    private const int LOOPS = 3;
    private const float TEMPO_DE_ANIMA_FUGA = 1.25F;

    private enum EstadoDaqui
    {
        particulasAtivas,
        rotacionando
    }

    public MbPergaminhoDeFuga(int estoque) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pergaminhoDeFuga)
    {
        valor = 250
    }
        )
    {

        Estoque = estoque;
    }

    public override void IniciaUsoDeMenu(GameObject dono)
    {
        Estado = EstadoDeUsoDeItem.emEspera;

        GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(FecharMensagem, BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.mensLuta));
    }

    public override bool AtualizaUsoDeMenu()
    {
        return false;
    }

    public override void IniciaUsoComCriature(GameObject dono)
    {
        if (GameController.g.estaEmLuta && ! GameController.g.ContraTreinador)
        {
            estadoDaqui = EstadoDaqui.particulasAtivas;
            Estado = EstadoDeUsoDeItem.emEspera;
            disparado = 0;

            Manager = GameController.g.Manager;
            RetirarUmItem(Manager, this, 1);
            animator = GameController.g.InimigoAtivo.GetComponent<Animator>();
            InicializacaoComum(dono, animator.transform);
            Estado = EstadoDeUsoDeItem.animandoBraco;
        }
        else
        {
            Estado = EstadoDeUsoDeItem.finalizaUsaItem;

            if(!GameController.g.estaEmLuta)
                GameController.g.HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.mensLuta)[0], 30, 7);
            else if(GameController.g.ContraTreinador)
                GameController.g.HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.mensLuta)[4], 30, 7);
        }
    }

    public override bool AtualizaUsoComCriature()
    {
        switch (Estado)
        {
            case EstadoDeUsoDeItem.animandoBraco:
                if (!AnimaB.AnimaTroca(true))
                {
                    Estado = EstadoDeUsoDeItem.aplicandoItem;
                    //Manager.Mov.Animador.ResetaTroca();
                    particula = AuxiliarDeInstancia.InstancieEDestrua(
                        DoJogo.particulaDaFuga,
                         GameController.g.InimigoAtivo.transform.position, 100);
                }
                break;
            case EstadoDeUsoDeItem.aplicandoItem:
                TempoDecorrido += Time.deltaTime;
                switch (estadoDaqui)
                {
                    case EstadoDaqui.particulasAtivas:
                        int arredondado = Mathf.RoundToInt(TempoDecorrido);


                        if (arredondado != disparado && arredondado < LOOPS)
                        {
                            //particulasSaiDaLuva(G.transform);
                            animator.CrossFade("dano1", 0);
                            animator.SetBool("dano1", true);
                            animator.SetBool("dano2", true);

                            disparado = arredondado;
                        }

                        if (arredondado >= LOOPS)
                        {

                            animator.SetBool("dano1", false);
                            animator.SetBool("dano2", false);
                            MonoBehaviour.Destroy(particula);

                            if (fugiu)
                            {

                                animator.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;


                                GameObject Gg = GameController.g.El.retorna("sucessoDaFuga");
                                Gg = (GameObject)MonoBehaviour.Instantiate(
                                    Gg, GameController.g.InimigoAtivo.transform.position, Quaternion.identity);
                                MonoBehaviour.Destroy(Gg, 2);
                                controle = animator.GetComponent<CharacterController>();
                                estadoDaqui = EstadoDaqui.rotacionando;
                                //MonoBehaviour.Destroy(particula);
                                TempoDecorrido = 0;
                            }
                            else
                            {

                                GameObject Gg = GameController.g.El.retorna("encontro");
                                Gg = (GameObject)MonoBehaviour.Instantiate(
                                    Gg, GameController.g.InimigoAtivo.transform.position, Quaternion.identity);
                                Gg.GetComponent<ParticleSystem>().GetComponent<Renderer>().material
                                    = GameController.g.El.materiais[0];
                                MonoBehaviour.Destroy(Gg, 2);
                                Estado = EstadoDeUsoDeItem.finalizaUsaItem;
                            }
                        }
                        break;
                    case EstadoDaqui.rotacionando:
                        if (TempoDecorrido < TEMPO_DE_ANIMA_FUGA)
                        {
                            Transform transform = GameController.g.InimigoAtivo.transform;
                            transform.Rotate(1000 * Time.deltaTime, 0, 0);
                            controle.Move((Camera.main.transform.position - transform.position) * Time.deltaTime * 2);
                        }
                        else
                        {
                            GameController.g.RetornarParaFluxoDoHeroi();
                            Manager.Mov.Animador.ResetaTroca();
                            Estado = EstadoDeUsoDeItem.finalizaUsaItem;
                        }
                        break;
                }

                break;
            case EstadoDeUsoDeItem.finalizaUsaItem:
                return false;
        }

        return true;
    }

    public override void IniciaUsoDeHeroi(GameObject dono)
    {
        Estado = EstadoDeUsoDeItem.finalizaUsaItem;
        GameController.g.HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.mensLuta), 30, 7);
    }

    public override bool AtualizaUsoDeHeroi()
    {
        return false;
    }
}
