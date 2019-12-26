
using UnityEngine;

namespace MyNamespace
{
    public class CameraControl : MonoBehaviour
    {
        [Header("速度"), Range(0, 100)]
        public float speed = 1.5f;
        [Header("上方限制")]
        public float top;
        [Header("下方限制")]
        public float bottom;

        private Transform player;

        private void Start()
        {
            player = GameObject.Find("女孩").transform;
        }

        //在 Update 之後才會執行，可用在 = 攝影機追蹤、物件追蹤
        private void LateUpdate()
        {
            Track();
        }

        /// <summary>
        /// 攝影機追蹤效果
        /// </summary>
        private void Track()
        {
            Vector3 posP = player.position; // 玩家
            Vector3 posC = transform.position; // 攝影機

            posP.x = 0;    // 固定x軸
            posP.y = 25;   // 固定y軸
            posP.z += 29;  //放在玩家後方 (+或-)= 29

            posP.z = Mathf.Clamp(posP.z, bottom, top);  // 玩家.Z軸 夾住在 上方限制~下方限制

            // 攝影機.座標 = 三維插值(A座標，B座標，百分比(攝影機移動速度)
            transform.position = Vector3.Lerp(posC, posP, 0.3f * Time.deltaTime * speed);
        }
    }
}

