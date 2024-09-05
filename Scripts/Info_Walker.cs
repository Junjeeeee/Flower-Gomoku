using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Info_Walker : MonoBehaviour
{
    private static Info_Walker instance;
    public string AI = "Random";
    public GameObject Random;
    public GameObject Confused;
    public GameObject Greedy;

    public GameObject Slow;
    public GameObject Hateful;
    public GameObject Local;

    public GameObject Normal;
    public GameObject Buddy;

    public GameObject Cheap;
    public GameObject Quick;
    public GameObject Fred;

    public GameObject Jeff;
    public GameObject Unoptimized;

    public bool vitoriaRandom = false;
    public bool vitoriaConfused = false;
    public bool vitoriaGreedy = false;
    public bool vitoriaHateful = false;
    public bool vitoriaSlow = false;
    public bool vitoriaLocal = false;
    public bool vitoriaNormal = false;
    public bool vitoriaBud = false;
    public bool vitoriaCheap = false;
    public bool vitoriaQuick = false;
    public bool vitoriaFred = false;
    public bool vitoriaJeff = false;
    public bool vitoriaUnoptimized = false;
    private void Awake()
    {
        // Verifica se já existe uma instância do GameManager
        if (instance == null)
        {
            // Se não houver uma instância, define esta como a instância única
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se já existir uma instância, destrói este objeto duplicado
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Battlefield")
        {
            if(AI == "Random") Instantiate(Random,Vector3.zero,Quaternion.identity);
            else if(AI == "Confused") Instantiate(Confused,Vector3.zero,Quaternion.identity);
            else if(AI == "Greedy") Instantiate(Greedy,Vector3.zero,Quaternion.identity);
            else if(AI == "Slow") Instantiate(Slow,Vector3.zero,Quaternion.identity);
            else if(AI == "Hateful") Instantiate(Hateful,Vector3.zero,Quaternion.identity);
            else if(AI == "Local") Instantiate(Local,Vector3.zero,Quaternion.identity);
            else if(AI == "Normal") Instantiate(Normal,Vector3.zero,Quaternion.identity);
            else if(AI == "Buddy") Instantiate(Buddy,Vector3.zero,Quaternion.identity);
            else if(AI == "Cheap") Instantiate(Cheap,Vector3.zero,Quaternion.identity);
            else if(AI == "Quick") Instantiate(Quick,Vector3.zero,Quaternion.identity);
            else if(AI == "Fred") Instantiate(Fred,Vector3.zero,Quaternion.identity);
            else if(AI == "Jeff") Instantiate(Jeff,Vector3.zero,Quaternion.identity);
            else if(AI == "Unoptimized") Instantiate(Unoptimized,Vector3.zero,Quaternion.identity);
        }
    }

    public void go_play()
    {
        SceneManager.LoadScene("Battlefield");
    }
    public void go_menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void go_tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
