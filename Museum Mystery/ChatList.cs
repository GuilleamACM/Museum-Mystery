using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatList : MonoBehaviour {

    [SerializeField]
    private Text myText;
    private Image myImage;

    public void SetText(string textString)
    {
        myText = textString;
    }

    public void SetImage(Sprite image)
    {
        myImage.sprite = image;
    }
}
