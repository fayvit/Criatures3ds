using UnityEngine;
using System.Collections;

public class IA_Agressiva : IA_Base
{
    private float coolDown = 0;
    private bool podeAtualizar = false;

    private const float TEMPO_DE_COOLDOWN = 2;
    private const float MOD_DISTANCIA_DE_ATAQUE = 14;

    public bool PodeAtualizar
    {
        get { return podeAtualizar; }
        set { podeAtualizar = value; }
    }

    protected override void AplicaIaDeAtaque()
    {
        if(PodeAtualizar)
            coolDown += Time.deltaTime;

        Atributos A = meuCriature.MeuCriatureBase.CaracCriature.meusAtributos;
        GerenciadorDeGolpes gg = meuCriature.MeuCriatureBase.GerenteDeGolpes;

        if (criatureDoJogador
            &&
            A.PV.Corrente > 0
            &&
            coolDown > TEMPO_DE_COOLDOWN
            &&
            gg.meusGolpes.Count > 0
            &&
            podeAtualizar
            )
        {

            GolpeBase GB = gg.meusGolpes[gg.golpeEscolhido];

            if (GB.Caracteristica == caracGolpe.colisao || GB.Caracteristica==caracGolpe.colisaoComPow)
            {
                {

                    VerifiqueSigaOuAtaque(GB, A);
                }
            }
            else
            {

                Disparador(GB, A);
            }
        }
        else if (gg.meusGolpes.Count <= 0)
        {
            Debug.Log("lista de golpes vazia. POr que??? nivel" + meuCriature.MeuCriatureBase.CaracCriature.mNivel.Nivel);
            BugDaListaVazia();
        }
        else if (A.PV.Corrente <= 0)
        {
            siga.PareAgora();
        }
        else if (coolDown < TEMPO_DE_COOLDOWN)
        {
            AproximeEnquantoEspera();
        }
        else if (!criatureDoJogador && !procurando)
            ProcuraCriatureDoJogador();
            
        
    }

    void AproximeEnquantoEspera()
    {
        Vector3 instancia = criatureDoJogador.transform.position + 7 * ((meuCriature.transform.position - criatureDoJogador.transform.position).normalized);
        
        melhoraPos melhoraPF = new melhoraPos();

        instancia = melhoraPF.posEmparedado(instancia, criatureDoJogador.transform.position);

        instancia = melhoraPF.novaPos(instancia, meuCriature.transform.lossyScale.y);

        siga.Update(instancia);
    }


    void BugDaListaVazia()
    {
        CriatureBase C = meuCriature.MeuCriatureBase;
        C.GerenteDeGolpes.meusGolpes.AddRange(
            C.GolpesAtivos(
                C.CaracCriature.mNivel.Nivel, C.GerenteDeGolpes.listaDeGolpes.ToArray()));
    }

    void VerifiqueSigaOuAtaque(GolpeBase GB,Atributos A)
    {
        if ((criatureDoJogador.transform.position - meuCriature.transform.position).magnitude
            >
            MOD_DISTANCIA_DE_ATAQUE *
            (GB.TempoDeMoveMax - GB.TempoDeMoveMin)
            )
        {
            int numStatus = StatusTemporarioBase.ContemStatus(TipoStatus.amedrontado, meuCriature.MeuCriatureBase);
            if (numStatus > -1)
            {
                siga.ModVelocidade = 1 / (float)meuCriature.MeuCriatureBase.StatusTemporarios[numStatus].Quantificador;
            }
            else
                siga.ModVelocidade = 1;

            siga.Update(criatureDoJogador.transform.position);
        }
        else
        {
            siga.PareAgora();
            Disparador(GB,A);
        }
    }

    void Disparador(GolpeBase GB,Atributos A)
    {
        coolDown = TEMPO_DE_COOLDOWN;

        Vector3 olhe = criatureDoJogador.transform.position
            - meuCriature.transform.position;
        olhe = new Vector3(olhe.x, 0, olhe.z);
        meuCriature.transform.rotation = Quaternion.LookRotation(olhe);

        if (GB.CustoPE <= A.PE.Corrente)
        {
            AplicaGolpe();
        }
        else
        {
            ProcureColisao();
        }        
    }

    void AplicaGolpe()
    {
        meuCriature.transform.rotation = Quaternion.LookRotation(
            Vector3.ProjectOnPlane(
                criatureDoJogador.transform.position-meuCriature.transform.position,
                Vector3.up
                )
            );
        if(!DisparadorDoGolpe.Dispara(meuCriature.MeuCriatureBase,meuCriature.gameObject))
        {
            coolDown = 0;
            meuCriature.MeuCriatureBase.GerenteDeGolpes.golpeEscolhido = SorteadorDeGolpe.Sorteia(
                meuCriature.MeuCriatureBase.NomeID, meuCriature.MeuCriatureBase.GerenteDeGolpes);
        }
    }
    

    void ProcureColisao()
    {
        GerenciadorDeGolpes gg = meuCriature.MeuCriatureBase.GerenteDeGolpes;
        bool foi = false;
        for (int i = 0; i < gg.meusGolpes.Count; i++)
        {
            if (gg.meusGolpes[i].CustoPE == 0)
            {
                foi = true;
                gg.golpeEscolhido = i;
            }
        }

        if (foi)
        {
            coolDown = 0;            
        }
        else
        {
            meuCriature.MeuCriatureBase.CaracCriature.meusAtributos.PE.AumentaAoMaximo();
        }
    }
}
