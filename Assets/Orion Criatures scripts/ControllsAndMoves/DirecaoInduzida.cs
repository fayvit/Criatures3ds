using UnityEngine;
using System.Collections;

public class DirecaoInduzida
{
    private bool focoPraMove = false;
    private bool focoEra = false;
    private float guardeH;
    private float guardeV;
    private Vector3 direcaoGuardada;

    public Vector3 Direcao(bool focando, Transform cameraTransform, float h, float v)
    {
        Vector3 retorno;

        if (!focoPraMove)
        {
            focoPraMove = focando;
            if (focoEra != focoPraMove)
            {
                direcaoGuardada = cameraTransform.TransformDirection(Vector3.forward);
                guardeH = h;
                guardeV = v;

            }
            retorno = direcaoGuardada;
        }
        else
        {
            direcaoGuardada = Vector3.Lerp(direcaoGuardada, cameraTransform.TransformDirection(Vector3.forward), 0.25f * Time.deltaTime);
//            if (Vector3.Distance(direcaoGuardada, cameraTransform.TransformDirection(Vector3.forward)) < 0.1f)
  //              focoPraMove = false;
            retorno = direcaoGuardada;
        }

        if (focoPraMove)
        {
            if( (h == 0 && v == 0) || (Mathf.Abs(guardeH - h) > 0.3f && Mathf.Abs(guardeV - v) > 0.3f))
            {
                focoPraMove = false;
                retorno = cameraTransform.TransformDirection(Vector3.forward);
            }
        }
        else
        {

            retorno = cameraTransform.TransformDirection(Vector3.forward);
        }



        focoEra = focoPraMove;


        return retorno;
    }
}
