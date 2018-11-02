using UnityEngine;
using System.Collections.Generic;

public class ListaEncontravelDaPetrolifera
{

    public static List<Encontravel> EncontraveisDaqui
    {
        get
        {
            //List<Encontravel> retorno = new List<Encontravel>();
            //Vector3 pos = GameController.g.Manager.transform.position;
            
            return new List<Encontravel>()
                {
                    new Encontravel(nomesCriatures.Izicuolo,14,3,7),
                    new Encontravel(nomesCriatures.Cabecu,7,3,7),
                    new Encontravel(nomesCriatures.Rocketler,6,3,7),
                    new Encontravel(nomesCriatures.DogMour,8,3,7),
                    new Encontravel(nomesCriatures.Flam,2,3,7),
                    new Encontravel(nomesCriatures.Baratarab,14,3,7),
                    new Encontravel(nomesCriatures.Nessei,2,3,7),
                    new Encontravel(nomesCriatures.Iruin,14,3,7),
                    new Encontravel(nomesCriatures.Babaucu,6,3,7),
                    new Encontravel(nomesCriatures.Wisks,6,3,7),
                    new Encontravel(nomesCriatures.Onarac,6,3,7),
                    new Encontravel(nomesCriatures.Escorpion,9,3,7),
                    new Encontravel(nomesCriatures.Marak,6,3,7)
                };
            
        }
    }
}
