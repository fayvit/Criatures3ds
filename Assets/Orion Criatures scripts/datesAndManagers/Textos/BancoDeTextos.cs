using UnityEngine;
using System.Collections.Generic;

public class BancoDeTextos
{
    public static idioma linguaChave = idioma.pt_br;
    public static readonly Dictionary<idioma, Dictionary<ChaveDeTexto, List<string>>> falacoesComChave
    = new Dictionary<idioma, Dictionary<ChaveDeTexto, List<string>>>() {
        { idioma.pt_br,
            ListaDeTextosPT_BR.Txt
        },
        { idioma.en_google,
            ListaDeTextosEN_G.Txt
        }
    };

    public static readonly Dictionary<idioma, Dictionary<InterfaceTextKey, string>> textosDeInterface
        = new Dictionary<idioma, Dictionary<InterfaceTextKey, string>>() {
            {
                idioma.pt_br,
                InterfaceTextList.txt
            },
            {
                idioma.en_google,
                InterfaceTextListEN_G.txt
            }
        };

    public static readonly Dictionary<idioma, Dictionary<IndiceDeArmagedoms, string>> nomesArmagedoms
        = new Dictionary<idioma, Dictionary<IndiceDeArmagedoms, string>>() {
            {
                idioma.pt_br,
                NomeDeArmagedomPT_BR.n
            },
            {
                idioma.en_google,
                NomeDeArmagedomEN_G.n
            }
        };

    public static void VerificaChavesFortes(idioma primeiro, idioma segundo)
    {
        if (falacoesComChave.ContainsKey(primeiro) && falacoesComChave.ContainsKey(segundo))
        {
            Dictionary<ChaveDeTexto, List<string>>.KeyCollection keys = falacoesComChave[primeiro].Keys;

            foreach (ChaveDeTexto k in keys)
            {
                if (falacoesComChave[segundo].ContainsKey(k))
                {
                    if (falacoesComChave[segundo][k].Count != falacoesComChave[primeiro][k].Count)
                    {
                        Debug.Log("As listas de mensagem no indice " + k + " tem tamanhos diferentes");
                    }
                }
                else
                {
                    Debug.Log("A lista " + segundo + " nao contem a chave: " + k);
                }
            }
        }
        else
        {
            Debug.Log("Falacoes nao contem alguma das chaves de idioma");
        }

        if (nomesArmagedoms.ContainsKey(primeiro) && nomesArmagedoms.ContainsKey(segundo))
        {
            Dictionary<IndiceDeArmagedoms,string>.KeyCollection keys = nomesArmagedoms[primeiro].Keys;

            foreach (IndiceDeArmagedoms k in keys)
            {
                if (!nomesArmagedoms[segundo].ContainsKey(k))
                {
                    Debug.Log("A lista " + segundo + " nao contem a chave de armagedom: " + k);
                }
            }
        }
        else
        {
            Debug.Log("NomesArmagedoms nao contem alguma das chaves de idioma");
        }
    }

    public static List<string> RetornaListaDeTextoDoIdioma(ChaveDeTexto chave)
    {
        return falacoesComChave[linguaChave][chave];
    }

    public static string RetornaFraseDoIdioma(ChaveDeTexto chave)
    {
        return falacoesComChave[linguaChave][chave][0];
    }

    public static string RetornaTextoDeInterface(InterfaceTextKey chave)
    {
        return textosDeInterface[linguaChave][chave];
    }

    /*
	public Dictionary<string,Dictionary<string,int>> nome 
	= new Dictionary<string, Dictionary<string,int>>()
		{{"nome",
			new Dictionary<string,int>(){{"nome2",1}} }};
*/
}

public enum idioma
{
    pt_br,
    en_google
}

