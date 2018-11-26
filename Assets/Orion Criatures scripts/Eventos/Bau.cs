using UnityEngine;
using System.Collections;

public class Bau : AtivadorDeBotao
{
    [SerializeField] private string ID = "";
    [SerializeField] private ItemDeBau[] itemDoBau;
    [SerializeField] private Transform tampa;

    private int indiceDoItem = 0;
    private string[] textos = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.bau).ToArray();
    private FasesDoBau fase = FasesDoBau.emEspera;

    private enum FasesDoBau
    {
        emEspera,
        abrindo,
        lendoOpcoes,
        aberto,
        fechando
    }



    // Use this for initialization
    void Start()
    {
        if (ExistenciaDoController.AgendaExiste(Start, this))
        {
            if (GameController.g.MyKeys.VerificaAutoShift(ID))
                tampa.Rotate(tampa.right, -70, Space.World);

            textoDoBotao = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.textoBaseDeAcao)[1];
            SempreEstaNoTrigger();
        }
    }

    void AcaoDeOpcaoLida()
    {
        QualOpcao(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
    }

    void BauAberto()
    {
        if (indiceDoItem + 1 > itemDoBau.Length || GameController.g.MyKeys.VerificaAutoShift(ID))
        {
            GameController.g.HudM.Painel.EsconderMensagem();
            GameController.g.HudM.MostrarItem.DesligarPainel();
            indiceDoItem = 0;
            fase = FasesDoBau.fechando;
        }
        else
        {
            VerificaItem();
        }
    }

    private void OnValidate()
    {
        BuscadorDeID.Validate(ref ID, this);
        /*
#if UNITY_EDITOR
        if (gameObject.scene.name!=null && autoKey== "0" )
        {
            //autoKey = BuscadorDeID.GetUniqueID(gameObject, autoKey);
            autoKey = GetInstanceID() + "_" + gameObject.scene.name + "_Bau";
            BuscadorDeID.SetUniqueIdProperty(this, autoKey, "autoKey");
        }
#endif */
    }

    // Update is called once per frame
    new void Update()
    {

        base.Update();
        switch (fase)
        {
            case FasesDoBau.lendoOpcoes:

                GameController.g.HudM.Menu_Basico.MudarOpcao();


                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                    QualOpcao(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
                break;
            case FasesDoBau.abrindo:

                if (Vector3.Angle(tampa.forward, transform.forward) < 70)
                    tampa.Rotate(tampa.right, -75 * Time.deltaTime, Space.World);
                else
                {
                    fase = FasesDoBau.aberto;

                    VerificaItem();
                }
                break;
            case FasesDoBau.aberto:
                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                    BauAberto();
                break;
            case FasesDoBau.fechando:
                // if (Vector3.Angle(tampa.forward, transform.forward) > 5)
                //    tampa.Rotate(tampa.right, 75 * Time.deltaTime, Space.World);
                //else
                {
                    GameController.g.MyKeys.MudaAutoShift(ID, true);
                    //tampa.rotation = Quaternion.LookRotation(transform.forward);
                    FinalizarAcaoDeBau();
                }
                break;
        }


    }

    void VerificaItem()
    {
        if (GameController.g.MyKeys.VerificaAutoShift(ID))
        {
            GameController.g.HudM.Painel.AtivarNovaMens(textos[1], 25);
        }
        else
        {
            ItemDeBau ii = itemDoBau[indiceDoItem];
            GameController.g.HudM.Painel.AtivarNovaMens(string.Format(textos[2], ii.Quantidade, MbItens.NomeEmLinguas(ii.Item)), 25);
            GameController.g.HudM.MostrarItem.IniciarPainel(ii.Item, ii.Quantidade);
            GameController.g.Manager.Dados.AdicionaItem(ii.Item, ii.Quantidade);
            EventAgregator.Publish(new StandardSendStringEvent(gameObject, SoundEffectID.coisaBoaRebot.ToString(), EventKey.disparaSom));

            indiceDoItem++;
        }
    }

    void QualOpcao(int qual)
    {
        switch (qual)
        {
            case 0://sim
                EventAgregator.Publish(new StandardSendStringEvent(gameObject, SoundEffectID.paraBau.ToString(), EventKey.disparaSom));
                fase = FasesDoBau.abrindo;
                break;
            case 1://nao

                FinalizarAcaoDeBau();
                break;
        }

        GameController.g.HudM.Menu_Basico.FinalizarHud();
        GameController.g.HudM.Painel.EsconderMensagem();
    }

    void FinalizarAcaoDeBau()
    {
        EventAgregator.Publish(new StandardSendStringEvent(GameController.g.gameObject, SoundEffectID.XP_Swing03.ToString(), EventKey.disparaSom));
        fase = FasesDoBau.emEspera;
        //GameController.g.HudM.ligarControladores();
        //AndroidController.a.LigarControlador();
        GameController.g.Manager.AoHeroi();

        /* Linha de salvamento do jogo */
        GameController.g.Salvador.SalvarAgora();
    }

    public override void FuncaoDoBotao()
    {
        SomDoIniciar();
        FluxoDeBotao();

        //commandR = GameController.g.CommandR;
        ActionManager.ModificarAcao(GameController.g.transform, AcaoDeOpcaoLida);

        if (GameController.g.MyKeys.VerificaAutoShift(ID))
            fase = FasesDoBau.abrindo;
        else
        {
            fase = FasesDoBau.lendoOpcoes;
            GameController.g.HudM.Painel.AtivarNovaMens(textos[0], 25);
            GameController.g.HudM.Menu_Basico.IniciarHud(QualOpcao,
            BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.simOuNao).ToArray()
            );
        }
    }


}

[System.Serializable]
public class ItemDeBau
{
    [SerializeField] private nomeIDitem item;
    [SerializeField] private string itemString = "";
    [SerializeField] private int quantidade;

    public nomeIDitem Item
    {
        get
        {
            nomeIDitem n = item;
            if (itemString != "")
                n = StringParaEnum.ObterEnum<nomeIDitem>(itemString);
            return n;
        }
        set { item = value; }
    }

    public int Quantidade
    {
        get { return quantidade; }
        set { quantidade = value; }
    }
}