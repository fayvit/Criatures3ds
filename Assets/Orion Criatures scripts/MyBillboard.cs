using UnityEngine;
using System.Collections;

public class MyBillboard : MeuBillboard
{

    public Texture2D[] Tex
    {
        get { return Images; }
        set { Images = value; }
    }

    // Use this for initialization
    new void Start()
    {
        if (AplicadorDeCamera.cam != null)
        {
            base.Start();
            AplicadorDeCamera.cam.enabled = false;
            SetarCameraPrincipal();
            new BilboardGeneration().GetBillboardArray(this, gameObject);
        }
        else
            Invoke("Start", 0.15f);
    }

    void SetarCameraPrincipal()
    {
        //cam = Camera.main;
    }
}
