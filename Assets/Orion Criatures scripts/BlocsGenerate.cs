using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocsGenerate : MonoBehaviour {

    [SerializeField] private GameObject instanciavel;
    [SerializeField] private Texture2D img;
    [SerializeField] private bool vai;
    [SerializeField] private bool inverter = false;
    [SerializeField] private int xMin = 0;
    [SerializeField] private int xCont = 10;
    [SerializeField] private int zMin = 0;
    [SerializeField] private int zCont = 10;
    [SerializeField] private int alturaMin = 0;
    [SerializeField] private int alturaMax = 5;
    [SerializeField] private int taxaDePixels = 1;
    [SerializeField] private Vector2 origem = Vector2.zero;
    [SerializeField] private Transform pai;

    private Vector3 tamInstanciavel = Vector3.zero;
	// Use this for initialization
	void Start () {
        Bounds B = instanciavel.GetComponent<MeshRenderer>().bounds;

        tamInstanciavel = B.max - B.min;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (vai)
        {
            Bounds B = instanciavel.GetComponent<MeshRenderer>().bounds;

            tamInstanciavel = B.max - B.min;

            for (int i = 0; i < xCont; i++)
                for (int j = 0; j < zCont; j++)
                {
                    //if(i<xCont && j<zCont)
                    ColoqueBloco(i,j);
                }

            vai = false;
        }
	}

    void ColoqueBloco(int i, int j)
    {
        float multiplicador = inverter
            ?
            img.GetPixel((int)(i * (float)255 / xCont), (int)(j * (float)255 / zCont)).r
        :
        (1 - img.GetPixel((int)(i * (float)255 / xCont), (int)(j * (float)255 / zCont)).r);

        float altura = tamInstanciavel.y *
            (int)(alturaMin + (alturaMax - alturaMin) * multiplicador);
            //(1 - img.GetPixel((int)origem.x + i * taxaDePixels, (int)origem.y + j * taxaDePixels).r));
        Vector3 pos = new Vector3(xMin+i*tamInstanciavel.x,altura,zMin + j * tamInstanciavel.z);
        Instantiate(instanciavel, pos, Quaternion.identity,pai);

        if ((i == 0 || j == 0 || i == xCont - 1 || j == zCont - 1) && altura <= alturaMax*tamInstanciavel.y)
            TermineAteAltura(pos);
    }

    void TermineAteAltura(Vector3 pos)
    {
        if (inverter)
        {
            while (pos.y > alturaMin * tamInstanciavel.y)
            {
                pos -= new Vector3(0, tamInstanciavel.y, 0);
                Instantiate(instanciavel, pos, Quaternion.identity, pai);
            }
        } else
        while (pos.y < alturaMax*tamInstanciavel.y)
        {
            pos += new Vector3(0,tamInstanciavel.y,0);
            Instantiate(instanciavel, pos, Quaternion.identity,pai);
        }
    }
}
