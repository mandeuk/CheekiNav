using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIcon : MonoBehaviour
{
    Icon icon;
    public GameObject background;
    public SpriteRenderer image;

    private void Awake()
    {
        icon = new Icon();
    }

    public Vector3 GetIconPosition()
    {
        return icon.iconpos;
    }
    public float GetIconSize()
    {
        return icon.iconsize;
    }
    public Icon GetIconData()
    {
        return icon;
    }




    public void SetIcon(Vector3 inputpos)
    {
        SetIconPos(inputpos);
    }
    public void SetIcon(string name)
    {
        SetIconImage(name);
    }
    public void SetIcon(string select, float value)
    {
        switch (select)
        {
            case "rotation":
                SetIconRotation(value);
                break;
            case "size":
                SetIconSize(value);
                break;
        }
    }
    public void SetIcon(Color inputcolor)
    {
        SetIconColor(inputcolor);
    }
    public void SetIcon(Icon icon)
    {
        SetIconPos(icon.iconpos);
        SetIconRotation(icon.iconrotation);
        SetIconSize(icon.iconsize);
        SetIconImage(icon.spritename);
        SetIconColor(icon.iconcolor);
    }





    void SetIconPos(Vector3 inputpos)
    {
        Vector3 ZzeroPos;
        ZzeroPos = inputpos;
        ZzeroPos.z = 0.0f;
        icon.iconpos = ZzeroPos;
        this.transform.SetPositionAndRotation(ZzeroPos, this.transform.rotation);
    }
    void SetIconRotation(float rotation)
    {
        icon.iconrotation = rotation;

        this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        this.transform.Rotate(Vector3.forward, icon.iconrotation);
    }
    void SetIconSize(float input)
    {
        if (input < 0.1f)
            input = 0.1f;
        else if (input > 100.0f)
            input = 100.0f;

        icon.iconsize = input;

        Vector3 scale;
        scale.x = input;
        scale.y = input;
        scale.z = 1.0f;
        this.transform.localScale = scale;
    }
    void SetIconImage(string name)
    {
        icon.spritename = name;
        image.sprite = Resources.Load<Sprite>("Images/Icon/" + name);
    }
    void SetIconColor(Color inputcolor)
    {
        icon.iconcolor = inputcolor;
        image.color = inputcolor;
    }

    
}


[System.Serializable]
public class Icon
{
    public float iconsize, iconrotation;
    public Vector3 iconpos;
    public Color iconcolor;
    public string spritename;

    public Icon()
    {
        iconsize = 1.0f;
        iconrotation = 0.0f;
        
        iconpos = Vector3.zero;
        iconcolor = Color.white;
        spritename = "chiki128";
    }
}