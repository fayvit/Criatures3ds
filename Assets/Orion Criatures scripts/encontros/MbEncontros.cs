using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MbEncontros
{
    [SerializeField]private int minEncontro = 50;
    [SerializeField]private int maxEncontro = 300;
    [SerializeField]private float andado = 0;
    [SerializeField]private float proxEncontro = 100;
    [SerializeField] private bool contarPassos = true;

    private Vector3 posHeroi;
    private Vector3 posAnterior = Vector3.zero;
    private CharacterManager manager;
    private Encontravel encontrado;
    private EncounterManager gerenteDeEncontro;
    private bool luta = false;
    private bool contraTreinador = false;

    public bool ContaTreinador
    {
        get { return contraTreinador; }
    }

    public bool Luta
    {
        get{return luta;}
    }

    public bool ContarPassos
    {
        get { return contarPassos; }
        set { contarPassos = value; }
    }

    public CreatureManager InimigoAtivo
    {
        get { return gerenteDeEncontro.Inimigo; }
    }
    // Use this for initialization
    public void Start()
    {
        manager = MonoBehaviour.FindObjectOfType<CharacterManager>();
        posAnterior = manager.transform.position;
        gerenteDeEncontro = new EncounterManager();
        proxEncontro = SorteiaPassosParaEncontro();
    }

    // Update is called once per frame
    public void Update()
    {
        
        //if (!pausaJogo.pause)
        {
            if (!luta)
                posHeroi = manager.transform.position;

            //  if (!heroiMB)
            //    heroiMB = tHeroi.GetComponent<movimentoBasico>();

            if (!LugarSeguro() && !luta && MovimentacaoBasica.noChaoS(manager.Mov.Controle,0.01f) && contarPassos)
            {
                andado += (posHeroi - posAnterior).magnitude;
                posAnterior = posHeroi;
            }


            if (!luta && andado >= proxEncontro )
            {
                IniciaEncontro();
            }

            if (gerenteDeEncontro.Update() && luta)
            {
                
                RetornaParaModoPasseio();
                AplicadorDeCamera.cam.GetComponent<Camera>().farClipPlane = 1000;
                GameController.g.Salvador.SalvarAgora();
                //System.GC.Collect();
                //Resources.UnloadUnusedAssets();
            }


            Debug.DrawRay(posHeroi - 40f * manager.transform.forward, 1000f * Vector3.up, Color.yellow);
        }
    }

    public void FinalizaEncontro(bool treinador)
    {
        gerenteDeEncontro.FinalizarEncontro(treinador);
    }

    public void IniciarEncontroCom(CreatureManager c,bool treinador,string nomeTreinador = "")
    {
        gerenteDeEncontro.InicializarEncounterManager(c, manager,treinador,nomeTreinador);
        contraTreinador = treinador;
        if (gerenteDeEncontro.Inimigo)
        {
            luta = true;
            AplicadorDeCamera.cam.GetComponent<Camera>().farClipPlane = 100;
            GameController.g.FinalizaHuds();

            if(!treinador)
                InsereElementosDoEncontro.encontroPadrao(manager);

            GameController.g.HudM.ModoLimpo();
        }
        else
        {
            Debug.Log("não foram encontrados criatures compativeis com esse encontro");
        }
    }

    void IniciaEncontro()
    { 
        andado = 0;
        proxEncontro = SorteiaPassosParaEncontro();
        encontrado = criatureEncontrado();
        IniciarEncontroCom(InsereInimigoEmCampo.RetornaInimigoEmCampo(encontrado, manager),false);

    }

    void RetornaParaModoPasseio()
    {
        MonoBehaviour.Destroy(GameObject.Find("cilindroEncontro"));
        //GameController.g.HudM.Btns.btnParaCriature.interactable = true;
        //heroi.emLuta = false;
        luta = false;        
        manager.AoHeroi();        
        manager.transform.position = posHeroi;
        
    }

    protected virtual bool LugarSeguro()
    {

        return ListaDeLocaisSeguros.LocalSeguro();
    }

    protected float SorteiaPassosParaEncontro()
    {
        return Random.Range(minEncontro, maxEncontro);
    }

    protected virtual List<Encontravel> listaEncontravel()
    {
        //return new List<Encontravel>() { new Encontravel(nomesCriatures.Nessei, 1, 3, 5) };//ListaDeEncontraveis.EncontraveisDaqui;
        return ListaDeEncontraveis.EncontraveisDaqui;
    }

    Encontravel criatureEncontrado()
    {
        List<Encontravel> encontraveis = listaEncontravel();//secaoEncontravel[IndiceDoLocal()];
        float maiorAleatorio = 0;
        for (int i = 0; i < encontraveis.Count; i++)
        {
            maiorAleatorio += encontraveis[i].taxa;
        }
        //		print(maiorAleatorio);
        float roleta = Random.Range(0, maiorAleatorio);
        //		print(roleta);
        float sum = 0;
        int retorno = -1;
        for (int i = 0; i < encontraveis.Count; i++)
        {
            sum += encontraveis[i].taxa;

            if (roleta <= sum && retorno == -1)
                retorno = i;
        }
        if(encontraveis.Count>0)
            return encontraveis[retorno];
        else
            return new Encontravel();
    }

    public void ZeraPosAnterior()
    {
        posAnterior = manager.transform.position;
        posHeroi = manager.transform.position;
        andado = 0;
    }


    /*
    Funções originalmente criadas para testes
        */

    #region TestRegion
    public void ZerarPassosParaProxEncontro()
    {
        proxEncontro = 0;
    }

    public void ColocarUmPvNoInimigo()
    {
        gerenteDeEncontro.Inimigo.MeuCriatureBase.CaracCriature.meusAtributos.PV.Corrente = 1;
    }

    #endregion
}
