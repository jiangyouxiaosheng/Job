using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashGround : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * GroundManager.instance.groundSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
            StartCoroutine("DestroyGameObject");
        }
    }
 
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);

        }
    }
    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
  
}
