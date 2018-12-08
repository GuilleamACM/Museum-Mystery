using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCoroutine : MonoBehaviour {
    static public StaticCoroutine instance; 
    public int wordConstant; // time = txt.length/wordConstant = 30;
    public int wordConstant2; // time = time + (txt.Length / wordConstant2 = 30) * weight);
    public float weight; // peso que vale para proxima palavra a ser mandada 0.5f.
    public float imageTime; // tempo direto de mandar uma imagem = 1.5f.
    public float dicasTime; //tempo para mandar uma dica 1.75f

    void Awake()
    {
        instance = this;
    }

    IEnumerator WaitMsgsResposta()
    {
        float time = 1;

        for (int i = 1; i < Detetive.resposta[Detetive.etapa].imgOrTxt.Length; i++)
        {
            if (Detetive.resposta[Detetive.etapa].imgOrTxt[i].isImg())
            {
                if (Detetive.resposta[Detetive.etapa].imgOrTxt[i].isLeft)
                {
                    ChatListControl.RenderizarImagem(Detetive.resposta[Detetive.etapa].imgOrTxt[i].img, true);
                }
                else
                {
                    ChatListControl.RenderizarImagem(Detetive.resposta[Detetive.etapa].imgOrTxt[i].img, false);
                }
                Handheld.Vibrate();
                time = imageTime;
                yield return new WaitForSeconds(time);
                Debug.Log(time + "<-------IMAGEM--------");
            }
            else
            {
                if (Detetive.resposta[Detetive.etapa].imgOrTxt[i].isLeft)
                {
                    ChatListControl.RenderizarTexto(Detetive.resposta[Detetive.etapa].imgOrTxt[i].txt, true);
                }
                else
                {
                    ChatListControl.RenderizarTexto(Detetive.resposta[Detetive.etapa].imgOrTxt[i].txt, false);
                }
                time = (Detetive.resposta[Detetive.etapa].imgOrTxt[i].txt.Length) / wordConstant;
                if (i + 1 < Detetive.resposta[Detetive.etapa].imgOrTxt.Length && !Detetive.resposta[Detetive.etapa].imgOrTxt[i + 1].isImg())
                {
                    time = time + ((Detetive.resposta[Detetive.etapa].imgOrTxt[i + 1].txt.Length / wordConstant2) * weight);
                }

                Handheld.Vibrate();
                yield return new WaitForSeconds(time);
                Debug.Log(time + "<-------TEXTO--------");
            }
        }
        Detetive.EnviarMsgFeedback();
    }



    IEnumerator WaitMsgsAutomatic()
    {
        float time = 1;
        
        for (int i = 0; i < Detetive.automatico[Detetive.automatic].imgOrTxt.Length; i++)
        {
            if (Detetive.automatico[Detetive.automatic].imgOrTxt[i].isImg())
            {
                if (Detetive.automatico[Detetive.automatic].imgOrTxt[i].isLeft)
                {
                    ChatListControl.RenderizarImagem(Detetive.automatico[Detetive.automatic].imgOrTxt[i].img, true);
                }
                else
                {
                    ChatListControl.RenderizarImagem(Detetive.automatico[Detetive.automatic].imgOrTxt[i].img, false);
                }
                Handheld.Vibrate();
                time = imageTime;
                yield return new WaitForSeconds(time);
                Debug.Log(time + "<-------IMAGEM--------");
            }
            else
            {
                if (Detetive.automatico[Detetive.automatic].imgOrTxt[i].isLeft)
                {
                    ChatListControl.RenderizarTexto(Detetive.automatico[Detetive.automatic].imgOrTxt[i].txt, true);
                }
                else
                {
                    ChatListControl.RenderizarTexto(Detetive.automatico[Detetive.automatic].imgOrTxt[i].txt, false);
                }
                time = (Detetive.automatico[Detetive.automatic].imgOrTxt[i].txt.Length) / wordConstant;
                if (i + 1 < Detetive.automatico[Detetive.automatic].imgOrTxt.Length && !Detetive.automatico[Detetive.automatic].imgOrTxt[i + 1].isImg())
                {
                    time = time + ((Detetive.automatico[Detetive.automatic].imgOrTxt[i + 1].txt.Length / wordConstant2) * weight);
                }

                Handheld.Vibrate();
                yield return new WaitForSeconds(time);
                Debug.Log(time + "<-------TEXTO--------");
            }

        }

        if(Detetive.etapa == 0)
        {
            if(Detetive.automatic <= 0)
            {
                Detetive.automatico[Detetive.automatic].enviado = true;
                MainMenu.TurnOnChatNofication();
                Handheld.Vibrate();
                Detetive.automatic++;
                PlayerInfo.AumentarEtapa();
                PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("mapaCigarro"), false);
                PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("livro"), false);
                //liberar mapaCigarro -  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat  
            }
            else
            {
                Detetive.automatico[Detetive.automatic].enviado = true;
                MainMenu.TurnOnChatNofication();
                Detetive.exploracao = false;
            }

        }else if (Detetive.etapa == 1)
        {
            Detetive.automatico[Detetive.automatic].enviado = true;
            MainMenu.TurnOnChatNofication();
            Handheld.Vibrate();
            Detetive.exploracao = false;
            int aux = PlayerInfo.ProcurarPista("suspeitos");
            PlayerInfo.DescobrirPista(aux, false);
            // suspeitos deve ser habilitado.  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat
        }
        else if (Detetive.etapa == 2)
        {
            Detetive.automatico[Detetive.automatic].enviado = true;
            // aumenta automatic e etapa do player e detetive, pois nao tem feedback nessa parte.   no MAINMENU             
            MainMenu.TurnOnChatNofication();
            Handheld.Vibrate();
            int aux = PlayerInfo.ProcurarPista("cordel");
            PlayerInfo.DescobrirPista(aux, false);
            Detetive.automatic++;
            Detetive.etapa++;
            PlayerInfo.AumentarEtapa();
        }
        else if (Detetive.etapa == 3)
        {
            Detetive.automatico[Detetive.automatic].enviado = true;
            Detetive.exploracao = false;
            MainMenu.TurnOnChatNofication();
            Handheld.Vibrate();
        }
        else if (Detetive.etapa == 4)
        {
            Detetive.exploracao = false;
            Detetive.automatico[Detetive.automatic].enviado = true;
            MainMenu.TurnOnChatNofication();
            Handheld.Vibrate();
        }
    }


    IEnumerator WaitMsgsIntro2()
    {
        float time=1;

        if (Detetive.etapa == 1)
        {
            for (int i = 0; i < Detetive.intro[Detetive.etapa].imgOrTxt.Length; i++)
            {
                if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isImg())
                {
                    if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isLeft)
                    {
                        ChatListControl.RenderizarImagem(Detetive.intro[Detetive.etapa].imgOrTxt[i].img, true);

                    }
                    else
                    {
                        ChatListControl.RenderizarImagem(Detetive.intro[Detetive.etapa].imgOrTxt[i].img, false);
                    }
                    Handheld.Vibrate();
                    time = imageTime;
                    yield return new WaitForSeconds(time);
                    Debug.Log(time + "<-------IMAGEM--------");
                }
                else
                {
                    if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isLeft)
                    {
                        ChatListControl.RenderizarTexto(Detetive.intro[Detetive.etapa].imgOrTxt[i].txt, true);

                    }
                    else
                    {
                        ChatListControl.RenderizarTexto(Detetive.intro[Detetive.etapa].imgOrTxt[i].txt, false);
                    }
                    time = (Detetive.intro[Detetive.etapa].imgOrTxt[i].txt.Length) / wordConstant;
                    if (i + 1 < Detetive.intro[Detetive.etapa].imgOrTxt.Length && !Detetive.intro[Detetive.etapa].imgOrTxt[i + 1].isImg())
                    {
                        time = time + ((Detetive.intro[Detetive.etapa].imgOrTxt[i + 1].txt.Length / wordConstant2) * weight);
                    }

                    Handheld.Vibrate();
                    yield return new WaitForSeconds(time);
                    Debug.Log(time + "<-------TEXTO--------");
                }
            }
            Detetive.intro[Detetive.etapa].enviado = true;
            Detetive.exploracao = true;
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("mapaCalor"), false);
        }

        else if (Detetive.etapa == 2)
        {
            for (int i = 0; i < Detetive.intro[Detetive.etapa].imgOrTxt.Length; i++)
            {
                if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isImg())
                {
                    if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isLeft)
                    {
                        ChatListControl.RenderizarImagem(Detetive.intro[Detetive.etapa].imgOrTxt[i].img, true);

                    }
                    else
                    {
                        ChatListControl.RenderizarImagem(Detetive.intro[Detetive.etapa].imgOrTxt[i].img, false);
                    }
                    Handheld.Vibrate();
                    time = imageTime;
                    yield return new WaitForSeconds(time);
                    Debug.Log(time + "<-------IMAGEM--------");
                }
                else
                {
                    if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isLeft)
                    {
                        ChatListControl.RenderizarTexto(Detetive.intro[Detetive.etapa].imgOrTxt[i].txt, true);

                    }
                    else
                    {
                        ChatListControl.RenderizarTexto(Detetive.intro[Detetive.etapa].imgOrTxt[i].txt, false);
                    }
                    time = (Detetive.intro[Detetive.etapa].imgOrTxt[i].txt.Length) / wordConstant;
                    if (i + 1 < Detetive.intro[Detetive.etapa].imgOrTxt.Length && !Detetive.intro[Detetive.etapa].imgOrTxt[i + 1].isImg())
                    {
                        time = time + ((Detetive.intro[Detetive.etapa].imgOrTxt[i + 1].txt.Length / wordConstant2) * weight);
                    }

                    Handheld.Vibrate();
                    yield return new WaitForSeconds(time);
                    Debug.Log(time + "<-------TEXTO--------");
                }
            }
            Detetive.intro[Detetive.etapa].enviado = true;
            Detetive.exploracao = true;
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("frase"), false);
        }
        else if (Detetive.etapa == 3)
        {
            for (int i = 0; i < Detetive.intro[Detetive.etapa].imgOrTxt.Length; i++)
            {
                if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isImg())
                {
                    if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isLeft)
                    {
                        ChatListControl.RenderizarImagem(Detetive.intro[Detetive.etapa].imgOrTxt[i].img, true);

                    }
                    else
                    {
                        ChatListControl.RenderizarImagem(Detetive.intro[Detetive.etapa].imgOrTxt[i].img, false);
                    }
                    Handheld.Vibrate();
                    time = imageTime;
                    yield return new WaitForSeconds(time);
                    Debug.Log(time + "<-------IMAGEM--------");
                }
                else
                {
                    if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isLeft)
                    {
                        ChatListControl.RenderizarTexto(Detetive.intro[Detetive.etapa].imgOrTxt[i].txt, true);

                    }
                    else
                    {
                        ChatListControl.RenderizarTexto(Detetive.intro[Detetive.etapa].imgOrTxt[i].txt, false);
                    }
                    time = (Detetive.intro[Detetive.etapa].imgOrTxt[i].txt.Length) / wordConstant;
                    if (i + 1 < Detetive.intro[Detetive.etapa].imgOrTxt.Length && !Detetive.intro[Detetive.etapa].imgOrTxt[i + 1].isImg())
                    {
                        time = time + ((Detetive.intro[Detetive.etapa].imgOrTxt[i + 1].txt.Length / wordConstant2) * weight);
                    }

                    Handheld.Vibrate();
                    yield return new WaitForSeconds(time);
                    Debug.Log(time + "<-------TEXTO--------");
                }
            }
            Detetive.intro[Detetive.etapa].enviado = true;
            Detetive.exploracao = true;

        }
        else if (Detetive.etapa == 4)
        {
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("cartaJaco"), false);
            for (int i = 0; i < Detetive.intro[Detetive.etapa].imgOrTxt.Length; i++)
            {
                if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isImg())
                {
                    if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isLeft)
                    {
                        ChatListControl.RenderizarImagem(Detetive.intro[Detetive.etapa].imgOrTxt[i].img, true);

                    }
                    else
                    {
                        ChatListControl.RenderizarImagem(Detetive.intro[Detetive.etapa].imgOrTxt[i].img, false);
                    }
                    Handheld.Vibrate();
                    time = imageTime;
                    yield return new WaitForSeconds(time);
                    Debug.Log(time + "<-------IMAGEM--------");
                }
                else
                {
                    if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isLeft)
                    {
                        ChatListControl.RenderizarTexto(Detetive.intro[Detetive.etapa].imgOrTxt[i].txt, true);

                    }
                    else
                    {
                        ChatListControl.RenderizarTexto(Detetive.intro[Detetive.etapa].imgOrTxt[i].txt, false);
                    }
                    time = (Detetive.intro[Detetive.etapa].imgOrTxt[i].txt.Length) / wordConstant;
                    if (i + 1 < Detetive.intro[Detetive.etapa].imgOrTxt.Length && !Detetive.intro[Detetive.etapa].imgOrTxt[i + 1].isImg())
                    {
                        time = time + ((Detetive.intro[Detetive.etapa].imgOrTxt[i + 1].txt.Length / wordConstant2) * weight);
                    }

                    Handheld.Vibrate();
                    yield return new WaitForSeconds(time);
                    Debug.Log(time + "<-------TEXTO--------");
                }
            }
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("mapaEnzo"), false);
            Detetive.intro[Detetive.etapa].enviado = true;
            Detetive.exploracao = true;
        }
        else if (Detetive.etapa == 5) //acabou game feedback final.
        {
            for (int i = 0; i < Detetive.intro[Detetive.etapa].imgOrTxt.Length; i++)
            {
                if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isImg())
                {
                    if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isLeft)
                    {
                        ChatListControl.RenderizarImagem(Detetive.intro[Detetive.etapa].imgOrTxt[i].img, true);

                    }
                    else
                    {
                        ChatListControl.RenderizarImagem(Detetive.intro[Detetive.etapa].imgOrTxt[i].img, false);
                    }
                    Handheld.Vibrate();
                    time = imageTime;
                    yield return new WaitForSeconds(time);
                    Debug.Log(time + "<-------IMAGEM--------");
                }
                else
                {
                    if (Detetive.intro[Detetive.etapa].imgOrTxt[i].isLeft)
                    {
                        ChatListControl.RenderizarTexto(Detetive.intro[Detetive.etapa].imgOrTxt[i].txt, true);

                    }
                    else
                    {
                        ChatListControl.RenderizarTexto(Detetive.intro[Detetive.etapa].imgOrTxt[i].txt, false);
                    }
                    time = (Detetive.intro[Detetive.etapa].imgOrTxt[i].txt.Length) / wordConstant;
                    if (i + 1 < Detetive.intro[Detetive.etapa].imgOrTxt.Length && !Detetive.intro[Detetive.etapa].imgOrTxt[i + 1].isImg())
                    {
                        time = time + ((Detetive.intro[Detetive.etapa].imgOrTxt[i + 1].txt.Length / wordConstant2) * weight);
                    }

                    Handheld.Vibrate();
                    yield return new WaitForSeconds(time);
                    Debug.Log(time + "<-------TEXTO--------");
                }
            }
            Detetive.exploracao = true;
            Detetive.intro[Detetive.etapa].enviado = true;
            //acabou o jogo, se for adcionar algo pra quem zerou, é aqui.       }            
        }


    }

    IEnumerator WaitMsgsIntro()
    {
        float time;

        if (PlayerInfo.etapaAtual == 0)
        {
            for (int i = 0; i < Detetive.intro[PlayerInfo.etapaAtual].imgOrTxt.Length; i++)
            {
                

                if (Detetive.intro[PlayerInfo.etapaAtual].imgOrTxt[i].isImg())
                {
                    


                    if (Detetive.intro[PlayerInfo.etapaAtual].imgOrTxt[i].isLeft)
                    {
                        ChatListControl.RenderizarImagem(Detetive.intro[PlayerInfo.etapaAtual].imgOrTxt[i].img, true);
                    }
                    else
                    {
                        ChatListControl.RenderizarImagem(Detetive.intro[PlayerInfo.etapaAtual].imgOrTxt[i].img, false);
                    }
                    Handheld.Vibrate();
                    time = imageTime;
                    yield return new WaitForSeconds(time);
                    Debug.Log(time + "<-------IMAGEM--------");

                }
                else
                {                   

                    if ((Detetive.intro[PlayerInfo.etapaAtual].imgOrTxt[i].isLeft))
                    {
                        ChatListControl.RenderizarTexto(Detetive.intro[PlayerInfo.etapaAtual].imgOrTxt[i].txt, true);
                    }
                    else
                    {
                        ChatListControl.RenderizarTexto(Detetive.intro[PlayerInfo.etapaAtual].imgOrTxt[i].txt, false);
                    }

                    time = (Detetive.intro[PlayerInfo.etapaAtual].imgOrTxt[i].txt.Length) / wordConstant;
                    if(i+1 < Detetive.intro[PlayerInfo.etapaAtual].imgOrTxt.Length && !Detetive.intro[PlayerInfo.etapaAtual].imgOrTxt[i + 1].isImg())
                    {
                        time = time + ((Detetive.intro[PlayerInfo.etapaAtual].imgOrTxt[i + 1].txt.Length/wordConstant2) * weight);
                    }
                   
                    Handheld.Vibrate();
                    yield return new WaitForSeconds(time);
                    Debug.Log(time + "<-------TEXTO--------");

                }
            }
            Detetive.intro[PlayerInfo.etapaAtual].enviado = true;
            Detetive.exploracao = true;
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("enzoCamera"), false);

            PlayerInfo.AumentarEtapa();
            MainMenu.firstOpenchat = true;
        }
        else
        {
            Debug.Log("erro etapa inicial");
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        MainMenu.ARTextNotification.SetActive(false);

    }

    IEnumerator Wait2(string resp)
    {
        yield return new WaitForSeconds(dicasTime);
        ChatListControl.RenderizarTexto(resp, true);
    }

    static public void DoCoroutine(float time)
    {
        instance.StartCoroutine("Wait", time);
    }

    static public void DoCoroutineDelayMsgDetetive(string resp)
    {
        instance.StartCoroutine("Wait2", resp);
    }

    static public void DoCoroutineDelayMsgs()
    {
        instance.StartCoroutine("WaitMsgsIntro");
    }

    static public void DoCoroutineDelayIntro()
    {
        instance.StartCoroutine("WaitMsgsIntro2");
    }

    static public void DoCoroutineDelayResposta()
    {
        instance.StartCoroutine("WaitMsgsResposta");
    }

    static public void DoCoroutineAutomatic()
    {
        instance.StartCoroutine("WaitMsgsAutomatic");
    }
}
