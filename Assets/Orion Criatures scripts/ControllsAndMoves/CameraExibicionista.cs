using UnityEngine;
using System.Collections;

[System.Serializable]
public class CameraExibicionista
{
    [SerializeField]private Transform transform;
    [SerializeField]private Transform foco;
    [SerializeField]private float alturaDoPersonagem;

    private Transform baseDeMovimento;
    private float contadorDeTempo = 0;
    private bool contraParedes = false;

    public void OnDestroy()
    {
        if(baseDeMovimento!=null)
            MonoBehaviour.Destroy(baseDeMovimento.gameObject);
    }

    public void ZerarContadorDeTempo()
    {
        contadorDeTempo = 0;
    }

    public CameraExibicionista(Transform daCamera,Transform doFoco,float altura,bool contraParedes = false)
    {
        this.contraParedes = contraParedes;
        ZerarContadorDeTempo();
        transform = daCamera;
        foco = doFoco;
        alturaDoPersonagem = altura;
        baseDeMovimento = (new GameObject()).transform;
        baseDeMovimento.position = daCamera.position;
        baseDeMovimento.name = "baseDeMovimentoExibicionista";
    }
    public void MostrandoUmCriature()
    {
        baseDeMovimento.RotateAround(foco.position, Vector3.up, 15 * Time.deltaTime);
        transform.RotateAround(foco.position, Vector3.up, 15 * Time.deltaTime);
        baseDeMovimento.position = Vector3.Lerp(baseDeMovimento.position, foco.position
            + 8 * (Vector3.ProjectOnPlane(baseDeMovimento.position-foco.position,Vector3.up).normalized)
            + (5 + alturaDoPersonagem) * Vector3.up,2*Time.deltaTime);

        baseDeMovimento.LookAt(foco);

        if (CameraDeLuta.contraParedes(baseDeMovimento, foco, alturaDoPersonagem, true))
        {
            CameraDeLuta.contraParedes(transform, foco, alturaDoPersonagem, true);
        } else
        {
            transform.position = baseDeMovimento.position;
            transform.rotation = baseDeMovimento.rotation;
        }
    }

    public bool MostrarFixa(float velocidadeTempoDeFoco, float distancia = 6, float altura = -1, bool tempo = false,
        Vector3 posInicialDMovimento = default(Vector3),bool focoDoTransform = false,Vector3 deslFocoCamera = default(Vector3)
        )
    {
        //Debug.Log(foco);
        if (altura < 0)
            altura = alturaDoPersonagem;

        float lerp = 0;
        if (!tempo)
            lerp = velocidadeTempoDeFoco * Time.deltaTime;
        else
        {
            contadorDeTempo += Time.deltaTime;
            lerp = contadorDeTempo / velocidadeTempoDeFoco;
            
        }
        if (posInicialDMovimento == default(Vector3))
            posInicialDMovimento = transform.position;

        Vector3 posAlvo = foco.position + foco.forward * distancia + Vector3.up * altura;
        Vector3 dirAlvo = (focoDoTransform)?-foco.forward:foco.position - (transform.position+deslFocoCamera);
        dirAlvo.Normalize();
        if (transform)
        {
            
            transform.position = Vector3.Lerp(posInicialDMovimento, posAlvo, lerp);
            transform.rotation = Quaternion.LookRotation(
                Vector3.Lerp(transform.forward, dirAlvo, lerp)
                );
            
            if(contraParedes)
                CameraDeLuta.contraParedes(transform, foco, altura, true);

            if (!tempo && Vector3.Distance(transform.position, posAlvo) < 0.5f && Vector3.Distance(transform.forward, dirAlvo) < 0.5f)
            {
                transform.position = posAlvo;
                transform.rotation = Quaternion.LookRotation(foco.position - transform.position);
                return true;
            }
            else if (tempo && Vector3.Distance(transform.position, posAlvo) < 0.1f && Vector3.Distance(transform.forward, dirAlvo) < 0.1f)
                return true;
        }
        else
            Debug.LogError("A camera não foi setada corretamente");

        return false;
    }
}