using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class KeyVar
{
    private Dictionary<KeyShift, bool> shift = new Dictionary<KeyShift, bool>();
    private Dictionary<string, bool> autoShift = new Dictionary<string, bool>();
    private Dictionary<KeyCont, int> contadorChave = new Dictionary<KeyCont, int>();
    private Dictionary<nomesCriatures, bool> visto = new Dictionary<nomesCriatures, bool>();
    private Dictionary<nomesCriatures, bool> colecionado = new Dictionary<nomesCriatures, bool>();
    private List<IndiceDeArmagedoms> localArmag = new List<IndiceDeArmagedoms>() { IndiceDeArmagedoms.cavernaIntro};
    private List<NomesCenas> cenasAtivas = new List<NomesCenas>();
    private NomesCenas cenaAtiva = NomesCenas.cavernaIntro;

    public List<IndiceDeArmagedoms> LocalArmag
    {
        get { return localArmag; }
    }

    public NomesCenas CenaAtiva
    {
        get { return cenaAtiva; }
    }

    public List<NomesCenas> CenasAtivas
    {
        get { return cenasAtivas; }
    }

    public void SetarCenasAtivas(NomesCenas[] cenasAtivas)
    {
        this.cenasAtivas = new List<NomesCenas>();
        
        this.cenasAtivas.AddRange(cenasAtivas);        

        cenaAtiva = cenasAtivas[0];
    }

    public void SetarCenasAtivas()
    {
        NomesCenas[] nomesDeCenas = (NomesCenas[])(System.Enum.GetValues(typeof(NomesCenas)));
        cenasAtivas = new List<NomesCenas>();

        for (int i = 0; i < nomesDeCenas.Length; i++)
        {
            if (SceneManager.GetSceneByName(nomesDeCenas[i].ToString()).isLoaded)
            {
                cenasAtivas.Add(nomesDeCenas[i]);
            }
        }

        cenaAtiva = (NomesCenas)System.Enum.Parse(typeof(NomesCenas),SceneManager.GetActiveScene().name);
    }

    void MudaDic<T1,T2>(Dictionary<T1,T2> dic, T1 key, T2 val)
    {
        if (!dic.ContainsKey(key))
        {
            dic.Add(key, val);
        }
        else
            dic[key] = val;
    }

    public void MudaShift(KeyShift key, bool val = false)
    {
        MudaDic(shift, key, val);
    }

    public void MudaAutoShift(string key, bool val = false)
    {
        MudaDic(autoShift, key, val);
    }

    public void MudaVisto(nomesCriatures nome, bool val = false)
    {
        MudaDic(visto, nome, val);
    }

    public void MudaColecionado(nomesCriatures nome, bool val = false)
    {
        MudaDic(colecionado, nome, val);
    }

    public void MudaCont(KeyCont key, int val = 0)
    {
        MudaDic(contadorChave, key,val);
    }

    public void SomaCont(KeyCont key, int soma = 0)
    {
        if (contadorChave.ContainsKey(key))
        {
            contadorChave[key] += soma;
        }
        else
            contadorChave.Add(key, soma);
    }

    public bool VerificaAutoShift(string key)
    {
        //Debug.Log(autoShift.ContainsKey(key));
        if (!autoShift.ContainsKey(key))
        {
            autoShift.Add(key, false);
            return false;
        }
        else { //Debug.Log(autoShift[key]); 
            return autoShift[key];
        }
    }


    public bool VerificaAutoShift(KeyShift key)
    {
        if (!shift.ContainsKey(key))
        {
            shift.Add(key, false);
            return false;
        } else return shift[key];
    }

    public bool VerificaVisto(nomesCriatures key)
    {
        if (!visto.ContainsKey(key))
        {
            visto.Add(key, false);
            return false;
        }
        else return visto[key];
    }

    public bool VerificaColecionado(nomesCriatures key)
    {
        if (!colecionado.ContainsKey(key))
        {
            colecionado.Add(key, false);
            return false;
        }
        else return colecionado[key];
    }

    public int VerificaAutoCont(KeyCont key)
    {
        if (!contadorChave.ContainsKey(key))
        {
            contadorChave.Add(key, 0);
            return 0;
        }
        else return contadorChave[key];
    }
}

public enum KeyShift
{
    sempreFalse = -2,
    nula = -1,
    primeiraCaptura,
    estouNoTuto,
    fezPrimeiraFalaDeTuto,
    fezSegundaFalaDeTuto,
    barreiraPrimeira,
    ensinandoMudarDeAtaque,
    puzzleTuto,
    conversouPrimeiroComDerek,
    conversouPrimeiroComIan,
    venceuDerekPrimeiraVez,
    entreouCanetaDeIan,
    venceuMalucoDosInsetos,
    viuMalucoDosInsetos
}

public enum KeyCont
{
    pergSinaraComprados,
    pergAlanaComprados
}
