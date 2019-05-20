using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlight : MonoBehaviour {

    public Material normal;
    public Material enemy;
    public Material check;
    public bool it;
    public Chessman[,] chessmans;
    public bool isWhiteturn;
    public static BoardHighlight Instance { set; get; }
    public GameObject highlightPrefab;
    private List<GameObject> highlights;
    void Start () {
        Instance = this;
        highlights = new List<GameObject>();
	}
	private GameObject GetHighlightObject()
    {
        GameObject go = highlights.Find(g => g.activeSelf);
        if(go==null)
        {
            go = Instantiate(highlightPrefab );
            highlights.Add(go);
        }
        return go;
    }
    public void HighlightAllowaedMoves(bool[,] moves )
    {
        chessmans = BoardManager.Instance.Chessmans;
        isWhiteturn = BoardManager.Instance.isWhiteTurn;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if(moves[i,j])
                {
                    //GameObject go = GetHighlightObject();
                    GameObject go=Instantiate(highlightPrefab);
                    if(chessmans[i,j]!=null)
                    {
                        if(chessmans[i, j].GetType() == typeof(King)) go.GetComponent<MeshRenderer>().material = check;
                        else go.GetComponent<MeshRenderer>().material = enemy;
                    }
                    else go.GetComponent<MeshRenderer>().material = normal;
                    highlights.Add(go);
                    go.transform.position = new Vector3(i+0.5f, -0.0386f, j+0.5f );
                }
            }
        }
    }
    public void Hidehighlights()
    {
        foreach (GameObject go in highlights)
        {
            Destroy(go);
            
        }
        highlights.Clear();
    }
}
