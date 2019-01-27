using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float maxDist;
    public GameObject cam;
    Vector3 camStart;
    public GameObject player;
    public GameObject ball;
    GameObject ballInst;
    Rigidbody2D rb;
    Vector3 rotationCenter;
    private Vector3 playerStartPos;
    private bool hitWall;
    public float speed;
    Vector3 startpos;
    Vector3 pointA;
    Vector3 ballPos;
    private Vector3 currentPos;


    public float rotationRadius = 10f;
    public float throwRate, throwWait;
    private bool blocked;
   private float posX, posY, angle = 0f;

    public enum patrolPat
    {
        HORIZONTAL,
        VERTICAL,
        CIRCULAR,
        DIAGONAL,
        BALL,
        PLAYERFOLLOW,
        NONE
    }



    public patrolPat currentPat, previousPat;

    // Start is called before the first frame update
    void Start()
    {
        camStart = cam.transform.position;
        playerStartPos = player.transform.position;
        blocked = false;
        hitWall = false;
        startpos = transform.position;
        Debug.Log(transform.position.x);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;
        ballPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        rotationCenter.x = startpos.x;
        rotationCenter.y = startpos.y-0.4f;
        posX = rotationCenter.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.y + Mathf.Sin(angle) * rotationRadius;
        if (blocked == false)
        {
            switch (currentPat)
            {
                case patrolPat.HORIZONTAL:
                    pointA.x = startpos.x + maxDist;
                    pointA.y = startpos.y;
                    transform.position = Vector3.Lerp(pointA, startpos, Mathf.PingPong(Time.time * speed, 1));
                    break;
                case patrolPat.VERTICAL:
                    pointA.x = startpos.x;
                    pointA.y = startpos.y + maxDist;
                    transform.position = Vector3.Lerp(pointA, startpos, Mathf.PingPong(Time.time * speed, 1));

                    //transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * speed, maxDist - (startpos.y)) + (maxDist * -1));
                    break;
                case patrolPat.CIRCULAR:
                    transform.position = new Vector2(posX, posY);
                    angle = angle + Time.deltaTime * speed;
                    if (angle >= 360f)
                        angle = 0f;
                    break;
                case patrolPat.DIAGONAL:
                    pointA.x = startpos.x + maxDist;
                    pointA.y = startpos.y + maxDist;
                    transform.position = Vector3.Lerp(pointA, startpos, Mathf.PingPong(Time.time * speed, 1));
                    break;
                case patrolPat.BALL:
                    if (ballInst == null)
                    {
                        ballInst = Instantiate(ball, ballPos, Quaternion.identity);
                    }
                    break;
                case patrolPat.PLAYERFOLLOW:
                    //  transform.LookAt(player.transform);
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                    break;
                case patrolPat.NONE:
                    break;
            }
        }


    }

    void ThrowBall()
    {
        ballInst.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, speed, 0), ForceMode2D.Impulse);



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Invoke("ThrowBall", 3);
        }

        if(collision.gameObject.tag == "Draggable")
        {
            blocked = true;
        }

        if(collision.gameObject.tag == "Player")
        {
            player.transform.position = playerStartPos;
            cam.transform.position = camStart;
            if(currentPat == patrolPat.PLAYERFOLLOW)
            {
                currentPat = previousPat;
                transform.position = startpos;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Draggable")
        {
            blocked = false;
        }
    }





}

