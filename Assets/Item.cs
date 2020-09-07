//==========================
// - FileName:      Item.cs         
// - Created:       #AuthorName#	
// - CreateTime:    #CreateTime#	
// - Description:   
//==========================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Button btn;
    InputField taskName;
    InputField allTasksNum;
    InputField dailyTasksNum;
    Text daysCount;
    [HideInInspector]public int index=0;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnBtnDown);

        taskName = transform.Find("InputGroup/name").GetComponent<InputField>();
        allTasksNum = transform.Find("InputGroup/all").GetComponent<InputField>();
        dailyTasksNum = transform.Find("InputGroup/daily").GetComponent<InputField>();
        daysCount = transform.Find("days").GetComponent<Text>();
        canvasGroup = transform.Find("InputGroup").gameObject.AddComponent<CanvasGroup>();
        canvasGroup.interactable = false;

    }

    public void OnBtnDown()
    {
        Debug.Log("按下item");
        canvasGroup.interactable = true;
        ItemManager.taskName = taskName;
        ItemManager.allTasksNum = allTasksNum;
        ItemManager.dailyTasksNum = dailyTasksNum;
        ItemManager.daysCount = daysCount;
        ItemManager.itemIndex = index.ToString();
    }
}
