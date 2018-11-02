using UnityEngine;
using System.Collections;

public static class ActionManager
{
    static Transform visualizado;
    static System.Action acao;

    public static bool useiCancel = false;
    public static bool anularAcao = false;


    public static bool PodeVisualizarEste(Transform T)
    {
        bool pode = false;
        if (visualizado != null)
        {
            if (Vector3.Distance(GameController.g.Manager.transform.position, T.position)
                <
                Vector3.Distance(GameController.g.Manager.transform.position, visualizado.position))
            {
                pode = true;
                visualizado = T;
                acao = null;
            }

            if (visualizado == T)
                pode = true;
        }
        else
        {
            pode = true;
            visualizado = T;
            acao = null;
        }
        return pode;
    }

    public static bool TransformDeActionE(Transform T)
    {
        return T == visualizado;
    }

    public static void ModificarAcao(Transform T,System.Action acao)
    {
        visualizado = T;
        ActionManager.acao = acao;
    }

    public static void VerificaAcao()
    {

        if (!anularAcao )
        {
            //anularAcao = true;
            if (visualizado != null)
                if (visualizado.gameObject.activeSelf)
                    if (acao != null)
                    {
                        acao();
                    }else if(GameController.g.HudM.P_Action.ActiveSelf)
                    {
                        visualizado.GetComponent<AtivadorDeBotao>().FuncaoDoBotao();
                    }
        }
        else if (anularAcao)
            anularAcao = false;
    }

    public static bool ButtonUp(int n,Controlador c)
    {
        bool press = CommandReader.ButtonUp(n, c);
        if (!esteQuadro&& press)
        {
            Debug.Log("Botao: " + n);
            AgendaEsseQuadro();
            return true;
        }
        else return false;
    }

    static bool esteQuadro = false;
    static void AgendaEsseQuadro()
    {
        esteQuadro = true;
        GlobalController.g.StartCoroutine(VoltaQuadro());
    }

    static IEnumerator VoltaQuadro()
    {
        yield return new WaitForEndOfFrame();

        esteQuadro = false;
    }
}
