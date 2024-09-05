using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class MinMax_Max : AI
{
    
    // Start is called before the first frame update
    List<char[,]> dfs_boards;
    List<Tuple<int,int,int,int>> pointboard; //pontos,indice,i,j
    public int profundidade = 3;
    void Start()
    {
        dfs_boards = new List<char[,]>();
        pointboard = new List<Tuple<int, int, int, int>>();
        board_controller = GameObject.FindWithTag("GameController").GetComponent<Board>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<playercontroller>();
    }
    
    public override void escolha()
    {
        dfs_boards.Clear();
        pointboard.Clear();
        char[,] tabuleiro = new char[10,10];
        for(int i=0;i<10;i++)
        {
            for(int j=0;j<10;j++)
            {
                tabuleiro[i,j] = board_controller.board[i,j];
            }
        }

        dfs_boards.Add(tabuleiro);
        dfs(0,0,0,0);


        pointboard.Sort((x, y) => y.Item1.CompareTo(x.Item1));
        int xx = pointboard[0].Item3;
        int yy = pointboard[0].Item4;
        Debug.Log(pointboard[0].Item1);
        if(pointboard[0].Item1 >-1000000)player2.buy_seeds(xx,yy,1);
    }


    // Método para embaralhar os elementos do array
    void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = UnityEngine.Random.Range(0, n);
            n--;
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    public int dfs(int ind,int D,int x, int y)
    {
        int max;
        if(D%2 != 1) max = -1000000;
        else max = 10000000;
        char[,] tabuleiro = new char[10,10];
        int betterx = x;
        int bettery = y;

        for(int i=0;i<10;i++)
        {
            for(int j=0;j<10;j++)
            {
                tabuleiro[i,j] = dfs_boards[ind][i,j];
            }
        }

        if(D != profundidade)
        {
            int[] arrayx = {0,1,2,3,4,5,6,7,8,9};
            int[] arrayy = {0,1,2,3,4,5,6,7,8,9};
            if(tipo != "local") Shuffle(arrayx);
            if(tipo != "local") Shuffle(arrayy);
            for(int i=0;i<10;i++)
            {
                for(int j=0;j<10;j++)
                {
                    if(valid(arrayy[j],arrayx[i],ref tabuleiro))
                    {
                        if(D%2 != 1) // se for a vez da AI
                        {
                            tabuleiro[arrayx[i],arrayy[j]] = '1'; // coloca peça da AI
                            board_controller.merge_check(arrayy[j],arrayx[i],1,ref tabuleiro,false); //checa se formou alguma sequência e atualiza o tabuleiro
                            dfs_boards.Add(tabuleiro); // adiciona o tabuleiro a lista de tabuleiros
                            int tmp = dfs(dfs_boards.Count-1,D+1,arrayy[j],arrayx[i]);
                            if(max < tmp)
                            {
                                max = tmp;
                                betterx = arrayy[j];
                                bettery = arrayx[i];
                            }
                        }
                        else //vez do jogador
                        {
                            tabuleiro[arrayx[i],arrayy[j]] = '0'; //peça do jogador 
                            board_controller.merge_check(arrayy[j],arrayx[i],0,ref tabuleiro,false); //checagem de sequencia
                            dfs_boards.Add(tabuleiro); //add na lista
                            int tmp = dfs(dfs_boards.Count-1,D+1,arrayy[j],arrayx[i]);
                            if(max > tmp)
                            {
                                max = tmp;
                                betterx = arrayy[j];
                                bettery = arrayx[i];
                            }
                        }
                        
                        for(int k=0;k<10;k++)
                        {
                            for(int l=0;l<10;l++)
                            {
                                tabuleiro[k,l] = dfs_boards[ind][k,l];
                            }
                        }
                    }
                }
            }
        }
        else{
            return count(ref tabuleiro);
        }

        if(D == 0)
        {
            pointboard.Add(new Tuple<int, int, int, int>(max, ind, betterx, bettery));
        }
        return max;
    }



    public int count(ref char[,] board)
    {
        int resultado = 0;
        for(int i=0;i<10;i++)
        {
            for(int j=0;j<10;j++)
            {
                char atual = board[i,j];

                if(atual == '2')
                {
                    if(tipo == "confused") resultado-= UnityEngine.Random.Range(0,3);
                    else if(tipo == "bud")
                    {
                        resultado--;
                    }
                    else if(tipo == "hateful")
                    {
                        resultado-=20;
                    }
                    else resultado--;
                }
                else if(atual == '3')
                {
                    if(tipo == "confused") resultado+= UnityEngine.Random.Range(0,3);
                    else if(tipo == "bud")
                    {
                        if(board_controller.time < 50) resultado += 2;
                        else resultado++;
                    }
                    else resultado++;
                }
                else if(atual == '4')
                {
                    if(tipo == "confused") resultado-= UnityEngine.Random.Range(0,6);
                    else if(tipo == "bud")
                    {
                        resultado-=2;
                    }
                    else if(tipo == "hateful")
                    {
                        resultado-=40;
                    }
                    else resultado -= 2;
                }
                else if(atual == '5')
                {
                    if(tipo == "confused") resultado+= UnityEngine.Random.Range(0,6);
                    else if(tipo == "bud")
                    {
                        if(board_controller.time < 50) resultado += 1;
                        else resultado+=2;
                    }
                    else if(tipo == "hateful")
                    {
                        resultado+=3;
                    }
                    else resultado += 2;
                }
                else if(atual == '6')
                {
                    if(tipo == "confused") resultado-= UnityEngine.Random.Range(0,10);
                    else if(tipo == "hateful")
                    {
                        resultado-=60;
                    }
                    else resultado -= 3;
                }
                else if(atual == '7')
                {
                    if(tipo == "confused") resultado-= UnityEngine.Random.Range(0,+10);
                    else if(tipo == "hateful")
                    {
                        resultado+=4;
                    }
                    else resultado += 3;
                }
            }
        }
        return resultado;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
