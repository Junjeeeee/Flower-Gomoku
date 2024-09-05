using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playercontroller : MonoBehaviour
{
    // Start is called before the first frame update

    public float sun_grow = 1f;
    public float seed_cost = 3f;
    public float weed_cost = 2f;
    public float destroy_weed_cost = 2f;
    public float sun = 0f;
    public float total_sun = 0f;

    public int amount_seeds = 0;
    public int amount_buds = 0;
    public int amount_flowers = 0;
    public int amount_big_flowers = 0;

    public bool pause = false;
    public Board game_board;
    public int player = 0;
    public int points;
    public TextMeshProUGUI sun_txt;
    public TextMeshProUGUI sun_per_second_txt;
    public TextMeshProUGUI playerstats;
    public Image sprite;

    public bool AI = false;
    public AI IA; 

    /*
    Sun collected:

    Amount of plants
    Buds:
    Flowers:
    Big Flowers:
    Total:

    POINTS
    Buds:
    Flowers:
    Big Flowers:
    Total:

    */
    public void Game_Over()
    {
        pause = true;
        points = (amount_buds) + (amount_flowers*2) + (amount_big_flowers*3);

        playerstats.text= "POINTS: " + points.ToString("") + "\n\n"
        + "Sun collected: " + Mathf.FloorToInt(total_sun).ToString("F2") + "\n\n"
        + "AMOUNT OF PLANTS\nSeeds: " + amount_seeds.ToString("") + '\n'
        + "Buds: " + amount_buds.ToString("") + '\n'
        + "Flowers: " + amount_flowers.ToString("") + '\n'
        + "Big Flowers: " + amount_big_flowers.ToString("") + '\n'
        + "Total: " + (amount_big_flowers + amount_flowers + amount_buds).ToString("") + "\n\n"
        + "POINTS\nBuds: " + amount_buds.ToString("") + '\n'
        + "Flowers: " + (amount_flowers*2).ToString("") + '\n'
        + "Big Flowers: " + (amount_big_flowers*3).ToString("") + '\n'
        + "Total: " + points.ToString("") + "\n\n";
    }
    void Start()
    {
        game_board = GameObject.FindWithTag("GameController").GetComponent<Board>();
        IA = GameObject.FindWithTag("AI").GetComponent<AI>();
        if(IA == null)
        {
            IA = GameObject.FindWithTag("AI").GetComponent<MinMax_Max>();
        }
        if( IA != null && IA.tipo == "cheap" && AI) seed_cost = 2.70f;

        sprite = GameObject.FindWithTag("enemy").GetComponent<Image>();
        if(IA != null && AI) escolhe_sprite(IA.tipo);
    }

    public Sprite Randomm;
    public Sprite Confused;
    public Sprite Greedy;

    public Sprite Hateful;
    public Sprite Slow;
    public Sprite Local;

    public Sprite Normal;
    public Sprite Bud;
    
    public Sprite Cheap;
    public Sprite Quick;
    public Sprite Fred;

    public Sprite Jeff;
    public Sprite Unoptimized;
    public void escolhe_sprite(string tipo)
    {
        if(tipo == "random") sprite.sprite = Randomm;
        else if(tipo == "confused") sprite.sprite = Confused;
        else if(tipo == "greedy") sprite.sprite = Greedy;
        else if(tipo == "hateful") sprite.sprite = Hateful;
        else if(tipo == "slow") sprite.sprite = Slow;
        else if(tipo == "local") sprite.sprite = Local;
        else if(tipo == "normal") sprite.sprite = Normal;
        else if(tipo == "bud") sprite.sprite = Bud;
        else if(tipo == "cheap") sprite.sprite = Cheap;
        else if(tipo == "quick") sprite.sprite = Quick;
        else if(tipo == "Fred") sprite.sprite = Fred;
        else if(tipo == "Jeff") sprite.sprite = Jeff;
        else if(tipo == "unoptimized") sprite.sprite = Unoptimized;
    }

    public void buy_seeds(int posx,int posy, int playere)
    {
        if(sun < seed_cost) return;
        sun -= seed_cost;
        game_board.board[posy,posx] = playere.ToString()[0];
        game_board.print_piece(posy,posx);
        game_board.merge_check(posx,posy,playere,ref game_board.board,true);
    }

    bool movendo = false;
    public IEnumerator human_time()
    {
        int x;
        x = Random.Range(0,50);
        if(IA.tipo == "slow") x = Random.Range(100,400);
        if(IA.tipo == "quick") x = 0;
        if(IA.tipo == "hateful") x = Random.Range(0,300);
        float y = x/100f;
        yield return new WaitForSeconds(y);
        IA.escolha();
        movendo = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(!pause){
            sun += Time.fixedDeltaTime * (1+(amount_buds*0.015f)+(amount_flowers*0.03f)+(amount_big_flowers*0.045f));
            total_sun += Time.fixedDeltaTime * (1+(amount_buds*0.015f)+(amount_flowers*0.03f)+(amount_big_flowers*0.045f));
            sun_txt.text = Mathf.FloorToInt(sun).ToString("F2") + " Sun";
            sun_per_second_txt.text = (1+(amount_buds*0.01f)+(amount_flowers*0.02f)+(amount_big_flowers*0.03f)).ToString("F2") + "\nSun per second";
        }

        if(AI)
        {
            if(sun >= seed_cost && !movendo)
            {
                movendo = true;
                StartCoroutine(human_time());
            }
        }
        
    }
}
