using UnityEngine;
using System.Collections.Generic;

public class TextosDaCavernaInicialEN_G
{
    public static Dictionary<ChaveDeTexto, List<string>> txt = new Dictionary<ChaveDeTexto, List<string>>()
        {
        { ChaveDeTexto.entradinha, new List<string>()
            {
                "Então... é você que veio juntar-se a nós?",
                "Nós entendemos os seus motivos.",
                "Também sentimos que há algo errado com o rumo que o império está tomando",
                "Nós estamos tentando abrir a <color=yellow>Torre da Vida Eterna</color> ",
                "para encarar o imperador <color=orange>Logan</color> e mudar destino de <color=yellow>Orion</colors>",
                "O caminho para isso é muito longo e muito difícil,",
                " você precisa de muita determinação para percorrer todo o caminho",
                "Nós estamos apenas no meio dele um tanto quanto sem rumo mas já temos algumas lições para tirar",
                "Rapaz, todos os que querem fazer uma tarefa difícil tem um começo.",
                "E só quem já está no meio do caminho sabe como é dificil começar.",
                "Por isso vamos te ajudar",
                "Seu nome é <color=cyan>Cesar Corean</color> não é?",
                "Siga na caverna, Cesar Corean, e você poderá pegar um criature de nossa reserva"
            } },
            {ChaveDeTexto.bemVindo, new List<string>()
            {
               "Hello, newbie, welcome.",
                "We are the resistance to the empire of <color=orange> Logan </color>",
                "We currently base resistance to the empire in the struggle with criatures",
                "Go in the cave and you could catch a creature from our reservation"
            } },
              {ChaveDeTexto.hooliganSeApresenta, new List<string>()
            {
                "Bem vindo <color=cyan>Cesar Corean</color>",
                "Meu nome é <color=orange>Random Hooligan</color>.",
                "Meu irmão é um clérigo da coragem e regente de uma arena de Criatures.",
                "Somos de uma tradicional familia aristocratica de <color=orange>Orion</color>",
                "Logo eu partirei para encontrar meu irmão na cidade de <color=yellow>Afarenga</color>.",
                 "Pretendo com a ajuda dele conseguir o apoio do povo da cidade para nossa causa."
            } },
            {ChaveDeTexto.lanceSeApresenta, new List<string>()
            {
                "Seja muito bem vindo companheiro",
                "Meu nome é <color=orange>Lance Lutz</color>. Sou um sociologo formado na universidade do deserto da predominância",
                "tenho grande contato entre os pensadores e movimentos sociais de Orion.",
                "Logo viajarei para a cidade de <color=yellow>Ofawing</color> organizar um ato proximo das administrações governamentais"
            }},
            {ChaveDeTexto.comSeuCriature, new List<string>()
            {
                "Great you already have a criature now!",
                "Keep following the path of the cave and you will soon reach the city of <color=yellow> Infinity </color>"
            }},
            {
                ChaveDeTexto.comoVaiSuaJornada,new List<string>()
                {
                    "Hello boy! How's your journey going?",
                    "Already on the way to open the <color=yellow> Tower of Eternal Life </color>"
                }
            },
        {
            ChaveDeTexto.mudarDeCriature,new List<string>()
            {
                "You can switch between the hero and the criature by pressing Q on the keyboard or LS on the Xbox controller"
            }
        },
        {
            ChaveDeTexto.primeiroCriatureNoArmgd,new List<string>()
            {
                "Hello rookie !! I was able to make a connection with <color=cyan> Armageddon </color>",
                "I can now access our reservation of criatures",
                "Since we do not have many criatures, I can only give you one of them",
                "Which creature will you choose?",
                ""
            }
        },
        {
            ChaveDeTexto.qualEscolher,new List<string>()
            {
                "Choose a Criature"
            }
        },
        {
            ChaveDeTexto.certezaDeEscolhaInicial,new List<string>()
            {
                "Are you sure you want to choose {0} the creature of type {1}?"
            }
        },
        {
            ChaveDeTexto.voceRecebeuCriature,new List<string>()
            {
                "You received Criature {0}."
            }
        },
        {
            ChaveDeTexto.tutoCapturaOpcoes,new List<string>()
            {
                "How do I capture a new Criature?",
                "How do I use a glove card?",
                "What is the glove card?",
                "What's store glove?"
            }
        },
        {
            ChaveDeTexto.tutoCapturaIntro,new List<string>()
            {
                "Hello rookie !! Did you know you can capture new creatures using a <color=cyan> Glove card</color>?",
                ""
            }
        },
        {
            ChaveDeTexto.comoCapturo,new List<string>()
            {
               "To capture a new creature you must first initiate a combat against a wild creature",
                "In combat you must weaken the creature, leaving it with 1 or 2 life points preferentially",
                "When the enemy has a low life, you should use the <color=cyan> Glove Card</color> item",
                "So you have a chance to capture the creature"
            }
        },
        {
            ChaveDeTexto.comoUsoCartaLuva,new List<string>()
            {
                "To use the glove chart select the item using the 1 and / or 2 buttons on the keyboard or DPAD of the XBOX control",
                "The selected item will be displayed in the lower left panel",
                "With selected glove card press F on the keyboard or X on the XBOX control to use it"
            }
        },
        {
            ChaveDeTexto.oQueSerCartaLuva,new List<string>()
            {
                "The glove card is an energy card that increases the potency of your <color=cyan> Store glove </color>",
                "With the increase of power the store glove can send a lightning bolt to imprison a creature",
                "But the increase in energy may not be enough to capture a creature",
                "If the creature resists the capture attempt, the energy of the glove card is spent and it is discarded."
            }
        },
        {

        ChaveDeTexto.oQueSerLuvaDeGuarde,new List<string>()
        {
           "Store glove is a special glove used throughout <color=yellow> Orion </color> and is intended to store your items",
            "In the store glove are stored their creatures, Crystals and all type of item that is loading",
            "Store glove is one of the greatest inventions of recent times"
        }
        },
        {
            ChaveDeTexto.eSoUmCriature,new List<string>()
            {
                "Unfortunately I can only give you a criature",
                "But whenever you need to recover the energies of your criatures come back here",
                "I will be happy to help"
            }
        }
    };
}
