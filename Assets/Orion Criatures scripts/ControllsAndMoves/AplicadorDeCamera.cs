using UnityEngine;
using System.Collections;

public class AplicadorDeCamera : MonoBehaviour
{
    public static AplicadorDeCamera cam;

    [SerializeField] private CameraBasica basica;
    [SerializeField] private CameraDeLuta cDeLuta;
    [SerializeField] private CameraExibicionista cExibe;
    [SerializeField] private CameraDirecionavel cDir;

    private EstiloDeCamera estilo = EstiloDeCamera.passeio;
    public enum EstiloDeCamera
    {
        passeio,
        luta,
        lutaDir,
        mostrandoUmCriature,
        focandoPonto,
        basica,
        desligada
    }

    public CameraBasica Basica
    {
        get { return basica; }
        private set { basica = value; }
    }

    public CameraDirecionavel Cdir
    {
        get { return cDir; }
    }

    public EstiloDeCamera Estilo
    {
        get { return estilo; }
        private set { estilo = value; }
    }

    // Use this for initialization
    void Start()
    {
        basica.Start(transform);
        if (ExistenciaDoController.AgendaExiste(Start, this))
        {
            cam = this;

            cDir = new CameraDirecionavel(new CaracteristicasDeCamera() {
                alvo = GameController.g.Manager.transform,
                minhaCamera = transform });
            NovoFocoBasico(GameController.g.Manager.transform, 10, 10, true);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!GameController.g.HudM.MenuDePause.EmPause)
        switch (Estilo)
        {
            case EstiloDeCamera.passeio:
                //if(cDir!=null)
                  //  cDir.AplicaCamera(1);
                basica.Update();
            break;
            case EstiloDeCamera.luta:
                    if (MyN3dsCommandDefines.FocarCamera())
                        estilo = EstiloDeCamera.lutaDir;
                cDeLuta.Update();
            break;
            case EstiloDeCamera.lutaDir:
                    if (MyN3dsCommandDefines.FocarCamera())
                        estilo = EstiloDeCamera.luta;
                    Cdir.AplicaCamera(1);
            break;
            case EstiloDeCamera.mostrandoUmCriature:
                cExibe.MostrandoUmCriature();
            break;
            case EstiloDeCamera.basica:
                basica.Update();
            break;
        }
    }

    public void FocarBasica(Transform T,float altura,float distancia)
    {
        /*
        cDir.SetarCaracteristicas(new CaracteristicasDeCamera()
        { alvo = T,
            minhaCamera = transform,
            altura = altura,
            distancia = distancia });*/
        NovoFocoBasico(T, 10, 10, true);
        Estilo = EstiloDeCamera.passeio;
    }

    public void InicializaCameraExibicionista(Transform focoComCharacterController)
    {
        InicializaCameraExibicionista(focoComCharacterController, 
            focoComCharacterController.GetComponent<CharacterController>().height);
    }

    public void InicializaCameraExibicionista(Transform doFoco, float altura, bool contraParedes = false)
    {
        if (cExibe != null)
            cExibe.OnDestroy();
        cExibe = new CameraExibicionista(transform, doFoco, altura,contraParedes);
        Estilo = EstiloDeCamera.mostrandoUmCriature;
    }    

    public void InicializaCameraDeLuta(CreatureManager alvo,Transform inimigo)
    {
        cDeLuta.Start(transform,alvo.transform,alvo.MeuCriatureBase.alturaCameraLuta,alvo.MeuCriatureBase.distanciaCameraLuta);
        cDeLuta.T_Inimigo = inimigo;
        Estilo = EstiloDeCamera.luta;
    }

    public bool FocarPonto(Vector3 deslFocoCamera,
        float velocidadeTempoDeFoco,
        float distancia = 6,
        float altura = -1,
        bool comTempo = false)
    {
        return FocarPonto(velocidadeTempoDeFoco,distancia,altura,comTempo,default(Vector3),false,deslFocoCamera);
    }

    public bool FocarPonto(float velocidadeTempoDeFoco,
        float distancia = 6,
        float altura = -1,
        bool comTempo = false,
        Vector3 dirIni = default(Vector3),
        bool focoDoTransform = false,
        Vector3 deslFocoCamera = default(Vector3)
        )
    {
        Estilo = EstiloDeCamera.focandoPonto;
        return cExibe.MostrarFixa(velocidadeTempoDeFoco,distancia,altura,comTempo,dirIni,focoDoTransform,deslFocoCamera);
    }

    public void NovoFocoBasico(Transform T, float altura, float distancia, bool contraParedes = false, bool dirDeObj = false)
    {
        Estilo = EstiloDeCamera.basica;
        basica.Start(transform);
        Basica.NovoFoco(T,altura,distancia,contraParedes,dirDeObj);
    }

    public void DesligarMoveCamera()
    {
        estilo = EstiloDeCamera.desligada;
    }

    public void FocarDirecionavel()
    {
        Cdir.EstadoAtual = EstadoDeCamera.focando;
    }
}
