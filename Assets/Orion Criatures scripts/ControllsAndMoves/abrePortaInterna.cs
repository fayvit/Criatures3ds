using UnityEngine;
using System.Collections;

public class abrePortaInterna : AtivadorDeBotao
{
	public Transform baseDaPorta;

	private bool estaAberta = false;
	private bool estaAbrindo = false;

	private int dir = 1;
	private float tempoComPortaAberta = 0;
	// Use this for initialization
	void Start () {
        textoDoBotao = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.textoBaseDeAcao)[1];
    }
	
	// Update is called once per frame
	new protected void Update () {
        //print(Vector3.Distance(transform.position,tHeroi.position)+" : "+podeAbrir);

        base.Update();

		if(estaAbrindo)
		{
			if(!estouAbrindoAPorta(transform,baseDaPorta,1,dir))
			{
				estaAbrindo = false;
				estaAberta = true;
                GameController.g.Manager.AoHeroi();
            }
		}

		if(estaAberta)
		{
			tempoComPortaAberta+=Time.deltaTime;
			if(tempoComPortaAberta>5f)
			{
				if(!estouFechandoAPorta(transform,baseDaPorta,1,dir))
				{
					tempoComPortaAberta=0;
					estaAberta = false;
				}
			}
		}
	}

	public static bool estouFechandoAPorta(Transform porta,Transform baseDaPorta,int deComparacao = 0,int dir = 1)
	{
		Vector3 comparavel = vetorComparavel(porta,deComparacao,dir);
		if(Vector3.Angle(comparavel,Vector3.up)<90)
			porta.RotateAround(baseDaPorta.position,baseDaPorta.up,-dir*25*Time.deltaTime);
		else{
			return false;
		}
		
		return true;
	}

	static Vector3 vetorComparavel(Transform T,int deComparacao,int dir)
	{
		/*
			Apliquei a funçao estouAbrindoAPOrta em dois blocos 
			que tinham o forward apontando para direçoes diferentes
			entao precisei saber com qual vetor comparar para parar o giro
		 */


		Vector3 retorno = T.up;
		
		switch(deComparacao)
		{
		case 0:
			retorno = T.up; 
		break;
		case 1:
			retorno = dir*T.forward;
		break;
		}

		return retorno;
	}

	public static bool estouAbrindoAPorta(Transform porta,Transform baseDaPorta,int deComparacao = 0,int dir = 1)
	{
        ElementosDoJogo El = GameController.g.El;
		Vector3 V = porta.position-4*Vector3.up;
		Vector3 comparavel = vetorComparavel(porta,deComparacao,dir);

		if(((int)Vector3.Angle(comparavel,Vector3.up))%7==0)
		for(int i = 0;i<5;i++)
		{
		
			Destroy(Instantiate(
				El.retorna("poeiraAoVento"),
				V+i*2*
				((deComparacao==0 && dir==1)?baseDaPorta.forward:baseDaPorta.up),
				Quaternion.identity
				),2);
			Destroy(Instantiate(
				El.retorna("poeiraAoVento"),
				V-i*2*
				((deComparacao==0 && dir==1)?baseDaPorta.forward:baseDaPorta.up),
				Quaternion.identity
				),2);
		}


		if(Vector3.Angle(comparavel,Vector3.up)>1)
			porta.RotateAround(baseDaPorta.position,
			  (deComparacao==0)?-baseDaPorta.forward : baseDaPorta.up,dir*25*Time.deltaTime);
		else{
			return false;
		}

		return true;
	}

    public override void FuncaoDoBotao()
    {
        FluxoDeBotao();
        estaAbrindo = true;
        Transform tHeroi = GameController.g.Manager.transform;
        if (Vector3.Dot(transform.position - tHeroi.position, transform.forward) > 0)
            dir = 1;
        else
            dir = -1;
    }
}
