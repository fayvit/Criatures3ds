using UnityEngine;
using System.Collections;

public class PegaCristal : MonoBehaviour
{
    [SerializeField] private int valor = 1;
    [SerializeField] private string ID ="0";

    // Use this for initialization
    void Start()
    {
        if (Application.isPlaying)
        {
            if (ExistenciaDoController.AgendaExiste(Start, this))
            {
                

                if (GameController.g.MyKeys.VerificaAutoShift(ID))
                    Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        Event e = Event.current;
        bool foi = false;
        if (e != null)
        {
            foi = e.commandName == "Duplicate" || e.commandName == "Paste";
            Debug.Log(e.commandName);
        }

        if ((ID == "0" ||foi) && gameObject.scene.name != null)
        {
           // ID = BuscadorDeID.GetUniqueID(gameObject, ID.ToString());
            
            ID = GetInstanceID()+"_"+gameObject.scene.name+"_Cristalx";
            //ID = System.Guid.NewGuid().ToString();
            BuscadorDeID.SetUniqueIdProperty(this, ID, "ID");
        }
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Application.isPlaying)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag=="Criature")
            {
                GameController g = GameController.g;
                g.Manager.Dados.Cristais += valor;
                GameObject G = g.El.retorna(DoJogo.pegueiCristal);
                Destroy(Instantiate(G,transform.position+1f*Vector3.up,G.transform.rotation),5);
                g.MyKeys.MudaAutoShift(ID, true);
                if(g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto))
                    g.HudM.AtualizeImagemDeAtivos();
                Destroy(gameObject);
            }
        }
    }
}
