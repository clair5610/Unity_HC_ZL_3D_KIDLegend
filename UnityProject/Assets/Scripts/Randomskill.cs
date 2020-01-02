using UnityEngine;
using UnityEngine.UI; // 引用介面 API
using System.Collections; // 因IEnumerator包含在這裡面，因此要先寫他IEnumerator才有效

public class Randomskill : MonoBehaviour
{
    [Header("技能圖片:模糊與正常")]   // 在Unity裡增加欄位
    public Sprite[] spritesBlurs;     // 模糊塗片陣列 16
    public Sprite[] spritesSkills;    // 技能圖片陣列 8

    [Header("捲動速度"), Range(0.00001f, 3)]
    public float speed = 0.1f;

    [Header("捲動次數"), Range(1, 10)]  //捲動次數在1~10之間，可在Unity中調整
    public int count = 5;

    [Header("音效")]
    public AudioClip soundScroll;    //捲動音效(將抓好的音效放入)
    public AudioClip soundGetSkill;  //確認技能

    private string[] nameSkills = { "連續射擊", "正向箭", "背向箭", "側向箭", "血量提升", "攻擊提升", "攻速提升", "爆擊提升" };
    // 讓文字可對應轉出的圖片更換，第一個對應圖1，然後2對2、3對3...輪迴
    private Image img;                // 圖片元件
    private Button btn;               // 按鈕元件
    private AudioSource aud;          // 音源元件
    private Text textskill;           // 技能名稱
    private GameObject panelSkill;    // 隨機技能物件
    private int index;                // 隨機技能編號

    private void Start()
    {
        img = GetComponent<Image>();        // 抓到圖片元件(才可使用)
        btn = GetComponent<Button>();
        aud = GetComponent<AudioSource>();
        textskill = transform.GetChild(0).GetComponent<Text>();  // 變形.取得子物件(編號)
        panelSkill = GameObject.Find("隨機技能");                // 取得遊戲物件(Unity中的"隨機技能")

        btn.onClick.AddListener(chooseSkill);                    // 玩家點擊按鈕 執行監聽者

        StartCoroutine(ScrollEffect()); //啟動協程
    }

    /// <summary>
    /// 選擇技能後 : 隱藏隨機技能物件
    /// </summary>
    private void chooseSkill()
    {
        panelSkill.SetActive(false);                      // SetActive是指隨機技能最上方的(有立方體的那塊)區域，在上方監聽到玩家點擊後，將選擇技能的畫面關閉(隱藏隨機技能物件)
        print("玩家選取技能為:" + nameSkills[index]);     // 紀錄玩家選的技能
    }

    //定義協程方法 捲動效果
    /// <summary>
    /// 捲動效果
    /// </summary>
    /// <returns></returns>
    private IEnumerator ScrollEffect()  // IEnumerator是一個等待的語法，普通的寫等待可能會讓所有程式變成等待狀態，此與法是在主線分出分支等待(不會影響其他) Scro~~~ct是名稱可隨己取名
    {
        btn.interactable = false; //按鈕無法點選(使用後老虎機在轉的時候才能不讓人點選)

        //迴圈
        for (int j = 0; j < count; j++)
        {
            for (int i = 0; i < spritesBlurs.Length; i++)
            {
                img.sprite = spritesBlurs[i];            // 圖片元件.圖片 = 模糊圖片陣列[編號]
                aud.PlayOneShot(soundScroll, 0.2f);      // play~~~ot是指播放音效 (指定的音效項目名稱, 音量)
                yield return new WaitForSeconds(speed);  // 初始圖片到結尾圖片的等待
            }
        }

        // Ctrl+r+r 可將選取的詞的同類全數選取並一齊進行更改 

        index = Random.Range(0, spritesSkills.Length);     // 隨機挑選 技能圖片陣列
        img.sprite = spritesSkills[index];                 // 圖片元件.圖片 = 技能圖片陣列[隨機值]
        aud.PlayOneShot(soundGetSkill, 0.8f);
        textskill.text = nameSkills[index];                // 技能名稱.文字 = 技能名稱[隨機值]

        btn.interactable = true;        // 按鈕可以點選
    }
}
