using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detetive : MonoBehaviour {


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


    public Mensagem[] dicas = new Mensagem[] {new Mensagem("teste", "você achou a pista teste", "você já achou essa pista"), };
    public Mensagem[] feedback = new Mensagem[] {new Mensagem("id", "Parabéns você acertou"), } ;
    public Mensagem[] intro = new Mensagem[] {new Mensagem("id", "Comecou game"), };
    public Mensagem[] automatico = new Mensagem[] {new Mensagem("id", "mensagem automatica qdo descobrir as criptografias"), } ;
    public Mensagem[] resposta = new Mensagem[] {new Mensagem("id", "resposta", "A resposta é: pega na minha e balança"), };
    public pistaDetetive[] pistasDetetive = new pistaDetetive[] {new pistaDetetive("Criptografia1"), new pistaDetetive("Criptografia2"), new pistaDetetive("Criptografia3"), }; // esse array deve ser mapeado igualmente a o array de dicas. 
    public bool exploracao = false; // para bloquear e desbloquear o envio de RESPOSTAS
    public int etapa = 0;


    public int procurarPistaDetetive(string id)
    {

        for(int index=0;index<pistasDetetive.Length; index++)
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
                if(dicas[i].enviado)
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


    public void EnviarMsgResposta(string input) {
        if (resposta[etapa].text.Equals(input))
        {
            Debug.Log(resposta[etapa].text2);
            EnviarMsgFeedback();
        } else
        {
            Debug.Log("Errou! - Faustão");
        }
    }

    public void EnviarMsgFeedback()
    {
        Debug.Log(feedback[etapa].text);
        etapa++;
        PlayerInfo.AumentarEtapa();
        //chamar intro 2
    }

    public void EnviarMsgIntro() // a primeira vez que essa funcao for chamada, sera no menu, quando vc apertar no chat. Lembrar de habilitar a pista introdutoria no banco de dados
    {
   
        if(etapa == 1)
        {
            Debug.Log(intro[etapa].text); // introdução
            intro[0].enviado = true;
            exploracao = true;

        }
        else if (etapa == 2)
        {

        }
        else if (etapa == 3)
        {

        }
        else if (etapa == 4)
        {

        }
    }

    void Update()
    {

        if (etapa == 1)
        {
            if((PlayerInfo.pistas[1].descoberta & PlayerInfo.pistas[2].descoberta & PlayerInfo.pistas[3].descoberta) & !(automatico[0].enviado)) //as pistas 1,2,3 = Criptografia1,Criptografia2,Criptografia3
            {
                Debug.Log(automatico[0].text);
                automatico[0].enviado = true;
               // PlayerInfo.DescobrirPista(PlayerInfo.ProcurarPista("MapaCigarro"));  essa linha terá que ser chamada ao abrir o chat, para só adicionar ao banco de pistas se voce tiver entrado no chat               
            }

            int index1 = procurarPistaDetetive("Criptografia1"); // talvez, inicializar todas essas variaveis no start() para só acessar no update ou inves de sempre procurar.
            int index2 = index1 + 1;
            int index3 = index2 + 1;

            if ((pistasDetetive[index1].enviado) || (pistasDetetive[index2].enviado) || (pistasDetetive[index3].enviado))
            {
                exploracao = false;

            }

        }
        else if(etapa == 2)
        {

        }
        else if (etapa == 3)
        {

        }
        else if (etapa == 4)
        {

        }
        else if (etapa == 5)
        {

        }
    }
}
