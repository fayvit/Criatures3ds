using UnityEngine;
using System.Collections;

public class MeuBillboard : MonoBehaviour
{
    [SerializeField] private int billboardDistance = 100;
    [SerializeField] private Texture2D[] images = new Texture2D[8];

    private Camera cam;
    private Vector3[] visoes;
    private MeshRenderer MR_quad;
    private MeshRenderer MR_object;
    private bool estaMaior = false;

    protected Texture2D[] Images
    {
        get { return images; }
        set { images = value; }
    }

    private void Awake()
    {
        visoes = new Vector3[8] {
            Vector3.forward,
            (Vector3.forward+Vector3.right).normalized,
            Vector3.right,
            (Vector3.right-Vector3.forward).normalized,
            -Vector3.forward,
            -(Vector3.forward+Vector3.right).normalized,
            -Vector3.right,
            Vector3.forward-Vector3.right
        };
    }

    protected void Start()
    {
        cam = Camera.main;
        MR_object = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (cam)
        {
            if (Vector3.Distance(cam.transform.position, transform.position) > billboardDistance)
            {
                if (!estaMaior)
                {
                    estaMaior = true;
                    VerificaInsereQuad();
                    MR_object.enabled = false;

                }

                if (MR_quad != null)
                {
                    for (int i = 0; i < visoes.Length; i++)
                    {
                        if (Vector3.Angle(transform.position - cam.transform.position, visoes[i]) < 23)
                            MR_quad.material.mainTexture = Images[i];
                    }


                    MR_quad.transform.rotation = Quaternion.LookRotation(
                        Vector3.ProjectOnPlane(
                            MR_quad.transform.position - cam.transform.position,
                            Vector3.up
                            )
                        );
                }
            }
            else if (Vector3.Distance(cam.transform.position, transform.position) < billboardDistance)
            {

                if (estaMaior && MR_quad != null)
                {
                    MR_quad.gameObject.SetActive(false);
                    MR_object.enabled = true;
                    estaMaior = false;
                }
            }
        }
        else
            cam = Camera.main;
    }

    void VerificaInsereQuad()
    {
        if (MR_quad == null)
        {
            Bounds bb = GetComponent<Renderer>().bounds;

            GameObject G = GameObject.CreatePrimitive(PrimitiveType.Quad);
            G.transform.position = bb.center;
            G.transform.rotation = Quaternion.LookRotation(cam.transform.forward);
            G.transform.localScale = new Vector3(2.5f * bb.extents.x, 2 * bb.extents.y, 1);

            MR_quad = G.GetComponent<MeshRenderer>();
            for (int i = 0; i < visoes.Length; i++)
            {
                if (Vector3.Angle(cam.transform.forward, visoes[i]) < 23)
                    MR_quad.material = new Material(GameController.g.El.materiais[2]) { mainTexture = Images[i] };
            }

            
            MR_quad.transform.SetParent(transform);
        }
        else
            MR_quad.gameObject.SetActive(true);

    }

    /*
    // Use this for initialization
    void Start()
    {
        SetarCameraPrincipal();
    }

    void SetarCameraPrincipal()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(cam.transform.position, transform.position) > billboardDistance)
        {
            if (!estaMaior)
            {
                Debug.Log("maiorou");
                estaMaior = true;
               new BilboardGeneration().Start(pai, out quad);
                
            }
            if (quad != null)
            {
                quad.transform.rotation = Quaternion.LookRotation(
                    Vector3.ProjectOnPlane(Camera.main.transform.position - quad.transform.position, Vector3.up)
                    );
            }
        }
        else if (Vector3.Distance(cam.transform.position, transform.position) < billboardDistance && estaMaior)
        {
            Debug.Log("minorou");
            pai.SetActive(true);
            if (quad != null)
            {
                MeshRenderer MR = quad.GetComponent<MeshRenderer>();
                Destroy(MR.material.mainTexture);
                Destroy(MR.material);
                Destroy(quad);
            }
            estaMaior = false;
        }
    }
    */
}
