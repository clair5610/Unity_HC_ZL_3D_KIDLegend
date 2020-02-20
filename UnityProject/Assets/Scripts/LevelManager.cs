using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public GameObject skill; 
    public GameObject ObjLight;

    [Header("是否自動顯示技能")]
    public bool autoShowSkill;   // 是否顯示技能
    [Header("是否自動開門")]
    public bool autoOpenDoor;    // 是否自動開門
    [Header("復活畫面，看廣告復活按鈕")]
    public GameObject panelRevival;
    public Button btnRevival;

    private Animator aniDoor;     // 門(動畫)
    private Image imgCross;       // 轉場
    private AdManager adManager;  // 廣告管理器

    private void Start()
    {
        //GameObject.Find("") 無法找到隱藏的物件,因此技能跟光直接丟到遊戲管理器的欄位裡
        aniDoor = GameObject.Find("門").GetComponent<Animator>();

        imgCross = GameObject.Find("轉場白").GetComponent<Image>();

        // 如果 是 顯示技能 呼叫 顯示技能
        if (autoShowSkill) ShowSkill();
        // 如果 是 自動開門 延遲呼叫 開門方法
        //if (autoOpenDoor) Invoke("OpenDoor", 6);

        // 延遲調用("方法名稱"， 延遲時間)
        // Invoke("OpenDoor", 6);

        // 重複調用("方法名稱"，延遲時間，重複頻率)
        // InvokeRepeating("OpenDoor", 0, 1.5f);

        adManager = FindObjectOfType<AdManager>();                 // 透過類行尋找物件<廣告管理器>
        btnRevival.onClick.AddListener(adManager.ShowADRevival);   // 按鈕.點及.增加監聽者(廣告管理器.顯示復活廣告)

    }

    /// <summary>
    ///  顯示技能
    /// </summary>
    private void ShowSkill()
    {
        skill.SetActive(true);
    }

    /// <summary>
    /// 開門、光照
    /// </summary>
    private void OpenDoor()
    {
        ObjLight.SetActive(true);
        aniDoor.SetTrigger("開門觸發");
    }
    /// <summary>
    /// 載入下一關
    /// </summary>
    /// <returns></returns>
    public IEnumerator NextLevel()
    {
        print("載入下一關");

        // 迴圈
        for (int i = 0; i < 100; i++)
        {
            imgCross.color += new Color(0, 0, 0, 0.01f); // 轉場.顏色 += new Color( r, g, b, a(透明度)
            yield return new WaitForSeconds(0.005f);        // 等待0.005秒
        }

        yield return new WaitForSeconds(0.2f);

        if (SceneManager.GetActiveScene().name.Contains("魔王"))
        {
            // 載入下一關
            SceneManager.LoadScene(0);
        }
        else
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++index);
        }
        
    }
    /// <summary>
    /// 顯示復活畫面
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowRevival()
    {
        panelRevival.SetActive(true);
        Text textSecond = panelRevival.transform.GetChild(1).GetComponent<Text>();

        for (int i = 3; i > 0; i--)               // 迴圈從 3 跑到 1
        {
            textSecond.text = i.ToString();       // 更新秒數
            yield return new WaitForSeconds(1);   // 等待1秒
        }
    }
    /// <summary>
    /// 關閉復活畫面
    /// </summary>
    public void HideRevival()
    {
        StopCoroutine(ShowRevival());   // 停止協程
        panelRevival.SetActive(false);  // 隱藏
    }

    public void Pass()
    {
        OpenDoor();

        Item[] items = FindObjectsOfType<Item>();    // 取得所有道具

        for (int i = 0; i < items.Length; i++)       // 迴圈跑每一個道具
        {
            items[i].pass = true;                    // 每個道具.過關 = 勾選
        }
    }
}
