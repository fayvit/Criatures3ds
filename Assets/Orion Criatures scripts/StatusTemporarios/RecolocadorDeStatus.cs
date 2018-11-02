using UnityEngine;
using System.Collections;

public class RecolocadorDeStatus
{
    public static void ColocarStatus(CriatureBase C)
    {

        DatesForTemporaryStatus dados;

        if (C.StatusTemporarios != null)
        {
            CreatureManager cm = null;

            for (int i = 0; i < C.StatusTemporarios.Count; i++)
            {
                dados = C.StatusTemporarios[i];

                if (GameController.g.Manager.Dados.CriaturesAtivos.IndexOf(C) == 0)
                {
                    cm = GameController.g.Manager.CriatureAtivo; ;
                    Debug.Log("ser ou naõ ser ");
                }

                switch (dados.Tipo)
                {
                    case TipoStatus.envenenado:


                        // Debug.Log(GameController.g.Manager.Dados.CriaturesAtivos.IndexOf(C) + " : " + cm.MeuCriatureBase.NomeID);

                        GameController.g.ContStatus.StatusDoHeroi.Add(new Envenenado()
                        {
                            Dados = dados,
                            CDoAfetado = cm,
                            OAfetado = C
                        });


                    break;
                    case TipoStatus.amedrontado:
                        GameController.g.ContStatus.StatusDoHeroi.Add(new Amedrontado()
                        {
                            Dados = dados,
                            CDoAfetado = cm,
                            OAfetado = C
                        });
                    break;
                    case TipoStatus.fraco:
                        GameController.g.ContStatus.StatusDoHeroi.Add(new Fraco()
                        {
                            Dados = dados,
                            CDoAfetado = cm,
                            OAfetado = C
                        });
                    break;
                    default:
                        Debug.Log("foi encontrado um status sem recolocação configurada");
                    break;
                }
            }
            if (cm != null)
                VerificaInsereParticulaDeStatus(cm);
        }
        else C.StatusTemporarios = new System.Collections.Generic.List<DatesForTemporaryStatus>();
    }

    public static void VerificaStatusDosAtivos()
    {
        for (int i = 0; i < GameController.g.Manager.Dados.CriaturesAtivos.Count; i++)
            ColocarStatus(GameController.g.Manager.Dados.CriaturesAtivos[i]);
    }

    public static void VerificaInsereParticulaDeStatus(CreatureManager C)
    {
        for (int i = 0; i < GameController.g.ContStatus.StatusDoHeroi.Count; i++)
        {
            StatusTemporarioBase sTb = GameController.g.ContStatus.StatusDoHeroi[i];

            if (GameController.g.Manager.Dados.CriaturesAtivos.IndexOf(sTb.OAfetado) 
                == GameController.g.Manager.Dados.CriaturesAtivos.IndexOf(C.MeuCriatureBase))
            {
                sTb.CDoAfetado = C;
                sTb.Start();
            }            
        }
    }
}
