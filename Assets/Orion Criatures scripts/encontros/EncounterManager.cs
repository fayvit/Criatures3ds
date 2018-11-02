using UnityEngine;
using System.Collections;

public class EncounterManager
{
    private float contadorDeTempo = 0;
    private bool treinador = false;
    private AplicadorDeCamera cam;
    private ApresentadorDeAdversario apresentaAdv;
    private ApresentaFim apresentaFim;
    private ApresentaDerrota apresentaDerrota;
    private PassouDeNivelEmLuta passou;
    private CreatureManager inimigo;
    private CharacterManager manager;
    private EncounterState estado = EncounterState.emEspera;
    private Atributos aDoI;
    private Atributos aDoH;


    private enum EncounterState
    {
        emEspera,
        truqueDeCamera,
        apresentaAdversario,
        comecaLuta,
        verifiqueVida,
        vitoriaNaLuta,
        VoltarParaPasseio,
        morreuEmLuta,
        passouDeNivel,
        gerenciaGolpe,
        aprendeuEsse
            // O 13 era aprendendo golpe fora, deverá ser feito separadamente
    }

    public CreatureManager Inimigo
    {
        get { return inimigo; }
    }

    public void InicializarEncounterManager(CreatureManager inimigo,CharacterManager manager,bool treinador,string nomeTreinador)
    {
        if (inimigo)
        {
            this.inimigo = inimigo;
            this.manager = manager;
            this.treinador = treinador;

            VerificaContainerDeAtributos();

            apresentaAdv = new ApresentadorDeAdversario(inimigo,treinador,nomeTreinador);
            estado = EncounterState.truqueDeCamera;
        }
    }

    public bool Update()
    {
        bool retorno = false;
        switch (estado)
        {
            case EncounterState.truqueDeCamera:
                TruqueDeCamera();
            break;
            case EncounterState.apresentaAdversario:
                contadorDeTempo += Time.deltaTime;
                if (apresentaAdv.Apresenta(contadorDeTempo, cam))
                    depoisDeTerminarAApresentacao();
            break;
            case EncounterState.comecaLuta:
                GameController.g.HudM.ModoCriature(true);
                ((IA_Agressiva)inimigo.IA).PodeAtualizar = true;
                manager.CriatureAtivo.Estado = CreatureManager.CreatureState.emLuta;
                cam.InicializaCameraDeLuta(manager.CriatureAtivo,inimigo.transform);
                estado = EncounterState.verifiqueVida;
            break;
            case EncounterState.verifiqueVida:
                //GameController.g.HudM.AtualizeHud(manager, inimigo.MeuCriatureBase);
                VerifiqueVida();                
            break;
            case EncounterState.vitoriaNaLuta:
                if (!apresentaFim.EstouApresentando(treinador))
                {
                    RecebePontosDaVitoria();
                }
            break;
            case EncounterState.VoltarParaPasseio:

                if(inimigo)
                    MonoBehaviour.Destroy(inimigo.gameObject);

                Debug.Log("treinador = " + treinador);
                if (!treinador)
                {
                    cam.FocarBasica(manager.transform, 10, 10);
                    retorno = true;
                }

                estado = EncounterState.emEspera;

            break;
            case EncounterState.morreuEmLuta:
                ApresentaDerrota.RetornoDaDerrota R = apresentaDerrota.Update();
                if (R!=ApresentaDerrota.RetornoDaDerrota.atualizando)
                {
                    if (R == ApresentaDerrota.RetornoDaDerrota.voltarParaPasseio)
                        estado = EncounterState.verifiqueVida;
                    else
                    if (R == ApresentaDerrota.RetornoDaDerrota.deVoltaAoArmagedom)
                        estado = EncounterState.emEspera;
                }
            break;
            case EncounterState.passouDeNivel:
                if (passou.Update())
                {
                    estado = EncounterState.VoltarParaPasseio;
                }
            break;
        }
        return retorno;
    }

    public void FinalizarEncontro(bool mudarTreinador = false)
    {
        treinador &= !mudarTreinador;
        estado = EncounterState.VoltarParaPasseio;
    }

    void RecebePontosDaVitoria()
    {
        IGerenciadorDeExperiencia G_XP = manager.CriatureAtivo.MeuCriatureBase.CaracCriature.mNivel;
        G_XP.XP += (treinador)? aDoI.PV.Maximo:(int)((float)aDoI.PV.Maximo/2);
        if (G_XP.VerificaPassaNivel())
        {
            G_XP.AplicaPassaNivel(aDoH);
            //GameController.g.HudM.AtualizeHud(manager, inimigo.MeuCriatureBase);
            passou = new PassouDeNivelEmLuta(manager.CriatureAtivo.MeuCriatureBase);
            estado = EncounterState.passouDeNivel;
        }
        else
            estado = EncounterState.VoltarParaPasseio;

        manager.Dados.Cristais += treinador?aDoI.PV.Maximo*2: aDoI.PV.Maximo;
    }

    protected void VerifiqueVida()
    {
        VerificaContainerDeAtributos();

        contadorDeTempo = 0;

        if (aDoI.PV.Corrente <= 0 && aDoH.PV.Corrente > 0)
        {
           UmaVitoria();
        }

        if (aDoH.PV.Corrente <= 0)
            UmaDerrota();

    }

    void UmaDerrota()
    {
        InterrompeFluxoDeLuta();
        inimigo.Estado = CreatureManager.CreatureState.parado;
        apresentaDerrota = new ApresentaDerrota(manager, inimigo);
        estado = EncounterState.morreuEmLuta;
        GameController.g.FinalizaHuds();
    }

    public void VerificaContainerDeAtributos()
    {
        aDoI = inimigo.MeuCriatureBase.CaracCriature.meusAtributos;

        if(manager.CriatureAtivo)
            aDoH = manager.CriatureAtivo.MeuCriatureBase.CaracCriature.meusAtributos;
    }

    void UmaVitoria()
    {
        InterrompeFluxoDeLuta();
        apresentaFim = new ApresentaFim(manager.CriatureAtivo, inimigo, cam);
        estado = EncounterState.vitoriaNaLuta;   
    }

    void InterrompeFluxoDeLuta()
    {
        manager.CriatureAtivo.Estado = CreatureManager.CreatureState.parado;
    }

    protected virtual void depoisDeTerminarAApresentacao()
    {
        estado = EncounterState.comecaLuta;
    }

    void TruqueDeCamera()
    {
        contadorDeTempo += Time.deltaTime;
        if (contadorDeTempo > 0.5f)
        {
            estado = EncounterState.apresentaAdversario;
            cam = AplicadorDeCamera.cam;
            contadorDeTempo = 0;
        }
    }
}