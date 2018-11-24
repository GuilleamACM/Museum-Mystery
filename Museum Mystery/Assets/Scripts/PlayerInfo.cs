using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public class Pista
    {
        public string id;
        public bool descoberta;

        public Pista(string id)
        {
            this.id = id;
            this.descoberta = false;
        }
    }

    public static int etapaAtual;
    public static Pista[] pistas;

    public PlayerInfo()
    {
        etapaAtual = 0;
        //Todas as pistas serão criadas aqui junto com o array de pistas
        pistas = new Pista[] { new Pista("enzoCamera"), new Pista("discoContinental"), new Pista("musicBox"),
                               new Pista("gonzaga"), new Pista("mapaCigarro"), new Pista("albumLuizCostas"),
                               new Pista("mapaCalor"), new Pista("suspeito1"), new Pista("suspeito2"),
                               new Pista("suspeito3"), new Pista("faca"), new Pista("santo"),
                               new Pista("xilogravuraMoldura"), new Pista("caixaPoesia"), new Pista("cacto"),
                               new Pista("mapaNordeste"), new Pista("cordel"),  new Pista("quadroBoneco")};
    }
    public static int ProcurarPista(string id)
    {
        for (int i = 0; i < pistas.Length; i++)
        {
            if (pistas[i].id.Equals(id))
            {
                return i;
            }

        }
        return -1;
    }

    public static void DescobrirPista(int index)
    {
        if (pistas[index].descoberta == false)
        {
            pistas[index].descoberta = true;
            MainMenu.DicasNotification.SetActive(true);
            Debug.Log("Você descobriu a pista: "+pistas[index].id);
            MainMenu.staticBotaoDicas[index].SetActive(true);
            MainMenu.staticBotaoPista[index].SetActive(true);
            MainMenu.ARTextNotification.SetActive(true);
            StaticCoroutine.DoCoroutine(2);
        }
        else
        {
            Debug.Log("Pista já descoberta: "+index);
        }

    }

    public static void AumentarEtapa()
    {
        etapaAtual++;
        if (etapaAtual == 1)
        {
            MainMenu.Etapa1.SetActive(true);
        }
        else if (etapaAtual == 2)
        {
            MainMenu.Etapa2.SetActive(true);
        }
        else if (etapaAtual == 3)
        {
            MainMenu.Etapa3.SetActive(true);
        }
        else
        {
            MainMenu.Etapa4.SetActive(true);
        }
    }
}