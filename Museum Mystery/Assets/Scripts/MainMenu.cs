using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject popupFirstAR;
    public Image img;
    public Image imgAmpliada;
    public Text subTitle;
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
    public static GameObject ARTextNotification;
    public static Animator animator;
    public static GameObject[] staticBotaoPista;
    public static GameObject[] staticBotaoDicas;
    public static bool check = false;
    public static int staticRefBotao;
    public static bool firstOpenchat = false;
    public Button arBotao;
    public Button arquivoBotao;
    public static bool arquivoBotaoInteractable = false;
    public bool firstAR = true;

    void Awake()
    {
        Debug.Log("Awake called");
        ChatNotification = GameObject.Find("ChatNotification");
        ARNotification = GameObject.Find("ARNotification");
        DicasNotification = GameObject.Find("DicasNotification");
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
        if (Detetive.msgs > 0)
        {
            TurnOnChatNofication();
        }
        else
        {
            TurnOffChatNofication();
        }
        if (PlayerInfo.newPistas > 0)
        {
            TurnOnDicasNofication();
        }
        else
        {
            TurnOffDicasNofication();
        }
    }

    public void desativarPopupFirstAR()
    {
        popupFirstAR.SetActive(false);
    }

    public void OpenAR()
    {
        if (firstAR)
        {
            popupFirstAR.SetActive(true);
            Invoke("desativarPopupFirstAR",5);
            firstAR = false;
        }
        
        AR.SetActive(true);
        MenuCanvas.SetActive(false);
        TurnOffARNofication();
    }

    public void OpenChat()
    {

        ChatCanvas.SetActive(true);
        MenuCanvas.SetActive(false);

        if (PlayerInfo.etapaAtual == 0)
        {
            if (!check)
            {
                Detetive.StartIntro();
                check = true;
            }
        }

        else if (Detetive.etapa == 0)
        {
            Debug.Log("etapa: "+Detetive.etapa+"  detetive.automatico[detetive.etapa]= "+ Detetive.automatico[Detetive.etapa].enviado);
            if (Detetive.automatico[Detetive.etapa].enviado && !check)
            {
               
                check = true;
            }
        }
        else if (Detetive.etapa == 1)
        {

            if (Detetive.automatico[Detetive.etapa].enviado && !check)
            {
                
                check = true;
            }
        }

        else if(Detetive.etapa == 2)
        {
            if (Detetive.automatico[Detetive.etapa].enviado && !check)
            {
                
                check = true;
            }
        }

        /*else if (Detetive.etapa == 3)
        {
            if (Detetive.automatico[Detetive.etapa].enviado && !check)
            {
                PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("cordel"), false);
                check = true;
            }
        }*/

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
        if (firstOpenchat)
        {
            arBotao.interactable = true;
            arquivoBotao.interactable = true;
            arquivoBotaoInteractable = true;
            firstOpenchat = false;
        }

        AR.SetActive(false);
        if (ChatCanvas.activeSelf)
        {
            ChatCanvas.SetActive(false);
            Detetive.resetMsgs();
        }
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
        if (PistasCanvas.activeSelf)
        {
            PistasCanvas.SetActive(false);
            PlayerInfo.resetNewPistas();
        }
        MenuCanvas.SetActive(true);
        
    }

    public void OpenPista(int i)
    {
        subTitle.text = titles[i];
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
        if (arquivoBotaoInteractable)
        {
            DicasNotification.SetActive(true);
        }
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

    public void AumentarImagemLupa()
    {
        GameObject go = GameObject.Find("ImageZoom");
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


