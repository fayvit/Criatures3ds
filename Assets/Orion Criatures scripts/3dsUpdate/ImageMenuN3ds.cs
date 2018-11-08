using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageMenuN3ds : FerramentasDeHud/*ferramentas de hud será removida*/
{
    [SerializeField] private RawImage[] imagensDaHud;
    [SerializeField] private Text[] textosDasQuantidades;
    /* Legado */
    [SerializeField] private Text textoDoItem;

    private DadosDoPersonagem dados;
    private TipoHud tipo = TipoHud.items;

    private int numeroDeElementos;

    private const float TEMPO_PARA_SAIR = 5;
    /*****************************************************************/

    private void Awake()
    {
        entrando = false;
    }
    // Use this for initialization
    void Start()
    {
        if (ExistenciaDoController.AgendaExiste(Start, this))
        {

            dados = GameController.g.Manager.Dados;
            
            switch (tipo)
            {
                case TipoHud.criatures:
                    numeroDeElementos = dados.CriaturesAtivos.Count - 1;
                break;
                case TipoHud.golpes:
                    numeroDeElementos = GameController.g.Manager.CriatureAtivo.MeuCriatureBase.GerenteDeGolpes.meusGolpes.Count;
                break;
                case TipoHud.items:
                    numeroDeElementos = dados.Itens.Count;
                break;
            }

            
        }
    }

    public void AtualizaModificacaoNaHud()
    {
        bool foi = false;
        //VerificaTempoAtiva(TEMPO_PARA_SAIR);
        switch (tipo)
        {
            case TipoHud.criatures:
                if (dados.CriaturesAtivos.Count > 1)
                    foi = true;
            break;
            case TipoHud.golpes:
                if (dados.CriaturesAtivos != null)
                    if (dados.CriaturesAtivos.Count > 0)
                        foi = true;
                    
            break;
            case TipoHud.items:
                if (dados.Itens.Count > 0)
                    foi = true;
            break;
        }

        if (foi)
            AtualizaDadosDeHUD();
        else
            Esconde();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dados!=null)
        AtualizaModificacaoNaHud();
    }

    
    void VerificaNumeroDeElementos()
    {
        if (tipo == TipoHud.items)
        {           
            numeroDeElementos = dados.Itens.Count;
        }
    }

    void AtualizaDadosDeHUD()
    {
        int principal = 0;
        int indice = 0;
        if (dados != null)
            switch (tipo)
            {
                case TipoHud.criatures:
                    principal = dados.CriatureSai + 1;
                    imagensDaHud[2].texture = GameController.g.El.RetornaMini(dados.CriaturesAtivos[principal].NomeID);
                    textoDoItem.text = dados.CriaturesAtivos[principal].NomeEmLinguas;

                    for (int i = 0; i < 5; i++)
                    {
                        textosDasQuantidades[i].enabled = false;
                    }

                    if (numeroDeElementos > 1)
                    {
                        indice = dados.CriaturesAtivos.Count > principal + 1 ? principal + 1 : 1;
                        imagensDaHud[3].texture = GameController.g.El.RetornaMini(dados.CriaturesAtivos[indice].NomeID);

                        indice = dados.CriaturesAtivos.Count > indice + 1 ? indice + 1 : 1;
                        imagensDaHud[4].texture = GameController.g.El.RetornaMini(dados.CriaturesAtivos[indice].NomeID);
                        indice = principal;
                        indice = indice - 1 > 0 ? indice - 1 : dados.CriaturesAtivos.Count - 1;
                        imagensDaHud[1].texture = GameController.g.El.RetornaMini(dados.CriaturesAtivos[indice].NomeID);

                        indice = indice - 1 > 0 ? indice - 1 : dados.CriaturesAtivos.Count - 1;
                        imagensDaHud[0].texture = GameController.g.El.RetornaMini(dados.CriaturesAtivos[indice].NomeID);
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (i != 2)
                            {
                                imagensDaHud[i].enabled = false;
                                textosDasQuantidades[i].enabled = false;
                            }
                        }
                    }
                    break;
                case TipoHud.golpes:
                    GerenciadorDeGolpes gg = dados.CriaturesAtivos[0].GerenteDeGolpes;

                    for (int i = 0; i < 5; i++)
                    {
                        textosDasQuantidades[i].enabled = false;
                    }
                    principal = gg.golpeEscolhido;
                    textoDoItem.text = gg.meusGolpes[principal].NomeEmLinguas();

                    imagensDaHud[2].texture = GameController.g.El.RetornaMini(gg.meusGolpes[principal].Nome);
                    if (numeroDeElementos > 1)
                    {
                        indice = gg.meusGolpes.Count > principal + 1 ? principal + 1 : 0;
                        imagensDaHud[3].texture = GameController.g.El.RetornaMini(gg.meusGolpes[indice].Nome);

                        indice = gg.meusGolpes.Count > indice + 1 ? indice + 1 : 0;
                        imagensDaHud[4].texture = GameController.g.El.RetornaMini(gg.meusGolpes[indice].Nome);
                        indice = principal;
                        indice = indice - 1 > -1 ? indice - 1 : gg.meusGolpes.Count - 1;
                        imagensDaHud[1].texture = GameController.g.El.RetornaMini(gg.meusGolpes[indice].Nome);

                        indice = indice - 1 > -1 ? indice - 1 : gg.meusGolpes.Count - 1;
                        imagensDaHud[0].texture = GameController.g.El.RetornaMini(gg.meusGolpes[indice].Nome);
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (i != 2)
                            {
                                imagensDaHud[i].enabled = false;
                                textosDasQuantidades[i].enabled = false;
                            }
                        }
                    }
                    break;
                case TipoHud.items:
                    principal = dados.itemSai;
                    textoDoItem.text = MbItens.NomeEmLinguas(dados.Itens[principal].ID);

                    imagensDaHud[2].texture = GameController.g.El.RetornaMini(dados.Itens[principal].ID);
                    textosDasQuantidades[2].text = dados.Itens[principal].Estoque.ToString();
                    if (numeroDeElementos > 1)
                    {
                        indice = dados.Itens.Count > principal + 1 ? principal + 1 : 0;
                        imagensDaHud[3].texture = GameController.g.El.RetornaMini(dados.Itens[indice].ID);
                        textosDasQuantidades[3].text = dados.Itens[indice].Estoque.ToString();

                        indice = dados.Itens.Count > indice + 1 ? indice + 1 : 0;
                        imagensDaHud[4].texture = GameController.g.El.RetornaMini(dados.Itens[indice].ID);
                        textosDasQuantidades[4].text = dados.Itens[indice].Estoque.ToString();

                        indice = principal;
                        indice = indice > 0 ? indice - 1 : dados.Itens.Count - 1;
                        imagensDaHud[1].texture = GameController.g.El.RetornaMini(dados.Itens[indice].ID);
                        textosDasQuantidades[1].text = dados.Itens[indice].Estoque.ToString();

                        indice = indice > 0 ? indice - 1 : dados.Itens.Count - 1;
                        imagensDaHud[0].texture = GameController.g.El.RetornaMini(dados.Itens[indice].ID);
                        textosDasQuantidades[0].text = dados.Itens[indice].Estoque.ToString();
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (i != 2)
                            {
                                imagensDaHud[i].enabled = false;
                                textosDasQuantidades[i].enabled = false;
                            }
                        }
                    }
                    break;
            }
    }    

    public void Acionada(TipoHud tipo)
    {
        if (this.tipo != tipo)
        {
            Esconde();
            this.tipo = tipo;
            Start();

            
        }

        for (int i = 0; i < 5; i++)
        {
            imagensDaHud[i].enabled = true;

            if (tipo == TipoHud.items &&!textosDasQuantidades[i].enabled)
            {
                textosDasQuantidades[i].enabled = true;
                textosDasQuantidades[i].transform.parent.GetComponent<Image>().enabled = true;
            }
        }

        textoDoItem.enabled = true;


        tempoAtiva = 0;
    }

    public override void Esconde()
    {
        for (int i = 0; i < imagensDaHud.Length; i++)
        {
            imagensDaHud[i].enabled = false;
            textosDasQuantidades[i].enabled = false;
            textosDasQuantidades[i].transform.parent.GetComponent<Image>().enabled = false;
        }
        textoDoItem.enabled = false;
    }    

    public void Destruir()
    {
        Destroy(gameObject);
    }
}