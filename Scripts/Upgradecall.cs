using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradecall : MonoBehaviour
{
    // Start is called before the first frame update
    public float sfxvolume = 0.3f;
    public float musicvolume = 1f;
    private AudioSource audioSource;
    public bool a;
    public Audio_Manager musca;
    public GameObject menu_normal;
    public GameObject escolherAI;
    public Info_Walker walker;

    public GameObject star0;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star4;
    public GameObject star5;
    public GameObject star6;
    public GameObject star7;
    public GameObject star8;
    public GameObject star9;
    public GameObject star10;
    public GameObject star11;
    public GameObject star12;

    
    void Start()
    {
        //audioSource = GameObject.FindWithTag("musca").GetComponent<AudioSource>();
        //musca = GameObject.FindWithTag("musca").GetComponent<Audio_Manager>();
        walker = GameObject.FindWithTag("Walker").GetComponent<Info_Walker>();
        star();
    }

    public void star()
    {
        if(walker.vitoriaRandom) star0.SetActive(true);
        if(walker.vitoriaConfused) star1.SetActive(true);
        if(walker.vitoriaGreedy) star2.SetActive(true);
        if(walker.vitoriaHateful) star3.SetActive(true);
        if(walker.vitoriaLocal) star4.SetActive(true);
        if(walker.vitoriaSlow) star5.SetActive(true);
        if(walker.vitoriaNormal) star6.SetActive(true);
        if(walker.vitoriaBud) star7.SetActive(true);
        if(walker.vitoriaCheap) star8.SetActive(true);
        if(walker.vitoriaQuick) star9.SetActive(true);
        if(walker.vitoriaFred) star10.SetActive(true);
        if(walker.vitoriaJeff) star11.SetActive(true);
        if(walker.vitoriaUnoptimized) star12.SetActive(true);
    }
    public void SairDoJogo()
    {
        // Sai do jogo (funciona no build, não no Editor)
        Application.Quit();

    }

    public void Mudamusca(float mudar)
    {
        if(audioSource == null) audioSource = GameObject.FindWithTag("musca").GetComponent<AudioSource>();
        audioSource.volume = mudar;
    }

    public void troca(bool a)
    {
        menu_normal.SetActive(!a);
        escolherAI.SetActive(a);
    }

    public void escolhe_ai(string ai)
    {
        walker.AI = ai;
        walker.go_play();
    }

    public void lesgotutorial()
    {
        walker.AI = "none";
        walker.go_tutorial();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
