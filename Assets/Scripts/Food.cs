using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    void Update()
    {
        transform.localScale += new Vector3(0.0001f, 0.0001f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.tag == "Player")
		{
            collision.transform.parent.GetComponent<Player>().AddBodyPart(transform.localScale);
            gameObject.transform.parent.GetComponent<Field>().UpdateFood();
            Destroy(this.gameObject);
        }
    }
    
}
