using UnityEngine;

public class AnimadorCriature
{
    public static void AnimaAtaque(GameObject G, string nomeAnima)
    {
        Animator a = G.GetComponent<Animator>();
        a.SetBool("atacando", true);
        a.Play(nomeAnima);
    }
}