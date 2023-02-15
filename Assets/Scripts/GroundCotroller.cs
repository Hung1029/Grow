using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundCotroller : ObjectController
{
    public int plastic_level = 0;
    public Animator mhole;
    public GameObject mFeiliao;
    public GameObject gress;

    private bool has_plastic = false;
    private bool delay_ground = false;
    private float timestamp = 0;

    public AudioSource levelUp;


    public void PlayLevelU() {
        levelUp.Play(0);
    }

    //�콦�������ʵe
    public void SetPlasticAnim()
    {
        if (plastic_level == 1)
        {
            animator.SetTrigger("plastic");
            has_plastic = true;
        }
        else if (plastic_level == 2)
        {
            mhole.SetBool("dig", true);
        }
    }

    public void SetPlasticLevel(int plevel)
    {
        plastic_level = plevel;
    }

    // �]�w�I�ΰʵe
    public void SetFeiliaoAnim()
    {
        if (animator != null)
        {
            mFeiliao.GetComponent<Animator>().SetTrigger("feiliao");
            delay_ground = true;
        }
    }



    // �]�w�g�[�ʵe
    public void SetGroundAnim(int stop)
    {
        if (animator != null)
        {
            
            if (stop == 1)
            {
                if (level == 0)
                {
                    animator.SetInteger("level", -1);
                }
                else
                {
                    animator.SetInteger("level", 0);
                }
                if (!has_plastic && plastic_level == 1)
                {
                    SetPlasticAnim();
                }
            }
            else
            {
                if (level == 2) Destroy(mFeiliao);
                if (level == 0) Destroy(gress);
                animator.SetInteger("level", level);
            }
        }
    }

    private void Update()
    {
        if (delay_ground) {
            timestamp += Time.deltaTime;
            if (timestamp > 2.0)
            {
                timestamp = 0;
                delay_ground = false;
                SetGroundAnim(0);
            }
        }
    }

    // �]�w�콦�B�g�[�B�I�ε���
    public void SetGroundLevel(int g_level)
    {
        level = g_level;
        if (g_level == 0) //�P�g
        {
            delay_ground = true;
        }
        else if (g_level > 0) // ��L�n���I�ΰʵe�b�g�[�e�{�ɯ�
        {
            SetFeiliaoAnim();
        }
    }
}
