using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MapMarkerManager : MonoBehaviour
{
    public static MapMarkerManager instance;

    //게임 오브젝트
    public GameObject btnMarkerOn, btnMarkerEdit, btnMarkerMove, btnMarkerRemove, btnIconOn, btnCopyOn;
    public TMPro.TMP_InputField savenamefield;

    //마커수정창
    public TMPro.TMP_InputField textinputfield, markersizeinputfield;
    public GameObject textRotation, fontColor;
    public GameObject fontBold, fontItalic, fontUnderline, fontStrikethrough;

    //아이콘 수정창
    public TMPro.TMP_InputField iconsizeinputfield;
    public GameObject iconRotation, iconColor;

    //마킹관련
    Vector3 touchStart;
    static bool markingMode;
    static GameObject selectedMarker;
    static int selectedMarkerType;

    //아이콘관련
    static bool iconMode;

    //이동관련
    public bool movingMode;
    static bool movingOn;

    //복사관련
    static bool copyMode;

    //색상
    static Color32 whitebrush, lightbluebrush;





    void Awake()
    {
#if UNITY_STANDALONE_WIN&&UNITY_EDITOR
        Debug.Log("MapMakerManager.cs PC mode ON");
#endif
#if UNITY_ANDROID&&UNITY_EDITOR
        Debug.Log("MapMakerManager.cs 안드로이드 mode ON");
#endif

        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        markingMode = false;
        iconMode = false;

        movingMode = false;
        movingOn = false;

        whitebrush.r = 255;
        whitebrush.g = 255;
        whitebrush.b = 255;
        whitebrush.a = 255;

        lightbluebrush.r = 120;
        lightbluebrush.g = 120;
        lightbluebrush.b = 255;
        lightbluebrush.a = 255;
    }

    void Update()
    {

#if UNITY_STANDALONE_WIN
        //PC
        //처음 클릭한 지점 저장
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //마우스 포인터가 Raycast 켜진 UI 위에 있는가?
        //이걸 안해주면 UI클릭 시 UI뒤의 게임화면도 같이 클릭되는 현상이 생김
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (markingMode)//새로 마커를 배치할 때
            {
                if (Input.GetMouseButtonDown(0))//마우스 버튼을 누를 때 해당 위치에 생성
                {
                    Vector3 endtouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 distanceVector = touchStart - endtouchPos;
                    float distance = Vector3.Magnitude(distanceVector);

                    if (distance < 0.5f)
                    {
                        SpawnMarker(touchStart);

                        markingMode = false;
                        btnMarkerOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
                    }
                }
            }
            else if (iconMode)//아이콘을 배치할 때
            {
                if (Input.GetMouseButtonDown(0))//마우스 버튼을 누를 때 해당 위치에 생성
                {
                    Vector3 endtouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 distanceVector = touchStart - endtouchPos;
                    float distance = Vector3.Magnitude(distanceVector);

                    if (distance < 0.5f)
                    {
                        SpawnIcon(touchStart);
                        iconMode = false;
                        btnIconOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
                    }
                }
            }
            else if (copyMode && (selectedMarker != null))
            {
                if (Input.GetMouseButtonDown(0))//마우스 버튼을 누를 때 해당 위치에 생성
                {
                    Vector3 endtouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 distanceVector = touchStart - endtouchPos;
                    float distance = Vector3.Magnitude(distanceVector);

                    if (distance < 0.5f)
                    {
                        if (selectedMarker.CompareTag("Marker"))
                        {
                            GameObject marker = SpawnMarker(touchStart);
                            marker.GetComponent<MapMarker>().SetMarker(selectedMarker.transform.parent.GetComponent<MapMarker>().GetMarkerData());
                            marker.GetComponent<MapMarker>().SetMarker(touchStart);
                        }
                        if (selectedMarker.CompareTag("Icon"))
                        {
                            GameObject marker = SpawnIcon(touchStart);
                            marker.GetComponent<MapIcon>().SetIcon(selectedMarker.transform.parent.GetComponent<MapIcon>().GetIconData());
                            marker.GetComponent<MapIcon>().SetIcon(touchStart);
                        }
                        copyMode = false;
                        btnCopyOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
                    }
                }
            }
            else//마커 또는 아이콘을 선택할 때
            {
                if (Input.GetMouseButtonDown(0))//마우스 버튼을 누를 때
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))//충돌체크 박스를 선택했을 때
                    {
                        if (hit.transform.CompareTag("Marker"))//마커선택
                        {
                            if (selectedMarker != null)
                            {
                                selectedMarker.transform.Find("redbox").gameObject.SetActive(false);
                                selectedMarker = hit.transform.gameObject;
                                selectedMarker.transform.Find("redbox").gameObject.SetActive(true);

                            }
                            else
                            {
                                selectedMarker = hit.transform.gameObject;
                                selectedMarker.transform.Find("redbox").gameObject.SetActive(true);
                            }
                            SetEditboxInfo(selectedMarker.transform.parent.GetComponent<MapMarker>().GetMarkerData());
                            selectedMarkerType = 0;
                        }
                        else if (hit.transform.CompareTag("Icon"))//아이콘선택
                        {
                            if (selectedMarker != null)
                            {
                                selectedMarker.transform.Find("redbox").gameObject.SetActive(false);
                                selectedMarker = hit.transform.gameObject;
                                selectedMarker.transform.Find("redbox").gameObject.SetActive(true);
                            }
                            else
                            {
                                selectedMarker = hit.transform.gameObject;
                                selectedMarker.transform.Find("redbox").gameObject.SetActive(true);
                            }
                            SetEditboxInfo(selectedMarker.transform.parent.GetComponent<MapIcon>().GetIconData());
                            selectedMarkerType = 1;
                        }
                    }
                    else//빈 공간을 선택했을 때
                    {
                        if (selectedMarker != null)
                        {
                            selectedMarker.transform.Find("redbox").gameObject.SetActive(false);
                        }
                        selectedMarker = null;

                        if (movingMode)
                        {
                            movingMode = false;
                            movingOn = false;

                            btnMarkerMove.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
                        }
                    }
                }
            }//end of else

            //마커를 움직이려고 할 때
            if (movingMode && (selectedMarker != null))
            {

                if (Input.GetMouseButtonDown(0))//마커를 선택했는지
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))//마우스 위치에 무언가 있는지 확인
                    {
                        if (hit.transform.CompareTag("Marker") || hit.transform.CompareTag("Icon"))//마우스 위치에 있는게 마커인지 확인
                        {
                            movingOn = true;
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0))//마우스 드래그를 끝내고 마우스버튼을 떼면 마커의 이동은 끝나지만 이동모드는 아직 켜진 상태
                {
                    movingOn = false;
                }
                if (movingOn && Input.GetMouseButton(0)) //이동모드가 켜지고 마커가 움직이는 코드
                {
                    Vector3 temppos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    switch (selectedMarkerType)
                    {
                        case 0:
                            selectedMarker.transform.parent.GetComponent<MapMarker>().SetMarker(temppos);
                            break;
                        case 1:
                            selectedMarker.transform.parent.GetComponent<MapIcon>().SetIcon(temppos);
                            break;
                    }
                }
            }

        }
        //end of PC
