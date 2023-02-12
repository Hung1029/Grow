using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    protected Animator animator;

    public int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void SetLevel(int i_level)
    {
        level = i_level;
        if (animator != null)
        {
            animator.SetInteger("level", level);
        }
    }


    public void StopAni()
    {
        if (animator != null)
        {
            animator.SetInteger("level", 0);
        }
    }
}
