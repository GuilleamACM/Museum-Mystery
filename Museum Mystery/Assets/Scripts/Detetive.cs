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


    public static Mensagem[] dicas = new Mensagem[4]; // qdo o jogador envia uma pista habilitada ao detetive, o detetive responde com um breve texto, falando melhor sobre a pista enviada.
    public static Mensagem[] feedback = new Mensagem[] { new Mensagem("0",new ImgOrTxt[]{ new ImgOrTxt("Está foi a única imagem de Sanfonatti que conseguimos restaurar das câmeras desegurança. Tente analisar o local com as ferramentas que lhe demos, talvez vocêencontre algo relevante."),
                                                         new ImgOrTxt ("Estranho, acho que nunca vi essas marcas antes, elas parecem ter sido colocadas alipelo próprio Sanfonatti, continue procurando, talvez isso signifique algo."),
                                                         //new ImgOrTxt (""), imagem heraldica
                                                         //new ImgOrTxt (""), imagem texto e numero
                                                         new ImgOrTxt ("Nenhum de nossos especialistas parece saber o que essas marcas significam. Vocêacha que consegue descobrir?"),
                                                         new ImgOrTxt ("Me perdoe, jovem, mas estou um tanto ocupado no momento para lhe ajudar. Vou lhe mandar uma ferramenta que pode ser útil www.google.com")

                                                       })};
    public static Mensagem[] intro = new Mensagem[4]; // introducoes do ciclo
    public static Mensagem[] automatico = new Mensagem[4]; // msgs automaticas que o detetive manda ao jogador completar alguma coisa
    public static Mensagem[] resposta = new Mensagem[4]; // a posicao 0 desse array eh a palavra para validar, e as posicoes adiante sao as mensagens que o jogador enviara para o detetive caso tenha acertado a validacao
    public static bool exploracao = false; // para bloquear e desbloquear o envio de RESPOSTAS
    public static int etapa = 0;
    public static string answer; // msg escrita no input
    public static int imgDicas; // referencia da img clicada em anexos.
    public static pistaDetetive[] pistasDetetive = new pistaDetetive[] { new pistaDetetive("EnzoCamera"),new pistaDetetive("Criptografia1"), new pistaDetetive("Criptografia2"),
                                                                         new pistaDetetive("Criptografia3"), new pistaDetetive("MapaCigarro"), new pistaDetetive("Suspeito1"),
                                                                         new pistaDetetive("Suspeito2"), new pistaDetetive("Suspeito3"),
                                                                         new pistaDetetive("MapaCalor"), new pistaDetetive("Digital1"), new pistaDetetive("Digital2"),
                                                                         new pistaDetetive("Digital3"), new pistaDetetive("SimboloVirtusAtlas"), new pistaDetetive("PistaFinal"),
                                                                         new pistaDetetive("MapaNordeste"), new pistaDetetive("Chapeu"), new pistaDetetive("QuebradaSertao"),
                                                                         new pistaDetetive("Cordel"),}; // esse array deve ser mapeado igualmente a o array de dicas.

    void Awake()
    {
        Mensagem msg1 = new Mensagem("0", new ImgOrTxt[16] { new ImgOrTxt("Olá, você deve ser um dos novatos, não? Eu sou Jerry, detetive chefe responsável pela supervisão do caso em questão."),
                                    new ImgOrTxt("Infelizmente, não posso estar aí com você neste momento, tenho que cuidar de toda a papelada da investigação aqui no escritório, mas não se preocupe, tentarei ajudá - lo o máximo que puder daqui."),
                                    new ImgOrTxt("Muito bem, o negócio é o seguinte: duas semanas atrás um jovem turista de 22 anos, de nome Enzo Sanfonatti, veio para a cidade do Recife."),
                                    new ImgOrTxt("Tudo parecia normal até que alguns dias atrás ele simplesmente desapareceu, e ninguém parece fazer ideia do que aconteceu."),
                                    new ImgOrTxt("Nossos especialistas estão neste momento trabalhando para conseguir novas informações. Tentarei manter você o mais atualizado possível com o que for chegando por aqui."),
                                    new ImgOrTxt("O último lugar em que Sanfonatti foi visto foi no Museu Cais do Sertão, e é lá onde começa seu trabalho"),
                                    new ImgOrTxt("Para isso, Instalamos em seu dispositivo algumas ferramentas para auxiliar na investigação. Preste atenção, isso vai ser importante. A câmera do seu celular, agora, pode utilizar uma luz negra, capaz de revelar coisas que os olhos humanos não podem ver."),
                                    new ImgOrTxt("Adicionalmente, instalamos também um banco de dados com pistas e informações em estado bruto recolhidas até agora sobre o caso."),
                                    new ImgOrTxt("Como nós, infelizmente não pudemos enviar mais agentes para ir com você ao museu, vai caber a você descobrir o significado dessas pistas sozinho."),
                                    new ImgOrTxt("Como eu falei antes, eu vou tentar ajudar o máximo que posso, porém estou muito ocupado, então sempre que possível, tente utilizar seus conhecimentos e outras coisas ao seu alcance para solucionar esse caso."),
                                    new ImgOrTxt("Vale salientar que eu tenho acesso ao seu banco de dados de pistas, mas as deduções que você possa vir a fazer vai ser necessário que você as envie para mim, para eu poder comunicá-las aos nossos especialistas."),
                                    new ImgOrTxt("Mas não se preocupe, sempre que eu achar que tem algo relevante a ser deduzido eu pergunto."),
                                    new ImgOrTxt("Lembre-se: se achar alguma evidência, não toque, além de ser contra as práticas do museu, alguma informação pode acabar sendo perdida, então cuidado."),
                                    new ImgOrTxt("Então é isso aí. A última evidência que temos de Sanfonatti é esta (imagem da câmera de segurança), as gravações da câmera de segurança do museu estão corrompidas, e isso foi tudo que conseguimos de relevante."),
                                    new ImgOrTxt("Nossos especialistas estão trabalhando em restaurá-las. Mas até lá sugiro que comece tentando seguir seus passos e ver se encontra algo relevante."),
                                    new ImgOrTxt("Boa sorte jovem!")
                                    //falta a imagem de introdução 
                                    });
        intro[0] = msg1;


        Mensagem msg2 = new Mensagem("1", new ImgOrTxt[7] { new ImgOrTxt("Humm, isso parece mais sério do que eu imaginava. Ele parece indicar que estavasendo perseguido ou algo assim. Isso muda algumas coisas, deixe-me ver o que euconsigo fazer."),
                                                            new ImgOrTxt("Vejamos. Não conseguimos reconstruir muitas imagens satisfatórias das câmeras desegurança ainda, o processo é um tanto complicado, mas conseguimos identificaralgumas coisas que talvez possam ajudar."),
                                                            new ImgOrTxt("Traçamos um mapa que indica os lugares de maior atividade de Sanfonatti nomuseu, em particular aqueles onde havia alguém por perto que possamosconsiderar um suspeito."),
                                                            new ImgOrTxt("O horário em que ele foi visitar o museu não era de muito movimento, então não tinha muita gente para observar, mas devido as câmeras estarem corrompidas, nãoé possível identificar nenhuma dessas pessoas."),
                                                            new ImgOrTxt("Tente investigar esses lugares, talvez seja possível encontrar alguma informaçãoque possa nos levar a um suspeito."),
                                                            new ImgOrTxt("Vou atualizar o seu software para que ele possa ter mais precisão."),
                                                            new ImgOrTxt("Ah, desculpe, já ia esquecendo. Aqui está o mapa:")
                                                            //falta a imagem mapa de calor
        });
        intro[1] = msg2;


        Mensagem msg3 = new Mensagem("2", new ImgOrTxt[2] { new ImgOrTxt("Descobre ai trouxa"), new ImgOrTxt("hahaha") });
        dicas[0] = msg3;
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
                Debug.Log("escreva alguma coisa brother");
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

    public void EnviarMsgDicas(int img) //ela só envia a mensagem do detetive (resposta para quando o jogador envia uma pista), só envia textos e as dicas são sempre uma mensagem
    {
        Debug.Log(img);
        Debug.Log(dicas[img]);

        if (dicas[img].enviado)
        {
            ChatListControl.RenderizarImagem(img, false);
            StaticCoroutine.DoCoroutineDelayMsgDetetive("Você perguntou isso antes");
            ChatListControl.closePanelDicasStatic();
            return;
        }
        else
        {
            dicas[img].enviado = true;
            pistasDetetive[img].enviado = true;
            ChatListControl.RenderizarImagem(img, false);
            for (int i = 0; i < dicas[img].imgOrTxt.Length; i++)
            {
                StaticCoroutine.DoCoroutineDelayMsgDetetive(dicas[img].imgOrTxt[i].txt);                              
            }
            ChatListControl.closePanelDicasStatic();
            return;
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
                //PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("Cordel"));  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat
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