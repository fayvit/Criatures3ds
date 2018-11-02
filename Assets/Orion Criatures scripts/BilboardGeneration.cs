using UnityEngine;
using System.Collections;

public class BilboardGeneration
{

    /*
Make a billboard out of an object in the scene
The camera will auto-place to get the best view of the object so no need for camera adjustment

To use - place an object in an empty scene with just camera and any lighting you want.
Add this script to your scene camera and link to the object you want to render.
Press play and you will get a snapshot of the object (looking down the +Z-axis at it) saved out to billboard.png in your project folder
Any pixels colored the same as the camera background color will be transparent
*/

    GameObject G;
    Camera cam;
    //int guardaLayer = -1;

    public GameObject objectToRender;
    public int imageWidth = 128;
    public int imageHeight = 128;
    //**bool to only conver once
    private MyBillboard my;
    private int tanto = 0;
    private Vector3[] visoes;
    private string nomeEscolhido;

    public void GetBillboardArray(MyBillboard my,GameObject objectToRender)
    {
        string nome = objectToRender.GetComponent<MeshFilter>().mesh.name;
        if (MatrizDeBillboards.m.ContainsKey(nome))
        {
            my.Tex = MatrizDeBillboards.m[nome];
            Debug.Log(nome);
        }
        else
        {
            MatrizDeBillboards.m[nome] = my.Tex;
            maskEverthings = Camera.main.cullingMask;
            Awake();
            this.objectToRender = objectToRender;
            CameraSetup();
            this.my = my;
            GameController.g.StartCoroutine(ConvertToImage());
        }
    }

    void Awake()
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

    public void Start()
    {
        
        //this.objectToRender = objectToRender
        CameraSetup();
        //quad = G;
        //GameController.g.StartCoroutine(ConvertToImage());
    }

    //**split into two methods
    int layerDoObjeto;
    int maskEverthings = 0;

    void CameraSetup()
    {
        //grab the main camera and mess with it for rendering the object - make sure orthographic
        cam = new GameObject().AddComponent<Camera>();
        
        layerDoObjeto = objectToRender.layer;
        objectToRender.layer = 10;
        
        Camera[] cams = GameObject.FindObjectsOfType<Camera>();

        foreach(Camera cam2 in cams)
        {
            cam2.orthographic = true;
            cam2.cullingMask = 1024;
            cam2.clearFlags = CameraClearFlags.SolidColor;
            //render to screen rect area equal to out image size
            float rw = imageWidth;
            rw /= Screen.width;
            float rh = imageHeight;
            rh /= Screen.height;
            cam2.rect = new Rect(0, 0, rw, rh);
            //**manually set the background color
            cam2.backgroundColor = new Vector4(0, 0, 0, 0);

            //grab size of object to render - place/size camera to fit
            Bounds bb = objectToRender.GetComponent<Renderer>().bounds;

            //place camera looking at centre of object - and backwards down the z-axis from it

            Debug.Log(cam2 + " :" + bb + " : " + visoes);
            cam2.transform.position = bb.center - 1.3f * (bb.extents.z + bb.extents.x) * visoes[tanto];
            cam2.transform.LookAt(objectToRender.transform);
            cam2.transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(cam2.transform.forward, Vector3.up));
            cam2.transform.position.Set(cam2.transform.position.x, cam2.transform.position.y, -1.0f + (bb.min.z * 2.0f));
            //make clip planes fairly optimal and enclose whole mesh
            cam2.nearClipPlane = 0.5f;
            cam2.farClipPlane = Mathf.Abs(cam2.transform.position.z) + 10.0f + bb.max.z;
            //set camera size to just cover entire mesh
            cam2.orthographicSize = 1.01f * Mathf.Max((bb.max.y - bb.min.y) / 2.0f, (bb.max.x - bb.min.x) / 2.0f);
            cam2.transform.position.Set(cam2.transform.position.x, cam2.orthographicSize * 0.05f, cam2.transform.position.y);
        }
    }

    IEnumerator ConvertToImage()
    {
        yield return new WaitForEndOfFrame();
        var tex = new Texture2D(imageWidth, imageHeight);
        // Read screen contents into the texture
        RenderTexture.active = new RenderTexture(128,128,1);
        cam.Render();
        //RenderTexture.active = cam. activeTexture;
        tex.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        tex.Apply();

        //turn all pixels == background-color to transparent
        //Camera cam = Camera.main;
        Color bCol = cam.backgroundColor;
        Color alpha = new Vector4(0, 0, 0, 0);
        alpha.a = 0.0f;
        for (int y = 0; y < imageHeight; y++)
        {
            for (int x = 0; x < imageWidth; x++)
            {
                Color c = tex.GetPixel(x, y);
                //**check for difference
                if (c.r != bCol.r || c.g != bCol.g || c.b != bCol.b)
                    tex.SetPixel(x, y, new Vector4(c.r, c.g, c.b, 1));
            }
        }
        tex.Apply();

        // Encode texture into PNG
        //byte[] bytes = tex.EncodeToPNG();

        my.Tex[tanto] = tex;

        //**path is in Assets, you must refresh Unity to see the file (can be done by clicking away then click on Unity window again, etc)
       // System.IO.File.WriteAllBytes(Application.dataPath + "/" + nomeEscolhido + "_" + tanto + ".png", bytes);
        //Debug.Log(Application.dataPath + "/" + nomeEscolhido + ".png");

        tanto++;
        if (tanto < 8)
        {
            MonoBehaviour.Destroy(cam.gameObject);
            GameController.g.StartCoroutine(ConvertToImage());
            CameraSetup();
            
        }
        else
        {
            objectToRender.layer = layerDoObjeto;
            ResetCam();
            AplicadorDeCamera.cam.enabled = true;
            MonoBehaviour.Destroy(cam.gameObject);
        }

        
        Debug.Log(tanto);
    }

    void ResetCam()
    {
            //grab the main camera and mess with it for rendering the object - make sure orthographic
            Camera cam = Camera.main;
            cam.orthographic = false;
            cam.cullingMask = maskEverthings;
        
            //render to screen rect area equal to out image size
            
            cam.rect = new Rect(0, 0, 1, 1);
        //**manually set the background color
            cam.clearFlags = CameraClearFlags.Skybox;

            cam.nearClipPlane = 0.3f;
            cam.farClipPlane = 1000;
            //set camera size to just cover entire mesh
            //cam.orthographicSize = 1.01f * Mathf.Max((bb.max.y - bb.min.y) / 2.0f, (bb.max.x - bb.min.x) / 2.0f);
            
        
    }
}

   