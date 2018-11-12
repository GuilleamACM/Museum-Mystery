using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detetive : MonoBehaviour
{


    public class pistaDetetive
    {
        public string id;
        public bool enviado;

        public pistaDetetive(string id)
        {
            this.id = id;
            this.enviado = false;
        }
    }


    public class Mensagem
    {
        public string id;
        public ImgOrTxt[] imgOrTxt;
        public bool enviado;

        public Mensagem(string id, ImgOrTxt[] iot)
        {
            this.id = id;
            this.imgOrTxt = iot;
            this.enviado = false;
        }

    }

    public class ImgOrTxt
    {
        public int img;
        public string txt;

        public ImgOrTxt(int img)
        {
            this.img = img;
            this.txt = null;
        }
        
        public ImgOrTxt(string text)
        {
            this.img = -1;
            this.txt = text;
        }

        public bool isImg()
        {
            if (this.img != -1)
                return true;
            else
                return false;
        }
    }


    public static Mensagem[] dicas = new Mensagem[4];
    public static Mensagem[] feedback = new Mensagem[4];
    public static Mensagem[] intro = new Mensagem[4];
    public static Mensagem[] automatico = new Mensagem[4];
    public static Mensagem[] resposta = new Mensagem[4];
    public static pistaDetetive[] pistasDetetive = new pistaDetetive[] { new pistaDetetive("Criptografia1"), new pistaDetetive("Criptografia2"), new pistaDetetive("Criptografia3"), }; // esse array deve ser mapeado igualmente a o array de dicas.
    public static bool exploracao = false; // para bloquear e desbloquear o envio de RESPOSTAS
    public static int etapa = 0;
    public static string answer;

    void Awake()
    {
        Mensagem msg1 = new Mensagem("id", new ImgOrTxt[2] { new ImgOrTxt("dale"), new ImgOrTxt(1)});
        intro[0] = msg1;
    }

    public void getTextInput(string input)
    {
        answer = input;
    }

    public void sendButton()
    {
        if (exploracao) // pop-up de que você não pode responder, nesse momento voce deve explorar
        {
            Debug.Log("Pop-up de você não pode enviar mensagem agora");
        }

        else
        {
            //show pop-up do que o detetive espera como resposta.
            if (!(answer.Equals("")))
            {
                EnviarMsgResposta(answer);
            }
            else
            {
                Debug.Log("escreva alguma coisa brtoher");
            }
        }
    }

    public int procurarPistaDetetive(string id)
    {

        for (int index = 0; index < pistasDetetive.Length; index++)
        {
            if (pistasDetetive[index].id.Equals(id))
            {
                return index;
            }
        }
        Debug.Log("Não foi encontrado!");
        return -1;
    }

    public void EnviarMsgDicas(string id) //ela só envia a mensagem do detetive (resposta para quando o jogador envia uma pista), só envia textos e as dicas são sempre uma mensagem
    {
        for (int i = 0; i <= dicas.Length; i++)
        {
            if (dicas[i].id.Equals(id))
            {
                if (dicas[i].enviado)
                {
                    ChatListControl.RenderizarTexto("Você perguntou isso antes",true);
                    return;
                }
                else
                {
                    pistasDetetive[i].enviado = true;
                    ChatListControl.RenderizarTexto(dicas[i].imgOrTxt[0].txt, true);
                    dicas[i].enviado = true;
                    return;
                }
            }
        }
    }

    public static void StartIntro()  //por invoke não poder chamar static, tive que criar essa classe auxiliar, e o invoke que eu chamo para aumentar a etapa, é chamado no menu.
    {
        if (PlayerInfo.etapaAtual == 0)
        {
            for(int i = 0; i < intro[PlayerInfo.etapaAtual].imgOrTxt.Length; i++)
            {
                Debug.Log("Entrou no primeiro if");
                if (intro[PlayerInfo.etapaAtual].imgOrTxt[i].isImg()) {
                    ChatListControl.RenderizarImagem(intro[PlayerInfo.etapaAtual].imgOrTxt[i].img, true);
                }
                else
                {
                    ChatListControl.RenderizarTexto(intro[PlayerInfo.etapaAtual].imgOrTxt[i].txt, true);
                }
            }
            intro[PlayerInfo.etapaAtual].enviado = true;
            exploracao = false;
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("EnzoCamera"));
        }
        else
        {
            Debug.Log("erro etapa inicial");
        }
    }

    public void EnviarMsgResposta(string input) //primeira celula eh sempre a validacao, a segunda eh sempre a mensagem real
    {
        if (resposta[etapa].imgOrTxt[0].Equals(input)) //pesquisar Ignore Case
        {
            for (int i = 1; 1 < resposta[etapa].imgOrTxt.Length; i++)
            {
                if (resposta[etapa].imgOrTxt[i].isImg())
                {
                    ChatListControl.RenderizarImagem(resposta[etapa].imgOrTxt[i].img, false);
                }
                else
                {
                    ChatListControl.RenderizarTexto(resposta[etapa].imgOrTxt[i].txt, false);
                }
            }
            EnviarMsgFeedback();
        }
        else
        {
            ChatListControl.RenderizarTexto("Errou! - Faustão", false); // DEFINIR QUAL É A MENSAGEM DE ERRO
        }
    }

    public void EnviarMsgFeedback()
    {
        for(int i = 0; i < feedback[etapa].imgOrTxt.Length; i++)
        {
            if (feedback[etapa].imgOrTxt[i].isImg())
            {
                ChatListControl.RenderizarImagem(feedback[etapa].imgOrTxt[i].img, true);
            }
            else
            {
                ChatListControl.RenderizarTexto(feedback[etapa].imgOrTxt[i].txt, true);
            }
        }
        etapa++;
        PlayerInfo.AumentarEtapa();
        MainMenu.check = false;    // um boolean que é resetado toda vez que passa de fase, pra controlar, qdo o jogador deve descobrir algo.
        Invoke("EnviarMsgIntro", 5);
    }

    public void AumentarEtapaIntro()
    {
        etapa++;
        PlayerInfo.AumentarEtapa();
        EnviarMsgIntro();
    }

    public void EnviarMsgIntro() // a primeira vez que essa funcao for chamada, sera no menu, quando vc apertar no chat. Lembrar de habilitar a pista introdutoria no banco de dados
    {

        if (etapa == 1) //verificar pista cameraezno se foi habilitada
        {
            for(int i = 0; i < intro[etapa].imgOrTxt.Length; i++)
            {
                if (intro[etapa].imgOrTxt[i].isImg())
                {
                    ChatListControl.RenderizarImagem(intro[etapa].imgOrTxt[i].img, true);
                }
                else
                {
                    ChatListControl.RenderizarTexto(intro[etapa].imgOrTxt[i].txt, true);
                }
            }
            intro[etapa].enviado = true;
            exploracao = false;
        }
        else if (etapa == 2)
        {
            for (int i = 0; i < intro[etapa].imgOrTxt.Length; i++)
            {
                if (intro[etapa].imgOrTxt[i].isImg())
                {
                    ChatListControl.RenderizarImagem(intro[etapa].imgOrTxt[i].img, true);
                }
                else
                {
                    ChatListControl.RenderizarTexto(intro[etapa].imgOrTxt[i].txt, true);
                }
            }
            intro[etapa].enviado = true;
            exploracao = false;
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("MapaCalor"));
        }

        else if (etapa == 3)
        {
            for (int i = 0; i < intro[etapa].imgOrTxt.Length; i++)
            {
                if (intro[etapa].imgOrTxt[i].isImg())
                {
                    ChatListControl.RenderizarImagem(intro[etapa].imgOrTxt[i].img, true);
                }
                else
                {
                    ChatListControl.RenderizarTexto(intro[etapa].imgOrTxt[i].txt, true);
                }
            }
            intro[etapa].enviado = true;
            exploracao = false; // OBS: Essa etapa a introducao ja pede uma resposta, ja tem que estar liberado
        }
        else if (etapa == 4)
        {
            for (int i = 0; i < intro[etapa].imgOrTxt.Length; i++)
            {
                if (intro[etapa].imgOrTxt[i].isImg())
                {
                    ChatListControl.RenderizarImagem(intro[etapa].imgOrTxt[i].img, true);
                }
                else
                {
                    ChatListControl.RenderizarTexto(intro[etapa].imgOrTxt[i].txt, true);
                }
            }
            intro[etapa].enviado = true;
            exploracao = false;
        }
        else if (etapa == 5)
        {
            for (int i = 0; i < intro[etapa].imgOrTxt.Length; i++)
            {
                if (intro[etapa].imgOrTxt[i].isImg())
                {
                    ChatListControl.RenderizarImagem(intro[etapa].imgOrTxt[i].img, true);
                }
                else
                {
                    ChatListControl.RenderizarTexto(intro[etapa].imgOrTxt[i].txt, true);
                }
            }
            intro[etapa].enviado = true;
            exploracao = false;
        }

    }

    void Update()
    {

        if (etapa == 1)
        {
            //checa se o jogador descobriu as pistas para mandar uma mensagem automatica.
            int index = PlayerInfo.ProcurarPista("Criptografia1");
            if ((PlayerInfo.pistas[index].descoberta && PlayerInfo.pistas[index + 1].descoberta && PlayerInfo.pistas[index + 2].descoberta) && !(automatico[etapa].enviado)) //as pistas 1,2,3 = Criptografia1,Criptografia2,Criptografia3
            {
                for(int i = 0; i < automatico[etapa].imgOrTxt.Length; i++)
                {
                    if (automatico[etapa].imgOrTxt[i].isImg())
                    {
                        ChatListControl.RenderizarImagem(automatico[etapa].imgOrTxt[i].img, true);
                    }
                    else
                    {
                        ChatListControl.RenderizarTexto(automatico[etapa].imgOrTxt[i].txt, true);
                    }
                }
                automatico[etapa].enviado = true;
                MainMenu.TurnOnChatNofication();
                // PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("MapaCigarro"));  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat              
            }


            //Checa no update se A DICA JA FOI ENVIADA para poder liberar o pop-up
            int index1 = procurarPistaDetetive("Criptografia1"); // talvez, inicializar todas essas variaveis no start() para só acessar no update ou inves de sempre procurar.
            int index2 = index1 + 1;
            int index3 = index2 + 1;

            if ((pistasDetetive[index1].enviado) || (pistasDetetive[index2].enviado) || (pistasDetetive[index3].enviado))
            {
                exploracao = false;

            }

        }
        else if (etapa == 2)
        {
            int index = PlayerInfo.ProcurarPista("Digital1");
            if ((PlayerInfo.pistas[index].descoberta) && (PlayerInfo.pistas[index + 1].descoberta) && (PlayerInfo.pistas[index + 2].descoberta) && !(automatico[etapa].enviado))
            {
                for (int i = 0; i < automatico[etapa].imgOrTxt.Length; i++)
                {
                    if (automatico[etapa].imgOrTxt[i].isImg())
                    {
                        ChatListControl.RenderizarImagem(automatico[etapa].imgOrTxt[i].img, true);
                    }
                    else
                    {
                        ChatListControl.RenderizarTexto(automatico[etapa].imgOrTxt[i].txt, true);
                    }
                }
                automatico[etapa].enviado = true;
                MainMenu.TurnOnChatNofication();
                // PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("Suspeito1"));  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat
            }

            int index1 = procurarPistaDetetive("SimboloVirtusAtlas");

            if ((pistasDetetive[index1].enviado))
            {
                exploracao = false;
            }


        }

        //etapa 3 não tem nada automatico. só uma validação, lembrar só de deixar habilitado o pop-up.

        else if (etapa == 4)
        {
            int index = PlayerInfo.ProcurarPista("MapaNordeste");
            if ((PlayerInfo.pistas[index].descoberta) && !(automatico[etapa].enviado))
            {
                for (int i = 0; i < automatico[etapa].imgOrTxt.Length; i++)
                {
                    if (automatico[etapa].imgOrTxt[i].isImg())
                    {
                        ChatListControl.RenderizarImagem(automatico[etapa].imgOrTxt[i].img, true);
                    }
                    else
                    {
                        ChatListControl.RenderizarTexto(automatico[etapa].imgOrTxt[i].txt, true);
                    }
                }
                automatico[etapa].enviado = true;
                exploracao = false;
                MainMenu.TurnOnChatNofication();
                // PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("Cordel"));  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat
            }

            //aqui coincidentemente a mensagem automatica e a liberação de exploração são iguais
        }
        else if (etapa == 5)
        {
            int index = procurarPistaDetetive("PistaFinal");
            if ((pistasDetetive[index].enviado))
            {
                exploracao = false;
            }
        }
    }
}