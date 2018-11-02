using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoadAndSaveGame
{
    public int indiceDoJogoAtualSelecionado = 0;

    public void ExcluirArquivo(string nomeArquivo)
    {
        if (File.Exists(Application.persistentDataPath + "/" + nomeArquivo))
        {
            File.Delete(Application.persistentDataPath + "/" + nomeArquivo);
        }
    }

    public void SalvarArquivo(string nomeArquivo,object conteudo)
    {
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            FileStream file = File.Create(Application.persistentDataPath + "/" + nomeArquivo);
            bf.Serialize(file, conteudo);
            file.Close();
        }
        catch (IOException e)
        {
            Debug.Log(e.StackTrace);
            Debug.LogWarning("Save falhou");
        }

        
    }

    public void Save(SaveDates paraSalvar)
    {
       // Debug.Log(indiceDoJogoAtualSelecionado);
        SalvarArquivo("criatures.ori" + indiceDoJogoAtualSelecionado,paraSalvar);
        
    }

    public SaveDates Load(int indice)
    {
        indiceDoJogoAtualSelecionado = indice;
        return (SaveDates)(CarregarArquivo("criatures.ori" + indice));
    }

    public object CarregarArquivo(string nomeArquivo)
    {
        object retorno = null;
        if (File.Exists(Application.persistentDataPath+"/"+nomeArquivo))
        {
            Debug.Log(Application.persistentDataPath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath+"/"+nomeArquivo, FileMode.Open);
            retorno = bf.Deserialize(file);
            file.Close();
        }

        return retorno;
    }
}
