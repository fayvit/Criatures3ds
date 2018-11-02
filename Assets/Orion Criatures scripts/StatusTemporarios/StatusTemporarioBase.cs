using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class StatusTemporarioBase
{
    [SerializeField] private DatesForTemporaryStatus dados;

    private Transform particula;
    private CriatureBase oAfetado;
    private CreatureManager cDoAfetado;

    public CriatureBase OAfetado
    {
        get { return oAfetado; }
        set { oAfetado = value; }
    }

    protected Transform Particula
    {
        get { return particula; }
        private set { particula = value; }
    }

    public DatesForTemporaryStatus Dados
    {
        get { return dados; }
        set { dados = value; }
    }

    public CreatureManager CDoAfetado
    {
        get { return cDoAfetado; }
        set { cDoAfetado = value; }
    }

    // Use this for initialization
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    
    /// <summary>
    /// Função para inserção da particula no criature alvo do status
    /// </summary>
    /// <param name="essaParticula">identificador da particula</param>
    /// <param name="transform">transform o qual a particula será filiada</param>
    protected void ColocaAParticulaEAdicionaEsseStatus(string essaParticula,Transform transform)
    {
        Particula = ((GameObject)MonoBehaviour.Instantiate(GameController.g.El.retorna(essaParticula),
                                             Vector3.zero, Quaternion.identity)).transform;
        Particula.parent = transform;
        Particula.localPosition = Vector3.zero;
        Particula.localRotation = Quaternion.identity;

        if (ContemStatus(Dados.Tipo, OAfetado) <= -1)
        {
            OAfetado.StatusTemporarios.Add(Dados);
        }
    }

    public static int ContemStatus(TipoStatus esseStatus, CriatureBase X)
    {
        int retorno = -1;
        if (X.StatusTemporarios.Count > 0)
        {
            foreach (DatesForTemporaryStatus sT in X.StatusTemporarios)
            {
                if (sT.Tipo == esseStatus)
                {
                    retorno = X.StatusTemporarios.IndexOf(sT);
                }
            }
        }

        return retorno;
    }

    public static void TiraStatusDaLista(TipoStatus tipo, List<DatesForTemporaryStatus> afetado)
    {
        if (afetado.Count > 0)
        {
            List<DatesForTemporaryStatus> aTirar = new List<DatesForTemporaryStatus>();
            foreach (DatesForTemporaryStatus sT in afetado)
            {
                if (sT.Tipo == tipo || tipo == TipoStatus.todos)
                {
                    aTirar.Add(sT);
                }
            }

            for (int i = 0; i < aTirar.Count; i++)
            {
                afetado.Remove(aTirar[i]);
            }
        }
    }

    /*
    public static void LimpaStatus(CriatureBase X, int i)
    {

        StatusTemporarioBase[] sTs;

        if (i == 0)
        {
            GameObject G = GameController.g.Manager.CriatureAtivo.gameObject;

            sTs = G.GetComponents<StatusTemporarioBase>();
            foreach (StatusTemporarioBase sTb in sTs)
            {
                sTb.Destrua();
            }

        }
        else
        {
            sTs = GameController.g.Manager.GetComponents<StatusTemporarioBase>();
            foreach (StatusTemporarioBase sTb in sTs)
            {
                if (sTb.oAfetado == X)
                {
                    Destroy(sTb);
                }
            }
        }

        X.StatusTemporarios.Clear();
    }*/

    public virtual void RetiraComponenteStatus()
    {
        Debug.Log("aqui");
        if (CDoAfetado != null && Particula != null)
            MonoBehaviour.Destroy(Particula.gameObject);

        if (GameController.g.ContStatus.StatusDoHeroi.Contains(this))
            GameController.g.ContStatus.StatusDoHeroi.Remove(this);
        else if (GameController.g.ContStatus.StatusDoInimigo.Contains(this))
            GameController.g.ContStatus.StatusDoInimigo.Remove(this);

        OAfetado.StatusTemporarios.Remove(Dados);
    }

    public static string NomeEmLinguas(TipoStatus nome)
    {
        string[] arr = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.nomeStatus).ToArray();
        if (arr.Length > (int)nome)
            return arr[(int)nome];
        else
        {
            Debug.LogError("O array de nomes de status não contem um nome para o ID= " + nome);
            return nome.ToString();// BancoDeTextos.falacoes[heroi.lingua]["listaDeGolpes"][(int)Nome];
        }
    }
}
