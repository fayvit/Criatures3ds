using UnityEngine;
using UnityEngine.UI;
using System;

public class UmaOpcaoDeLinguagem : UmaOpcaoDeMenu
{
    [SerializeField] private Image imgDaBandeira;
    public void SetarOpcao(Action<int> acaoDaOpcao, string txtDaOpcao,Sprite img)
    {
        base.SetarOpcao(acaoDaOpcao, txtDaOpcao);
        imgDaBandeira.sprite = img;
    }
}
