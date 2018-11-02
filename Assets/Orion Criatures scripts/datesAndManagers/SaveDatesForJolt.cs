using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveDatesForJolt
{
    private idioma chosenLanguage = idioma.en_google;
    private List<PropriedadesDeSave> saveProps = new List<PropriedadesDeSave>();
    private List<SaveDates> savedGames = new List<SaveDates>();

    public static SaveDatesForJolt s;

    public SaveDatesForJolt()
    {
        s = this;
    }

    public List<PropriedadesDeSave> SaveProps
    {
        get { return saveProps; }

        set { saveProps = value; }
    }

    public List<SaveDates> SavedGames
    {
        get { return savedGames; }

        set { savedGames = value; }
    }

    public idioma ChosenLanguage
    {
        get { return chosenLanguage; }
        set { chosenLanguage = value; }
    }

    public static byte[] SaveDatesForBytes()
    {
        MemoryStream ms = new MemoryStream();
        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(ms, s);

        return ms.ToArray();
    }

    public static void SetSavesWithBytes(byte[] b)
    {
        MemoryStream ms = new MemoryStream(b);
        BinaryFormatter bf = new BinaryFormatter();
        s = (SaveDatesForJolt)bf.Deserialize(ms);

        Debug.Log("Saves set: "+s.saveProps.Count+" : "+s.savedGames.Count);
    }
}