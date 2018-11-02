using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ContainerDosStatus
{
    [SerializeField]private List<StatusTemporarioBase> statusDoHeroi = new List<StatusTemporarioBase>();
    [SerializeField]private List<StatusTemporarioBase> statusDoInimigo = new List<StatusTemporarioBase>();

    public void AdicionaStatusAoHeroi(StatusTemporarioBase S)
    {
        GameController.g.HudM.StatusHud.DoHeroi.IniciarHudStatus(S.OAfetado);
        statusDoHeroi.Add(S);
        S.Start();
        
    }

    public void AdicionaStatusAoInimigo(StatusTemporarioBase S)
    {
        GameController.g.HudM.StatusHud.DoInimigo.IniciarHudStatus(S.OAfetado);
        statusDoInimigo.Add(S);
        S.Start();
    }

    public List<StatusTemporarioBase> StatusDoHeroi
    {
        get { return statusDoHeroi; }
        set { statusDoHeroi = value; }
    }

    public List<StatusTemporarioBase> StatusDoInimigo
    {
        get { return statusDoInimigo; }
        set { statusDoInimigo = value; }
    }

    public void Update()
    {
        AtualizaListaDeStatus(StatusDoHeroi);
        AtualizaListaDeStatus(StatusDoInimigo);
        
    }

    void AtualizaListaDeStatus(List<StatusTemporarioBase> lista)
    {
        for (int i = 0; i < lista.Count; i++)
            lista[i].Update();
    }
}