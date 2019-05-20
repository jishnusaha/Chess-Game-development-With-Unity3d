using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chessman : MonoBehaviour {
    
    public int index = -10;
    public int CurrentX { set; get; }
    public int CurrentZ { set; get; }
    public bool isWhite;
    public bool[,] r;
    public void setPosition(int x, int z)
    {
        CurrentX = x;
        CurrentZ = z;
    }
    public virtual bool[,] PossibleMove()
    {
        return new bool[8, 8];
    }
    /*
    public void isKingInDanger()
    {
        for (int i=0;i<8;i++)
        {
            for(int j=0;j<8;j++)
            {
                int intiX = CurrentX, initz = CurrentZ;
                if(r[i,j])
                {
                    List<GameObject> temp = new List<GameObject>();
                    BoardManager.Instance.getActiveChessman(temp);
                    Chessman[,] chessmans = new Chessman[8, 8];
                    BoardManager.Instance.getChessmans(chessmans);
                    if(chessmans[i,j]==null)
                    {
                        Chessman c = chessmans[CurrentX, CurrentZ];
                        chessmans[CurrentX, CurrentZ] = null;
                        setPosition(i, j);
                    }

                }
                CurrentX = intiX; CurrentZ = initz;
            }
        }
    }

    public bool isChecked(Chessman[,] chessmans)
    {
        Debug.Log("in ischecked");
        bool[,] updatedAllowedMoves = new bool[8, 8];
        for(int i=0;i<8;i++)
        {
            for(int j=0;j<8;j++)
            {
                if (isWhite == !chessmans[i, j].isWhite)
                {
                    bool[,] singlecheck = chessmans[i, j].PossibleMove();
                    for(int p=0;p<8;p++)
                    {
                        for(int q=0;q<0;q++)
                    }
                }
            }
        }
    }
    */
}
