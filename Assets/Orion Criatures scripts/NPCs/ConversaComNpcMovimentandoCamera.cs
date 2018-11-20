using UnityEngine;
using System.Collections;

[System.Serializable]
public class ConversaComNpcMovimentandoCamera : NPCdeConversa
{
    [SerializeField]private MovimentoDeCamera[] movs;
    [SerializeField]private float velocidadeTempoDeCamera = .75f;

    private float alturaDaCamera = -1;
    private float distanciaDaCamera = 6;
    private int indiceDaConversaCondiciona = 0;

    public MovimentoDeCamera[] Movs
    {
        get { return movs; }
        private set { movs = value; }
    }

    public int IndiceDaConversaCondiciona
    {
        get { return indiceDaConversaCondiciona; }
        set { indiceDaConversaCondiciona = value; }
    }

    public override bool Update()
    {
        if (estado == EstadoDoNPC.conversando)
        {
            
            for (int i = 0; i < movs.Length; i++)
                if (GameController.g.HudM.DisparaT.IndiceDaConversa == movs[i].IndiceDeInicioDeMovimento &&
                    indiceDaConversaCondiciona == movs[i].indiceDaCondicional
                    )
                {
                    if (movs[i].AlvoDoMovimento != null)
                    {
                        AplicadorDeCamera.cam.InicializaCameraExibicionista(movs[i].AlvoDoMovimento, movs[i].alturaDaCamera, true);
                    }
                   // else if (estado == EstadoDoNPC.conversando)
                    //    AplicadorDeCamera.cam.InicializaCameraExibicionista(Destrutivel, 1, true);

                    alturaDaCamera = movs[i].alturaDaCamera;
                    distanciaDaCamera = movs[i].distanciaDaCamera;
                }


            if(AplicadorDeCamera.cam.Estilo ==AplicadorDeCamera.EstiloDeCamera.mostrandoUmCriature)
                AplicadorDeCamera.cam.FocarPonto(velocidadeTempoDeCamera, distanciaDaCamera, alturaDaCamera,true);
        }

        return base.Update();
    }
}

[System.Serializable]
public class MovimentoDeCamera
{
    public float alturaDaCamera = 1;
    public float distanciaDaCamera = 6;
    public int indiceDaCondicional = 0;
    [SerializeField] private int indiceDeInicioDeMovimento;
    [SerializeField] private Transform alvoDoMovimento;

    public int IndiceDeInicioDeMovimento
    {
        get { return indiceDeInicioDeMovimento; }
        set { indiceDeInicioDeMovimento = value; }
    }

    public Transform AlvoDoMovimento
    {
        get { return alvoDoMovimento; }
        set { alvoDoMovimento = value; }
    }
}