using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{

    public GameObject MenuCanvas;
    public GameObject ChatCanvas;
    public GameObject PistasCanvas;
    public static GameObject ChatNotification;
    public static GameObject ARNotification;
    public static GameObject DicasNotification;
    public GameObject AR;
    public GameObject Targets;
    public static GameObject Etapa1;
    public static GameObject Etapa2;
    public static GameObject Etapa3;
    public static GameObject Etapa4;
    public static GameObject BotaoPista0;
    public static GameObject ARTextNotification;
    public static Animator animator;

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
        Etapa1 = GameObject.Find("Etapa 1");
        Etapa2 = GameObject.Find("Etapa 2");
        Etapa3 = GameObject.Find("Etapa 3");
        Etapa4 = GameObject.Find("Etapa 4");
        BotaoPista0 = GameObject.Find("Button0");
        ARTextNotification = GameObject.Find("PopupNotification");
    }

    void Start()
    {
        animator = ARTextNotification.GetComponent<Animator>();
        BotaoPista0.SetActive(false);
        ChatCanvas.SetActive(false);
        PistasCanvas.SetActive(false);
        AR.SetActive(false);
        Etapa1.SetActive(false);
        Etapa2.SetActive(false);
        Etapa3.SetActive(false);
        Etapa4.SetActive(false);
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

    public static void TurnOnChatNofication()
    {
        ChatNotification.SetActive(true);
    }

    public void TurnOffChatNofication()
    {
        ChatNotification.SetActive(false);
    }

    public static void TurnOnARNofication()
    {
        ARNotification.SetActive(true);
    }

    public void TurnOffARNofication()
    {
        ARNotification.SetActive(false);
    }

    public static void TurnOnDicasNofication()
    {
        DicasNotification.SetActive(true);
    }

    public void TurnOffDicasNofication()
    {
        DicasNotification.SetActive(false);
    }
}