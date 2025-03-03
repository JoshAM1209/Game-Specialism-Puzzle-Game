using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    public Vector3 rotationPoint;
    
    private float timeCheck;
    public float fallSpeed = 0.8f;

    public static int height = 6;
    public static int width = 4;

    private static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if(!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if(!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }

        if(Time.time - timeCheck > (Input.GetKey(KeyCode.DownArrow) ? fallSpeed / 10 : fallSpeed))
        {
            transform.position += new Vector3(0, -1, 0);
            if(!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                //AddToGrid();
                this.enabled = false;
                FindObjectOfType<SpawnPiece>().NewPiece();
            }
            timeCheck = Time.time;
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int currPosX = Mathf.RoundToInt(children.transform.position.x);
            int currPosY = Mathf.RoundToInt(children.transform.position.y);

            grid[currPosX, currPosY] = children;
        }
    }
    
    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int currPosX = Mathf.RoundToInt(children.transform.position.x);
            int currPosY = Mathf.RoundToInt(children.transform.position.y);
            if (currPosX <= -width  || currPosX >= width || currPosY < -height)
            {
                return false;
            }

            //if(grid[currPosX, currPosY] != null)
            //{
            //    return false;
            //}
        }

        return true;
    }
}
