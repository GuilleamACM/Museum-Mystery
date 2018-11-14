using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatListControl : MonoBehaviour {

    [SerializeField]
    public GameObject detetiveTxtTemplate;
    public GameObject playerTxtTemplate;
    public GameObject detetiveImgTemplate;
    public GameObject playerImgTemplate;
    public static GameObject staticDetetiveTxtTemplate;
    public static GameObject staticPlayerTxtTemplate;
    public static GameObject staticDetetiveImgTemplate;
    public static GameObject staticPlayerImgTemplate;
    public static Sprite[] sprites;
    public Sprite [] sprites2;


    void Awake()
    {
        staticDetetiveTxtTemplate = detetiveTxtTemplate;
        staticPlayerTxtTemplate = playerTxtTemplate;
        staticDetetiveImgTemplate = detetiveImgTemplate;
        staticPlayerImgTemplate = playerImgTemplate;
        sprites = sprites2;
    }

   /* public static void RenderizarImagem(int img, bool isLeft)
    {
        GameObject msg = Instantiate(msgTemplate) as GameObject;
        msg.SetActive(true);
        msg.GetComponent<ChatList>().SetImage(sprites[img]);
        msg.transform.GetChild(1).gameObject.SetActive(false);
        msg.transform.SetParent(msgTemplate.transform.parent, false);
    }*/

    public static void RenderizarTexto(string text, bool isLeft)
    {
        if(isLeft == true)
        {
            GameObject msg = Instantiate(staticDetetiveTxtTemplate) as GameObject;
            msg.SetActive(true);
            msg.GetComponent<ChatList>().SetText(text);
            msg.transform.SetParent(staticPlayerTxtTemplate.transform.parent, false);
        } else
        {
            GameObject msg = Instantiate(staticPlayerTxtTemplate) as GameObject;
            msg.SetActive(true);
            msg.GetComponent<ChatList>().SetText(text);
            msg.transform.SetParent(staticPlayerTxtTemplate.transform.parent, false);
        }
    }

    public static void RenderizarImagem(int img, bool isLeft)
    {
        if (isLeft == true)
        {
            GameObject msg = Instantiate(staticDetetiveImgTemplate) as GameObject;
            msg.SetActive(true);
            msg.GetComponent<ChatList>().SetImage(sprites[img]);
            msg.transform.SetParent(staticPlayerTxtTemplate.transform.parent, false);
        }
        else
        {
            GameObject msg = Instantiate(staticPlayerImgTemplate) as GameObject;
            msg.SetActive(true);
            msg.GetComponent<ChatList>().SetImage(sprites[img]);
            msg.transform.SetParent(staticPlayerTxtTemplate.transform.parent, false);
        }
    }

}
