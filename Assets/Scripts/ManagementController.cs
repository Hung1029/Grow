using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagementController : MonoBehaviour
{
    public TextMeshProUGUI year;
    public TextMeshProUGUI month;
    public TextMeshProUGUI ph_value;

    private int m = 10;
    private int y = 1;
    private float cal_time = 0;
    private bool cal_run = false;
    private int target = 7;

    public GameObject farmer;
    public GameObject ground;
    public GameObject pineapple;
    public GameObject sun;
    public GameObject steam;
    public GameObject success;

    public GameObject btn_farmer;
    public GameObject btn_ground;
    public GameObject btn_pineapple;
    public GameObject btn_plastic;
    public GameObject btn_hat;

    private int farmer_level = 0;
    private int ground_level = -1;
    private int plastic_level = 0;
    private int pineapple_level = 0;
    private int hat_level = 0;
    private bool has_plastic = false;

    private bool have_delay = false;
    private float time = 0;
    private string delay_anim = "";
    private float delay_time = 2;


    private void PineappleAndFarmer()
    {
        if (farmer_level == 1 && pineapple_level == 1) // 1級鳳梨觸發1級農夫
        {
            have_delay = true;
            delay_time = 3;
            delay_anim = "farmer";
        }
        else if (farmer_level == 2 && pineapple_level == 1) // 2級農夫觸發1級鳳梨
        {
            Debug.Log("2級鳳梨");
            pineapple_level += 1;
            have_delay = true;
            delay_time = 3;
            delay_anim = "pineapple";
            target = 7;
        }
        else if (farmer_level == 2 && pineapple_level == 2) // 2級鳳梨觸發2級農夫
        {
            Debug.Log("辣手摧花");
            have_delay = true;
            delay_time = 4;
            delay_anim = "farmer";
        }
        else if (farmer_level == 3 && pineapple_level == 2) // 3級農夫觸發2級鳳梨
        {
            Debug.Log("長花");
            pineapple_level += 1;
            have_delay = true;
            delay_time = 3.5f;
            delay_anim = "pineapple";
            target = 11;

        }
        else if (pineapple_level == 5) {
            have_delay = true;
            delay_time = 3;
            delay_anim = "success";
            steam.SetActive(false);
        }

    }

    public void SetFarmerLevel()
    {
        farmer_level += 1;
        if (farmer_level == 3) SetRunTarget(11);
        farmer.GetComponent<ObjectController>().SetLevel(farmer_level);

        if (farmer_level == 1 && ground_level == -1) { // 如果翻土土地還沒施肥過
            SetGroundLevel();
        }

        PineappleAndFarmer();
    }

    //控制土地
    public void SetGroundLevel()
    {
        if (!has_plastic) { //還沒有蓋布才會施肥成功
            if (ground_level == -1) // 沒有翻土
            {
                ground_level = 0;
                ground.GetComponent<GroundCotroller>().SetGroundLevel(ground_level);
            }
            else if (ground_level < 2)
            {
                ground_level += 1;
                if(ground_level == 1) ph_value.text = "6.1-6.9ph";
                if (ground_level == 2) ph_value.text = "4.5~6.0ph";
                ground.GetComponent<GroundCotroller>().SetGroundLevel(ground_level);
            }
            
        }

    }

    // click 塑膠布
    public void SetPlastic()
    {
        plastic_level += 1;
        ground.GetComponent<GroundCotroller>().SetPlasticLevel(plastic_level);
        //如果已經施肥1自動升級
        if (ground_level == 1)
        {
            SetGroundLevel();
        }

        //如果是塑膠2手動撥動畫
        if (plastic_level == 2)
        {
            ground.GetComponent<GroundCotroller>().SetPlasticAnim();
        }
    }

    public void SetPineaplle() 
    {
        pineapple_level += 1;
        
        if (plastic_level == 1)
        {
            SetPlastic();
            have_delay = true;
            delay_anim = "pineapple";
            target = 7;
        }
        if (pineapple_level == 4)
        {
            pineapple.GetComponent<ObjectController>().SetLevel(pineapple_level);
            have_delay = true;
            delay_time = 3.5f;
            delay_anim = "sun";
            target = 2;
            cal_run = true;
        }
    }

    private void SetCalender()
    {
        switch(y){
            case 1:
                year.text = "第一年";
                break;
            case 2:
                year.text = "第二年";
                break;
            case 3:
                year.text = "第三年";
                break;
        }
        month.text = m.ToString();
        Debug.Log($"第{y}年 {m}月");
    }

  

    public void SetHat()
    {
        hat_level += 1;
        if (pineapple_level == 3)
        {
            SetPineaplle();
        }
    }


    private void SetSun()
    {
        sun.GetComponent<Animator>().SetTrigger("sun");
        have_delay = true;
        delay_time = 3.0f;
        steam.SetActive(true);
        delay_anim = "steam";

    }

    private void SetSteam()
    {
        steam.GetComponent<Animator>().SetTrigger("rise");
        have_delay = true;
        delay_time = 3.0f;
        delay_anim = "pineapple";
        pineapple_level += 1;
        target = 3;
    }

    private void SetSuccess()
    {
        success.SetActive(true);
        success.GetComponent<Animator>().SetTrigger("show");

    }

    private void DelayAnimation()
    {
        // 前面一定有其他動畫再跑
        switch (delay_anim)
        {
            case "pineapple":
                if(pineapple_level != 1 && pineapple_level != 3) cal_run = true;
                pineapple.GetComponent<ObjectController>().SetLevel(pineapple_level);
                PineappleAndFarmer();
                break;
            case "farmer":
                SetFarmerLevel();
                break;
            case "sun":
                SetSun();
                break;
            case "steam":
                SetSteam();
                break;
            case "success":
                SetSuccess();
                break;
        }
    }

    private void Update()
    {
        if (have_delay)
        {
            time += Time.deltaTime;
            if (time > delay_time)
            {
                time = 0;
                have_delay = false;
                DelayAnimation();
            }
        }
        if (cal_run) // 計算日曆
        {
            cal_time += Time.deltaTime;
            if (cal_time >= 0.5f)
            {
                if (m == 12)
                {
                    m = 1;
                    y += 1;
                }
                else
                {
                    m += 1;
                }
                SetCalender();
                if (m == target) {
                    cal_run = false;
                }
                cal_time = 0;
            }
        }
    }

    private void SetRunTarget(int m)
    {
        target = m;
        cal_run = true;
    }


    public void ClickOn(string name)
    {
        switch (name) {
            case "pineapple":
                btn_pineapple.GetComponent<Animator>().SetTrigger("flip");
                SetPineaplle();
                break;
            case "farmer":
                Debug.Log("flip");
                btn_farmer.GetComponent<Animator>().SetTrigger("flip");
                SetFarmerLevel();
                break;
            case "hat":
                btn_hat.GetComponent<Animator>().SetTrigger("flip");
                SetHat();
                break;
            case "fertilize":
                btn_ground.GetComponent<Animator>().SetTrigger("flip");
                SetGroundLevel();
                break;
            case "plastic":
                btn_plastic.GetComponent<Animator>().SetTrigger("flip");
                SetPlastic();
                break;
        }
    }
}
