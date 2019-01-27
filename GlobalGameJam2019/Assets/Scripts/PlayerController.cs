using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D player;

    public PuzzleManager puzzle;
    public Camera cam;
    Animator anim;
    public float speed;
    float interp;
    public float minscrollX, maxscrollX, minscrollY, maxscrollY;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        puzzle = puzzle.GetComponent<PuzzleManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        interp = speed * Time.deltaTime;
        Vector3 pos = cam.transform.position;
        if (transform.position.x > minscrollX && transform.position.x < maxscrollX)
        {
            pos.x = Mathf.Lerp(cam.transform.position.x, transform.position.x, interp);

        }
        if (transform.position.y > minscrollY && transform.position.y < maxscrollY)
        {
            pos.y = Mathf.Lerp(cam.transform.position.y, transform.position.y, interp);

        }
        cam.transform.position = pos;

        if (Input.GetAxis("Horizontal") > 0) {
            anim.SetBool("isFacingRight", true);
            anim.SetBool("isFacingUp", false);
            anim.SetBool("isFacingDown", false);
            anim.SetBool("isFacingLeft", false);

            Vector3 temp = player.velocity;
            temp.x = 2*speed;
            player.velocity = temp;
           
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("isFacingRight", false);
            anim.SetBool("isFacingUp", false);
            anim.SetBool("isFacingDown", false);
            anim.SetBool("isFacingLeft", true);
            Vector3 temp = player.velocity;
            temp.x = -2*speed;
            player.velocity = temp;

        }
        if (Input.GetAxis("Vertical") > 0)
        {
            anim.SetBool("isFacingRight", false);
            anim.SetBool("isFacingUp", true);
            anim.SetBool("isFacingDown", false);
            anim.SetBool("isFacingLeft", false);
            Vector3 temp = player.velocity;
            temp.y = 2*speed;
            player.velocity = temp;

        }
        if (Input.GetAxis("Vertical") < 0)
        {
            anim.SetBool("isFacingRight", false);
            anim.SetBool("isFacingUp", false);
            anim.SetBool("isFacingDown", true);
            anim.SetBool("isFacingLeft", false);
            Vector3 temp = player.velocity;
            temp.y = -2*speed;
            player.velocity = temp;

        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            Vector3 temp = player.velocity;
            temp.x = 0;
            player.velocity = temp;


        }
        if (Input.GetAxis("Vertical") == 0)
        {
            Vector3 temp = player.velocity;
            temp.y = 0;
            player.velocity = temp;

        }
        if(player.velocity.x != 0 || player.velocity.y != 0)
        {
            anim.SetBool("isWalking", true);
        }
        if (player.velocity.x == 0 && player.velocity.y == 0)
        {
            anim.SetBool("isWalking", false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "BALL")
        {
            puzzle.puzzle = PuzzleManager.puzzChoice.PUZZLE1;
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "Dialouge"){
            collision.gameObject.GetComponent<DialougeTrigger>().TriggerDialouge();
            Destroy(collision);
        }
    }
}