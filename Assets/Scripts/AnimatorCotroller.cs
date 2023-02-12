using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCotroller : MonoBehaviour
{
    private Animator animator;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void SetLevel(int level)
    {   
        if (animator != null)
        {
            animator.SetInteger("level", level);
        }
    }

}
