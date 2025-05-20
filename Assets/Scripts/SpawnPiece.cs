using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPiece : MonoBehaviour
{
    public GameObject[] Pieces;
    public GameObject gameBoard;
    public GameObject nextSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        nextSpawn = Pieces[Random.Range(0, Pieces.Length)];
        NewPiece();
    }

    // Update is called once per frame
    
    public void NewPiece()
    {
        Instantiate(nextSpawn, transform.position, Quaternion.identity, gameBoard.transform);
        nextSpawn = Pieces[Random.Range(0, Pieces.Length)];
    }
}
