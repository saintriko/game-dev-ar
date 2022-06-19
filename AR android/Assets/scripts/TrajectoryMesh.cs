using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryMesh : MonoBehaviour
{
    float[] radiuses = { 0.2f, 0.3f, 0.5f };
    private int POINT_COUNT = 15;
    private List<Vector3> positionBalls = new List<Vector3>();

    enum directionArc
    {
        horizontal,
        vertical,
    }
    void Start()
    {
        foreach (float radius in radiuses)
        {
            List<List<Vector3>> meshPoints = GetMeshPoints(new Vector3(0, 0, 0), radius);
            RenderMesh(meshPoints);
            Debug.Log(positionBalls.Count);
        }

    }

    List<List<Vector3>> GetMeshPoints(Vector3 pos, float radius)
    {
        List<List<Vector3>> meshPoints = new List<List<Vector3>>();
        for (int i = 0; i < POINT_COUNT; i++)
        {
            float alpha = Mathf.Deg2Rad * (i * 180f / (POINT_COUNT - 1));
            float offset = radius * Mathf.Cos(alpha);
            float newRadius = radius * Mathf.Sin(alpha);
            List<Vector3> points = GetArcPoint(newRadius, new Vector3(pos.x, pos.y + (radius - newRadius), pos.z + offset), directionArc.horizontal);
            meshPoints.Add(points);
        }
        for (int i = 0; i < POINT_COUNT; i++)
        {
            meshPoints.Add(new List<Vector3>());
            positionBalls.InsertRange(positionBalls.Count, meshPoints[i]);
        }
        for (int i = 0; i < POINT_COUNT; i++)
        {
            for (int j = 0; j < POINT_COUNT; j++)
            {
                meshPoints[POINT_COUNT + i].Add(meshPoints[j][i]);
            }
        }
        return meshPoints;
    }


    private List<Vector3> GetArcPoint(float radius, Vector3 position, directionArc direction)
    {
        List<Vector3> points = new List<Vector3>();
        float alpha = 0;
        if (direction == directionArc.vertical)
        {
            alpha = Mathf.Deg2Rad * 90;
        }
        for (int j = 0; j < POINT_COUNT; j++)
        {
            float beta = Mathf.Deg2Rad * (j * 180f / (POINT_COUNT - 1));
            float x = radius * Mathf.Cos(beta) * Mathf.Cos(alpha) + position.x;
            float z = radius * Mathf.Cos(beta) * Mathf.Sin(alpha) + position.z;
            float y = radius - radius * Mathf.Sin(beta) + position.y;
            Vector3 pos = new Vector3(x, y, z);
            points.Add(pos);
        }
        return points;
    }

    private void RenderMesh(List<List<Vector3>> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            GameObject line = new GameObject();
            line.transform.parent = this.gameObject.transform;
            line.transform.localScale = new Vector3(1, 1, 1);
            line.transform.position = new Vector3(0, 0.05f, 0);
            line.name = "Arc-" + i.ToString();
            LineRenderer lr = line.AddComponent<LineRenderer>();
            lr.positionCount = points[i].Count;
            lr.SetPositions(points[i].ToArray());
            lr.startWidth = 0.002f;
            lr.endWidth = 0.002f;
            lr.startColor = new Color(0, 100, 0);
            lr.endColor = new Color(0, 100, 0);
            lr.useWorldSpace = false;
            lr.enabled = true;
        }
    }

}
