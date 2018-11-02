using UnityEngine;
using UnityEngine.SceneManagement;
using GameJolt.API;
using System.Collections;

public class LoginJoltManager : MonoBehaviour
{
    [SerializeField]private GameObject JoltManager;
    [SerializeField]private GameObject loadBar;
    [SerializeField]private GameObject buttonsPanel;

    private bool estouCarregando = false;
    // Use this for initialization
    void Start()
    {
#if UNITY_WEBGL
        if (!GameObject.Find("GameJoltAPI"))
            JoltManager.SetActive(true);
        buttonsPanel.SetActive(true);
#else
        //PlayerPrefs.DeleteAll();
        SaveAndLoadInJolt.Load();
        loadBar.SetActive(true);
#endif

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_WEBGL
        bool isSignedIn = Manager.Instance.CurrentUser != null;

        if (isSignedIn)
        {
            GameObject G = GameObject.Find("SignInPanel");
            if (G)
            {
                GameJolt.UI.Controllers.SignInWindow S = GameObject.Find("SignInPanel").GetComponent<GameJolt.UI.Controllers.SignInWindow>();
                if (S)
                    S.Dismiss(true);
            }


            buttonsPanel.SetActive(false);
            loadBar.SetActive(true);

            if (!estouCarregando)
            {
                AgendeODiferenteDeZero();
                estouCarregando = true;
            }

            if (SaveAndLoadInJolt.estaCarregado)
            {
#if !UNITY_EDITOR
                Sessions.Open(AbriuSessao);
                Sessions.Ping();
#endif
                SceneManager.LoadScene("Inicial");
            }


        }
        else
        {
            buttonsPanel.SetActive(true);
            loadBar.SetActive(false);
        }
#else
        if (SaveAndLoadInJolt.estaCarregado)
        {
            SceneManager.LoadScene("Inicial");
        }
#endif
    }

    void AbriuSessao(bool foi)
    {
        if (foi)
            Debug.Log("Sessão aberta com sucesso");
        else
            Debug.Log("Sessão falhou");
    }

    void OnLoad(bool X)
    {
        estouCarregando = true;
        if (X)
            AgendeODiferenteDeZero();
        else
            Debug.Log("Puta que o pariu");
    }

    void AgendeODiferenteDeZero()
    {

        Debug.Log(Manager.Instance.CurrentUser.ID);
        if (Manager.Instance.CurrentUser.ID != 0)
        {
            SaveAndLoadInJolt.estaCarregado = false;
            SaveAndLoadInJolt.Load();
        }
        else
            Invoke("AgendeODiferenteDeZero", 0.25f);
    }

    public void FazerLogin()
    {
        bool isSignedIn = Manager.Instance.CurrentUser != null;

        if (!isSignedIn)
            GameJolt.UI.Manager.Instance.ShowSignIn(OnLoad);
    }

    public void ContinuarSemLogin()
    {
        SaveDatesForJolt.s = new SaveDatesForJolt();
        SceneManager.LoadScene("Inicial");
    }
}
