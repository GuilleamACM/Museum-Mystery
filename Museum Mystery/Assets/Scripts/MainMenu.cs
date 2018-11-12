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
    public static bool check = false;

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

        if (PlayerInfo.etapaAtual == 0)
        {
            Detetive.StartIntro();
            //Invoke("AumentarEtapaIntro", 5); // como invoke não pode ser chamado num static, criei uma funcao start para comecar e dps de 5 segundos chamar a funcao aumentarEtapa para o jogador avançar no jogo.
        }

        else if (Detetive.etapa == 1)
        {
            if (Detetive.automatico[Detetive.etapa].enviado && !check)
            {
                PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("MapaCigarro"));
                check = true;
            }
        }
        else if (Detetive.etapa == 2)
        {

            if (Detetive.automatico[Detetive.etapa].enviado && !check)
            {
                int aux = PlayerInfo.ProcurarPista("Suspeito1");
                PlayerInfo.DescobrirPista(aux);
                PlayerInfo.DescobrirPista(aux + 1);
                PlayerInfo.DescobrirPista(aux + 2);
                check = true;
            }
        }

        /* else if(Detetive.etapa == 3)
        {
            não tem nada para habilitar.
        } */
        else if (Detetive.etapa == 4)
        {
            if (Detetive.automatico[Detetive.etapa].enviado && !check)
            {
                PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("Cordel"));
                check = true;
            }
        }

        /* else if(Detetive.etapa == 5)
        {
            não tem nada para habilitar.
        } */

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
