using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraContraParedes {


    static void RaiosVisualizadores(Transform cameraP,Transform alvo,float escalA)
    {
        Debug.DrawLine(cameraP.position, alvo.position + escalA * Vector3.up, Color.blue);


        Debug.DrawLine(alvo.position + 2 * Vector3.up, alvo.position -
                       Vector3.Project(alvo.position - cameraP.position, alvo.forward) + 2 * Vector3.up,
                       Color.green);
    }

    public static bool ContraParedes(Transform cameraP, Transform alvo, float escalA, bool suave = false)
    {
        return ContraParedes(cameraP.position, cameraP, alvo, escalA, suave);
    }

    public static bool ContraParedes(Vector3 lineOrigin,Transform cameraP, Transform alvo, float escalA, bool suave = false)
    {
        RaiosVisualizadores(cameraP,alvo,escalA);

        RaycastHit raioColisor;
        
        if (Physics.Linecast(alvo.position + escalA * Vector3.up, lineOrigin, out raioColisor, 9))
        {
            Debug.DrawLine(cameraP.position, raioColisor.point, Color.red);

            if (raioColisor.transform.tag != "Player"
               &&
               raioColisor.transform.tag != "Criature"
               &&
               raioColisor.transform.tag != "desvieCamera"
               )
            {
                if (suave)
                {
                    cameraP.position = Vector3.Lerp(cameraP.position,
                        raioColisor.point - raioColisor.normal * 0.3f, 25 * Time.deltaTime);
                }
                else
                    cameraP.position = 
                        raioColisor.point + cameraP.forward * 0.2f;
               
                return true;
            }

        }

        return false;
    }

    public static bool VerificaParedeNoCaminho(Vector3 posDaCamera,Vector3 posAlvoComSuaEscala)
    {
        RaycastHit raioColisor;

        if (Physics.Linecast(posAlvoComSuaEscala, posDaCamera, out raioColisor, 9))
        {

            if (raioColisor.transform.tag != "Player"
               &&
               raioColisor.transform.tag != "Criature"
               &&
               raioColisor.transform.tag != "desvieCamera"
               )
            {
                return true;
            }
        }

        return false;
    }
}
