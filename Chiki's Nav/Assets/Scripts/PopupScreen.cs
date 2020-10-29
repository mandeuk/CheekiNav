using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupScreen : MonoBehaviour
{
    public TMPro.TextMeshProUGUI popuptitle, popupsubtitle, popupdesc;
    public void TurnonPopupScreen(string title, string subtitle, string description)
    {
        popuptitle.text = title;
        popupsubtitle.text = subtitle;
        popupdesc.text = description;
    }
}
