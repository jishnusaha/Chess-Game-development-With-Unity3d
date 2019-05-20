using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChessPiece : MonoBehaviour {

    public Transform target;
    public float speed=1;
    /*public void Update()
    {
        if(transform.position!=target.position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else
        {
            GetComponent<MoveChessPiece>().enabled = false;
            GetComponent<BoardHighlight>().enabled = true;


        }
    } */
}
