using UnityEngine;

[System.Serializable]
public class MovimentacaoBasica 
{
    //public delegate void Acoes();

    [SerializeField]private CaracteristicasDeMovimentacao caracMov;
    [SerializeField]private ElementosDeMovimentacao elementos;

    private Pulo pulo;
    private Vector3 direcaoMovimento = Vector3.zero;
    private bool gravidadeAplicavel = true;
    private bool noChao = false;
    private float velocidadeDescendo = 0;

    private float targetSpeed = 0;

    public bool GravidadeAplicavel
    {
        get { return gravidadeAplicavel; }

        set { gravidadeAplicavel = value; }
    }

    public CharacterController Controle
    {
        get { return elementos.controle; }
    }

    public Pulo _Pulo
    {
        get { return pulo; }
    }

    
    public AnimadorHumano Animador
    {
          get { return elementos.animador; }
    }

    public MovimentacaoBasica(CaracteristicasDeMovimentacao caracMov, ElementosDeMovimentacao elementos)
    {
        this.caracMov = caracMov;
        this.elementos = elementos;
        pulo = new Pulo(caracMov.caracPulo, elementos);
    }



    /*
    public Vector3 DirecaoAlvoControlada(EstadoDeCamera estadoC)
    {
        float h = Input.GetAxis("joy " + (int)elementos.gerente.controlador + " horizontal");
        float v = Input.GetAxis("joy " + (int)elementos.gerente.controlador + " vertical");

        Vector3 forward = new Vector3(1, 0, 0);
        if (elementos.cam.MinhaCamera)
            forward = elementos.cam.DirecaoInduzida(estadoC, h, v);

        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        return (h * right + v * forward);
    }
    */
    public bool NoChao(float distanciaFundamentadora)
    {
        if (Time.timeScale > 0)
            noChao = noChaoS(elementos.controle, distanciaFundamentadora);

        return noChao;
    }

    public static bool noChaoS(CharacterController controle, float erroDeAncora, bool especial = false)
    {

        if (especial)
        {
            return
                Physics.Raycast(controle.transform.position, Vector3.down, erroDeAncora);
        }
        else
        {

            //CharacterController controle =  GetComponent<CharacterController>();
            CollisionFlags collisionFlags = controle.collisionFlags;
            bool um = (collisionFlags & CollisionFlags.CollidedBelow) != 0;
            return um ||
            Physics.Raycast(controle.transform.position, Vector3.down, erroDeAncora);
        }
    }

    public void AplicadorDeMovimentos(
        Vector3 dir,
        float distanciaFundamentadora = 0.01f,
        Transform T = null,
        System.Action acaoNaMovimentacao = null
        )
    {
        if (elementos.transform == null && T != null)
        {
            elementos.transform = T;
            elementos.controle = T.GetComponent<CharacterController>();
        }

        if (NoChao(distanciaFundamentadora))
        {
            movimentacao(dir);
            if (acaoNaMovimentacao != null)
                acaoNaMovimentacao();
        }
        else if (caracMov.caracPulo.estouPulando)
        {
            _Pulo.VerificaPulo(dir);
        }
        else if (GravidadeAplicavel)
        {
            AplicaGravidade();
        }
    }

