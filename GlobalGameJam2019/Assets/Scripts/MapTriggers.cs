using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTriggers : MonoBehaviour
{
    public GameObject player;
    public EnemyPatrol enemy;
    private Vector3 playerStartPos;
    bool haz;
    // Start is called before the first frame update
    void Start()
    {
        haz = false;
        playerStartPos = player.transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && haz == false) 
        {
            collision.gameObject.transform.position = playerStartPos;
        }

      

        if(this.tag == "EnemyTrigger")
        {
            if (collision.gameObject.tag == "Player")
            {
                enemy.previousPat = enemy.currentPat;
                enemy.currentPat = EnemyPatrol.patrolPat.PLAYERFOLLOW;
                
            }
        }
       

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Draggable")
        {
            haz = true;
        }
    }
}
