//==========================
// - FileName:      ItemManager.cs         
// - Created:       fww	
// - CreateTime:    2020.9.1
// - Description:   
//==========================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ItemManager : MonoBehaviour
{
    public static InputField allTasksNum;
    public static InputField dailyTasksNum;
    public static Text daysCount;


    public void OkBtnDown()
    {
        Debug.Log("按下ok");
        float all = Convert.ToInt32(allTasksNum.text);
        float daily = Convert.ToInt32(dailyTasksNum.text);
        int count = Convert.ToInt32(Mathf.Ceil(all/ daily));
        daysCount.text = "需要" + count.ToString() + "天完成！";
    }
    public void ClearBtnDown()
    {

    }
    public void AddBtnDown()
    {

    }

}
