using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatListControl : MonoBehaviour {

    [SerializeField]
    public GameObject msgTemplate;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    int i = 1;

    public void Dale()
    {
        GameObject button = Instantiate(msgTemplate) as GameObject;
        button.SetActive(true);
        if (i == 1)
        {
            button.GetComponent<ChatList>().SetImage(sprite1);
        } else if (i == 2)
        {
            button.GetComponent<ChatList>().SetImage(sprite2);
        } else if (i == 3)
        {
            button.GetComponent<ChatList>().SetImage(sprite3);
        } else if (i == 4)
        {
            button.GetComponent<ChatList>().SetText("Dale");
            var tempColor = button.GetComponent<Image>().color;
            tempColor.a = 0f;
            button.GetComponent<ChatList>().myImage.color = tempColor;
            button.GetComponent<Image>().color = tempColor;
        } else if (i == 5)
        {
            button.GetComponent<ChatList>().SetText("Deus é GOD");
            var tempColor = button.GetComponent<Image>().color;
            tempColor.a = 0f;
            button.GetComponent<ChatList>().myImage.color = tempColor;
            button.GetComponent<Image>().color = tempColor;
        }
        button.transform.SetParent(msgTemplate.transform.parent, false);
        i++;
    }
}
