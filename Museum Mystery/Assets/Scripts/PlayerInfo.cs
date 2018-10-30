using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    
    public class Pista
    {
        public string id;
        public int etapa;
        public bool descoberta;

        public Pista(string id, int etapa)
        {
            this.id = id;
            this.etapa = etapa;
            this.descoberta = false;
        }
    }

    public int etapaAtual;
    public Pista[] pistas;
    
	// Use this for initialization
	void Start () {
        etapaAtual = 0;
        Pista pista1 = new Pista ("Dale", 1);
        //Todas as pistas serão inicializadas aqui
        pistas = new Pista[] {pista1, };
        //O array de pistas será inicializado aqui

	}

    public int ProcurarPista (string id)
    {
        for(int i = 0; i < pistas.Length; i++)
        {
            if (pistas[i].id.Equals(id))
            {
                return i;
            }

        }
        return 999;
    }

    public void DesobrirPista (int index)
    {
        pistas[index].descoberta = true;
        //Inserir aqui código para habilitar o botão dessa pista na database
    }
    
    public void AumentarEtapa()
    {
        etapaAtual++;
        //Inserir aqui código pra habilitar a próxima etapa
    }
}
