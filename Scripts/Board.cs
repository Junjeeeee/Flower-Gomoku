using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Board : MonoBehaviour
{

    public char[,] board;
    public GameObject[,] board_gameobjects;

    public float time = 0f;
    public playercontroller player1;
    public playercontroller player2;

    public TextMeshProUGUI estado;
    public TextMeshProUGUI time_txt;
    public int tamanho_tabuleiro = 4;
    public Info_Walker walker;
    public bool tutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        initialize();
        player1 = GameObject.FindWithTag("Player1").GetComponent<playercontroller>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<playercontroller>();
        walker = GameObject.FindWithTag("Walker").GetComponent<Info_Walker>();
    }

    void initialize()
    {
        board_gameobjects = new GameObject[10,10];
        board = new char[10,10];
        for(int i=0;i<10;i++)
        {
            for(int j=0;j<10;j++)
            {
                board[i,j] = 'b';
                print_piece(i,j);
                board_gameobjects[i,j].SetActive(false);
            }
        }
    }

    public GameObject blank; // b
    public GameObject white_seed; // 0
    public GameObject dark_seed; // 1
    public GameObject white_bud; //2
    public GameObject dark_bud; //3
    public GameObject white_flower; //4
    public GameObject dark_flower; //5
    public GameObject big_white_flower; //6
    public GameObject big_dark_flower; //7

    public GameObject end_game;

    public bool print_piece(int y, int x)
    {

        if(board_gameobjects[y,x] != null)
        {
            if(board_gameobjects[y,x].GetComponent<vaso>().nome == board[y,x]) return false;
            Destroy(board_gameobjects[y,x]);
        }

        char atual = board[y,x];
        GameObject obj = null;
        Vector3 pos = Vector3.zero;
        pos.x = x;
        pos.y = y;
        if(atual == 'b')
        {
            obj = Instantiate(blank,pos,Quaternion.identity);
        }
        else if(atual == '0')
        {
            obj = Instantiate(white_seed,pos,Quaternion.identity);
        }
        else if(atual == '1')
        {
            obj = Instantiate(dark_seed,pos,Quaternion.identity);
        }
        else if(atual == '2')
        {
            obj = Instantiate(white_bud,pos,Quaternion.identity);
        }
        else if(atual == '3')
        {
            obj = Instantiate(dark_bud,pos,Quaternion.identity);
        }
        else if(atual == '4')
        {
            obj = Instantiate(white_flower,pos,Quaternion.identity);
        }
        else if(atual == '5')
        {
            obj = Instantiate(dark_flower,pos,Quaternion.identity);
        }
        else if(atual == '6')
        {
            obj = Instantiate(big_white_flower,pos,Quaternion.identity);
        }
        else if(atual == '7')
        {
            obj = Instantiate(big_dark_flower,pos,Quaternion.identity);
        }

        board_gameobjects[y,x] = obj;
        obj.GetComponent<vaso>().posx = x;
        obj.GetComponent<vaso>().posy = y;
        obj.GetComponent<vaso>().nome = atual;
        return true;
    }

    public void count()
    {
        int seeds1=0;
        int seeds2=0;
        int buds1=0;
        int buds2=0;
        int flowers1=0;
        int flowers2=0;
        int bigflowers1=0;
        int bigflowers2=0;
        for(int i=0;i<10;i++)
        {
            for(int j=0;j<10;j++)
            {
                char atual = board[i,j];
                if(atual == '0')
                {
                    seeds1++;
                }
                else if(atual == '1')
                {
                   seeds2++;
                }
                else if(atual == '2')
                {
                    buds1++;
                }
                else if(atual == '3')
                {
                    buds2++;
                }
                else if(atual == '4')
                {
                    flowers1++;
                }
                else if(atual == '5')
                {
                    flowers2++;
                }
                else if(atual == '6')
                {
                    bigflowers1++;
                }
                else if(atual == '7')
                {
                    bigflowers2++;
                }
            }
        }

        player1.amount_seeds = seeds1;
        player1.amount_buds = buds1;
        player1.amount_flowers = flowers1;
        player1.amount_big_flowers = bigflowers1;
        player2.amount_seeds = seeds2;
        player2.amount_buds = buds2;
        player2.amount_flowers = flowers2;
        player2.amount_big_flowers = bigflowers2;
    }

    public bool valid(int posx, int posy)
    {
        if(posx < 0 || posy < 0) return false;
        if(posx > 9 || posy > 9) return false;
        return true;
    }

    public bool merge_plus(int posx,int posy,int player_id,ref char[,] board,bool real)
    {
        bool foi = false;
        if(!valid(posx,posy)) return false;
        if(board[posy,posx] == 'b') return false;

        if(valid(posx-2,posy) && valid(posx+2,posy) && valid(posx,posy-2) && valid(posx,posy+2))
        {
            

            if(board[posy,posx+1] != 'b' && (board[posy,posx+1]+player_id) % 2 == 0 && board[posy,posx+1] < '8'
            && board[posy,posx+2] != 'b' && (board[posy,posx+2]+player_id) % 2 == 0 && board[posy,posx+2] < '8'
            && board[posy,posx-1] != 'b' && (board[posy,posx-1]+player_id) % 2 == 0 && board[posy,posx-1] < '8'
            && board[posy,posx-2] != 'b' && (board[posy,posx-2]+player_id) % 2 == 0 && board[posy,posx-2] < '8'
            && board[posy+1,posx] != 'b' && (board[posy+1,posx]+player_id) % 2 == 0 && board[posy+1,posx] < '8'
            && board[posy+2,posx] != 'b' && (board[posy+2,posx]+player_id) % 2 == 0 && board[posy+2,posx] < '8'
            && board[posy-1,posx] != 'b' && (board[posy-1,posx]+player_id) % 2 == 0 && board[posy-1,posx] < '8'
            && board[posy-2,posx] != 'b' && (board[posy-2,posx]+player_id) % 2 == 0 && board[posy-2,posx] < '8'
            )
            {
                board[posy,posx] = (6 + player_id).ToString()[0];
                board[posy,posx+1] = (6 + player_id).ToString()[0];
                board[posy,posx+2] = (6 + player_id).ToString()[0];
                board[posy,posx-1] = (6 + player_id).ToString()[0];
                board[posy,posx-2] = (6 + player_id).ToString()[0];
                board[posy+1,posx] = (6 + player_id).ToString()[0];
                board[posy+2,posx] = (6 + player_id).ToString()[0];
                board[posy-1,posx] = (6 + player_id).ToString()[0];
                board[posy-2,posx] = (6 + player_id).ToString()[0];
                if(real) foi |= print_piece(posy,posx);
                if(real) foi |= print_piece(posy,posx+1);
                if(real) foi |= print_piece(posy,posx+2);
                if(real) foi |= print_piece(posy,posx-1);
                if(real) foi |= print_piece(posy,posx-2);
                if(real) foi |= print_piece(posy+1,posx);
                if(real) foi |= print_piece(posy+2,posx);
                if(real) foi |= print_piece(posy-1,posx);
                if(real) foi |= print_piece(posy-2,posx);
            }
        }
        if(valid(posx-2,posy-2) && valid(posx-2,posy+2) && valid(posx+2,posy-2) && valid(posx+2,posy+2))
        {
            if(board[posy+1,posx+1] != 'b' && (board[posy+1,posx+1]+player_id) % 2 == 0 && board[posy+1,posx+1] < '8'
                && board[posy+2,posx+2] != 'b' && (board[posy+2,posx+2]+player_id) % 2 == 0 && board[posy+2,posx+2] < '8'
                && board[posy-1,posx-1] != 'b' && (board[posy-1,posx-1]+player_id) % 2 == 0 && board[posy-1,posx-1] < '8'
                && board[posy-2,posx-2] != 'b' && (board[posy-2,posx-2]+player_id) % 2 == 0 && board[posy-2,posx-2] < '8'
                && board[posy+1,posx-1] != 'b' && (board[posy+1,posx-1]+player_id) % 2 == 0 && board[posy+1,posx-1] < '8'
                && board[posy+2,posx-2] != 'b' && (board[posy+2,posx-2]+player_id) % 2 == 0 && board[posy+2,posx-2] < '8'
                && board[posy-1,posx+1] != 'b' && (board[posy-1,posx+1]+player_id) % 2 == 0 && board[posy-1,posx+1] < '8'
                && board[posy-2,posx+2] != 'b' && (board[posy-2,posx+2]+player_id) % 2 == 0 && board[posy-2,posx+2] < '8'
            )
            {
                board[posy,posx] = (6 + player_id).ToString()[0];
                board[posy+1,posx+1] = (6 + player_id).ToString()[0];
                board[posy+2,posx+2] = (6 + player_id).ToString()[0];
                board[posy-1,posx-1] = (6 + player_id).ToString()[0];
                board[posy-2,posx-2] = (6 + player_id).ToString()[0];
                board[posy+1,posx-1] = (6 + player_id).ToString()[0];
                board[posy+2,posx-2] = (6 + player_id).ToString()[0];
                board[posy-1,posx+1] = (6 + player_id).ToString()[0];
                board[posy-2,posx+2] = (6 + player_id).ToString()[0];
                if(real) foi |= print_piece(posy,posx);
                if(real) foi |= print_piece(posy+1,posx+1);
                if(real) foi |= print_piece(posy+2,posx+2);
                if(real) foi |= print_piece(posy-1,posx-1);
                if(real) foi |= print_piece(posy-2,posx-2);
                if(real) foi |= print_piece(posy+1,posx-1);
                if(real) foi |= print_piece(posy+2,posx-2);
                if(real) foi |= print_piece(posy-1,posx+1);
                if(real) foi |= print_piece(posy-2,posx+2);
            }
        }
        return foi;
    }

    char MaxChar(char a, char b)
{
    int asciiA = (int)a;
    int asciiB = (int)b;
    
    if (asciiA > asciiB)
    {
        return a;
    }
    else
    {
        return b;
    }
}

    //LEARNING: Dont use parse, instead you can use the ascii code, or, easier, make a matrix of int instead of char, i'll refatorate this in the future
    // but i'll keep this comment for me to remenber how trash this code was
    public void merge_check(int posx,int posy,int player_id,ref char[,] board,bool real)
    {
        bool foi = false;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx,posy,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx-1,posy,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx-2,posy,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx+1,posy,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx+2,posy,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx,posy-1,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx,posy-2,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx,posy+1,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx,posy+2,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx-1,posy-1,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx-2,posy-2,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx+1,posy+1,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx+2,posy+2,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx-1,posy+1,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx-2,posy+2,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx+1,posy-1,player_id,ref board,real) || foi;
        if(!((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" ) && player_id == 1)) foi = merge_plus(posx+2,posy-2,player_id,ref board,real) || foi;

        //if(foi) return;

        //two pointers

        int pont1 =0,pont2= 0;
        bool p1v=true,p2v=true;
        bool me_5 = false;
        bool me_3 = false;
        int quant = 1;
        int move = 0;

        //3 vertical

        while((p1v || p2v))
        {
            if((move%2 == 0) && p1v)
            {
                pont1++;
                if(valid(posx,posy+pont1) && board[posy+pont1,posx] != 'b' && board[posy+pont1,posx] < '8' && (int.Parse(board[posy+pont1,posx].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont1--;
                    p1v = false;
                }
            }
            else if(p2v)
            {
                pont2++;
                if(valid(posx,posy-pont2) && board[posy-pont2,posx] != 'b' && board[posy-pont2,posx] < '8' && (int.Parse(board[posy-pont2,posx].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont2--;
                    p2v = false;
                }
            }
            move++;
        }

        if(quant >= 3)
        {
            me_3 = true;
            while(pont1 > 0)
            {
                board[posy+pont1,posx] = MaxChar(board[posy+pont1,posx],(2+player_id).ToString()[0]);
                if(real) print_piece(posy+pont1,posx);
                pont1--;
            }
            while(pont2 > 0)
            {
                board[posy-pont2,posx] = MaxChar(board[posy-pont2,posx],(2+player_id).ToString()[0]);
                if(real) print_piece(posy-pont2,posx);
                pont2--;
            }
        }

        p1v = true; p2v = true;
        quant = 1; pont1 = 0; pont2 = 0; move = 0;

        //3 horizontal

        while((p1v || p2v))
        {
            if((move%2 == 0) && p1v)
            {
                pont1++;
                if(valid(posx+pont1,posy) && board[posy,posx+pont1] != 'b' && board[posy,posx+pont1] < '8' && (int.Parse(board[posy,posx+pont1].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont1--;
                    p1v = false;
                }
            }
            else if(p2v)
            {
                pont2++;
                if(valid(posx-pont2,posy) && board[posy,posx-pont2] != 'b' && board[posy,posx-pont2] < '8' && (int.Parse(board[posy,posx-pont2].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont2--;
                    p2v = false;
                }
            }
            move++;
        }

        if(quant >= 3)
        {
            me_3 = true;
            while(pont1 > 0)
            {
                board[posy,posx+pont1] = MaxChar(board[posy,posx+pont1],(2+player_id).ToString()[0]);
                if(real) print_piece(posy,posx+pont1);
                pont1--;
            }
            while(pont2 > 0)
            {
                board[posy,posx-pont2] = MaxChar(board[posy,posx-pont2],(2+player_id).ToString()[0]);
                if(real) print_piece(posy,posx-pont2);
                pont2--;
            }
        }

        p1v = true; p2v = true;
        quant = 1; pont1 = 0; pont2 = 0; move = 0;

        //3 diagonal right

        while((p1v || p2v))
        {
            if((move%2 == 0) && p1v)
            {
                pont1++;
                if(valid(posx+pont1,posy+pont1) && board[posy+pont1,posx+pont1] != 'b' && board[posy+pont1,posx+pont1] < '8' && (int.Parse(board[posy+pont1,posx+pont1].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont1--;
                    p1v = false;
                }
            }
            else if(p2v)
            {
                pont2++;
                if(valid(posx-pont2,posy-pont2) && board[posy+pont1,posx+pont1] != 'b' && board[posy-pont2,posx-pont2] < '8' && (int.Parse(board[posy-pont2,posx-pont2].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont2--;
                    p2v = false;
                }
            }
            move++;
        }

        if(quant >= 3)
        {
            me_3 = true;
            while(pont1 > 0)
            {
                board[posy+pont1,posx+pont1] = MaxChar(board[posy+pont1,posx+pont1],(2+player_id).ToString()[0]);
                if(real) print_piece(posy+pont1,posx+pont1);
                pont1--;
            }
            while(pont2 > 0)
            {
                board[posy-pont2,posx-pont2] = MaxChar(board[posy-pont2,posx-pont2],(2+player_id).ToString()[0]);
                if(real) print_piece(posy-pont2,posx-pont2);
                pont2--;
            }
        }

        p1v = true; p2v = true;
        quant = 1; pont1 = 0; pont2 = 0; move = 0;

        //3 diagonal left

        while((p1v || p2v))
        {
            if((move%2 == 0) && p1v)
            {
                pont1++;
                if(valid(posx+pont1,posy-pont1) && board[posy-pont1,posx+pont1] != 'b' && board[posy-pont1,posx+pont1] < '8' && (int.Parse(board[posy-pont1,posx+pont1].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont1--;
                    p1v = false;
                }
            }
            else if(p2v)
            {
                pont2++;
                if(valid(posx-pont2,posy+pont2) && board[posy+pont2,posx-pont2] != 'b' && board[posy+pont2,posx-pont2] < '8' && (int.Parse(board[posy+pont2,posx-pont2].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont2--;
                    p2v = false;
                }
            }
            move++;
        }

        if(quant >= 3)
        {
            me_3 = true;
            while(pont1 > 0)
            {
                board[posy-pont1,posx+pont1] = MaxChar(board[posy-pont1,posx+pont1],(2+player_id).ToString()[0]);
                if(real) print_piece(posy-pont1,posx+pont1);
                pont1--;
            }
            while(pont2 > 0)
            {
                board[posy+pont2,posx-pont2] = MaxChar(board[posy+pont2,posx-pont2],(2+player_id).ToString()[0]);
                if(real) print_piece(posy+pont2,posx-pont2);
                pont2--;
            }
        }

        p1v = true; p2v = true;
        quant = 1; pont1 = 0; pont2 = 0; move = 0;

        //5 vertical

        while((p1v || p2v))
        {
            if((move%2 == 0) && p1v)
            {
                pont1++;
                if(valid(posx,posy+pont1) && board[posy+pont1,posx] != 'b' && board[posy+pont1,posx] < '8' && (int.Parse(board[posy+pont1,posx].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont1--;
                    p1v = false;
                }
            }
            else if(p2v)
            {
                pont2++;
                if(valid(posx,posy-pont2) && board[posy-pont2,posx] != 'b' && board[posy-pont2,posx] < '8' && (int.Parse(board[posy-pont2,posx].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont2--;
                    p2v = false;
                }
            }
            move++;
        }

        if(quant >= 5)
        {
            me_5 = true;
            while(pont1 > 0)
            {
                board[posy+pont1,posx] = MaxChar(board[posy+pont1,posx],(4+player_id).ToString()[0]);
                if((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" )&& player_id == 1)  board[posy+pont1,posx] = (6+player_id).ToString()[0];
                if(real) print_piece(posy+pont1,posx);
                pont1--;
            }
            while(pont2 > 0)
            {
                board[posy-pont2,posx] = MaxChar(board[posy-pont2,posx],(4+player_id).ToString()[0]);
                if((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" )&& player_id == 1)  board[posy-pont2,posx] = (6+player_id).ToString()[0];
                if(real) print_piece(posy-pont2,posx);
                pont2--;
            }
        }

        p1v = true; p2v = true;
        quant = 1; pont1 = 0; pont2 = 0; move = 0;

        //5 horizontal

        while((p1v || p2v))
        {
            if((move%2 == 0) && p1v)
            {
                pont1++;
                if(valid(posx+pont1,posy) && board[posy,posx+pont1] != 'b' && board[posy,posx+pont1] < '8' && (int.Parse(board[posy,posx+pont1].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont1--;
                    p1v = false;
                }
            }
            else if(p2v)
            {
                pont2++;
                if(valid(posx-pont2,posy) && board[posy,posx-pont2] != 'b' && board[posy,posx-pont2] < '8' && (int.Parse(board[posy,posx-pont2].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont2--;
                    p2v = false;
                }
            }
            move++;
        }

        if(quant >= 5)
        {
            me_5 = true;
            while(pont1 > 0)
            {
                board[posy,posx+pont1] = MaxChar(board[posy,posx+pont1],(4+player_id).ToString()[0]);
                if((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" )&& player_id == 1)  board[posy,posx+pont1] = (6+player_id).ToString()[0];
                if(real) print_piece(posy,posx+pont1);
                pont1--;
            }
            while(pont2 > 0)
            {
                board[posy,posx-pont2] = MaxChar(board[posy,posx-pont2],(4+player_id).ToString()[0]);
                if((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" )&& player_id == 1)  board[posy,posx-pont2] = (6+player_id).ToString()[0];
                if(real) print_piece(posy,posx-pont2);
                pont2--;
            }
        }

        p1v = true; p2v = true;
        quant = 1; pont1 = 0; pont2 = 0; move = 0;

        //5 diagonal right

        while((p1v || p2v))
        {
            if((move%2 == 0) && p1v)
            {
                pont1++;
                if(valid(posx+pont1,posy+pont1) && board[posy+pont1,posx+pont1] != 'b' && board[posy+pont1,posx+pont1] < '8' && (int.Parse(board[posy+pont1,posx+pont1].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont1--;
                    p1v = false;
                }
            }
            else if(p2v)
            {
                pont2++;
                if(valid(posx-pont2,posy-pont2) && board[posy-pont2,posx-pont2] != 'b' && board[posy-pont2,posx-pont2] < '8' && (int.Parse(board[posy-pont2,posx-pont2].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont2--;
                    p2v = false;
                }
            }
            move++;
        }

        if(quant >= 5)
        {
            me_5 = true;
            while(pont1 > 0)
            {
                board[posy+pont1,posx+pont1] = MaxChar(board[posy+pont1,posx+pont1],(4+player_id).ToString()[0]);
                if((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" )&& player_id == 1)  board[posy+pont1,posx+pont1] = (6+player_id).ToString()[0];
                if(real) print_piece(posy+pont1,posx+pont1);
                pont1--;
            }
            while(pont2 > 0)
            {
                board[posy-pont2,posx-pont2] = MaxChar(board[posy-pont2,posx-pont2],(4+player_id).ToString()[0]);
                if((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" )&& player_id == 1)  board[posy-pont2,posx-pont2] = (6+player_id).ToString()[0];
                if(real) print_piece(posy-pont2,posx-pont2);
                pont2--;
            }
        }

        p1v = true; p2v = true;
        quant = 1; pont1 = 0; pont2 = 0; move = 0;

        //5 diagonal left

        while((p1v || p2v))
        {
            if((move%2 == 0) && p1v)
            {
                pont1++;
                if(valid(posx+pont1,posy-pont1) && board[posy-pont1,posx+pont1] != 'b' && board[posy-pont1,posx+pont1] < '8' && (int.Parse(board[posy-pont1,posx+pont1].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont1--;
                    p1v = false;
                }
            }
            else if(p2v)
            {
                pont2++;
                if(valid(posx-pont2,posy+pont2) && board[posy+pont2,posx-pont2] != 'b' && board[posy+pont2,posx-pont2] < '8' && (int.Parse(board[posy+pont2,posx-pont2].ToString())+player_id) % 2 == 0)
                {
                    quant++;
                }
                else
                {
                    pont2--;
                    p2v = false;
                }
            }
            move++;
        }

        if(quant >= 5)
        {
            me_5 = true;
            while(pont1 > 0)
            {
                board[posy-pont1,posx+pont1] = MaxChar(board[posy-pont1,posx+pont1],(4+player_id).ToString()[0]);
                if((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" )&& player_id == 1)  board[posy-pont1,posx+pont1] = (6+player_id).ToString()[0];
                if(real) print_piece(posy-pont1,posx+pont1);
                pont1--;
            }
            while(pont2 > 0)
            {
                board[posy+pont2,posx-pont2] = MaxChar(board[posy+pont2,posx-pont2],(4+player_id).ToString()[0]);
                if((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" )&& player_id == 1)  board[posy+pont2,posx-pont2] = (6+player_id).ToString()[0];
                if(real) print_piece(posy+pont2,posx-pont2);
                pont2--;
            }
        }



        if(me_5)
        {
            board[posy,posx] = MaxChar(board[posy,posx],(4+player_id).ToString()[0]);
            if((player2.IA.tipo == "Fred" || player2.IA.tipo == "Jeff" )&& player_id == 1) board[posy,posx] = (6+player_id).ToString()[0];
            if(real) print_piece(posy,posx);
        }
        else if(me_3)
        {
            board[posy,posx] = MaxChar(board[posy,posx],(2+player_id).ToString()[0]);
            if(real) print_piece(posy,posx);
        }

        if(real) count();

    }


    bool t1 = false,t2 = false, t3 = false,t4 = false,tend = false;
    public void game_controller()
    {
        if(time < 10 && !t1)
        {
            t1 = true;
            board_gameobjects[3,3].SetActive(true);
            board_gameobjects[3,4].SetActive(true);
            board_gameobjects[3,5].SetActive(true);
            board_gameobjects[3,6].SetActive(true);

            board_gameobjects[4,3].SetActive(true);
            board_gameobjects[4,4].SetActive(true);
            board_gameobjects[4,5].SetActive(true);
            board_gameobjects[4,6].SetActive(true);

            board_gameobjects[5,3].SetActive(true);
            board_gameobjects[5,4].SetActive(true);
            board_gameobjects[5,5].SetActive(true);
            board_gameobjects[5,6].SetActive(true);

            board_gameobjects[6,3].SetActive(true);
            board_gameobjects[6,4].SetActive(true);
            board_gameobjects[6,5].SetActive(true);
            board_gameobjects[6,6].SetActive(true);

        }

        else if(time > 10 && !t2)
        {
            tamanho_tabuleiro+=2;
            t2 = true;
            board_gameobjects[2,2].SetActive(true);
            board_gameobjects[2,3].SetActive(true);
            board_gameobjects[2,4].SetActive(true);
            board_gameobjects[2,5].SetActive(true);
            board_gameobjects[2,6].SetActive(true);
            board_gameobjects[2,7].SetActive(true);

            board_gameobjects[7,2].SetActive(true);
            board_gameobjects[7,3].SetActive(true);
            board_gameobjects[7,4].SetActive(true);
            board_gameobjects[7,5].SetActive(true);
            board_gameobjects[7,6].SetActive(true);
            board_gameobjects[7,7].SetActive(true);

            board_gameobjects[3,2].SetActive(true);
            board_gameobjects[4,2].SetActive(true);
            board_gameobjects[5,2].SetActive(true);
            board_gameobjects[6,2].SetActive(true);

            board_gameobjects[3,7].SetActive(true);
            board_gameobjects[4,7].SetActive(true);
            board_gameobjects[5,7].SetActive(true);
            board_gameobjects[6,7].SetActive(true);
        }

        else if(time > 19 && !t3)
        {
            tamanho_tabuleiro+=2;
            t3 = true;
            board_gameobjects[1,1].SetActive(true);
            board_gameobjects[1,2].SetActive(true);
            board_gameobjects[1,3].SetActive(true);
            board_gameobjects[1,4].SetActive(true);
            board_gameobjects[1,5].SetActive(true);
            board_gameobjects[1,6].SetActive(true);
            board_gameobjects[1,7].SetActive(true);
            board_gameobjects[1,8].SetActive(true);

            board_gameobjects[8,1].SetActive(true);
            board_gameobjects[8,2].SetActive(true);
            board_gameobjects[8,3].SetActive(true);
            board_gameobjects[8,4].SetActive(true);
            board_gameobjects[8,5].SetActive(true);
            board_gameobjects[8,6].SetActive(true);
            board_gameobjects[8,7].SetActive(true);
            board_gameobjects[8,8].SetActive(true);

            board_gameobjects[2,1].SetActive(true);
            board_gameobjects[3,1].SetActive(true);
            board_gameobjects[4,1].SetActive(true);
            board_gameobjects[5,1].SetActive(true);
            board_gameobjects[6,1].SetActive(true);
            board_gameobjects[7,1].SetActive(true);

            board_gameobjects[2,8].SetActive(true);
            board_gameobjects[3,8].SetActive(true);
            board_gameobjects[4,8].SetActive(true);
            board_gameobjects[5,8].SetActive(true);
            board_gameobjects[6,8].SetActive(true);
            board_gameobjects[7,8].SetActive(true);
        }

        else if(time > 35 && !t4)
        {
            tamanho_tabuleiro+=2;
            t4 = true;

            board_gameobjects[0,0].SetActive(true);
            board_gameobjects[0,1].SetActive(true);
            board_gameobjects[0,2].SetActive(true);
            board_gameobjects[0,3].SetActive(true);
            board_gameobjects[0,4].SetActive(true);
            board_gameobjects[0,5].SetActive(true);
            board_gameobjects[0,6].SetActive(true);
            board_gameobjects[0,7].SetActive(true);
            board_gameobjects[0,8].SetActive(true);
            board_gameobjects[0,9].SetActive(true);

            board_gameobjects[9,0].SetActive(true);
            board_gameobjects[9,1].SetActive(true);
            board_gameobjects[9,2].SetActive(true);
            board_gameobjects[9,3].SetActive(true);
            board_gameobjects[9,4].SetActive(true);
            board_gameobjects[9,5].SetActive(true);
            board_gameobjects[9,6].SetActive(true);
            board_gameobjects[9,7].SetActive(true);
            board_gameobjects[9,8].SetActive(true);
            board_gameobjects[9,9].SetActive(true);

            board_gameobjects[1,0].SetActive(true);
            board_gameobjects[2,0].SetActive(true);
            board_gameobjects[3,0].SetActive(true);
            board_gameobjects[4,0].SetActive(true);
            board_gameobjects[5,0].SetActive(true);
            board_gameobjects[6,0].SetActive(true);
            board_gameobjects[7,0].SetActive(true);
            board_gameobjects[8,0].SetActive(true);

            board_gameobjects[1,9].SetActive(true);
            board_gameobjects[2,9].SetActive(true);
            board_gameobjects[3,9].SetActive(true);
            board_gameobjects[4,9].SetActive(true);
            board_gameobjects[5,9].SetActive(true);
            board_gameobjects[6,9].SetActive(true);
            board_gameobjects[7,9].SetActive(true);
            board_gameobjects[8,9].SetActive(true);
        }

        else if(time > 90 && !tend)
        {
            tend = true;
            count();
            player1.Game_Over();
            player2.Game_Over();
            if(player1.points>player2.points)
            {
                estado.text = "Player wins!";
                string tipo = player2.IA.tipo;
                if(tipo == "random") walker.vitoriaRandom = true;
                else if(tipo == "confused") walker.vitoriaConfused = true;
                else if(tipo == "greedy") walker.vitoriaGreedy = true;
                else if(tipo == "hateful") walker.vitoriaHateful = true;
                else if(tipo == "slow") walker.vitoriaSlow = true;
                else if(tipo == "local") walker.vitoriaLocal = true;
                else if(tipo == "normal") walker.vitoriaNormal = true;
                else if(tipo == "bud") walker.vitoriaBud = true;
                else if(tipo == "cheap") walker.vitoriaCheap = true;
                else if(tipo == "quick") walker.vitoriaQuick = true;
                else if(tipo == "Fred") walker.vitoriaFred = true;
                else if(tipo == "Jeff") walker.vitoriaJeff = true;
                else if(tipo == "unoptimized") walker.vitoriaUnoptimized = true;
            }
            else if(player1.points>player2.points)
            {
                estado.text = "Draw?";
            }
            else estado.text = "AI wins!";
            end_game.SetActive(true);
        }
    }

    public TextMeshProUGUI tutorial_text;

    public void tutorial_controller()
    {
        count();
        if(time > 10 && !t1)
        {
            t1 = true;
            board_gameobjects[3,3].SetActive(true);
            board_gameobjects[3,4].SetActive(true);
            board_gameobjects[3,5].SetActive(true);
            board_gameobjects[3,6].SetActive(true);

            board_gameobjects[4,3].SetActive(true);
            board_gameobjects[4,4].SetActive(true);
            board_gameobjects[4,5].SetActive(true);
            board_gameobjects[4,6].SetActive(true);

            board_gameobjects[5,3].SetActive(true);
            board_gameobjects[5,4].SetActive(true);
            board_gameobjects[5,5].SetActive(true);
            board_gameobjects[5,6].SetActive(true);

            board_gameobjects[6,3].SetActive(true);
            board_gameobjects[6,4].SetActive(true);
            board_gameobjects[6,5].SetActive(true);
            board_gameobjects[6,6].SetActive(true);
            tutorial_text.text = "Left click to pay 3 sun and put seeds in the pot, combine 3 seeds in a row to make buds! try it out!";
        }
        else if(player1.amount_buds >= 3 && !t2)
        {
            t2 = true;
            tutorial_text.text = "Each Bud increase your gains in suns per second, but also are worth one point in the final score, we need lots of points to win the red flowers! Try to make 5 pieces in a row to create flowers!";
            board_gameobjects[2,2].SetActive(true);
            board_gameobjects[2,3].SetActive(true);
            board_gameobjects[2,4].SetActive(true);
            board_gameobjects[2,5].SetActive(true);
            board_gameobjects[2,6].SetActive(true);
            board_gameobjects[2,7].SetActive(true);

            board_gameobjects[7,2].SetActive(true);
            board_gameobjects[7,3].SetActive(true);
            board_gameobjects[7,4].SetActive(true);
            board_gameobjects[7,5].SetActive(true);
            board_gameobjects[7,6].SetActive(true);
            board_gameobjects[7,7].SetActive(true);

            board_gameobjects[3,2].SetActive(true);
            board_gameobjects[4,2].SetActive(true);
            board_gameobjects[5,2].SetActive(true);
            board_gameobjects[6,2].SetActive(true);

            board_gameobjects[3,7].SetActive(true);
            board_gameobjects[4,7].SetActive(true);
            board_gameobjects[5,7].SetActive(true);
            board_gameobjects[6,7].SetActive(true);
        }
        else if(player1.amount_flowers >= 5 && !t3)
        {
            t3 = true;
            tutorial_text.text = "Flowers increase your income even more! And are worth even more points! Now try out biggest combination possible, unite 9 or more pieces in the format of a + (plus) or a x (multiplication sign) to create Big Flowers!";
            board_gameobjects[1,1].SetActive(true);
            board_gameobjects[1,2].SetActive(true);
            board_gameobjects[1,3].SetActive(true);
            board_gameobjects[1,4].SetActive(true);
            board_gameobjects[1,5].SetActive(true);
            board_gameobjects[1,6].SetActive(true);
            board_gameobjects[1,7].SetActive(true);
            board_gameobjects[1,8].SetActive(true);

            board_gameobjects[8,1].SetActive(true);
            board_gameobjects[8,2].SetActive(true);
            board_gameobjects[8,3].SetActive(true);
            board_gameobjects[8,4].SetActive(true);
            board_gameobjects[8,5].SetActive(true);
            board_gameobjects[8,6].SetActive(true);
            board_gameobjects[8,7].SetActive(true);
            board_gameobjects[8,8].SetActive(true);

            board_gameobjects[2,1].SetActive(true);
            board_gameobjects[3,1].SetActive(true);
            board_gameobjects[4,1].SetActive(true);
            board_gameobjects[5,1].SetActive(true);
            board_gameobjects[6,1].SetActive(true);
            board_gameobjects[7,1].SetActive(true);

            board_gameobjects[2,8].SetActive(true);
            board_gameobjects[3,8].SetActive(true);
            board_gameobjects[4,8].SetActive(true);
            board_gameobjects[5,8].SetActive(true);
            board_gameobjects[6,8].SetActive(true);
            board_gameobjects[7,8].SetActive(true);
        }
        else if(player1.amount_big_flowers >= 9 && !t4)
        {
            tutorial_text.text = "Awesome Brother! Take your time here, but remenber that real matches have a duration of 90 seconds! When you're ready, go to the Menu and face those invaders! I believe in you!";
            t4 = true;
            board_gameobjects[0,0].SetActive(true);
            board_gameobjects[0,1].SetActive(true);
            board_gameobjects[0,2].SetActive(true);
            board_gameobjects[0,3].SetActive(true);
            board_gameobjects[0,4].SetActive(true);
            board_gameobjects[0,5].SetActive(true);
            board_gameobjects[0,6].SetActive(true);
            board_gameobjects[0,7].SetActive(true);
            board_gameobjects[0,8].SetActive(true);
            board_gameobjects[0,9].SetActive(true);

            board_gameobjects[9,0].SetActive(true);
            board_gameobjects[9,1].SetActive(true);
            board_gameobjects[9,2].SetActive(true);
            board_gameobjects[9,3].SetActive(true);
            board_gameobjects[9,4].SetActive(true);
            board_gameobjects[9,5].SetActive(true);
            board_gameobjects[9,6].SetActive(true);
            board_gameobjects[9,7].SetActive(true);
            board_gameobjects[9,8].SetActive(true);
            board_gameobjects[9,9].SetActive(true);

            board_gameobjects[1,0].SetActive(true);
            board_gameobjects[2,0].SetActive(true);
            board_gameobjects[3,0].SetActive(true);
            board_gameobjects[4,0].SetActive(true);
            board_gameobjects[5,0].SetActive(true);
            board_gameobjects[6,0].SetActive(true);
            board_gameobjects[7,0].SetActive(true);
            board_gameobjects[8,0].SetActive(true);

            board_gameobjects[1,9].SetActive(true);
            board_gameobjects[2,9].SetActive(true);
            board_gameobjects[3,9].SetActive(true);
            board_gameobjects[4,9].SetActive(true);
            board_gameobjects[5,9].SetActive(true);
            board_gameobjects[6,9].SetActive(true);
            board_gameobjects[7,9].SetActive(true);
            board_gameobjects[8,9].SetActive(true);

        }
    }

    public void Menu()
    {
        walker.go_menu();
    }
    public void Again()
    {
        walker.go_play();
    }

    public GameObject end_visible;
    public Image olhinho;
    public Sprite olhinho_aberto;
    public Sprite olhinho_fechado;
    public bool olho_posicao = false;

    public void muda_olho()
    {
        if(olho_posicao) olhinho.sprite = olhinho_aberto;
        else olhinho.sprite = olhinho_fechado;
        end_visible.SetActive(olho_posicao);
        olho_posicao = !olho_posicao;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(!tend)
        {
            if(tutorial) tutorial_controller();
            else game_controller();
            time += Time.fixedDeltaTime;
            time_txt.text = Mathf.FloorToInt(time).ToString("F2") + " seconds";
        }
        
    }
}
