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
    public int pieceColour; //0 = blue, 1 = green, 2 = purple, 3 = red, 4 = yellow

    public int thisPiecePosX;
    public int thisPiecePosY;

    public bool markedToPop = false;

    public List<GameObject> piecesToPop = new List<GameObject>();
    public int popCount = 0;

    public int leftCheck;
    public int rightCheck;
    public int upCheck;
    public int downCheck;

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
        PopCheck(thisPiece);
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
                if (markedToPop == false)
                {
                    PopCheck(thisPiece);
                }
                if (popCount >= 4)
                {
                    RunPop();
                }
                else
                {
                    foreach (GameObject piece in piecesToPop)
                    {
                        LockedInPiece pieceChecking = piece.GetComponent<LockedInPiece>();
                        pieceChecking.markedToPop = false;
                    }
                    piecesToPop.Clear();
                    popCount = 0;
                }
            }
        }
    }

    void PopCheck(GameObject checkPiece)
    {
        LockedInPiece pieceChecking = checkPiece.GetComponent<LockedInPiece>();
        GameObject nextCheck;
        if (pieceChecking.leftCheck >= 0)
        {
            if (gridManager.grid[pieceChecking.leftCheck, pieceChecking.thisPiecePosY] != null)
            {
                if (gridManager.grid[pieceChecking.leftCheck, pieceChecking.thisPiecePosY].CompareTag(this.gameObject.tag))
                {
                    if (pieceChecking.markedToPop != true)
                    {
                        pieceChecking.markedToPop = true;
                        AddToPop(pieceChecking);
                    }
                    nextCheck = gridManager.grid[pieceChecking.leftCheck, pieceChecking.thisPiecePosY];
                    PopCheck(nextCheck);
                }
            }
        }
        if (pieceChecking.rightCheck <= 6)
        {
            if (gridManager.grid[pieceChecking.rightCheck, pieceChecking.thisPiecePosY] != null)
            {
                if (gridManager.grid[pieceChecking.rightCheck, pieceChecking.thisPiecePosY].CompareTag(this.gameObject.tag))
                {
                    if (pieceChecking.markedToPop != true)
                    {
                        pieceChecking.markedToPop = true;
                        AddToPop(pieceChecking);
                    }
                    nextCheck = gridManager.grid[pieceChecking.leftCheck, pieceChecking.thisPiecePosY];
                    PopCheck(nextCheck);
                }
            }
        }
        if (pieceChecking.upCheck <= 13)
        {
            if (gridManager.grid[pieceChecking.thisPiecePosX, pieceChecking.upCheck] != null)
            {
                if (gridManager.grid[pieceChecking.thisPiecePosX, pieceChecking.upCheck].CompareTag(this.gameObject.tag))
                {
                    if (pieceChecking.markedToPop != true)
                    {
                        pieceChecking.markedToPop = true;
                        AddToPop(pieceChecking);
                    }
                    nextCheck = gridManager.grid[pieceChecking.leftCheck, pieceChecking.thisPiecePosY];
                    PopCheck(nextCheck);
                }
            }
        }
        if (pieceChecking.downCheck >= 0)
        {
            if (gridManager.grid[pieceChecking.thisPiecePosX, pieceChecking.downCheck] != null)
            {
                if (gridManager.grid[pieceChecking.thisPiecePosX, pieceChecking.downCheck].CompareTag(this.gameObject.tag))
                {
                    if (pieceChecking.markedToPop != true)
                    {
                        pieceChecking.markedToPop = true;
                        AddToPop(pieceChecking);
                    }
                    nextCheck = gridManager.grid[pieceChecking.leftCheck, pieceChecking.thisPiecePosY];
                    PopCheck(nextCheck);
                }
            }
        }
    }

    void AddToPop(LockedInPiece piecePop)
    {
        popCount += 1;
        piecesToPop.Add(piecePop.gameObject);
    }

    void RunPop()
    {
        foreach (GameObject piece in piecesToPop)
        {
            LockedInPiece pieceChecking = piece.GetComponent<LockedInPiece>();
            int popPosX = pieceChecking.thisPiecePosX;
            int popPosY = pieceChecking.thisPiecePosY;
            gridManager.grid[popPosX, popPosY] = null;
            Destroy(piece.gameObject);
        }
        piecesToPop.Clear();
        popCount = 0;
    }
}
