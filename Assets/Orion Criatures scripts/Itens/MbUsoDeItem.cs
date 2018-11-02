using UnityEngine;
using System.Collections;

public class MbUsoDeItem
{ 
    private CharacterManager gerente;
    private FluxoDeRetorno fluxo;

    private MbItens esseItem;
    private bool retorno = false;
    private bool retornoDeFora = false;


    public bool EstouUsandoItem
    {
        get { return retornoDeFora; }
    }
    // Use this for initialization
    public void Start(CharacterManager manager,FluxoDeRetorno fluxo)
    {
        retorno = false;
        retornoDeFora = true;
        gerente = manager;
        esseItem = gerente.Dados.Itens[gerente.Dados.itemSai];
        this.fluxo = fluxo;

        switch (fluxo)
        {
            case FluxoDeRetorno.criature:
                esseItem.IniciaUsoComCriature(gerente.gameObject);
            break;
            case FluxoDeRetorno.heroi:
                esseItem.IniciaUsoDeHeroi(gerente.gameObject);
            break;
            case FluxoDeRetorno.menuHeroi:
            case FluxoDeRetorno.menuCriature:
                esseItem.IniciaUsoDeMenu(gerente.gameObject);
            break;
        }
    }

    // Update is called once per frame
    public bool Update()
    {
        if (retornoDeFora)
        {
            switch (fluxo)
            {
                case FluxoDeRetorno.criature:
                    retorno = !esseItem.AtualizaUsoComCriature();
                break;
                case FluxoDeRetorno.heroi:
                    retorno = !esseItem.AtualizaUsoDeHeroi();
                break;
                case FluxoDeRetorno.menuHeroi:
                case FluxoDeRetorno.menuCriature:
                    retorno = !esseItem.AtualizaUsoDeMenu();// parece desnecessario
                break;
            }

            if (retorno)
            {

                switch (fluxo)
                {
                    case FluxoDeRetorno.criature:
                        Debug.Log(esseItem.ID);
                        gerente.AoCriature(GameController.g.InimigoAtivo);
                    break;
                    case FluxoDeRetorno.heroi:
                        gerente.AoHeroi();
                    break;
                    case FluxoDeRetorno.menuHeroi:
                    case FluxoDeRetorno.menuCriature:
                        //GameController.g.HudM.PauseM.FinalizaUsoDeItemComMenu();
                    break;
                }

                retornoDeFora = false;

            }
            return true;
        }
        else
            return false;
    }

    public void FinalizaUsaItemDeFora()
    {
        retorno = false;
        retornoDeFora = false;
        fluxo = FluxoDeRetorno.heroi;
    }
}
