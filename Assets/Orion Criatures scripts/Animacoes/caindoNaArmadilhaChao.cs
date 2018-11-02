using UnityEngine;
using System.Collections;

public class caindoNaArmadilhaChao : MonoBehaviour {

	public Vector3 posAlvo;
    public DoJogo nomePrefab = DoJogo.acaoDeCura1;//"caindoNaArmadilhaChao";

	private bool iniciou = false;
	private Animator animator;
	private FadeView p;
	private float tempoDecorrido = 0;
	private fasesDaQueda fase = fasesDaQueda.animaInicial;

	private SigaOLider siga;
	private UnityEngine.AI.NavMeshAgent nav;

	private enum fasesDaQueda
	{
		animaInicial,
		colocouPretoMorte,
		tiraPretoMorte,
		levantando
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(iniciou)
		{
			tempoDecorrido+=Time.deltaTime;
			switch(fase)
			{
			case fasesDaQueda.animaInicial:
				if(tempoDecorrido<0.5f)
				{
					GameController.g.Manager.transform.position -= 5*Vector3.up*Time.deltaTime;
				}else
				{
					tempoDecorrido = 0;
					p = gameObject.AddComponent<FadeView>();
					fase = fasesDaQueda.colocouPretoMorte;
				}
			break;
			case fasesDaQueda.colocouPretoMorte:
				if(tempoDecorrido>1f)
				{
					animator.Play("damage_25");
					p.entrando = false;
					fase = fasesDaQueda.tiraPretoMorte;
					GameController.g.Manager.transform.position = posAlvo;
                    GameController.g.ReiniciarContadorDeEncontro();
				}
			break;
			case fasesDaQueda.tiraPretoMorte:
				if(GameController.g.Manager.Mov.NoChao(0.1f))
				{
					animator.Play("getup_20_p");
                    Destroy(GameController.g.Manager.CriatureAtivo.gameObject);
                    //CreatureManager T = GameController.g.Manager.CriatureAtivo;
                    //T.PararCriatureNoLocal();
                    //nav = T.GetComponent<UnityEngine.AI.NavMeshAgent>();
                    //siga = T.GetComponent<sigaOLider>();
                    //siga.enabled = false;
                    //nav.enabled = false;
                    GameController.g.Manager.InserirCriatureEmJogo();
					fase = fasesDaQueda.levantando;
				}
			break;
			case fasesDaQueda.levantando:
				if(tempoDecorrido>0.25f)
				{
                    GameController.g.Manager.AoHeroi();
					iniciou = false;
					fase = fasesDaQueda.animaInicial;
                        
                    //nav.enabled = true;
                    //siga.enabled = true;
                }
			break;
			}

		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag=="Player" && !GameController.g.estaEmLuta&& !iniciou)
		{
			iniciou = true;
            GameController.g.Manager.Estado = EstadoDePersonagem.parado;
			animator = col.GetComponent<Animator>();
			Destroy(
			Instantiate(
				GameController.g.El.retorna(nomePrefab),
				col.transform.position,
				Quaternion.identity
				),5);


			animator.Play("damage_25_2");
		}
	}
}
