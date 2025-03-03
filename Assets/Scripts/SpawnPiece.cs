using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPiece : MonoBehaviour
{
    public GameObject[] Pieces;
    public GameObject gameBoard;
    
    // Start is called before the first frame update
    void Start()
    {
        NewPiece();
    }

    // Update is called once per frame
    public void NewPiece()
    {
        Instantiate(Pieces[Random.Range(0, Pieces.Length)], transform.position, Quaternion.identity, gameBoard.transform);
    }
}
