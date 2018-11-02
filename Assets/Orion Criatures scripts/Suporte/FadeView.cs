using UnityEngine;
using System.Collections;

public class FadeView : MonoBehaviour
{

    private float lerp = 0;
    public float vel = 1;
    public bool entrando = true;

    public Color cor = Color.black;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (entrando)
            lerp = Mathf.Lerp(lerp, 1, vel * Time.fixedDeltaTime);
        else
            lerp = Mathf.Lerp(lerp, 0, 0.5f * vel * Time.fixedDeltaTime);

        GUIStyle verm = new GUIStyle(GUI.skin.box);
        Texture2D vermelha = new Texture2D(10, 10);
        for (int i = 0; i < 20; i++)
            for (int j = 0; j < 20; j++)
                vermelha.SetPixel(i, j, new Color(cor.r, cor.g, cor.b, lerp));
        vermelha.Apply();
        verm.normal.background = vermelha;
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", verm);

        if (!entrando && lerp < 0.01f)
            Destroy(this);
    }
}
