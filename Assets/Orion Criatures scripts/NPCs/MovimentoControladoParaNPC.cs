using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MovimentoControladoParaNPC
{
    private GameObject oControlado;
    private MovimentacaoBasica mov;
    private AnimadorHumano anima;
    private NavMeshPath path;

    private int indiceDaDirecao = 0;

    public MovimentacaoBasica Mov
    {
        get { return mov; }
    }

    public void InsereElementosDeControle(GameObject oControlado,Transform ondeChegar,float velocidadeAndando = 6)
    {
        this.oControlado = oControlado;
        if (!oControlado.GetComponent<CharacterController>())
        {
            CharacterController Cc = oControlado.AddComponent<CharacterController>();
            Cc.center = new Vector3(0, 1.05f, 0);
        }

        anima = new AnimadorHumano(oControlado.GetComponent<Animator>());
        mov = new MovimentacaoBasica(
            new CaracteristicasDeMovimentacao() {
                velocidadeAndando = velocidadeAndando,
                caracPulo = new CaracteristicasDePulo()
            },
            new ElementosDeMovimentacao(oControlado.transform,anima)
            );

        path = new NavMeshPath();
        NavMeshHit navHit = new NavMeshHit();

        if(NavMesh.SamplePosition(ondeChegar.position,out navHit,10,1))
        Debug.Log(
        NavMesh.CalculatePath(oControlado.transform.position, navHit.position, 1, path));

        Debug.Log("cantos da Path: "+path.corners.Length);

        indiceDaDirecao = 1;
    }

    public bool UpdatePosition()
    {
        bool retorno = false;

        if (indiceDaDirecao < path.corners.Length)
        {
            Vector3 pos = oControlado.transform.position;
            mov.AplicadorDeMovimentos((path.corners[indiceDaDirecao] - pos).normalized);
            RaycastHit hit = new RaycastHit();
            Vector3 proj = Vector3.ProjectOnPlane((path.corners[indiceDaDirecao] - pos),Vector3.up);
            if(Vector3.Distance(oControlado.transform.forward, proj.normalized)<0.5f)
            if (Physics.Raycast(pos, oControlado.transform.forward, out hit, 1))
            {
                if (Vector3.Angle(hit.normal, Vector3.up) > 70 
                    && Vector3.Angle(hit.normal, Vector3.up) < 110 
                    && !mov._Pulo.EstouPulando)
                    mov._Pulo.IniciaAplicaPulo();
            }

            if (Vector3.Distance(path.corners[indiceDaDirecao], pos) < 1)
                indiceDaDirecao++;
        }
        else
        {
            mov.Animador.PararAnimacao();
            mov.AplicadorDeMovimentos(Vector3.zero);
            mov._Pulo.NaoEstouPulando();
            retorno = true;
        }
        return retorno;
    }

    public void Destruir()
    {
        MonoBehaviour. Destroy(oControlado.GetComponent<CharacterController>());
    }


}
