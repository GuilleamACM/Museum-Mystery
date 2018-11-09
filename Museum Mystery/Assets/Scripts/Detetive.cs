using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detetive : MonoBehaviour {

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
    public Mensagem[] intro;
    public Mensagem[] resposta = new Mensagem[] {new Mensagem("id", "resposta", "A resposta é: pega na minha e balança"), };
    public bool exploracao;
    public int etapa = 0;

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
                Debug.Log(dicas[i].text);
                dicas[i].enviado = true;
                return;
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
    }

    public void EnviarMsgIntro()
    {
        Debug.Log(intro[etapa].text);
    }
}
