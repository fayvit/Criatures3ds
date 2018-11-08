using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour {

    [SerializeField] private Transform pos2Camera;
    [SerializeField] private Transform posEscondeheroi;
    [SerializeField] private Transform posRetornoDoheroi;
    [SerializeField] private Transform pos3Camera;

    private float tempoDecorrido = 0;
    private string[] falas;
    private FaseDaCamera faseCam = FaseDaCamera.alcancandoPos2;
    private FaseDaConversa faseFala = FaseDaConversa.emEspera;
    private DisparaTexto dispara;
    private ControlledMoveForCharacter cMove;
    

    private const float TEMPO_DO_DESLOCAMENTO_UM = 6;
    private enum FaseDaConversa
    {
        emEspera,
        iniciando,
        horaDoCorean,
        finalizar
    }

    private enum FaseDaCamera
    {
        emEspera,
        alcancandoPos2,
        horaDoCorean
    }

	// Use this for initialization
	void Start () {

        if (ExistenciaDoController.AgendaExiste(Start, this))
        {
            if (GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto))
                Destroy(gameObject);
            else
            {
                falas = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.entradinha).ToArray();
                dispara = GameController.g.HudM.DisparaT;
                dispara.IniciarDisparadorDeTextos();
                StartCoroutine(DesligaCamera());
                GameController.g.Manager.enabled = false;
                GameController.g.Manager.transform.position = posEscondeheroi.position;
                
            }
        }

    }

    IEnumerator DesligaCamera()
    {
        yield return new WaitForEndOfFrame();

        if (AplicadorDeCamera.cam != null)
        {
            AplicadorDeCamera.cam.enabled = false;
            Camera.main.transform.position = transform.position;
            Camera.main.transform.rotation = transform.rotation;

            yield return new WaitForEndOfFrame();
            Camera.main.transform.rotation = transform.rotation;

            yield return new WaitForSeconds(0.01f);
            Camera.main.transform.rotation = transform.rotation;

            yield return new WaitForSeconds(0.1f);
            Camera.main.transform.rotation = transform.rotation;

            yield return new WaitForSeconds(1f);
            Camera.main.transform.rotation = transform.rotation;
        }
        else
            StartCoroutine(DesligaCamera());
    }
	
	// Update is called once per frame
	void Update () {
        if (GameController.g!= null)
        {
            tempoDecorrido += Time.deltaTime;

            switch (faseFala)
            {
                case FaseDaConversa.emEspera:
                    if (tempoDecorrido > 1)
                    {
                        faseFala = FaseDaConversa.iniciando;
                    }
                    break;
                case FaseDaConversa.iniciando:
                    if (dispara.UpdateDeTextos(falas))
                    {
                        faseFala = FaseDaConversa.finalizar;
                        GameController.g.Manager.transform.position = posRetornoDoheroi.position;
                    }

                    if (dispara.IndiceDaConversa >= 10)
                    {
                        faseFala = FaseDaConversa.horaDoCorean;
                        Camera.main.transform.position = pos3Camera.position;
                        Camera.main.transform.rotation = pos3Camera.rotation;
                        GameController.g.Manager.transform.position = posRetornoDoheroi.position;
                        GameController.g.Manager.transform.rotation = posRetornoDoheroi.rotation;
                        cMove = new ControlledMoveForCharacter(new CaracteristicasDeMovimentacao()
                        {
                            caracPulo = new CaracteristicasDePulo()
                        }, 
                        GameController.g.Manager.transform);
                        cMove.ModificarOndeChegar(GameController.g.Manager.transform.position + Vector3.right * 10, 2);
                    }
                break;
                case FaseDaConversa.horaDoCorean:
                    cMove.UpdatePosition();

                    if (dispara.UpdateDeTextos(falas))
                    {
                        faseFala = FaseDaConversa.finalizar;
                    }
                break;
                case FaseDaConversa.finalizar:
                    AplicadorDeCamera.cam.enabled = true;
                    GameController.g.Manager.enabled = true;
                    faseFala = FaseDaConversa.emEspera;
                    faseCam = FaseDaCamera.emEspera;
                    enabled = false;
                break;
            }

            switch (faseCam)
            {
                case FaseDaCamera.alcancandoPos2:
                    if (tempoDecorrido <= TEMPO_DO_DESLOCAMENTO_UM)
                    {
                        Camera.main.transform.position = Vector3.Lerp(transform.position, pos2Camera.position, tempoDecorrido / TEMPO_DO_DESLOCAMENTO_UM);
                    }
                    else
                        faseCam = FaseDaCamera.emEspera;
                break;
            }
        }
	}
}
