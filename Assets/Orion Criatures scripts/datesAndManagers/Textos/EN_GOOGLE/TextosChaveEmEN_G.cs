using UnityEngine;
using System.Collections.Generic;

public static class TextosChaveEmEN_G
{
    public static Dictionary<ChaveDeTexto, List<string>> txt = new Dictionary<ChaveDeTexto, List<string>>()
            {
                {ChaveDeTexto.bomDia, new List<string>()
                    {
                        "Good morning to you",
                        "Good morning to you",
                        "Good morning, ....",
                        "Good morning to you"
                    }},
                {
                    ChaveDeTexto.apresentaInimigo,new List<string>()
                    {
                        "You have found a <color=orange>{0}</color> Level {1} \n\r LP: {2} \t\t\t EP: {3}",
                        "The trainer {0}",
                        " Sent a <color=orange>{0}</color> Level {1} \n\r LP: {2} \t\t\t EP: {3}"
                    }
                },
                {
                    ChaveDeTexto.naoAprendeuGolpeNovo,new List<string>()
                    {
                        "<color=orange>{0}</color> did not learn Attack <color=yellow>{1}</color> and kept its four know attacks"
                    }
                },
                {
                    ChaveDeTexto.usoDeGolpe,new List<string>()
                    {
                    "Attack at recharge time. \r \n {0} \r \n until the next use.",
                    "There are not enough energy points for this blow"
                    }
                },
                {
                    ChaveDeTexto.apresentaFim,new List<string>()
                    {
                        @"{0} won, for victory {0} received {1} <color=#FFD700> experience points. </color> And Cesar Corean received {2} <color=#FF4500> CRYSTALS </color>"
                    }
                },
                {
                    ChaveDeTexto.apresentaDerrota,new List<string>()
                    {
                        "{0} was defeated",
                        "Which creature will you leave to continue the battle?",
                        "All your creatures have been defeated. \n\r Now you must go back to armageddon, heal your criatures and return to your adventure"
                    }
                },
                {
                    ChaveDeTexto.criatureParaMostrador,new List<string>()
                    {
                        "Are you sure you want to put <color=orange> {0} </color> to continue the fight?",
                        "The creature <color=orange> {0} </color> is not fit to enter the fight",
                        "The creature <color=orange> {0} </color> is fainted and can not enter"
                    }
                },
                {
                    ChaveDeTexto.passouDeNivel,new List<string>()
                    {
                        "{0} was able to reach the <color=yellow> level {1} ​​</color>"
                    }
                },
                {
                    ChaveDeTexto.naoPodeEssaAcao,new List<string>()
                    {
                        "<Color=orange> Your character is not in a position to do this </color>",
                        "<color=orange>Criature {0} is already in the field </color>",
                        "<color=orange>Select an item before clicking the use button </color>"
                    }
                },
                {
                    ChaveDeTexto.jogoPausado,new List<string>()
                    {
                        "Paused game"
                    }
                },
                {
                    ChaveDeTexto.selecioneParaOrganizar,new List<string>()
                    {
                        "Select the item to be repositioned",
                        "Select item to swap with {0}"
                    }
                },
                {
                    ChaveDeTexto.emQuem,new List<string>()
                    {
                        "<Color=yellow> In which criature you will use the item {0} </color>"
                    }
                },
                {
                    ChaveDeTexto.aprendeuGolpe,new List<string>()
                    {
                        "{0} learned the attack <color=yellow> {1} </color>"
                    }
                },
                {
                    ChaveDeTexto.tentandoAprenderGolpe,new List<string>()
                    {
                       "{0} is trying to learn the attack <color=yellow> {1} </color>, but for this he needs to forget an attack."
                    }
                },
                {
                    ChaveDeTexto.precisaEsquecer,new List<string>()
                    {
                        "Which attack {0} will forget?"
                    }
                },
                {
                    ChaveDeTexto.certezaEsquecer,new List<string>()
                    {
                        "Are you sure you want to forget the <color=yellow> {0} </color> attack to learn the attack <color=yellow> {1} </color> ??"
                    }
                },
                {
                    ChaveDeTexto.naoQueroAprenderEsse,new List<string>()
                    {
                        "<Color=orange> {0} </color> will stop learning the stroke <color=yellow> {1} </color>. Are you sure about this?"
                    }
                },
                {
                    ChaveDeTexto.aprendeuGolpeEsquecendo,new List<string>()
                    {
                        "<Color=orange> {0} </color> forgot <color=yellow> {1} </color> and learned <color=yellow> {2} </color>"
                    }
                },
                {
                    ChaveDeTexto.foiParaArmagedom,new List<string>()
                    {
                        "The Guarde glove of <color=yellow> Cesar Corean </color> can only carry {0} Criatures. Then <color=orange> {1} </ color> level {2} was sent to Armageddon"
                    }
                },
                {
                    ChaveDeTexto.primeiroArmagedom,new List<string>()
                    {
                        "Hello Strange !! \n \r Here is the local Armageddon.",
                        "What can I help you with?"
                    }
                },
                {ChaveDeTexto.simOuNao,new List<string>()
                    {
                        "yes",
                        "no"
                    }},
                {ChaveDeTexto.comprarOuVender,new List<string>()
                    {
                        "buy",
                        "Sell"
                    }},
                {
                    ChaveDeTexto.frasesDeArmagedom,new List<string>()
                    {
                        "Your criatures are healed, strange !!",
                        "I'm sorry stranger, but it seems like there are no criatures your saved at armageddon",
                        "Your Criatures in armagedom",
                        "O criature <color=orange> {0} </color> nivel {1} ​​joined your team.",
                        "Your team already has the maximum number of Criatures. To put <color=orange> {0} </color> level {1} ​​on the team you need to remove a Criature from your team",
                        "Which Criature will leave your team?",
                        "Criature {0} Level {1} joined the team in your place on Criature {2} level {3}",
                        "Are you sure you will remove {0} level {1} ​​from your team?",
                        "I can sell you every Armageddon Scroll by <color=yellow> {0} </color> Crystals. Do you want to buy?"
                    }
                },
                {
                    ChaveDeTexto.shopBasico,new List<string>()
                    {
                        "Welcome to my strange store.",
                        "How can I help you??"
                    }
                },
                {
                    ChaveDeTexto.frasesDeShoping,new List<string>()
                    {
                        "I have great products for you strange. Would you like to buy something?",
                        "What would you like to sell? strange...",
                        "Thank you very much for your purchase, strange ...",
                        "Excellent business, strange ...",
                        "I hope to do business with you again, strange ..."
                    }
                },
        {
            ChaveDeTexto.textosParaQuantidadesEmShop,new List<string>()
            {
                "CRYSTALS: ",
                "Amount to pay",
                "Receivable amount",
                "Do you want to buy how much {0} ??",
                "Do you want to sell what amount of {0} ??",
                "Buy",
                "Sell",
                "Unfortunately the crystals you carry only allow you to buy {0} {1}.",
                "Unfortunately you only have {0} {1} to sell.",
                "The minimum quantity you can buy is 1",
                "The minimum quantity you can sell is 1",
                "You do not have enough to buy this item"
            }
        },
        {
            ChaveDeTexto.certezaExcluir,new List<string>()
            {
                "Are you sure you want to delete Save {0}?"
            }
        },
                {ChaveDeTexto.itens,new List<string>()
                {
                    "You can not use this item at this time.",
                    "He does not need to use this item at this time.",
                    "Criature {0} is fainted and can not use this item at this time.",//O {0} será substituito pelo nome do Criature
					"Only Criatures of type {0} can use this item",
                    "O criature {0} não pode usar o item {1} pois ele já sabe o golpe {2}",
                    "Criature {0} can not learn the hit {1}",
                    "{0} did not use item {1}",
                    "Tem certeza que deseja usar o item {0} ?",
                    " Cesar Corean can not use this item in this location",
                    "{0} does not need to use this item at this time",
                    "You can not use items through the menu while fighting.",
                    "You do not have any items at this time.",
                    "Cesar Corean uses item <color=cyan>{0}</color>"
                }},
                {ChaveDeTexto.bau,new List<string>()
                {
                    "You have found chest. Do you want to open it?",
                    "The chest is empty",
                    "Inside the chest Cesar Corean get <color=cyan> {0} {1} </color>",
                    "With the victory Cesar Corean can achieve <color=cyan> {0} {1} </color>"
                }},
                {ChaveDeTexto.resetPuzzle,new List<string>()
                {
                    "Would you like to return the elements to their initial positions?"
                }},
                {ChaveDeTexto.viajarParaArmagedom,new List<string>()
                {
                    "<Color=cyan> For which Armageddon would you like to travel? </Color>"
                }},
                {ChaveDeTexto.Voltar,new List<string>()
                {
                    "Back"
                }},
                {ChaveDeTexto.ObrigadoComPressa,new List<string>()
                {
                    "Thanks, but I'm in a hurry...."
                }},


                {ChaveDeTexto.mensLuta,new List<string>()
                {
                    "You can not use this item at this time.",
                    "Cesar Corean uses ",
                    "{0} does not need to use this item at this time",
                    "You can not try to capture a trainer's creatures",
                    "You can not use Escape Scroll against coaches"
                }},


                {ChaveDeTexto.listaDeItens,new List<string>()
                {
					/*ID==0*/"Apple", "Burguer", "Grove Card",
                    "Gasoline", "tonic water", /* ID == 5 */ "Watering", "Star", "Quartz", "Fertilizant",
                    "Sap", /* ID == 10 */ "insecticide", "Aura", "cabbage with egg", "Fan", "battery",
					/* ID == 15 */ "Dry Ice", "Escape Scroll", "Secret", "Mysterious Statue",
                    "Crystals", "Perfection Scroll", "Antidote", "Courage Charm", "tonic", /* ID = 24 */
					"Bust of Water Scroll", "Exit Scroll", "Citations Alpha", "Armageddon Scroll", "Sabre Scroll", "Insect Ooze Scroll",
                    "Acid Ooze Scroll", "Multiply Scroll", "Gale Scroll",
                    "Winds Cutting Scroll", "Look weakening Scroll", "Look Evil Scroll", "Citations Beta", "Leaves Hurricane Scroll",
                    "Explosives", "Medallion of the Waters",
                    "Holy Ink of Log", "Laurense Scroll", "Boutjoi Scroll", "Ananda Scroll", /* ID = 45 */
                    "Holy Pen of Log", "Sinara Scroll", "Alana Scroll", "Saber Scroll"
                }},
                {ChaveDeTexto.shopInfoItem,new List<string>()
                    {
                        "Apple recovers 10 PV of a Creature",
                        "Burger recovers 40 PV a Criature",
                        "Glove Card is used to try to capture new Criatures",
                        "Gasoline recovers 40 PE a Criature Fire-type",
                        "Tonic Water recovers 40 PE a Criature Water-type ",
                        " Watering recovers 40 PE of a plant type Criature ",
                        " Star recovers 40 PE a Criature Normal-type ",
                        " Quartz recovers 40 PE a Criature Stone-type ",
                        " Fertilizer recovers 40 PE a Criature Earth-type ",
                        " Sap recovers 40 PE a Criature the Insect ",
                        " Insecticide recovers 40 PE a Criature the Poison ",
                        " Aura recovers 40 PE a Criature the Psychic type ",
                        "cabbage with egg recovers 40 PE of a gas type Criature",
                        "Fan recovers 40 PE a Criature the Flying type",
                        "battery recovers 40 PE a Criature the Eletrico type",
                        "Dry Ice recovers 40 PE a Criature Ice type ",
                        " When you read this scroll invokes a spell to expel the fighting opponent ",
                        " A very suspicious item leaning on Store fund ",
                        " A statue made of yellow stone in imposing pose ",
                        " Crystals is the monetary unit of orion",
                        " When you read this scroll the target criature fully recovers PVs, PEs and removes negative status ",
                        " The Criatures healing Antidoto who are poisoned ",
                        " The Amulet returns the courage to Criatures frightened ",
                        " The tonic cure Criatures Weke ",
                        " Agua Gust of scroll help a Criature type Agua to learn Gust coup de Agua ",
                        " can be used in closed places to teleport you out ",
                        " The award that Cesar Corean received from Captain Atos Aramis. ",
                        " Armageddon scroll teleports you to the last Armageddon you entered. You need to be in the open ",
                        " Sabre scroll helps a Criature to learn Sabre blow ",
                        " The scroll of Goop Insect helps a Criature Insect learn the Goop blow Bug ",
                        " The scroll of Goop Acida help one Criature Insect learn the Goop blow Acid ",
                        " The scroll of Multiply insects helps a Criature Insect learn the Multiply blow ",
                        " The scroll of the wind helps a Criature Flying learn the wind blow ",
                        " The scroll of the Winds Cutters helps a Criature Flying to learn the coup Winds Cutting ",
                        " The scroll of Look weakening helps a Criature to learn Look weakening blow ",
                        " The scroll of Evil Look helps a Criature to learn Evil Look blow ",
                        " The award that Cesar Corean received from Captain Fishbone. ",
                        " The leaves of Hurricane scroll can teach Sheets Hurricane blow to a plant type Criature ",
                        " The explosives needed to clear the way for Afarenga ",
                        " The locket Waters God Drag achieved with Omar Water ",
                        "A container containing the ink used by the priests of Log to write scrolls",
                        "The parchment in the name of Laurense the god of force, when read, increases 1 point of attack of a criature",
                        "The parchment in the name of Boutijoi the god of nature, when read, increases 1 point of defense of a criature",
                        "The parchment in the name of Ananda the goddess of magic, when read, increases 1 point of power to a creature",
                        "The pen used by Log's priests to write magical scrolls",
                        "The parchment in the name of Sinara, goddess mother of the criatures, when read, increases by 4 points the maximum life of a creature",
                        "The parchment in the name of Alana, goddess of fertility, when read, increases by 4 points the maximum energy of a creature",
                        "Saber's parchment can teach the Saber ability to a creature with the ability to learn it"
                }},
        {
            ChaveDeTexto.textoBaseDeAcao,new List<string>()
            {
                "Talk",
                "Examine"
            }
        },
                {ChaveDeTexto.tentaCapturar,new List<string>()
                {
                    "resisted Capture's attempt.",
                    "Cesar Corean's only keep glove can carry",
                    "So: ",
                    "Level",
                    "was sent to the",
                    "Cesar Corean was able to capture a"
                }},
                 {
            ChaveDeTexto.usarPergaminhoDeLaurense,new List<string>()
            {
                "Laurense's parchment can increase 1 Attack of a creature, who would you like to use?",
                "The creature {0} level {1} ​​increased 1 point in its attack"
            }
        },
        {
            ChaveDeTexto.usarPergaminhoDeBoutjoi,new List<string>()
            {
                "Boutjoi's parchment can increase 1 Defense of a creature, who would you like to use?",
                "The creature {0} level {1} ​​increased 1 point in its defense"
            }
        },
        {
            ChaveDeTexto.usarPergaminhoDeAnanda,new List<string>()
            {
                "Ananda's parchment can increase 1 Power of a creature, who would you like to use?",
                "The creature {0} level {1} ​​increased 1 point in its power"
            }
        },
        {
            ChaveDeTexto.ComecandoConversaComIan,new List<string>()
            {
               "Hello, traveler, would you like me to write you scrolls?",
                ""
            }
        },
        {
            ChaveDeTexto.opcoesDeIan,new List<string>()
            {
                "Write Sinar Parchment",
                "Write Alana Parchment",
                "Talk",
                "Get out"
            }
        },
        {
            ChaveDeTexto.conversaBasicaDeIan,new List<string>()
            {
                "I am a student, study to become a priest of <color=cyan> Log </color>, the god of wisdom and knowledge",
                "So I can write a few scrolls, in fact, only two that I can say useful",
                "The parchment of Sinara, goddess of the criatures, when read increases in 4 points the limit of life of a creature",
                "The parchment of Alana, goddess of fertility, when read increases by 4 points the energy limit of a criature",
                "I hope these scrolls are useful to you, traveler"
            }
        },
        {
            ChaveDeTexto.conversaBasicaDeIan2,new List<string>()
            {
                "I'm on a journey north, I have to settle with a little guy in the city of <color=yellow>Cyzor</color>",
                "I'll stay here for a while, but I'll need to resume my journey soon",
                "So if you want me to write scrolls for you, come back soon",
                "otherwise you may not find me here"
            }
        },
        {
            ChaveDeTexto.despedidabasicaDeIan,new List<string>()
            {
               "Even more travelers, come back soon so I can write more scrolls for you.I'll soon return to my journey"
            }
        },
        {
            ChaveDeTexto.frasesDeVendaDeIan,new List<string>()
            {
                "For me to write a {0} to you, I need {1} Holy Ink of Log and {2} crystals.Would you like me to write?",
                "Unfortunately, it seems that you do not have the necessary requirements for the purchase, traveler",
                "Ho How, it looks like we'll have a parchment !!",
                "Receive your traveling parchment",
                "Would you like me to write another parchment?",
            }
        },
        {
            ChaveDeTexto.frasesDaLutaContraTreinador,new List<string>()
            {
                "In this fight I will use {0} criatures",
                "To start I choose ...",
                "It looks like you had a partial win, but that's not over yet",
                "My next creature will be ..."
            }
        },
        {
            ChaveDeTexto.usarPergaminhoDeSinara,new List<string>()
            {
                "Sinara's parchment can increase the PV max of a creature by 4 points. Who would you like to use?",
                "The creature {0} level {1} ​​increased by 4 points on its PV max"
            }
        },
        {
            ChaveDeTexto.usarPergaminhoDeAlana,new List<string>()
            {
                "Alana's parchment can increase the PV max of a creature by 4 points. Who would you like to use?",
                "The creature {0} level {1} ​​increased by 4 points on its PV max"
            }
        },
        {
            ChaveDeTexto.nomesDosGolpes,new List<string>()
            {
                "Burst Water","Water Turbo","Fire Ball","Burst of Fire","Leaf Blade",
                "Leaves Hurricane","Horn","Slap","Claw","Whip Hand","Bite","Beak","Gale","Winds Cutting","Insect Ooze","Acid Ooze","Tail Whip","Halter","Electricity",
                "Concentrated Electricity","Poison needle","Poison Wave","Kick","Sword","Jumping Up","Gravel","Boulder","Burst of Earth","Energy of Claws","Earth Revenge","Psychosis","Hydro Bomb","Psychic Ball","Toast Attack","Leaf Storm",
                "Poison Rain","Multiply","Electrical Storm","Avalanche","Ring of Look",
                "Look Evil","Earth Curtain","Teleportation","Overflight","Look Weakening",
                "Gas Bomb","Burst of Gas","Smokescreen","Rod","Wing Sabre","Rod Sabre","Fin Sabre","Sword Sabre","Scissors","Spin Attack"
            }
        },
        {
            ChaveDeTexto.preparadoParaMeEnfrentar,new List<string>()
            { "Are you prepared to face me?"}
        },
        {
            ChaveDeTexto.itemEmTempoDeRecarga,new List<string>()
            { "The use of items is in recharge time"}
        },
        {
            ChaveDeTexto.status,new List<string>()
            {
                "<color=yellow> Attention </color> \r \n Your Criature <color=orange> {0} </color> fainted by poisoning",
                "Your Criature <color=orange> {0} </color> has suffered {1} PV of poison damage",
                "The creature Enemy suffered {0} HP from poison damage"
            }
        },
        {
            ChaveDeTexto.nomeStatus,new List<string>()
            {
                "poisoned",
                "weak",
                "frightened"
            }
        },
        {
            ChaveDeTexto.menuDePause,new List<string>()
            {
                "Status", "Items", "Colection", "Back to title", "Return to Game"
            }
        },
        {
            ChaveDeTexto.menuDeArmagedom,new List<string>()
            {
                "Heal criatures",
                "My armagedded criatures",
                "Buy Armageddon scroll",
                "Back in the game"
            }
        }



            };
}