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
    public static string itemIndex;
    public static InputField taskName;
    public static InputField allTasksNum;
    public static InputField dailyTasksNum;
    public static Text daysCount;
    public Transform content;
    VerticalLayoutGroup contentLayout;

    public static List<Item> itemList;
    int currentIndex=0;//新的item的index应该是多少
    Button okBtn;
    Button clearBtn;
    Button addBtn;
    Button deleteBtn;

    private void Start()
    {
        contentLayout = content.GetComponent<VerticalLayoutGroup>();

        okBtn = transform.Find("OKButton").GetComponent<Button>();
        okBtn.onClick.AddListener(OnOkBtnDown);

        clearBtn = transform.Find("ClearButton").GetComponent<Button>();
        clearBtn.onClick.AddListener(OnClearBtnDown);

        addBtn = transform.Find("AddButton").GetComponent<Button>();
        addBtn.onClick.AddListener(OnAddBtnDown);


        deleteBtn = transform.Find("DeleteButton").GetComponent<Button>();
        deleteBtn.onClick.AddListener(OnDeleteBtnDown);

        int length = PlayerPrefs.GetInt("currentIndex");
        itemList = new List<Item>();
        
        //创建多个item
        for (int i = 0; i < length; i++)
        {
 
            GameObject itemObj = Instantiate(Resources.Load<GameObject>("Prefabs/DailyItem"));
            itemObj.transform.parent = content;
            Item item = itemObj.GetComponent<Item>();
            itemList.Add(item);
            item.index = i;
            itemObj.name = "dailyItem" + item.index.ToString();

            taskName = itemObj.transform.Find("InputGroup/name").GetComponent<InputField>();
            allTasksNum = itemObj.transform.Find("InputGroup/all").GetComponent<InputField>();
            dailyTasksNum = itemObj.transform.Find("InputGroup/daily").GetComponent<InputField>();
            daysCount = itemObj.transform.Find("days").GetComponent<Text>();

            string index = i.ToString();
            if (PlayerPrefs.HasKey(index))
            {
                taskName.text=PlayerPrefs.GetString("name" + index);
                allTasksNum.text = PlayerPrefs.GetFloat("all" + index).ToString();
                dailyTasksNum.text= PlayerPrefs.GetFloat("daily" + index).ToString();
                int count = PlayerPrefs.GetInt("count" + index);
                DateTime dt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                //将当前日期加天
                dt=dt.AddDays(count);

                daysCount.text = "需要" + count.ToString() + "天完成！" + "预计在" + dt.Year + "年" + dt.Month + "月" + dt.Day + "日完成！";
            }
        }
    }

    public void OnOkBtnDown()
    {
        Debug.Log("按下ok");
        string index = itemIndex;
        string name = taskName.text;
        float all = Convert.ToInt32(allTasksNum.text);
        float daily = Convert.ToInt32(dailyTasksNum.text);
        int count = Convert.ToInt32(Mathf.Ceil(all/ daily));
        DateTime dt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
        //将当前日期加天
        dt=dt.AddDays(count);

        daysCount.text = "需要" + count.ToString() + "天完成！"+"预计在"+ dt.Year +"年"+dt.Month+"月"+dt.Day+"日完成！";

        if (name != "") SaveItemInfo(name,all,daily,count,index);
    }

    public void OnClearBtnDown()
    {
        //清除当前item的内容
        //删除所有item和存档
        PlayerPrefs.DeleteAll();
        int count = content.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(content.GetChild(0).gameObject);
        }
    }
    public void OnClearAllBtnDown()
    {
        //删除所有item和存档
        PlayerPrefs.DeleteAll();
        int count = content.childCount;
        for(int i = 0; i < count; i++)
        {
            Destroy(content.GetChild(0));
        }
    }
    public void OnAddBtnDown()
    {
        GameObject itemObj = Instantiate(Resources.Load<GameObject>("Prefabs/DailyItem"));
        itemObj.transform.parent = content;
        Item item = itemObj.GetComponent<Item>();
        itemList.Add(item);
        currentIndex = PlayerPrefs.GetInt("currentIndex");
        currentIndex+=1;
        item.index = currentIndex;
        itemObj.name = "dailyItem" + item.index.ToString();

        SaveCurrentIndex(currentIndex);

        ChangeContentHeight(10);
    }
    public void OnDeleteBtnDown()
    {
        //删除当前末尾的item
        DeleteItemInfo(itemIndex);
        
        currentIndex = PlayerPrefs.GetInt("currentIndex");
        Debug.Log("currentIndex: "+currentIndex);
        if (currentIndex <= 0) return;
        Destroy(itemList[currentIndex-1].gameObject);
        itemList.Remove(itemList[currentIndex - 1]);
        currentIndex--;
        SaveCurrentIndex(currentIndex);

        ChangeContentHeight(10,false);
    }

    void SaveItemInfo(string name,float all,float daily,int count,string index)
    {
        PlayerPrefs.SetInt(index, 1);
        PlayerPrefs.SetString("name"+ index, name);
        PlayerPrefs.SetFloat("all" + index, all);
        PlayerPrefs.SetFloat("daily" + index, daily);
        PlayerPrefs.SetInt("count" + index, count);
    }
    void DeleteItemInfo(string index)
    {
        PlayerPrefs.DeleteKey(index);
        PlayerPrefs.DeleteKey("name" + index);
        PlayerPrefs.DeleteKey("all" + index);
        PlayerPrefs.DeleteKey("daily" + index);
        PlayerPrefs.DeleteKey("count" + index);
    }
    void SaveCurrentIndex(int currentIndex)
    {
        PlayerPrefs.SetInt("currentIndex", currentIndex);
    }
    void ChangeContentHeight(float height,bool add=true)
    {
        float x0 = content.GetComponent<RectTransform>().sizeDelta.x;
        float y0 = content.GetComponent<RectTransform>().sizeDelta.y;
        y0 += height;
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(x0,y0);
    }
}
