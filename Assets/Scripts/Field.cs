using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject borderBox;
    [SerializeField] private float distance;
    private Vector3 leftTopPoint, rightTopPoint, rightBottomPoint, leftBottomPoint;

    private void Start()
	{
        leftBottomPoint = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, distance));
        leftTopPoint = mainCamera.ScreenToWorldPoint(new Vector3(0f, mainCamera.pixelHeight, distance));
        rightTopPoint = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, mainCamera.pixelHeight, distance));
        rightBottomPoint = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0f, distance));

        CreateBorder(new Vector3(0, leftTopPoint.y), new Vector3(Mathf.Abs(leftTopPoint.x) * 2, 0.3f, 1));
        CreateBorder(new Vector3(leftTopPoint.x, 0), new Vector3(0.3f, Mathf.Abs(leftTopPoint.y) * 2, 1));
        CreateBorder(new Vector3(0, -leftTopPoint.y), new Vector3(Mathf.Abs(leftTopPoint.x) * 2, 0.3f, 1));
        CreateBorder(new Vector3(-leftTopPoint.x, 0), new Vector3(0.3f, Mathf.Abs(leftTopPoint.y) * 2, 1));
    }
	
    private void CreateBorder(Vector3 position, Vector3 scale)
	{
        GameObject temp = Instantiate(borderBox, position, new Quaternion(0, 0, 0, 0), transform);
        temp.transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(leftTopPoint, rightTopPoint);
        Gizmos.DrawLine(rightTopPoint, rightBottomPoint);
        Gizmos.DrawLine(rightBottomPoint, leftBottomPoint);
        Gizmos.DrawLine(leftBottomPoint, leftTopPoint);
    }
}
