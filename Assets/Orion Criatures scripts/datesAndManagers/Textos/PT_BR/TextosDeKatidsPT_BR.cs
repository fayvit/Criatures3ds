using UnityEngine;
using System.Collections.Generic;

public class TextosDeKatidsPT_BR
{
    public static Dictionary<ChaveDeTexto, List<string>> txt = new Dictionary<ChaveDeTexto, List<string>>()
    {
        {ChaveDeTexto.katids1, new List<string>()
            {
            "Olá estranho, aqui é a cidade de <color=yellow>Katids</color>",
            "Se você olhar para o leste verá a grande represa do rio Mariinque",
            "A necessidade por energia dos tempos antigos motivou a construção de represas no rio Mariinque",
            "As represas acabaram separando as planícies de Orion",
            "Atraves da represa pelo caminho do rio podemos alcaçar a planície de <color=yellow>Infinity</color>"
        }
        },
        { ChaveDeTexto.katids2,new List<string>()
        {
            "Seguindo pelos caminhos do sul é possível alcançar a cidade de <color=yellow>Marjan</color>",
            "Nesse caminho é necessário passar por algumas pequenas cavernas e pelo campo de um templo Agnun",
            "Da cidade de Marjan é possível ver a <color=yellow>Torre da Vida Eterna</color>"
        }
        },
        { ChaveDeTexto.katids3,new List<string>()
        {
            "Você já ouviu falar da <color=yellow>Torre da vida Eterna</color>?",
            "Ela é a morada do imperador",
            "Dizem as lendas de Orion que é uma torre construida pelo acordo dos deuses em proteger o imperador",
            "Fechada por magia só pode ser aberta pelos autorizados do imperador ou...",
            "Por alguém que possua oito dos medalhões dos deuses"
        }
        },
        { ChaveDeTexto.katids4,new List<string>()
        {
            "Pelo caminho do sudoeste você pode encontrar uma entrada para os esgotos da planície",
            "Dizem que os esgotos ligam um caminho para a arena divina que fica no fundo do rio",
            "Essa é a arena divida de Drag, o deus das águas",
            "Lá, você pode conseguir o medalhão das aguas"
        }
        },
        { ChaveDeTexto.katids5,new List<string>()
        {
            "Ei rapaz, que roupinha é essa?",
            "Se a <color=orange>Garra Governamental</color> te pega vestido assim ",
            "Não quero nem saber o que farão com você"
        }
        },
        { ChaveDeTexto.katids6,new List<string>()
        {
            "Você já visitou o <color=orange>Armagedom</color>?",
            "Nas sedes do Armagedom espalhadas por Orion você pode fazer coisas importantes como:",
            "Restaurar a vida dos seus criatures",
            "Pegar os criatures da sua reserva, os chamados criatures armagedados",
            "Comprar <color=cyan>pergaminhos de Armagedom</color>",
            "Esse podem te ajudar a retornar para um dos armagedoms que você visitou"
        }
        },
        {
            ChaveDeTexto.falandoPrimeiroComIan,new List<string>()
            {
                "Ola viajante! Meu nome é <color=orange>Ian</color>.",
                "Eu estudo para ser um sacerdote de <color=cyan>Log</color>, o deus da sabedoria e conhecimento.",
                "Eu estou com um grave problema viajante.",
                "Um ser repugnante e mediocre roubou a caneta sagrada que eu usava para escrever pergaminhos",
                "Ele se esgueirou por entre aquelas pedras...",
                "Se você puder me ajudar, recuperando minha caneta, poderei te recompensar escrevendo pergaminhos para você"
            }
        },
        {
            ChaveDeTexto.falandoPrimeiroComDerek,new List<string>()
            {
                "Ola viajante, eu sou <color=orange>Derek</color>, sou um paladino de <color=cyan>Laurense</color>, o deus da coragem",
                "Estou aqui descansando um pouco antes de retomar minha viajem.",
                "Estou numa missão muito importante e preciso chegar até o templo da sabedoria",
                "mas faz algum tempo estou sendo seguido por um sujeitinho mal encarado",
                "Acredito que ele esteja agora me aguardando na sombra da represa",
                "Tenha muito cuidado com ele!"
            }
        },
        {
            ChaveDeTexto.IanDepoisDeDerek,new List<string>()
            {
                "Ola viajante! Meu nome é <color=orange>Ian</color>.",
                "Eu estudo para ser um sacerdote de <color=cyan>Log</color>, o deus da sabedoria e conhecimento.",
                "Você deve ter encontrado por entre as montanhas aquele sujeitinho repugnante e mediocre que me roubou",
                "Ele me tomou a caneta sagrada que eu usava para escrever pergaminhos!",
                "Se você puder me ajudar, recuperando minha caneta, poderei te recompensar escrevendo pergaminhos para você"
            }
        },
        {
            ChaveDeTexto.DerekDepoisDeIan,new List<string>()
            {
                "Então você quer me tomar a caneta sagrada de Log??",
                "Prepare-se para lutar!!",
                ""
            }
        },
        {
            ChaveDeTexto.DerekDerrotado,new List<string>()
            {
                "Não acredito que você me venceu!!",
                "Você deve ser um lacaio daquele mal carater!!",
                "Ouça o que eu digo viajante! Nos encontraremos de novo",
                "E quando nos encontrarmos você poderá não ter a mesma sorte"
            }
        },
        {
            ChaveDeTexto.IanComCaneta,new List<string>()
            {
                "Muito obrigado viajante!! Você recuperou a minha caneta!",
                "A partir de agora escreverei pergaminhos para você",
                "Mas para escreve-los precisarei do material adequado",
                "Para cada pergaminho que eu escrever para você preciso de uma quantidade de tinta sagrada",
                "Os <color=orange>Clérigos de Log</color> costumam esconder alguns tinteiros nos caminhos de Orion",
                "Me traga <color=cyan>tinteiros sagrados de Log</color> e escreverei pergaminhos para você"
            }
        },
        {
            ChaveDeTexto.miniArmagedomDeKatids,new List<string>()
            {
                "Olá novato. Fique a vontade para recuperar seus criatures, descanse!"
            }
        },
        {
            ChaveDeTexto.companhiaDoMiniArmgdDeKatids,new List<string>()
            {
                "Você está indo bem novato. Passando pela planicie das ruinas do templo você encontrará a cidade de <color=yellow>Marjan</color>.",
                "Da cidade de Marjan você poderá ver a <color=yellow>Torre da Vida Eterna</color> morada do imperador",
                "Estamos instalando mini armagedom's pelas planícies de Orion",
                "Se sua jornada se estender mais talvez poderemos nos encontrar novamente."
            }
        },
        {
            ChaveDeTexto.NPCdaRepresa,new List<string>()
            {
                "Ola viajante, aqui é a grande represa de Katids, ela é responsável pelo abastecimento de agua de...",
                "... várias cidades de Orion",
                "O lago da represa liga algumas planícies que dão acesso a cidades",
                "Sei que você deve estar interssado em entrar na represa para alcançar essas planícies, mas...",
                "A <color=orange>Garra Governamental</color> proibiu a abertura da represa para visitantes ou viajantes",
                "Para entrar na represa você precisa de uma autorização da Garra Governamental"
            }
        },
        {
            ChaveDeTexto.CamusMontanhaDoTemplo1,new List<string>()
            {
                "Ola. Você também é um turista que veio visitar os supostos templos Agnun?",
                "Durante as minhas viagens por Orion percebi que o senso comum acredita que os Agnun's são os mais próximos dos deuses",
                "Disseram-me que aqui era o local de um grande templo Agnun",
                "O que vejo são vestigios de destruição ou uma obra de um designer preguiçoso"
            }
        },
        {
            ChaveDeTexto.CamusMontanhaDoTemplo2,new List<string>()
            {
                "Meu nome é <color=orange>Camy Hanahatus</color>. Posso me dizer uma estudiosa da teologia de Orion",
                "Venho pesquisando durante algum tempo as diferentes faces da crença em um deus",
                "Tenho ficado muito surpresa com a customização da fé",
                "Cada pessoa acredita da forma que lhe é mais conveniente em determinado deus",
                "Não acredito que nossos deuses estejam satisfeitos com esse tipo de fé"
            }
        },
        {
            ChaveDeTexto.CamusMontanhaDoTemplo3,new List<string>()
            {
                "Da beirada da montanha você pode ver uma caverna, esta te levará a cidade de <color=yellow>Marjan</color>",
                "Estive lá faz pouco tempo. Tem lojas interessantes por lá, talvez te interesse visita-las"
            }
        },
        {
            ChaveDeTexto.CamusMontanhaDoTemplo4,new List<string>()
            {
                "Umas das coisas mais interessantes que vi na minha visita foram pequenos altares cerimoniais dedicados a deuses especificos",
                "A sudeste daqui um altar dedicado a <color=cyan>Boutjoi</color> o deus da natureza, conhecido pela pele de rocha e defesa intransponivel",
                "A nordeste daqui um altar dedicado a <color=cyan>Ananda</color> deusa da mágia e do poder",
                "A oeste há um altar dedicado a <color=cyan>Laurense</color> deus da coragem e da força",
                "Pessoas de fé dizem que você pode conseguir a benção dos deuses visitando seus altares",
                "Será que vale a pena ir até lá?"
            }
        }
    };
}