using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockedInPiece : MonoBehaviour
{
    public GridManager gridManager;
    public PopCounter popCounter;
    public int pieceNo;
    public MainPiecePos mainPiecePos;
    public SecondPiecePos secondPiecePos;
    public GameObject thisPiece;

    private int thisPiecePosX;
    private int thisPiecePosY;

    public bool markedToPop = false;

    private int leftCheck;
    private int rightCheck;
    private int upCheck;
    private int downCheck;
    
    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        popCounter = FindObjectOfType<PopCounter>();
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
        leftCheck = thisPiecePosX - 1;
        rightCheck = thisPiecePosX + 1;
        upCheck = thisPiecePosY + 1;
        downCheck = thisPiecePosY - 1;

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
                leftCheck = thisPiecePosX - 1;
                rightCheck = thisPiecePosX + 1;
                upCheck = thisPiecePosY + 1;
                downCheck = thisPiecePosY - 1;
            }
        }
        PopCheck();
    }

    void PopCheck()
    {
        if (leftCheck >= 0)
        {
            if (gridManager.grid[leftCheck, thisPiecePosY] != null)
            {
                if (gridManager.grid[leftCheck, thisPiecePosY].tag == thisPiece.tag)
                {
                    markedToPop = true;
                    AddToPop();
                }
            }
        }
        if (rightCheck <= 7)
        {
            if (gridManager.grid[rightCheck, thisPiecePosY] != null)
            {
                if (gridManager.grid[rightCheck, thisPiecePosY].tag == thisPiece.tag)
                {
                    markedToPop = true;
                    AddToPop();
                }
            }
        }
        if (upCheck <= 13)
        {
            if (gridManager.grid[thisPiecePosX, upCheck] != null)
            {
                if (gridManager.grid[thisPiecePosX, upCheck].tag == thisPiece.tag)
                {
                    markedToPop = true;
                    AddToPop();
                }
            }
        }
        if (downCheck >= 0)
        {
            if (gridManager.grid[thisPiecePosX, downCheck] != null)
            {
                if (gridManager.grid[thisPiecePosX, downCheck].tag == thisPiece.tag)
                {
                    markedToPop = true;
                    AddToPop();
                }
            }
        }
    }

    void AddToPop()
    {
        popCounter.popCount += 1;
        //popCounter.piecesToPop.Add(GameObject.this);
    }
}
