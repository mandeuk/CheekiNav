using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.IO;


public class SaveDownload : MonoBehaviour
{
    static int loopnum;

    private void Start()
    {
        loopnum = 0;

    }

    public void DownloadSavefile()
    {
        StartCoroutine(DownloadFileRoutine());
    }

    /*
    private void FtpDownload(int num)
    {
        string ftpPath = "ftp://eftkor.ipdisk.co.kr/HDD1/Tarkov/기본 지명 세이브 파일/";
        string id = "eft";  // FTP 사용자 로그인
        

        // WebClient 객체 생성
        using (WebClient cli = new WebClient())
        {
            // FTP 사용자 설정
            cli.Credentials = new NetworkCredential(id, id);
            cli.Encoding = System.Text.Encoding.Unicode;


            //switch (num)
            //{
            //    case 0:cli.DownloadFileAsync(new System.Uri(ftpPath + "Chiki.factory")      , GameManager.instance.savedatapath + "/" + "Chiki.factory"     );break;
            //    case 1:cli.DownloadFileAsync(new System.Uri(ftpPath + "Chiki.customs")      , GameManager.instance.savedatapath + "/" + "Chiki.customs"     );break;
            //    case 2:cli.DownloadFileAsync(new System.Uri(ftpPath + "Chiki.interchange")  , GameManager.instance.savedatapath + "/" + "Chiki.interchange" );break;
            //    case 3:cli.DownloadFileAsync(new System.Uri(ftpPath + "Chiki.reserve")      , GameManager.instance.savedatapath + "/" + "Chiki.reserve"     );break;
            //    case 4:cli.DownloadFileAsync(new System.Uri(ftpPath + "Chiki.resort")       , GameManager.instance.savedatapath + "/" + "Chiki.resort"      );break;
            //    case 5:cli.DownloadFileAsync(new System.Uri(ftpPath + "Chiki.shoreline")    , GameManager.instance.savedatapath + "/" + "Chiki.shoreline"   );break;
            //    case 6:cli.DownloadFileAsync(new System.Uri(ftpPath + "Chiki.woods")        , GameManager.instance.savedatapath + "/" + "Chiki.woods"       );break;
            //    case 7:cli.DownloadFileAsync(new System.Uri(ftpPath + "Exits.reserve")      , GameManager.instance.savedatapath + "/" + "Exits.reserve"     );break;
            //    case 8:cli.DownloadFileAsync(new System.Uri(ftpPath + "readme.txt")         , GameManager.instance.savedatapath + "/" + "readme.txt"        );break;
            //}
            switch (num)
            {
                case 0: cli.DownloadFileAsync(new System.Uri("https://drive.google.com/uc?export=download&id=1kPfVxQLpD2cuTTlUEx5N-MfL7X0EyE3t"), GameManager.instance.savedatapath + "/" + "Chiki.factory"); break;
                case 1: cli.DownloadFileAsync(new System.Uri("https://drive.google.com/uc?export=download&id=1noM52C_LY7vf7KbYyM_PzUZM75BEhHqE"), GameManager.instance.savedatapath + "/" + "Chiki.customs"); break;
                case 2: cli.DownloadFileAsync(new System.Uri("https://drive.google.com/uc?export=download&id=1cCCo82Qvdc5I9H9AyW7UfDg87mgNnlHE"), GameManager.instance.savedatapath + "/" + "Chiki.interchange"); break;
                case 3: cli.DownloadFileAsync(new System.Uri("https://drive.google.com/uc?export=download&id=1HZYDlL7O5yP9NRKK1v-bNzDRD0KBBcBk"), GameManager.instance.savedatapath + "/" + "Chiki.reserve"); break;
                case 4: cli.DownloadFileAsync(new System.Uri("https://drive.google.com/uc?export=download&id=1-jv_XxUOf3TAxk5kd7ntJcRCOVAWibo-"), GameManager.instance.savedatapath + "/" + "Chiki.resort"); break;
                case 5: cli.DownloadFileAsync(new System.Uri("https://drive.google.com/uc?export=download&id=1CiP628yH-U79d7DDIWPxoOV5LuVg99GI"), GameManager.instance.savedatapath + "/" + "Chiki.shoreline"); break;
                case 6: cli.DownloadFileAsync(new System.Uri("https://drive.google.com/uc?export=download&id=1y9aSZbuTOsGHFcbictQxfbQtJjqyRVc0"), GameManager.instance.savedatapath + "/" + "Chiki.woods"); break;
                case 7: cli.DownloadFileAsync(new System.Uri("https://drive.google.com/uc?export=download&id=1X7SerKI5FmEvLuqxeH5jMM_5NVxfAvl7"), GameManager.instance.savedatapath + "/" + "Exits.reserve"); break;
                case 8: cli.DownloadFileAsync(new System.Uri("https://drive.google.com/uc?export=download&id=13G6-bZasAaReB3kzmRGdlvZR0C49Csr5"), GameManager.instance.savedatapath + "/" + "readme.txt"); break;
            }
        }
    }
    private IEnumerator DownloadDefaultSavefile()
    {
        if(loopnum < 9)
        {
            FtpDownload(loopnum);
            loopnum++;
            yield return DownloadDefaultSavefile();
        }
        else
        {
            yield break;
        }
        
    }
    */
    IEnumerator DownloadFileRoutine()
    {
        for (int loop = 0; loop < 9; ++loop)
        {
            UnityWebRequest request = null;
            switch (loop)
            {
                case 0: request = UnityWebRequest.Get("ftp://eft:eft@eftkor.ipdisk.co.kr/HDD1/Tarkov/기본 지명 세이브 파일/Chiki.factory"); break;
                case 1: request = UnityWebRequest.Get("ftp://eft:eft@eftkor.ipdisk.co.kr/HDD1/Tarkov/기본 지명 세이브 파일/Chiki.customs"); break;
                case 2: request = UnityWebRequest.Get("ftp://eft:eft@eftkor.ipdisk.co.kr/HDD1/Tarkov/기본 지명 세이브 파일/Chiki.interchange"); break;
                case 3: request = UnityWebRequest.Get("ftp://eft:eft@eftkor.ipdisk.co.kr/HDD1/Tarkov/기본 지명 세이브 파일/Chiki.reserve"); break;
                case 4: request = UnityWebRequest.Get("ftp://eft:eft@eftkor.ipdisk.co.kr/HDD1/Tarkov/기본 지명 세이브 파일/Chiki.resort"); break;
                case 5: request = UnityWebRequest.Get("ftp://eft:eft@eftkor.ipdisk.co.kr/HDD1/Tarkov/기본 지명 세이브 파일/Chiki.shoreline"); break;
                case 6: request = UnityWebRequest.Get("ftp://eft:eft@eftkor.ipdisk.co.kr/HDD1/Tarkov/기본 지명 세이브 파일/Chiki.woods"); break;
                case 7: request = UnityWebRequest.Get("ftp://eft:eft@eftkor.ipdisk.co.kr/HDD1/Tarkov/기본 지명 세이브 파일/Exits.reserve"); break;
                case 8: request = UnityWebRequest.Get("ftp://eft:eft@eftkor.ipdisk.co.kr/HDD1/Tarkov/기본 지명 세이브 파일/readme.txt"); break;
            }

            /*
            Chiki.customs
            Chiki.interchange
            Chiki.reserve
            Chiki.resort
            Chiki.shoreline
            Chiki.woods
            Exits.reserve
            readme.txt
            */

            yield return request.SendWebRequest();
            
            if (request.isNetworkError || request.isHttpError)
            {
                GameManager.instance.UsePopup("오류", "개발자에게 문의해주세요.", request.error);
            }
            else
            {
                switch (loop)
                {
                    case 0: SaveFile(request.downloadHandler.data, "Chiki.factory"); break;
                    case 1: SaveFile(request.downloadHandler.data, "Chiki.customs"); break;
                    case 2: SaveFile(request.downloadHandler.data, "Chiki.interchange"); break;
                    case 3: SaveFile(request.downloadHandler.data, "Chiki.reserve"); break;
                    case 4: SaveFile(request.downloadHandler.data, "Chiki.resort"); break;
                    case 5: SaveFile(request.downloadHandler.data, "Chiki.shoreline"); break;
                    case 6: SaveFile(request.downloadHandler.data, "Chiki.woods"); break;
                    case 7: SaveFile(request.downloadHandler.data, "Exits.reserve"); break;
                    case 8: SaveFile(request.downloadHandler.data, "readme.txt"); break;
                }

            }
            
        }
        yield break;
    }

    void SaveFile(byte[] bytes, string filename)
    {
        string destination = GameManager.instance.savedatapath + "/" + filename;

        if(File.Exists(destination))
        {
            File.Delete(destination);
        }

        File.WriteAllBytes(destination, bytes);
    }

}
