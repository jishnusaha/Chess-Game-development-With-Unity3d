using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman
{
    public override bool[,] PossibleMove()
    {
        //Debug.Log("possible move pawn")
        r = new bool[8, 8];
        Chessman c, c2;
        
        //white team
        if (isWhite)
        {
            //dioganl Left
            if (CurrentX != 0 && CurrentZ != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentZ + 1];
                if (c != null && !c.isWhite)
                {

                    //Debug.Log("true");
                    r[CurrentX - 1, CurrentZ + 1] = true;
                }
                
            }

            //diogonal right
            if (CurrentX != 7 && CurrentZ != 7)
            {
                //Debug.Log("checking for digonal right");
                //Debug.Log("selected position : " + CurrentX + " " + CurrentZ);
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentZ + 1];
                if (c != null && !c.isWhite)
                {
                    //Debug.Log("true");
                    r[CurrentX + 1, CurrentZ + 1] = true;
                }
                //else Debug.Log("here someone. index" + c.index);
            }
            
            //middle
            if (CurrentZ != 7)
            {
                //Debug.Log("checking for middle");
                //Debug.Log("selected position : " + CurrentX + " " + CurrentZ);
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentZ + 1];
                if (c == null)
                {
                    //Debug.Log("true");
                    r[CurrentX, CurrentZ + 1] = true;
                }
            }

            //middel on first move
            if (CurrentZ == 1)
            {
                //Debug.Log("checking for first");
                //Debug.Log("selected position : " + CurrentX + " " + CurrentZ);
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentZ + 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentZ + 2];
                if (c == null && c2 == null)
                {
                    //Debug.Log("true");
                    r[CurrentX, CurrentZ + 2] = true;
                }

            }
        }
        else
        {
            //dioganl Left
            if (CurrentX != 0 && CurrentZ != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentZ - 1];
                if (c != null && c.isWhite)
                    r[CurrentX - 1, CurrentZ - 1] = true;
            }
            
            //diogonal right
            if (CurrentX != 7 && CurrentZ != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentZ - 1];
                if (c != null && c.isWhite)
                    r[CurrentX + 1, CurrentZ - 1] = true;
            }
            
            //middle
            if (CurrentZ != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentZ - 1];
                if (c == null)
                    r[CurrentX, CurrentZ - 1] = true;
            }

            //middel on first move
            if (CurrentZ == 6)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentZ - 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentZ - 2];
                if (c == null && c2 == null)
                    r[CurrentX, CurrentZ - 2] = true;


            }
        }
        return r;
    }
}
