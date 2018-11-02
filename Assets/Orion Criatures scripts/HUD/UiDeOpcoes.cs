using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class UiDeOpcoes
{
    [SerializeField] protected GameObject itemDoContainer;
    [SerializeField] protected RectTransform painelDeTamanhoVariavel;
    [SerializeField] protected ScrollRect sr;

    //private CommandReader commandR;

    public abstract void SetarComponenteAdaptavel(GameObject G, int indice);
    protected abstract void FinalizarEspecifico();

    private int opcaoEscolhida = 0;
    private float contadorDeTempo = 0;
    private const float TEMPO_DE_SCROLL = .15F;

    public int OpcaoEscolhida
    {
        get { return opcaoEscolhida; }
        private set { opcaoEscolhida = value; }
    }

    public bool EstaAtivo
    {
        get { return painelDeTamanhoVariavel.parent.parent.gameObject.activeSelf; }
    }

    protected void IniciarHUD(int quantidade,TipoDeRedimensionamento tipo = TipoDeRedimensionamento.vertical)
    {
        OpcaoEscolhida = 0;
        painelDeTamanhoVariavel.parent.parent.gameObject.SetActive(true);

        itemDoContainer.SetActive(true);

        if(tipo==TipoDeRedimensionamento.vertical)
            RedimensionarUI.NaVertical(painelDeTamanhoVariavel, itemDoContainer, quantidade);
        else if(tipo==TipoDeRedimensionamento.emGrade)
            RedimensionarUI.EmGrade(painelDeTamanhoVariavel, itemDoContainer, quantidade);
        else if(tipo==TipoDeRedimensionamento.horizontal)
            RedimensionarUI.NaHorizontal(painelDeTamanhoVariavel, itemDoContainer, quantidade);

        for (int i = 0; i < quantidade; i++)
        {
            GameObject G = ParentearNaHUD.Parentear(itemDoContainer, painelDeTamanhoVariavel);
            SetarComponenteAdaptavel(G,i);

            G.name += i.ToString();
            if (i == OpcaoEscolhida)
            {
                if (GameController.g != null)
                    G.GetComponent<UmaOpcao>().SpriteDoItem.sprite = GameController.g.El.uiDestaque;
                else
                {
                    Color C;
                    ColorUtility.TryParseHtmlString("#B61B1BFF", out C);
                    G.GetComponent<UmaOpcao>().SpriteDoItem.color = C;
                        }

            }
        }

        itemDoContainer.SetActive(false);

        if (sr != null)
            if (sr.verticalScrollbar)
                sr.verticalScrollbar.value = 1;

        AgendaScrollPos();
        
    }

    void AgendaScrollPos()
    {
        if (GameController.g)
            GameController.g.StartCoroutine(ScrollPos());
        else
            GameObject.FindObjectOfType<InitialSceneManager>().StartCoroutine(ScrollPos());
    }

    public void MudarOpcaoComVal(int quanto,int rowCellCount = -1)
    {
        if (quanto != 0)
        {
            UmaOpcao[] umaS = painelDeTamanhoVariavel.GetComponentsInChildren<UmaOpcao>();

            if (quanto > 0)
            {
                if (OpcaoEscolhida + quanto < umaS.Length)
                    OpcaoEscolhida += quanto;
                else
                    OpcaoEscolhida = 0;
            }
            else if (quanto < 0)
            {
                if (OpcaoEscolhida + quanto >= 0)
                    OpcaoEscolhida += quanto;
                else
                    OpcaoEscolhida = umaS.Length - 1;
            }

            for (int i = 0; i < umaS.Length; i++)
            {
                if (i == OpcaoEscolhida)
                {
                    if (GameController.g != null)
                        umaS[i].GetComponent<UmaOpcao>().SpriteDoItem.sprite = GameController.g.El.uiDestaque;
                    else
                    {
                        Color C;
                        ColorUtility.TryParseHtmlString("#B61B1BFF", out C);
                        umaS[i].GetComponent<UmaOpcao>().SpriteDoItem.color = C;
                    }
                }
                else
                {
                    if (GameController.g != null)
                        umaS[i].GetComponent<UmaOpcao>().SpriteDoItem.sprite = GameController.g.El.uiDefault;
                    else
                    {
                        Color C;
                        ColorUtility.TryParseHtmlString("#1B4AB6FF", out C);
                        umaS[i].GetComponent<UmaOpcao>().SpriteDoItem.color = C;
                    }
                }
                   
            }

            if (sr != null)
                if (sr.verticalScrollbar || sr.horizontalScrollbar)
                {
                    
                    AjeitaScroll(umaS,rowCellCount);
                }
                else
                {
                    Debug.Log("erro scroll 2");
                }

            else
                Debug.Log("erro no scrool");

            EventAgregator.Publish(EventKey.UiDeOpcoesChange, null);
        }
    }

    public virtual void MudarOpcao()
    {
        int quanto = 0;

        quanto = -CommandReader.ValorDeGatilhos("VDpad", GlobalController.g.Control);

        if(quanto==0)
          quanto = -CommandReader.ValorDeGatilhosTeclado("vertical", GlobalController.g.Control);


        MudarOpcaoComVal(quanto);
    }

    public virtual void MudarOpcao_H()
    {
        int quanto = 0;

        quanto = -CommandReader.ValorDeGatilhos("HDpad", GlobalController.g.Control);

        if(quanto==0)
          quanto = -CommandReader.ValorDeGatilhosTeclado("horizontal", GlobalController.g.Control);


        MudarOpcaoComVal(quanto);
    }

    void AjeitaScroll(UmaOpcao[] umaS,int rowCellCount)
    {
        contadorDeTempo = 0;

        if(GameController.g)
            GameController.g.StartCoroutine(MovendoScroll(umaS,rowCellCount));
        else
            MonoBehaviour.FindObjectOfType<InitialSceneManager>().StartCoroutine(MovendoScroll(umaS, rowCellCount));
    }

    /*
    IEnumerator MovendoScroll(UmaOpcao[] umaS,int rowCellCount)
    {
        yield return new WaitForSecondsRealtime(0.01f);
        int val = Mathf.CeilToInt((float)umaS.Length / rowCellCount);
        int opc = Mathf.FloorToInt((float)OpcaoEscolhida/ rowCellCount);
        contadorDeTempo += 0.01f;
        Debug.Log(sr.verticalScrollbar.value+" : "+val);
        sr.verticalScrollbar.value = Mathf.Lerp(sr.verticalScrollbar.value, 
            Mathf.Clamp((float)(val - 2 - opc) / Mathf.Max(val - 2,1), 0, 1),contadorDeTempo/TEMPO_DE_SCROLL);

        if (sr.verticalScrollbar.value != Mathf.Clamp((float)(val - 1 - opc) / Mathf.Max(val - 2, 1), 0, 1))
            GameController.g.StartCoroutine(MovendoScroll(umaS,rowCellCount));
    }*/

    protected virtual IEnumerator MovendoScroll(UmaOpcao[] umaS, int rowCellCount)
    {
        yield return new WaitForSecondsRealtime(0.01f);
        int val = (rowCellCount==-1)?umaS.Length: Mathf.CeilToInt((float)umaS.Length / rowCellCount);
        int opc = OpcaoEscolhida /( (rowCellCount==-1)?1:rowCellCount);
        
        contadorDeTempo += 0.01f;
        float destiny = Mathf.Clamp((float)(val - opc-1) / Mathf.Max(val-1, 1), 0, 1);

        //Debug.Log(OpcaoEscolhida + " : " + rowCellCount + " : " + opc);
        //Debug.Log(sr.verticalScrollbar.value + " : " + val+" : "+destiny);


        sr.verticalScrollbar.value = Mathf.Lerp(sr.verticalScrollbar.value,
            destiny, contadorDeTempo / TEMPO_DE_SCROLL);

        if (sr.verticalScrollbar.value != destiny)
            if (GameController.g)
            {
                GameController.g.StartCoroutine(MovendoScroll(umaS, rowCellCount));
            }
            else
                MonoBehaviour.FindObjectOfType<InitialSceneManager>().StartCoroutine(MovendoScroll(umaS, rowCellCount));
        //GameController.g.StartCoroutine(MovendoScroll(umaS, rowCellCount));
    }

    protected virtual IEnumerator MovendoScroll_H(UmaOpcao[] umaS, int rowCellCount)
    {
        yield return new WaitForSecondsRealtime(0.01f);
        int val = (rowCellCount == -1) ? umaS.Length : Mathf.CeilToInt((float)umaS.Length / rowCellCount);
        int opc = OpcaoEscolhida / ((rowCellCount == -1) ? 1 : rowCellCount);

        contadorDeTempo += 0.01f;
        float destiny = 1-Mathf.Clamp((float)(val - opc - 1) / Mathf.Max(val - 1, 1), 0, 1);

        //Debug.Log(OpcaoEscolhida + " : " + rowCellCount + " : " + opc);
        //Debug.Log(sr.verticalScrollbar.value + " : " + val+" : "+destiny);


        sr.horizontalScrollbar.value = Mathf.Lerp(sr.horizontalScrollbar.value,
            destiny, contadorDeTempo / TEMPO_DE_SCROLL);

        if (sr.horizontalScrollbar.value != destiny)
            if (GameController.g)
            {
                GameController.g.StartCoroutine(MovendoScroll_H(umaS, rowCellCount));
            }
            else
                MonoBehaviour.FindObjectOfType<InitialSceneManager>().StartCoroutine(MovendoScroll_H(umaS, rowCellCount));
        //GameController.g.StartCoroutine(MovendoScroll(umaS, rowCellCount));
    }



    IEnumerator ScrollPos()
    {
        yield return new WaitForSecondsRealtime(0.01f);

        if (sr != null)
            if (sr.verticalScrollbar)
            {
                sr.verticalScrollbar.value = 1;
            }


        if (sr != null)
            if (sr.horizontalScrollbar)
                sr.horizontalScrollbar.value = 0;

        yield return new WaitForEndOfFrame();

        if (sr != null)
            if (sr.verticalScrollbar)
            {
                if (sr.verticalScrollbar.value != 1)
                    AgendaScrollPos();
            }


        if (sr != null)
            if (sr.horizontalScrollbar)
                if (sr.horizontalScrollbar.value != 0)
                    AgendaScrollPos();

    }

    public void FinalizarHud()
    {
        for (int i = 1; i < painelDeTamanhoVariavel.transform.childCount; i++)
        {
            MonoBehaviour.Destroy(painelDeTamanhoVariavel.GetChild(i).gameObject);
            painelDeTamanhoVariavel.parent.parent.gameObject.SetActive(false);
        }

        FinalizarEspecifico();
    }

    
}
