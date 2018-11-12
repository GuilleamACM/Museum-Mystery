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

    static public void DoCoroutine(float time)
    {
        instance.StartCoroutine("Wait", time);
    }

}
