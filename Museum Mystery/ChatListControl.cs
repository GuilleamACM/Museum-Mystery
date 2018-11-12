using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatListControl : MonoBehaviour {

    [SerializeField]
    private GameObject msgTemplate;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    void Start()
    {
        for (int i = 1; i <= 3; i++)
        {
            GameObject button = Instantiate(msgTemplate) as GameObject;
            button.setAtctive(true);
            button.GetComponent<ChatList>().SetImage(sprite1);
            button.transform.SetPartent(buttonTemplate.transform.parent, false);
        }
    }
}
