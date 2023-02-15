using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    protected Animator animator;

    public int level = 0;

    public AudioSource audio_gress;
    public AudioSource audio_seed;
    public AudioSource shrink;
    public AudioSource hat;

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

    public void PlayGetRidGress()
    {
        FindObjectOfType<SoundManager>().Play("get_rid_gress");
        audio_gress.Play(0);
    }

    public void PlayLevelUp()
    {
        FindObjectOfType<SoundManager>().Play("level_up");
    }

    public void PlaySeed()
    {
        audio_seed.Play(0);
    }

    public void PlayShrink()
    {
        shrink.Play(0);
    }

    public void PlayHat()
    {
        hat.Play(0);
    }
}
