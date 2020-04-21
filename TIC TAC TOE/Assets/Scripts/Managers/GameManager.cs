using UnityEngine;

public class GameManager : Singleton<GameManager> 
{
    private int size;
    private int [,] board;
    private int winner;

    /// <summary>
    /// Tipo de algoritmo a usar, Minimax Simple, Minimax con Suspensión o Minimax con Poda Alfa-Beta.
    /// </summary>
    private int algorithmType;

    /// <summary>
    /// Propiedad para asignar y retornar el tipo de Algoritmo a usar
    /// </summary>
    /// <value>Tipo de algoritmo a utilizar.</value>
    public int AlgorithmType
    {
        get { return algorithmType; }
        set { algorithmType = value; }
    }

	public void StartGame()
    {
        algorithmType = 0;
        size = 3;
        board = new int[size, size];
        winner = -1;

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                board[i, j] = -1;
    }

    public void PlayerPlay(string box)
    {
        int f = 0, c = 0;

        switch(box)
        {
            case "0": f = 0; c = 0; break;
            case "1": f = 0; c = 1; break;
            case "2": f = 0; c = 2; break;
            case "3": f = 1; c = 0; break;
            case "4": f = 1; c = 1; break;
            case "5": f = 1; c = 2; break;
            case "6": f = 2; c = 0; break;
            case "7": f = 2; c = 1; break;
            case "8": f = 2; c = 2; break;
        }

        board[f, c] = 0;
        HudManager.Instance.AssignBox(int.Parse(box), "O");
        winner = WinnerGame();
        CPU_Play();

        if (BoardComplete())
            HudManager.Instance.WinnerPanel();

        if (winner == -1)
            return;
        
        HudManager.Instance.UpdateScore(winner);
    }

    int WinnerGame()
    {
        if (board[0, 0] != -1 && board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2])
            return board[0, 0];
        if (board[0, 2] != -1 && board[0, 2] == board[1, 1] && board[0, 2] == board[2, 0])
            return board[0, 2];
        for (int i = 0; i < size; i++)
        {
            if (board[i, 0] != -1 && board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2])
                return board[i, 0];
            if (board[0, i] != -1 && board[0, i] == board[1, i] && board[0, i] == board[2, i])
                return board[0, i];
        }
        return -1;
    }

    bool BoardComplete()
    {
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                if(board[i,j] == -1)
                    return false;

        return true;
    }

    bool FinPartida()
    {
        return BoardComplete() || WinnerGame() != -1;
    }

    void CPU_Play()
    {
        if(!FinPartida())
        {
            int f = 0, c = 0;
            int value = int.MinValue;
            int aux;
            int box = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] == -1)
                    {
                        board[i, j] = 1;
                        aux = Min();
                        if(aux > value)
                        {
                            value = aux;
                            f = i;
                            c = j;
                        }
                        board[i, j] = -1;
                    }
                }
            }
            board[f, c] = 1;

            if (f == 0 && c == 0) box = 0;
            if (f == 0 && c == 1) box = 1;
            if (f == 0 && c == 2) box = 2;
            if (f == 1 && c == 0) box = 3;
            if (f == 1 && c == 1) box = 4;
            if (f == 1 && c == 2) box = 5;
            if (f == 2 && c == 0) box = 6;
            if (f == 2 && c == 1) box = 7;
            if (f == 2 && c == 2) box = 8;

            HudManager.Instance.AssignBox(box, "X");
        }
        winner = WinnerGame();
    }

    int Max()
    {
        if(FinPartida())
        {
            if (WinnerGame() != -1) 
                return -1;
            else 
                return 0;
        }

        int value = int.MinValue;
        int aux;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if(board[i,j] == -1)
                {
                    board[i, j] = 1;
                    aux = Min();
                    if (aux > value)
                        value = aux;
                    
                    board[i, j] = -1;
                }
            }
        }
        return value;
    }

    int Min()
    {
        if (FinPartida())
        {
            if (WinnerGame() != -1)
                return 1;
            else
                return 0;
        }

        int value = int.MaxValue;
        int aux;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] == -1)
                {
                    board[i, j] = 0;
                    aux = Max();
                    if (aux < value)
                        value = aux;

                    board[i, j] = -1;
                }
            }
        }
        return value;
    }

    /// <summary>
    /// Salir del juego
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}