    public void movimentacao(Vector3 direcaoAlvo)
    {
        if (pulo == null)
            pulo = new Pulo(caracMov.caracPulo, elementos);

        pulo.NaoEstouPulando();
        targetSpeed = Mathf.Min(direcaoAlvo.magnitude, 1.0f);

        
        if (elementos.transform.tag=="Player")
        {
            targetSpeed *= (CommandReader.PressionadoBotao(2,(int)GameController.g.Manager.Control)) ?
                caracMov.velocidadeCorrendo :
                caracMov.velocidadeAndando;
        }
        else
            targetSpeed *= caracMov.velocidadeAndando;

        if (direcaoAlvo != Vector3.zero)
        {

            if (elementos.controle.velocity.magnitude < caracMov.velocidadeAndando /*&& VerificaChao.noChao(elementos.controle, elementos.transform)*/)
            {
                direcaoMovimento = direcaoAlvo.normalized;
            }
            else
            {
                direcaoMovimento = Vector3.RotateTowards(direcaoMovimento, direcaoAlvo, 500 * Mathf.Deg2Rad * Time.deltaTime, 1000);

                direcaoMovimento = direcaoMovimento.normalized;
            }
        }
        else
        {
            direcaoMovimento = Vector3.Lerp(direcaoMovimento, Vector3.zero, 1);
        }


        if (direcaoAlvo.magnitude > 0.3f)
            elementos.transform.rotation = Quaternion.LookRotation(new Vector3(direcaoMovimento.x, 0, direcaoMovimento.z));

        //Fundamentador();
        
        elementos.controle.Move((direcaoMovimento * targetSpeed + velocidadeDescendo * Vector3.down) * Time.deltaTime);
        elementos.animador.AnimaAndar(targetSpeed);

    }
    
    public void Fundamentador(bool comControle = false)
    {
        if (!noChaoS(elementos.controle,0.01f) && !caracMov.caracPulo.estouPulando)
            velocidadeDescendo = Mathf.Lerp(velocidadeDescendo, caracMov.caracPulo.velocidadeDescendo, 25 * Time.deltaTime);
        else
            velocidadeDescendo = Mathf.Lerp(velocidadeDescendo, 0, 25 * Time.deltaTime);

        
        if(comControle)
            elementos.controle.Move(velocidadeDescendo * Vector3.down * Time.deltaTime);
    }

    
    public void AplicaGravidade()
    {
        AplicaGravidade(9.8f, 5);
    }
    
    public void AplicaGravidade(float velMax, float amortecimento)
    {
        velocidadeDescendo = Mathf.Lerp(velocidadeDescendo, velMax, amortecimento * Time.deltaTime);
        elementos.controle.Move((direcaoMovimento * targetSpeed + velocidadeDescendo * Vector3.down) * Time.deltaTime);
    }
    
    public void VerificaComandoDePulo(float distanciaFundamentadora)
    {
        if (!_Pulo.EstouPulando && NoChao(distanciaFundamentadora))
        {
            pulo.IniciaAplicaPulo();
        }
    }

    public void AplicaPulo(Vector3 direcaoAlvo)
    {
        pulo.VerificaPulo(direcaoAlvo);
    }
    
}

[System.Serializable]
public class CaracteristicasDeMovimentacao : System.ICloneable
{
    public float velocidadeAndando = 6;
    public float velocidadeCorrendo = 12;
    public CaracteristicasDePulo caracPulo;

    public object Clone()
    {
        return new CaracteristicasDeMovimentacao()
        {
            velocidadeAndando = this.velocidadeAndando,
            velocidadeCorrendo = this.velocidadeCorrendo,
            caracPulo = this.caracPulo                       
        };
    }
}

[System.Serializable]
public class CaracteristicasDePulo
{
    public float alturaDoPulo = 2;
    public float tempoMaxPulo = 1;
    public float velocidadeSubindo = 3;
    public float velocidadeDescendo = 4.8f;
    public float velocidadeDuranteOPulo = 6;
    public float amortecimentoNaTransicaoDePulo = 0.05f;
    public float impulsoInicial = 0.3f;
    public bool estouPulando = false;
    public bool estavaPulando = false;
}

[System.Serializable]
public struct ElementosDeMovimentacao
{
    public CharacterController controle;
    public Transform transform;
    public AnimadorHumano animador;
    /*
    public GerenciadorDeEstadoDePersonagem gerente;
    
    public ICameraBase cam;
    */
    public ElementosDeMovimentacao(Transform transform,AnimadorHumano animador = null
                                    /*GerenciadorDeEstadoDePersonagem gerente,
                                    , ICameraBase cam*/)
    {
        this.controle = transform.GetComponent<CharacterController>();
        this.transform = transform;
        this.animador = animador;
        /*
        this.gerente = gerente;
        
        this.cam = cam;
        */
    }
}
