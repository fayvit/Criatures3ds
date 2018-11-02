public struct Encontravel
{
    public nomesCriatures nome;
    public float taxa;
    public int nivelMin;
    public int nivelMax;

    public Encontravel(nomesCriatures _nome, float tax, int nMin = 1, int nMax = -1)
    {
        nome = _nome;
        taxa = tax;
        nivelMax = nMax;
        nivelMin = nMin;
    }
}
