using UnityEngine;
using System.Collections;

public class MbMudeCena : MonoBehaviour
{
    [SerializeField]private Vector3 posicao;
    [SerializeField]private NomesCenas[] cenasParaCarregar;
    [SerializeField]private float olhePraLa;
    [SerializeField]private Color corDoFade = Color.black;

    private bool iniciarCarregamento = false;
    private FadeView p;
    private Vector3 dirDeMove;

    // Use this for initialization
    void Start()
    {
        gameObject.tag = "cenario";
        gameObject.layer = 9;
    }

    // Update is called once per frame
    void Update()
    {
        if (iniciarCarregamento)
        {
            
            GameController.g.ReiniciarContadorDeEncontro();
            GameController.g.Manager.Mov.AplicadorDeMovimentos(dirDeMove);
        }
    }

    void IniciarCarregamentoDeCena()
    {
        iniciarCarregamento = true;
        GameController.EntrarNoFluxoDeTexto();
        //p = GameController.g.gameObject.AddComponent<FadeView>();
        GlobalController.g.FadeV.IniciarFadeOut(corDoFade);
        EventAgregator.AddListener(EventKey.fadeOutComplete, OnFadeOutComplete);
        GameController.g.Manager.Estado = EstadoDePersonagem.movimentoDeFora;
    }

    private void OnDestroy()
    {         
        EventAgregator.RemoveListener(EventKey.fadeOutComplete, OnFadeOutComplete);
    }


    void OnFadeOutComplete(IGameEvent e)
    {
       
        CharacterManager manager = GameController.g.Manager;
        manager.transform.position = posicao;
        manager.transform.rotation = Quaternion.Euler(0, olhePraLa, 0);
        GameController.g.Salvador.SalvarAgora(cenasParaCarregar);
        GameObject G = new GameObject();
        SceneLoader loadScene = G.AddComponent<SceneLoader>();
        loadScene.CenaDoCarregamento(GameController.g.Salvador.IndiceDoJogoAtual);
        //Destroy(gameObject);
        iniciarCarregamento = false;
        StartCoroutine(RemoveSaporra());
        
    }

    IEnumerator RemoveSaporra()
    {
        yield return new WaitForEndOfFrame();
        OnDestroy();

    }

    

    void OnTriggerEnter(Collider col)
    {
       // if (!heroi.emLuta)
        {
            if (col.tag == "Player")
            {
                DontDestroyOnLoad(gameObject);
                dirDeMove = col.transform.forward;
                IniciarCarregamentoDeCena();
            }

            if (col.tag == "Criature"&&!GameController.g.estaEmLuta)
            {
                EvitaAvancarNoTrigger.Evita();
            }
        }
    }
}
