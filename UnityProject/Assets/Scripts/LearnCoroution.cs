using UnityEngine;
using System.Collections; // 引用 系統.集合 API

public class LearnCoroution : MonoBehaviour
{
    // 定義 : 傳回類型 IEnumerator
    public IEnumerator Test()
    {
        print("執行協程方法");

        yield return new WaitForSeconds(2); //等待兩秒

        print("兩秒後~");
    }

    public Transform people;

    public IEnumerator Big()
    {
        for (int i = 0; i < 10; i++)
        {
            people.localScale += Vector3.one;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Start()
    {
        StartCoroutine(Test()); // 啟動協程(協程名稱())

        StartCoroutine(Big());
    }
}
