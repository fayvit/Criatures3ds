using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlledMoveForCharacter
{
    private MovimentacaoBasica mov;

    private GameObject oControlado;
    private NavMeshPath path;

    private int indiceDaDirecao = 0;

    public MovimentacaoBasica Mov
    {
        get { return mov; }
    }

    public ControlledMoveForCharacter(CaracteristicasDeMovimentacao carac, Transform T)
    {
        mov = new MovimentacaoBasica(carac, new ElementosDeMovimentacao(T));
        oControlado = T.gameObject;
    }

    /*
    public void InsereElementosDeControle(GameObject oControlado, Vector3 ondeChegar, float velocidadeAndando = 6)
    {
        this.oControlado = oControlado;
        if (!oControlado.GetComponent<CharacterController>())
        {
            CharacterController Cc = oControlado.AddComponent<CharacterController>();
            Cc.center = new Vector3(0, 1.05f, 0);
        }

        ModificarOndeChegar(ondeChegar,velocidadeAndando);
        
    }*/

    public void ModificarOndeChegar(Vector3 ondeChegar, float velocidade)
    {
        mov.VelocidadeAndando = velocidade;

        // if (mov.NoChao())
        {

            path = new NavMeshPath();
            NavMeshHit navHit = new NavMeshHit();
            NavMeshHit navHit2 = new NavMeshHit();

            if (NavMesh.SamplePosition(ondeChegar, out navHit, 10, 1)
                && NavMesh.SamplePosition(oControlado.transform.position, out navHit2, 10, 1))
            {
                //Debug.Log(
                NavMesh.CalculatePath(navHit2.position, navHit.position, 1, path);//);
            }

            //Debug.Log("cantos da Path: " + path.corners.Length);

            indiceDaDirecao = 1;
        }
    }

    public void TestepuloBoxOverlap(Vector3 pos)
    {

        Vector3 centerBox = oControlado.transform.position + oControlado.transform.forward * 1.1f * mov.Controle.radius + mov.Controle.center;
        Vector3 extendsBox = new Vector3(mov.Controle.radius, 0.5f * Mathf.Max(0.1f, mov.Controle.height - mov.Controle.radius, 0.2f));

        Collider[] C = Physics.OverlapBox(centerBox, extendsBox, Quaternion.LookRotation(oControlado.transform.forward));
        if (C.Length > 0)
        {
            for (int i = 0; i < C.Length; i++)
            {
                //                Debug.Log(C[i].gameObject.name);

                if (C[i].gameObject.layer == 9 && mov.NoChao(0.01f))
                    mov._Pulo.IniciaAplicaPulo();// oControlado.transform.position.y);
            }

        }
    }

    public void TestePuloRaycastOriginal(Vector3 pos)
    {
        Transform T = oControlado.transform;
        RaycastHit hit = new RaycastHit();
        Vector3 proj = Vector3.ProjectOnPlane((path.corners[indiceDaDirecao] - pos), Vector3.up);
        if (Vector3.Distance(T.forward, proj.normalized) < 0.5f)
            if (Physics.Raycast(pos, T.forward, out hit, 1))
                if (Vector3.Angle(hit.normal, Vector3.up) > 70
                     && Vector3.Angle(hit.normal, Vector3.up) < 110
                    && !mov._Pulo.EstouPulando)
                    mov._Pulo.IniciaAplicaPulo();
    }

    public bool UpdatePosition()
    {
        bool retorno = false;
        if (indiceDaDirecao < path.corners.Length)
        {
            Vector3 pos = oControlado.transform.position;
            mov.AplicadorDeMovimentos(Vector3.ProjectOnPlane(path.corners[indiceDaDirecao] - pos, Vector3.up).normalized);

            TestepuloBoxOverlap(pos);

            if (Vector3.Distance(path.corners[indiceDaDirecao], pos) < 1 && mov.NoChao(0.01f))
                indiceDaDirecao++;

        }
        else
        {
            mov.AplicadorDeMovimentos(Vector3.zero);
            AnimacaoPadrao();
            retorno = true;
        }
        return retorno;
    }


    public void AnimacaoPadrao()
    {
        mov.Animador.MeuAnimator.SetFloat("velocidade", 0);
        mov.AplicadorDeMovimentos(Vector3.zero);
        mov._Pulo.NaoEstouPulando();
        mov.Animador.MeuAnimator.SetBool("pulo", !mov.NoChao(0.01f));
    }

    public void Destruir()
    {
        MonoBehaviour.Destroy(oControlado.GetComponent<CharacterController>());
    }


}
