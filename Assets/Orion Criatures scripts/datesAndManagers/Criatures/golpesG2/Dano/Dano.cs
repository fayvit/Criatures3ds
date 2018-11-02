using UnityEngine;
using System.Collections;

public class Dano
{

    public static void VerificaComportamentoDeIA(CreatureManager GdC,GameObject atacante)
    {
        /*
        if (!GdC.gerenteCri)
        {
            GerenciadorDeIA GdIA = GdC.GetComponent<GerenciadorDeIA>();
            
            
            if (GdIA)
            {
                CreatureManager gDoAtacante = atacante.GetComponent<CreatureManager>();
                if (gDoAtacante.gerenteCri)
                {
                    GdIA.FuiAtacadoPor(gDoAtacante);
                }
            }
        }*/
    }

    public static void VerificaDano(GameObject atacado,GameObject atacante,IGolpeBase golpe)
    {
        if (atacado.tag == "eventoComGolpe" && !GameController.g.estaEmLuta)
        {
            atacado.GetComponent<EventoComGolpe>().DisparaEvento(golpe.Nome);
        }

        CreatureManager GdC = atacado.GetComponent<CreatureManager>();
        if (GdC && !GameController.g.UsandoItemOuTrocandoCriature)
        {
            if (GdC.MeuCriatureBase.CaracCriature.meusAtributos.PV.Corrente > 0)
            {
                AplicaDano(GdC,atacante,golpe);
                VerificaComportamentoDeIA(GdC,atacante);
            }
        }
    }

    public static void EmEstadoDeDano(Animator animatorDoAtacado,CreatureManager doAtacado)
    {
        doAtacado.MudaParaEstouEmDano();
        //Transform T = doAtacado.transform;
        
        //  doAtacado.MudaEmDano();

        animatorDoAtacado.Play("dano2");
        animatorDoAtacado.SetBool("dano1", true);
        animatorDoAtacado.Play("dano1");
    }

    public static void InsereEstouEmDano(CreatureManager doAtacado,Animator animatorDoAtacado,IGolpeBase golpe)
    {
        EstouEmDano eED = doAtacado.gameObject.AddComponent<EstouEmDano>();
        eED.esseGolpe = golpe;
        eED.animator = animatorDoAtacado;
        eED.gerente = doAtacado;
    }

    public static void AplicaDano(CreatureManager doAtacado,GameObject atacante,IGolpeBase golpe)
    {
        Animator animatorDoAtacado = doAtacado.GetComponent<Animator>();
        EmEstadoDeDano(animatorDoAtacado, doAtacado);

        CalculaDano(doAtacado,atacante,golpe);

        InsereEstouEmDano(doAtacado,animatorDoAtacado,golpe);

        VerificaVida(atacante,doAtacado,animatorDoAtacado);
    }

    static void VerificaVida(GameObject atacante,CreatureManager doAtacado,Animator a)
    {
        if (doAtacado.MeuCriatureBase.CaracCriature.meusAtributos.PV.Corrente <= 0)
        {
            a.SetBool("cair", true);

            UnityEngine.AI.NavMeshAgent nav = a.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (nav.enabled)
                nav.Stop();// nav.isStopped = true;
            
         /*   doAtacado.MudaParaDerrotado();

            if (!doAtacado.gerenteCri)
            {
                AplicaSelvagemDerrotado aSD =  doAtacado.gameObject.AddComponent<AplicaSelvagemDerrotado>();
                aSD.oDerrotado = doAtacado;
                aSD.oQDerrotou = atacante.GetComponent<GerenciadorDeCriature>();

            }
            else
            {
                //Morte de um criature selvagem
            }
            */
        }
    }

