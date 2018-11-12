using UnityEngine;
using System.Collections;
using GameJolt.API;
using System.IO;

public class SaveAndLoadInJolt
{
    public static bool estaCarregado = false;
    public static void Save()
    {
#if UNITY_WEBGL //GameJolt
        if (SaveDatesForJolt.s != null && Manager.Instance.CurrentUser != null)
        {
            Debug.Log("salvou: "+ Manager.Instance.CurrentUser.ID.ToString());
            byte[] sb = SaveDatesForJolt.SaveDatesForBytes();
            preJSON pre = new preJSON() { b = sb };

            DataStore.Set(Manager.Instance.CurrentUser.ID.ToString(),
                    JsonUtility.ToJson(pre), true,
                   Acertou);
        }
#endif
#if UNITY_N3DS
        if (SaveDatesForJolt.s != null)
        {
            byte[] sb = SaveDatesForJolt.SaveDatesForBytes();
            preJSON pre = new preJSON() { b = sb };
            UnityEngine.N3DS.FileSystemSave.Mount();

            StreamWriter sw = File.CreateText(Application.persistentDataPath + "/file1");
            sw.WriteLine(JsonUtility.ToJson(pre));
            sw.Close();
            UnityEngine.N3DS.FileSystemSave.Unmount();
        }
#endif
#if !UNITY_N3DS &&!UNITY_WEBGL
        if (SaveDatesForJolt.s != null)
        {
            byte[] sb = SaveDatesForJolt.SaveDatesForBytes();
            preJSON pre = new preJSON() { b = sb };

            PlayerPrefs.SetString("dates_OC", JsonUtility.ToJson(pre));

            PlayerPrefs.Save();
        }
#endif
    }

    public static void Load()
    {
#if UNITY_WEBGL //GameJolt
        if (Manager.Instance.CurrentUser != null)
        {

            DataStore.Get(Manager.Instance.CurrentUser.ID.ToString(), true, (string S2) => {
                if (!string.IsNullOrEmpty(S2))
                {
                    Debug.Log("Dados Carregados do Jolt");
                    SaveDatesForJolt.SetSavesWithBytes(JsonUtility.FromJson<preJSON>(S2).b);
                }
                else
                {
                    Debug.Log("string nula do Jolt");
                    new SaveDatesForJolt();
                }

                GameObject.FindObjectOfType<LoginJoltManager>().StartCoroutine(Carregado());
            });
        }
#endif
#if UNITY_N3DS
        UnityEngine.N3DS.FileSystemSave.Mount();
        string S2 = string.Empty;
        if (File.Exists(Application.persistentDataPath + "/file1"))
        {
            StreamReader sr = File.OpenText(Application.persistentDataPath + "/file1");
            S2 = sr.ReadLine();
            sr.Close();

        }



        UnityEngine.N3DS.FileSystemSave.Unmount();

        if (!string.IsNullOrEmpty(S2))
        {
            Debug.Log("não é null");
            SaveDatesForJolt.SetSavesWithBytes(JsonUtility.FromJson<preJSON>(S2).b);
        }
        else
        {
            Debug.Log("não achou");
            new SaveDatesForJolt();
        }
        Debug.Log("sou um N3DS");
        GameObject.FindObjectOfType<LoginJoltManager>().StartCoroutine(Carregado());
#endif

#if !UNITY_N3DS && !UNITY_WEBGL
        string S2 = PlayerPrefs.GetString("dates_OC", string.Empty);

        if (!string.IsNullOrEmpty(S2))
        {
            
            SaveDatesForJolt.SetSavesWithBytes(JsonUtility.FromJson<preJSON>(S2).b);
        }
        else
        {
            Debug.Log("não achou");
            new SaveDatesForJolt();
        }

        GameObject.FindObjectOfType<LoginJoltManager>().StartCoroutine(Carregado());
#endif
        GameObject.FindObjectOfType<LoginJoltManager>().StartCoroutine(Carregado());
    }

    static IEnumerator Carregado()
    {
        yield return new WaitForEndOfFrame();
        estaCarregado = true;
    }

    static void Acertou(bool foi)
    {
        if (foi)
        {
            Debug.Log("Deu certo" + SaveDatesForJolt.s.SavedGames[0].Posicao);
        }
        else
            Debug.Log("algo errado");
    }
}


[System.Serializable]
public class preJSON
{
    public byte[] b;
}
