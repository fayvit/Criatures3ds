using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[System.Serializable]
public class AtualizadorDeImpactoAereo
{
    private Vector3 dirDeslocamento;
    private Vector3 posInicial;
    private Transform alvoProcurado;
    private CharacterController controle;
    private NavMeshAgent nav;
    private impactoAoChao ao;

    private float tempoDecorrido = 0;
    private bool adview = false;
    private bool alcancouOApceDaAltura = false;
    private bool procurouAlvo = false;
    private bool estavaParada;
    

    public void ReiniciaAtualizadorDeImpactos(GameObject G)
    {
        if(nav==null)
            nav = G.GetComponent<NavMeshAgent>();

        estavaParada = nav.enabled;

        nav.enabled = false;

        posInicial = G.transform.position;

        if(controle == null)
            controle = G.GetComponent<CharacterController>();

        adview = false;
        tempoDecorrido = 0;
        alcancouOApceDaAltura = false;
        procurouAlvo = false;
    }

    public void ImpactoAtivo(GameObject G, IGolpeBase ativa, CaracteristicasDeImpactoComSalto caracteristica,float colocarColisor = 0)
    {
        tempoDecorrido += Time.deltaTime;

        if (!procurouAlvo)
        {
            alvoProcurado = CriaturesPerto.procureUmBomAlvo(G);
            procurouAlvo = true;
            // Debug.Log(alvoProcurado + "  esse é o alvo");
            AtualizadorDeImpactos.ajudaAtaque(alvoProcurado, G.transform);
            if (alvoProcurado != null)
                ativa.DirDeREpulsao = (Vector3.ProjectOnPlane(alvoProcurado.position - G.transform.position, Vector3.up)).normalized;

            AnimadorCriature.AnimaAtaque(G, ativa.Nome.ToString());

            /* aproveitado da geração 1 de scripts*/
            ao = G.AddComponent<impactoAoChao>();
            ao.aoChao = caracteristica.toque.ToString();
            /* ******************* */
        }

        if (!adview && tempoDecorrido>colocarColisor)
        {           
            
            ColisorDeGolpe.AdicionaOColisor(G, ativa, caracteristica.deImpacto, tempoDecorrido+ativa.TempoDeMoveMin);           

            adview = true;
        }

        if (caracteristica.final == ImpactoAereoFinal.MaisAltoQueOAlvo)
            MaisAltoQueOAlvo( G, ativa);
        else
            AvanceEPareAbaixo(G, ativa);
        

        if (tempoDecorrido > ativa.TempoDeMoveMax)
            nav.enabled = estavaParada;
    }

