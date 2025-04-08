using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockedInPiece : MonoBehaviour
{
    public GridManager gridManager;
    public int pieceNo;
    public MainPiecePos mainPiecePos;
    public SecondPiecePos secondPiecePos;
    public GameObject thisPiece;

    private int thisPiecePosX;
    private int thisPiecePosY;
    
    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (pieceNo == 1)
        {
            thisPiecePosX = mainPiecePos.piecePosX;
            thisPiecePosY = mainPiecePos.piecePosY;
        }
        else if (pieceNo == 2)
        {
            thisPiecePosX = secondPiecePos.piecePosX;
            thisPiecePosY = secondPiecePos.piecePosY;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (thisPiecePosY > 0)
        {
            if (gridManager.grid[thisPiecePosX, thisPiecePosY - 1] == null)
            {
                transform.position += new Vector3(0, -1, 0);
                gridManager.grid[thisPiecePosX, thisPiecePosY] = null;
                thisPiecePosY -= 1;
                gridManager.grid[thisPiecePosX, thisPiecePosY] = thisPiece; 
            }
        }
    }
}
