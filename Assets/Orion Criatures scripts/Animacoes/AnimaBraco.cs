using UnityEngine;
using System.Collections;

/// <summary>
/// Classe responsavel pela animação de uso de item e envio de criatures
/// </summary>
public class AnimaBraco
{
    private EstadoDoAnimaBraco estado = EstadoDoAnimaBraco.manipulandoCamera;
    private EstadoDoAnimaEnvia estadoEnvia = EstadoDoAnimaEnvia.iniciaAnimacao;
    private AplicadorDeCamera mCamera;
    private AnimadorHumano animador;
    private GameObject luz;
    private GameObject raio;
    private GameObject C;
    private GameObject alvo;
    private Transform oAnimado;
    private Vector3 posCriature = Vector3.zero;
    private bool treinador = false;
    private float tempoDecorrido = 0;

    private const float VELOCIDADE_DE_MOVIMENTO_DE_CAMERA = 0.05F;
    private const float TEMPO_PARA_INSTANCIAR_PARTICULA_CHAO = 0.5F;
    private const float TEMPO_PARA_FINALISAR_RAIO = 0.25F;
    private const float TEMPO_DE_REDUCAO_DE_ESCALA = 1f;
    private const float VELOCIDADE_MINIMA_PARA_MOVIMENTO_DE_CAMERA = 5;

//    private const float TEMPO_DA_ANIMA_ENVIA = 3.75F;
//    private const float TEMPO_DA_ANIMA_ENVIA_TREINADOR = 1.75F;
    private const float TEMPO_PARA_INSTANCIAR_CRIATURE = 1F;

    public AnimaBraco(Transform oAnimado, Transform alvo,bool treinador = false)
    {
        this.treinador = treinador;
        this.oAnimado = oAnimado;
        mCamera = AplicadorDeCamera.cam;
        mCamera.InicializaCameraExibicionista(oAnimado, 2.5f);
        animador = (treinador)
            ?new AnimadorHumano(oAnimado.GetComponent<Animator>())
            : GameController.g.Manager.Mov.Animador;
        oAnimado.rotation = Quaternion.LookRotation(
            Vector3.ProjectOnPlane(alvo.position - oAnimado.position, Vector3.up)
            );
        this.alvo = alvo.gameObject;
        PosCriature = alvo.position;
    }

    private enum EstadoDoAnimaEnvia
    {
        iniciaAnimacao,
        animaEnvia,
        Instancia,
        AumentaEscala,
        finalizaEnvia
    }

    private enum EstadoDoAnimaBraco
    {
        manipulandoCamera,
        animaTroca,
        AnimandoTroca,
        InsereRaioDeLuva,
        DiminuiEscalaDoCriature,
        TerminaORaio,
        AnimaBracoFinalizado
    }

    public Vector3 PosCriature
    {
        set { posCriature = value; }
    }

    public bool AnimaEnvia(
        CriatureBase oInstanciado = null,
        string nomeDoGameObject = "")
    {
        tempoDecorrido += Time.deltaTime;
        switch (estadoEnvia)
        {
            case EstadoDoAnimaEnvia.iniciaAnimacao:
                tempoDecorrido = 0;
                animador.AnimaEnvia();
                estadoEnvia = EstadoDoAnimaEnvia.animaEnvia;
            break;
            case EstadoDoAnimaEnvia.animaEnvia:
                //if (tempoDecorrido > ((treinador)?TEMPO_DA_ANIMA_ENVIA_TREINADOR:TEMPO_DA_ANIMA_ENVIA))
                AnimatorStateInfo A = oAnimado.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                if(A.IsName("enviaCriature")&&A.normalizedTime>=1)
                {
                    luz = ParticulasDeSubstituicao.InsereParticulaDaLuva(oAnimado.gameObject, false);
                    raio = ParticulasDeSubstituicao.InsereParticulaDoRaio(posCriature, oAnimado.gameObject, false);
                    estadoEnvia = EstadoDoAnimaEnvia.Instancia;
                    tempoDecorrido = 0;
                }
            break;
            case EstadoDoAnimaEnvia.Instancia:
                if (tempoDecorrido > TEMPO_PARA_INSTANCIAR_CRIATURE)
                {
                    if (oInstanciado == null && !treinador)
                        oInstanciado = GameController.g.Manager.Dados.CriaturesAtivos[0];

                    if (!treinador)
                    {
                        C = GameController.g.El.retorna(oInstanciado.NomeID);
                        posCriature = MelhoraInstancia.ProcuraPosNoMapa(posCriature);
                        C = (GameObject)MonoBehaviour.Instantiate(C, posCriature, Quaternion.identity);

                        C.transform.parent = GameController.g.transform;
                    }

                    Debug.Log(nomeDoGameObject + " : " + treinador);
                    if (nomeDoGameObject == "" && !treinador)
                        InicializadorDoJogo.InsereCriatureEmJogo(C, GameController.g.Manager);
                    else if (treinador)
                    {
                        Debug.Log("inimigo");
                        C = InsereInimigoEmCampo.RetornaInimigoEmCampo(oInstanciado).gameObject;
                        posCriature = MelhoraInstancia.ProcuraPosNoMapa(posCriature);
                        C.name = nomeDoGameObject != "" ? nomeDoGameObject : "Inimigo";
                    }

                    C.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    ParticulasDeSubstituicao.ParticulaSaiDaLuva(posCriature);

                    tempoDecorrido = 0;
                    estadoEnvia = EstadoDoAnimaEnvia.AumentaEscala;
                }
            break;
            case EstadoDoAnimaEnvia.AumentaEscala:
                if(!treinador)
                GameController.g.Manager.CriatureAtivo.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                if (C.transform.localScale.sqrMagnitude < 2.5f)
                {
                    C.transform.localScale = Vector3.Lerp(C.transform.localScale, new Vector3(1, 1, 1), 4 * Time.deltaTime);
                    C.transform.position = posCriature;
                }
                else
                {
                    C.transform.localScale = new Vector3(1, 1, 1);
                    estadoEnvia = EstadoDoAnimaEnvia.finalizaEnvia;
                    tempoDecorrido = 0;
                }
                break;
            case EstadoDoAnimaEnvia.finalizaEnvia:

                if (tempoDecorrido < TEMPO_PARA_FINALISAR_RAIO)
                {
                    if(!treinador)
                        GameController.g.Manager.CriatureAtivo.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                    ParticulasDeSubstituicao.ReduzVelocidadeDoRaio(raio);
                }
                else
                {
                    if (treinador)
                    {
                        animador.ResetaEnvia();
                        animador.ResetaTroca();
                    }

                    MonoBehaviour.Destroy(raio);
                    MonoBehaviour.Destroy(luz);
                    return false;
                }
                break;
        }
        return true;
    }

