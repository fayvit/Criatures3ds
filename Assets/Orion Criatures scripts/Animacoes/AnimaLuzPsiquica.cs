using UnityEngine;
using System.Collections;

public class AnimaLuzPsiquica : MonoBehaviour
{

    private Light L;
    private float luminosidade;
    private bool clareando = false;
    // Use this for initialization
    void Start()
    {
        L = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!clareando)
        {
            luminosidade = Mathf.Lerp(luminosidade, 1, 5 * Time.deltaTime);
            if (luminosidade - 1 < 1)
                clareando = true;
        }
        else
        {
            luminosidade = Mathf.Lerp(luminosidade, 15, 5 * Time.deltaTime);
            if (15 - luminosidade < 1)
                clareando = false;
        }

        L.intensity = luminosidade;

    }
}
