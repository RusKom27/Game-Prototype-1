using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    public class Marker
	{
		public Vector3 position;
		public Quaternion rotation;

		public Marker(Vector3 pos, Quaternion rot)
		{
			position = pos;
			rotation = rot;
			Debug.DrawLine(pos, pos + new Vector3(0.1f, 0.1f, 0.1f));
		}
	}

	public List<Marker> markerList = new List<Marker>();

	private void FixedUpdate()
	{
		UpdateMarkerList();
	}

	private void UpdateMarkerList()
	{
		markerList.Add(new Marker(transform.position, transform.rotation));
	}

	public void ClearMarkerList()
	{
		markerList.Clear();
		markerList.Add(new Marker(transform.position, transform.rotation));
	}
	
}
