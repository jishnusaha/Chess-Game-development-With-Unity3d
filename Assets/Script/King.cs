using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman
{
    public override bool[,] PossibleMove()
    {
        //Debug.Log("possible move king")

        r = new bool[8, 8];
        Chessman c;
        
        //right
        if(CurrentX+1<8)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentZ];
            if (c == null) r[CurrentX + 1,CurrentZ] = true;
            else if(c.isWhite!=isWhite) r[CurrentX + 1, CurrentZ] = true;
        }
        
        //left
        if (CurrentX - 1 >= 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentZ];
            if (c == null) r[CurrentX - 1, CurrentZ] = true;
            else if (c.isWhite != isWhite) r[CurrentX - 1, CurrentZ] = true;
        }
        //up
        if (CurrentZ + 1 < 8)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentZ+1];
            if (c == null) r[CurrentX, CurrentZ + 1] = true;
            else if (c.isWhite != isWhite) r[CurrentX, CurrentZ + 1] = true;
        }
        //down
        if (CurrentZ -1 >= 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentZ - 1];
            if (c == null) r[CurrentX, CurrentZ - 1] = true;
            else if (c.isWhite != isWhite) r[CurrentX, CurrentZ - 1] = true;
        }

        //diagonal right up
        if (CurrentX + 1 < 8 && CurrentZ + 1 < 8)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentZ + 1];
            if (c == null) r[CurrentX + 1, CurrentZ + 1] = true;
            else if (c.isWhite != isWhite) r[CurrentX + 1, CurrentZ + 1] = true;
        }

        //diagonal right down
        if (CurrentX + 1 < 8 && CurrentZ - 1 >= 0 )
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentZ - 1];
            if (c == null) r[CurrentX + 1, CurrentZ - 1] = true;
            else if (c.isWhite != isWhite) r[CurrentX + 1, CurrentZ - 1] = true;
        }

        //diagonal left up
        if (CurrentX - 1 >= 0 && CurrentZ + 1 < 8)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentZ + 1];
            if (c == null) r[CurrentX - 1, CurrentZ + 1] = true;
            else if (c.isWhite != isWhite) r[CurrentX - 1, CurrentZ + 1] = true;
        }

        //diagonal left down
        if (CurrentX - 1 >=0 && CurrentZ - 1 >= 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentZ - 1];
            if (c == null) r[CurrentX - 1, CurrentZ - 1] = true;
            else if (c.isWhite != isWhite) r[CurrentX - 1, CurrentZ - 1] = true;
        }

        return r;
    }
}
