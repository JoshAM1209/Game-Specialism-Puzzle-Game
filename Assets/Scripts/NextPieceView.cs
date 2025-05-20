using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class NextPieceView : MonoBehaviour
{
    public SpawnPiece spawnPiece;
    public GameObject pieceToShow;
    private GameObject currentShown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowNext()
    {
        Destroy(currentShown);
        pieceToShow = spawnPiece.nextSpawn;
        currentShown = Instantiate(pieceToShow, transform.position, Quaternion.identity);
        currentShown.GetComponent<LockedInPiece>().enabled = false;
        //this is intended to show the next piece that will spawn but it does not currently work

    }
}
