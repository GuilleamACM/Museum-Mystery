using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image img;
    public Image imgAmpliada;
    public Text title;
    public Text titleAmpliado;
    public Sprite[] sprites;
    public Text descricao;
    public string[] titles;
    public string[] descricoes;
    public GameObject MenuCanvas;
    public GameObject ChatCanvas;
    public GameObject PistasCanvas;
    public GameObject panel;
    public GameObject AR;
    public GameObject Targets;
    public GameObject[] BotaoPista;
    public GameObject[] BotaoDicas;
    public GameObject telaPista;
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
    public static int staticRefBotao;
    public bool firstOpenchat = true;
    public Button arBotao;
    public Button arquivoBotao;

    void Awake()
    {
        Debug.Log("Awake called");
        ChatNotification = GameObject.Find("ChatNotification");
        ARNotification = GameObject.Find("ARNotification");
        DicasNotification = GameObject.Find("DicasNotification");
        Etapa1 = GameObject.Find("Etapa 1");
        Etapa2 = GameObject.Find("Etapa 2");
        Etapa3 = GameObject.Find("Etapa 3");
        Etapa4 = GameObject.Find("Etapa 4");
        ARTextNotification = GameObject.Find("PopupNotification");
        staticBotaoPista = BotaoPista;
        staticBotaoDicas = BotaoDicas;
        DicasNotification.SetActive(false);
        ARNotification.SetActive(false);
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
        if (firstOpenchat)
        {
            arBotao.interactable = true;
            arquivoBotao.interactable = true;
            firstOpenchat = false;
            TurnOnARNofication();
            TurnOnChatNofication();
        }

        ChatCanvas.SetActive(true);
        MenuCanvas.SetActive(false);
        TurnOffChatNofication();

        if (PlayerInfo.etapaAtual == 0)
        {
            if (!check)
            {
                Detetive.StartIntro();
                check = true;
                PlayerInfo.AumentarEtapa();
                
            }
        }

        else if (Detetive.etapa == 0)
        {
            if (Detetive.automatico[Detetive.etapa].enviado && !check)
            {
                PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("mapaCigarro"));
                check = true;
            }
        }
        else if (Detetive.etapa == 1)
        {

            if (Detetive.automatico[Detetive.etapa].enviado && !check)
            {
                int aux = PlayerInfo.ProcurarPista("suspeito1");
                PlayerInfo.DescobrirPista(aux);
                PlayerInfo.DescobrirPista(aux + 1);
                PlayerInfo.DescobrirPista(aux + 2);
                check = true;
            }
        }

        /* else if(Detetive.etapa == 2)
        {
            não tem nada para habilitar.
        } */
        else if (Detetive.etapa == 3)
        {
            if (Detetive.automatico[Detetive.etapa].enviado && !check)
            {
                PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("cordel"));
                check = true;
            }
        }

        /* else if(Detetive.etapa == 4)
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
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            return;
        }
        if (telaPista.activeSelf)
        {
            telaPista.SetActive(false);
            return;
        }
        PistasCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    public void OpenPista(int i)
    {
        img.sprite = sprites[i];
        title.text = titles[i];
        descricao.text = descricoes[i];
        telaPista.SetActive(true);
        titleAmpliado.text = titles[i];
        atualizarReferencia(i);

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
        imgAmpliada.sprite = go.GetComponent<Image>().sprite;
        imgAmpliada.SetNativeSize();
    }

    public void DiminuirImagem()
    {
        panel.SetActive(false);
    }

    public static void atualizarReferencia(int i)
    {
        staticRefBotao = i;
    }
}


