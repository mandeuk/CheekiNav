using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffBtn : MonoBehaviour
{

    public GameObject editbox, iconEditbox, markerEditbox;

    public void ShowHideEditbox()
    {
        if(editbox.activeInHierarchy)
        {
            editbox.SetActive(false);
        }
        else
        {
            editbox.SetActive(true);
        }
    }

    public void MarkerEditBtn()
    {
        if (markerEditbox.activeInHierarchy)
        {
            markerEditbox.SetActive(false);
        }
        else
        {
            markerEditbox.SetActive(true);
            if (iconEditbox.activeInHierarchy)
            {
                iconEditbox.SetActive(false);
            }
        }
    }

    public void IconEditBtn()
    {
        if (iconEditbox.activeInHierarchy)
        {
            iconEditbox.SetActive(false);
        }
        else
        {
            iconEditbox.SetActive(true);
            if (markerEditbox.activeInHierarchy)
            {
                markerEditbox.SetActive(false);
            }
        }
    }
}
