using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 11;
    [SerializeField] private float turnSpeed = 180;
    [SerializeField] private float distanceBetween = 2f;
    [SerializeField] private List<GameObject> bodyParts = new List<GameObject>();

    public List<GameObject> snakeBody;
    private float direction;
    private float countUp = 0;

    void Start()
    {
        
    }

	private void Update()
	{
        direction = Input.GetAxis("Horizontal");
    }

	private void FixedUpdate()
    {
        if (bodyParts.Count > 0)
		{
            CreateBodyParts();
		}
        Movement(direction);
    }

    private void Movement(float direction)
	{
        
        snakeBody[0].GetComponent<Rigidbody2D>().velocity = snakeBody[0].transform.right * speed * Time.fixedDeltaTime;
        if (direction != 0)
		{
            snakeBody[0].transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime * direction));
		}
        if (snakeBody.Count > 1)
		{
			for (int i = 1; i < snakeBody.Count; i++)
			{
                MarkerManager markerManager = snakeBody[i - 1].GetComponent<MarkerManager>();
                snakeBody[i].transform.position = markerManager.markerList[0].position;
                snakeBody[i].transform.rotation = markerManager.markerList[0].rotation;
                markerManager.markerList.RemoveAt(0);
            }
		}
	}

    private void CreateBodyParts()
	{
        if (snakeBody.Count == 0)
		{
            CreateBodyPart(transform.position, transform.rotation, transform);
        }

        MarkerManager markerManager = snakeBody[snakeBody.Count - 1].GetComponent<MarkerManager>();
        if (countUp != 0)
		{
            markerManager.ClearMarkerList();
		}
        countUp += Time.deltaTime;
        if (countUp >= distanceBetween)
		{
            GameObject temp = CreateBodyPart(markerManager.markerList[0].position, markerManager.markerList[0].rotation, transform);
            temp.GetComponent<MarkerManager>().ClearMarkerList();
            countUp = 0;
		}

	}

    private GameObject CreateBodyPart(Vector3 position, Quaternion rotation, Transform transform)
	{
        GameObject temp = Instantiate(bodyParts[0], position, rotation, transform);
        if (!temp.GetComponent<MarkerManager>())
        {
            temp.AddComponent<MarkerManager>();
        }
        if (!temp.GetComponent<Rigidbody2D>())
        {
            temp.AddComponent<Rigidbody2D>();
            temp.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        temp.GetComponent<SpriteRenderer>().color = new Color(1f / Random.Range(0, 100), 1f / Random.Range(0, 100), 1f / Random.Range(0, 100));
        if (snakeBody.Count != 0)
            temp.transform.localScale = snakeBody[snakeBody.Count - 1].transform.localScale - new Vector3(0.01f, 0.01f, 0);
        snakeBody.Add(temp);
        bodyParts.RemoveAt(0);
        return temp;
    }
}
