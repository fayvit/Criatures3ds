using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTextura : MonoBehaviour {
    [SerializeField] private Vector2 dirdeDeslocamento;
    [SerializeField] private float vel = 1;

    MeshRenderer M;
    Vector2 desl;
	// Use this for initialization
	void Start () {
        M = GetComponent<MeshRenderer>();

        desl = M.material.mainTextureOffset;
        if (dirdeDeslocamento == Vector2.zero)
            dirdeDeslocamento = new Vector2(1, 0);
	}
	
	// Update is called once per frame
	void Update () {
        desl += dirdeDeslocamento * Time.deltaTime * vel;
        M.material.mainTextureOffset = desl;
        M.material.SetTextureOffset(1, desl);

    }
}
