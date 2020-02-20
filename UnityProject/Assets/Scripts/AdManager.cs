using UnityEngine;
using UnityEngine.Advertisements;  // 引用 廣告 API

// C# 繼承僅限一個
// C# 介面無限多個 : 裝備，裝上裝備後可以擁有裝備的功能(方法)
// 介面 interface，介面都是 I(大寫) 開頭
// IUnityAdsListener 廣告監聽者 : 監聽玩家看廣告的行為，例如 : 失敗、略過或成功
public class AdManager : MonoBehaviour,IUnityAdsListener
{
    private string googleID = "3459590";             // Google Play 廣告 ID
    private string placementRevival = "Revival";     // 廣告名稱
    private player player;

    private void Start()
    {
        Advertisement.Initialize(googleID, false);    // 廣告.初始化(廣告 ID, 是否開啟測試)
        Advertisement.AddListener(this);             // 廣告.增加監聽者(此腳本)
        player = FindObjectOfType<player>();
    }

    /// <summary>
    /// 顯示復活廣告
    /// </summary>
    public void ShowADRevival()
    {
        if (Advertisement.IsReady(placementRevival))    // 如果 廣告準備完成(廣告名稱)
        {
            Advertisement.Show(placementRevival);       // 顯示廣告(廣告名稱)
        }
    }

    // 廣告準備完成
    public void OnUnityAdsReady(string placementId)
    {
    }
    // 廣告錯誤
    public void OnUnityAdsDidError(string message)
    {
    }
    // 廣告開始
    public void OnUnityAdsDidStart(string placementId)
    {
    }
    // 廣告完成
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == placementRevival)    // 如果 目前廣告 = 復活廣告
        {
            switch (showResult)                 // switch 判斷式
            {
                case ShowResult.Failed:         // 第一種可能
                    // print("失敗");
                    break;
                case ShowResult.Skipped:        // 第一種可能
                    // print("略過");
                    break;
                case ShowResult.Finished:       // 第一種可能
                    // print("完全");
                    GameObject.Find("女孩").GetComponent<player>().Revival();
                    break;
            }
        }
    }
}
