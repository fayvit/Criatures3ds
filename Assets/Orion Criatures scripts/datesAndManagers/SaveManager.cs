using UnityEngine;
using System.Collections;

public class SaveManager
{
    private float tempoDecorrido = 0;
    private LoadAndSaveGame loadSave = new LoadAndSaveGame();
    private const float INTERVALO_DE_SAVE = 60;

    public int IndiceDoJogoAtual
    {
        get { return loadSave.indiceDoJogoAtualSelecionado; }
    }
    public void SetarJogoAtual(int qual)
    {
        loadSave.indiceDoJogoAtualSelecionado = qual;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        tempoDecorrido += Time.deltaTime;
        if (tempoDecorrido > INTERVALO_DE_SAVE)
        {
            SalvarAgora();
        }
    }

    public void SalvarAgora()
    {
        
        if (SaveDatesForJolt.s == null)
            new SaveDatesForJolt();

        if (SaveDatesForJolt.s.SavedGames.Count > loadSave.indiceDoJogoAtualSelecionado)
        {
            SaveDatesForJolt.s.SavedGames[loadSave.indiceDoJogoAtualSelecionado] = (new SaveDates());
        }
        else
            SaveDatesForJolt.s.SavedGames.Add(new SaveDates());

        Debug.Log(SaveDatesForJolt.s.SavedGames.Count + " savedGamesCount: saveprops" + SaveDatesForJolt.s.SaveProps.Count);

        SaveAndLoadInJolt.Save();
          //  loadSave.Save(new SaveDates());
        
        tempoDecorrido = 0;
    }

    public void SalvarAgora(NomesCenas[] cenasAtivas)
    {
       
        if (SaveDatesForJolt.s == null)
           new SaveDatesForJolt();
        
        if (SaveDatesForJolt.s.SavedGames.Count > loadSave.indiceDoJogoAtualSelecionado)
            SaveDatesForJolt.s.SavedGames[loadSave.indiceDoJogoAtualSelecionado] = new SaveDates(cenasAtivas);
        else
            SaveDatesForJolt.s.SavedGames.Add(new SaveDates(cenasAtivas));
        SaveAndLoadInJolt.Save();
            //loadSave.Save(new SaveDates(cenasAtivas));
        
        tempoDecorrido = 0;
    }
}
