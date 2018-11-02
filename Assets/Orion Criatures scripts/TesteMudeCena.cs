using UnityEngine;
using System.Collections.Generic;

public class TesteMudeCena : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject G = new GameObject();
        SceneLoader loadScene = G.AddComponent<SceneLoader>();
        LoadAndSaveGame load = new LoadAndSaveGame();
        load.ExcluirArquivo("criatures.ori0");
        loadScene.CenaDoCarregamento(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public struct PropriedadesDeSave : System.IComparable
{
    public string nome;
    public int indiceDoSave;
    public System.DateTime ultimaJogada;

    public int CompareTo(object obj)
    {
        return System.DateTime.Compare(((PropriedadesDeSave)obj).ultimaJogada, ultimaJogada);
    }
}