    public bool AnimaTroca(
        bool eItem = false
        )
    {
        GameObject gerente = oAnimado.gameObject;
        tempoDecorrido += Time.deltaTime;
        switch (estado)
        {
            case EstadoDoAnimaBraco.manipulandoCamera:
                if (mCamera.FocarPonto(VELOCIDADE_DE_MOVIMENTO_DE_CAMERA
                    * Mathf.Max(Vector3.Distance(gerente.transform.position, alvo.transform.position),
                    VELOCIDADE_MINIMA_PARA_MOVIMENTO_DE_CAMERA),6,3,true,mCamera.transform.position))
                    estado = EstadoDoAnimaBraco.animaTroca;
            break;
            case EstadoDoAnimaBraco.animaTroca:
                animador.AnimaTroca();
                estado = EstadoDoAnimaBraco.AnimandoTroca;
                tempoDecorrido = 0;
            break;
            case EstadoDoAnimaBraco.AnimandoTroca:
                //if (tempoDecorrido > TEMPO_DA_PRIMEIRA_ANIMACAO)
                AnimatorStateInfo A = oAnimado.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                
                if(A.IsName("chamaCriature") &&A.normalizedTime>=1)
                {
                    luz = ParticulasDeSubstituicao.InsereParticulaDaLuva(gerente, true);
                    raio = ParticulasDeSubstituicao.InsereParticulaDoRaio(posCriature, gerente);
                    estado = EstadoDoAnimaBraco.InsereRaioDeLuva;
                    tempoDecorrido = 0;
                }
            break;
            case EstadoDoAnimaBraco.InsereRaioDeLuva:
                if (tempoDecorrido > TEMPO_PARA_INSTANCIAR_PARTICULA_CHAO && alvo.transform.localScale.sqrMagnitude > 0.01f)
                {
                    ParticulasDeSubstituicao.ParticulaSaiDaLuva(alvo.transform.position);
                    if (!eItem)
                        estado = EstadoDoAnimaBraco.DiminuiEscalaDoCriature;
                    else
                    {
                       
                        mCamera.InicializaCameraExibicionista(alvo.transform, 2*alvo.GetComponent<CharacterController>().height);
                        
                        
                        estado = EstadoDoAnimaBraco.TerminaORaio;
                    }
                    tempoDecorrido = 0;
                }
                break;
            case EstadoDoAnimaBraco.DiminuiEscalaDoCriature:
                if (tempoDecorrido < TEMPO_DE_REDUCAO_DE_ESCALA)
                {
                    alvo.transform.localScale = Vector3.Lerp(alvo.transform.localScale, Vector3.zero, 2 * Time.deltaTime);
                }
                else
                {                    
                    estado = EstadoDoAnimaBraco.TerminaORaio;
                    tempoDecorrido = 0;
                }
                break;
            case EstadoDoAnimaBraco.TerminaORaio:
                if (tempoDecorrido < TEMPO_PARA_FINALISAR_RAIO)
                {
                    if (!eItem)
                        MonoBehaviour.Destroy(alvo);

                    ParticulasDeSubstituicao.ReduzVelocidadeDoRaio(raio);
                }
                else if (!eItem)
                {
                    MudarParaAnimaBracoFinalizado();
                    return false;
                }

                if (eItem)
                {                
                    if (mCamera.FocarPonto(VELOCIDADE_DE_MOVIMENTO_DE_CAMERA *
                        Mathf.Max(Vector3.Distance(gerente.transform.position, alvo.transform.position),
                    VELOCIDADE_MINIMA_PARA_MOVIMENTO_DE_CAMERA),6,3,true,mCamera.transform.position))
                    {
                        MudarParaAnimaBracoFinalizado();
                        return false;
                    }
                }
                break;
            case EstadoDoAnimaBraco.AnimaBracoFinalizado:
                return false;
                //break;
        }
        return true;
    }

    void MudarParaAnimaBracoFinalizado()
    {
        MonoBehaviour.Destroy(raio);
        MonoBehaviour.Destroy(luz);
        estado = EstadoDoAnimaBraco.AnimaBracoFinalizado;
    }
}
