using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeViewLoad : MonoBehaviour {
    [SerializeField] private Image escurecedorUpper;
    [SerializeField] private Image escurecedorLower;
    private Color corDoFade = Color.black;
    private FaseDaqui fase = FaseDaqui.emEspera;
    private float tempoDeEscurecimento = 1;
    private float tempoDecorrido = 0;
    

    public bool escurecer = false;
    public bool clarear = false;

    private enum FaseDaqui
    {
        emEspera,
        escurecendo,
        clareando
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (escurecer)
        {
            IniciarFadeOut();
            escurecer = false;
        }

        if (clarear)
        {
            IniciarFadeIn();
            clarear = false;
        }
        switch (fase)
        {
            case FaseDaqui.escurecendo:
                tempoDecorrido += Time.fixedDeltaTime;
                corDoFade.a = tempoDecorrido / tempoDeEscurecimento;
                escurecedorUpper.color = corDoFade;
                escurecedorLower.color = corDoFade;

                if (tempoDecorrido > tempoDeEscurecimento)
                {
                    fase = FaseDaqui.emEspera;
                    EventAgregator.Publish(EventKey.fadeOutComplete, null);
                }
            break;
            case FaseDaqui.clareando:
                tempoDecorrido += Time.fixedDeltaTime;
                corDoFade.a = (tempoDeEscurecimento- tempoDecorrido) / tempoDeEscurecimento;
                escurecedorUpper.color = corDoFade;
                escurecedorLower.color = escurecedorUpper.color;
                if (tempoDecorrido > tempoDeEscurecimento)
                {
                    fase = FaseDaqui.emEspera;
                    EventAgregator.Publish(EventKey.fadeInComplete, null);
                }
            break;
        }
	}

    public void LimparFade()
    {
        escurecedorUpper.color = new Color(0, 0, 0,0);
        escurecedorLower.color = new Color(0, 0, 0, 0);
    }

    public void IniciarFadeOut(Color corDoFade = default(Color))
    {
        this.corDoFade = corDoFade;
        this.corDoFade.a = 0;
        fase = FaseDaqui.escurecendo;
        tempoDecorrido = 0;
        EventAgregator.Publish(EventKey.fadeOutStart, null);
    }

    public void IniciarFadeIn(Color corDoFade = default(Color))
    {
        this.corDoFade = corDoFade;
        this.corDoFade.a = 1;
        fase = FaseDaqui.clareando;
        tempoDecorrido = 0;
        EventAgregator.Publish(EventKey.fadeInStart, null);
    }
}
