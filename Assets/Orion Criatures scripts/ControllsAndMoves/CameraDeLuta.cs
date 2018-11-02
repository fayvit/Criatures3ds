using UnityEngine;
using System.Collections;

[System.Serializable]
public class CameraDeLuta
{
    [SerializeField]private Transform tInimigo;
    [SerializeField]private Transform alvo;
    [SerializeField]private float altura = 3;
    [SerializeField]private float velocidadeMaxFoco = 10f;
    [SerializeField]private float distancia = 7.0f;
    [SerializeField]private float escalA = 1;

    private float x = 0;
    private float y = 0;

    private Transform transform;

    public Transform T_Inimigo
    {
        get { return tInimigo; }
        set { tInimigo = value; }
    }
    // Use this for initialization
    public void Start(Transform aCamera,Transform alvo,float altura, float distancia)
    {
        transform = aCamera;
        this.alvo = alvo;

        this.altura = altura;
        this.distancia = distancia;
        /*
        Vector3 angles = alvo.eulerAngles;
        x = angles.y;
        y = angles.x;
        */

        escalA = alvo.GetComponent<CharacterController>().height;
    }

    // Update is called once per frame
    public void Update()
    {
        if (tInimigo && alvo && transform)
            focoDeLuta();
        else
            Debug.LogAssertion("transforms não setados corretamente, inimigo = " + tInimigo + ", alvo= " + alvo + ", camera = " + transform);
    }

    void focoDeLuta()
    {
        //if (tInimigo == null)
          //  tInimigo = GameObject.Find("inimigo").transform;

        Vector3 direcaoDaVisao
            = Vector3.ProjectOnPlane(tInimigo.position - transform.position, Vector3.up);

        Quaternion alvoQ = Quaternion.LookRotation(direcaoDaVisao +
                                                   altura / 10 * Vector3.down);

        x = Mathf.LerpAngle(x, alvoQ.eulerAngles.y, velocidadeMaxFoco * Time.deltaTime);
        y = Mathf.LerpAngle(y, alvoQ.eulerAngles.x, velocidadeMaxFoco * Time.deltaTime);



       // if (Mathf.Abs(x - alvoQ.eulerAngles.y) % 360 < 5 && Mathf.Abs(y - alvoQ.eulerAngles.x) % 360 < 15f)
         //   focar = false;

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        Vector3 position = rotation * (new Vector3(0.0f, 0.0f, -distancia)) + alvo.position
            + (escalA + altura / 8) * Vector3.up;

        transform.rotation = Quaternion.Lerp(transform.rotation,
                            rotation,
                                           50 * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position,
                                        position,
                                        50 * Time.deltaTime);

         contraParedes(transform, alvo, escalA);

    }

    public static bool contraParedes(Transform cameraP, Transform alvo, float escalA, bool suave = false)
    {
        RaycastHit raioColisor;
        Debug.DrawLine(cameraP.position, alvo.position + escalA * Vector3.up, Color.blue);


        Debug.DrawLine(alvo.position + 2 * Vector3.up, alvo.position -
                       Vector3.Project(alvo.position - cameraP.position, alvo.forward) + 2 * Vector3.up,
                       Color.green);
        if (Physics.Linecast(alvo.position + escalA * Vector3.up, cameraP.position, out raioColisor, 9))
        { 
            Debug.DrawLine(cameraP.position, raioColisor.point, Color.red);
            
            if (raioColisor.transform.tag != "Player"
               &&
               raioColisor.transform.tag != "Criature"
               &&
               raioColisor.transform.tag != "desvieCamera"
               )
            {
                if (suave)
                {
                    cameraP.position = Vector3.Lerp(cameraP.position,
                        raioColisor.point + raioColisor.normal*0.2f, 25 * Time.deltaTime);
                }
                else
                    cameraP.position = //Vector3.Lerp(cameraP.position,
                        raioColisor.point + cameraP.forward * 0.2f;
                    //           50*Time.deltaTime);
                    //					doMovimento = true;
                    /*
                    float raio = 0.5f;
                    if (Physics.SphereCast(cameraP.position, raio, alvo.position-cameraP.position, out raioColisor, 7,9))
                    {
                    if (raioColisor.transform.tag != "Player"
                       &&
                       raioColisor.transform.tag != "Criature"
                       &&
                       raioColisor.transform.tag != "desvieCamera"
                       )
                        {
                            Debug.Log(raioColisor.collider.gameObject.name);
                            cameraP.position = raioColisor.point + 0.1f*raio * raioColisor.normal;
                        }
                    }*/
                return true;
            }

        }

        return false;
    }

    /*
     static void VerificaBool(Transform alvo,float escala, ref DadosContraParede dados)
    {
        dados.foi = Physics.Linecast(alvo.position + escala * Vector3.up, dados.V, out dados.ray);
    }

    public static bool contraParedes(Transform cameraP, Transform alvo, float escalA, bool suave = false)
    {
        bool retorno = false;
        Camera cam = cameraP.GetComponent<Camera>();
        DadosContraParede dados = new DadosContraParede()
        {
            V = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)),
            ray = new RaycastHit()
        };

        VerificaBool(alvo, escalA, ref dados);

        Debug.DrawLine(dados.V,alvo.position + escalA * Vector3.up, Color.blue);

        Debug.DrawLine(
            Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)),
            /* cameraP.position,
    alvo.position + escalA* Vector3.up, Color.blue);

        if (!dados.foi)
        {
            dados.V = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
            VerificaBool(alvo, escalA, ref dados);
    Debug.DrawLine(dados.V, alvo.position + escalA* Vector3.up, Color.blue);
            if (!dados.foi)
            {
                dados.V = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
                VerificaBool(alvo, escalA, ref dados);
    Debug.DrawLine(dados.V, alvo.position + escalA* Vector3.up, Color.blue);
                if (!dados.foi)
                {
                    dados.V = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));
                    VerificaBool(alvo, escalA, ref dados);
    Debug.DrawLine(dados.V, alvo.position + escalA* Vector3.up, Color.blue);
}
            }
        }

        if (dados.foi)
        {
            Debug.DrawLine(dados.V, dados.ray.point, Color.red);

            if (dados.ray.transform.tag != "Player"
               &&
               dados.ray.transform.tag != "Criature"
               &&
               dados.ray.transform.tag != "desvieCamera"
               )
            {
                if (suave)
                {
                    cameraP.position = Vector3.Lerp(cameraP.position,
                        dados.ray.point + dados.ray.normal* 0.5f /*+ cameraP.forward * 0.2f, 25 * Time.deltaTime);
                }
                else
                    cameraP.position = //Vector3.Lerp(cameraP.position,
                        dados.ray.point + dados.ray.normal* 0.5f;//cameraP.forward * 0.2f;
                //           50*Time.deltaTime);
                //					doMovimento = true;

                retorno = true;
            }
        }

        return retorno;
    }
     */
}
