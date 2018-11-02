using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PropsPorScript : MonoBehaviour
{
    public Material materialAlvo;
    public Material[] materiaisConfirmadores;
    public GameObject planoTransparencia;
    [SerializeField]private PropsRegion[] props;
    public bool vai;

    [System.Serializable]
    private class PropsRegion
    {
        [SerializeField]private Rect regiao;
        [Range(0,1)]public float taxaDeInsercaoDeProps;
        public PropsDeCenario[] propsCompativelComBaixo;
        public PropsDeCenario[] propsNaoCompativelComBaixo;
        public PropsDeCenario[] propsDeCanto;

        public Rect Regiao
        {
            get { return regiao; }
            set { regiao = value; }
        }
    }

    [System.Serializable]
    public class PropsDeCenario
    {
        public GameObject prop;
        public float taxa;
    }

    public void Insere()
    {
        vai = true;
    }

    // Use this for initialization
    void Start()
    {
        if (props.Length > 1)
        {
            for (int i = 1; i < props.Length; i++)
            {
                if (props[i].propsCompativelComBaixo.Length == 0)
                {
                    props[i].propsCompativelComBaixo = props[0].propsCompativelComBaixo;
                }

                if (props[i].propsNaoCompativelComBaixo.Length == 0)
                {
                    props[i].propsNaoCompativelComBaixo = props[0].propsNaoCompativelComBaixo;
                }

                if (props[i].propsDeCanto.Length == 0)
                {
                    props[i].propsDeCanto = props[0].propsDeCanto;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (vai)
        {

            Debug.Log("fase 1");

            GameObject[] Gs = GameObject.FindGameObjectsWithTag("cenario");
            
            VerifiqueProps(OrganizaGOs(Gs));
            vai = false;
        }
    }

    GameObject[,] OrganizaGOs(GameObject[] Gs)
    {

        Vector3 T = Gs[0].transform.position;
        int xMax = (int)T.x;
        int xMin = (int)T.x;
        int zMax = (int)T.z;
        int zMin = (int)T.z;
        
        for (int i = 0; i < Gs.Length; i++)
        {
            T = Gs[i].transform.position;

            if (T.x > xMax)
                xMax = (int)T.x;
            if (T.x < xMin)
                xMin = (int)T.x;

            if (T.z > zMax)
                zMax = (int)T.z;

            if (T.z < zMin)
                zMin = (int)T.z;
        }

        GameObject[,] meusGs= new GameObject[(xMax-xMin)/10+1,(zMax-zMin)/10+1];
        Debug.Log("o tamanho é " + meusGs.GetLength(0) + " : " + meusGs.GetLength(1));
        
        for (int i = 0; i < Gs.Length; i++)
        {
            if (Gs[i].transform.localScale==new Vector3(10,10,10))
            {
                T = Gs[i].transform.position;
                Debug.Log((((int)T.x - xMin) / 10) + " :" +( ((int)T.z - zMin) / 10));
                meusGs[((int)T.x - xMin) / 10, ((int)T.z - zMin) / 10] = Gs[i];
            }
        }

        return meusGs;
    }

    void VerifiqueProps(GameObject[,] Gs)
    {
        Debug.Log(Gs.GetLength(0)+" : "+Gs.GetLength(1));
        for (int i = 0; i < Gs.GetLength(0); i++)
            for (int j = 0; j < Gs.GetLength(1); j++)
            {
                
                if (Gs[i, j] != null)
                {
                    
                    if (Gs[i, j].GetComponent<MeshRenderer>().material.mainTexture == materialAlvo.mainTexture)
                    {
                        
                        VerificaInsereProps(i, j, Gs);
                    }
                }
            }
    }

    void VerificaInsereProps(int i,int j,GameObject[,] Gs)
    {
        bool temp = false;
        bool vai = true;
        MeshRenderer MR;
        if (j + 1 < Gs.GetLength(1))
        {
            if (Gs[i, j + 1] != null)
            {
                MR = Gs[i, j + 1].GetComponent<MeshRenderer>();

                temp = VerificaMateriaisConfirmadores(MR.material);                

                if (temp)
                {
                    InstanciaTransparenciaSomadoCom(Vector3.forward,Gs[i,j]);
                    VerificaInstanciaProps(Gs[i, j],true);
                    vai = false;
                }
            }

            temp |= VerificaInsereTransparencia(i, j, 1, 0, Gs);
            temp |= VerificaInsereTransparencia(i, j, -1, 0, Gs);
            temp |= VerificaInsereTransparencia(i, j, 0, -1, Gs);

            vai &= temp;

            if (vai)
                VerificaInstanciaProps(Gs[i,j]);

            
            VerificaEscondeCantos(i, j,1,1, Gs);
            VerificaEscondeCantos(i, j, -1, 1, Gs);
            VerificaEscondeCantos(i, j, 1, -1, Gs);
            VerificaEscondeCantos(i, j, -1, -1, Gs);
            
        }
    }

    void VerificaEscondeCantos(int i, int j, int somaI,int somaJ,GameObject[,] Gs)
    {
        if (i + somaI > 0 && i + somaI < Gs.GetLength(0) && j + somaJ > 0 && j + somaJ < Gs.GetLength(1))
            if (Gs[i + somaI, j] != null && Gs[i + somaI, j + somaJ] != null && Gs[i, j + somaJ] != null)
                if (VerificaMateriaisConfirmadores(Gs[i + somaI, j].GetComponent<MeshRenderer>().material)
                    &&
                    VerificaMateriaisConfirmadores(Gs[i + somaI, j + somaJ].GetComponent<MeshRenderer>().material)
                    &&
                    VerificaMateriaisConfirmadores(Gs[i, j + somaJ].GetComponent<MeshRenderer>().material)
                    )
                {
                    Instantiate(SorteiaPropsDeCanto(Gs[i, j]), 
                        Gs[i, j].transform.position + 5 * somaI * Vector3.right + 5 * somaJ * Vector3.forward+5*Vector3.up, 
                        Quaternion.identity);
                }
    }

    GameObject SorteiaPropsDeCanto(GameObject G)
    {
        GameObject retorno = null;
        float sum = 0;
        int indiceDeProps = VerificaPropsRegion(G);
        PropsDeCenario[] props = this.props[indiceDeProps>-1?indiceDeProps:0].propsDeCanto;
        for (int i = 0; i < props.Length; i++)
        {
            sum += props[i].taxa;
        }

        float sorteado = Random.Range(0, sum);

        sum = 0;
        for (int i = 0; i < props.Length; i++)
        {

            sum += props[i].taxa;


            if (sorteado <= sum && retorno == null)
                retorno = props[i].prop;
        }

        return retorno;
    }

    int VerificaPropsRegion(GameObject G)
    {
        int qual = -1;
        for (int i = 0; i < props.Length; i++)
        {
            if (G.transform.position.x < props[i].Regiao.width
                &&
                G.transform.position.x > props[i].Regiao.x
                &&
                G.transform.position.z < props[i].Regiao.height
                &&
                G.transform.position.z > props[i].Regiao.y
                )
            {

                qual = i;
            }
        }
        return qual;
    }

    void VerificaInstanciaProps(GameObject G,bool compativelComBaixo = false)
    {

        int qual = VerificaPropsRegion(G);
        if (qual > -1)
        {
            bool TeraProps = (Random.Range(0,1f)< props[qual].taxaDeInsercaoDeProps);
            if (TeraProps)
            {
                GameObject GG = SorteadorDeProps(compativelComBaixo,props[qual]);
                Instantiate(GG,G.transform.position+5*Vector3.up,GG.transform.rotation);
            }
        }
    }

    GameObject SorteadorDeProps(bool compativelComBaixo,PropsRegion props)
    {
        GameObject retorno = null;
        float sum = 0;
        for (int i = 0; i < props.propsCompativelComBaixo.Length; i++)
        {
            sum += props.propsCompativelComBaixo[i].taxa;
        }

        if(!compativelComBaixo)
            for (int i = 0; i < props.propsNaoCompativelComBaixo.Length; i++)
            {
                sum += props.propsNaoCompativelComBaixo[i].taxa;
            }

        float sorteado = Random.Range(0, sum);
        
        sum = 0;
        for (int i = 0; i < props.propsCompativelComBaixo.Length; i++)
        {

            sum += props.propsCompativelComBaixo[i].taxa;


            if (sorteado <= sum && retorno == null)
                retorno = props.propsCompativelComBaixo[i].prop;
        }

        if (retorno == null)
        {
            for (int i = 0; i < props.propsNaoCompativelComBaixo.Length; i++)
            {

                sum += props.propsNaoCompativelComBaixo[i].taxa;


                if (sorteado <= sum && retorno == null)
                    retorno = props.propsNaoCompativelComBaixo[i].prop;
            }
        }

        return retorno;
    }

    void SorteiaProps(GameObject G,GameObject prop)
    {
        Instantiate(prop, G.transform.position + 5*Vector3.up, Quaternion.identity);
    }

    bool VerificaInsereTransparencia(int i, int j, int somaI, int somaJ,GameObject[,] Gs)
    {
        if (Gs.GetLength(0) > i + somaI && Gs.GetLength(1) > j + somaJ && i + somaI > -1 && j + somaJ > -1)
            return VerificaInsereTransparencia(Gs[i + somaI, j + somaJ], Gs[i, j], somaI * Vector3.right + somaJ * Vector3.forward);
        else return false;
    }

    bool VerificaInsereTransparencia(GameObject verifica,GameObject centro,Vector3 deslocador)
    {
        
        MeshRenderer MR;
        bool temp = false;
        if (verifica != null)
        {
            
            MR = verifica.GetComponent<MeshRenderer>();
            temp = VerificaMateriaisConfirmadores(MR.material);
            if (temp)
                InstanciaTransparenciaSomadoCom(deslocador, centro);
        }

        return temp;
    }

    void InstanciaTransparenciaSomadoCom(Vector3 somado,GameObject G)
    {
        Instantiate(planoTransparencia, G.transform.position + 2.2f*somado+5.1f*Vector3.up, Quaternion.LookRotation(somado));
    }

    bool VerificaMateriaisConfirmadores(Material M)
    {
        for (int m = 0; m < materiaisConfirmadores.Length; m++)
        {
            if (M.mainTexture == materiaisConfirmadores[m].mainTexture)
                return true;
        }

        return false;
    }
}