    private void MaisAltoQueOAlvo(GameObject G,IGolpeBase ativa)
    {

        Vector3 pontoAlvo = 3 * Vector3.up;

        if (alvoProcurado != null)
        {
            pontoAlvo += alvoProcurado.position + 3 * Vector3.up + (G.transform.position - alvoProcurado.position).normalized;
        }


        Vector3 direcaoDoSalto = alvoProcurado == null 
            ? G.transform.forward + 0.6f * Vector3.up 
            : Vector3.Dot(
                    Vector3.ProjectOnPlane(alvoProcurado.position - G.transform.position, Vector3.up),
                    ativa.DirDeREpulsao) > 0 ? pontoAlvo - G.transform.position:G.transform.forward;

        //Debug.Log(Vector3.ProjectOnPlane(pontoAlvo - G.transform.position, Vector3.up).magnitude);

        if ((Vector3.ProjectOnPlane(pontoAlvo- G.transform.position,Vector3.up).magnitude < 2f
            &&
            Vector3.ProjectOnPlane(posInicial - G.transform.position, Vector3.up).sqrMagnitude > 25f)
           ||
           Mathf.Abs(posInicial.y - G.transform.position.y) > 4
           )
        {
            alcancouOApceDaAltura = true;
            direcaoDoSalto = Vector3.ProjectOnPlane(direcaoDoSalto, Vector3.up);
        }

        

        if (Vector3.ProjectOnPlane(posInicial - G.transform.position, Vector3.up).sqrMagnitude > 25f/* distancia ao quadrado*/ &&
            (alcancouOApceDaAltura || tempoDecorrido > 0.75f*ativa.TempoDeMoveMax))
        {

            Vector3 descendo = Vector3.down;
            if (alvoProcurado != null)
            {

                if (Vector3.Dot(
                    Vector3.ProjectOnPlane(alvoProcurado.position - G.transform.position, Vector3.up),
                    ativa.DirDeREpulsao) < 0)
                {
                    descendo = descendo + ativa.DirDeREpulsao;
                }
                else
                {
                    float vel = 0.75f;
                    if (G.name == "CriatureAtivo")
                        vel = 0.1f;

                    descendo = descendo + vel* (alvoProcurado.position - G.transform.position);
                }
                descendo.Normalize();
                
                G.transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(descendo, Vector3.up));
            }
            //Debug.DrawRay(G.transform.position, descendo * ativa.VelocidadeDeGolpe, Color.green, 10);
            controle.Move(descendo * ativa.VelocidadeDeGolpe * Time.deltaTime);
        }
        else
        {

            //Debug.DrawRay(G.transform.position, direcaoDoSalto.normalized * ativa.VelocidadeDeGolpe, Color.yellow, 10);
            controle.Move(direcaoDoSalto.normalized * Time.deltaTime * ativa.VelocidadeDeGolpe);
        }


    }


    void AvanceEPareAbaixo(GameObject G, IGolpeBase ativa)
    {
        float distanciaDeParada = 5.75f;
        float velocidadeAvante = ativa.VelocidadeDeGolpe;

        Vector3 V = posInicial+10*ativa.DirDeREpulsao;
        Vector3 project;
        if (alvoProcurado)
        {
            V = alvoProcurado.position;
           // project = (Vector3.ProjectOnPlane(G.transform.position - V, Vector3.up));
          //  if (Vector3.Dot(project,G.transform.forward)>0)
            //    AtualizadorDeImpactos.ajudaAtaque(alvo, G.transform);
        }

        project = (Vector3.ProjectOnPlane(G.transform.position - V, Vector3.up));

        if (Vector3.Dot(project, G.transform.forward) > 0)
            G.transform.rotation = Quaternion.LookRotation(-project);
        else
            G.transform.rotation = Quaternion.LookRotation(ativa.DirDeREpulsao);


        if ((project.magnitude > distanciaDeParada
            &&
            G.transform.position.y - posInicial.y < 4
            &&
            !alcancouOApceDaAltura)
           &&
           tempoDecorrido < ativa.TempoDeMoveMax/2)
        {
            dirDeslocamento = (velocidadeAvante * G.transform.forward + Vector3.up * 15);
        }
        else if (project.magnitude <= distanciaDeParada)
        {
            Vector3 foco = velocidadeAvante * G.transform.forward + Vector3.down * 9.8f;
            if (alvoProcurado && Vector3.Dot(-project, G.transform.forward) > 0)
                foco = velocidadeAvante * (alvoProcurado.position - G.transform.position).normalized+18.8f*Vector3.down;

            dirDeslocamento = Vector3.Lerp(dirDeslocamento, foco, 20 * Time.deltaTime);
            alcancouOApceDaAltura = true;
        }
        else if (G.transform.position.y - posInicial.y > 4)
        {
            alcancouOApceDaAltura = true;
        }
        else
        {
                dirDeslocamento = Vector3.Lerp(dirDeslocamento, velocidadeAvante * G.transform.forward + Vector3.down * 18.8f, 20 * Time.deltaTime);
        }

        controle.Move(dirDeslocamento * Time.deltaTime);
    }

    public void ReligarNavMesh()
    {
        if (nav)
            nav.enabled = estavaParada;
    }

    public void DestruirAo()
    {
        if (ao)
            MonoBehaviour.Destroy(ao,0.25f);
    }
}

public enum ImpactoAereoFinal
{
    AvanceEPareAbaixo,
    MaisAltoQueOAlvo
}
