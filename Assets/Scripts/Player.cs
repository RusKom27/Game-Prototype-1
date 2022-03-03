using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 11;
    [SerializeField] private float turnSpeed = 180;
    [SerializeField] private float distanceBetween = 2f;
    [SerializeField] private int countOfCells;
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject UIControl;

    public List<GameObject> snakeBody;
    private float direction;
    private float countUp = 0;
    private Vector3 lastScale = new Vector3(0.2f, 0.2f, 1);

	private void Update()
	{
        direction = Input.GetAxis("Horizontal");
    }

	private void FixedUpdate()
    {
        ManageSnakeBody();
        if (snakeBody.Count > 0)
		{
            Movement(direction);
        }
    }

    public void Respawn()
	{
        transform.position = new Vector3(0, 0, 0);
        countOfCells = 2;
        speed = 100;
        foreach (GameObject cell in snakeBody)
        {
            if (snakeBody.Count > 2)
			{
                Destroy(cell.gameObject);
            }
			else
			{
                return;
			}
        }
	}

    private void ManageSnakeBody()
	{
        if (countOfCells > 0)
        {
            CreateBodyParts();
        }
		for (int i = 0; i < snakeBody.Count; i++)
		{
            if (snakeBody[i] == null)
			{
                snakeBody.RemoveAt(i);
                i--;
			}
		}
        if (snakeBody.Count == 0)
		{
            UIControl.GetComponent<UIControl>().GameOver();
		}
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
                snakeBody[i].transform.position = markerManager.markerList[markerManager.markerList.Count - 1].position;
                snakeBody[i].transform.rotation = markerManager.markerList[markerManager.markerList.Count - 1].rotation;
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
        if (snakeBody.Count > 0)
        {
            MarkerManager markerManager = snakeBody[0].GetComponent<MarkerManager>();

            if (countUp > 4)
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
	}

    private GameObject CreateBodyPart(Vector3 position, Quaternion rotation, Transform transform)
	{
        GameObject temp = Instantiate(cell, position, rotation, transform);
        if (countOfCells > 0)
        {
            countOfCells--;
        }
        temp.transform.localScale = lastScale;
        snakeBody.Add(temp);
        
        return temp;
    }

    public void AddBodyPart(Vector3 scale)
	{
        countOfCells++;
        UIControl.GetComponent<UIControl>().IncreaseScores();
        lastScale = scale;
    }

    public void IncreaseDifficulty()
	{
        speed += speed / 100 * 0.5f;
	}
}
