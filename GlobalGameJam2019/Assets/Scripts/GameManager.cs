using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public EnemyPatrol enemy;
    EnemyPatrol inst;
    public GameObject puzz;
    PuzzleManager puuuuu;
    public bool swap = false;
    // Start is called before the first frame update
    void Start()
    {
        puuuuu = puzz.GetComponent<PuzzleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (puuuuu.state == PuzzleManager.STATE.BALL && swap)
        {
            SceneManager.LoadScene("Level_02", LoadSceneMode.Single);
            swap = false;
        }
        else if (puuuuu.state == PuzzleManager.STATE.MOP && swap)
        {
            SceneManager.LoadScene("Level_03", LoadSceneMode.Single);
            swap = false;
        }
        else if (puuuuu.state == PuzzleManager.STATE.RING && swap)
        {
            SceneManager.LoadScene("Level_04", LoadSceneMode.Single);
            swap = false;
        }
        else if (puuuuu.state == PuzzleManager.STATE.HAT && swap)
        {
            SceneManager.LoadScene("Level_03", LoadSceneMode.Single);
            swap = false;
        }
    }
}
