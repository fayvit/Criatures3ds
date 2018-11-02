using UnityEngine;
using System.Collections.Generic;

public class CreatureManager : MonoBehaviour
{
    [SerializeField]private CreatureState estado = CreatureState.seguindo;
    [SerializeField]private Transform tDono;
    [SerializeField]private CriatureBase meuCriatureBase;
    [SerializeField]private IA_Base ia;
    [SerializeField]private MovimentacaoBasica mov;

    //private CommandReader comandR;
    //private AndroidController controle;

    public enum CreatureState
    {
        aPasseio,
        seguindo,
        morto,
        selvagem,
        aplicandoGolpe,
        emDano,
        emLuta,
        parado
    }

    public CriatureBase MeuCriatureBase
    {
        get { return meuCriatureBase; }
        set { meuCriatureBase = value; }
    }

    public Transform TDono
    {
        get { return tDono; }
        set { tDono = value; }
    }

    public CreatureState Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    public IA_Base IA
    {
        get { return ia; }
        set { ia = value; }
    }

    public MovimentacaoBasica Mov
    {
        get {
            if (mov == null)
                SetaMov();
            return mov;

        }
    }

    public void MudaParaEstouEmDano()
    {
        estado = CreatureState.emDano;
        ia.SuspendeNav();
    }


    public bool MudaAplicaGolpe()
    {
        bool retorno = false;
        if (estado == CreatureState.aPasseio || estado==CreatureState.emLuta ||estado==CreatureState.selvagem)
        {
            estado = CreatureState.aplicandoGolpe;
            retorno = true;
        }else
            Debug.LogError("estado indefinido");

        return retorno;
    }

    public bool LiberaMovimento(CreatureState esseEstado)
    {
        if (estado == esseEstado)
        {
            //Debug.Log("libera MOvimento: "+gameObject.name);

            if (name == "CriatureAtivo" && GameController.g.Manager.Estado == EstadoDePersonagem.comMeuCriature)
                estado = GameController.g.estaEmLuta ? CreatureState.emLuta : CreatureState.aPasseio;
            else if (name == "CriatureAtivo")
                estado = CreatureState.seguindo;
            else
                estado = CreatureState.selvagem;

            return true;
        }
        else
            return false;
    }

    // Use this for initialization
    void Start()
    {
        mov = null;
        ia.Start(this);
        
        //personagemG2.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.up);
        switch (estado)
        {
            case CreatureState.parado:
            case CreatureState.emDano:
            case CreatureState.aplicandoGolpe:
                if (mov != null)
                    mov.AplicadorDeMovimentos(Vector3.zero, meuCriatureBase.CaracCriature.distanciaFundamentadora, transform);
                else
                    SetaMov();
            break;
            case CreatureState.seguindo:
            case CreatureState.selvagem:
                ia.Update();
            break;
            case CreatureState.aPasseio:
            case CreatureState.emLuta:
                Vector3 dir = Vector3.zero;
                
                int controlador = (int)GameController.g.Manager.Control;
                dir = CommandReader.VetorDirecao(controlador);
                if (estado == CreatureState.emLuta)
                {
                    dir = CommandReader.DirDeEixos(controlador);
                    dir = DirecaoInduzida(dir.x, dir.z);
                }
                else
                    dir = CommandReader.VetorDirecao(controlador);


                MyN3dsCommandDefines.StandardButtons();

                if (mov == null)
                {
                    SetaMov();
                }
                else
                {
                    int temStatus = StatusTemporarioBase.ContemStatus(TipoStatus.amedrontado, meuCriatureBase);

                    if (temStatus > -1)
                        dir *= 1 / (float)meuCriatureBase.StatusTemporarios[temStatus].Quantificador;

                    if (!GameController.g.HudM.MenuDePause.EmPause)
                    {
                        mov.AplicadorDeMovimentos(dir, meuCriatureBase.CaracCriature.distanciaFundamentadora, transform);

                    }
                }

            break;
        }
    }

    Vector3 DirecaoInduzida(float h, float v)
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 retorno = cameraTransform.TransformDirection(Vector3.forward);
        retorno = Vector3.ProjectOnPlane(retorno, Vector3.up).normalized;
        retorno = v * retorno - h * Vector3.Cross(retorno, Vector3.up);

        return retorno;
    }

    void SetaMov()
    {
        mov = new MovimentacaoBasica(
                       meuCriatureBase.Mov, new ElementosDeMovimentacao()
                       {
                           animador = new AnimadorHumano(GetComponent<Animator>()),
                           controle = GetComponent<CharacterController>(),
                           transform = transform
                       }
                        );
    }

    Vector3 direcaoInduzida(float h, float v)
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 retorno = cameraTransform.TransformDirection(Vector3.forward);
        retorno = Vector3.ProjectOnPlane(retorno, Vector3.up).normalized;
        retorno = v * retorno - h * (Vector3.Cross(retorno, Vector3.up));
        
        return retorno;
    }

    void AplicaGolpe()
    {
        Atributos A = MeuCriatureBase.CaracCriature.meusAtributos;
        IGolpeBase gg = meuCriatureBase.GerenteDeGolpes.meusGolpes[meuCriatureBase.GerenteDeGolpes.golpeEscolhido];
        Debug.Log("no chão: " + (mov.NoChao(meuCriatureBase.CaracCriature.distanciaFundamentadora)));
        if ((mov.NoChao(meuCriatureBase.CaracCriature.distanciaFundamentadora) || gg.PodeNoAr))
        {
            if (!DisparadorDoGolpe.Dispara(meuCriatureBase, gameObject))
            {
                
                string[] textos = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.usoDeGolpe).ToArray();
                
                if (gg.UltimoUso + gg.TempoDeReuso >= Time.time)
                    GameController.g.HudM.Painel.AtivarNovaMens(
                        string.Format(textos[0], MostradorDeTempo(gg.UltimoUso - (Time.time - gg.TempoDeReuso)))
                        , 25, 2);
                else if (A.PE.Corrente < gg.CustoPE)
                GameController.g.HudM.Painel.AtivarNovaMens(textos[1], 25, 2);
            }
        }
    }

    public void ComandoDeAtacar()
    {
        if (estado == CreatureState.aPasseio || estado == CreatureState.emLuta)
            AplicaGolpe();
    }

    public void IniciaPulo()
    {
        if (!meuCriatureBase.Mov.caracPulo.estouPulando && (estado == CreatureState.aPasseio ||estado==CreatureState.emLuta))
            mov._Pulo.IniciaAplicaPulo();
    }

    public void PararCriatureNoLocal()
    {
        estado = CreatureState.parado;
        ia.SuspendeNav();
    }

    public static string MostradorDeTempo(float t, string tipo = "m.s.ms", bool tiraZero = true)
    {
        string retorno = "";
        float ms = (int)(t * 1000) % 1000;
        float s = ((int)t) % 60;
        float h = ((int)t) / 3600;
        float m = (((int)t) / 60) % 60;

        switch (tipo)
        {
            case "m.s.ms":
                if (tiraZero)
                {
                    if (m == 0)
                        retorno = s + "s" + ms + "ms";
                    else
                        retorno = m + "m" + s + "s" + ms + "ms";
                }
                else
                    retorno = m + "m" + s + "s" + ms + "ms";

                break;
            case "h.m.s.ms":
                retorno = h + "h" + m + "min" + s + "s" + ms + "ms";
                break;
            case "s":
                retorno = (s + 60 * m).ToString();
                break;
        }

        return retorno;
    }
}


