using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CameraDirecionavel
{
    [SerializeField]private CaracteristicasDeCamera caracteristicas;

    [System.NonSerialized] private Transform posCamHeroi;
    [System.NonSerialized] private Transform posCamCriature;

    private EstadoDeCamera estadoAtual = EstadoDeCamera.controlando;
    private DirecaoInduzida dir = new DirecaoInduzida();
    //private FocarUmCriature criatureFocado = new FocarUmCriature();

    private float x;
    private float y;
    public CameraDirecionavel(CaracteristicasDeCamera car)
    {
        this.caracteristicas = car;
        //       if(this.caracteristicas.alvo!=null && this.caracteristicas.minhaCamera!= null)
    }

    public void SetarCaracteristicas(CaracteristicasDeCamera car)
    {
        caracteristicas = car;

        if (car.alvo.tag == "Player")
        {
            if (posCamHeroi != null)
            {
                if (car.minhaCamera.position != posCamHeroi.position)
                {
                    car.minhaCamera.position = posCamHeroi.position;
                    car.minhaCamera.rotation = posCamHeroi.rotation;
                }
            }
        }
        else if (car.alvo.name == "CriatureAtivo")
        {
            if (posCamCriature != null)
            {
                car.minhaCamera.position = posCamCriature.position;
                car.minhaCamera.rotation = posCamCriature.rotation;
            }
        }
    }

    public Transform MinhaCamera
    {
        get { return caracteristicas.minhaCamera; }
    }

    public EstadoDeCamera EstadoAtual
    {
        get { return estadoAtual; }
        set { estadoAtual = value; }
    }

    public Vector3 DirecaoInduzida(float h, float v)
    {
        return dir.Direcao((estadoAtual == EstadoDeCamera.focando), MinhaCamera, h, v);
    }

    public void RetiraCameraFocado(/*GerenciadorDeCamera gerenteC*/)
    {
        //criatureFocado.RemoveMira(gerenteC.EstadoPersonagem.controlador);
        //gerenteC.EstadoAtual = EstadoDeCamera.controlando;
    }

    public void FocarCamera(/*GerenciadorDeCamera gerenteC*/)
    {
        Quaternion alvoQ = Quaternion.LookRotation(caracteristicas.alvo.forward +
                                                    caracteristicas.altura / 10 * Vector3.down);
        x = Mathf.LerpAngle(x, alvoQ.eulerAngles.y, caracteristicas.velocidadeMaxFoco * Time.deltaTime);
        y = Mathf.LerpAngle(y, alvoQ.eulerAngles.x, caracteristicas.velocidadeMaxFoco * Time.deltaTime);


        float paraContinha = Mathf.Min(Mathf.Abs(x - alvoQ.eulerAngles.y), Mathf.Abs(360 - Mathf.Abs(x - alvoQ.eulerAngles.y) % 360));

        
        if (paraContinha % 360 < 5 && Mathf.Abs(y - alvoQ.eulerAngles.x) % 360 < 15)
            estadoAtual = EstadoDeCamera.controlando;
            
    }

    public void AplicaCamera(float altura)
    {
        if (caracteristicas.alvo && caracteristicas.minhaCamera)
        {
#if UNITY_N3DS
            if (MyN3dsCommandDefines.FocarCamera())
            {
                EstadoAtual = EstadoDeCamera.focando;

            }
#endif
#if !UNITY_N3DS

            if (CommandReader.ButtonDown(8,GameController.g.Manager.Control)
                || CommandReader.ButtonDown(9, GameController.g.Manager.Control))
            {    
                EstadoAtual = EstadoDeCamera.focando;
                
            }
#endif

            if (estadoAtual==EstadoDeCamera.controlando)
                CameraControlavel();
            else if(estadoAtual==EstadoDeCamera.focando)
                FocarCamera();

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector3 position = rotation * (new Vector3(0.0f, 0.0f, -caracteristicas.distancia))
                + caracteristicas.alvo.position + (altura + caracteristicas.AlturaDoPersonagem) * Vector3.up;

            caracteristicas.minhaCamera.rotation = rotation;

            caracteristicas.minhaCamera.position = position + Vector3.up;

            SetarTransformsDeRetorno();

            CameraContraParedes.ContraParedes(caracteristicas.minhaCamera, caracteristicas.alvo, altura + caracteristicas.AlturaDoPersonagem);
        }
        else
            caracteristicas.alvo = GameController.g.Manager.transform;
    }

    void SetarTransformsDeRetorno()
    {
        Transform camera = caracteristicas.minhaCamera;

        if (caracteristicas.alvo.tag == "Player")
        {
            if (posCamHeroi == null)
            {
                posCamHeroi = new GameObject().transform;
                posCamHeroi.parent = GameController.g.transform;
                posCamHeroi.name = "Transform de guardar heroi";
            }

            
            posCamHeroi.position = camera.position;
            posCamHeroi.rotation = posCamHeroi.rotation;
        }
        else if (caracteristicas.alvo.name == "CriatureAtivo")
        {
            if (posCamCriature == null)
            {
                posCamCriature = new GameObject().transform;
                posCamCriature.parent = GameController.g.transform;
                posCamCriature.name = "Transform de guardar criature";
            }

            posCamCriature.position = camera.position;
            posCamCriature.rotation = camera.rotation;
        }
    }

    public void CameraControlavel()//(int numControl)
    {
#if !UNITY_N3DS
        x += CommandReader.GetAxis("Xcam", GameController.g.Manager.Control) * caracteristicas.xSpeed * 0.02f;
        y -= CommandReader.GetAxis("Ycam", GameController.g.Manager.Control) * caracteristicas.ySpeed * 0.02f;
#endif
#if UNITY_N3DS
        Vector2 V = MyN3dsCommandDefines.DirForCam();
        x += V.x;
        y -= V.y;
#endif
        y = ClampAngle(y, caracteristicas.yMinLimit, caracteristicas.yMaxLimit);
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}

[System.Serializable]
public class CaracteristicasDeCamera
{
    [HideInInspector] public Transform minhaCamera;
    [HideInInspector] public Transform alvo;
    public float velocidadeMaxFoco = 10f;
    public float distancia = 7.0f;
    public float altura = 3.0f;
    public float xSpeed = 125.0f;
    public float ySpeed = 60.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    private CharacterController controle;

    public float AlturaDoPersonagem
    {
        get
        {
            if (!controle)
                controle = alvo.GetComponent<CharacterController>();
            return controle.height / 2;
        }
    }

}

public enum EstadoDeCamera
{
    focando,
    controlando,
    estatica,
    criatureFocado
}