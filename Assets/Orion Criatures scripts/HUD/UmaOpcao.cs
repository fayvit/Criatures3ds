using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UmaOpcao : MonoBehaviour
{
    [SerializeField] private Image spriteDoItem;

    public Image SpriteDoItem
    {
        get { return spriteDoItem; }
        set { spriteDoItem = value; }
    }

    protected System.Action<int> Acao { get; set; }

    public virtual void FuncaoDoBotao()
    {
        Acao(transform.GetSiblingIndex() - 1);
    }
}
