using UnityEngine;

public class LearnLoop : MonoBehaviour
{
    private void Start()
    {
        // 不使用迴圈: 
        print("嗨嗨，1");
        print("嗨嗨，2");
        print("嗨嗨，3");
        print("嗨嗨，4");
        print("嗨嗨，5");

        //執行一次
        if (true)
        {

        }

        // while 迴圈 : 持續執行 () 直到為 false
        int count = 0;

        while (count<25)
        {
            count++;
            print("哈 while 迴圈:" + count);
        }

        for (int number = 0; number < 10; number++)
        {
            print("哈 for 迴圈:" + number);
        }
    }

}
