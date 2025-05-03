using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Points
{
    public List<Transform> points;
}
[System.Serializable]
public class PointsList
{
    public List<Points> pointsList;

    public int Count()
    {
        return pointsList.Count;
    }
}