public enum ChaveDeTexto
{
    bomDia,
    apresentaInimigo,
    usoDeGolpe,
    apresentaFim,
    apresentaDerrota,
    criatureParaMostrador,
    passouDeNivel,
    naoPodeEssaAcao,
    jogoPausado,
    selecioneParaOrganizar,
    emQuem,
    aprendeuGolpe,
    tentandoAprenderGolpe,
    precisaEsquecer,
    certezaEsquecer,
    naoQueroAprenderEsse,
    aprendeuGolpeEsquecendo,
    foiParaArmagedom,
    primeiroArmagedom,
    frasesDeArmagedom,
    simOuNao,
    itens,
    barreirasDeGolpes,
    shopBasico,
    frasesDeShoping,
    comprarOuVender,
    textosParaQuantidadesEmShop,
    certezaExcluir,
    entradinha,
    segundaConversaDaEntradinha,
    continueMeSeguindo,
    asBarreirasDeTuto,
    mudeParaSeuCriature,
    certezaDeEscolhaInicial,
    voceRecebeuCriature,
    queroCriatureDoMiniArmagedom,
    qualEscolher,
    eSoUmCriature,
    primeiraBarreiraDerrubada,
    bau,
    ensinaMudarDeAtaque,
    resetPuzzle,
    depoisDoPuzzleTuto,
    teAjudoSairDaCaverna,
    recebaUmaCartaLuva,
    comoCapturo,
    comoUsoCartaLuva,
    oQueSerCartaLuva,
    tutoCapturaIntro,
    oQueSerLuvaDeGuarde,
    perguntasParaQueroCriature,
    estouAguardando,
    miniArmagedomDeKatids,
    akinaEmKatids,
    viajarParaArmagedom,
    Voltar,
    ObrigadoComPressa,
    tentaCapturar,
    listaDeItens,
    mensLuta,
    bemVindo,
    comSeuCriature,
    comoVaiSuaJornada,
    mudarDeCriature,
    primeiroCriatureNoArmgd,
    tutoCapturaOpcoes,
    katids1,
    katids2,
    katids3,
    katids4,
    katids5,
    katids6,
    shopInfoItem,
    textoBaseDeAcao,
    usarPergaminhoDeLaurense,
    usarPergaminhoDeBoutjoi,
    usarPergaminhoDeAnanda,
    falandoPrimeiroComIan,
    falandoPrimeiroComDerek,
    IanDepoisDeDerek,
    DerekDepoisDeIan,
    DerekDerrotado,
    IanComCaneta,
    ComecandoConversaComIan,
    opcoesDeIan,
    conversaBasicaDeIan,
    conversaBasicaDeIan2,
    despedidabasicaDeIan,
    frasesDeVendaDeIan,
    frasesDaLutaContraTreinador,
    companhiaDoMiniArmgdDeKatids,
    usarPergaminhoDeSinara,
    usarPergaminhoDeAlana,
    NPCdaRepresa,
    CamusMontanhaDoTemplo1,
    CamusMontanhaDoTemplo2,
    CamusMontanhaDoTemplo3,
    CamusMontanhaDoTemplo4,
    Marjan1,
    Marjan2,
    Marjan3,
    Marjan4,
    Marjan5,
    Marjan6,
    Marjan7,
    Marjan8,
    nomesDosGolpes,
    naFrenteDoCruzador,
    naFrenteDoCruzadorComCapitao,
    socorroNaPetrolifera,
    socorridoNaPetrolifera,
    anunciaQueda,
    malucoDosInsetos,
    malucoConversado,
    MalucoNoMOmentoDaDerrota,
    malucoDepoisDeDerrotado,
    preparadoParaMeEnfrentar,
    naoAprendeuGolpeNovo,
    ensinaTrocarCriature,
    ensinaBotaoDeAtacar,
    ensinaUsarItem,
    ensinaTrocarItem,
    oCriatureSelecionadoParaMudanca,
    gradeDeEsgoto,
    ensinaAndarE_Correr,
    ensinaPular,
    ensinaCamera,
    itemEmTempoDeRecarga,
    status,
    nomeStatus,
    riquinhaDoEsgoto,
    menuDePause,
    menuDeArmagedom
}
