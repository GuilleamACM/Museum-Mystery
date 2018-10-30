using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{

    GameObject MenuCanvas;
    GameObject ChatCanvas;
    GameObject PistasCanvas;
    GameObject ChatNotification;
    GameObject ARNotification;
    GameObject DicasNotification;
    GameObject AR;
    GameObject Targets;
    GameObject ARTextNotification;

    void Awake()
    {
        Debug.Log("Awake called");
        MenuCanvas = GameObject.Find("MenuCanvas");
        ChatCanvas = GameObject.Find("ChatCanvas");
        PistasCanvas = GameObject.Find("PistasCanvas");
        ChatNotification = GameObject.Find("ChatNotification");
        ARNotification = GameObject.Find("ARNotification");
        DicasNotification = GameObject.Find("DicasNotification");
        AR = GameObject.Find("AR");
        Targets = GameObject.Find("Targets");
        ARTextNotification = GameObject.Find("ARNotificationText");

    }

    void Start()
    {
        ChatCanvas.SetActive(false);
        PistasCanvas.SetActive(false);
        ARTextNotification.SetActive(false);
        AR.SetActive(false);
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                BacktoMainMenu();
                return;
            }
        }
    }

    public void OpenAR()
    {
        AR.SetActive(true);
        MenuCanvas.SetActive(false);
        TurnOffARNofication();
    }

    public void OpenChat()
    {
        ChatCanvas.SetActive(true);
        MenuCanvas.SetActive(false);
        TurnOffChatNofication();
    }

    public void OpenDatabase()
    {
        PistasCanvas.SetActive(true);
        MenuCanvas.SetActive(false);
        TurnOffDicasNofication();
    }

    public void BacktoMainMenu()
    {
        PistasCanvas.SetActive(false);
        AR.SetActive(false);
        ChatCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    public void TurnOnChatNofication()
    {
        ChatNotification.SetActive(true);
    }

    public void TurnOffChatNofication()
    {
        ChatNotification.SetActive(false);
    }

    public void TurnOnARNofication()
    {
        ARNotification.SetActive(true);
    }

    public void TurnOffARNofication()
    {
        ARNotification.SetActive(false);
    }

    public void TurnOnDicasNofication()
    {
        DicasNotification.SetActive(true);
    }

    public void TurnOffDicasNofication()
    {
        DicasNotification.SetActive(false);
    }
}