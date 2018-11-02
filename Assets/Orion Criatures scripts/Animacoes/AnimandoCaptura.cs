using UnityEngine;
using System.Collections;

public class AnimandoCaptura
{
    private GameObject CriatureAlvoDoItem;
    private Animator animator;
    private FaseDoAnimaCaptura fase = FaseDoAnimaCaptura.inicial;
    private AnimaPoseDeCaptura animaPose;
    private float tempoDecorrido = 0;
    private bool iraCapturar = false;
    private int disparado = 0;

    private const int LOOPS = 2;

    private enum FaseDoAnimaCaptura
    {
        inicial,
        cameraDoHeroi,
        animaPersonagemCapturando,
        finalizaCapturando,
        finalizaSemCapturar
    }

    public AnimandoCaptura(bool iraCapturar)
    {
        this.iraCapturar = iraCapturar;
        CriatureAlvoDoItem = GameController.g.InimigoAtivo.gameObject;
        animator = CriatureAlvoDoItem.GetComponent<Animator>();
        AplicadorDeCamera.cam.InicializaCameraExibicionista(CriatureAlvoDoItem.transform);
        
    }

    // Update is called once per frame
    public bool Update()
    {
        tempoDecorrido += Time.deltaTime;
        switch (fase)
        {
            case FaseDoAnimaCaptura.inicial:
                AplicadorDeCamera.cam.FocarPonto(10,GameController.g.InimigoAtivo.MeuCriatureBase.distanciaCameraLuta);
                int arredondado = Mathf.RoundToInt(tempoDecorrido);
                Vector3 variacao = arredondado % 2 == 1 ? Vector3.zero : new Vector3(1.5f, 1.5f, 1.5f);

                if (arredondado != disparado && arredondado < LOOPS)
                {
                    ParticulasDeSubstituicao.ParticulaSaiDaLuva(CriatureAlvoDoItem.transform.position);
                    
                    animator.CrossFade("dano1", 0);
                    animator.SetBool("dano1", true);
                    animator.SetBool("dano2", true);

                    disparado = arredondado;
                }

                if (arredondado >= LOOPS)
                {
                    if (iraCapturar)
                    {
                        PreparaFinalComCaptura();
                        fase = FaseDoAnimaCaptura.cameraDoHeroi;
                        
                    }
                    else {
                        PreparaFinalSemCaptura();
                        fase = FaseDoAnimaCaptura.finalizaSemCapturar;
                    }

                    tempoDecorrido = 0;
                }

                CriatureAlvoDoItem.transform.localScale = Vector3.Lerp(
                    CriatureAlvoDoItem.transform.localScale, variacao, Time.deltaTime);

            break;
            case FaseDoAnimaCaptura.finalizaSemCapturar:
                if (tempoDecorrido > 1)
                {
                    return false;
                }
            break;
            case FaseDoAnimaCaptura.cameraDoHeroi:
                if (tempoDecorrido > 1.5f)
                {
                    AplicadorDeCamera.cam.InicializaCameraExibicionista(GameController.g.Manager.transform);
                    fase = FaseDoAnimaCaptura.animaPersonagemCapturando;
                    tempoDecorrido = 0;
                }
            break;
            case FaseDoAnimaCaptura.animaPersonagemCapturando:
                if (tempoDecorrido > 1)
                {
                    animaPose = new AnimaPoseDeCaptura(GameController.g.InimigoAtivo.MeuCriatureBase);
                    MonoBehaviour.Destroy(GameController.g.InimigoAtivo.gameObject);
                    fase = FaseDoAnimaCaptura.finalizaCapturando;                    
                }
            break;
            case FaseDoAnimaCaptura.finalizaCapturando:
                if (!animaPose.Update()) 
                {
                    return false;
                }
            break;
            
        }

        return true;
    }

    void PreparaFinalComCaptura()
    {
        GameController.g.HudM.ModoCriature(false);
        
        Vector3 maoDoHeroi = GameController.g.Manager.transform
            .Find("metarig/hips/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/palm_02_L")
                .transform.position;
        CriatureAlvoDoItem.transform.localScale = Vector3.zero;

        MonoBehaviour.Destroy(
        ParticulasDeSubstituicao.InsereParticulaDoRaio(CriatureAlvoDoItem.transform.position, maoDoHeroi),2.5f);
        fase = FaseDoAnimaCaptura.cameraDoHeroi;
    }

    void PreparaFinalSemCaptura()
    {
        ParticulasDeSubstituicao.ParticulaSaiDaLuva(CriatureAlvoDoItem.transform.position, DoJogo.encontro);
        CriatureAlvoDoItem.transform.localScale = new Vector3(1, 1, 1);
        animator.SetBool("dano1", false);
        animator.SetBool("dano2", false);

        GameController.g.HudM.Painel.AtivarNovaMens(
           GameController.g.InimigoAtivo.MeuCriatureBase.NomeEmLinguas + BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.tentaCapturar),
            24,5);

    }
}