#endif

#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            //처음 클릭한 지점 저장
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

            //마우스 포인터가 Raycast 켜진 UI 위에 있는가?
            //이걸 안해주면 UI클릭 시 UI뒤의 게임화면도 같이 클릭되는 현상이 생김
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (markingMode)//새로 마커를 놓을 때
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)//마우스 버튼을 누를 때 해당 위치에 마커 생성
                    {
                        Vector3 endtouchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        Vector3 distanceVector = touchStart - endtouchPos;
                        float distance = Vector3.Magnitude(distanceVector);
                        if (distance < 0.5f)
                        {
                            SpawnMarker(touchStart);
                            markingMode = false;
                            btnMarkerOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
                        }
                    }
                }
                else if (iconMode)//아이콘을 배치할 때
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)//마우스 버튼을 누를 때 해당 위치에 생성
                    {
                        Vector3 endtouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector3 distanceVector = touchStart - endtouchPos;
                        float distance = Vector3.Magnitude(distanceVector);

                        if (distance < 0.5f)
                        {
                            SpawnIcon(touchStart);
                            iconMode = false;
                            btnIconOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
                        }
                    }
                }
                else if (copyMode && (selectedMarker != null))
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)//마우스 버튼을 누를 때 해당 위치에 생성
                    {
                        Vector3 endtouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector3 distanceVector = touchStart - endtouchPos;
                        float distance = Vector3.Magnitude(distanceVector);

                        if (distance < 0.5f)
                        {
                            if (selectedMarker.CompareTag("Marker"))
                            {
                                GameObject marker = SpawnMarker(touchStart);
                                marker.GetComponent<MapMarker>().SetMarker(selectedMarker.transform.parent.GetComponent<MapMarker>().GetMarkerData());
                                marker.GetComponent<MapMarker>().SetMarker(touchStart);
                            }
                            if (selectedMarker.CompareTag("Icon"))
                            {
                                GameObject marker = SpawnIcon(touchStart);
                                marker.GetComponent<MapIcon>().SetIcon(selectedMarker.transform.parent.GetComponent<MapIcon>().GetIconData());
                                marker.GetComponent<MapIcon>().SetIcon(touchStart);
                            }
                            copyMode = false;
                            btnCopyOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
                        }
                    }
                }
                else//마커를 선택할 때
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)//마우스 버튼을 누를 때
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))//충돌체크 박스를 선택했을 때
                        {
                            if (hit.transform.CompareTag("Marker"))//마커선택
                            {
                                if (selectedMarker != null)
                                {
                                    selectedMarker.transform.Find("redbox").gameObject.SetActive(false);
                                    selectedMarker = hit.transform.gameObject;
                                    selectedMarker.transform.Find("redbox").gameObject.SetActive(true);

                                }
                                else
                                {
                                    selectedMarker = hit.transform.gameObject;
                                    selectedMarker.transform.Find("redbox").gameObject.SetActive(true);
                                }
                                SetEditboxInfo(selectedMarker.transform.parent.GetComponent<MapMarker>().GetMarkerData());
                                selectedMarkerType = 0;
                            }
                            else if (hit.transform.CompareTag("Icon"))//아이콘선택
                            {
                                if (selectedMarker != null)
                                {
                                    selectedMarker.transform.Find("redbox").gameObject.SetActive(false);
                                    selectedMarker = hit.transform.gameObject;
                                    selectedMarker.transform.Find("redbox").gameObject.SetActive(true);
                                }
                                else
                                {
                                    selectedMarker = hit.transform.gameObject;
                                    selectedMarker.transform.Find("redbox").gameObject.SetActive(true);
                                }
                                SetEditboxInfo(selectedMarker.transform.parent.GetComponent<MapIcon>().GetIconData());
                                selectedMarkerType = 1;
                            }
                        }
                        else//빈 공간을 선택했을 때
                        {
                            if (selectedMarker != null)
                            {
                                selectedMarker.transform.Find("redbox").gameObject.SetActive(false);
                            }
                            selectedMarker = null;

                            if (movingMode)
                            {
                                movingMode = false;
                                movingOn = false;

                                btnMarkerMove.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
                            }
                        }
                        /*
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.transform.name == "Text")
                            {
                                if (selectedMarker != null)
                                {
                                    selectedMarker.transform.Find("redbox").gameObject.SetActive(false);
                                    selectedMarker = hit.transform.gameObject;
                                    selectedMarker.transform.Find("redbox").gameObject.SetActive(true);
                                    SetEditboxInfo(selectedMarker.transform.parent.GetComponent<MapMarker>().GetMarkerData());
                                }
                                else
                                {
                                    selectedMarker = hit.transform.gameObject;
                                    selectedMarker.transform.Find("redbox").gameObject.SetActive(true);
                                    SetEditboxInfo(selectedMarker.transform.parent.GetComponent<MapMarker>().GetMarkerData());
                                }
                            }
                        }
                        else
                        {
                            if (selectedMarker != null)
                            {
                                selectedMarker.transform.Find("redbox").gameObject.SetActive(false);
                            }
                            selectedMarker = null;


                            if (movingMode)
                            {
                                movingMode = false;
                                movingOn = false;

                                btnMarkerMove.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
                            }
                        }
                        */
                    }
                }//end of else

                //마커를 움직이려고 할 때
                if (movingMode && (selectedMarker != null))
                {

                    if (Input.GetTouch(0).phase == TouchPhase.Began)//마커를 선택했는지
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))//마우스 위치에 무언가 있는지 확인
                        {
                            if (hit.transform.CompareTag("Marker") || hit.transform.CompareTag("Icon"))//마우스 위치에 있는게 마커인지 확인
                            {
                                movingOn = true;
                            }
                        }
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)//마우스 드래그를 끝내고 마우스버튼을 떼면 마커의 이동은 끝나지만 이동모드는 아직 켜진 상태
                    {
                        movingOn = false;
                    }
                    if (movingOn && (Input.GetTouch(0).phase == TouchPhase.Moved))//이동모드가 켜지고 마커가 움직이는 코드
                    {
                        Vector3 temppos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        switch (selectedMarkerType)
                        {
                            case 0:
                                selectedMarker.transform.parent.GetComponent<MapMarker>().SetMarker(temppos);
                                break;
                            case 1:
                                selectedMarker.transform.parent.GetComponent<MapIcon>().SetIcon(temppos);
                                break;
                        }
                        //selectedMarker.transform.parent.SetPositionAndRotation(temppos, qzero);//new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                        //selectedMarker.transform.parent.GetComponent<MapMarker>().SetMarker(temppos);
                    }
                }

            }
        }
        // end of 모바일
