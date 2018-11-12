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
        public string text; //"Virtus Atlas"
        public string text2; //"A organização se chama virtus atlas....
        public bool enviado;

        public Mensagem(string id, string text)
        {
            this.id = id;
            this.text = text;
            this.enviado = false;
        }

        public Mensagem(string id, string text, string text2)
        {
            this.id = id;
            this.text = text;
            this.text2 = text2;
            this.enviado = false;
        }
    }


    public static Mensagem[] dicas = new Mensagem[] { new Mensagem("teste", "você achou a pista teste", "você já achou essa pista"), };
    public static Mensagem[] feedback = new Mensagem[] { new Mensagem("id", "Parabéns você acertou"), };
    public static Mensagem[] intro = new Mensagem[] { new Mensagem("id", "Comecou game"), };
    public static Mensagem[] automatico = new Mensagem[] { new Mensagem("id", "mensagem automatica qdo descobrir as criptografias"), };
    public static Mensagem[] resposta = new Mensagem[] { new Mensagem("id", "resposta", "A resposta é: pega na minha e balança"), };
    public static pistaDetetive[] pistasDetetive = new pistaDetetive[] { new pistaDetetive("Criptografia1"), new pistaDetetive("Criptografia2"), new pistaDetetive("Criptografia3"), }; // esse array deve ser mapeado igualmente a o array de dicas.
    public static bool exploracao = false; // para bloquear e desbloquear o envio de RESPOSTAS
    public static int etapa = 0;
    public static string answer;


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

    public void EnviarMsgDicas(string id)
    {
        for (int i = 0; i <= dicas.Length; i++)
        {
            if (dicas[i].id.Equals(id))
            {
                if (dicas[i].enviado)
                {
                    Debug.Log(dicas[i].text2);
                    return;
                }
                else
                {
                    pistasDetetive[i].enviado = true;
                    Debug.Log(dicas[i].text);
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
            Debug.Log(intro[PlayerInfo.etapaAtual].text);
            intro[PlayerInfo.etapaAtual].enviado = true;
            exploracao = false;
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("EnzoCamera"));
        }
        else
        {
            Debug.Log("erro etapa inicial");
        }
    }

    public void EnviarMsgResposta(string input)
    {
        if (resposta[etapa].text.Equals(input))
        {
            Debug.Log(resposta[etapa].text2);
            EnviarMsgFeedback();
        }
        else
        {
            Debug.Log("Errou! - Faustão"); // DEFINIR QUAL É A MENSAGEM DE ERRO
        }
    }

    public void EnviarMsgFeedback()
    {
        Debug.Log(feedback[etapa].text);
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

        if (etapa == 1)
        {
            Debug.Log(intro[etapa].text);
            intro[etapa].enviado = true;
            exploracao = false;
        }
        else if (etapa == 2)
        {
            Debug.Log(intro[etapa].text);
            intro[etapa].enviado = true;
            exploracao = false;
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("MapaCalor"));
        }

        else if (etapa == 3)
        {
            Debug.Log(intro[etapa].text);
            intro[etapa].enviado = true;
            exploracao = false; // OBS: Essa etapa a introducao ja pede uma resposta, ja tem que estar liberado
        }
        else if (etapa == 4)
        {
            Debug.Log(intro[etapa].text);
            intro[etapa].enviado = true;
            exploracao = false;
        }
        else if (etapa == 5)
        {
            Debug.Log(intro[etapa].text);
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
                Debug.Log(automatico[etapa].text);
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
                Debug.Log(automatico[etapa].text);
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
                Debug.Log(automatico[etapa].text);
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