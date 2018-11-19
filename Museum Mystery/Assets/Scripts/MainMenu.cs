using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Image img;
    public GameObject MenuCanvas;
    public GameObject ChatCanvas;
    public GameObject PistasCanvas;
    public GameObject panel;
    public GameObject AR;
    public GameObject Targets;
    public GameObject[] BotaoPista;
    public GameObject[] BotaoDicas;
    public GameObject[] telasPistas; 
    public static GameObject ChatNotification;
    public static GameObject ARNotification;
    public static GameObject DicasNotification;
    public static GameObject Etapa1;
    public static GameObject Etapa2;
    public static GameObject Etapa3;
    public static GameObject Etapa4;
    public static GameObject ARTextNotification;
    public static Animator animator;
    public static GameObject[] staticBotaoPista;
    public static GameObject[] staticBotaoDicas;
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
        staticBotaoPista = BotaoPista;       
        ARTextNotification = GameObject.Find("PopupNotification");
        staticBotaoDicas = BotaoDicas;
    }

    void Start()
    {
        ARTextNotification.SetActive(false);
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
            if (!check)
            {
                Detetive.StartIntro();
                check = true;
                //Invoke("AumentarEtapaIntro", 5); // como invoke não pode ser chamado num static, criei uma funcao start para comecar e dps de 5 segundos chamar a funcao aumentarEtapa para o jogador avançar no jogo.
            }
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
        AR.SetActive(false);
        ChatCanvas.SetActive(false);
        for (int i = 0; i < telasPistas.Length; i++)
        {
            if (telasPistas[i].activeSelf)
            {
                telasPistas[i].SetActive(false);
                return;
            }
        }
        PistasCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    public void OpenPista(int i)
    {
        telasPistas[i].SetActive(true);
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

    public void AumentarImagem()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        panel.SetActive(true);
        img.sprite = go.GetComponent<Image>().sprite;
        img.SetNativeSize();
    }

    public void DiminuirImagem()
    {
        panel.SetActive(false);
    }
}
