using UnityEngine;

public class Item : MonoBehaviour
{
    /// <summary>
    /// 是否過關，過關時就前往玩家的位置
    /// </summary>
    [HideInInspector]    // 在屬性面板隱藏
    public bool pass;

    [Header("道具音效")]
    public AudioClip sound;

    private Transform player;     // 玩家變形元件
    private AudioSource aud;

    private void Start()
    {
        Physics.IgnoreLayerCollision(10, 10, false);
        aud = GetComponent<AudioSource>();
        player = GameObject.Find("女孩").transform;

        HandleCollision();
    }

    private void Update()
    {
        GoToPlayer();
    }

    /// <summary>
    /// 控制忽略碰撞
    /// </summary>
    private void HandleCollision()
    {
        Physics.IgnoreLayerCollision(10, 8);    // 忽略碰撞 金幣 玩家
        Physics.IgnoreLayerCollision(10, 9);    // 忽略碰撞 金幣 敵人
    }

    private void GoToPlayer()
    {
        if (pass)
        {
            Physics.IgnoreLayerCollision(10, 10);
            transform.position = Vector3.Lerp(transform.position, player.position, 1.5f * Time.deltaTime * 25);

            if (Vector3.Distance(transform.position, player.position) < 1.5f && !aud.isPlaying)
            {
                aud.PlayOneShot(sound, 0.3f);
                Destroy(gameObject, 0.3f);
            }
        }
       
    }
}
