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
    Button btn;
    InputField allTasksNum;
    InputField dailyTasksNum;
    Text daysCount;
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnBtnDown);

        allTasksNum = transform.Find("all").GetComponent<InputField>();
        dailyTasksNum = transform.Find("daily").GetComponent<InputField>();
        daysCount = transform.Find("days").GetComponent<Text>();
    }

    public void OnBtnDown()
    {
        Debug.Log("按下item");
        ItemManager.allTasksNum = allTasksNum;
        ItemManager.dailyTasksNum = dailyTasksNum;
        ItemManager.daysCount = daysCount;
    }
}
