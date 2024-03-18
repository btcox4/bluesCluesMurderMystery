using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{   

    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[] {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F,
        KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R,
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X,
        KeyCode.Y, KeyCode.Z,
    };

    private Row[] rows;

    private string sol;
    private string[] valid;
    private int rowIndex;
    private int colIndex;

    public Tile.State emptyState;
    public Tile.State occupiedState;
    public Tile.State correctState;
    public Tile.State wrongSpotState;
    public Tile.State incorrectState;

    private void Awake()
    {
        rows = GetComponentsInChildren<Row>();
    }

    private void Start()
    {
        LoadData();
        sol = "phone";
        rowIndex = 0;
        colIndex = 0;
    }

    private void LoadData()
    {
        TextAsset textFile = Resources.Load("official_wordle_all") as TextAsset;
        valid = textFile.text.Split('\n');
    }

    // Update is called once per frame
    void Update()
    {
        Row curRow = rows[rowIndex];
        
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            colIndex = Mathf.Max(colIndex - 1, 0);
            curRow.tiles[colIndex].SetLetter('\0');
            curRow.tiles[colIndex].SetState(emptyState);
        }
        else if (colIndex >= curRow.tiles.Length)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SubmitRow(curRow);
            }
        }
        else
        {
            for(int i = 0; i < SUPPORTED_KEYS.Length; i++)
            {
                if (Input.GetKeyDown(SUPPORTED_KEYS[i]))
                {
                    curRow.tiles[colIndex].SetLetter((char)SUPPORTED_KEYS[i]);
                    curRow.tiles[colIndex].SetState(occupiedState);
                    colIndex++;
                    break;
                }
            }
        }
    }

    private void SubmitRow(Row row)
    {
        if(!isValid(row.word))
        {
            // add screenshake?
            return;
        }
        
        string remaining = sol;
        
        for (int i = 0; i < row.tiles.Length; i++)
        {
            Tile tile = row.tiles[i];
            
            if (tile.letter == sol[i])
            {
                // correct
                tile.SetState(correctState);

                remaining = remaining.Remove(i, 1);
                remaining = remaining.Insert(i, " ");
            }
            else if (!sol.Contains(tile.letter))
            {
                // wrong spot
                tile.SetState(incorrectState);
            }
        }

        for(int j = 0; j < row.tiles.Length; j++)
            {
                Tile tile1 = row.tiles[j];

                if(tile1.state != correctState && tile1.state != incorrectState)
                {
                    if (remaining.Contains(tile1.letter))
                    {
                        tile1.SetState(wrongSpotState);
                        int index = remaining.IndexOf(tile1.letter);
                        remaining = remaining.Remove(index, 1);
                        remaining = remaining.Insert(index, " ");
                    }
                    else
                    {
                        tile1.SetState(incorrectState);
                    }
                }
            }

        if (hasWon(row))
        {
            enabled = false;
        }

        rowIndex++;
        colIndex = 0;

        // out of rows
        if (rowIndex >= rows.Length)
        {
           if (!hasWon(row))
           {
                Advance();
           }
        }
    }
   
    private bool isValid(string word)
    {
        for (int i = 0; i < valid.Length; i++)
        {
            if (valid[i] == word){
                return true;
            }
        }
        return false;
    }

    private bool hasWon(Row row)
    {
        for (int i = 0; i < row.tiles.Length; i++)
        {
            if (row.tiles[i].state != correctState)
            {
                return false;
            }
        }
        return true;
    }

    private void Advance()
    {
        // clears first row
        for (int col = 0; col < rows[0].tiles.Length; col++)
        {
            rows[0].tiles[col].SetLetter('\0');
            rows[0].tiles[col].SetState(emptyState);
        }

        // loops through rest of rows and moves them up one
        for (int row = 1; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row].tiles.Length; col++)
            {
                rows[row - 1].tiles[col].SetLetter(rows[row].tiles[col].letter);
                rows[row - 1].tiles[col].SetState(rows[row].tiles[col].state);
            }
        }

        // clears last row
            for (int col = 0; col < rows[6].tiles.Length; col++)
            {
                rows[6].tiles[col].SetLetter('\0');
                rows[6].tiles[col].SetState(emptyState);
            }
        
        colIndex = 0;
        rowIndex--;
    }

}
