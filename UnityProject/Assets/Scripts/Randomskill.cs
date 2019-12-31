using UnityEngine;
using UnityEngine.UI; // 引用介面 API

public class NewBehaviourScript : MonoBehaviour
{
    [Header("技能圖片:模糊與正常")]

    public Sprite[] spritesBlurs; // 模糊塗片陣列 16
    public Sprite[] spritesSkills; // 技能圖片陣列 8

    private Image img; //圖片元件
    private Button btn; //按鈕元件

}
