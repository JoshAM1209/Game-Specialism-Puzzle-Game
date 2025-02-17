using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int boardWidth;
    public int boardHeight;
    public GameObject tileBase;
    private BackTile[,] boardTiles;
    
    // Start is called before the first frame update
    void Start()
    {
        boardTiles = new BackTile[boardWidth, boardHeight];
        boardSetUp();
    }

    private void boardSetUp()
    {
        for (int i = 0; i < boardWidth; i++)
        {
            for (int j = 0; j < boardHeight; j++)
            {
                Vector2 tilePos = new Vector2(i, j);
                Instantiate(tileBase, tilePos, Quaternion.identity);
            }
        }
    }
}
