using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    Vector3 target = Vector3.zero;
    float smoothing = 4.5f;
    private bool isMoving = false;
    private int centreX = 4, centreZ = 4, radius = 8;
    private float pi = 3.1416f, angle = -180, an = 90f;
    public Transform camera;
    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }
    public Chessman[,] Chessmans { set; get; } //store chessman component if there is an GameObject in that specefic position

    private bool blackdown,whitedown;
    private Vector2 blackupPos = new Vector2(-1, 3), blackdownPos = new Vector2(-1, 4), whiteupPos = new Vector2(8, 3), whitedownPos = new Vector2(8, 4);

    public bool[,] whiteKingFreeMove { set; get; }
    private bool[,] blackKingFreeMove { set; get; }

    private Chessman selectedChessman; //current selected chessman
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;
    private Quaternion orientation = Quaternion.Euler(90, 0, 0);
    private int selectionX = -1;
    private int selectionZ = -1;
    public List<GameObject> chessPrefab;
    public List<GameObject> activeChessmans; //store the active chessman in board
    private List<GameObject> deadChessmans; //store the Dead chessman in board
    public bool isWhiteTurn = true;
    public bool cameraTowhite = true;
    private void Start()
    {
        Instance = this;
        SpawnAllChessmans();
    }
    private void Update()
    {
        whiteKingFreeMove = checkKingFreeMove(true);
        blackKingFreeMove = checkKingFreeMove(false);
        UpdateSelection();
        DrawChessboard();

        if(isMoving)
        {
            //Debug.Log("in ismoving");
            if(selectedChessman.transform.position!=target)
            {
                selectedChessman.transform.position = Vector3.MoveTowards(selectedChessman.transform.position, target, smoothing * Time.deltaTime);
            }
            else
            {
                BoardHighlight.Instance.Hidehighlights();
                isMoving = false;
                Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentZ] = null;
                Chessmans[(int)target.x, (int)target.z] = selectedChessman;
                selectedChessman.setPosition((int)target.x, (int)target.z);
                if (selectedChessman.GetType() == typeof(Pawn) && ((int)target.z == 7 || (int)target.z == 0))
                {
                    if (selectedChessman.isWhite) SpawnChessman(1, (int)target.x, (int)target.z);
                    else SpawnChessman(7, (int)target.x, (int)target.z);
                    activeChessmans.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                }
                
                selectedChessman = null;
                isWhiteTurn = !isWhiteTurn;
                if (isWhiteTurn) Invoke( "MoveCamera_To_White",0.5f);
                else Invoke("MoveCamera_To_Black",0.5f);



            }

        }
        else if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("mouseclick");
            //print("pint");
            //if (Input.GetMouseButtonDown(0))
            //if(Input.GetKeyDown(KeyCode.Space))
            //Debug.Log(selectionX + " " + selectionZ);
            if (selectionX >= 0 && selectionZ >= 0)
            {
                if (selectedChessman == null)
                {
                    //Debug.Log("selecting");
                    //not selected one. select chessman
                    Slectchessman(selectionX, selectionZ);
                }
                else
                {
                    Debug.Log("move function calling");
                    //already selected. now move
                    MoveChessman(selectionX, selectionZ);
                    //Debug.Log("active chessman : " + activeChessmans.Count);
                }
            }


        }
    }
    private void MoveChessman(int x, int z)
    {
        Debug.Log("trying to move");
        if (allowedMoves[x, z])
        {
            //Debug.Log("it is allowed move");
            Chessman enemy = Chessmans[x, z];
            if (enemy != null) //if there is an enemy
            {
                //Debug.Log("destroying enemy");
                activeChessmans.Remove(enemy.gameObject);
                //Destroy(enemy.gameObject); //removing from board
                enemy.transform.position = getDestroyPosition(enemy);
                Chessmans[x, z] = null; //removing from chessman 2d array

                if (enemy.GetType() == typeof(King))
                {
                    //end game
                    EndGame();
                }
            }
            isMoving = true;
            //Debug.Log("no enemy found");
            target = getObjectPosition(selectedChessman.index, x, z);
            BoardHighlight.Instance.Hidehighlights();
            return;
        }
        Debug.Log("not allowed move");
        selectedChessman = null;
        BoardHighlight.Instance.Hidehighlights();
        Slectchessman(selectionX, selectionZ);
        //Debug.Log("selected chessmen null");
    }
    private void Slectchessman(int x, int z)
    {
        Debug.Log("tryig to select " + x + " " + z);
        if (Chessmans[x, z] == null) return;
        if (Chessmans[x, z].isWhite != isWhiteTurn) return;
        allowedMoves = Chessmans[x, z].PossibleMove();
        BoardHighlight.Instance.HighlightAllowaedMoves(allowedMoves);

        selectedChessman = Chessmans[x, z];
        Debug.Log("selected");


    }
    public void getActiveChessman(List<GameObject> go)
    {
        go = activeChessmans;
    }
    public void getChessmans(Chessman [,] temp)
    {
        temp = Chessmans;
    }
    private void SpawnAllChessmans()
    {
        activeChessmans = new List<GameObject>();
        Chessmans = new Chessman[8, 8];
        //white chessmans
        {
            //king
            SpawnChessman(0, 4, 0);

            //queen
            SpawnChessman(1, 3, 0);

            //rook
            SpawnChessman(2, 0, 0);
            SpawnChessman(2, 7, 0);

            //bishop
            SpawnChessman(3, 2, 0);
            SpawnChessman(3, 5, 0);

            //knight
            SpawnChessman(4, 1, 0);
            SpawnChessman(4, 6, 0);

            //pawn
            for (int i = 0; i < 8; i++)
                SpawnChessman(5, i, 1);
        }

        //black chessmans
        {
            //king
            SpawnChessman(6, 4, 7);

            //queen
            SpawnChessman(7, 3, 7);

            //rook
            SpawnChessman(8, 0, 7);
            SpawnChessman(8, 7, 7);

            //bishop
            SpawnChessman(9, 2, 7);
            SpawnChessman(9, 5, 7);

            //knight
            SpawnChessman(10, 1, 7);
            SpawnChessman(10, 6, 7);

            //pawn
            for (int i = 0; i < 8; i++)
                SpawnChessman(11, i, 6);
        }

    }
    private void UpdateSelection()
    {
        if (!Camera.main) return;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessLayer")))
        {
            ////Debug.Log(hit.point);
            selectionX = (int)hit.point.x;
            selectionZ = (int)hit.point.z;
            //Debug.Log(selectionX + " " + selectionZ);
        }
        else
        {
            selectionX = -1;
            selectionZ = -1;
        }

    }
    private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;
        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
        }
        for (int j = 0; j <= 8; j++)
        {
            Vector3 start = Vector3.right * j;
            Debug.DrawLine(start, start + heightLine);
        }
        if (selectionX >= 0 && selectionZ >= 0)
        {
            Vector3 start = Vector3.right * selectionX + Vector3.forward * selectionZ;
            Vector3 end = start + Vector3.forward + Vector3.right;
            Debug.DrawLine(start, end);
            Debug.DrawLine(start + Vector3.forward, end - Vector3.forward);
        }

    }
    private void SpawnChessman(int index, int x, int z)
    {
        GameObject go = Instantiate(chessPrefab[index], getObjectPosition(index, x, z), orientation) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[x, z] = go.GetComponent<Chessman>();
        Chessmans[x, z].index = index;
        Chessmans[x, z].setPosition(x, z);
        activeChessmans.Add(go);
    }
    private Vector3 getObjectPosition(int index, int x, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += TILE_SIZE * x + TILE_OFFSET;
        origin.z += TILE_SIZE * z + TILE_OFFSET;
        if (index == 0 || index == 6) origin = origin + Vector3.up * 0.9f;//king
        else if (index == 3 || index == 9) origin = origin + Vector3.up * 0.75f; //bishop
        else if (index == 4 || index == 10) origin = origin + Vector3.up * 0.8f; //knight
        else if (index == 1 || index == 7) origin = origin + Vector3.up * 0.75f; //queen
        else if (index == 2 || index == 8) origin = origin + Vector3.up * 0.55f; //rook
        else if (index == 5 || index == 11) origin = origin + Vector3.up * 0.47f; //pawn
        return origin;
    }
    private void MoveCamera_To_Black()
    {
        //Debug.Log("in to black");
        float an = 90f;
        for (float angle = -180; angle >= -270; angle -= 90)
        {
            int cosvalue = (int)Mathf.Cos(angle * (pi / 180)) * 1000;
            float cosfloat = cosvalue / 1000.0f;
            int sinvalue = (int)Mathf.Sin(angle * (pi / 180)) * 1000;
            float sinfloat = sinvalue / 1000.0f;
            float current_x = centreX + radius * cosfloat;
            float current_z = centreZ + radius * sinfloat;
            //camera.position = new Vector3(current_x, 8, current_z);
            camera.position = new Vector3(current_x, 8, current_z);
            //camera.Rotate(0, an, 0);
            camera.rotation = Quaternion.Euler(40, an, 0);
            an += 90;
            //Debug.Log("angle " + angle + ". x : " + current_x + " . z : " + current_z);
            //angle -= 90;

        }
    }
    private void MoveCamera_To_White()
    {
        //Debug.Log("in to white");
        float an = 270f;
        for (float angle = -360; angle >= -450; angle -= 90)
        {
            int cosvalue = (int)Mathf.Cos(angle * (pi / 180)) * 1000;
            float cosfloat = cosvalue / 1000.0f;
            int sinvalue = (int)Mathf.Sin(angle * (pi / 180)) * 1000;
            float sinfloat = sinvalue / 1000.0f;
            float current_x = centreX + radius * cosfloat;
            float current_z = centreZ + radius * sinfloat;
            //camera.position = new Vector3(current_x, 8, current_z);
            camera.position = new Vector3(current_x, 8, current_z);
            //camera.Rotate(0, an, 0);
            camera.rotation = Quaternion.Euler(40, an, 0);
            an += 90;


        }

    }
    private void EndGame()
    {
        if (isWhiteTurn) WinName.winName = "WHITE ";
        else WinName.winName = "BLACK ";
        SceneManager.LoadScene("Finish");

    }
    private IEnumerator MoveToPosition(Vector3 target)
    {
        float d = Vector3.Distance(selectedChessman.transform.position, target);
        while (d > 0.5f)
        {
            selectedChessman.transform.position = Vector3.MoveTowards(selectedChessman.transform.position, target, 0.5f*Time.deltaTime);
            d = Vector3.Distance(selectedChessman.transform.position, target);
            yield return null;
            //d = Vector3.Distance(selectedChessman.transform.position, target);
        }
        yield return new WaitForSeconds(3.0f);

        /*
        float smoothing = 0.5f;
        float d = Vector3.Distance(selectedChessman.transform.position, target);
        while (d > 0.05f)
        {
            selectedChessman.transform.position = Vector3.Lerp(selectedChessman.transform.position, target, smoothing * Time.deltaTime);
            d = Vector3.Distance(selectedChessman.transform.position, target);
            yield return null;
        }
        print("reached to target");
        yield return new WaitForSeconds(3.0f);
        print("my corutine is now finished");

        /*
        Debug.Log("in movetopos corutine");
        
        float t_x = 1, t_z = 1;
        if (chessman.position.x == x) t_x = 0;
        else if (chessman.position.z == z) t_z = 0;
        while (chessman.position.x <= x || chessman.position.z <= z)
        {
            var p_x = t_x * Time.deltaTime * 150.0f;
            var p_z = t_z * Time.deltaTime * 150.0f;

            //chessman.transform.Rotate(0, x, 0);
            chessman.Translate(p_x*0.5f, 0, p_z*0.5f);
            
            Debug.Log("before wait");
            //new WaitForSeconds(0.5f);
        }
        yield return 0;
        */

    }
    private Vector3 getDestroyPosition(Chessman enemy)
    {
        int posX, posZ;
        if (enemy.isWhite)
        {
            if (whitedown)
            {
                //Debug.Log("white down");
                if (whitedownPos.y == 0) { posX =(int) ++whitedownPos.x; posZ =(int) (whitedownPos.y=5); }
                else { posX =(int) whitedownPos.x; posZ = (int)--whitedownPos.y; }
                
            }
            else
            {
                //Debug.Log("whiteup");
                if (whiteupPos.y == 7) { posX = (int)++whiteupPos.x; posZ = (int)(whiteupPos.y = 3); }
                else { posX = (int)whiteupPos.x; posZ = (int)++whiteupPos.y; }
            }
            whitedown = !whitedown;
            
        }
        else
        {
            if (blackdown)
            {
                //Debug.Log("black down");
                if (blackdownPos.y == 0) { posX = (int)--blackdownPos.x; posZ = (int)(blackdownPos.y = 5); }
                else { posX = (int)blackdownPos.x; posZ = (int)--blackdownPos.y; }

            }
            else
            {
                //Debug.Log("black up");
                if (blackupPos.y == 7) { posX = (int)--blackupPos.x; posZ = (int)(blackupPos.y = 3); }
                else { posX = (int)blackupPos.x; posZ = (int)++blackupPos.y; }
            }
            blackdown = !blackdown;
        }
        //Debug.Log("posX : " + posX + " posZ : " + posZ);
        return getObjectPosition(enemy.index, posX, posZ);
    }
    private bool[,] checkKingFreeMove(bool white)
    {
        

        bool[, ] free = new bool[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                free[i, j] = true;
            }
        }
        if (white)
        {
            foreach(GameObject go in activeChessmans)
            {
                Chessman c = go.GetComponent<Chessman>();
                if(!c.isWhite)
                {
                    bool[,] r = c.PossibleMove();
                    for(int i=0;i<8;i++)
                    {
                        for(int j=0;j<8;j++)
                        {
                            if (r[i, j]) free[i, j] = false;
                             
                        }
                    }
                }
            }
        }
        else
        {
            foreach (GameObject go in activeChessmans)
            {
                Chessman c = go.GetComponent<Chessman>();
                if (c.isWhite)
                {
                    bool[,] r = c.PossibleMove();
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (r[i, j]) free[i, j] = false;
                        }
                    }
                }
            }
        }
        
        return free;
    }
    private void reversePlayingMood()
    {


        isWhiteTurn = !isWhiteTurn;
    }
    
}
