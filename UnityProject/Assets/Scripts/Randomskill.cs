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
    private Image img;         // 圖片元件
    private Button btn;        // 按鈕元件
    private AudioSource aud;   // 音源元件
    private Text textskill;    // 技能名稱

    private void Start()
    {
        img = GetComponent<Image>();        //抓到圖片元件(才可使用)
        btn = GetComponent<Button>();
        aud = GetComponent<AudioSource>();
        textskill = transform.GetChild(0).GetComponent<Text>();  //變形.取得子物件(編號)

        StartCoroutine(ScrollEffect()); //啟動協程
    }

    //定義協程方法 捲動效果
    private IEnumerator ScrollEffect()  //IEnumerator是一個等待的語法，普通的寫等待可能會讓所有程式變成等待狀態，此與法是在主線分出分支等待(不會影響其他) Scro~~~ct是名稱可隨己取名
    {
        btn.interactable = false; //按鈕無法點選(使用後老虎機在轉的時候才能不讓人點選)

        //迴圈
        for (int j = 0; j < count; j++)
        {
            for (int i = 0; i < spritesBlurs.Length; i++)
            {
                img.sprite = spritesBlurs[i];            // 圖片元件.圖片 = 模糊圖片陣列[編號]
                aud.PlayOneShot(soundScroll, 0.2f);      // play~~~ot是指播放音效 (指定的音效項目名稱, 音量)
                yield return new WaitForSeconds(speed);  // 等待
            }
        }

        int r = Random.Range(0, spritesSkills.Length); // 隨機挑選 技能圖片陣列
        img.sprite = spritesSkills[r];                 // 圖片元件.圖片 = 技能圖片陣列[隨機值]
        aud.PlayOneShot(soundGetSkill, 0.8f);
        textskill.text = nameSkills[r];                // 技能名稱.文字 = 技能名稱[隨機值]

        btn.interactable = true;        // 按鈕可以點選
    }
}
