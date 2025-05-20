using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    public Vector3 rotationPoint;

    private float timeCheck;
    public float fallSpeed = 0.8f;
    //these are used to adjust how fast the piece falls

    public static int height = 6;
    public static int width = 4;
    //this stores the possible spaces a piece can move between, it is used when checking if a move is a valid one to make

    public SpawnPiece spawnPiece;
    public GridManager gridManager;

    public GameObject mainPiece;
    public GameObject secondPiece;
    //these store both individual pieces in the current pair of pieces

    public MainPiecePos mainPiecePos;
    public SecondPiecePos secondPiecePos;
    //these store the position scripts for both individual pieces

    public LockedInPiece lockedInPiece1;
    public LockedInPiece lockedInPiece2;
    //these store the lockedinpiece script on both individual pieces

    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //all uses of keycode were intended to be replaced with unity input system
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            mainPiecePos.piecePosX -= 1;
            secondPiecePos.piecePosX -= 1;
            //moves the piece and updates the values stored in position scripts to correlate
            if (!ValidMove() || !ValidGrid()) //runs a check to see if the move is valid and if it isn't it reverts the piece back to it's previous position
            {
                transform.position -= new Vector3(-1, 0, 0);
                mainPiecePos.piecePosX += 1;
                secondPiecePos.piecePosX += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) //same as previous code for other directions
        {
            transform.position += new Vector3(1, 0, 0);
            mainPiecePos.piecePosX += 1;
            secondPiecePos.piecePosX += 1;
            if (!ValidMove() || !ValidGrid())
            {
                transform.position -= new Vector3(1, 0, 0);
                mainPiecePos.piecePosX -= 1;
                secondPiecePos.piecePosX -= 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            RotationCheck();
            if (!ValidMove() || !ValidGrid())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                RotationCheck();
            }
        }

        if (Time.time - timeCheck > (Input.GetKey(KeyCode.DownArrow) ? fallSpeed / 10 : fallSpeed))
        {
            transform.position += new Vector3(0, -1, 0);
            mainPiecePos.piecePosY -= 1;
            secondPiecePos.piecePosY -= 1;
            if (!ValidMove() && (mainPiecePos.piecePosY < 0 || secondPiecePos.piecePosY < 0))
            {
                transform.position -= new Vector3(0, -1, 0);
                mainPiecePos.piecePosY += 1;
                secondPiecePos.piecePosY += 1;
                AddToGrid();
                this.enabled = false;
                lockedInPiece1.enabled = true;
                lockedInPiece2.enabled = true;
                FindObjectOfType<SpawnPiece>().NewPiece();
            }
            else if (!ValidMove() || !ValidGrid())
            {
                transform.position -= new Vector3(0, -1, 0);
                mainPiecePos.piecePosY += 1;
                secondPiecePos.piecePosY += 1;
                AddToGrid();
                this.enabled = false;
                lockedInPiece1.enabled = true;
                lockedInPiece2.enabled = true;
                FindObjectOfType<SpawnPiece>().NewPiece();
            }
            timeCheck = Time.time;
        }
    }

    void AddToGrid()
    {
        gridManager.grid[mainPiecePos.piecePosX, mainPiecePos.piecePosY] = mainPiece;
        gridManager.grid[secondPiecePos.piecePosX, secondPiecePos.piecePosY] = secondPiece;
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int currPosX = Mathf.RoundToInt(children.transform.position.x);
            int currPosY = Mathf.RoundToInt(children.transform.position.y);
            if (currPosX <= -width || currPosX >= width || currPosY < -height)
            {
                return false;
            }
        }
        return true;
    }
      
    bool ValidGrid()
    {
        if(gridManager.grid[mainPiecePos.piecePosX, mainPiecePos.piecePosY] || gridManager.grid[secondPiecePos.piecePosX, secondPiecePos.piecePosY] != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void RotationCheck()
    {
        if(mainPiece.transform.position.y > secondPiece.transform.position.y)
        {
            secondPiecePos.piecePosX = mainPiecePos.piecePosX;
            secondPiecePos.piecePosY = mainPiecePos.piecePosY - 1;
        }
        else if (mainPiece.transform.position.y < secondPiece.transform.position.y)
        {
            secondPiecePos.piecePosX = mainPiecePos.piecePosX;
            secondPiecePos.piecePosY = mainPiecePos.piecePosY + 1;
        }
        else if (mainPiece.transform.position.y == secondPiece.transform.position.y)
        {
            if(mainPiece.transform.position.x > secondPiece.transform.position.x)
            {
                secondPiecePos.piecePosX = mainPiecePos.piecePosX - 1;
                secondPiecePos.piecePosY = mainPiecePos.piecePosY;
            }
            else if (mainPiece.transform.position.x < secondPiece.transform.position.x)
            {
                secondPiecePos.piecePosX = mainPiecePos.piecePosX + 1;
                secondPiecePos.piecePosY = mainPiecePos.piecePosY;
            }
        }
    }
}
