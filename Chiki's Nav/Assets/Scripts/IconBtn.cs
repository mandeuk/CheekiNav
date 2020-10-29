using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconBtn : MonoBehaviour
{
    public Sprite iconimage;
    // Start is called before the first frame update
    void Start()
    {
        if(iconimage != null)
        {
            this.GetComponent<UnityEngine.UI.Image>().sprite = iconimage;
        }
    }

    public void SetSpriteToIcon()
    {
        if (iconimage != null)
        {
            MapMarkerManager.instance.SetIconSprite(iconimage);
        }
    }
}
