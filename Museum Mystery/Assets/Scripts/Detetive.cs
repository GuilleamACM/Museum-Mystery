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
    public static Mensagem[] dicas = new Mensagem[20]; // qdo o jogador envia uma pista habilitada ao detetive, o detetive responde com um breve texto, falando melhor sobre a pista enviada.
    
    public static Mensagem[] intro = new Mensagem[5]; // introducoes do ciclo
    public static Mensagem[] automatico; // msgs automaticas que o detetive manda ao jogador completar alguma coisa

    public static Mensagem[] resposta = new Mensagem[] { new Mensagem("0", new ImgOrTxt[] { new ImgOrTxt("Parece que já me encontraram, mas eu não vou deixar que escondam a verdade. O seu nome não foi esquecido nas"),
                                                                           new ImgOrTxt("Olá Jerry. A série de letras e números que eu encontrei no disco, eram parte de um link de um vídeo. "),
                                                                           new ImgOrTxt("O vídeo, continha a chave para traduzir aquelas marcações estranhas que Enzo havia deixado nas obras."),
                                                                           new ImgOrTxt("Pelo que eu consegui identificar, elas traduzem para: “Parece que já me encontraram, mas eu não vou deixar que escondam a verdade. O seu nome não foi esquecido nas”"), }),
                                                         new Mensagem("1", new ImgOrTxt[] { new ImgOrTxt("Virtus Atlas"),
                                                                                            new ImgOrTxt("Jerry, eu encontrei em um perfil online de um dos suspeitos uma foto, onde ele aparece com uma tatuagem."), 
                                                                                            new ImgOrTxt("Essa tatuagem é o mesmo símbolo que estava na porta com as marcas de gado. Nessa mesma tatuagem há uma inscrição, que eu consegui traduzir para “Virtus Atlas”."),
                                                                                            new ImgOrTxt("Você faz alguma ideia do que significa?"), }),
                                                         null,
                                                         new Mensagem("2", new ImgOrTxt[] { new ImgOrTxt("7°51'34.8\"S"),
                                                                                            new ImgOrTxt("Jerry, acho que encontrei a outra coordenada."),
                                                                                            new ImgOrTxt("Ela estava escondida no cordel que você me mandou."),
                                                                                            new ImgOrTxt("A coordenada é: 7°51'34.8\"S")}),
                                                         new Mensagem("3", new ImgOrTxt[] { new ImgOrTxt("Agência da verdade"),
                                                                                            new ImgOrTxt("Jerry, a mensagem que Enzo havia deixado, era um link para uma página contendo alguns documentos escritos por ele."),
                                                                                            new ImgOrTxt("Nesses documentos ele explica tudo o que ele descobriu sobre a Virtus Atlas, e como eles têm manipulado diversas corporações para esconder a verdade sobre as suas ações, e manter o poder."),
                                                                                            new ImgOrTxt("Enzo estava investigando o que havia acontecido com um familiar, quando se deparou com o nome Virtus Atlas, ele começou a investigá-los, mas eles acabaram descobrindo e começaram a perseguí-lo pois ele sabia demais, assim como aconteceu com Jacó."),
                                                                                            new ImgOrTxt("Enzo tentou desmascará-los mas não conseguiu. A última coisa que ele disse foi para procurar a “Agência da verdade”."),
                                                                                            new ImgOrTxt("O que isso significa?!"),}),
                                                        }; // a posicao 0 desse array eh a palavra para validar, e as posicoes adiante sao as mensagens que o jogador enviara para o detetive caso tenha acertado a validacao
    public static bool exploracao = false; // para bloquear e desbloquear o envio de RESPOSTAS
    public static int etapa = 0;
    public static string answer; // msg escrita no input
    public static int automatic = 0;
    public static int imgDicas; // referencia da img clicada em anexos.
    public static pistaDetetive[] pistasDetetive = new pistaDetetive[] { new pistaDetetive("enzoCamera"),new pistaDetetive("discoContinental"), new pistaDetetive("musicBox"),
                                                                         new pistaDetetive("gonzaga"), new pistaDetetive("mapaCigarro"), new pistaDetetive("livro"), new pistaDetetive("albumLuizCostas"),
                                                                         new pistaDetetive("mapaCalor"), new pistaDetetive("faca"), new pistaDetetive("santo"),
                                                                         new pistaDetetive("xilogravuraMoldura"), new pistaDetetive("caixaPoesia"), new pistaDetetive("suspeitos"), new pistaDetetive("frase"), new pistaDetetive("cacto"),
                                                                         new pistaDetetive("mapaNordeste"), new pistaDetetive("cordel"),new pistaDetetive("cartaJaco"),new pistaDetetive("mapaEnzo"), new pistaDetetive("quadroBoneco"),}; // esse array deve ser mapeado igualmente a o array de dicas.

    void Awake()
    {

        //carregando introducoes

        Mensagem msg1 = new Mensagem("0", new ImgOrTxt[18] { new ImgOrTxt("Olá, você deve ser um dos novatos, não? Eu sou Jerry, detetive chefe responsável pela supervisão do caso em questão."),
                                    new ImgOrTxt("Infelizmente, não posso estar aí com você neste momento, tenho que cuidar de toda a papelada da investigação aqui no escritório, mas não se preocupe, tentarei ajudá-lo o máximo que puder daqui."),
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
                                    new ImgOrTxt(PlayerInfo.ProcurarPista("enzoCamera")), //camera segurança
                                    new ImgOrTxt("As gravações da câmera de segurança do museu estão corrompidas, e isso foi tudo que conseguimos de relevante."),
                                    new ImgOrTxt("Nossos especialistas estão trabalhando em restaurá-las. Mas até lá sugiro que comece tentando seguir seus passos e ver se encontra algo relevante."),
                                    new ImgOrTxt("Boa sorte jovem!")
                                    //falta a imagem de introdução 
                                    });
        intro[0] = msg1;


        Mensagem msg2 = new Mensagem("1", new ImgOrTxt[] { new ImgOrTxt("Humm, isso parece mais sério do que eu imaginava. Ele parece indicar que estava sendo perseguido ou algo assim. Isso muda algumas coisas, deixe-me ver o que eu consigo fazer."),
                                                            new ImgOrTxt("Vejamos. Não conseguimos reconstruir muitas imagens satisfatórias das câmeras de segurança ainda, o processo é um tanto complicado, mas conseguimos identificar algumas coisas que talvez possam ajudar."),
                                                            new ImgOrTxt("Traçamos um mapa que indica os lugares de maior atividade de Sanfonatti no museu, em particular aqueles onde havia alguém por perto que possamos considerar um suspeito."),
                                                            new ImgOrTxt("O horário em que ele foi visitar o museu não era de muito movimento, então não tinha muita gente para observar, mas devido as câmeras estarem corrompidas, não é possível identificar nenhuma dessas pessoas."),
                                                            new ImgOrTxt("Tente investigar esses lugares, talvez seja possível encontrar alguma informação que possa nos levar a um suspeito."),
                                                            new ImgOrTxt("Vou atualizar o seu software para que ele possa ter mais precisão."),
                                                            new ImgOrTxt("Ah, desculpe, já ia esquecendo. Aqui está o mapa:"),
                                                            new ImgOrTxt(PlayerInfo.ProcurarPista("mapaCalor")) // mapa de calor
        });
        intro[1] = msg2;


        Mensagem msg3 = new Mensagem("2", new ImgOrTxt[] { new ImgOrTxt("Virtus Atlas? Curioso. Isso é, ou pelo menos deveria ser, uma lenda."),
                                                            new ImgOrTxt("É uma história antiga que contavam nas cidades do interior. Alguma coisa sobre um grupo de poderosos do ramo da agropecuária, que pretendia dominar todo o mercado controlando toda a produção da região."),
                                                            new ImgOrTxt("Ninguém nunca descobriu o quanto disso era verdade ou não, só se sabe que isso rendeu várias histórias ao longo dos anos, uma delas era algo sobre eles contratarem os capangas para fazerem o trabalho sujo deles, e todos esses eram marcados como gado."),
                                                            new ImgOrTxt("Talvez daí que tenha surgido a tatuagem desse cara, não que ele seja membro de um grupo secreto nem nada do tipo. Como eu disse nada disso foi comprovado, mas existe maluco pra tudo não é mesmo."),
                                                            new ImgOrTxt("Bem, vou mandar isso para os especialistas aqui na agência para ver se eles descobrem algo mais sobre esse Átila Saverin."),
                                                            new ImgOrTxt("Enquanto eles veem isso, eu estive pensando sobre a frase que você me enviou algum tempo atrás. Especificamente a parte em que ele fala. “Seu nome não foi esquecido nas”... "),
                                                            new ImgOrTxt("isso me lembra uma música de Luiz Gonzaga, e a forma como a frase foi cortada, me faz pensar que isso possa ser relevante de alguma forma, você poderia tentar descobrir.")


        });
        intro[2] = msg3;

        Mensagem msg4 = new Mensagem("3", new ImgOrTxt[] { new ImgOrTxt("Bom trabalho jovem. Vou enviar um agente para lá agora mesmo. O local apontado pelas coordenadas fica na cidade de Serrita."),
                                                            new ImgOrTxt("Nosso agente está entrando em contato nesse momento com a força policial da cidade"),
                                                            new ImgOrTxt("humm"),
                                                            new ImgOrTxt("Eles encontraram isso..."),
                                                            new ImgOrTxt(PlayerInfo.ProcurarPista("cartaJaco")),  //foto da carta de jacó
                                                            new ImgOrTxt("Esta carta, os especialistas da nossa agência em Serrita identificaram que ela foi de fato escrita por Raimundo Jacó, primo de Luiz Gonzaga. Ele foi assassinado na década de 50. A sua morte foi sempre um mistério. "),
                                                            new ImgOrTxt("Ninguém nunca conseguiu comprovar o que realmente aconteceu. Mas, a forma como ele escreveu, e as pistas de Sanfonatti que nos levaram até a Virtus Atlas… Será que está tudo conectado?"),
                                                            new ImgOrTxt("Qual a relação entre os casos? Isso é tudo muito estranho. Mas bem. Nós ainda temos um caso para resolver. E isso não exatamente nos diz o que aconteceu com Sanfonatti não é mesmo?"),
                                                            new ImgOrTxt("Precisamos seguir em frente. Enfim. Enquanto descobríamos sobre a carta, nossos especialistas conseguiram reconstruir tudo o que era possível das imagens das câmeras de segurança."),
                                                            new ImgOrTxt("O mais estranho é que, tem um setor do museu onde, sempre que Sanfonatti passa por ele, a imagem simplesmente some. Elas não foram apenas corrompidas. É como se alguém tivesse bloqueado completamente o sinal da câmera nesta área."),
                                                            new ImgOrTxt("Você deve investigar este local. Lá deve haver algo que possa nos dar mais respostas. Tome, o setor do museu é este aqui. "),
                                                            new ImgOrTxt(PlayerInfo.ProcurarPista("mapaEnzo")) // foto do mapa do museu
        });
        intro[3] = msg4;

        Mensagem msg5 = new Mensagem("4", new ImgOrTxt[] { new ImgOrTxt("Bem, jovem. Acho que você tem o direito de saber."),
                                                            new ImgOrTxt("A nossa agência de investigação, a Alítheia, tem trabalhado, assim como Sanfonatti, em tentar descobrir a verdade sobre as informações que tem sido manipuladas por essa sociedade secreta que controla as grandes corporações, para isso a agência utiliza o programa de seleção de novos investigadores, para tentar encontrar pessoas com esse mesmo interesse, pois, como imagino que você já tenha percebido, eles são extremamente influentes, e tentar expô-los não iria nos ajudar muito."),
                                                            new ImgOrTxt("Foi apenas recentemente que descobrimos sobre Sanfonatti. Queríamos chamar ele para a nossa agência, mas infelizmente não fomos rápidos o suficiente. Não tínhamos como saber que a Virtus Atlas estava por trás disso, nem que já estava tão próxima de encontrá-lo."),
                                                            new ImgOrTxt("Ele parece ter resistido, mas isso não parece tê-los impedido. Maldição, eles parecem estar sempre um passo na nossa frente."),
                                                            new ImgOrTxt("Enfim, pelo que você descobriu, me parece que Sanfonatti estava tentando deixar alguma mensagem para que alguém capaz de encontrá-la pudesse continuar o que ele começou, ou ajudá-lo de alguma forma."),
                                                            new ImgOrTxt("Além de tudo, me parece que ele descobriu sozinho sobre a nossa agência, antes mesmo de termos feito contato. "),
                                                            new ImgOrTxt("Alítheia. Grego para ‘verdade’. Imagino que ele estava se referindo a nós. Quando disse para procurar pela Agência da verdade."),
                                                            new ImgOrTxt("Se me permite a modéstia, um movimento esperto da parte dele, acho que pouquíssimos outros receberam bem essas informações, considerando o quanto são manipulados sem sequer perceber."),
                                                            new ImgOrTxt("Bem, jovem. Agora que você já sabe a verdade, espero que você queira se juntar a nossa agência. Eu vejo muito futuro em você."),
                                                            new ImgOrTxt("Tudo o que você descobriu por aí pode nos ajudar bastante. Apesar de não termos o seu paradeiro exato, temos informação o suficiente para iniciar uma busca maior por Sanfonatti, além de agora termos o nome de um dos capangas da Virtus Atlas para seguir."),
                                                            new ImgOrTxt("Não é qualquer um que conseguiria chegar onde você chegou. Sinta-se orgulhoso. Bem!"),
                                                            new ImgOrTxt("Acho que esse caso do museu encerra por aqui."),
                                                            new ImgOrTxt("Então jovem, está preparado para descobrir as verdades sobre o mundo?"),
                                                            //fim de jogo
                                                            });

        intro[4] = msg5;

        //carregando dicas
        Mensagem dicas0 = new Mensagem("1", new ImgOrTxt[] { new ImgOrTxt("Está foi a única imagem de Sanfonatti que conseguimos restaurar das câmeras de segurança."),
                                                              new ImgOrTxt("Tente analisar o local com as ferramentas que lhe demos, talvez você encontre algo relevante.") });
        dicas[0] = dicas0;

        Mensagem dicas1 = new Mensagem("2", new ImgOrTxt[] { new ImgOrTxt("Estranho, acho que nunca vi essas marcas antes"),
                                                              new ImgOrTxt("Elas parecem ter sido colocadas ali pelo próprio Sanfonatti, continue procurando, talvez isso signifique algo.") });
        dicas[1] = dicas1; //Disco

        Mensagem dicas2 = new Mensagem("3", new ImgOrTxt[] { new ImgOrTxt("Estranho, acho que nunca vi essas marcas antes"),
                                                              new ImgOrTxt("Elas parecem ter sido colocadas ali pelo próprio Sanfonatti, continue procurando, talvez isso signifique algo.") });
        dicas[2] = dicas2; //musicBox

        Mensagem dicas3 = new Mensagem("4", new ImgOrTxt[] { new ImgOrTxt("Estranho, acho que nunca vi essas marcas antes"),
                                                              new ImgOrTxt("Elas parecem ter sido colocadas ali pelo próprio Sanfonatti, continue procurando, talvez isso signifique algo.") });
        dicas[3] = dicas3; //gonzaga

        Mensagem dicas4 = new Mensagem("5", new ImgOrTxt[] { new ImgOrTxt("Esse pedaço de papel foi encontrado na bolsa de Sanfonatti, pelo que conseguimos identificar, essa e a letra dele mas nenhum de nossos especialistas sabe exatamente o que significa."),
                                                             new ImgOrTxt("Achamos que possa ser relevante para o caso.") });
        dicas[4] = dicas4; //mapaCigarro

        Mensagem dicas5 = new Mensagem("6", new ImgOrTxt[] { new ImgOrTxt("Este livro estava na bolsa de Sanfonatti."),
                                                             new ImgOrTxt("Não encontramos nada fora do comum dentro do livro.") });
        dicas[5] = dicas5; //livro

        Mensagem dicas6 = new Mensagem("7", new ImgOrTxt[] { new ImgOrTxt("Me perdoe, jovem, mas estou um tanto ocupado no momento para lhe ajudar."),
                                                             new ImgOrTxt("Vou lhe mandar uma ferramenta que pode ser útil"),
                                                             new ImgOrTxt("www.google.com")});
        dicas[6] = dicas6; //album

        Mensagem dicas7 = new Mensagem("8", new ImgOrTxt[] { new ImgOrTxt("Este é um mapa de calor que traçamos das atividades de Sanfonatti no museu, as áreas com cor mais intensa indicam os setores de maior atividade."),
                                                             new ImgOrTxt("Recomendo você tentar investigar essas áreas.") });
        dicas[7] = dicas7; //mapaCalor

        Mensagem dicas8 = new Mensagem("9", new ImgOrTxt[] { new ImgOrTxt("Deixe-me ver."),
                                                             new ImgOrTxt("Essa digital pertence a Rajish Al-Habib."),
                                                             new ImgOrTxt("25 anos, casado."),
                                                             new ImgOrTxt("Humm, tem ficha na delegacia por porte ilegal de arma."),
                                                             new ImgOrTxt("Nasceu nos emirados árabes, mas reside na cidade do Recife, trabalha como professor de história em escola pública."),});
        dicas[8] = dicas8; //faca

        Mensagem dicas9 = new Mensagem("10", new ImgOrTxt[] { new ImgOrTxt("De acordo com o banco de dados de digitas da nossa agência. Essa digital pertence a Atila Saverin."),
                                                             new ImgOrTxt("Vejamos."),
                                                             new ImgOrTxt("Atila Saverin, 27 anos, solteiro, sem antecedentes criminais, residente na cidade de Exu, trabalha como vendedor em uma loja de produtos agropecuários."), });
        dicas[9] = dicas9; //santo

        Mensagem dicas10 = new Mensagem("11", new ImgOrTxt[] { new ImgOrTxt("Pelo que consegui verificar no nosso banco de dados de digitais, essa digital pertence a Nariadna Gleycielly."),
                                                               new ImgOrTxt("Nariadna, 22 anos, casada, sem antecedentes criminais, reside em Salgueiro e é dona de uma loja de tecidos.") });
        dicas[10] = dicas10; //xilogravura

        Mensagem dicas11 = new Mensagem("12", new ImgOrTxt[] { new ImgOrTxt("Estranho esse símbolo."),
                                                               new ImgOrTxt("Pelo que eu consegui analisar aqui, ele não foi feito por Sanfonatti."),
                                                               new ImgOrTxt("Talvez esteja relacionado com algum dos suspeitos, pode significar algo."),});
        dicas[11] = dicas11; //caixaPoesia

        Mensagem dicas12 = new Mensagem("13", new ImgOrTxt[] { new ImgOrTxt("Essas são as fichas com as informações que eu consegui encontrar dos suspeitos."),
                                                              new ImgOrTxt("Infelizmente é o máximo que eu consigo fazer daqui da agência. Talvez você seja capaz de descobrir mais algumas informações se pesquisar um pouco.") });
        dicas[12] = dicas12; //suspeito

        Mensagem dicas13 = new Mensagem("14", new ImgOrTxt[] { new ImgOrTxt("Veja se você consegue descobrir o que pode ser a continuação dessa frase. Ela pode conter alguma dica de onde ele pode ter deixado algo no museu."),
                                                              new ImgOrTxt("Deve haver algo sobre essa música no museu, Sanfonatti pode ter escondido algo por perto.") });
        dicas[13] = dicas13; //frase

        Mensagem dicas14 = new Mensagem("15", new ImgOrTxt[] { new ImgOrTxt("Esse Sanfonatti parece realmente gostar de escrever em enigmas. A essa altura eu tenho certeza que essa frase significa algo."),
                                                              new ImgOrTxt("Tente descobrir o que significa. ") });
        dicas[14] = dicas14; //cacto

        Mensagem dicas15 = new Mensagem("16", new ImgOrTxt[] { new ImgOrTxt("Sanfonatti parece ter nos deixado uma coordenada escrita nesse mapa."),
                                                              new ImgOrTxt("39°20'37.9”W"),
                                                              new ImgOrTxt("Me avise se você descobrir qual é a outra para que possamos mandar algum agente até o local descobrir o que isso significa.")}); 
        dicas[15] = dicas15; //mapaNordeste

        Mensagem dicas16 = new Mensagem("17", new ImgOrTxt[] { new ImgOrTxt("O cordel foi encontrado por um funcionário do museu no mesmo local onde você encontrou a frase deixada por Sanfonatti."),
                                                              new ImgOrTxt("É bem provável que tenha algo escondido nele. Você consegue descobrir o que é?") });
        dicas[16] = dicas16; //cordel

        Mensagem dicas17 = new Mensagem("18", new ImgOrTxt[] { new ImgOrTxt("Essa carta foi escrita por Raimundo Jacó, e foi encontrada no local indicado pelas coordenadas deixadas por Sanfonatti."),
                                                               new ImgOrTxt("Jacó faz parecer que estava sendo perseguido por ter descoberto algo, logo antes de ter sido assassinado.") });
        dicas[17] = dicas17; //cartaJaco

        Mensagem dicas18 = new Mensagem("19", new ImgOrTxt[] { new ImgOrTxt("Esta é a área do museu em que não existe nenhuma imagem da câmera de segurança de Sanfonatti."),
                                                               new ImgOrTxt("Deve haver algo de importante aí.") });
        dicas[18] = dicas18; //mapaEnzo

        Mensagem dicas19 = new Mensagem("20", new ImgOrTxt[] { new ImgOrTxt("Eu acredito que a esse ponto você já deve entender como a mente de Sanfonatti funciona melhor do que eu."),
                                                              new ImgOrTxt("Essa é a última pista que nos temos. Tente descobrir o que ela significa, é possível que tenha algo que ajude a decifrar nas informações que você já coletou antes."),
                                                              new ImgOrTxt("Boa sorte, jovem.")});
        dicas[19] = dicas19; //quadroBoneco

        //carregando automaticas

        automatico = new Mensagem[] {  new Mensagem("0", new ImgOrTxt[]{ new ImgOrTxt("Olá, novamente jovem. Enquanto você investigava aí no museu, um dos nossas investigadores encontrou uma bolsa jogada no lado de fora do museu perto de uma lixeira. Era a mesma bolsa que Sanfonatti estava usando quando visitou o museu. Estranho não acha? Bem, dentro da bolsa encontramos este pedaço de papel"),
                                                                                              new ImgOrTxt(PlayerInfo.ProcurarPista("mapaCigarro")), //imagem do mapa
                                                                                             new ImgOrTxt("E este livro"),
                                                                                             new ImgOrTxt(PlayerInfo.ProcurarPista("livro")), //foto do livro
                                                                                             new ImgOrTxt("Não sabemos ainda o que significa ou se é relevante. Mas, de qualquer forma, é uma pista, então achei que seria útil enviar."),


                                                        }), new Mensagem("1", new ImgOrTxt[]{ new ImgOrTxt("Essas marcas de novo, estranho. Essa outra palavra embaixo, também parece ter sido feita por Sanfonatti."),
                                                                                              new ImgOrTxt("Realmente estranho."),
                                                                                              new ImgOrTxt("Bem, acha que você consegue descobrir o que essas marcas estranhas significa? Se descobrir, por favor me envie."),


                                                        }),
                                                           new Mensagem("2", new ImgOrTxt[]{ new ImgOrTxt("Bem, pelo que conseguimos coletar das suas pistas, temos 3 suspeitos principais,Rajish Al-Habib, Atila Saverin, e Nariadna Gleycielly, mas só, nossos especialistas não conseguiram extrair nenhuma boa informação disso, é muito pouco para conseguirmos avaliar algo, precisamos de algo maior para prosseguir."),
                                                                                          new ImgOrTxt("Tem também a questão desse símbolo que você encontrou, ele me parece ser relevante."),
                                                                                          new ImgOrTxt("Humm, me desculpe, mas não tem muito que eu esteja conseguindo fazer por aqui. Talvez você consiga encontrar alguma informação sobre os suspeitos que possa ajudar."),
                                                                                          new ImgOrTxt("Tente descobrir alguma informação que possa levar a algum dos suspeitos em particular, algo que possa está relacionado ao caso. Acha que consegue?"),
                                                                                          new ImgOrTxt("Aqui estão as fichas policiais que eu consegui referentes as digitais dos suspeitos que você encontrou aí no museu."),
                                                                                          new ImgOrTxt(PlayerInfo.ProcurarPista("suspeitos")), //ficha suspeitos
                                                                                          new ImgOrTxt("Me envie uma mensagem se encontrar algo importante.")

                                                        }),
                                                        new Mensagem("3", new ImgOrTxt[]{ new ImgOrTxt("Humm. Esse local que você encontrou agora. Um dos funcionários do museu havia encontrado isto escondido aí."),
                                                                                          new ImgOrTxt(PlayerInfo.ProcurarPista("cordel")),  //imagem do cordel
                                                                                          new ImgOrTxt("Não parecia relevante antes, era apenas um pedaço de papel com um cordel escrito, mas seria coincidência demais isso estar no mesmo lugar."),

                                                        }),
                                                         new Mensagem("4", new ImgOrTxt[]{ new ImgOrTxt("Essas marcações deixadas no mapa. Elas parecem apontar uma coordenada geográfica."),
                                                                                          new ImgOrTxt("Humm"),
                                                                                          new ImgOrTxt("Se quisermos encontrar o lugar para onde isso aponta, precisaremos de outra coordenada"),
                                                                                          new ImgOrTxt("Por favor, se descobrir qual é, me avise que eu mandarei um dos nossos agentes averiguar o local.")
                                                         }),
                                                         new Mensagem("5", new ImgOrTxt[]{ new ImgOrTxt("Humm, o que será que isso significa? Esta é a última pista que nos resta. Talvez descobrir o que ela quer dizer possa nos   ajudar a descobrir o que aconteceu com Sanfonatti."),
                                                                                           new ImgOrTxt("Bem jovem. Eu sei que não temos muito para seguir em frente. Mas, se você descobrir onde Sanfonatti pode estar, ou se ele deixou alguma mensagem que possa nos dizer onde procurar, por favor me envie."),

                                                         }),
                                                         new Mensagem("6", new ImgOrTxt[]{ new ImgOrTxt("Bem, jovem. Acho que você tem o direito de saber."),
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
        Invoke("desativarPopupEncaminhado", 2);
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
            for (int i = 0; i < intro[PlayerInfo.etapaAtual].imgOrTxt.Length; i++)
            {
                if (intro[PlayerInfo.etapaAtual].imgOrTxt[i].isImg())
                {
                    ChatListControl.RenderizarImagem(intro[PlayerInfo.etapaAtual].imgOrTxt[i].img, true);
                }
                else
                {
                    ChatListControl.RenderizarTexto(intro[PlayerInfo.etapaAtual].imgOrTxt[i].txt, true);
                }
            }
            intro[PlayerInfo.etapaAtual].enviado = true;
            exploracao = true;
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("enzoCamera"));
        }
        else
        {
            Debug.Log("erro etapa inicial");
        }
    }

    public void EnviarMsgResposta(string input) //primeira celula eh sempre a validacao, a segunda eh sempre a mensagem real
    {
        Debug.Log("A resposta é :"+resposta[etapa].imgOrTxt[0].txt);
        if (resposta[etapa].imgOrTxt[0].txt.Equals(input)) //pesquisar Ignore Case
        {
            for (int i = 1; i < resposta[etapa].imgOrTxt.Length; i++)
            {
                Debug.Log("imgOrTxt.length = "+ resposta[etapa].imgOrTxt.Length+"| i =  "+i);
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
            ChatListControl.RenderizarTexto("Errou! - Faustão", true); // DEFINIR QUAL É A MENSAGEM DE ERRO
        }
    }

    public void EnviarMsgFeedback()
    {
        automatic++;
        etapa++;
        PlayerInfo.AumentarEtapa();
        MainMenu.check = false;    // um boolean que é resetado toda vez que passa de fase, pra controlar, qdo o jogador deve descobrir algo.
        Invoke("EnviarMsgIntro", 5);
    }


    public void EnviarMsgIntro() // a primeira vez que essa funcao for chamada, sera no menu, quando vc apertar no chat. Lembrar de habilitar a pista introdutoria no banco de dados
    {

        if (etapa == 1)
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
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("mapaCalor"));
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
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("frase"));
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
            exploracao = true;

        }
        else if (etapa == 4)
        {
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("cartaJaco"));
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
            PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("mapaEnzo"));
            intro[etapa].enviado = true;
            exploracao = true;
        }
        else if (etapa == 5) //acabou game feedback final.
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
            exploracao = true;
            intro[etapa].enviado = true;
            //acabou o jogo, se for adcionar algo pra quem zerou, é aqui.
        }

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
                    automatic++;
                    //liberar mapaCigarro -  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat              
                }
            }
            else
            {

                int index2 = PlayerInfo.ProcurarPista("albumLuizCostas");
                if (PlayerInfo.pistas[index2].descoberta && !(automatico[automatic].enviado))
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
                    exploracao = false;
                }
            }

        }

        else if (etapa == 1)
        {

            int index = PlayerInfo.ProcurarPista("faca");
            if ((PlayerInfo.pistas[index].descoberta) && (PlayerInfo.pistas[index + 1].descoberta) && (PlayerInfo.pistas[index + 2].descoberta) && (PlayerInfo.pistas[index + 3].descoberta) && !(automatico[automatic].enviado))
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
                exploracao = false;
                // suspeitos deve ser habilitado.  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat
            }

        }

        //etapa 2 não tem validação
        else if (etapa == 2)
        {
            int index = PlayerInfo.ProcurarPista("cacto");
            if ((PlayerInfo.pistas[index].descoberta) && !(automatico[automatic].enviado))
            { //lembrar de dar um tempo para o detetive entrar em contato.
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
                PlayerInfo.AumentarEtapa();
                automatico[automatic].enviado = true;
                automatic++;
                etapa++;  // aumenta automatic, pois nao tem feedback nessa parte.                
                MainMenu.TurnOnChatNofication();
            }

        }



        else if (etapa == 3)
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

            }


        }

        else if (etapa == 4)
        {
            int index = procurarPistaDetetive("quadroBoneco");
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
                exploracao = false;
                automatico[automatic].enviado = true;
                MainMenu.TurnOnChatNofication();
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
        // PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("mapaCigarro"));  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat              
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
        // PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("Suspeito1"));  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat
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
        //PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("Cordel"));  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat
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

