using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class EnemyAndHeroStatusHudManager
{
    [SerializeField] private StatusHudManager doHeroi;
    [SerializeField] private StatusHudManager doInimigo;

    public StatusHudManager DoHeroi
    {
        get { return doHeroi; }
    }

    public StatusHudManager DoInimigo
    {
        get { return doInimigo; }
    }

    public void Start()
    {
        doHeroi.Start();
        doInimigo.Start();
    }

    public void Update()
    {
        DoHeroi.Update();
        DoInimigo.Update();
    }
}

[System.Serializable]
public class StatusHudManager
{
    [SerializeField] private RawImage imgDoStatus;
    [SerializeField] private Text txDoStatus;

    private float contadorDeTempo = 0;
    private int indiceDoStatus = 0;
    private bool ativa;
    private CriatureBase criatureDoStatus;

    private const float TEMPO_TOTAL_STATUS = 3;

    // Use this for initialization
    public void Start()
    {
        if (criatureDoStatus == null)
            DesligarPainel();
    }

    // Update is called once per frame
    public void Update()
    {

        if (ativa)
        {
            if (criatureDoStatus.CaracCriature.meusAtributos.PV.Corrente <= 0)
                DesligarPainel();

            int numStatus = criatureDoStatus.StatusTemporarios.Count;
            if (numStatus > 1)
            {
                contadorDeTempo += Time.fixedDeltaTime;
                if (contadorDeTempo > TEMPO_TOTAL_STATUS / numStatus)
                {
                    indiceDoStatus = indiceDoStatus + 1 < numStatus ? indiceDoStatus + 1 : 0;
                    contadorDeTempo = 0;
                    ModificaApresentacaoDoStatus();
                }

            }
            else if (numStatus == 0 )
                DesligarPainel();
        }
    }

    void ModificaApresentacaoDoStatus()
    {
        if (criatureDoStatus.StatusTemporarios.Count > 0)
        {
            TipoStatus tipo = criatureDoStatus.StatusTemporarios[indiceDoStatus].Tipo;
            imgDoStatus.texture = GameController.g.El.RetornaMini(tipo);
            txDoStatus.text = StatusTemporarioBase.NomeEmLinguas(tipo);
        }
    }

    public void LigarPainel()
    {
        ativa = true;
        imgDoStatus.transform.parent.gameObject.SetActive(true);
        txDoStatus.transform.parent.gameObject.SetActive(true);
    }

    public void DesligarPainel()
    {
        ativa = false;
        imgDoStatus.transform.parent.gameObject.SetActive(false);
        txDoStatus.transform.parent.gameObject.SetActive(false); 
    }

    public void IniciarHudStatus(CriatureBase C)
    {
        if (imgDoStatus != null)
        {
            criatureDoStatus = C;
            contadorDeTempo = 0;
            indiceDoStatus = 0;
            ModificaApresentacaoDoStatus();
            
            if (C.StatusTemporarios.Count > 0)
                LigarPainel();
            else
                DesligarPainel();

        }
    }
}
