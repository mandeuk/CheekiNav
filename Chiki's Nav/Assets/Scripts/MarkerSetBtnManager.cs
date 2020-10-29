using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSetBtnManager : MonoBehaviour
{
    public GameObject dropdown, factory, customs, woods, interchange, shoreline, shorelineresort, reserve, labs, map;
    public Sprite factoryimg, customsimg, woodsimg, interchangeimg, shorelineimg, shorelineresortimg, reserveimg, labsimg;

    private void Start()
    {
#if UNITY_STANDALONE_WIN&&UNITY_EDITOR
        Debug.Log("MarketSetBtnManager.cs PC mode ON");
#endif
#if UNITY_ANDROID&&UNITY_EDITOR
        Debug.Log("MarketSetBtnManager.cs 안드로이드 mode ON");
#endif
        RefreshMarkersetList();
    }

    public void RefreshMarkersetList()
    {
        System.IO.DirectoryInfo dinfo = new System.IO.DirectoryInfo(GameManager.instance.savedatapath);

        List<string> factoryoption = new List<string>();
        List<string> customsoption = new List<string>();
        List<string> woodsoption = new List<string>();
        List<string> interchangeoption = new List<string>();
        List<string> shorelineoption = new List<string>();
        List<string> shorelineresortoption = new List<string>();
        List<string> reserveoption = new List<string>();
        List<string> labsoption = new List<string>();

        factoryoption.Add("선택안함(None Select)");
        customsoption.Add("선택안함(None Select)");
        woodsoption.Add("선택안함(None Select)");
        interchangeoption.Add("선택안함(None Select)");
        shorelineoption.Add("선택안함(None Select)");
        shorelineresortoption.Add("선택안함(None Select)");
        reserveoption.Add("선택안함(None Select)");
        labsoption.Add("선택안함(None Select)");

        foreach (var item in dinfo.GetFiles())
        {
            //if(item.Extension == extentionName)
            //    Debug.Log(item.Extension);

            switch (item.Extension)
            {
                case ".factory":
                    factoryoption.Add(item.Name);
                    break;
                case ".customs":
                    customsoption.Add(item.Name);
                    break;
                case ".woods":
                    woodsoption.Add(item.Name);
                    break;
                case ".interchange":
                    interchangeoption.Add(item.Name);
                    break;
                case ".shoreline":
                    shorelineoption.Add(item.Name);
                    break;
                case ".resort":
                    shorelineresortoption.Add(item.Name);
                    break;
                case ".reserve":
                    reserveoption.Add(item.Name);
                    break;
                case ".labs":
                    labsoption.Add(item.Name);
                    break;
            }
            
        }
        if (factoryoption.Count != 0)
        {
            factory.GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
            factory.GetComponent<TMPro.TMP_Dropdown>().AddOptions(factoryoption);
        }
        if (customsoption.Count != 0)
        {
            customs.GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
            customs.GetComponent<TMPro.TMP_Dropdown>().AddOptions(customsoption);
        }
        if (woodsoption.Count != 0)
        {
            woods.GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
            woods.GetComponent<TMPro.TMP_Dropdown>().AddOptions(woodsoption);
        }
        if (interchangeoption.Count != 0)
        {
            interchange.GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
            interchange.GetComponent<TMPro.TMP_Dropdown>().AddOptions(interchangeoption);
        }
        if (shorelineoption.Count != 0)
        {
            shoreline.GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
            shoreline.GetComponent<TMPro.TMP_Dropdown>().AddOptions(shorelineoption);
        }
        if (shorelineresortoption.Count != 0)
        {
            shorelineresort.GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
            shorelineresort.GetComponent<TMPro.TMP_Dropdown>().AddOptions(shorelineresortoption);
        }
        if (reserveoption.Count != 0)
        {
            reserve.GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
            reserve.GetComponent<TMPro.TMP_Dropdown>().AddOptions(reserveoption);
        }
        if (labsoption.Count != 0)
        {
            labs.GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
            labs.GetComponent<TMPro.TMP_Dropdown>().AddOptions(labsoption);
        }
        
    }

    public void ChangeMarkersetGroup()
    {
        MapMarkerManager.instance.RemoveAllMarker();

        if (factory.activeSelf)
            factory.SetActive(false);
        if (customs.activeSelf)
            customs.SetActive(false);
        if (woods.activeSelf)
            woods.SetActive(false);
        if (interchange.activeSelf)
            interchange.SetActive(false);
        if (shoreline.activeSelf)
            shoreline.SetActive(false);
        if (shorelineresort.activeSelf)
            shorelineresort.SetActive(false);
        if (reserve.activeSelf)
            reserve.SetActive(false);
        if (labs.activeSelf)
            labs.SetActive(false);
        
        switch (dropdown.GetComponent<TMPro.TMP_Dropdown>().value)
        {
            case 0:
                factory.SetActive(true);
                map.GetComponent<SpriteRenderer>().sprite = factoryimg;
                GameManager.instance.selectedmap = "factory";
                break;
            case 1:
                customs.SetActive(true);
                map.GetComponent<SpriteRenderer>().sprite = customsimg;
                GameManager.instance.selectedmap = "customs";
                break;
            case 2:
                woods.SetActive(true);
                map.GetComponent<SpriteRenderer>().sprite = woodsimg;
                GameManager.instance.selectedmap = "woods";
                break;
            case 3:
                interchange.SetActive(true);
                map.GetComponent<SpriteRenderer>().sprite = interchangeimg;
                GameManager.instance.selectedmap = "interchange";
                break;
            case 4:
                shoreline.SetActive(true);
                map.GetComponent<SpriteRenderer>().sprite = shorelineimg;
                GameManager.instance.selectedmap = "shoreline";
                break;
            case 5:
                shorelineresort.SetActive(true);
                map.GetComponent<SpriteRenderer>().sprite = shorelineresortimg;
                GameManager.instance.selectedmap = "resort";
                break;
            case 6:
                reserve.SetActive(true);
                map.GetComponent<SpriteRenderer>().sprite = reserveimg;
                GameManager.instance.selectedmap = "reserve";
                break;
            case 7:
                labs.SetActive(true);
                map.GetComponent<SpriteRenderer>().sprite = labsimg;
                GameManager.instance.selectedmap = "labs";
                break;
        }
    }
}
