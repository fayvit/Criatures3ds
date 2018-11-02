using UnityEngine;
using System.Collections;

public class Emissor
{
    public static Vector3 UseOEmissor(GameObject G,nomesGolpes nome)
    {
        GolpePersonagem gP = GolpePersonagem.RetornaGolpePersonagem(G,nome);

        if (GameController.g.estaEmLuta && G.name == "CriatureAtivo")
        {
            if(GameController.g.InimigoAtivo!=null)
                G.transform.rotation = Quaternion.LookRotation(
                    GameController.g.InimigoAtivo.transform.position - G.transform.position
                    );
        }
        else if (GameController.g.estaEmLuta && G.name != "CriatureAtivo")
            GameController.g.InimigoAtivo.IA.SuspendeNav();

            return  G.transform.Find(gP.Colisor.osso).position
            + G.transform.forward * (gP.DistanciaEmissora)
                + Vector3.up * gP.AcimaDoChao;
    }
}
