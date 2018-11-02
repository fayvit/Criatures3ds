using UnityEngine;
using System.Collections;

public class FerramentasDeHud:MonoBehaviour
{
    protected float tempoAtiva = 0;
    protected bool entrando = false;

    protected void VerificaTempoAtiva(float tempoParaSair)
    {
        if (entrando)
        {
            tempoAtiva += Time.deltaTime;

            if (tempoAtiva > tempoParaSair)
                Esconde();
        }
    }

    public static void AtualizaPosHud(RectTransform Quem,escondeHudPara dir,float posAlvo,bool entrando,float velDeMove = 0.25f)
    {
        float maximo = (dir==escondeHudPara.Ypos ||dir==escondeHudPara.Xpos)?1:posAlvo;
        float minimo = (dir == escondeHudPara.Ypos || dir == escondeHudPara.Xpos) ? posAlvo : 0;

        int multiplicador = (dir == escondeHudPara.Ypos || dir == escondeHudPara.Xpos) ? 2 : 0;
       
        if (dir == escondeHudPara.Ypos || dir == escondeHudPara.Yneg)
        {
            if (entrando)
            {
                Quem.anchorMax = Vector2.Lerp(Quem.anchorMax, new Vector2(Quem.anchorMax.x, maximo), velDeMove);
                Quem.anchorMin = Vector2.Lerp(Quem.anchorMin, new Vector2(Quem.anchorMin.x, minimo), velDeMove);
            }
            else
            {
                Quem.anchorMax = Vector2.Lerp(Quem.anchorMax, new Vector2(Quem.anchorMax.x, multiplicador*maximo), velDeMove);
                Quem.anchorMin = Vector2.Lerp(Quem.anchorMin, new Vector2(Quem.anchorMin.x, multiplicador*minimo), velDeMove);
            }
        }
        else if (dir == escondeHudPara.Xpos || dir == escondeHudPara.Xneg)
        {
            if (entrando)
            {
                Quem.anchorMax = Vector2.Lerp(Quem.anchorMax, new Vector2(maximo, Quem.anchorMax.y), velDeMove);
                Quem.anchorMin = Vector2.Lerp(Quem.anchorMin, new Vector2(minimo,Quem.anchorMin.y), velDeMove);
            }
            else
            {
                Quem.anchorMax = Vector2.Lerp(Quem.anchorMax, new Vector2(multiplicador*maximo,Quem.anchorMax.y), velDeMove);
                Quem.anchorMin = Vector2.Lerp(Quem.anchorMin, new Vector2(multiplicador*minimo, Quem.anchorMin.y), velDeMove);
            }
        }
    }

    public virtual void Esconde()
    {
        entrando = false;
    }
}

public enum escondeHudPara
{
    Xpos,
    Xneg,
    Ypos,
    Yneg
}
