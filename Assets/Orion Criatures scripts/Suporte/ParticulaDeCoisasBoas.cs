using UnityEngine;
using System.Collections;

public class ParticulaDeCoisasBoas
{
    private const float INTERVALO_DE_TEMPO_DE_PARTICULAS = 0.25F;
    private const float DESLOCAMENTO_DAS_PARTICULAS = 100;
    private const float REESCALONAMENTO_DA_PARTICULA = 100;
    private const float TEMPO_DE_DESTOICAO_DA_PARTICULA = 2;

    static void AdapteParticula(GameObject G, int multiplicador)
    {
        RectTransform rt2 = G.GetComponent<RectTransform>();
        GameController.g.StartCoroutine(AgendaDestroy(G, TEMPO_DE_DESTOICAO_DA_PARTICULA));
        rt2.sizeDelta = new Vector2(REESCALONAMENTO_DA_PARTICULA, REESCALONAMENTO_DA_PARTICULA);
        rt2.anchoredPosition += new Vector2(multiplicador * DESLOCAMENTO_DAS_PARTICULAS, 0);
    }

    static IEnumerator AgendaDestroy(GameObject G,float tempo)
    {
        yield return new WaitForSecondsRealtime(tempo);
        MonoBehaviour.Destroy(G);
    }

    public static IEnumerator ParticulasMaisBotao(RectTransform rt,System.Action finalizar)
    {
        
        GameObject particulaCoisaBoa = GameController.g.El.retorna(DoJogo.particulasCoisasBoasUI);
        GameObject particulaUpei = GameController.g.El.retorna(DoJogo.particulasUpeiDeNivel);

        GameObject G = ParentearNaHUD.Parentear(particulaCoisaBoa, rt);
        AdapteParticula(G, -1);

        yield return new WaitForSecondsRealtime(INTERVALO_DE_TEMPO_DE_PARTICULAS);
        G = ParentearNaHUD.Parentear(particulaCoisaBoa, rt);
        AdapteParticula(G, 0);

        yield return new WaitForSecondsRealtime(INTERVALO_DE_TEMPO_DE_PARTICULAS);
        G = ParentearNaHUD.Parentear(particulaUpei, rt);
        AdapteParticula(G, 1);
        
        yield return new WaitForSecondsRealtime(2*INTERVALO_DE_TEMPO_DE_PARTICULAS);
        finalizar();
        
    }
}
