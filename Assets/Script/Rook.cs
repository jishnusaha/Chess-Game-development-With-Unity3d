using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman
{

    public override bool[,] PossibleMove()
    {
        //Debug.Log("possible move rook")

        r = new bool[8, 8];
        Chessman c;

        //right
        for(int i=CurrentX+1;i<8;i++)
        {
            c = BoardManager.Instance.Chessmans[i, CurrentZ];
            if (c!=null)
            {
                if(isWhite!=c.isWhite)
                {
                    r[i, CurrentZ] = true;
                }
                break;
            }
            r[i, CurrentZ] = true;
        }

        //left
        for (int i = CurrentX - 1; i >=0; i--)
        {
            c = BoardManager.Instance.Chessmans[i, CurrentZ];
            if (c != null)
            {
                if (isWhite != c.isWhite)
                {
                    r[i, CurrentZ] = true;
                }
                break;
            }
            r[i, CurrentZ] = true;
        }

        //up
        for (int i = CurrentZ + 1; i < 8; i++)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c != null)
            {
                if (isWhite != c.isWhite)
                {
                    r[CurrentX, i] = true;
                }
                break;
            }
            r[CurrentX, i] = true;
        }

        //down
        for (int i = CurrentZ - 1; i >= 0 ; i--)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c != null)
            {
                if (isWhite != c.isWhite)
                {
                    r[CurrentX, i] = true;
                }
                break;
            }
            r[CurrentX, i] = true;
        }



        return r;
    }
}
