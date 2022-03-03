using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject borderBox;
    [SerializeField] private GameObject food;
    [SerializeField] private float distance;
    [SerializeField] private float foodSpawnPlaceOffset;

    private Vector3 leftTopPoint;
    private GameObject actualFood;

    private void Start()
	{
        leftTopPoint = mainCamera.ScreenToWorldPoint(new Vector3(0f, mainCamera.pixelHeight, distance));

		CreateBorder(new Vector3(0, leftTopPoint.y), new Vector3(Mathf.Abs(leftTopPoint.x) * 2, 0.3f, 1));
		CreateBorder(new Vector3(leftTopPoint.x, 0), new Vector3(0.3f, Mathf.Abs(leftTopPoint.y) * 2, 1));
		CreateBorder(new Vector3(0, -leftTopPoint.y), new Vector3(Mathf.Abs(leftTopPoint.x) * 2, 0.3f, 1));
		CreateBorder(new Vector3(-leftTopPoint.x, 0), new Vector3(0.3f, Mathf.Abs(leftTopPoint.y) * 2, 1));
        UpdateFood();
	}

	private void CreateBorder(Vector3 position, Vector3 scale)
	{
        GameObject temp = Instantiate(borderBox, position, new Quaternion(0, 0, 0, 0), transform);
        temp.transform.localScale = scale;
    }

    public void UpdateFood()
	{
        float randomX = Random.Range(leftTopPoint.x + foodSpawnPlaceOffset, -leftTopPoint.x - foodSpawnPlaceOffset);
        float randomY = Random.Range(leftTopPoint.y - foodSpawnPlaceOffset, -leftTopPoint.y + foodSpawnPlaceOffset);
        Destroy(actualFood);
        actualFood = Instantiate(food, new Vector3(randomX, randomY, 1), new Quaternion(0, 0, 0, 0), transform);
    }

}
