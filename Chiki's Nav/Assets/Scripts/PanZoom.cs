using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    public GameObject mapObject;

    Vector3 touchStart, direction, camdirection;
    Vector3 ztouchStart, zdirection;

    public float zoomOutMin = 0.1f;
    public float zoomOutMax = 30.0f;

    //for 모바일 줌인줌아웃
    Touch touchZero, touchOne;
    Vector2 touchZeroPrevPos, touchOnePrevPos;
    float prevMagnitude, currentMagnitude, difference;

    private void Awake()
    {
#if UNITY_STANDALONE_WIN&&UNITY_EDITOR
        Debug.Log("PanZoom.cs PC mode ON");
#endif
#if UNITY_ANDROID&&UNITY_EDITOR
        Debug.Log("PanZoom.cs 안드로이드 mode ON");
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (MapMarkerManager.instance.movingMode == false)
            {

#if UNITY_STANDALONE_WIN
                //PC 카메라 이동
                if (Input.GetMouseButtonDown(0))
                {
                    touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                if (Input.GetMouseButton(0))
                {
                    direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Camera.main.transform.position += direction;
                }

                //PC 마우스휠 줌인줌아웃
                zoom(Input.GetAxis("Mouse ScrollWheel"));

                //end of PC 카메라 이동
                
#endif

#if UNITY_ANDROID
                //모바일 카메라 이동
                if (Input.touchCount == 1)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        touchStart = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    }
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        direction = touchStart - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        Camera.main.transform.position += direction;
                    }

                }//end of if(터치카운트 1)



                //모바일 줌인줌아웃
                if (Input.touchCount == 2)
                {
                    //줌과 동시에 카메라 이동1
                    if ((Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetTouch(1).phase == TouchPhase.Began))
                        ztouchStart = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position + (Input.GetTouch(1).position - Input.GetTouch(0).position));

                    //동시터치 도중 touch(0)손가락을 떼면 touch(1)이 0 이되며 좌표가 바뀌고 화면이 확 이동한다.
                    //이 문제를 해결하기위해 동시터치 도중 0번 손가락을 뗄 때 좌표를 보정해준다.
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        touchStart = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
                    }
                    else if (Input.GetTouch(1).phase == TouchPhase.Ended)
                    {
                        touchStart = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    }
                    else
                    {
                        touchZero = Input.GetTouch(0);
                        touchOne = Input.GetTouch(1);

                        touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                        touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                        prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                        currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                        difference = currentMagnitude - prevMagnitude;
                        zoom(difference * 0.005f);


                        //줌과 동시에 카메라 이동2
                        zdirection = ztouchStart - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position + (Input.GetTouch(1).position - Input.GetTouch(0).position));
                        Camera.main.transform.position += zdirection;
                    }
                }//end of if(터치카운트 2)

                //end of 모바일
#endif

            }//if 마커이동모드가 off인가?

            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
        }//end of if

        if(isCameraLocatedOutside())
        {
            camdirection = -(Camera.main.transform.position / 100.0f);
            camdirection.z = 0.0f;
            Camera.main.transform.position += camdirection;
        }
    }//end of Update()

    void zoom(float increment)
    {
        float size = Camera.main.orthographicSize;
        if (size < 1.0f)
            Camera.main.orthographicSize = Mathf.Clamp(size - increment, zoomOutMin, zoomOutMax);
        else
            Camera.main.orthographicSize = Mathf.Clamp(size - (increment * size), zoomOutMin, zoomOutMax);
    }


    bool isCameraLocatedOutside()
    {
        if(Camera.main.transform.position.x < -(mapObject.GetComponent<SpriteRenderer>().sprite.rect.width/200))
        {
            return true;
        }
        else if (Camera.main.transform.position.x > (mapObject.GetComponent<SpriteRenderer>().sprite.rect.width / 200))
        {
            return true;
        }
        if (Camera.main.transform.position.y < -(mapObject.GetComponent<SpriteRenderer>().sprite.rect.height/200))
        {
            return true;
        }
        else if (Camera.main.transform.position.y > (mapObject.GetComponent<SpriteRenderer>().sprite.rect.height/200))
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

}