#endif

    }//end of Update()





    public GameObject SpawnMarker(Vector3 inputpos)
    {
        GameObject marker = Instantiate(Resources.Load("Prefabs/Marker2"), this.transform) as GameObject;

        //Vector3 spawnpos;
        //spawnpos.x = inputpos.x;
        //spawnpos.y = inputpos.y;
        //spawnpos.z = 0.0f;

        marker.GetComponent<MapMarker>().SetMarker(inputpos, "지명", 10.0f);

        return marker;
    }

    public void SpawnMarkerForLoad(Marker mymarker)//MarkerData
    {
        GameObject marker = Instantiate(Resources.Load("Prefabs/Marker2"), this.transform) as GameObject;
        marker.GetComponent<MapMarker>().SetMarker(mymarker);
    }

    public GameObject SpawnIcon(Vector3 inputpos)
    {
        GameObject icon = Instantiate(Resources.Load("Prefabs/Icon"), this.transform) as GameObject;

        icon.GetComponent<MapIcon>().SetIcon(inputpos);

        return icon;
    }

    public void SpawnIconForLoad(Icon myicon)
    {
        GameObject marker = Instantiate(Resources.Load("Prefabs/Icon"), this.transform) as GameObject;
        marker.GetComponent<MapIcon>().SetIcon(myicon);
    }





    public void MarkingMode()
    {
        if (markingMode == true)
        {
            markingMode = false;
            btnMarkerOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
        }
        else if (markingMode == false)
        {
            markingMode = true;
            btnMarkerOn.GetComponent<UnityEngine.UI.Image>().color = lightbluebrush;
        }
        movingMode = false;
        iconMode = false;
        copyMode = false;
        btnMarkerMove.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
        btnIconOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
        btnCopyOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
    }

    public void SetMarker()
    {
        if (selectedMarker != null)
        {
            if (selectedMarker.CompareTag("Marker"))
            {
                float textsize;
                if (textinputfield.text != "")//지명변경
                {
                    selectedMarker.transform.parent.GetComponent<MapMarker>().SetMarker(textinputfield.text);
                }
                if (float.TryParse(markersizeinputfield.text, out textsize))//폰트사이즈 변경
                {
                    selectedMarker.transform.parent.GetComponent<MapMarker>().SetMarker("size", textsize);
                }

                //색상
                //if(fontColor.GetComponent<Pallet>().GetColor() != null)
                {
                    selectedMarker.transform.parent.GetComponent<MapMarker>().SetMarker(fontColor.GetComponent<Pallet>().GetColor());
                }
                //폰트스타일
                {
                    int style = 0;

                    if (fontBold.GetComponent<UnityEngine.UI.Toggle>().isOn)
                    {
                        style += 1;//TMPro.FontStyle.Bold = 1;
                    }
                    if (fontItalic.GetComponent<UnityEngine.UI.Toggle>().isOn)
                    {
                        style += 2;//TMPro.FontStyle.Italic = 2;
                    }
                    if (fontUnderline.GetComponent<UnityEngine.UI.Toggle>().isOn)
                    {
                        style += 4;//TMPro.FontStyle.Underline = 4;
                    }
                    if (fontStrikethrough.GetComponent<UnityEngine.UI.Toggle>().isOn)
                    {
                        style += 64;//TMPro.FontStyle.Strikethrough = 64;
                    }

                    selectedMarker.transform.parent.GetComponent<MapMarker>().SetMarker("style", style);
                }
                //회전
                {
                    selectedMarker.transform.parent.GetComponent<MapMarker>().SetMarker("rotation", textRotation.GetComponent<InputRotationField>().GetRotationValue());
                }
            }
        }
        else
        {
            //Debug.LogError("선택된 마커가 없습니다.");
        }
    }

    public void MovingMode()
    {
        if (movingMode == true)
        {
            movingMode = false;
            btnMarkerMove.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
        }
        else if (movingMode == false)
        {
            movingMode = true;
            btnMarkerMove.GetComponent<UnityEngine.UI.Image>().color = lightbluebrush;
        }
        markingMode = false;
        iconMode = false;
        copyMode = false;
        btnMarkerOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
        btnIconOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
        btnCopyOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
    }

    public void CopyMode()
    {
        if (copyMode == true)
        {
            copyMode = false;
            btnCopyOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
        }
        else if (copyMode == false)
        {
            copyMode = true;
            btnCopyOn.GetComponent<UnityEngine.UI.Image>().color = lightbluebrush;
        }
        movingMode = false;
        markingMode = false;
        iconMode = false;
        btnMarkerMove.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
        btnMarkerOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
        btnIconOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
    }

    public void IconMode()
    {
        if (iconMode == true)
        {
            iconMode = false;
            btnIconOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
        }
        else if (iconMode == false)
        {
            iconMode = true;
            btnIconOn.GetComponent<UnityEngine.UI.Image>().color = lightbluebrush;
        }
        markingMode = false;
        movingMode = false;
        copyMode = false;
        btnMarkerOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
        btnMarkerMove.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
        btnCopyOn.GetComponent<UnityEngine.UI.Image>().color = whitebrush;
    }

    public void SetIcon()
    {
        if (selectedMarker != null)
        {
            if (selectedMarker.CompareTag("Icon"))
            {
                float iconsize;

                //아이콘사이즈 변경
                if (float.TryParse(iconsizeinputfield.text, out iconsize))
                {
                    iconsize /= 10.0f;
                    selectedMarker.transform.parent.GetComponent<MapIcon>().SetIcon("size", iconsize);
                }

                //회전
                selectedMarker.transform.parent.GetComponent<MapIcon>().SetIcon("rotation", iconRotation.GetComponent<InputRotationField>().GetRotationValue());

                //색상
                selectedMarker.transform.parent.GetComponent<MapIcon>().SetIcon(iconColor.GetComponent<Pallet>().GetColor());
            }
        }
    }





    public void SetIconSprite(Sprite spriteimg)
    {
        if (selectedMarker.CompareTag("Icon"))
        {
            selectedMarker.transform.parent.GetComponent<MapIcon>().SetIcon(spriteimg.name);
        }
    }



    public void SaveMarkerData()
    {
        string fileName = savenamefield.text;
        string path;
        string json;

        if (fileName.Length == 0)
            path = GameManager.instance.savedatapath + "/noname" + "." + GameManager.instance.selectedmap;
        else
            path = GameManager.instance.savedatapath + "/" + fileName + "." + GameManager.instance.selectedmap;


        System.IO.FileStream filestream = new System.IO.FileStream(path, System.IO.FileMode.Create);

        List<Marker> mList = new List<Marker>();
        List<Icon> iList = new List<Icon>();

        int loop = transform.childCount;
        Transform temptransform;
        PackedMarkerData pmData = new PackedMarkerData();


        for (int i = 0; i < loop; i++)
        {
            temptransform = this.transform.GetChild(i);

            if (temptransform.CompareTag("Marker"))
            {
                Marker mdata = new Marker();
                mdata = temptransform.GetComponent<MapMarker>().GetMarkerData();
#if UNITY_EDITOR
                Debug.Log("Save Text : " + mdata.markername + ", Pos : " + mdata.markerpos + ", Size : " + mdata.markersize + ", Color : " + mdata.fontcolor);
#endif
                mList.Add(mdata);
            }
            else if (temptransform.CompareTag("Icon"))
            {
                Icon Idata = new Icon();
                Idata = temptransform.GetComponent<MapIcon>().GetIconData();
#if UNITY_EDITOR
                Debug.Log("Pos : " + Idata.iconpos + ", Size : " + Idata.iconsize + ", Color : " + Idata.iconcolor);
#endif
                iList.Add(Idata);
            }
        }


        pmData.markerPackage = mList.ToArray();
        pmData.iconPackage = iList.ToArray();
        pmData.selectedmap = GameManager.instance.selectedmap;
        json = JsonUtility.ToJson(pmData);


        using (System.IO.StreamWriter swriter = new System.IO.StreamWriter(filestream))
        {
            swriter.Write(json);
        }

    }//end of SaveMarkerData(string fileName)
    public void LoadMarkerData(string fileName)
    {
        if (fileName == "선택안함")
            return;

        string path;

        if (fileName.Length == 0)
            path = GameManager.instance.savedatapath + "/default" + "." + GameManager.instance.selectedmap;
        else
            path = GameManager.instance.savedatapath + "/" + fileName;


        string json;
        Marker mdata = new Marker();
        PackedMarkerData pmData = new PackedMarkerData();



        if (System.IO.File.Exists(path))
        {
            using (System.IO.StreamReader sreader = new System.IO.StreamReader(path))
            {
                json = sreader.ReadToEnd();
                pmData = JsonUtility.FromJson<PackedMarkerData>(json);

                int loop = pmData.markerPackage.Length;
                for (int i = 0; i < loop; i++)
                {
                    SpawnMarkerForLoad(pmData.markerPackage[i]);
                }

                loop = pmData.iconPackage.Length;
                for (int i = 0; i < loop; i++)
                {
                    SpawnIconForLoad(pmData.iconPackage[i]);
                }
            }
        }
        else
        {
            //Debug.LogWarning("파일이 존재하지 않습니다");
        }

    }//end of LoadMarkerData(string fileName)



    public void DeleteMarker()
    {
        if (selectedMarker != null)
        {
            Destroy(selectedMarker.transform.parent.gameObject);
            selectedMarker = null;
        }
    }
    public void RemoveAllMarker()
    {
        if (this.transform.childCount > 0)
        {
            for (int i = this.transform.childCount; i > 0; --i)
            {
                GameObject.Destroy(this.transform.GetChild(i - 1).gameObject);
            }
        }
    }

    void SetEditboxInfo(Marker inputmarker)
    {
        textinputfield.text = inputmarker.markername;//지명
        markersizeinputfield.text = inputmarker.markersize.ToString();//글자크기
        textRotation.GetComponent<TMPro.TMP_InputField>().text = inputmarker.markerrotation.ToString();//회전
        textRotation.GetComponent<InputRotationField>().SetRotationValue(inputmarker.markerrotation);//회전내부저장값도 변경
        fontColor.GetComponent<Pallet>().ChangeSlider(inputmarker.fontcolor);//글자색상
        //옵션들
        fontBold.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        fontItalic.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        fontUnderline.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        fontStrikethrough.GetComponent<UnityEngine.UI.Toggle>().isOn = false;

        switch (inputmarker.fontStyles)
        {
            case 0:
                fontBold.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                fontItalic.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                fontUnderline.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                fontStrikethrough.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
                break;
            case 1://Bold
                fontBold.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;
            case 3://Bold + Italic
                fontBold.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontItalic.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;
            case 5://Bold + Underline
                fontBold.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontUnderline.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;
            case 65://Bold + Strikethrough
                fontBold.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontStrikethrough.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;

            case 7://Bold + Italic + Underline
                fontBold.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontItalic.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontUnderline.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;
            case 67://Bold + Italic + Strikethrough
                fontBold.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontItalic.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontStrikethrough.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;
            case 71://Bold + Italic + Underline + Strikethrough
                fontBold.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontItalic.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontUnderline.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontStrikethrough.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;

            case 69://Bold + Underline + Strikethrough
                fontBold.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontUnderline.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontStrikethrough.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;


            case 2://Italic
                fontItalic.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;
            case 6://Italic + Underline
                fontItalic.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontUnderline.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;
            case 66://Italic + Strikethrough
                fontItalic.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontStrikethrough.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;
            case 70://Italic + Underline + Strikethrough
                fontItalic.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontUnderline.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontStrikethrough.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;


            case 4://Underline
                fontUnderline.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;
            case 68://Underline + Strikethrough
                fontUnderline.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                fontStrikethrough.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;


            case 64://Strikethrough
                fontStrikethrough.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                break;

            default:
                Debug.LogWarning("MapMarkerManager.cs의 폰트옵션 Switch문에서 예외상황 발생");
                break;
        }//end of switch

    }//end of SetEditboxInfo(Marker inputmarker)
    void SetEditboxInfo(Icon inputicon)
    {
        iconsizeinputfield.text = (inputicon.iconsize * 10.0f).ToString();

        iconColor.GetComponent<Pallet>().ChangeSlider(inputicon.iconcolor);

        iconRotation.GetComponent<TMPro.TMP_InputField>().text = inputicon.iconrotation.ToString();
        iconRotation.GetComponent<InputRotationField>().SetRotationValue(inputicon.iconrotation);//회전내부저장값도 변경
    }
}


[System.Serializable]
public class PackedMarkerData
{
    public string selectedmap;
    public Marker[] markerPackage;
    public Icon[] iconPackage;
}