    static void CalculaDano(CreatureManager doAtacado, GameObject atacante, IGolpeBase golpe)
    {
        float multiplicador = 1;
        
        for (int i = 0; i < doAtacado.MeuCriatureBase.CaracCriature.contraTipos.Length; i++)
        {
            if (golpe.Tipo.ToString() == doAtacado.MeuCriatureBase.CaracCriature.contraTipos[i].Nome)
            {
                multiplicador *= doAtacado.MeuCriatureBase.CaracCriature.contraTipos[i].Mod;
            }
        }

        CriatureBase cDoAtacante = atacante.GetComponent<CreatureManager>().MeuCriatureBase;
        Atributos A = cDoAtacante.CaracCriature.meusAtributos;

        int potenciaDoAtacante = (golpe.Caracteristica == caracGolpe.colisao)
            ?
                Mathf.RoundToInt(A.Ataque.Minimo + (A.Ataque.Corrente - A.Ataque.Minimo) * Random.Range(0.85f, 1))
            :
                Mathf.RoundToInt(A.Poder.Minimo + (A.Poder.Corrente - A.Poder.Minimo) * Random.Range(0.85f, 1));

        int numStatus = StatusTemporarioBase.ContemStatus(TipoStatus.fraco, cDoAtacante);
        if (numStatus > -1)
        {
            potenciaDoAtacante = (int)Mathf.Max(1 / cDoAtacante.StatusTemporarios[numStatus].Quantificador * potenciaDoAtacante, (A.Ataque.Minimo + A.Poder.Minimo) / 2);
            golpe.ModCorrente = -(int)cDoAtacante.StatusTemporarios[numStatus].Quantificador;
        }
        else
            golpe.ModCorrente = 0;

        GolpePersonagem golpePersonagem = cDoAtacante.GerenteDeGolpes.ProcuraGolpeNaLista(cDoAtacante.NomeID, golpe.Nome);

        CalculoC(multiplicador, golpe, golpePersonagem, potenciaDoAtacante, doAtacado,cDoAtacante);

        golpe.VerificaAplicaStatus(cDoAtacante, doAtacado);
    }

    static void CalculoC(
        float multiplicador, 
        IGolpeBase golpe, 
        GolpePersonagem golpePersonagem, 
        int potenciaDoAtacante, 
        CreatureManager doAtacado,
        CriatureBase cDoAtacado)
    {
        Atributos aDoAtacado = doAtacado.MeuCriatureBase.CaracCriature.meusAtributos;
        float rd = Random.Range(0.85f, 1);
        int level = cDoAtacado.CaracCriature.mNivel.Nivel;
        float STAB = 1;
        if (cDoAtacado.CaracCriature.TemOTipo(golpe.Tipo))
        {
            STAB = 1.5f;
        }
        Debug.Log("modificador de potencia para esse golpe é " + golpePersonagem.ModPersonagem);
        //int  dano = (int)((((((((2 * level / 5) + 2) * potenciaDoAtacante* 20*(golpe.PotenciaCorrente+golpePersonagem.ModPersonagem) )/ aDoAtacado.Defesa.Corrente)/ 50) +2) *STAB * multiplicador) *rd / 100);
        int dano = (int)(((2*level+10)*potenciaDoAtacante*(golpe.PotenciaCorrente+golpePersonagem.ModPersonagem)/(aDoAtacado.Defesa.Corrente+250)+2)*STAB*multiplicador*rd);
        AplicaCalculoComVIsaoDeDano(doAtacado, golpe, aDoAtacado, multiplicador, dano, aDoAtacado.Defesa.Corrente, potenciaDoAtacante);
    }

    static void AplicaCalculoComVIsaoDeDano(CreatureManager doAtacado,
        IGolpeBase golpe,
        Atributos aDoAtacado,
        float multiplicador,
        int dano,
        int defesa,
        int potenciaDoAtacante)
    {
        AplicaCalculoDoDano(aDoAtacado, dano);

        Debug.Log("O dano do Golpe e " + dano + " O nome do golpe e " + golpe.Nome + " o multiplicador e" + multiplicador
            + " A defesa do inimigo é " + defesa
            + " A potencia original é " + potenciaDoAtacante
            +" A potencia do Golpe é "+golpe.PotenciaCorrente);

        AplicaVisaoDeDano(doAtacado,dano);
    }

