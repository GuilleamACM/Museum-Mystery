using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatListControl : MonoBehaviour {

    [SerializeField]
    public GameObject detetiveTxtTemplate;
    public GameObject playerTxtTemplate;
    public GameObject detetiveImgTemplate;
    public GameObject playerImgTemplate;
    public Image img;
    public GameObject panel;
    public GameObject panelDicas;
    public static GameObject staticPanelDicas;
    public static GameObject staticDetetiveTxtTemplate;
    public static GameObject staticPlayerTxtTemplate;
    public static GameObject staticDetetiveImgTemplate;
    public static GameObject staticPlayerImgTemplate;
    public static Sprite[] sprites;
    public Sprite [] sprites2;



    public static void closePanelDicasStatic()
    {
        staticPanelDicas.SetActive(false);
    }

    public void openPanelDicas()
    {
       panelDicas.SetActive(true);

    }

    public void closePanelDicas()
    {
        panelDicas.SetActive(false);
    }

    void Awake()
    {
        staticDetetiveTxtTemplate = detetiveTxtTemplate;
        staticPlayerTxtTemplate = playerTxtTemplate;
        staticDetetiveImgTemplate = detetiveImgTemplate;
        staticPlayerImgTemplate = playerImgTemplate;
        sprites = sprites2;
        staticPanelDicas = panelDicas;
        
    }

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

    public void AumentarImagem()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        panel.SetActive(true);
        img.sprite = go.GetComponent<Image>().sprite;
        
    }

    public void DiminuirImagem()
    {
        panel.SetActive(false);
    }

}
