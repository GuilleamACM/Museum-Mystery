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
        public Boolean isLeft;

        public ImgOrTxt(int img, Boolean isLeft)
        {
            this.img = img;
            this.txt = null;
            this.isLeft = isLeft;
        }

        public ImgOrTxt(string text, Boolean isLeft)
        {
            this.img = -1;
            this.txt = text;
            this.isLeft = isLeft;
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
    public GameObject popupError;
    public GameObject popupAllow;
    public GameObject popupDeny;
    public GameObject popupTutorial;
    public GameObject popupEncaminhado;
    public GameObject popupAskAgain;
    public static GameObject popupAskAgainStatic;
    public GameObject popupAskAgain2;

    public static Mensagem[] dicas = new Mensagem[21]; // qdo o jogador envia uma pista habilitada ao detetive, o detetive responde com um breve texto, falando melhor sobre a pista enviada.
    
    public static Mensagem[] intro = new Mensagem[6]; // introducoes do ciclo
    public static Mensagem[] automatico; // msgs automaticas que o detetive manda ao jogador completar alguma coisa

    public static Mensagem[] resposta = new Mensagem[] { new Mensagem("0", new ImgOrTxt[] { new ImgOrTxt("Parece que já me encontraram, mas eu não vou deixar que escondam a verdade. O seu nome não foi esquecido nas", false),
                                                                                            new ImgOrTxt("Olá Jerry. A série de letras e números que eu encontrei no disco, eram parte de um link de um vídeo. ", false),
                                                                                            new ImgOrTxt("O vídeo, continha a chave para traduzir aquelas marcações estranhas que Enzo havia deixado nas obras.", false),
                                                                                            new ImgOrTxt("Pelo que eu consegui identificar, elas traduzem para: “Parece que já me encontraram, mas eu não vou deixar que escondam a verdade. O seu nome não foi esquecido nas”", false), }),
                                                         new Mensagem("1", new ImgOrTxt[] { new ImgOrTxt("Virtus Atlas", false),
                                                                                            new ImgOrTxt("Jerry, eu encontrei em um perfil online de um dos suspeitos uma foto, onde ele aparece com uma tatuagem.", false), 
                                                                                            new ImgOrTxt("Essa tatuagem é o mesmo símbolo que estava na porta com as marcas de gado. Nessa mesma tatuagem há uma inscrição, que eu consegui traduzir para “Virtus Atlas”.", false),
                                                                                            new ImgOrTxt("Você faz alguma ideia do que significa?", false), }),
                                                         null,
                                                         new Mensagem("2", new ImgOrTxt[] { new ImgOrTxt("7°51'34.8\"S", false),
                                                                                            new ImgOrTxt("Jerry, acho que encontrei a outra coordenada.", false),
                                                                                            new ImgOrTxt("Ela estava escondida no cordel que você me mandou.", false),
                                                                                            new ImgOrTxt("A coordenada é: 7°51'34.8\"S", false)}),
                                                         new Mensagem("3", new ImgOrTxt[] { new ImgOrTxt("Agência da verdade", false),
                                                                                            new ImgOrTxt("Jerry, a mensagem que Enzo havia deixado, era um link para uma página contendo alguns documentos escritos por ele.", false),
                                                                                            new ImgOrTxt("Nesses documentos ele explica tudo o que ele descobriu sobre a Virtus Atlas, e como eles têm manipulado diversas corporações para esconder a verdade sobre as suas ações, e manter o poder.", false),
                                                                                            new ImgOrTxt("Enzo estava investigando o que havia acontecido com um familiar, quando se deparou com o nome Virtus Atlas, ele começou a investigá-los, mas eles acabaram descobrindo e começaram a perseguí-lo pois ele sabia demais, assim como aconteceu com Jacó.", false),
                                                                                            new ImgOrTxt("Enzo tentou desmascará-los mas não conseguiu. A última coisa que ele disse foi para procurar a “Agência da verdade”.", false),
                                                                                            new ImgOrTxt("O que isso significa?!", false),}),
                                                        }; // a posicao 0 desse array eh a palavra para validar, e as posicoes adiante sao as mensagens que o jogador enviara para o detetive caso tenha acertado a validacao
    public static bool exploracao = false; // para bloquear e desbloquear o envio de RESPOSTAS
    public static int etapa = 0;
    public InputField inputField;
    public static string answer; // msg escrita no input
    public static int automatic = 0;
    public static int imgDicas; // referencia da img clicada em anexos.
    public static pistaDetetive[] pistasDetetive = new pistaDetetive[] { new pistaDetetive("enzoCamera"),new pistaDetetive("discoContinental"), new pistaDetetive("musicBox"),
                                                                         new pistaDetetive("gonzaga"), new pistaDetetive("mapaCigarro"), new pistaDetetive("livro"), new pistaDetetive("albumLuizCostas"), new pistaDetetive("albumLuizCostas2"),
                                                                         new pistaDetetive("mapaCalor"), new pistaDetetive("faca"), new pistaDetetive("santo"),
                                                                         new pistaDetetive("xilogravuraMoldura"), new pistaDetetive("caixaPoesia"), new pistaDetetive("suspeitos"), new pistaDetetive("frase"), new pistaDetetive("cacto"),
                                                                         new pistaDetetive("mapaNordeste"), new pistaDetetive("cordel"),new pistaDetetive("cartaJaco"),new pistaDetetive("mapaEnzo"), new pistaDetetive("quadroBoneco"),}; // esse array deve ser mapeado igualmente a o array de dicas.

    void Awake()
    {
        popupAskAgainStatic = popupAskAgain;
        //carregando introducoes

        Mensagem msg0 = new Mensagem("0", new ImgOrTxt[20] { new ImgOrTxt("Olá, você deve ser um dos novatos, não? Eu sou Jerry, detetive chefe responsável pela supervisão do caso em questão.", true),
                                    new ImgOrTxt("Infelizmente, não posso estar aí com você neste momento, tenho que cuidar de toda a papelada da investigação aqui no escritório, mas não se preocupe, tentarei ajudá-lo o máximo que puder daqui.", true),
                                    new ImgOrTxt("Muito bem, o negócio é o seguinte: duas semanas atrás um jovem turista de 22 anos, de nome Enzo Sanfonatti, veio para a cidade do Recife.", true),
                                    new ImgOrTxt("Tudo parecia normal até que alguns dias atrás ele simplesmente desapareceu, e ninguém parece fazer ideia do que aconteceu.", true),
                                    new ImgOrTxt("Nossos especialistas estão neste momento trabalhando para conseguir novas informações. Tentarei manter você o mais atualizado possível com o que for chegando por aqui.", true),
                                    new ImgOrTxt("O último lugar em que Sanfonatti foi visto foi no Museu Cais do Sertão, e é lá onde começa seu trabalho", true),
                                    new ImgOrTxt("Para isso, Instalamos em seu dispositivo algumas ferramentas para auxiliar na investigação. Preste atenção, isso vai ser importante. A câmera do seu celular, agora, pode utilizar uma luz negra, capaz de revelar coisas que os olhos humanos não podem ver.", true),
                                    new ImgOrTxt("Adicionalmente, instalamos também um banco de dados com pistas e informações em estado bruto recolhidas até agora sobre o caso.", true),
                                    new ImgOrTxt("Como nós, infelizmente não pudemos enviar mais agentes para ir com você ao museu, vai caber a você descobrir o significado dessas pistas sozinho.", true),
                                    new ImgOrTxt("Como eu falei antes, eu vou tentar ajudar o máximo que posso, porém estou muito ocupado, então sempre que possível, tente utilizar seus conhecimentos e outras coisas ao seu alcance para solucionar esse caso.", true),
                                    new ImgOrTxt("Vale salientar que eu tenho acesso ao seu banco de dados de pistas, mas as deduções que você possa vir a fazer vai ser necessário que você as envie para mim, para eu poder comunicá-las aos nossos especialistas.", true),
                                    new ImgOrTxt("Mas não se preocupe, sempre que eu achar que tem algo relevante a ser deduzido eu pergunto.", true),
                                    new ImgOrTxt("Lembre-se: se achar alguma evidência, não toque, além de ser contra as práticas do museu, alguma informação pode acabar sendo perdida, então cuidado.", true),
                                    new ImgOrTxt("Então é isso aí. A última evidência que temos de Sanfonatti é esta", true),
                                    new ImgOrTxt(PlayerInfo.ProcurarPista("enzoCamera"), true), //camera segurança
                                    new ImgOrTxt("As gravações da câmera de segurança do museu estão corrompidas, e isso foi tudo que conseguimos de relevante.", true),
                                    new ImgOrTxt("Nossos especialistas estão trabalhando em restaurá-las. Mas até lá sugiro que comece tentando seguir seus passos e ver se encontra algo relevante.", true),
                                    new ImgOrTxt("Boa sorte jovem!", true),
                                    new ImgOrTxt("Obrigado Jerry.", false),
                                    new ImgOrTxt("Vou começar a investigar imediatamente.", false)
                                    //falta a imagem de introdução 
                                    });
        intro[0] = msg0;


        Mensagem msg1 = new Mensagem("1", new ImgOrTxt[] { new ImgOrTxt("Humm, isso parece mais sério do que eu imaginava. Ele parece indicar que estava sendo perseguido ou algo assim. Isso muda algumas coisas, deixe-me ver o que eu consigo fazer.", true),
                                                            new ImgOrTxt("Vejamos. Não conseguimos reconstruir muitas imagens satisfatórias das câmeras de segurança ainda, o processo é um tanto complicado, mas conseguimos identificar algumas coisas que talvez possam ajudar.", true),
                                                            new ImgOrTxt("Traçamos um mapa que indica os lugares de maior atividade de Sanfonatti no museu, em particular aqueles onde havia alguém por perto que possamos considerar um suspeito.", true),
                                                            new ImgOrTxt("O horário em que ele foi visitar o museu não era de muito movimento, então não tinha muita gente para observar, mas devido as câmeras estarem corrompidas, não é possível identificar nenhuma dessas pessoas.", true),
                                                            new ImgOrTxt("Tente investigar esses lugares, talvez seja possível encontrar alguma informação que possa nos levar a um suspeito.", true),
                                                            new ImgOrTxt("Vou atualizar o seu software para que ele possa ter mais precisão.", true),
                                                            new ImgOrTxt("Ah, desculpe, já ia esquecendo. Aqui está o mapa:", true),
                                                            new ImgOrTxt(PlayerInfo.ProcurarPista("mapaCalor"), true), // mapa de calor
                                                             new ImgOrTxt("Ok Jerry, vamos ver se os nossos suspeitos deixaram algum rastro que possamos seguir.", false),
        });
        intro[1] = msg1;


        Mensagem msg2 = new Mensagem("2", new ImgOrTxt[] {  new ImgOrTxt("Virtus Atlas? Curioso", true),
                                                            new ImgOrTxt("Isso é, ou pelo menos deveria ser, uma lenda.", true),
                                                            new ImgOrTxt("Uma lenda?", false),
                                                            new ImgOrTxt("Isso mesmo.", true),
                                                            new ImgOrTxt("É uma história antiga que contavam nas cidades do interior. Alguma coisa sobre um grupo de poderosos do ramo da agropecuária, que pretendia dominar todo o mercado controlando toda a produção da região.", true),
                                                            new ImgOrTxt("Ninguém nunca descobriu o quanto disso era verdade ou não, só se sabe que isso rendeu várias histórias ao longo dos anos, uma delas era algo sobre eles contratarem os capangas para fazerem o trabalho sujo deles, e todos esses eram marcados como gado.", true),
                                                            new ImgOrTxt("Talvez daí que tenha surgido a tatuagem desse cara, não que ele seja membro de um grupo secreto nem nada do tipo. Como eu disse nada disso foi comprovado, mas existe maluco pra tudo não é mesmo.", true),
                                                            new ImgOrTxt("Isso é verdade.", false),
                                                            new ImgOrTxt("E bem, a maioria dessas lendas surgem por alguma razão, não seria impossível essa história ter uma pequena base de verdade por trás.", false),
                                                            new ImgOrTxt("Sim sim, você tem razão jovem.", true),
                                                             new ImgOrTxt("Não seria impossível.", true),
                                                            new ImgOrTxt("Bem, vou mandar isso para os especialistas aqui na agência para ver se eles descobrem algo mais sobre esse Átila Saverin.", true),
                                                            new ImgOrTxt("Enquanto eles veem isso, eu estive pensando sobre a frase que você me enviou algum tempo atrás. Especificamente a parte em que ele fala. “Seu nome não foi esquecido nas...”", true),
                                                            new ImgOrTxt("isso me lembra uma música de Luiz Gonzaga, e a forma como a frase foi cortada, me faz pensar que isso possa ser relevante de alguma forma, você poderia tentar descobrir.", true)


        });
        intro[2] = msg2;

        intro[3] = null; // correção pq nessa etapa o jogador avança uma etapa pra obter a resposta, isso eh uma gambiarra mas ia dar trabalho resolver na pratica
        Mensagem msg4 = new Mensagem("3", new ImgOrTxt[] {  new ImgOrTxt("Bom trabalho jovem.", true),
                                                            new ImgOrTxt("Vou enviar um agente para lá agora mesmo.", true),
                                                            new ImgOrTxt("O local apontado pelas coordenadas fica na cidade de Serrita.", true),
                                                            new ImgOrTxt("Nosso agente vão entrar em contato com a força policial da cidade para eles verificarem.", true), //dar um tempo aqui.

                                                            new ImgOrTxt("Serrita?", false),
                                                            new ImgOrTxt("Não é esta a cidade onde ocorre a missa do vaqueiro em homenagem ao primo de Luiz Gonzaga?", false),
                                                            new ImgOrTxt("O mesmo que é homenageado na música que Sanfonatti escreveu na mensagem?", false),

                                                            new ImgOrTxt("Isso mesmo.", true),
                                                            new ImgOrTxt("Curioso, não?", true),
                                                            new ImgOrTxt("Bem, os policiais estão verificando o local nesse momento.", true),
                                                            new ImgOrTxt("Vejamos.", true),
                                                            new ImgOrTxt("Eles encontraram isso.", true),
                                                            new ImgOrTxt(PlayerInfo.ProcurarPista("cartaJaco"), true),  //foto da carta de jacó
                                                            new ImgOrTxt("Esta carta, os especialistas da nossa agência em Serrita identificaram que ela foi de fato escrita por Raimundo Jacó, primo de Luiz Gonzaga. Ele foi assassinado na década de 50. A sua morte foi sempre um mistério. ", true),
                                                            new ImgOrTxt("Ninguém nunca conseguiu comprovar o que realmente aconteceu. Mas, a forma como ele escreveu, e as pistas de Sanfonatti que nos levaram até a Virtus Atlas… Será que está tudo conectado?", true),
                                                            new ImgOrTxt("Qual a relação entre os casos? Isso é tudo muito estranho.",true),
                                                            new ImgOrTxt("Mas bem. Nós ainda temos um caso para resolver. E isso não exatamente nos diz o que aconteceu com Sanfonatti não é mesmo?",true),
                                                            new ImgOrTxt("Precisamos seguir em frente.",true),

                                                            new ImgOrTxt("Sim, é verdade.",false),
                                                            new ImgOrTxt("Ainda temos o desaparecimento para nos preocupar.",false),

                                                            new ImgOrTxt("Enfim. Enquanto descobríamos sobre a carta, nossos especialistas conseguiram reconstruir tudo o que era possível das imagens das câmeras de segurança.", true),
                                                            new ImgOrTxt("O mais estranho é que, tem um setor do museu onde, sempre que Sanfonatti passa por ele, a imagem simplesmente some.", true),
                                                            new ImgOrTxt("Elas não foram apenas corrompidas. É como se alguém tivesse bloqueado completamente o sinal da câmera nesta área.", true),
                                                            new ImgOrTxt("Você deve investigar este local. Lá deve haver algo que possa nos dar mais respostas.", true),
                                                            new ImgOrTxt("Tome, o setor do museu é este aqui.",true),
                                                            new ImgOrTxt(PlayerInfo.ProcurarPista("mapaEnzo"), true) // foto do mapa do museu
        });
        intro[4] = msg4;

        Mensagem msg5 = new Mensagem("4", new ImgOrTxt[] { new ImgOrTxt("Bem, jovem. Acho que você tem o direito de saber.", true),
                                                            new ImgOrTxt("A nossa agência de investigação, a Alítheia, tem trabalhado, assim como Sanfonatti, em tentar descobrir a verdade sobre as informações que tem sido manipuladas por essa sociedade secreta que controla as grandes corporações, para isso a agência utiliza o programa de seleção de novos investigadores, para tentar encontrar pessoas com esse mesmo interesse, pois, como imagino que você já tenha percebido, eles são extremamente influentes, e tentar expô-los não iria nos ajudar muito.", true),
                                                            new ImgOrTxt("Foi apenas recentemente que descobrimos sobre Sanfonatti. Queríamos chamar ele para a nossa agência, mas infelizmente não fomos rápidos o suficiente. Não tínhamos como saber que a Virtus Atlas estava por trás disso, nem que já estava tão próxima de encontrá-lo.", true),
                                                            new ImgOrTxt("Ele parece ter resistido, mas isso não parece tê-los impedido. Maldição, eles parecem estar sempre um passo na nossa frente.", true),
                                                            new ImgOrTxt("Enfim, pelo que você descobriu, me parece que Sanfonatti estava tentando deixar alguma mensagem para que alguém capaz de encontrá-la pudesse continuar o que ele começou, ou ajudá-lo de alguma forma.", true),
                                                            new ImgOrTxt("Além de tudo, me parece que ele descobriu sozinho sobre a nossa agência, antes mesmo de termos feito contato. ", true),
                                                            new ImgOrTxt("Alítheia. Grego para ‘verdade’. Imagino que ele estava se referindo a nós. Quando disse para procurar pela Agência da verdade.", true),
                                                            new ImgOrTxt("Se me permite a modéstia, um movimento esperto da parte dele, acho que pouquíssimos outros receberam bem essas informações, considerando o quanto são manipulados sem sequer perceber.", true),
                                                            new ImgOrTxt("Bem, jovem. Agora que você já sabe a verdade, espero que você queira se juntar a nossa agência. Eu vejo muito futuro em você.", true),
                                                            new ImgOrTxt("Tudo o que você descobriu por aí pode nos ajudar bastante. Apesar de não termos o seu paradeiro exato, temos informação o suficiente para iniciar uma busca maior por Sanfonatti, além de agora termos o nome de um dos capangas da Virtus Atlas para seguir.", true),
                                                            new ImgOrTxt("Não é qualquer um que conseguiria chegar onde você chegou. Sinta-se orgulhoso. Bem!", true),
                                                            new ImgOrTxt("Acho que esse caso do museu encerra por aqui.", true),
                                                            new ImgOrTxt("Então jovem, está preparado para descobrir as verdades sobre o mundo?", true),
                                                            //fim de jogo
                                                            });

        intro[5] = msg5;

        //carregando dicas
        Mensagem dicas0 = new Mensagem("1", new ImgOrTxt[] { new ImgOrTxt("Está foi a única imagem de Sanfonatti que conseguimos restaurar das câmeras de segurança.", true),
                                                              new ImgOrTxt("Tente analisar o local com as ferramentas que lhe demos, talvez você encontre algo relevante.", true) });
        dicas[0] = dicas0;

        Mensagem dicas1 = new Mensagem("2", new ImgOrTxt[] { new ImgOrTxt("Estranho, acho que nunca vi essas marcas antes", true),
                                                              new ImgOrTxt("Elas parecem ter sido colocadas ali pelo próprio Sanfonatti, continue procurando, talvez isso signifique algo.", true) });
        dicas[1] = dicas1; //Disco

        Mensagem dicas2 = new Mensagem("3", new ImgOrTxt[] { new ImgOrTxt("Estranho, acho que nunca vi essas marcas antes", true),
                                                              new ImgOrTxt("Elas parecem ter sido colocadas ali pelo próprio Sanfonatti, continue procurando, talvez isso signifique algo.", true) });
        dicas[2] = dicas2; //musicBox

        Mensagem dicas3 = new Mensagem("4", new ImgOrTxt[] { new ImgOrTxt("Estranho, acho que nunca vi essas marcas antes", true),
                                                              new ImgOrTxt("Elas parecem ter sido colocadas ali pelo próprio Sanfonatti, continue procurando, talvez isso signifique algo.", true) });
        dicas[3] = dicas3; //gonzaga

        Mensagem dicas4 = new Mensagem("5", new ImgOrTxt[] { new ImgOrTxt("Esse pedaço de papel foi encontrado na bolsa de Sanfonatti, pelo que conseguimos identificar, essa e a letra dele mas nenhum de nossos especialistas sabe exatamente o que significa.", true),
                                                             new ImgOrTxt("Achamos que possa ser relevante para o caso.", true) });
        dicas[4] = dicas4; //mapaCigarro

        Mensagem dicas5 = new Mensagem("6", new ImgOrTxt[] { new ImgOrTxt("Este livro estava na bolsa de Sanfonatti.", true),
                                                             new ImgOrTxt("Apesar de ser um livro um tanto peculiar, nada nele parece estar fora do lugar.", true) });
        dicas[5] = dicas5; //livro

        Mensagem dicas6 = new Mensagem("7", new ImgOrTxt[] { new ImgOrTxt("Nenhum de nossos especialistas parece saber o que essas marcas significam.", true),                                                            
                                                             new ImgOrTxt("Você acha que consegue descobrir?", true)});
        dicas[6] = dicas6; //album 1 

        Mensagem dicas7 = new Mensagem("8", new ImgOrTxt[] { new ImgOrTxt("Me perdoe, jovem, mas estou um tanto ocupado no momento.", true),
                                                             new ImgOrTxt("E para ser bem honesto, eu não faço a menor ideia do que isso significa.", true),
                                                             new ImgOrTxt("Vou lhe mandar uma ferramenta que pode ser útil.", true),
                                                             new ImgOrTxt("www.google.com", true)});
        dicas[7] = dicas7; //album 2


        Mensagem dicas8 = new Mensagem("9", new ImgOrTxt[] { new ImgOrTxt("Este é um mapa de calor que traçamos das atividades de Sanfonatti no museu, as áreas com cor mais intensa indicam os setores de maior atividade.", true),
                                                             new ImgOrTxt("Recomendo você tentar investigar essas áreas.", true) });
        dicas[8] = dicas8; //mapaCalor

        Mensagem dicas9 = new Mensagem("10", new ImgOrTxt[] { new ImgOrTxt("De acordo com o banco de dados de digitas da nossa agência. Essa digital pertence a Atila Saverin.", true),
                                                             new ImgOrTxt("Vejamos.", true),
                                                             new ImgOrTxt("Atila Saverin, 27 anos, solteiro, sem antecedentes criminais, residente na cidade de Exu, trabalha como vendedor em uma loja de produtos agropecuários.", true), });
        dicas[9] = dicas9; //faca

        Mensagem dicas10 = new Mensagem("11", new ImgOrTxt[] { new ImgOrTxt("Pelo que consegui verificar no nosso banco de dados de digitais, essa digital pertence a Nariadna Gleycielly.", true),
                                                              new ImgOrTxt("Nariadna, 22 anos, casada, sem antecedentes criminais, reside em Salgueiro e é dona de uma loja de tecidos.", true) });
       
        dicas[10] = dicas10; //santo

        Mensagem dicas11 = new Mensagem("12", new ImgOrTxt[]{ new ImgOrTxt("Deixe-me ver.", true),
                                                              new ImgOrTxt("Essa digital pertence a Rajish Al-Habib.", true),
                                                              new ImgOrTxt("25 anos, casado.", true),
                                                              new ImgOrTxt("Humm, tem ficha na delegacia por porte ilegal de arma.", true),
                                                              new ImgOrTxt("Nasceu nos emirados árabes, mas reside na cidade do Recife, trabalha como professor de história em escola pública.", true),});
        dicas[11] = dicas11; //xilogravura

        Mensagem dicas12 = new Mensagem("13", new ImgOrTxt[] { new ImgOrTxt("Estranho esse símbolo.", true),
                                                               new ImgOrTxt("Pelo que eu consegui analisar aqui, ele não foi feito por Sanfonatti.", true),
                                                               new ImgOrTxt("Talvez esteja relacionado com algum dos suspeitos, pode significar algo.", true),});
        dicas[12] = dicas12; //caixaPoesia

        Mensagem dicas13 = new Mensagem("14", new ImgOrTxt[] { new ImgOrTxt("Essas são as fichas com as informações que eu consegui encontrar dos suspeitos.", true),
                                                              new ImgOrTxt("Infelizmente é o máximo que eu consigo fazer daqui da agência. Talvez você seja capaz de descobrir mais algumas informações se pesquisar um pouco.", true) });
        dicas[13] = dicas13; //suspeito

        Mensagem dicas14 = new Mensagem("15", new ImgOrTxt[] { new ImgOrTxt("Veja se você consegue descobrir o que pode ser a continuação dessa frase. Ela pode conter alguma dica de onde ele pode ter deixado algo.", true),
                                                              new ImgOrTxt("Deve haver algo sobre essa música no museu, afinal, é provavelmente uma música de Luiz Gonzaga. Sanfonatti pode ter escondido algo por perto.", true) });
        dicas[14] = dicas14; //frase

        Mensagem dicas15 = new Mensagem("16", new ImgOrTxt[] { new ImgOrTxt("Esse Sanfonatti parece realmente gostar de escrever em enigmas. A essa altura eu tenho certeza que essa frase significa algo.", true),
                                                              new ImgOrTxt("Tente descobrir o que significa. ", true) });
        dicas[15] = dicas15; //cacto

        Mensagem dicas16 = new Mensagem("17", new ImgOrTxt[] { new ImgOrTxt("O cordel foi encontrado por um funcionário do museu no mesmo local onde você encontrou a frase deixada por Sanfonatti.", true),
                                                              new ImgOrTxt("É bem provável que tenha algo escondido nele. Você consegue descobrir o que é?", true) });
        dicas[16] = dicas16; //cordel

        Mensagem dicas17 = new Mensagem("18", new ImgOrTxt[] { new ImgOrTxt("Sanfonatti parece ter nos deixado uma coordenada escrita nesse mapa.", true),
                                                              new ImgOrTxt("39°20'37.9”W", true),
                                                              new ImgOrTxt("Me avise se você descobrir qual é a outra para que possamos mandar algum agente até o local descobrir o que isso significa.", true)});
        dicas[17] = dicas17; //mapaNordeste

        Mensagem dicas18 = new Mensagem("19", new ImgOrTxt[] { new ImgOrTxt("Essa carta foi escrita por Raimundo Jacó, e foi encontrada no local indicado pelas coordenadas deixadas por Sanfonatti.", true),
                                                               new ImgOrTxt("Jacó faz parecer que estava sendo perseguido por ter descoberto algo, logo antes de ter sido assassinado.", true) });
        dicas[18] = dicas18; //cartaJaco

        Mensagem dicas19 = new Mensagem("20", new ImgOrTxt[] { new ImgOrTxt("Esta é a área do museu em que não existe nenhuma imagem de Sanfonatti nas câmeras de segurança.", true),
                                                               new ImgOrTxt("Deve haver algo de importante aí.", true) });
        dicas[19] = dicas19; //mapaEnzo

        Mensagem dicas20 = new Mensagem("21", new ImgOrTxt[] { new ImgOrTxt("Eu acredito que a esse ponto você já deve entender como a mente de Sanfonatti funciona melhor do que eu.", true),
                                                              new ImgOrTxt("Essa é a última pista que nos temos. Tente descobrir o que ela significa, é possível que tenha algo que ajude a decifrar nas informações que você já coletou antes.", true),
                                                              new ImgOrTxt("Boa sorte, jovem.", true)});
        dicas[20] = dicas20; //quadroBoneco

        //carregando automaticas

        automatico = new Mensagem[] {  new Mensagem("0", new ImgOrTxt[]{ new ImgOrTxt("Olá, novamente jovem. Enquanto você investigava aí no museu, um dos nossas investigadores encontrou uma bolsa jogada no lado de fora do museu perto de uma lixeira. Era a mesma bolsa que Sanfonatti estava usando quando visitou o museu. Estranho não acha? Bem, dentro da bolsa encontramos este pedaço de papel", true),
                                                                                              new ImgOrTxt(PlayerInfo.ProcurarPista("mapaCigarro"), true), //imagem do mapa
                                                                                             new ImgOrTxt("E este livro", true),
                                                                                             new ImgOrTxt(PlayerInfo.ProcurarPista("livro"), true), //foto do livro
                                                                                             new ImgOrTxt("Não sabemos ainda o que significa ou se é relevante. Mas, de qualquer forma, é uma pista, então achei que seria útil enviar.", true),
                                                                                             new ImgOrTxt("Estranho de fato, vou ver o que consigo descobrir.", false),


                                                        }), new Mensagem("1", new ImgOrTxt[]{ new ImgOrTxt("Essas marcas de novo, estranho. Essa outra palavra embaixo, também parece ter sido feita por Sanfonatti.", true),
                                                                                              new ImgOrTxt("Realmente estranho.", true),
                                                                                              new ImgOrTxt("Bem, acha que você consegue descobrir o que essas marcas estranhas significa? Se descobrir, por favor me envie.", true),
                                                                                              new ImgOrTxt("Sanfonatti deve ter deixado algo que me ajuda a descobrir o significado dessas marcas.", false),
                                                                                              new ImgOrTxt("Assim que eu encontrar a resposta eu envio.", false),


                                                        }),
                                                           new Mensagem("2", new ImgOrTxt[]{ new ImgOrTxt("Bem, pelo que conseguimos coletar das suas pistas, temos 3 suspeitos principais, Rajish Al-Habib, Atila Saverin, e Nariadna Gleycielly, nossos especialistas não conseguiram extrair nenhuma boa informação disso, mas só isso é muito pouco para conseguirmos avaliar algo, precisamos de algo maior para prosseguir.", true),
                                                                                          new ImgOrTxt("Tem também a questão desse símbolo que você encontrou, ele me parece ser relevante.", true),
                                                                                          new ImgOrTxt("Humm, me desculpe, mas não tem muito que eu esteja conseguindo fazer por aqui. Talvez você consiga encontrar alguma informação sobre os suspeitos que possa ajudar.", true),
                                                                                          new ImgOrTxt("Tente descobrir alguma informação que possa ligar algum desses suspeitos ao caso, ou que tenha alguma conexão com algo que você encontrou.", true),
                                                                                          new ImgOrTxt("Acha que consegue?", true),
                                                                                          new ImgOrTxt("Me envie uma mensagem se encontrar algo importante.", true),
                                                                                          new ImgOrTxt(PlayerInfo.ProcurarPista("suspeitos"), true), //ficha suspeitos
                                                                                          new ImgOrTxt("Aqui estão as fichas policiais que eu consegui referentes as digitais dos suspeitos", true),
                                                                                          new ImgOrTxt("Vou ver o que posso descobrir.", false),
                                                                                          new ImgOrTxt("Talvez eu consiga encontrar alguma outra informação sobre esses suspeitos ou o símbolo se eu pesquisar um pouco.", false),
                                                                                          new ImgOrTxt("Informarei assim que obtiver algo.", false),

                                                        }),
                                                        new Mensagem("3", new ImgOrTxt[]{ new ImgOrTxt("Humm. Esse local que você encontrou agora. Um dos funcionários do museu havia encontrado isto escondido aí.", true),
                                                                                          new ImgOrTxt(PlayerInfo.ProcurarPista("cordel"), true),  //imagem do cordel
                                                                                          new ImgOrTxt("Não parecia relevante antes, era apenas um pedaço de papel com um cordel escrito, mas seria coincidência demais isso estar no mesmo lugar.", true),

                                                        }),
                                                         new Mensagem("4", new ImgOrTxt[]{ new ImgOrTxt("Essas marcações deixadas no mapa. Elas parecem apontar uma coordenada geográfica.", true),
                                                                                          new ImgOrTxt("Humm", true),
                                                                                          new ImgOrTxt("Se quisermos encontrar o lugar para onde isso aponta, precisaremos de outra coordenada", true),
                                                                                          new ImgOrTxt("Por favor, se descobrir qual é, me avise que eu mandarei um dos nossos agentes averiguar o local.", true),
                                                                                          new ImgOrTxt("Ok, a outra coordenada deve estar aqui em algum lugar.", false),
                                                                                          new ImgOrTxt("Vou tentar encontrá-la.", false),
                                                         }),
                                                         new Mensagem("5", new ImgOrTxt[]{new ImgOrTxt("Humm, o que será que isso significa?", true),
                                                                                          new ImgOrTxt("Esta é a última pista que nos resta. Talvez descobrir o que ela quer dizer possa nos ajudar a descobrir o que aconteceu com Sanfonatti.", true),
                                                                                          new ImgOrTxt("Bem jovem. Eu sei que não temos muito para seguir em frente.", true),
                                                                                          new ImgOrTxt("Mas, se você descobrir onde Sanfonatti pode estar, ou se ele deixou alguma mensagem que possa nos dizer onde procurar, por favor me envie.", true),

                                                                                          new ImgOrTxt("Realmente não temos muito, mas eu vou ver o que posso fazer.", false),
                                                                                          new ImgOrTxt("Obrigado pelo voto de confiança Jerry.", false),

                                                         }),
                                                         new Mensagem("6", new ImgOrTxt[]{ new ImgOrTxt("Bem, jovem. Acho que você tem o direito de saber.", true),
                                                                                           new ImgOrTxt("A nossa agência de investigação, a Alítheia, tem trabalhado, assim como Sanfonatti, em tentar descobrir a verdade sobre essas informações que tem sido manipuladas por essa sociedade secreta que controla as grandes corporações do mundo, para isso a agência utiliza o programa de seleção de novos investigadores, para tentar encontrar pessoas com o mesmo interesse, afinal, essa sociedade é muito poderosa.", true),
                                                                                           new ImgOrTxt("Foi apenas recentemente que descobrimos sobre Sanfonatti. Queríamos chamar ele para a nossa agência, mas infelizmente não fomos rápidos o suficiente. Não tínhamos como saber que a Virtus Atlas estava por trás disso, e já estava tão próxima de encontrá-lo, e ele, sozinho, não teve muita opção. Maldição, eles parecem estar sempre um passo na nossa frente.", true),
                                                                                           new ImgOrTxt("Enfim, pelo que você descobriu, me parece que Sanfonatti estava tentando deixar alguma mensagem para que alguém capaz de encontrá-la pudesse continuar o que ele começou, e parece que além de tudo ele descobriu o que nossa agência era realmente nos últimos instantes. ", true),
                                                                                           new ImgOrTxt("É, a Alítheia é a agência da verdade.", true),
                                                                                           new ImgOrTxt("Bem. Agora que você já sabe a verdade, espero que você queira se juntar a nossa agência. Eu vejo muito futuro em você.", true),
                                                                                           new ImgOrTxt("Pois é, não ache que seu trabalho foi em vão jovem.", true),
                                                                                           new ImgOrTxt("Apesar de não termos o paradeiro exato, as informações que você conseguiu não só podem nos ajudar a encontrar o pobre Sanfonatti, como também agora nós temos o nome de um dos capangas da Virtus Atlas para seguir.", true),
                                                                                           new ImgOrTxt("Bem!", true),
                                                                                           new ImgOrTxt("Acho que esse caso do museu encerra por aqui.", true),
                                                                                           new ImgOrTxt("Então jovem, está preparado para descobrir as verdades sobre o mundo?", true),

                                                         }),};




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
            Invoke("desativarPopupDeny", 3);
        }

        else
        {
            //show pop-up do que o detetive espera como resposta.
            if (!(answer.Equals("")))
            {
                Debug.Log("Você tentou digitar: "+answer);
                EnviarMsgResposta(answer);
            }
            else
            {
                popupAllow.SetActive(true);
                Debug.Log("texto Vazio");
                Invoke("desativarPopupAllow", 3);
            }
        }
        answer = "";
        inputField.text = "";

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
            ativarPopupAskAgain();
            ChatListControl.closePanelDicasStatic();
            return;
        }
        else
        {
            dicas[img].enviado = true;
            pistasDetetive[img].enviado = true;
            ChatListControl.RenderizarTexto("Jerry, o que você pode me informar sobre esta pista?", false);
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
            ativarPopupAskAgain2();
            return;


        }
        else
        {
            dicas[img].enviado = true;
            pistasDetetive[img].enviado = true;
            ChatListControl.RenderizarTexto("Jerry, o que você pode me informar sobre esta pista?", false);
            ChatListControl.RenderizarImagem(img, false);
            for (int i = 0; i < dicas[img].imgOrTxt.Length; i++)
            {
                StaticCoroutine.DoCoroutineDelayMsgDetetive(dicas[img].imgOrTxt[i].txt);
            }

        }

        popupEncaminhado.SetActive(true);
        Invoke("desativarPopupEncaminhado", 2);
        MainMenu.TurnOnChatNofication();
        Handheld.Vibrate();
    }

    public void desativarPopupEncaminhado()
    {
        popupEncaminhado.SetActive(false);
    }

    public void ativarPopupAskAgain()
    {
        popupAskAgain.SetActive(true);
        popupAllow.SetActive(false);
        Invoke("desativarPopupAskAgain", 3);
        popupDeny.SetActive(false);
        popupError.SetActive(false);
        popupTutorial.SetActive(false);
    }

    public void ativarPopupAskAgain2()
    {
        popupAskAgain2.SetActive(true);
        popupAskAgain.SetActive(false);
        popupAllow.SetActive(false);
        Invoke("desativarPopupAskAgain2", 3);
        popupDeny.SetActive(false);
        popupError.SetActive(false);
        popupTutorial.SetActive(false);

    }


    public void ativarTutorialPopup()
    {
        if (firstTimePopup)
        {
            popupAskAgain2.SetActive(false);
            popupAskAgain.SetActive(false);
            popupTutorial.SetActive(true);
            Invoke("desativarPopupTutorial", 5);
            firstTimePopup = false;
            popupDeny.SetActive(false);
            popupError.SetActive(false);
            popupAllow.SetActive(false);
        }
        else
        {
            if (exploracao)
            {
                ativarPopupDeny();
            }
            else
            {
                ativarPopupAllow();
            }
        }

    }

    public void ativarPopupAllow()
    {
        popupAllow.SetActive(true);
        Invoke("desativarPopupAllow", 3);
        popupDeny.SetActive(false);
        popupError.SetActive(false);
        popupTutorial.SetActive(false);
        popupAskAgain2.SetActive(false);
        popupAskAgain.SetActive(false);
    }

    public void ativarPopupDeny()
    {

        popupDeny.SetActive(true);
        Invoke("desativarPopupDeny", 3);
        popupAllow.SetActive(false);
        popupError.SetActive(false);
        popupTutorial.SetActive(false);
        popupAskAgain2.SetActive(false);
        popupAskAgain.SetActive(false);
    }

    public void desativarPopupTutorial()
    {
        popupTutorial.SetActive(false);
    }

    public void desativarPopupAskAgain()
    {
        popupAskAgain.SetActive(false);
    }

    public void desativarPopupAskAgain2()
    {
        popupAskAgain2.SetActive(false);
    }

    public void desativarPopupDeny()
    {
        popupDeny.SetActive(false);
    }

    public void desativarPopupAllow()
    {
        popupAllow.SetActive(false);
    }

    public void ativarPopupError()
    {
        popupError.SetActive(true);
        Invoke("desativarPopupError", 3);
        popupDeny.SetActive(false);
        popupAllow.SetActive(false);
        popupTutorial.SetActive(false);
    }

    public void desativarPopupError()
    {
        popupError.SetActive(false);
    }
    

    public static void StartIntro()  //por invoke não poder chamar static, tive que criar essa classe auxiliar, e o invoke que eu chamo para aumentar a etapa, é chamado no menu.
    {
        StaticCoroutine.DoCoroutineDelayMsgs();
    }

    public void EnviarMsgResposta(string input) //primeira celula eh sempre a validacao, a segunda eh sempre a mensagem real
    {
        string original = resposta[etapa].imgOrTxt[0].txt;
        string originalLower = original.ToLower();
        Debug.Log("A resposta é :"+ original);
        string inputLower = input.ToLower();

        if(etapa == 3) // if elses para o jogador acertar a coordenada, basicamente colocar possiveis respostas.
        {
            if ((originalLower.Equals("7°51'34.8\"S") || (originalLower.Equals("7°51'34.8S")) || (originalLower.Equals("7°51'34.8 S")) || (originalLower.Equals("7°51 34.8 S")) || (originalLower.Equals("7 51 348S"))))
            {
                StaticCoroutine.DoCoroutineDelayResposta();

            }else if ((originalLower.Equals("7 51 34.8 S")) || (originalLower.Equals("7 51 348 S")) || (originalLower.Equals("751348S")) || (originalLower.Equals("7°51'34.8\" S")) )
            {
                StaticCoroutine.DoCoroutineDelayResposta();
            }else if ((originalLower.Equals("7° 51' 34.8\" S")) || (originalLower.Equals("7°51'348 S")) || (originalLower.Equals("7°51'348\"S")) || (originalLower.Equals("7 51' 348\" S")))
            {
                StaticCoroutine.DoCoroutineDelayResposta();
            }
            else
            {
                ativarPopupError();
            }

        }
        else
        {
            double similaridade = AlgoritmoIgualdade.CalculateSimilarity(originalLower,inputLower);

            if(similaridade == 100)
            {
                StaticCoroutine.DoCoroutineDelayResposta();
            }
            else if (similaridade == 0)
            {
                ativarPopupError(); //azedou
            }
            else
            {
                if(etapa == 0)
                {
                    if (similaridade >= 95)
                    {
                        StaticCoroutine.DoCoroutineDelayResposta(); //aceite
                    }
                    else
                    {
                        ativarPopupError(); //rejeite, mas acho que podemos dar um feedback diferente caso o jogador chegou muito perto da resposta.
                    }
                }else if(etapa == 1)
                {
                    if (similaridade >= 91)
                    {
                        StaticCoroutine.DoCoroutineDelayResposta(); //aceite
                    }
                    else
                    {
                        ativarPopupError(); //rejeite, mas acho que podemos dar um feedback diferente caso o jogador chegou muito perto da resposta.
                    }
                }
                else if(etapa == 4)
                {
                    if (similaridade >= 88)
                    {
                        StaticCoroutine.DoCoroutineDelayResposta(); //aceite
                    }
                    else
                    {
                        ativarPopupError(); //rejeite, mas acho que podemos dar um feedback diferente caso o jogador chegou muito perto da resposta.
                    }
                }
                else
                {
                    Debug.Log("Erro em RESPOSTA... nao era pra chegar aqui");
                }
            }
        }

        
    }


    public static void EnviarMsgFeedback()
    {
        automatic++;
        etapa++;
        PlayerInfo.AumentarEtapa();
        MainMenu.check = false;    // um boolean que é resetado toda vez que passa de fase, pra controlar, qdo o jogador deve descobrir algo.
        EnviarMsgIntro();
    }


    public static void EnviarMsgIntro() // a primeira vez que essa funcao for chamada, sera no menu, quando vc apertar no chat. Lembrar de habilitar a pista introdutoria no banco de dados
    {
        StaticCoroutine.DoCoroutineDelayIntro();
    }



    void Update()
    {


        if (etapa == 0)
        {

            //Etapa0();
            //checa se o jogador descobriu as pistas para mandar uma mensagem automatica.
            if (automatic <= 0)
            {
                int index = PlayerInfo.ProcurarPista("discoContinental");
                if ((PlayerInfo.pistas[index].descoberta && PlayerInfo.pistas[index + 1].descoberta && PlayerInfo.pistas[index + 2].descoberta) && !(automatico[automatic].enviado)) //as pistas 1,2,3 = Criptografia1,Criptografia2,Criptografia3 e simbolo
                {
                    automatico[automatic].enviado = true;
                    StaticCoroutine.DoCoroutineAutomatic();
                    
                    MainMenu.TurnOnChatNofication();
                    Handheld.Vibrate();

                }
            }
            else
            {

                int index2 = PlayerInfo.ProcurarPista("albumLuizCostas");
                if (PlayerInfo.pistas[index2].descoberta && !(automatico[automatic].enviado))
                {
                    automatico[automatic].enviado = true;
                    StaticCoroutine.DoCoroutineAutomatic();                   
                    MainMenu.TurnOnChatNofication();
                    Handheld.Vibrate();
                }
                   
                }
            }

        

        else if (etapa == 1)
        {

            int index = PlayerInfo.ProcurarPista("faca");
            if ((PlayerInfo.pistas[index].descoberta) && (PlayerInfo.pistas[index + 1].descoberta) && (PlayerInfo.pistas[index + 2].descoberta) && (PlayerInfo.pistas[index + 3].descoberta) && !(automatico[automatic].enviado))
            {
                automatico[automatic].enviado = true;
                StaticCoroutine.DoCoroutineAutomatic();
                MainMenu.TurnOnChatNofication();
                Handheld.Vibrate();
            }

        }

        //etapa 2 não tem validação
        else if (etapa == 2)
        {
            int index = PlayerInfo.ProcurarPista("cacto");
            if ((PlayerInfo.pistas[index].descoberta) && !(automatico[automatic].enviado))
            { //lembrar de dar um tempo para o detetive entrar em contato.
                automatico[automatic].enviado = true;
                StaticCoroutine.DoCoroutineAutomatic();
                MainMenu.TurnOnChatNofication();
                Handheld.Vibrate();
            }

               
            }

        



        else if (etapa == 3)
        {
            int index = PlayerInfo.ProcurarPista("mapaNordeste");
            if ((PlayerInfo.pistas[index].descoberta) && !(automatico[automatic].enviado))
            {
                automatico[automatic].enviado = true;
                StaticCoroutine.DoCoroutineAutomatic();
                MainMenu.TurnOnChatNofication();
                Handheld.Vibrate();

            }


        }

        else if (etapa == 4)
        {
            int index = procurarPistaDetetive("quadroBoneco");
            if ((PlayerInfo.pistas[index].descoberta) && !(automatico[automatic].enviado))
            {
                automatico[automatic].enviado = true;
                StaticCoroutine.DoCoroutineAutomatic();
                MainMenu.TurnOnChatNofication();
                Handheld.Vibrate();
            }
        }

    }
}
/*public void Etapa0()
{
    //checa se o jogador descobriu as pistas para mandar uma mensagem automatica.
    int index = PlayerInfo.ProcurarPista("discoContinental");
    if ((PlayerInfo.pistas[index].descoberta && PlayerInfo.pistas[index + 1].descoberta && PlayerInfo.pistas[index + 2].descoberta) && !(automatico[automatic].enviado)) //as pistas 1,2,3 = Criptografia1,Criptografia2,Criptografia3
    {
        for (int i = 0; i < automatico[automatic].imgOrTxt.Length; i++)
        {
            if (automatico[automatic].imgOrTxt[i].isImg())
            {
                ChatListControl.RenderizarImagem(automatico[automatic].imgOrTxt[i].img, true);
            }
            else
            {
                ChatListControl.RenderizarTexto(automatico[automatic].imgOrTxt[i].txt, true);
            }
        }
        automatico[automatic].enviado = true;
        MainMenu.TurnOnChatNofication();
        // PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("mapaCigarro"), false);  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat              
    }


    //Checa no update se A DICA JA FOI ENVIADA para poder liberar o pop-up
    int index1 = procurarPistaDetetive("discoContinental"); // talvez, inicializar todas essas variaveis no start() para só acessar no update ou inves de sempre procurar.
    int index2 = index1 + 1;
    int index3 = index2 + 1;

    if ((pistasDetetive[index1].enviado) || (pistasDetetive[index2].enviado) || (pistasDetetive[index3].enviado))
    {
        exploracao = false;

    }
}

public void Etapa1()
{
    int index = PlayerInfo.ProcurarPista("faca");
    if ((PlayerInfo.pistas[index].descoberta) && (PlayerInfo.pistas[index + 1].descoberta) && (PlayerInfo.pistas[index + 2].descoberta) && !(automatico[automatic].enviado))
    {
        for (int i = 0; i < automatico[automatic].imgOrTxt.Length; i++)
        {
            if (automatico[automatic].imgOrTxt[i].isImg())
            {
                ChatListControl.RenderizarImagem(automatico[automatic].imgOrTxt[i].img, true);
            }
            else
            {
                ChatListControl.RenderizarTexto(automatico[automatic].imgOrTxt[i].txt, true);
            }
        }
        automatico[automatic].enviado = true;
        MainMenu.TurnOnChatNofication();
        // PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("Suspeito1"), false);  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat
    }

    int index1 = procurarPistaDetetive("caixaPoesia");

    if ((pistasDetetive[index1].enviado))
    {
        exploracao = false;
    }
}

public void Etapa3()
{
    int index = PlayerInfo.ProcurarPista("mapaNordeste");
    if ((PlayerInfo.pistas[index].descoberta) && !(automatico[automatic].enviado))
    {
        for (int i = 0; i < automatico[automatic].imgOrTxt.Length; i++)
        {
            if (automatico[automatic].imgOrTxt[i].isImg())
            {
                ChatListControl.RenderizarImagem(automatico[automatic].imgOrTxt[i].img, true);
            }
            else
            {
                ChatListControl.RenderizarTexto(automatico[automatic].imgOrTxt[i].txt, true);
            }
        }
        automatico[automatic].enviado = true;
        exploracao = false;
        MainMenu.TurnOnChatNofication();
        //PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("Cordel"), false);  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat
    }

    //aqui coincidentemente a mensagem automatica e a liberação de exploração são iguais
}

public void Etapa4()
{
    int index = procurarPistaDetetive("quadroBoneco");
    if ((pistasDetetive[index].enviado))
    {
        exploracao = false;
    }
} */

