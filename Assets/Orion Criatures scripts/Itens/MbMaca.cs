[System.Serializable]
/// <summary>
/// Classe responsavel pelo uso da maçã
/// </summary>
public class MbMaca : ItemDeRecuperacaoBase
{
    //[System.NonSerialized]private CreatureManager CriatureAlvoDoItem;
    //private const float TEMPO_DE_ANIMA_CURA_1 = 1.5f;

    public MbMaca(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.maca)
    {
        valor = 40
    }
        )
    {
        Estoque = estoque;
        valorDeRecuperacao = 40;
    }

    
}
