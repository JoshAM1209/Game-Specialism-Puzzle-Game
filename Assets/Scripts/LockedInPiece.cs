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
    public int pieceColour; //0 = blue, 1 = green, 2 = purple, 3 = red, 4 = yellow (currently unused, only implemented as a backup to replace using tags if needed)

    public int thisPiecePosX;
    public int thisPiecePosY;
    //this is used to store the index for the pieces position in the grid

    public bool markedToPop = false;
    //this is changed to true when the script is checking for a match if the current piece is adjacent to one of the same colour

    public List<GameObject> piecesToPop = new List<GameObject>();
    public int popCount = 0;
    //this defines the list that will store all pieces that will be popped when a match is made, the counter keeps track of how many matching pieces are next to each other and will trigger pop when it is at least 4

    public int leftCheck;
    public int rightCheck;
    public int upCheck;
    public int downCheck;
    //these variable will store the positions adjacent to the current piece and are used when checking for a match

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
        //once the piece has been placed this will check if it is the main or seconday piece in the pair and grab the position values for the corresponding piece from the parent object and assign them to the individual piece
        leftCheck = thisPiecePosX - 1;
        rightCheck = thisPiecePosX + 1;
        upCheck = thisPiecePosY + 1;
        downCheck = thisPiecePosY - 1;
        //uses the pieces current posisition to work out what the adjacent positions are
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
                //this code checks if the space below the current piece is empty and if it is then the current piece will move down a space, this repeats until it can no longer move down again
                if (markedToPop == false)
                {
                    PopCheck(thisPiece); //if the current piece is not already marks to pop then it will run a pop check
                }
                if (popCount >= 4)
                {
                    RunPop(); //this is intended to run the code pop the pieces if popcount is greater than or equal to 4
                }
                else
                {
                    foreach (GameObject piece in piecesToPop)
                    {
                        LockedInPiece pieceChecking = piece.GetComponent<LockedInPiece>();
                        pieceChecking.markedToPop = false;
                        //if the prior code is ran and popcount still does not equel 4 then this is intended to uncheck all pieces that were marked to be popped
                    }
                    piecesToPop.Clear();
                    popCount = 0;
                    //this is intended to remove all pieces from the list to pop them if there wasn't a match of at least 4 and also reset popcount back to 0
                }
            }
        }
    }

    void PopCheck(GameObject checkPiece)
    {
        LockedInPiece pieceChecking = checkPiece.GetComponent<LockedInPiece>();
        GameObject nextCheck;
        if (pieceChecking.leftCheck >= 0) //This checks the piece to the left of the current piece (assuming the space is occupied and the current piece is not already at the edge of the gird, it checks if both pieces share a tag and if they do it is intended to mark the current piece to pop, add it to the list, increase pop count and run this same check using that piece as the "current piece"
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
        if (pieceChecking.rightCheck <= 6) //the next lines of code run the same check as the previous one but for all other directions
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
                    nextCheck = gridManager.grid[pieceChecking.upCheck, pieceChecking.thisPiecePosY];
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
                    nextCheck = gridManager.grid[pieceChecking.downCheck, pieceChecking.thisPiecePosY];
                    PopCheck(nextCheck);
                }
            }
        }
    }

    void AddToPop(LockedInPiece piecePop)
    {
        popCount += 1;
        piecesToPop.Add(piecePop.gameObject);
        //when this is ran it is intended to add the current piece to the list to pop them and increase the pop count
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
            //this loops through the list of pieces to pop, removes them from the grid and deletes the game object for them
        }
        piecesToPop.Clear();
        popCount = 0;
        //this clears the list of all pieces stored in it and resets pop count to 0
    }
}
