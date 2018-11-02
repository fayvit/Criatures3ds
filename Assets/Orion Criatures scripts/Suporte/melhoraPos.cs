using UnityEngine;
using System.Collections;

public class melhoraPos{


	public melhoraPos()
	{

	}

	public Vector3 novaPos(Vector3 pos)
	{
		return novaPos(pos,1);
	}

	public Vector3 posEmparedado(Vector3 oQProcura,Vector3 oParado)
	{
		Vector3 retorno = oQProcura;
		oQProcura +=Vector3.up;
		oParado+=Vector3.up;
		RaycastHit hit;
		if(Physics.Linecast (oParado,oQProcura,out hit))
		{
			if(Vector3.Angle(hit.normal,Vector3.ProjectOnPlane(hit.normal,Vector3.up))<5)
			{
				retorno = novaPos(hit.point+hit.normal,1);
				Debug.LogWarning("[melhoraPos] angulo Menor que 10 "+hit.collider. name);
			}

			//Debug.Log(hit.collider.gameObject+" o angulo e "+Vector3.Angle(hit.normal,oQProcura-oParado));
		}

		return retorno;
	}

	public Vector3 novaPos(Vector3 pos, float escala, string terra = "Terrain")
	{
		Vector3 retorno = pos;
		RaycastHit hit = new RaycastHit();

		if(Physics.Raycast (pos+Vector3.up,Vector3.down,out hit ))
		{
			terra = hit.collider.name;
			if(terra=="gambiarraSeguraQueda"||terra=="cilindroEncontro"||terra=="segundaGambiarra")
				terra = "Terrain";
//			Debug.Log(terra+" : "+hit.collider.name);
		}

		if(Physics.Raycast(pos,Vector3.up,out hit))
			if(hit.transform.name == terra || hit.transform.tag=="cenario")
            {

                //Debug.Log("acima: " + hit.transform.name + " : " + hit.transform.tag);
				retorno = hit.point+escala*Vector3.up;


		}

		if(Physics.Raycast(pos+0.1f*Vector3.up,Vector3.down,out hit))
			if(hit.transform.name == terra || hit.transform.tag == "cenario")
            {
              //  Debug.Log("abaixo: " + hit.transform.name + " : " + hit.transform.tag);
                retorno = hit.point+escala*Vector3.up;
		}

		return retorno;
	}
}
