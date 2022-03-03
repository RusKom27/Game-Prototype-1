using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    void Update()
    {
        if (Time.timeScale > 0)
		{
            transform.localScale += transform.localScale / 100 * 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.tag == "Player")
		{
            GameObject player = collision.transform.parent.gameObject;
            player.GetComponent<Player>().AddBodyPart(transform.localScale);
            player.GetComponent<Player>().IncreaseDifficulty();
            gameObject.transform.parent.GetComponent<Field>().UpdateFood();
        }
    }
    
}
