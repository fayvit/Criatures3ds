using UnityEngine;
[System.Serializable]
public struct colisor
{
    public string osso;
    /*
	[NonSerializedAttribute]
	public Vector3 deslColisor;
	[NonSerializedAttribute]
	public Vector3 deslTrail;
*/

    private float dCx;
    private float dCy;
    private float dCz;

    private float dTx;
    private float dTy;
    private float dTz;

    private float colisorScale;


    public colisor(string _osso, float colisorScale)
    {
        this = new colisor(_osso, Vector3.zero, Vector3.zero, colisorScale);
    }

    public colisor(string _osso = "", Vector3 _deslColisor = default(Vector3), Vector3 _deslTrail = default(Vector3), float colisorScale = 1)
    {
        osso = _osso;
        dCx = _deslColisor.x;
        dCy = _deslColisor.y;
        dCz = _deslColisor.z;

        dTx = _deslTrail.x;
        dTy = _deslTrail.y;
        dTz = _deslTrail.z;

        this.colisorScale = colisorScale;
    }

    public float ColisorScale
    {
        get
        {
            if (colisorScale == 0)
                colisorScale = 1;

            return colisorScale;
        }
    }

    public Vector3 deslColisor
    {
        get { return new Vector3(dCx, dCy, dCz); }
        set
        {
            Vector3 dC = value;
            dCx = dC.x;
            dCy = dC.y;
            dCz = dC.z;
        }
    }

    public Vector3 deslTrail
    {
        get { return new Vector3(dTx, dTy, dTz); }
        set
        {
            Vector3 dC = value;
            dTx = dC.x;
            dTy = dC.y;
            dTz = dC.z;
        }
    }
}