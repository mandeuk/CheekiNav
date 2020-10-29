using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pallet : MonoBehaviour
{
    public GameObject red, green, blue;
    public TMPro.TMP_InputField hexinputfield;
    Color refreshcolor;
    //float r, g, b;

    private void Awake()
    {
        refreshcolor.r = 1.0f;
        refreshcolor.g = 1.0f;
        refreshcolor.b = 1.0f;
        refreshcolor.a = 1.0f;
    }

    public void ChangeColor()
    {
        refreshcolor.r = red.GetComponent<Slider>().value;
        refreshcolor.g = green.GetComponent<Slider>().value;
        refreshcolor.b = blue.GetComponent<Slider>().value;
        refreshcolor.a = 1.0f;
        this.GetComponent<Image>().color = refreshcolor;

        ChangeHexcode(refreshcolor);
    }
    public void ChangeColorforHex()
    {
        if (ColorUtility.TryParseHtmlString("#"+hexinputfield.text, out refreshcolor))
        {
            this.GetComponent<Image>().color = refreshcolor;
            ChangeSlider(refreshcolor);
        }
    }

    public Color GetColor()
    {
        if(refreshcolor == Color.white)
            ChangeColor();
        return refreshcolor;
    }

    public void ChangeSlider(Color fontcolor)
    {
        red.GetComponent<Slider>().value = fontcolor.r;
        green.GetComponent<Slider>().value = fontcolor.g;
        blue.GetComponent<Slider>().value = fontcolor.b;
    }

    public void ChangeHexcode(Color fontcolor)
    {
        hexinputfield.text = ColorUtility.ToHtmlStringRGB(fontcolor);
    }
}
