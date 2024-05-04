using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public bool isRight;
    void Update()
    {
       // transform.Translate(Vector3.right * Time.deltaTime * GroundManager.instance.groundSpeed);
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
            if(isRight)
            {
                collision.gameObject.GetComponent<PlayerMove>().IsRightMove();
            }
            


        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
            collision.gameObject.GetComponent<PlayerMove>().IsNotRightMove();
        }
    }
}
