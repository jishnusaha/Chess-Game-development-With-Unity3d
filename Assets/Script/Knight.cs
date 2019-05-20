using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Chessman
{
    public override bool[,] PossibleMove()
    {
       // Debug.Log("possible move knight")

        r = new bool[8, 8];
        Chessman c;
        //right
        if (CurrentX + 2 < 8)
        {
            if(CurrentZ-1>=0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 2, CurrentZ - 1];
                if(c==null)
                {
                    r[CurrentX + 2, CurrentZ - 1] = true;
                }
                else if(c.isWhite != isWhite)
                {
                    r[CurrentX + 2, CurrentZ - 1] = true;
                }
            }
           
            if(CurrentZ+1 < 8)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 2, CurrentZ + 1];
                if(c==null)
                {
                    r[CurrentX + 2, CurrentZ + 1] = true;
                }
                else if(c.isWhite != isWhite)
                {
                    r[CurrentX + 2, CurrentZ + 1] = true;
                }
            }
        }

        //left
        if (CurrentX - 2 >= 0)
        {
            if (CurrentZ - 1 >= 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 2, CurrentZ - 1];
                if (c == null)
                {
                    r[CurrentX - 2, CurrentZ - 1] = true;
                }
                else if (c.isWhite != isWhite)
                {
                    r[CurrentX - 2, CurrentZ - 1] = true;
                }
            }

            if (CurrentZ + 1 < 8)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 2, CurrentZ + 1];
                if (c == null)
                {
                    r[CurrentX - 2, CurrentZ + 1] = true;
                }
                else if (c.isWhite != isWhite)
                {
                    r[CurrentX - 2, CurrentZ + 1] = true;
                }
            }
        }

        //up
        if (CurrentZ + 2 < 8)
        {
            //left
            if (CurrentX - 1 >= 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentZ + 2];
                if (c == null)
                {
                    r[CurrentX - 1, CurrentZ + 2] = true;
                }
                else if (c.isWhite != isWhite)
                {
                    r[CurrentX - 1, CurrentZ + 2] = true;
                }
            }

            //right
            if (CurrentX + 1 < 8)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentZ + 2];
                if (c == null)
                {
                    r[CurrentX + 1, CurrentZ + 2] = true;
                }
                else if (c.isWhite != isWhite)
                {
                    r[CurrentX + 1, CurrentZ + 2] = true;
                }
            }
        }

        //down
        if (CurrentZ - 2 >= 0)
        {
            //left
            if (CurrentX - 1 >= 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentZ - 2];
                if (c == null)
                {
                    r[CurrentX - 1, CurrentZ - 2] = true;
                }
                else if (c.isWhite != isWhite)
                {
                    r[CurrentX - 1, CurrentZ - 2] = true;
                }
            }
            //right
            if (CurrentX + 1 < 8)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentZ - 2];
                if (c == null)
                {
                    r[CurrentX + 1, CurrentZ - 2] = true;
                }
                else if (c.isWhite != isWhite)
                {
                    r[CurrentX + 1, CurrentZ - 2] = true;
                }
            }
        }



        return r;
    }

}
