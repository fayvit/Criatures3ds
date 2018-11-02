using UnityEngine;

[System.Serializable]
public class AnimadorHumano 
{

   [SerializeField] private Animator animator;

    public AnimadorHumano(Animator animator)
    {
        this.animator = animator;
    }

    public void AnimaAndar(float magnitude)
    {
        if(animator!=null)
            animator.SetFloat("velocidade", magnitude);
    }

    public void ResetaEnvia()
    {
        animator.SetBool("envia", false);
    }
    public void ResetaTroca()
    {
        animator.SetBool("chama", false);
    }

    public void AnimaEmpurra()
    {
        animator.Play("empurrando");
    }

    public void ForcarPadrao()
    {
        animator.Play("padrao");
    }

    public void AnimaEnvia()
    {
        animator.SetBool("envia", true);
    }

    public void AnimaTroca()
    {
//        animator.Play("animaTroca");
        animator.SetBool("chama", true);
    }    

    public void PararAnimacao()
    {
        animator.SetFloat("velocidade", 0);
    }

    public void AnimaIniciaPulo()
    {
        animator.Play("pulando");
        animator.SetBool("noChao", false);
    }

    public void AnimaDurantePulo()
    {
        // transição via tempo de fuga
    }

    public void AnimaDescendoPulo()
    {
        animator.SetBool("noChao", true);
    }
}
