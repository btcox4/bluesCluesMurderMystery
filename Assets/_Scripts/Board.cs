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

    private void Awake()
    {
        rows = GetComponentsInChildren<Row>();
    }

    private void Start()
    {
        LoadData();
        sol = "phone";
    }

    private void LoadData()
    {
        TextAsset textFile = Resources.Load("offical_wordle_all") as TextAsset;
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
        }
        else if (colIndex >= curRow.tiles.Length)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // SubmitRow(curRow);
            }
        }
        else
        {
            for(int i = 0; i < SUPPORTED_KEYS.Length; i++)
            {
                if (Input.GetKeyDown(SUPPORTED_KEYS[i]))
                {
                    curRow.tiles[colIndex].SetLetter((char)SUPPORTED_KEYS[i]);
                    colIndex++;
                    break;
                }
            }
        }
    }

   

}
