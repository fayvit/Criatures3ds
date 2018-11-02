using UnityEngine;

[System.Serializable]
public class RajadaDeAguaG2 : GolpeBase
{
    public RajadaDeAguaG2() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.rajadaDeAgua,
        tipo = nomeTipos.Agua,
        carac = caracGolpe.projetil,
        custoPE = 1,
        potenciaCorrente = 3,
        potenciaMaxima = 7,
        potenciaMinima = 1,
        tempoDeReuso = 5,
        tempoDeMoveMax = 1 ,
        tempoDeMoveMin = 0,
        tempoDeDestroy = 2,
        TempoNoDano = 1.75f       
    }
        )
    {
        
    }


    /****************************************************
    
        Essas variaveis são relacionadas com a ação de Golpe

    */
    [System.NonSerialized]private Vector3 posInicial;
    private float tempoDecorrido = 0;
    private int impactos = 0;
    private bool addView = false;
    [System.NonSerialized]private RaycastHit hit;

    /********************************************************/
    public override void IniciaGolpe(GameObject G)
    {
        impactos = 0;
        addView = false;
        tempoDecorrido = 0;
        posInicial = Emissor.UseOEmissor(G, this.Nome);
        DirDeREpulsao = G.transform.forward;        
        AnimadorCriature.AnimaAtaque(G, "emissor");         
    }

    public override void UpdateGolpe(GameObject G)
    {
        tempoDecorrido += Time.deltaTime;
        if (!addView)
        {
            GolpePersonagem golpeP = GolpePersonagem.RetornaGolpePersonagem(G, Nome);
            if (golpeP.TempoDeInstancia > 0)
                posInicial = Emissor.UseOEmissor(G, Nome);
            AuxiliarDeInstancia.InstancieEDestrua(Nome, posInicial, DirDeREpulsao, TempoDeDestroy);
            addView = true;
        }

        hit = new RaycastHit();
        
        Vector3 ort = Vector3.Cross(DirDeREpulsao, Vector3.up).normalized;

        float deslocadorInicial = tempoDecorrido > 1 ? tempoDecorrido : 1;
        float deslocadorFinal = tempoDecorrido < 0.7f ? tempoDecorrido : 0.7f;
        if (tempoDecorrido < TempoDeDestroy)
        {
            Debug.DrawLine(posInicial + 25 * (deslocadorInicial - 1) * DirDeREpulsao, posInicial + DirDeREpulsao * 25 * deslocadorFinal, Color.red);
            Debug.DrawLine(
                posInicial + 25 * (deslocadorInicial - 1) * DirDeREpulsao + 0.25f * Vector3.up,
                posInicial + 0.25f * Vector3.up + DirDeREpulsao * 25 * deslocadorFinal,
                Color.red);
            Debug.DrawLine(
                posInicial + 25 * (deslocadorInicial - 1) * DirDeREpulsao - 0.25f * Vector3.up,
                posInicial - 0.25f * Vector3.up + DirDeREpulsao * 25 * deslocadorFinal,
                Color.red);
            Debug.DrawLine(
                posInicial + 25 * (deslocadorInicial - 1) * DirDeREpulsao - 0.25f * ort,
                posInicial - 0.25f * ort + DirDeREpulsao * 25 * deslocadorFinal,
                Color.red);


            if (Physics.Linecast(posInicial + 25 * (deslocadorInicial - 1) * DirDeREpulsao, posInicial + DirDeREpulsao * 25 * tempoDecorrido, out hit)
               ||
               Physics.Linecast(
                posInicial + 25 * (deslocadorInicial - 1) * DirDeREpulsao - 0.25f * Vector3.up,
                posInicial - 0.25f * Vector3.up + DirDeREpulsao * 25 * tempoDecorrido,
                out hit)
               ||
               Physics.Linecast(
                posInicial + 25 * (deslocadorInicial - 1) * DirDeREpulsao - 0.25f * ort,
                posInicial - 0.25f * ort + DirDeREpulsao * 25 * tempoDecorrido,
                out hit)
               ||
               Physics.Linecast(
                posInicial + 25 * (deslocadorInicial - 1) * DirDeREpulsao + 0.25f * ort,
                posInicial + 0.25f * ort + DirDeREpulsao * 25 * tempoDecorrido,
                out hit)

               )
            {
                if (impactos % 10 == 0)
                {
                    
                    GameObject Golpe = GameController.g.El.retorna(DoJogo.impactoDeAgua);
                    Object impacto = MonoBehaviour.Instantiate(Golpe, hit.point, Quaternion.identity);
                    MonoBehaviour.Destroy(impacto, 0.5f);

                    if (impactos == 0)
                    {
                        Dano.VerificaDano(hit.transform.gameObject, G, this);

                        
                    }

                }
                impactos++;
            }
        }
    }
}