    public static void AplicaVisaoDeDano(CreatureManager doAtacado,int dano)
    {
        GameObject visaoDeDano = GameController.g.El.retorna("visaoDeDano");
        visaoDeDano = (GameObject)MonoBehaviour.Instantiate(visaoDeDano, doAtacado.transform.position, Quaternion.identity);
        DanoAparecendo danoAp = visaoDeDano.GetComponent<DanoAparecendo>();
        danoAp.dano = dano;
        danoAp.atacado = doAtacado.transform;

        /* INSERIDO PARA ATUALIZAR A HUD VIDA */
        GameController.g.HudM.AtualizaDadosDaHudVida(false);
        if (GameController.g.estaEmLuta)
            GameController.g.HudM.AtualizaDadosDaHudVida(true);
    }

    static void CalculoB(float multiplicador, IGolpeBase golpe, GolpePersonagem golpePersonagem, int potenciaDoAtacante, CreatureManager doAtacado)
    {
        Atributos aDoAtacado = doAtacado.MeuCriatureBase.CaracCriature.meusAtributos;

        int defesa = Mathf.RoundToInt(aDoAtacado.Defesa.Corrente * Random.Range(0.85f, 1));
        int dano = (int)(multiplicador * (golpe.PotenciaCorrente+golpePersonagem.ModPersonagem+potenciaDoAtacante/defesa));

        AplicaCalculoComVIsaoDeDano(doAtacado, golpe, aDoAtacado, multiplicador, dano, defesa, potenciaDoAtacante);
    }

    static void CalculoA(float multiplicador,IGolpeBase golpe,GolpePersonagem golpePersonagem, int potenciaDoAtacante,CreatureManager doAtacado)
    {

        int dano = Mathf.Abs(
            Mathf.RoundToInt(
                multiplicador * (golpe.PotenciaCorrente 
                            + golpePersonagem.ModPersonagem 
                            + potenciaDoAtacante)
                            ));

        potenciaDoAtacante = dano;//apenas para exibir no print a potencia original do golpe sem a defesa

        Atributos aDoAtacado = doAtacado.MeuCriatureBase.CaracCriature.meusAtributos;

        int defesa = Mathf.RoundToInt(aDoAtacado.Defesa.Corrente * Random.Range(0.85f,1));

        if (defesa < 0.75f * dano)
            dano =(dano-defesa>=1)?dano- defesa:1;
        else
            dano = (int)(0.25f * dano) >=1  ? Mathf.Max((int)(0.25f * dano*Random.Range(0.9f,1.15f)),1) : 1;

        /*
        if (dano > golpe.DanoMaximo && multiplicador <= 1)
            dano = golpe.DanoMaximo;
        else if (dano > multiplicador * golpe.DanoMaximo)
        {
            int mudaUmPoco = Random.Range(-1, 2);
            Debug.Log(mudaUmPoco);
            dano = Mathf.Max((int)(multiplicador * golpe.DanoMaximo) + mudaUmPoco, 1);
        }*/

        /*
        if (elementos == null)
            elementos = GameObject.Find("elementosDoJogo").GetComponent<elementosDoJogo>();

        mostraDano(elementos, T, dano);*/

        AplicaCalculoComVIsaoDeDano(doAtacado, golpe, aDoAtacado, multiplicador, dano, defesa, potenciaDoAtacante);

        /*
        if (X.cAtributos[0].Corrente > 0)
            aplicaStatus(T, X, ativa, Y);
            */
    }

    public static void AplicaCalculoDoDano(Atributos A, int dano)
    {
        if (A.PV.Corrente - dano > 0)
            A.PV.Corrente -= dano;
        else
            A.PV.Corrente = 0;
    }


}
