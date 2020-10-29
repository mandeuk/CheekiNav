using UnityEngine;
using System;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string selectedmap, savedatapath;

    public GameObject exitmenu, popup;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        selectedmap = "factory";

        //과도한 연산을 제한하는 코드 (frame rate limitation)
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;


#if UNITY_STANDALONE_WIN
        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(docpath + "/Chiki's Nav/Savefile");
        if (di.Exists == false)
            di.Create();
        savedatapath = docpath + "/Chiki's Nav/Savefile";
#endif
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo("/mnt/sdcard/Download/ChikiNav/Savefile");
        if (di.Exists == false)
            di.Create();

        savedatapath = "/mnt/sdcard/Download/ChikiNav/Savefile";
#endif
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))//프로그램 종료
        {
            ToggleExitmenu();
        }
#if UNITY_STANDALONE_WIN
        else if (Input.GetKeyDown(KeyCode.F1))
        {
            MapMarkerManager.instance.MarkingMode();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            MapMarkerManager.instance.IconMode();
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            MapMarkerManager.instance.MovingMode();
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            MapMarkerManager.instance.CopyMode();
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            MapMarkerManager.instance.DeleteMarker();
        }
#endif
    }

    public void ExitProgram()
    {
        Application.Quit();
    }

    public void ToggleExitmenu()
    {
        exitmenu.SetActive(!exitmenu.activeInHierarchy);
    }

    public void UsePopup(string title, string subtitle, string description)
    {
        popup.SetActive(true);
        popup.GetComponent<PopupScreen>().TurnonPopupScreen(title, subtitle, description);

    }
}
