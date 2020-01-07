using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject skill; 
    public GameObject ObjLight;

    [Header("是否自動顯示技能")]
    public bool autoShowSkill;   // 是否顯示技能
    [Header("是否自動開門")]
    public bool autoOpenDoor;    // 是否自動開門

    private Animator aniDoor;    // 門(動畫)

    private void Start()
    {
        //GameObject.Find("") 無法找到隱藏的物件,因此技能跟光直接丟到遊戲管理器的欄位裡
        aniDoor = GameObject.Find("門").GetComponent<Animator>();

        // 如果 是 顯示技能 呼叫 顯示技能
        if (autoShowSkill) ShowSkill();
        // 如果 是 自動開門 延遲呼叫 開門方法
        if (autoOpenDoor) Invoke("OpenDoor", 6);

        // 延遲調用("方法名稱"， 延遲時間)
        // Invoke("OpenDoor", 6);

        // 重複調用("方法名稱"，延遲時間，重複頻率)
        // InvokeRepeating("OpenDoor", 0, 1.5f);

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
        aniDoor.SetTrigger("開門處發");
    }
}
