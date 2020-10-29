using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMarker : MonoBehaviour
{
    public GameObject field, textmesh;
    
    //string markername;
    //float markersize, markerrotation;
    //Vector3 markerpos;
    //Color fontcolor;
    //int fontStyles;

    Marker marker;

    private void Awake()
    {
        marker = new Marker();
    }

    public string GetMarkerName()
    {
        return marker.markername;
    }
    public Vector3 GetMarkerPosition()
    {
        return marker.markerpos;
    }
    public float GetMarkerSize()
    {
        return marker.markersize;
    }

    public Marker GetMarkerData()
    {
        return marker;
    }

    //---------------------------------------SetMarker---------------------------------------------

    public void SetMarker(Vector3 pos)
    {
        SetMarkerPos(pos);

    }
    public void SetMarker(string name)
    {
        SetMarkerName(name);
        //Debug.Log("Saved name : " + name);
    }
    public void SetMarker(float size)
    {
        SetMarkerSize(size);
    }
    public void SetMarker(Vector3 pos, string name)
    {
        SetMarkerPos(pos);
        SetMarkerName(name);
        //Debug.Log("Saved name : " + name);
    }
    public void SetMarker(Vector3 pos, string name, float size)
    {
        SetMarkerPos(pos);
        SetMarkerName(name);
        SetMarkerSize(size);
        //Debug.Log("Saved name : " + size);
    }
    public void SetMarker(Vector3 pos, string name, float size, Color color)
    {
        SetMarkerPos(pos);
        SetMarkerName(name);
        SetMarkerSize(size);
        SetMarkerColor(color);
    }


    public void SetMarker(Color color)
    {
        Debug.Log("SetMarker() : " + color);
        SetMarkerColor(color);
    }
    public void SetMarker(string select, float value)
    {
        switch (select)
        {
            case "rotation":
                SetMarkerRotation(value);
                break;
            case "size":
                SetMarkerSize(value);
                break;
        }
    }
    public void SetMarker(string select, int value)
    {
        switch (select)
        {
            case "style":
                SetMarkerFontstyles(value);
                break;
        }

    }
    public void SetMarker(Marker inputmarker)
    {
        SetMarkerPos(inputmarker.markerpos);
        SetMarkerName(inputmarker.markername);
        SetMarkerSize(inputmarker.markersize);
        SetMarkerColor(inputmarker.fontcolor);
        SetMarkerRotation(inputmarker.markerrotation);
        SetMarkerFontstyles(inputmarker.fontStyles);
    }



    //--------------------------------------------NOT PUBLIC-------------------------------------------


    void SetMarkerPos(Vector3 inputpos)
    {
        Vector3 ZzeroPos;
        ZzeroPos = inputpos;
        ZzeroPos.z = 0.0f;
        marker.markerpos = ZzeroPos;
        this.transform.SetPositionAndRotation(ZzeroPos, this.transform.rotation);
    }
    void SetMarkerName(string name)
    {
        marker.markername = name;
        textmesh.GetComponent<TMPro.TextMeshPro>().SetText(name);
    }
    void SetMarkerSize(float size)
    {
        if (size < 1.0f)
            size = 1.0f;
        marker.markersize = size;

        textmesh.GetComponent<TMPro.TextMeshPro>().fontSize = size;//폰트 사이즈 변경
        Vector3 aftersize;
        aftersize.x = textmesh.GetComponent<TMPro.TextMeshPro>().GetPreferredValues().x;
        aftersize.y = textmesh.GetComponent<TMPro.TextMeshPro>().GetPreferredValues().y;
        aftersize.z = 1.0f;

        textmesh.GetComponent<BoxCollider>().size = aftersize;//클릭 시 선택을 감지하는 박스콜라이더 사이즈 변경
        field.transform.localScale = aftersize;//Field(뒷배경에 빨간 동그라미) 사이즈 변경
    }

    void SetMarkerColor(Color color)
    {
        marker.fontcolor = color;

        textmesh.GetComponent<TMPro.TextMeshPro>().color = marker.fontcolor;
    }

    void SetMarkerRotation(float rotation)
    {
        marker.markerrotation = rotation;

        //this.transform.SetPositionAndRotation(Vector3., new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        this.transform.Rotate(Vector3.forward, marker.markerrotation);


        //textmesh.GetComponent<TMPro.TextMeshPro>().fontStyle
        
    }

    public void SetMarkerFontstyles(int style)
    {
        marker.fontStyles = style;

        textmesh.GetComponent<TMPro.TextMeshPro>().fontStyle = (TMPro.FontStyles)style;
    }
    /*
    void SetMarkerOutline(float thickness)
    {
        markeroutlinesize = thickness;
        textmesh.GetComponent<TMPro.TextMeshPro>().fontSharedMaterial.SetFloat("_OutlineWidth", thickness);
        textmesh.GetComponent<TMPro.TextMeshPro>().UpdateMeshPadding();
    }
    */

}

[System.Serializable]
public class Marker
{
    public string markername;
    public float markersize, markerrotation;
    public Vector3 markerpos;
    public Color fontcolor;
    public int fontStyles;

    public Marker()
    {
        markername = "지명";
        markersize = 10.0f;
        markerrotation = 0.0f;
        markerpos = Vector3.zero;
        fontcolor = Color.white;
        fontStyles = 0;
    }
}