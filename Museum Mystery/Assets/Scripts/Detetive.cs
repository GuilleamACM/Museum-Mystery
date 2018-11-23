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

    public Boolean firstTimePopup = true;
    public GameObject popupAllow;
    public GameObject popupDeny;
    public GameObject popupTutorial;
    public GameObject popupEncaminhado;
    public static Mensagem[] dicas = new Mensagem[4]; // qdo o jogador envia uma pista habilitada ao detetive, o detetive responde com um breve texto, falando melhor sobre a pista enviada.
    public static Mensagem[] feedback = new Mensagem[] { new Mensagem("0",new ImgOrTxt[]{ new ImgOrTxt("Está foi a única imagem de Sanfonatti que conseguimos restaurar das câmeras desegurança. Tente analisar o local com as ferramentas que lhe demos, talvez vocêencontre algo relevante."),
                                                         new ImgOrTxt ("Estranho, acho que nunca vi essas marcas antes, elas parecem ter sido colocadas alipelo próprio Sanfonatti, continue procurando, talvez isso signifique algo."),
                                                         //new ImgOrTxt (""), imagem heraldica
                                                         //new ImgOrTxt (""), imagem texto e numero
                                                         new ImgOrTxt ("Nenhum de nossos especialistas parece saber o que essas marcas significam. Vocêacha que consegue descobrir?"),
                                                         new ImgOrTxt ("Me perdoe, jovem, mas estou um tanto ocupado no momento para lhe ajudar. Vou lhe mandar uma ferramenta que pode ser útil www.google.com")
                                                         }),
                                                         new Mensagem("1",new ImgOrTxt[]{ new ImgOrTxt("Este é um mapa de calor que traçamos das atividades de Sanfonatti no museu, asáreas com cor mais intensa indicam os setores de maior atividade.Recomendo você tentar investigar essas áreas."),
                                                                                          new ImgOrTxt("De acordo com o banco de dados de digitas da nossa agência. Essa digital pertence a Atila Saverin. Vejamos: Atila Saverin, 27 anos, solteiro, sem antecedentes criminais, residente na cidade de Exu, trabalha como vendedor em uma loja de produtos agropecuários."),
                                                                                          new ImgOrTxt("Deixe-me ver. Essa digital pertence a Rajish Al-Habib.25 anos, casado.Humm, tem ficha na delegacia por porte ilegal de arma. Nasceu nos emirados árabes, mas reside na cidade do Recife, trabalha como professor de história em escola pública."),
                                                                                          new ImgOrTxt("Pelo que consegui verificar no nosso banco de dados de digitais, essa digital pertencea Nariadna Gleycielly. Nariadna, 22 anos, casada, sem antecedentes criminais, reside em Salgueiro e é dona de uma loja de tecidos."),
                                                                                          new ImgOrTxt(" Estranho esse símbolo. Pelo que eu consegui analisar aqui, ele não foi feito por Sanfonatti.Talvez esteja relacionado com algum dos suspeitos, pode significar algo.")

                                                         }),
                                                         new Mensagem("2", new ImgOrTxt[]{ new ImgOrTxt("Veja se você consegue descobrir o que pode ser a continuação dessa frase. Ela pode conter alguma dica de onde ele pode ter deixado algo no museu."),
                                                                                           new ImgOrTxt("Esse Sanfonatti parece realmente gostar de escrever em enigmas. A essa altura eu tenho certeza que essa frase significa algo. Tente descobrir o que significa."),
                                                                                           new ImgOrTxt("O cordel foi encontrado por um funcionário do museu no mesmo local onde você encontrou a frase deixada por Sanfonatti. É bem provável que tenha algo escondido nele.Você consegue descobrir o que é?"),
                                                                                           new ImgOrTxt("Sanfonatti parece ter nos deixado uma coordenada escrita nesse mapa.39°20'37.9'WMe avise se você descobrir qual é a outra para que possamos mandar algum agenteaté o local descobrir o que isso significa.")


                                                         }),
                                                         new Mensagem("3", new ImgOrTxt[]{ new ImgOrTxt("Essa carta foi escrita por Raimundo Jacó, e foi encontrada no local indicado pelas coordenadas deixadas por Sanfonatti.Jacó faz parecer que estava sendo perseguido por ter descoberto algo, logo antes de ter sido assassinado."),
                                                                                           new ImgOrTxt("Esta é a área do museu em que não existe nenhuma imagem da câmera de segurança de Sanfonatti.Deve haver algo de importante aí."),
                                                                                           new ImgOrTxt("Eu acredito que a esse ponto você já deve entender como a mente de Sanfonattifunciona melhor do que eu.Essa é a última pista que nos temos. Tente descobrir o que ela significa, é possívelque tenha algo que ajude a decifrar nas informações que você já coletou antes. Boa sorte, jovem"),

                                                         })
    };
    public static Mensagem[] intro = new Mensagem[4]; // introducoes do ciclo
    public static Mensagem[] automatico = new Mensagem[] { new Mensagem("0", new ImgOrTxt[]{ new ImgOrTxt("Olá, novamente jovem. Enquanto você investigava aí no museu, um dos nossas investigadores encontrou uma bolsa jogada no lado de fora do museu perto de uma lixeira. Era a mesma bolsa que Sanfonatti estava usando quando visitou o museu. Estranho não acha? Bem, dentro da bolsa encontramos este pedaço de papel"),
                                                                                             //imagem do mapa
                                                                                             new ImgOrTxt("E este livro"),
                                                                                             //foto do livro
                                                                                             new ImgOrTxt("Não sabemos ainda o que significa ou se é relevante. Mas, de qualquer forma, é uma pista, então achei que seria útil enviar."),
                                                                                             new ImgOrTxt("Essas marcas de novo, estranho. Essa outra palavra embaixo, também parece ter sido feita por Sanfonatti.Realmente estranho.Bem, acha que você consegue descobrir o que essas marcas estranhas significa? Se descobrir, por favor me envie."),


                                                        }),
                                                        new Mensagem("1", new ImgOrTxt[]{ new ImgOrTxt("Bem, pelo que conseguimos coletar das suas pistas, temos 3 suspeitos principais,Rajish Al-Habib, Atila Saverin, e Nariadna Gleycielly, mas só, nossos especialistas não conseguiram extrair nenhuma boa informação disso, é muito pouco para conseguirmos avaliar algo, precisamos de algo maior para prosseguir."),
                                                                                          new ImgOrTxt("Tem também a questão desse símbolo que você encontrou, ele me parece ser relevante."),
                                                                                          new ImgOrTxt("Humm, me desculpe, mas não tem muito que eu esteja conseguindo fazer por aqui. Talvez você consiga encontrar alguma informação sobre os suspeitos que possa ajudar."),
                                                                                          new ImgOrTxt("Tente descobrir alguma informação que possa levar a algum dos suspeitos em particular, algo que possa está relacionado ao caso. Acha que consegue?"),
                                                                                          new ImgOrTxt("Me envie uma mensagem se encontrar algo importante.")

                                                        }),
                                                        new Mensagem("2", new ImgOrTxt[]{ new ImgOrTxt("Humm. Esse local que você encontrou agora. Um dos funcionários do museu havia encontrado isto escondido aí."),
                                                                                            //imagem do cordel
                                                                                          new ImgOrTxt("Não parecia relevante antes, era apenas um pedaço de papel com um cordel escrito, mas seria coincidência demais isso estar no mesmo lugar."),
                                                                                          new ImgOrTxt("Essas marcações deixadas no mapa. Elas parecem apontar uma coordenada geográfica."),
                                                                                          new ImgOrTxt("Humm"),
                                                                                          new ImgOrTxt("Se quisermos encontrar o lugar para onde isso aponta, precisaremos de outra coordenada"),
                                                                                          new ImgOrTxt("Por favor, se descobrir qual é, me avise que eu mandarei um dos nossos agentes averiguar o local.")

                                                        }),
                                                         new Mensagem("3", new ImgOrTxt[]{ new ImgOrTxt("Humm, o que será que isso significa?Esta é a última pista que nos resta. Talvez descobrir o que ela quer dizer possa nos   ajudar a descobrir o que aconteceu com Sanfonatti."),
                                                                                           new ImgOrTxt("Bem jovem. Eu sei que não temos muito para seguir em frente. Mas, se você descobrir onde Sanfonatti pode estar, ou se ele deixou alguma mensagem que possa nos dizer onde procurar, por favor me envie."),
                                                                                           new ImgOrTxt("Bem, jovem. Acho que você tem o direito de saber."),
                                                                                           new ImgOrTxt("A nossa agência de investigação, a Alítheia, tem trabalhado, assim como Sanfonatti, em tentar descobrir a verdade sobre essas informações que tem sido manipuladas por essa sociedade secreta que controla as grandes corporações do mundo, para isso a agência utiliza o programa de seleção de novos investigadores, para tentar encontrar pessoas com o mesmo interesse, afinal, essa sociedade é muito poderosa."),
                                                                                           new ImgOrTxt("Foi apenas recentemente que descobrimos sobre Sanfonatti. Queríamos chamar ele para a nossa agência, mas infelizmente não fomos rápidos o suficiente. Não tínhamos como saber que a Virtus Atlas estava por trás disso, e já estava tão próxima de encontrá-lo, e ele, sozinho, não teve muita opção. Maldição, eles parecem estar sempre um passo na nossa frente."),
                                                                                           new ImgOrTxt("Enfim, pelo que você descobriu, me parece que Sanfonatti estava tentando deixar alguma mensagem para que alguém capaz de encontrá-la pudesse continuar o que ele começou, e parece que além de tudo ele descobriu o que nossa agência era realmente nos últimos instantes. "),
                                                                                           new ImgOrTxt("É, a Alítheia é a agência da verdade."),
                                                                                           new ImgOrTxt("Bem. Agora que você já sabe a verdade, espero que você queira se juntar a nossa agência. Eu vejo muito futuro em você."),
                                                                                           new ImgOrTxt("Pois é, não ache que seu trabalho foi em vão jovem."),
                                                                                           new ImgOrTxt("Apesar de não termos o paradeiro exato, as informações que você conseguiu não só podem nos ajudar a encontrar o pobre Sanfonatti, como também agora nós temos o nome de um dos capangas da Virtus Atlas para seguir."),
                                                                                           new ImgOrTxt("Bem!"),
                                                                                           new ImgOrTxt("Acho que esse caso do museu encerra por aqui."),
                                                                                           new ImgOrTxt("Então jovem, está preparado para descobrir as verdades sobre o mundo?"),

                                                         })
    }; // msgs automaticas que o detetive manda ao jogador completar alguma coisa
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
        Mensagem msg1 = new Mensagem("0", new ImgOrTxt[17] { new ImgOrTxt("Olá, você deve ser um dos novatos, não? Eu sou Jerry, detetive chefe responsável pela supervisão do caso em questão."),
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
                                    new ImgOrTxt("Então é isso aí. A última evidência que temos de Sanfonatti é esta"),
                                    //imagem da camera de segurança
                                    new ImgOrTxt("As gravações da câmera de segurança do museu estão corrompidas, e isso foi tudo que conseguimos de relevante."),
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


        Mensagem msg3 = new Mensagem("2", new ImgOrTxt[7] { new ImgOrTxt("Virtus Atlas? Curioso.Isso é, ou pelo menos deveria ser, uma lenda."),
                                                            new ImgOrTxt("É uma história antiga que contavam nas cidades do interior. Alguma coisa sobre umgrupo de poderosos do ramo da agropecuária, que pretendia dominar todo omercado controlando toda a produção da região."),
                                                            new ImgOrTxt("Ninguém nunca descobriu o quanto disso era verdade ou não, só se sabe que issorendeu várias histórias ao longo dos anos, uma delas era algo sobre elescontratarem os capangas para fazerem o trabalho sujo deles, e todos esses erammarcados como gado."),
                                                            new ImgOrTxt("Talvez daí que tenha surgido a tatuagem desse cara, não queele seja membro de um grupo secreto nem nada do tipo.Como eu disse nada dissofoi comprovado, mas existe maluco pra tudo não é mesmo."),
                                                            new ImgOrTxt("Bem, vou mandar isso para os especialistas aqui na agência para ver se eles descobrem algo mais sobre esse Átila Saverin."),
                                                            new ImgOrTxt("Enquanto eles veem isso, eu estive pensando sobre a frase que você me envioualgum tempo atrás. Especificamente a parte em que ele fala. “Seu nome não foiesquecido nas”... "),
                                                            new ImgOrTxt("isso me lembra uma música de Luiz Gonzaga, e a forma como afrase foi cortada, me faz pensar que isso possa ser relevante de alguma forma, você poderia tentar descobrir.")


        });
        intro[2] = msg3;

        Mensagem msg4 = new Mensagem("3", new ImgOrTxt[10] { new ImgOrTxt("Bom trabalho jovem.Vou enviar um agente para lá agora mesmo.O local apontado pelas coordenadas fica na cidade de Serrita."),
                                                            new ImgOrTxt("Nosso agente está entrando em contato nesse momento com a força policial dacidade"),
                                                            new ImgOrTxt("humm"),
                                                            new ImgOrTxt("Eles encontraram isso..."),
                                                            //new ImgOrTxt(),  foto da carta de jacó
                                                            new ImgOrTxt("Esta carta, os especialistas da nossa agência em Serrita identificaram que ela foi de fato escrita por Raimundo Jacó, primo de Luiz Gonzaga. Ele foi assassinado na década de 50. A sua morte foi sempre um mistério. "),
                                                            new ImgOrTxt("Ninguém nunca conseguiu comprovar o que realmente aconteceu. Mas, a forma como ele escreveu, e as pistas de Sanfonatti que nos levaram até a Virtus Atlas… Será que está tudo conectado?"),
                                                            new ImgOrTxt("Qual a relação entre os casos? Isso é tudo muito estranho. Mas bem. Nós ainda temos um caso para resolver. E isso não exatamente nos diz o que aconteceu com Sanfonatti não é mesmo?"),
                                                            new ImgOrTxt("Precisamos seguir em frente.Enfim. Enquanto descobríamos sobre a carta, nossos especialistas conseguiramreconstruir tudo o que era possível das imagens das câmeras de segurança."),
                                                            new ImgOrTxt("O mais estranho é que, tem um setor do museu onde, sempre que Sanfonatti passa por ele,a imagem simplesmente some.Elas não foram apenas corrompidas.É como se alguém tivesse bloqueado completamente o sinal da câmera nesta área."),
                                                            new ImgOrTxt("Você deve investigar este local. Lá deve haver algo que possa nos dar mais respostas.Tome, o setor do museu é este aqui. "),
                                                            //new ImgOrTxt(""); foto do mapa do museu
        });
        intro[3] = msg4;

        Mensagem dicas0 = new Mensagem("2", new ImgOrTxt[2] { new ImgOrTxt("Descobre ai trouxa"), new ImgOrTxt("hahaha") });
        dicas[0] = dicas0;

    }

    public void getTextInput(string input)
    {
        answer = input;
    }


    public void sendButton()
    {
        if (exploracao) // pop-up de que você não pode responder, nesse momento voce deve explorar
        {
            popupDeny.SetActive(true);
            Debug.Log("Pop-up de você não pode enviar mensagem agora");
            Invoke("desativarPopupDeny",3);
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
                popupAllow.SetActive(true);
                Debug.Log("escreva alguma coisa brother");
                Invoke("desativarPopupAllow", 3);
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

    public void EncaminharPista() //ela só envia a mensagem do detetive (resposta para quando o jogador envia uma pista), só envia textos e as dicas são sempre uma mensagem
    {
        int img = MainMenu.staticRefBotao;
        Debug.Log(img);
        Debug.Log(dicas[img]);

        if (dicas[img].enviado)
        {
            ChatListControl.RenderizarImagem(img, false);
            StaticCoroutine.DoCoroutineDelayMsgDetetive("Você perguntou isso antes");
            ChatListControl.closePanelDicasStatic();
            
           
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
            
        }

        popupEncaminhado.SetActive(true);
        Invoke("desativarPopupEncaminhado",2);
        MainMenu.TurnOnChatNofication();
    }

    public void desativarPopupEncaminhado()
    {
        popupEncaminhado.SetActive(false);
    }

    public void ativarTutorialPopup()
    {
        if (firstTimePopup)
        {
            popupTutorial.SetActive(true);
            Invoke("desativarPopupTutorial", 5);
            firstTimePopup = false;
        }

    }

    public void desativarPopupTutorial()
    {
        popupTutorial.SetActive(false);
        Destroy(popupTutorial);
    }

    public void desativarPopupDeny()
    {
        popupDeny.SetActive(false);
    }

    public void desativarPopupAllow()
    {
        popupAllow.SetActive(false);
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
            exploracao = true;
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
            exploracao = true;
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
            exploracao = true;
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
            exploracao = true;
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
