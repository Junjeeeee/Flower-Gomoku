using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaso : MonoBehaviour
{
    // Start is called before the first frame update
    public Board game_board;
    public playercontroller player1;
    public playercontroller player2;
    public int posx,posy;
    public int playerid = 1;
    public char nome;
    public bool two_players = true;

    private void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if(game_board.board[posy,posx] == 'b') player1.buy_seeds(posx,posy,0);

        }
        if (Input.GetMouseButtonDown(1))
        {
            if(game_board.board[posy,posx] == 'b') player2.buy_seeds(posx,posy,1);
        }
        

    }
    void Start()
    {
        game_board = GameObject.FindWithTag("GameController").GetComponent<Board>();
        player1 = GameObject.FindWithTag("Player1").GetComponent<playercontroller>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<playercontroller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
