using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] puzzles;
    public GameObject puzzSpawn;
    public GameObject pieces;
    public GameManager game;
    Transform back;
    bool spawned = false;

    public enum puzzChoice
    {
        NONE,
        PUZZLE1,
        PUZZLE2,
        PUZZLE3,
        PUZZLE4
    }
    public puzzChoice puzzle;
    public enum STATE
    {
        NONE,
        BALL,
        MOP,
        RING,
        HAT
    }
    public STATE state;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform c in puzzSpawn.transform)
        {
            GameObject.Destroy(c.gameObject);
        }
        spawned = false;
    }
    private void Update()
    {
        switch (puzzle)
        {
            case puzzChoice.PUZZLE1:
                if(!spawned){
                    spawned = true;
                    Camera.main.transform.SetPositionAndRotation(new Vector3(0,0,-1),Quaternion.identity);
                    pieces = Instantiate(puzzles[0], new Vector2(Camera.main.transform.position.x,Camera.main.transform.position.y), Quaternion.identity, puzzSpawn.transform);
                    PuzzleADD(pieces);
                }
                if (PuzzleCheck(puzzles[0], pieces))
                {
                    puzzle = puzzChoice.NONE;
                    state = STATE.BALL;
                    game.swap = true;
                }
                break;
            case puzzChoice.PUZZLE2:
                if (!spawned)
                {
                    spawned = true;
                    Camera.main.transform.SetPositionAndRotation(new Vector3(0, 0, -1), Quaternion.identity);
                    pieces = Instantiate(puzzles[1], new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y), Quaternion.identity, puzzSpawn.transform);
                    PuzzleADD(pieces);
                }
                if (PuzzleCheck(puzzles[1], pieces))
                {
                    puzzle = puzzChoice.NONE;
                    state = STATE.MOP;
                    game.swap = true;
                }
                break;
            case puzzChoice.PUZZLE3:
                if (!spawned)
                {
                    spawned = true;
                    Camera.main.transform.SetPositionAndRotation(new Vector3(0, 0, -1), Quaternion.identity);
                    pieces = Instantiate(puzzles[2], new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y), Quaternion.identity, puzzSpawn.transform);
                    PuzzleADD(pieces);
                }
                if (PuzzleCheck(puzzles[2], pieces))
                {
                    puzzle = puzzChoice.NONE;
                    state = STATE.RING;
                }
                break;
            case puzzChoice.PUZZLE4:
                if (!spawned)
                {
                    spawned = true;
                    Camera.main.transform.SetPositionAndRotation(new Vector3(0, 0, -1), Quaternion.identity);
                    pieces = Instantiate(puzzles[3], new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y), Quaternion.identity, puzzSpawn.transform);
                    PuzzleADD(pieces);
                }
                if (PuzzleCheck(puzzles[3], pieces))
                {
                    puzzle = puzzChoice.NONE;
                    state = STATE.HAT;
                }
                break;
            case puzzChoice.NONE:
                foreach (Transform c in puzzSpawn.transform)
                {
                    GameObject.Destroy(c.gameObject);
                }
                spawned = false;
                break;
        }
    }
    public void PuzzleADD(GameObject _puzzle)
    {
        back = _puzzle.transform.Find("Background");
        back.localScale = new Vector3(Camera.main.pixelWidth,Camera.main.pixelHeight);
        Transform pices = _puzzle.transform.Find("Pieces");
        foreach (Transform c in pices)
        {
            float randOffsetX = Random.Range(-3, 4);
            float randOffsetY = Random.Range(-1.5f, 0.5f);
            Vector3 v = new Vector3(randOffsetX, randOffsetY);
            c.transform.position += v;
            
        }
    }
    public bool PuzzleCheck(GameObject _corPuzzle,GameObject _curPieces)
    {
        bool breakout = false;
        
        for (int i = 0;i<16;i++)
        {
            Transform pices = _corPuzzle.transform.Find("Pieces");
            Transform pces = _curPieces.transform.Find("Pieces");

            Transform c = pices.transform.GetChild(i);
            Transform h = pces.transform.GetChild(i);
            float a = Mathf.Abs(c.position.x - h.position.x);
            float b = Mathf.Abs(c.position.y - h.position.y);
            float distance = Mathf.Sqrt(a * a + b * b);
            if (distance > 2)
            {
                breakout = true;
                break;
            }
        }
        if (breakout == false)
        {
            Debug.Log("Correct");
            return true;
        }
        else
        {
            return false;
        }

        
    }
    
}
