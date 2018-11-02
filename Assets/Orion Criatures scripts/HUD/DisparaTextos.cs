using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class DisparaTexto
{
    [SerializeField] private int velocidadeDasLetras = 50;
    [SerializeField] private int velocidadeDaJanela = 15;
    [SerializeField] private RectTransform painelDaMens;
    [SerializeField] private GameObject painelDaPressa;

    private Text textoDaUI;    
    private Image img;
    
    private Vector2 posOriginal;
    private FasesDaMensagem fase = FasesDaMensagem.caixaIndo;

    private string texto = "";
    private float contadorDeTempo = 0;
    private bool dispara = false;
    private int indiceDaConversa = 0;
    

    public enum FasesDaMensagem
    {
        caixaIndo,
        mensagemEnchendo,
        mensagemCheia,
        caixaSaindo,
        caixaSaiu
    }

    public int IndiceDaConversa
    {
        get { return indiceDaConversa; }
        set { indiceDaConversa = value; }
    }

    public void IniciarDisparadorDeTextos(bool pressa = false)
    {
        
        painelDaPressa.SetActive(pressa);

        dispara = false;
        SetarComponetes();
        indiceDaConversa = 0;
    }

    public bool UpdateDeTextos(string[] conversa,Sprite foto = null)
    {
        if (indiceDaConversa < conversa.Length)
        {
            Dispara(conversa[indiceDaConversa], foto);
        }
        else
        {
            painelDaPressa.SetActive(false);
            return true;
        }

        if (LendoMensagem() == FasesDaMensagem.caixaSaiu)
        {
            indiceDaConversa++;
        }


        if (CommandReader.ButtonDown(0, GameController.g.Manager.Control))
        {
            Debug.Log("controlador de acao removido");
            Toque();
        }

        if (CommandReader.ButtonDown(1,GameController.g.Manager.Control) && painelDaPressa.activeSelf)
        {
            ActionManager.useiCancel = true;
            DesligarPaineis();
            return true;
        }

        return false;
    }

    void SetarComponetes()
    {
        if (textoDaUI == null)
        {
            textoDaUI = painelDaMens.GetComponentInChildren<Text>();
            img = painelDaMens.transform.GetChild(1).GetComponent<Image>();

            posOriginal = painelDaMens.anchoredPosition;
        }
    }

    public void Dispara(string texto, Sprite sDaFoto = null)
    {
        if (!dispara)
        {            

            dispara = true;
            painelDaMens.gameObject.SetActive(true);
            painelDaMens.anchoredPosition = new Vector2(posOriginal.x, Screen.height);
            textoDaUI.text = "";
            if (sDaFoto!=null)
            {
                img.sprite = sDaFoto;
            }
            else
                img.enabled = false;

            fase = FasesDaMensagem.caixaIndo;
            this.texto = texto;
            
        }
    }

    public bool LendoMensagemAteOCheia()
    {
        if (LendoMensagem() != FasesDaMensagem.mensagemCheia)
        {
            if (CommandReader.ButtonDown(0, GameController.g.Manager.Control))
            {
                Debug.Log("controlador de acao removido");
                Toque();
            }
            return true;
        }
        else
            return false;
    }

    public FasesDaMensagem LendoMensagem()
    {
        contadorDeTempo += Time.deltaTime;
        if (dispara)
        {
            switch (fase)
            {
                case FasesDaMensagem.caixaIndo:
                    if (Vector2.Distance(painelDaMens.anchoredPosition, posOriginal) > 0.1f)
                    {
                        painelDaMens.anchoredPosition = Vector2.Lerp(
                            painelDaMens.anchoredPosition, posOriginal, Time.deltaTime * velocidadeDaJanela);
                    }
                    else
                    {
                        fase = FasesDaMensagem.mensagemEnchendo;
                        contadorDeTempo = 0;
                    }
                    break;
                case FasesDaMensagem.mensagemEnchendo:
                    if ((int)(contadorDeTempo * velocidadeDasLetras) <= texto.Length  && !texto.Contains("<co"))
                        textoDaUI.text = texto.Substring(0, (int)(contadorDeTempo * velocidadeDasLetras));
                    else
                    {
                        fase = FasesDaMensagem.mensagemCheia;
                        textoDaUI.text = texto;
                    }
                    break;
                case FasesDaMensagem.caixaSaindo:
                    if (Mathf.Abs(painelDaMens.anchoredPosition.y - Screen.height) > 0.1f)
                    {
                        painelDaMens.anchoredPosition = Vector2.Lerp(painelDaMens.anchoredPosition,
                                                            new Vector2(painelDaMens.anchoredPosition.x, Screen.height),
                                                            Time.deltaTime * velocidadeDaJanela);
                    }
                    else
                    {
                        dispara = false;
                        fase = FasesDaMensagem.caixaSaiu;
                    }
                    break;
            }


        }
        return fase;
    }

    public void Toque()
    {
        switch (fase)
        {
            case FasesDaMensagem.mensagemEnchendo:
                textoDaUI.text = texto;
                fase = FasesDaMensagem.mensagemCheia;
            break;
            case FasesDaMensagem.mensagemCheia:
                fase = FasesDaMensagem.caixaSaindo;
                contadorDeTempo = 0;
            break;
        }
    }

    public void ReligarPaineis()
    {
        dispara = false;
        indiceDaConversa = 0;
        painelDaMens.gameObject.SetActive(true);
    }

    public void DesligarPaineis()
    {
        painelDaPressa.SetActive(false);
        painelDaMens.gameObject.SetActive(false);
    }
}
