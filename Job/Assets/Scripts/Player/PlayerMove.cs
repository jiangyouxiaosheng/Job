using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D playerRb2D;
    public bool isRightMove;

    [SerializeField] private float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
     // float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        transform.Translate((direction * (speed * Time.deltaTime)));

       

        if(isRightMove)
        {
            transform.Translate(Vector3.right*0.001f);
        }
        else
        {
            transform.Translate(Vector3.zero);
        }
       
    }

    public void IsRightMove()
    {
        isRightMove = true; 
    }

    public void IsNotRightMove()
    {
        isRightMove = false;
    }
}
