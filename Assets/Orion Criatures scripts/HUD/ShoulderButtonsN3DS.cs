using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShoulderButtonsN3DS {
    [SerializeField] private Image R_buttonImageBase;
    [SerializeField] private Image L_buttonImageBase;
    [SerializeField] private Image RX;
    [SerializeField] private Image RY;
    [SerializeField] private Image RA;
    [SerializeField] private Image RB;
    [SerializeField] private Image LX;
    [SerializeField] private Image LY;
    [SerializeField] private Image LA;
    [SerializeField] private Image LB;

    public void ChangeR(bool change)
    {
        R_buttonImageBase.enabled = change;
        RX.enabled = change;
        RY.enabled = change;
        RA.enabled = change;
        RB.enabled = change;
    }

    public void ChangeL(bool change)
    {
        L_buttonImageBase.enabled = change;
        LX.enabled = change;
        LY.enabled = change;
        LA.enabled = change;
        LB.enabled = change;
    }
}
