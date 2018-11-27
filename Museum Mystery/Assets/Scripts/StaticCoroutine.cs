using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCoroutine : MonoBehaviour {
    static public StaticCoroutine instance;

    void Awake()
    {
        instance = this;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        MainMenu.ARTextNotification.SetActive(false);

    }

    IEnumerator Wait2(string resp)
    {
        yield return new WaitForSeconds(1.75f);
        ChatListControl.RenderizarTexto(resp, true);
    }

    static public void DoCoroutine(float time)
    {
        instance.StartCoroutine("Wait", time);
    }

    static public void DoCoroutineDelayMsgDetetive(string resp)
    {
        instance.StartCoroutine("Wait2", resp);
    }

}
