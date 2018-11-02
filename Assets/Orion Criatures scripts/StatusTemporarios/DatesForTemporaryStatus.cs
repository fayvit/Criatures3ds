using UnityEngine;
using System.Collections;

[System.Serializable]
public class DatesForTemporaryStatus
{
    [SerializeField]private float quantificador = 1;
    [SerializeField]private float tempoSignificativo = 50;
    [SerializeField]private TipoStatus tipo = TipoStatus.nulo;

    public float Quantificador
    {
        get { return quantificador; }
        set { quantificador = value; }
    }

    public TipoStatus Tipo
    {
        get { return tipo; }
        set { tipo = value; }
    }

    public float TempoSignificativo
    {
        get { return tempoSignificativo; }
        set { tempoSignificativo = value; }
    }
}

public enum TipoStatus
{
    todos = -2,
    nulo = -1,
    envenenado,
    fraco,
    amedrontado
}