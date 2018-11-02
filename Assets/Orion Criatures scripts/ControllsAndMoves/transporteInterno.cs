using UnityEngine;
using System.Collections;

public class transporteInterno : MonoBehaviour {

	public Vector3 posAlvo;
	public Color corDoFade = Color.black;
	public int rotacaoAlvo = 1;

	private bool iniciou = false;
	private faseDoTransporte fase;
	private FadeView p;
    private Vector3 dirDeMove = Vector3.zero;
	private float tempoDeCorrido = 0;
	private Transform T;

	//private encontros e;

	private enum faseDoTransporte
	{
		iniciando,
		retornando
	}
	// Use this for initialization
	protected void Start () {
		//e = GameObject.Find("Terrain").GetComponent<encontros>();
	}

	protected virtual void iniciandoTransporte()
	{
		p.entrando = false;
		T.position = posAlvo;
		if(rotacaoAlvo!=1)
			T.rotation = Quaternion.Euler(0,rotacaoAlvo,0);



        
        fase = faseDoTransporte.retornando;
		tempoDeCorrido = 0;
		Destroy(GameObject.Find("CriatureAtivo"));
        GameController.g.Manager.InserirCriatureEmJogo();
        GameController.g.ReiniciarContadorDeEncontro();
        GameController.EntrarNoFluxoDeTexto();


    }

	protected virtual void terminandoOTransporte()
	{
        GameController.g.Manager.AoHeroi();
		iniciou = false;
		fase = faseDoTransporte.iniciando;
		tempoDeCorrido = 0;
	}
	
	// Update is called once per frame
	protected void Update () {
		if(iniciou)
			{
			tempoDeCorrido+=Time.deltaTime;

			switch(fase)
			{
			case faseDoTransporte.iniciando:
                GameController.g.Manager.Mov.AplicadorDeMovimentos(dirDeMove);
                if (tempoDeCorrido>1.5f)
				{
					iniciandoTransporte();
				}
			break;
			case faseDoTransporte.retornando:
				if(tempoDeCorrido>0.65f)
				{
					terminandoOTransporte();
				}
			break;
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag=="Player" && !GameController.g.estaEmLuta  && !iniciou)
		{
			iniciou = true;
            GameController.g.Manager.Estado = EstadoDePersonagem.movimentoDeFora;
            dirDeMove = col.transform.forward;
            p = gameObject.AddComponent<FadeView>();
			p.cor = corDoFade;
			T = col.transform;
		}

		if(col.tag=="Criature" && !GameController.g.estaEmLuta)
		{
            GameController.g.Manager.AoHeroi();
		}
	}
}
