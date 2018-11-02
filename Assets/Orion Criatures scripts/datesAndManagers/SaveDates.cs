using UnityEngine;

[System.Serializable]
public class SaveDates
{
    private DadosDoPersonagem dados;
    private KeyVar variaveisChave;
    private float[] posicao;
    private float[] rotacao;

    public SaveDates(NomesCenas[] cenasAtivas)
    {
        if (GameController.g)
            if (GameController.g.Manager)
            {
                SetarSaveDates();
                variaveisChave.SetarCenasAtivas(cenasAtivas);
            }
     
    }

    public SaveDates()
    {
        if (GameController.g)
            if (GameController.g.Manager)
            {
                SetarSaveDates();
                variaveisChave.SetarCenasAtivas();
            }
        
    }

    private void SetarSaveDates()
    {
        CharacterManager manager = GameController.g.Manager;
        variaveisChave = GameController.g.MyKeys;
        
        dados = manager.Dados;

        Vector3 X = manager.transform.position;
        Vector3 R = manager.transform.forward;

        
        posicao = new float[3] { X.x, X.y, X.z };
        rotacao = new float[3] { R.x, R.y, R.z };

     //   Debug.Log(X +" : "+ posicao[0]+" : "+posicao[1]+" : "+posicao[2]);
    }

    public DadosDoPersonagem Dados
    {
        get { return dados; }
    }

    public KeyVar VariaveisChave
    {
        get { return variaveisChave; }
    }

    public Vector3 Posicao
    {
        get { return new Vector3(posicao[0],posicao[1],posicao[2]);}
    }

    public Quaternion Rotacao
    {
        get { return Quaternion.LookRotation(new Vector3(rotacao[0], rotacao[1], rotacao[2])); }
    }
}

