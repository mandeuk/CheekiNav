using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffList : MonoBehaviour
{
    public GameObject[] editbox;
    Color OnColor, OffColor;
    bool isUIActive;

    private void Awake()
    {
        OnColor = new Color(1, 1, 1, 1);
        OffColor = new Color(1, 1, 1, 0.2f);
        isUIActive = true;
    }

    public void ShowHideEditbox()
    {
        if (isUIActive)
        {
            for (int i = 0; i < editbox.Length; ++i)
            {
                if (editbox[i].activeInHierarchy)
                    editbox[i].SetActive(false);
            }
            isUIActive = false;
        }
        else
        {
            for (int i = 0; i < editbox.Length; ++i)
            {
                if (editbox[i].CompareTag("Window"))
                {
                    if (editbox[i].activeInHierarchy)
                        editbox[i].SetActive(false);
                }
                else
                {
                    editbox[i].SetActive(true);
                }
            }
            isUIActive = true;
        }
    }

    public void ButtonAlphaChange()
    {
        if (isUIActive)
            this.GetComponent<UnityEngine.UI.Image>().color = OnColor;
        else
            this.GetComponent<UnityEngine.UI.Image>().color = OffColor;
    }
}
