using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class N3dsLoader : MonoBehaviour {

    private NomesCenas cenaComum;
    private NomesCenas[] cenasParaCarregar;
    private FadeView fade;
    private float tempoDecorrido = 0;

    public static void CarregarCenas(NomesCenas nomeCena, NomesCenas cenaComum = NomesCenas.nula)
    {
        CarregarCenas(new NomesCenas[1] { nomeCena }, cenaComum);
    }

    public static void CarregarCenas(NomesCenas[] nomesCenas, NomesCenas cenaComum = NomesCenas.nula)
    {
        GameObject G = new GameObject();
        N3dsLoader loadScene = G.AddComponent<N3dsLoader>();
        loadScene.CenaDoCarregamento(nomesCenas, cenaComum);
    }

    public void CenaDoCarregamento(NomesCenas[] nomesCenas, NomesCenas cenaComum = NomesCenas.nula)
    {
        this.cenaComum = cenaComum;
        DontDestroyOnLoad(gameObject);
        cenasParaCarregar = nomesCenas;
        Time.timeScale = 0;
        fade = gameObject.AddComponent<FadeView>();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(tempoDecorrido);
        tempoDecorrido += Time.fixedDeltaTime;
	}
}
