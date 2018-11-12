using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatListControl : MonoBehaviour {

    [SerializeField]
    public GameObject template;
    public static GameObject msgTemplate;
    public static Sprite[] sprites;
    public Sprite [] sprites2;


    void Awake()
    {
        msgTemplate = template;
        sprites = sprites2;
    }

    public static void RenderizarImagem(int img, bool isLeft)
    {
        GameObject msg = Instantiate(msgTemplate) as GameObject;
        msg.SetActive(true);
        msg.GetComponent<ChatList>().SetImage(sprites[img]);
        msg.transform.GetChild(1).gameObject.SetActive(false);
        msg.transform.SetParent(msgTemplate.transform.parent, false);
    }

    public static void RenderizarTexto(string text, bool isLeft) 
    {
        GameObject msg = Instantiate(msgTemplate) as GameObject;
        msg.SetActive(true);
        msg.GetComponent<ChatList>().SetText(text);
        msg.transform.GetChild(0).gameObject.SetActive(false);
        msg.transform.SetParent(msgTemplate.transform.parent, false);
    }
}
