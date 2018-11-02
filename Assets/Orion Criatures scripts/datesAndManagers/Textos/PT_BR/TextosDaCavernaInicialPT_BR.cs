using UnityEngine;
using System.Collections.Generic;

public class TextosDaCavernaInicialPT_BR
{
    public static Dictionary<ChaveDeTexto, List<string>> txt = new Dictionary<ChaveDeTexto, List<string>>()
        {
            {ChaveDeTexto.bemVindo, new List<string>()
            {
                "Olá novato, seja bem vindo",
                "Nós somos a resistencia ao império de <color=orange>Logan</color>",
                "Atualmente baseamos a resistencia ao império na luta com criatures",
                "Siga na caverna e você poderá pegar um criature de nossa reserva"
            } },
            {ChaveDeTexto.comSeuCriature, new List<string>()
            {
                "Ótimo você já tem um criature agora!",
                "Continue seguindo o caminho da caverna e logo chegará a cidade de <color=yellow>Katids</color>"
            }},
            {
                ChaveDeTexto.comoVaiSuaJornada,new List<string>()
                {
                    "Olá rapaz!! Como vai sua jornada?",
                    "Já está no rumo de abrir a <color=yellow>Torre da Vida Eterna</color>"
                }
            },
        {
            ChaveDeTexto.mudarDeCriature,new List<string>()
            {
                "Você pode alternar entre o heroi e o criature pressionando Q no teclado ou LS no controle de Xbox"
            }
        },
        {
            ChaveDeTexto.primeiroCriatureNoArmgd,new List<string>()
            {
                "Olá novato!! Eu consegui fazer uma conexão com <color=Cyan>Armagedom</color>",
                "Agora posso acessar nossa reserva de criatures",
                "Como não temos muitos criatures, só poderei lhe dar um deles",
                "QUal criature você irá escolher?",
                ""
            }
        },
        {
            ChaveDeTexto.qualEscolher,new List<string>()
            {
                "Escolha um Criature"
            }
        },
        {
            ChaveDeTexto.certezaDeEscolhaInicial,new List<string>()
            {
                "Tem certeza que deseja escolher {0} o criature do tipo {1}?"
            }
        },
        {
            ChaveDeTexto.voceRecebeuCriature,new List<string>()
            {
                "Você recebeu o Criature {0}."
            }
        },
        {
            ChaveDeTexto.tutoCapturaOpcoes,new List<string>()
            {
                "Como capturo um novo Criature?",
                "Como uso uma carta luva?",
                "O que é a carta luva?",
                "O que é luva de guarde?"
            }
        },
        {
            ChaveDeTexto.tutoCapturaIntro,new List<string>()
            {
                "Olá novato!! Você sabia que pode capturar novos criatures utilizando uma <color=cyan>Carta Luva</color>?",
                ""
            }
        },
        {
            ChaveDeTexto.comoCapturo,new List<string>()
            {
                "Para capturar um novo criature você precisa primeiramente iniciar um combate contra um criature selvagem",
                "No combate você deve enfraquecer o criature, deixando ele com 1 ou 2 pontos de vida preferencialmente",
                "Quando o inimigo estiver coma  vida baixa você deve usar o item <color=cyan>Carta Luva</color>",
                "Assim você terá uma chance de capturar o criature"
            }
        },
        {
            ChaveDeTexto.comoUsoCartaLuva,new List<string>()
            {
                "Para usar a carta luva selecione o item utilizando os botões 1 e/ou 2 no teclado ou DPAD do controle de XBOX",
                "O item selecionado será exibido no painel inferior esquerdo",
                "Com carta luva selecionada pressione F no teclado ou X no controle de XBOX para utiliza-la"
            }
        },
        {
            ChaveDeTexto.oQueSerCartaLuva,new List<string>()
            {
                "A carta luva é um cartão de energia que aumenta a potencia da sua <color=cyan>Luva de Guarde</color>",
                "Com o aumento de potencia a luva de guarde pode enviar um raio para aprisionar um criature",
                "Porém o aumento de energia pode não ser suficiente para capturar um criature",
                "Se o criature resisitir a tentativa de captura, a energia da carta luva é gasta e ela é descartda."
            }
        },
        {

        ChaveDeTexto.oQueSerLuvaDeGuarde,new List<string>()
        {
            "Luva de Guarde é uma luva especial usada em toda a <color=yellow>Orion</color> e tem por finalidade guardar seus itens",
            "Na luva de guarde são guardados seus criatures, Cristais e todo tipo de item que está a carregar",
            "Luva de guarde é uma das maiores invenções dos últimos tempos"
        }
        },
        {
            ChaveDeTexto.eSoUmCriature,new List<string>()
            {
                "Infelizmente só posso lhe dar um criature",
                "mas sempre que precisar recuperar as energias de seus criatures volte aqui,",
                " terei prazer em ajudar"
            }
        }
    };
}