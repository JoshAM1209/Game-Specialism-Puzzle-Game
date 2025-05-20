using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//this script is initially disabled but will become active once a piece has locked in to place
public class LockedInPiece : MonoBehaviour
{
    public GridManager gridManager;
    public PopCounter popCounter;
    public int pieceNo;
    public MainPiecePos mainPiecePos;
    public SecondPiecePos secondPiecePos;
    public GameObject thisPiece;
    public int pieceColour; //0 = blue, 1 = green, 2 = purple, 3 = red, 4 = yellow (this is currently unused and exists as a backup to replace tags)

    public int thisPiecePosX;
    public int thisPiecePosY;
    //stores the index for this piece's position in the grid

    public bool markedToPop = false;
    //used to check if a piece has already been included in a match to prevent pieces from getting checked multiple times

    public List<GameObject> piecesToPop = new List<GameObject>();
    public int popCount = 0;
    //all objects stored in the list are supposed to pop after a match is made which should occur when pop count is at least 4

    public int leftCheck;
    public int rightCheck;
    public int upCheck;
    public int downCheck;
    //these are used to check what piece is in the adjacent positions

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

    void AddToPop()
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
                    if (nextCheck.GetComponent<LockedInPiece>().markedToPop == false)
                    {
                        PopCheck(nextCheck);
                    }
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
                    nextCheck = gridManager.grid[pieceChecking.rightCheck, pieceChecking.thisPiecePosY];
                    if (nextCheck.GetComponent<LockedInPiece>().markedToPop == false)
                    {
                        PopCheck(nextCheck);
                    }
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
                    nextCheck = gridManager.grid[pieceChecking.upCheck, pieceChecking.thisPiecePosY];
                    if (nextCheck.GetComponent<LockedInPiece>().markedToPop == false)
                    {
                        PopCheck(nextCheck);
                    }
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
                    nextCheck = gridManager.grid[pieceChecking.downCheck, pieceChecking.thisPiecePosY];
                    if (nextCheck.GetComponent<LockedInPiece>().markedToPop == false)
                    {
                        PopCheck(nextCheck);
                    }
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
