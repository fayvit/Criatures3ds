using UnityEngine;
using System.Collections.Generic;

public class ListaEncontravelDoEsgoto
{

    public static List<Encontravel> EncontraveisDaqui
    {
        get
        {
            //List<Encontravel> retorno = new List<Encontravel>();
            //Vector3 pos = GameController.g.Manager.transform.position;

            return new List<Encontravel>()
                {
                    new Encontravel(nomesCriatures.Cracler,14,6,9),
                    new Encontravel(nomesCriatures.Urkan,7,6,9),
                    new Encontravel(nomesCriatures.DogMour,6,6,9),
                    new Encontravel(nomesCriatures.Iruin,8,6,9),
                    new Encontravel(nomesCriatures.Flam,2,6,9),
                    new Encontravel(nomesCriatures.Baratarab,14,6,9),
                    new Encontravel(nomesCriatures.Izicuolo,2,6,9),
                    new Encontravel(nomesCriatures.Batler,14,6,9),
                    new Encontravel(nomesCriatures.Babaucu,6,6,9),
                    new Encontravel(nomesCriatures.Serpente,5,6,9),
                    new Encontravel(nomesCriatures.Steal,1,6,9),
                    new Encontravel(nomesCriatures.Onarac,6,6,9),
                    new Encontravel(nomesCriatures.Escorpion,9,6,9),
                    new Encontravel(nomesCriatures.Marak,6,6,9)
                };

        }
    }
}
