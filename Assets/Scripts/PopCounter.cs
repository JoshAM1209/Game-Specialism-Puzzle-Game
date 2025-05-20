using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopCounter : MonoBehaviour
{
    public GridManager gridManager;
    public LockedInPiece lockedInPiece;
    
    public List<GameObject> piecesToPop = new List<GameObject>();
    public int popCount = 0;

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (popCount >= 4)
        {
            foreach (GameObject piece in piecesToPop)
            {
                lockedInPiece = piece.GetComponent<LockedInPiece>();
                int popPosX = lockedInPiece.thisPiecePosX;
                int popPosY = lockedInPiece.thisPiecePosY;
                gridManager.grid[popPosX, popPosY] = null;
                piecesToPop.Remove(piece);
                Destroy(piece.gameObject);
            }
        }
    }
}
