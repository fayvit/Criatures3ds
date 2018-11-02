public static class PegaUmGolpeG2
{

    public static GolpeBase RetornaGolpe(nomesGolpes nome)
    {
        GolpeBase retorno;
        switch (nome)
        {
            case nomesGolpes.rajadaDeAgua:
                retorno = new RajadaDeAguaG2();
            break;

            case nomesGolpes.turboDeAgua:
                retorno = new TurboDeAguaG2();
            break;
            case nomesGolpes.tapa:
                retorno = new TapaG2();
            break;

            case nomesGolpes.laminaDeFolha:
                retorno = new LaminaDeFolhaG2();
            break;

            case nomesGolpes.garra:
                retorno = new GarraG2();
            break;
            case nomesGolpes.furacaoDeFolhas:
                retorno = new FuracaoDeFolhasG2();
            break;
            case nomesGolpes.bolaDeFogo:
                retorno = new BolaDeFogoG2();
            break;
            case nomesGolpes.rajadaDeFogo:
                retorno = new RajadaDeFogoG2();
            break;
            case nomesGolpes.ventania:
                retorno = new VentaniaG2();
            break;
            case nomesGolpes.bico:
                retorno = new BicoG2();
            break;
            case nomesGolpes.ventosCortantes:
                retorno = new VentosCortantesG2();
            break;
            case nomesGolpes.chicoteDeCalda:
                retorno = new ChicoteDeCaldaG2();
            break;
            case nomesGolpes.gosmaDeInseto:
                retorno = new GosmaDeInsetoG2();
            break;
            case nomesGolpes.gosmaAcida:
                retorno = new GosmaAcidaG2();
            break;
            case nomesGolpes.psicose:
                retorno = new PsicoseG2();
            break;
            case nomesGolpes.bolaPsiquica:
                retorno = new BolaPsiquicaG2();
            break;
            case nomesGolpes.chicoteDeMao:
                retorno = new MbChicoteDeMao();
            break;
            case nomesGolpes.dentada:
                retorno = new MbDentada();
            break;
            case nomesGolpes.sobreSalto:
                retorno = new MbSobreSalto();
            break;
            case nomesGolpes.agulhaVenenosa:
                retorno = new MbAgulhaVenenosa();
            break;
            case nomesGolpes.ondaVenenosa:
                retorno = new MbOndaVenenosa();
            break;
            case nomesGolpes.bastao:
                retorno = new MbBastao();
            break;
            case nomesGolpes.pedregulho:
                retorno = new MbPedregulho();
            break;
            case nomesGolpes.cascalho:
                retorno = new MbCascalho();
            break;
            case nomesGolpes.cabecada:
                retorno = new MbCabecada();
            break;
            case nomesGolpes.chute:
                retorno = new MbChute();
            break;
            case nomesGolpes.espada:
                retorno = new MbEspada();
            break;
            case nomesGolpes.chifre:
                retorno = new MbChifre();
            break;
            case nomesGolpes.tempestadeDeFolhas:
                retorno = new MbTespestadeDeFolhas();
            break;
            case nomesGolpes.eletricidade:
                retorno = new MbEletricidade();
            break;
            case nomesGolpes.eletricidadeConcentrada:
                retorno = new MbEletricidadeConcentrada();
            break;
            case nomesGolpes.tempestadeEletrica:
                retorno = new MbTempestadeEletrica();
            break;
            case nomesGolpes.hidroBomba:
                retorno = new MbHidroBomba();
            break;
            case nomesGolpes.tosteAtaque:
                retorno = new MbTosteAtaque();
            break;
            case nomesGolpes.chuvaVenenosa:
                retorno = new MbChuvaVenenosa();
            break;
            case nomesGolpes.rajadaDeTerra:
                retorno = new MbRajadaDeTerra();
            break;
            case nomesGolpes.vingancaDaTerra:
                retorno = new MbVingancaDaTerra();
            break;
            case nomesGolpes.cortinaDeTerra:
                retorno = new MbCortinaDeTerra();
            break;
            case nomesGolpes.rajadaDeGas:
                retorno = new MbRajadaDeGas();
            break;
            case nomesGolpes.bombaDeGas:
                retorno = new MbBombaDeGas();
            break;
            case nomesGolpes.cortinaDeFumaca:
                retorno = new MbCortinaDeFumaca();
            break;
            case nomesGolpes.anelDoOlhar:
                retorno = new MbAnelDoOlhar();
            break;
            case nomesGolpes.teletransporte:
                retorno = new MbTeletransporte();
            break;
            case nomesGolpes.avalanche:
                retorno = new MbAvalanche();
            break;
            case nomesGolpes.multiplicar:
                retorno = new MbMultiplicar();
            break;
            case nomesGolpes.sobreVoo:
                retorno = new MbSobreVoo();
            break;
            case nomesGolpes.sabreDeAsa:
            case nomesGolpes.sabreDeBastao:
            case nomesGolpes.sabreDeEspada:
            case nomesGolpes.sabreDeNadadeira:
                retorno = new Sabre(nome);
            break;
            case nomesGolpes.olharMal:
                retorno = new OlharMal();
            break;
            case nomesGolpes.olharEnfraquecedor:
                retorno = new OlharEnfraquecedor();
            break;
            default:
                retorno = new GolpeBase(new ContainerDeCaracteristicasDeGolpe());
            break;
        }
        return retorno;
    }
}

public enum nomesGolpes
{
    cancelado = -2,
    nulo = -1,
    rajadaDeAgua,
    turboDeAgua,
    bolaDeFogo,
    rajadaDeFogo,
    laminaDeFolha,
    furacaoDeFolhas,
    chifre,
    tapa,
    garra,
    chicoteDeMao,
    dentada,
    bico,
    ventania,
    ventosCortantes,
    gosmaDeInseto,
    gosmaAcida,
    chicoteDeCalda,
    cabecada,
    eletricidade,
    eletricidadeConcentrada,
    agulhaVenenosa,
    ondaVenenosa,
    chute,
    espada,
    sobreSalto,
    cascalho,
    pedregulho,
    rajadaDeTerra,
    energiaDeGarras,
    vingancaDaTerra,
    psicose,
    hidroBomba,
    bolaPsiquica,
    tosteAtaque,
    tempestadeDeFolhas,
    chuvaVenenosa,
    multiplicar,
    tempestadeEletrica,
    avalanche,
    anelDoOlhar,
    olharMal,
    cortinaDeTerra,
    teletransporte,
    sobreVoo,
    olharEnfraquecedor,
    bombaDeGas,
    rajadaDeGas,
    cortinaDeFumaca,
    bastao,
    sabreDeAsa,
    sabreDeBastao,
    sabreDeNadadeira,
    sabreDeEspada,
    tesoura,
    ataqueEmGiro
}