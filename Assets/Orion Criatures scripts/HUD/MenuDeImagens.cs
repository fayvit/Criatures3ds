using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class MenuDeImagens : UiDeOpcoes
{
    private bool aberto = false;
    private float tempoDecorrido = 0;
    private float tempoParaFechar = 0;
    private int deslocamento = 0;
    private DadosDoPersonagem dados;
    private TipoDeDado tipo;
    private GameObject estePainel;
    private System.Action<int> acao;

    public bool Aberto
    {
        get { return aberto ; }
    }

    public TipoDeDado Tipo
    {
        get{ return tipo; }
    }

    protected override void FinalizarEspecifico()
    {
        aberto = false;
        acao = null;
    }

    public override void SetarComponenteAdaptavel(GameObject G, int indice)
    {
        G.GetComponent<DadosDeMiniaturas>().SetarDados(dados, indice, tipo, acao);
    }

    public void IniciarHud(
        DadosDoPersonagem dados,
        TipoDeDado tipo,
        int quantidade,System.Action<int> acao,
        float tempoParaFechar,
        TipoDeRedimensionamento tipoDeR = TipoDeRedimensionamento.vertical)
    {
        this.dados = dados;
        this.tipo = tipo;
        this.acao = acao;
        this.tempoParaFechar = tempoParaFechar;
        deslocamento = 0;
        estePainel = painelDeTamanhoVariavel.parent.parent.gameObject;
        tempoDecorrido = 0;
        aberto = true;        
        IniciarHUD(quantidade,tipoDeR);

        
    }

    public int LineCellCount()
    {
        GridLayoutGroup grid = painelDeTamanhoVariavel.GetComponent<GridLayoutGroup>();

        return
            (int)(painelDeTamanhoVariavel.rect.width / (grid.cellSize.x + grid.spacing.x));
    }

    public int RowCellCount()
    {
        GridLayoutGroup grid = painelDeTamanhoVariavel.GetComponent<GridLayoutGroup>();

        return
            (int)(painelDeTamanhoVariavel.rect.height / (grid.cellSize.y + grid.spacing.y));
    }

    public void Update()
    {
        if (tempoParaFechar > 0)
        {
            tempoDecorrido += Time.deltaTime;
            if (tempoDecorrido > tempoParaFechar)
                FinalizarHud();

        }

        if(estePainel!=null)
            if (estePainel.activeSelf)
            {
                VerificaDeslocamento();
                VerificaUso();
            }
    }

    void VerificaUso()
    {
        for (int i = 0; i < 5; i++)
            if ((Input.GetKeyDown((KeyCode)(49 + i)) || Input.GetKeyDown((KeyCode)(257 + i))) && painelDeTamanhoVariavel.childCount > i + deslocamento+1)
                painelDeTamanhoVariavel.GetChild(i + deslocamento + 1).GetComponent<DadosDeMiniaturas>().FuncaoDoBotao();
    }

    void VerificaDeslocamento()
    {
        
        if (Input.GetButtonDown("deslocadorDown")&& deslocamento< painelDeTamanhoVariavel.childCount-6)
        {
            deslocamento++;
            AtualizarDeslocamentos();
        }

        if (Input.GetButtonDown("deslocadorUp")&&deslocamento>0)
        {
            deslocamento--;
            AtualizarDeslocamentos();
        }
    }

    void AtualizarDeslocamentos()
    {
        int numFIlhos = painelDeTamanhoVariavel.childCount;

        for (int i = 1; i < numFIlhos; i++)
        {
            // painelDeTamanhoVariavel.GetChild(i).GetComponent<DadosDeMiniaturas>().AtualizaTxtDoBtn(deslocamento);
            sr.verticalScrollbar.value = (float)deslocamento / (painelDeTamanhoVariavel.childCount-6);
        }
    }
}
