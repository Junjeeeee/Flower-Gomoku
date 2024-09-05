using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audio_Manager : MonoBehaviour
{

    public Slider volumeSlidermusic;
    private AudioSource audioSource;
    public AudioClip[] musicas;

    public float sfxinicial=0.3f;
    private static Audio_Manager instance; // Referência estática para a instância do GameManager

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

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        audioSource = GetComponent<AudioSource>();
        if(SceneManager.GetActiveScene().name == "Menu")volumeSlidermusic = GameObject.FindWithTag("Music_Slider").GetComponent<Slider>();
        if(volumeSlidermusic != null) volumeSlidermusic.value = audioSource.volume;
        //PlayNextMusic();
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "Menu") return;
        volumeSlidermusic = GameObject.FindWithTag("Music_Slider").GetComponent<Slider>();
        if(volumeSlidermusic != null) volumeSlidermusic.value = audioSource.volume;
    }

    int musicaselecionada = 0;
    public void PlayNextMusic()
    {
        audioSource.clip = musicas[musicaselecionada];
        musicaselecionada++;
        if(musicaselecionada == 6) musicaselecionada = 0;
        audioSource.Play();
    }


    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            // A música acabou de ser reproduzida
            PlayNextMusic();
            Debug.Log("A música acabou!");
        }
    }
}
