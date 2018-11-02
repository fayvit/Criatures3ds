using UnityEngine;
[System.Serializable]
public class Atributos
{
    [SerializeField]AtributoConsumivel pv;
    [SerializeField]AtributoConsumivel pe;
    [SerializeField]AtributoInstrinseco ataque;
    [SerializeField]AtributoInstrinseco defesa;
    [SerializeField]AtributoInstrinseco poder;

    public Atributos(ContainerDeAtributos container)
    {
        pv = container.pv;
        pe = container.pe;
        ataque = container.ataque;
        defesa = container.defesa;
        poder = container.poder;
    }
    public AtributoInstrinseco Ataque
    {
        get { return ataque; }
        set { ataque = value; }
    }

    public AtributoInstrinseco Defesa
    {
        get { return defesa; }
        set { defesa = value; }
    }

    public AtributoConsumivel PE
    {
        get { return pe; }
        set { pe = value; }
    }

    public AtributoInstrinseco Poder
    {
        get { return poder; }
        set { poder = value; }
    }

    public AtributoConsumivel PV
    {
        get { return pv; }
        set { pv = value; }
    }
}


public class ContainerDeAtributos
{
    public AtributoConsumivel pv = new AtributoConsumivel(14);
    public AtributoConsumivel pe = new AtributoConsumivel(30);
    public AtributoInstrinseco ataque = new AtributoInstrinseco(9);
    public AtributoInstrinseco defesa = new AtributoInstrinseco(9);
    public AtributoInstrinseco poder = new AtributoInstrinseco(9);
}


[System.Serializable]
public class AtributoConsumivel
{
    [SerializeField]int basico;
    [SerializeField]int corrente;
    [SerializeField]int maximo;
    [SerializeField]int modMaximo;
    [SerializeField]float taxa;

    public AtributoConsumivel(int corrente, float taxa = 0.2f, int maximo = 0, int modMaximo = 0)
    {
        this.corrente = corrente;
        this.taxa = taxa;

        if (maximo != 0)
            this.maximo = maximo;
        else
            this.maximo = corrente;

        this.modMaximo = modMaximo;

        basico = this.maximo / 4;
    }

    public float Taxa
    {
        get { return taxa; }
        set { taxa = value; }
    }

    public int Basico
    {
        get { return basico; }
        set { basico = value; }
    }

    public int Corrente
    {
        get { return corrente; }
        set { corrente = Mathf.Max(0, Mathf.Min(value, maximo)); }
    }

    public int Maximo
    {
        get { return maximo; }
        set { maximo = Mathf.Max(1, value); }
    }

    public int ModMaximo
    {
        get { return modMaximo; }
        set { modMaximo = Mathf.Max(0, value); }
    }

    public void AumentaConsumivel(int valor)
    {
        corrente = Mathf.Min(corrente+valor,maximo);
    }

    public void AumentaAoMaximo()
    {
        corrente = maximo;
    }
}

[System.Serializable]
public class AtributoInstrinseco
{
    [SerializeField]int corrente;
    [SerializeField]int modCorrente;
    [SerializeField]int maximo;
    [SerializeField]int modMaximo;
    [SerializeField]int minimo;
    [SerializeField]float taxa;

    public AtributoInstrinseco(int corrente, float taxa = 0.2f, int maximo = 0,
                            int minimo = 1, int modCorrente = 0, int modMaximo = 0)
    {
        this.corrente = corrente;
        this.taxa = taxa;

        if (maximo != 0)
            this.maximo = maximo;
        else
            this.maximo = corrente;

        this.minimo = minimo;
        this.modMaximo = modMaximo;
        this.modCorrente = modCorrente;
    }

    public float Taxa
    {
        get { return taxa; }
        set { taxa = value; }
    }

    public int Corrente
    {
        get { return corrente; }
        set { corrente = Mathf.Max(0, value); }
    }

    public int ModCorrente
    {
        get { return modCorrente; }
        set { modCorrente = Mathf.Max(0, value); }
    }

    public int Maximo
    {
        get { return maximo; }
        set { maximo = Mathf.Max(1, value); }
    }

    public int Minimo
    {
        get { return minimo; }
        set { minimo = Mathf.Max(1, value); }
    }

    public int ModMaximo
    {
        get { return modMaximo; }
        set { modMaximo = Mathf.Max(0, value); }
    }
}

