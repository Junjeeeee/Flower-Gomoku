using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public string tipo = "normal";
    public Board board_controller;
    public playercontroller player2;
    void Start()
    {
        board_controller = GameObject.FindWithTag("GameController").GetComponent<Board>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<playercontroller>();
    }

    public bool valid(int posx, int posy, ref char[,] board)
    {   
        if(posx < (3-((board_controller.tamanho_tabuleiro-4)/2)) || posy < (3-((board_controller.tamanho_tabuleiro-4)/2))) return false;
        if(posx > (6+((board_controller.tamanho_tabuleiro-4)/2)) || posy > (6+((board_controller.tamanho_tabuleiro-4)/2))) return false;
        if(board[posy,posx] != 'b') return false;
        return true;
    }

    public virtual void escolha()
    {
        int tentativas = 0;
        int x,y;
        do
        {
            tentativas++;
            x = Random.Range(0,11);
            y = Random.Range(0,11);
            if(tentativas > 1000) return; //antri travamento i guess
        }
        while(!valid(x,y,ref board_controller.board));

        player2.buy_seeds(x,y,1);
    }



    void Update()
    {

    }
}
