[System.Serializable]
public class tipos  {

	[UnityEngine.SerializeField]private float _mod;
	[UnityEngine.SerializeField]private string _nome;

    public static tipos[] AplicaContraTipos(nomeTipos nomeDoTipo)
    {
        tipos[] retorno = new tipos[System.Enum.GetValues(typeof(nomeTipos)).Length];

        switch (nomeDoTipo)
        {
            case nomeTipos.Agua:
                retorno = new tipos[]
                    {
                        new tipos (){ Nome = nomeTipos.Agua.ToString(),    Mod = 1},
                        new tipos (){ Nome = nomeTipos.Fogo.ToString(),    Mod = 0.25f},
                        new tipos (){ Nome = nomeTipos.Planta.ToString(),  Mod = 1.75f},
                        new tipos (){ Nome = nomeTipos.Gelo.ToString(),    Mod = 2},
                        new tipos (){ Nome = nomeTipos.Terra.ToString(),   Mod = 0.5f},
                        new tipos (){ Nome = nomeTipos.Pedra.ToString(),   Mod = 0.5f},
                        new tipos (){ Nome = nomeTipos.Psiquico.ToString(),Mod = 1.75f},
                        new tipos (){ Nome = nomeTipos.Eletrico.ToString(),Mod = 2f},
                        new tipos (){ Nome = nomeTipos.Normal.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Veneno.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Inseto.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Voador.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Gas.ToString(),     Mod = 1}
                    };

            break;
            case nomeTipos.Planta:
                retorno = new tipos[]
                    {
                        new tipos (){ Nome = nomeTipos.Agua.ToString(),    Mod = 0.25f},
                        new tipos (){ Nome = nomeTipos.Fogo.ToString(),    Mod = 2f},
                        new tipos (){ Nome = nomeTipos.Planta.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Gelo.ToString(),    Mod = 1.25f},
                        new tipos (){ Nome = nomeTipos.Terra.ToString(),   Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Pedra.ToString(),   Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Psiquico.ToString(),Mod = 1.25f},
                        new tipos (){ Nome = nomeTipos.Eletrico.ToString(),Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Normal.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Veneno.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Inseto.ToString(),  Mod = 1.25f},
                        new tipos (){ Nome = nomeTipos.Voador.ToString(),  Mod = 1.25f},
                        new tipos (){ Nome = nomeTipos.Gas.ToString(),     Mod = 1}
                    };

            break;
            case nomeTipos.Fogo:
                retorno = new tipos[]
                    {
                        new tipos (){ Nome = nomeTipos.Agua.ToString(),    Mod = 1.5f},
                        new tipos (){ Nome = nomeTipos.Fogo.ToString(),    Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Planta.ToString(),  Mod = 0.5f},
                        new tipos (){ Nome = nomeTipos.Gelo.ToString(),    Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Terra.ToString(),   Mod = 1.5f},
                        new tipos (){ Nome = nomeTipos.Pedra.ToString(),   Mod = 1.25f},
                        new tipos (){ Nome = nomeTipos.Psiquico.ToString(),Mod = 1.75f},
                        new tipos (){ Nome = nomeTipos.Eletrico.ToString(),Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Normal.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Veneno.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Inseto.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Voador.ToString(),  Mod = 1.25f},
                        new tipos (){ Nome = nomeTipos.Gas.ToString(),     Mod = 2}
                    };

            break;
            case nomeTipos.Voador:
                retorno = new tipos[]
                    {
                        new tipos (){ Nome = nomeTipos.Agua.ToString(),    Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Fogo.ToString(),    Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Planta.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Gelo.ToString(),    Mod = 2f},
                        new tipos (){ Nome = nomeTipos.Terra.ToString(),   Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Pedra.ToString(),   Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Psiquico.ToString(),Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Eletrico.ToString(),Mod = 1.5f},
                        new tipos (){ Nome = nomeTipos.Normal.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Veneno.ToString(),  Mod = 1.5f},
                        new tipos (){ Nome = nomeTipos.Inseto.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Voador.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Gas.ToString(),     Mod = 0.25f}
                    };
            break;
            case nomeTipos.Inseto:
                retorno = new tipos[]
                    {
                        new tipos (){ Nome = nomeTipos.Agua.ToString(),    Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Fogo.ToString(),    Mod = 1.75f},
                        new tipos (){ Nome = nomeTipos.Planta.ToString(),  Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Gelo.ToString(),    Mod = 1.25f},
                        new tipos (){ Nome = nomeTipos.Terra.ToString(),   Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Pedra.ToString(),   Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Psiquico.ToString(),Mod = 0.5f},
                        new tipos (){ Nome = nomeTipos.Eletrico.ToString(),Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Normal.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Veneno.ToString(),  Mod = 2f},
                        new tipos (){ Nome = nomeTipos.Inseto.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Voador.ToString(),  Mod = 1.75f},
                        new tipos (){ Nome = nomeTipos.Gas.ToString(),     Mod = 1.5f}
                    };
            break;
            case nomeTipos.Psiquico:
                retorno = new tipos[]
                    {
                        new tipos (){ Nome = nomeTipos.Agua.ToString(),    Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Fogo.ToString(),    Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Planta.ToString(),  Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Gelo.ToString(),    Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Terra.ToString(),   Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Pedra.ToString(),   Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Psiquico.ToString(),Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Eletrico.ToString(),Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Normal.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Veneno.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Inseto.ToString(),  Mod = 1.75f},
                        new tipos (){ Nome = nomeTipos.Voador.ToString(),  Mod = 1.75f},
                        new tipos (){ Nome = nomeTipos.Gas.ToString(),     Mod = 1.5f}
                    };
            break;
            case nomeTipos.Normal:
                retorno = new tipos[]
                    {
                        new tipos (){ Nome = nomeTipos.Agua.ToString(),    Mod = 1},
                        new tipos (){ Nome = nomeTipos.Fogo.ToString(),    Mod = 1},
                        new tipos (){ Nome = nomeTipos.Planta.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Gelo.ToString(),    Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Terra.ToString(),   Mod = 1},
                        new tipos (){ Nome = nomeTipos.Pedra.ToString(),   Mod = 1},
                        new tipos (){ Nome = nomeTipos.Psiquico.ToString(),Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Eletrico.ToString(),Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Normal.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Veneno.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Inseto.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Voador.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Gas.ToString(),     Mod = 1},
                    };
            break;
            case nomeTipos.Veneno:
                retorno = new tipos[]
                    {
                        new tipos (){ Nome = nomeTipos.Agua.ToString(),    Mod = 1.25f},
                        new tipos (){ Nome = nomeTipos.Fogo.ToString(),    Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Planta.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Gelo.ToString(),    Mod = 1.25f},
                        new tipos (){ Nome = nomeTipos.Terra.ToString(),   Mod = 1.25f},
                        new tipos (){ Nome = nomeTipos.Pedra.ToString(),   Mod = 1.25f},
                        new tipos (){ Nome = nomeTipos.Psiquico.ToString(),Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Eletrico.ToString(),Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Normal.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Veneno.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Inseto.ToString(),  Mod = 0.5f},
                        new tipos (){ Nome = nomeTipos.Voador.ToString(),  Mod = 1},
                        new tipos (){ Nome = nomeTipos.Gas.ToString(),     Mod = 0.5f},
                    };
            break;
            case nomeTipos.Pedra:
                retorno = new tipos[]
                    {
                        new tipos (){ Nome = nomeTipos.Agua.ToString(),    Mod = 2f},
                        new tipos (){ Nome = nomeTipos.Fogo.ToString(),    Mod = 0.25f},
                        new tipos (){ Nome = nomeTipos.Planta.ToString(),  Mod = 1.75f},
                        new tipos (){ Nome = nomeTipos.Gelo.ToString(),    Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Terra.ToString(),   Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Pedra.ToString(),   Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Psiquico.ToString(),Mod = 1.5f},
                        new tipos (){ Nome = nomeTipos.Eletrico.ToString(),Mod = 0.1f},
                        new tipos (){ Nome = nomeTipos.Normal.ToString(),  Mod = 0.5f},
                        new tipos (){ Nome = nomeTipos.Veneno.ToString(),  Mod = 0.5f},
                        new tipos (){ Nome = nomeTipos.Inseto.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Voador.ToString(),  Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Gas.ToString(),     Mod = 0.5f},
                    };
            break;
            case nomeTipos.Eletrico:
                retorno = new tipos[]
                    {
                        new tipos (){ Nome = nomeTipos.Agua.ToString(),    Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Fogo.ToString(),    Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Planta.ToString(),  Mod = 1.25f},
                        new tipos (){ Nome = nomeTipos.Gelo.ToString(),    Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Terra.ToString(),   Mod = 1.5f},
                        new tipos (){ Nome = nomeTipos.Pedra.ToString(),   Mod = 1.75f},
                        new tipos (){ Nome = nomeTipos.Psiquico.ToString(),Mod = 1.5f},
                        new tipos (){ Nome = nomeTipos.Eletrico.ToString(),Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Normal.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Veneno.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Inseto.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Voador.ToString(),  Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Gas.ToString(),     Mod = 1f},
                    };
            break;
            case nomeTipos.Terra:
                retorno = new tipos[]
                    {
                        new tipos (){ Nome = nomeTipos.Agua.ToString(),    Mod = 2f},
                        new tipos (){ Nome = nomeTipos.Fogo.ToString(),    Mod = 0.1f},
                        new tipos (){ Nome = nomeTipos.Planta.ToString(),  Mod = 1.75f},
                        new tipos (){ Nome = nomeTipos.Gelo.ToString(),    Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Terra.ToString(),   Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Pedra.ToString(),   Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Psiquico.ToString(),Mod = 1.5f},
                        new tipos (){ Nome = nomeTipos.Eletrico.ToString(),Mod = 0.15f},
                        new tipos (){ Nome = nomeTipos.Normal.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Veneno.ToString(),  Mod = 0.95f},
                        new tipos (){ Nome = nomeTipos.Inseto.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Voador.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Gas.ToString(),     Mod = 0.75f},
                    };
            break;
            case nomeTipos.Gas:
                retorno = new tipos[]
                    {
                        new tipos (){ Nome = nomeTipos.Agua.ToString(),    Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Fogo.ToString(),    Mod = 2f},
                        new tipos (){ Nome = nomeTipos.Planta.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Gelo.ToString(),    Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Terra.ToString(),   Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Pedra.ToString(),   Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Psiquico.ToString(),Mod = 0.5f},
                        new tipos (){ Nome = nomeTipos.Eletrico.ToString(),Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Normal.ToString(),  Mod = 1f},
                        new tipos (){ Nome = nomeTipos.Veneno.ToString(),  Mod = 0.75f},
                        new tipos (){ Nome = nomeTipos.Inseto.ToString(),  Mod = 0.5f},
                        new tipos (){ Nome = nomeTipos.Voador.ToString(),  Mod = 2f},
                        new tipos (){ Nome = nomeTipos.Gas.ToString(),     Mod = 1f},
                    };
            break;
        }
        return retorno;
    }

    public tipos()
	{
		_mod = 1.0f;
		_nome = "";
	}

	public float Mod
	{
		get{return _mod;}
		set{_mod = value;}
	}

	public string Nome
	{
		get{return _nome;}
		set{_nome = value;}
	}

    public static string NomeEmLinguas(nomeTipos nome)
    {
        return nome.ToString();
    }
}

public enum nomeTipos
{
    nulo=-1,
	Agua,
	Fogo,
	Planta,
	Terra,
	Pedra,
	Psiquico,
	Eletrico,
	Normal,
	Veneno,
	Inseto,
	Voador,
	Gas,
    Gelo
}
