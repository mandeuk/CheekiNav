using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkersetLoader : MonoBehaviour
{
    public TMPro.TMP_InputField filename;

    public void LoadMarker()
    {
        List<TMPro.TMP_Dropdown.OptionData> optionslist = this.GetComponent<TMPro.TMP_Dropdown>().options.GetRange(this.GetComponent<TMPro.TMP_Dropdown>().value,1);
        foreach(var item in optionslist)
        {
            MapMarkerManager.instance.RemoveAllMarker();
            MapMarkerManager.instance.LoadMarkerData(item.text);

            if (!string.Equals(item.text, "선택안함(None Select)"))
            {
                int index = item.text.IndexOf(".");
                filename.text = item.text.Remove(index);
            }
            else
            {
                filename.text = "";
            }
        }
    }

}
