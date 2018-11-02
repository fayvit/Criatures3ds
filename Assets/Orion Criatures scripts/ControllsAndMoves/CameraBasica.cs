using UnityEngine;
using System.Collections;

[System.Serializable]
public class CameraBasica{

    [SerializeField]private Transform alvo;
    [SerializeField]private float altura = 20;
    [SerializeField]private float distanciaHorizontal=20;
    [SerializeField]private float velocidadeDeCamera = 10;
    [SerializeField]private float distanciaAFrenteParaFoco = 2;

    private Transform transform;
    private Vector3 dirAlvo;
    private bool contraParedes = false;
    private bool dirDeObj = false;
    private float velDeLerp = 1;
    

	// Use this for initialization
	public void Start (Transform T) {
        transform = T;
        if (!alvo)
        {
            GameObject doAlvo = GameObject.FindGameObjectWithTag("Player");
            if (doAlvo)
                alvo = doAlvo.transform;
        }

        dirAlvo = alvo.position-distanciaHorizontal*Vector3.forward+altura*Vector3.up;
        transform.position = dirAlvo;
            transform.LookAt(alvo.position+distanciaAFrenteParaFoco*Vector3.forward);
	}

    // Update is called once per frame
    public void Update()
    {
        Vector3 dirCamera = Vector3.forward;
        if (dirDeObj)
            dirCamera = -this.alvo.forward;


        dirAlvo = alvo.position - distanciaHorizontal * dirCamera + altura * Vector3.up;

        velDeLerp = velocidadeDeCamera * Mathf.Max(1,
            Vector3.Distance(dirAlvo, transform.position) / Mathf.Sqrt(Mathf.Pow(altura, 2) + Mathf.Pow(distanciaHorizontal, 2)
            ));

        transform.position = Vector3.Lerp(transform.position,
            dirAlvo
            , velDeLerp * Time.deltaTime);

        if (contraParedes)
            CameraDeLuta.contraParedes(transform, alvo, 1);
    }

    public void NovoFoco(Transform alvo, float altura, float distancia, bool contraParedes,bool forwardDoObj)
    {
        this.altura = altura;
        this.distanciaHorizontal = distancia;
        this.alvo = alvo;
        this.contraParedes = contraParedes;
        this.dirDeObj = forwardDoObj;

        if (transform == null)
            transform = Camera.main.transform;

        Vector3 dirCamera = Vector3.forward;

        if (forwardDoObj)
            dirCamera = -this.alvo.forward;

        transform.rotation = Quaternion.LookRotation(this.alvo.position + distanciaAFrenteParaFoco *dirCamera-
            (alvo.position - distanciaHorizontal * dirCamera + altura * Vector3.up));

        Update();
    }
}
