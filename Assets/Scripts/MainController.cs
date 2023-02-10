using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    [SerializeField]
    public GameObject[] page;
    public Button[] stageButton;

    // Start is called before the first frame update
    void Start()
    {
        initPage();
    }
    public void initPage()
    {
        for (int i = 0; i < page.Length; i++)
        {
            page[i].SetActive(false);
        }

        page[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageButtonClick(int stagenum)
    {
        for (int i = 0; i < page.Length; i++)
        {
            page[i].SetActive(false);
        }

        page[stagenum].SetActive(true);

    }

    public void BackToMenuClick()
    {
        for (int i = 0; i < page.Length; i++)
        {
            page[i].SetActive(false);
        }
        page[0].SetActive(true);

    }

    public void EnterStageClick(int num)
    {
        Debug.Log("EnterStage: "+num);
    }
}
