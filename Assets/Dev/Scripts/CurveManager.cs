using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CurveManager : MonoBehaviour
{
    public List<GameObject> points;
    [Range(0.001f, 0.1f)] public float pas = 0.5f;
    public GameObject prefab;
    [Range(0, 200)] public int nbPattoun; 
    private int index ;
    GameObject DossierPoints; 
    GameObject Dossier_go; 

    void OnDrawGizmos()
    {
        Init(); 
        DrawCurve(false);
    }
    void DrawCurve(bool instantiate)
    {
        for (int i = 0; i < points.Count - 2; i += 2)
        {
            for (float t = 0; t <= 1; t += pas)
            {
                Vector3 _AB = Vector3.Lerp(points[i].transform.position, points[i + 1].transform.position, t);
                Vector3 _BC = Vector3.Lerp(points[i + 1].transform.position, points[i + 2].transform.position, t);
                Vector3 posFinal = Vector3.Lerp(_AB, _BC, t);
                Vector3 posLineMax = Vector3.Lerp(_AB, _BC, t + pas);

                if (instantiate == false) Gizmos.DrawLine(posFinal, posLineMax);
            }
        }
    }
    void Init()
    {
        if (DossierPoints == null)
        {
            DossierPoints = new GameObject("DossierPoints");
            DossierPoints.transform.SetParent(transform);
        }

        if (Dossier_go == null)
        {
            Dossier_go = new GameObject("Dossier_go");
            Dossier_go.transform.SetParent(transform);
        }

        if (points.Count == 0)
        {
            GameObject pointA = new GameObject("A");
            pointA.transform.position = new Vector3(0, 0, 0);
            pointA.transform.SetParent(DossierPoints.transform);
            points.Insert(0, pointA);

            GameObject pointB = new GameObject("B");
            pointB.transform.position = new Vector3(1, 1.5f, 0);
            pointB.transform.SetParent(DossierPoints.transform);
            points.Insert(1, pointB);

            GameObject pointC = new GameObject("C");
            pointC.transform.position = new Vector3(2, 0, 0);
            pointC.transform.SetParent(DossierPoints.transform);
            points.Insert(2, pointC);
        }

        if (points[2] == null)
        {
            GameObject pointA = new GameObject("A");
            pointA.transform.position = new Vector3(0, 0, 0);
            pointA.transform.SetParent(DossierPoints.transform);
            points[0] = pointA;

            GameObject pointB = new GameObject("B");
            pointB.transform.position = new Vector3(1, 1.5f, 0);
            pointB.transform.SetParent(DossierPoints.transform);
            points[1] = pointB;

            GameObject pointC = new GameObject("C");
            pointC.transform.position = new Vector3(2, 0, 0);
            pointC.transform.SetParent(DossierPoints.transform);
            points[2] = pointC;
        }
    }
    public void GeneratePattoun()
    {
        Clear();
        InstantiatePattoun();
    }
    void InstantiatePattoun()
    {
        for (int i = 0; i < points.Count - 2; i += 2)
        {
            for (int j = index; j < nbPattoun; j++)
            {
                Vector3 _AB = Vector3.Lerp(points[i].transform.position, points[i + 1].transform.position, j / (float)nbPattoun);
                Vector3 _BC = Vector3.Lerp(points[i + 1].transform.position, points[i + 2].transform.position, j / (float)nbPattoun);
                Vector3 posLine = Vector3.Lerp(_AB, _BC, j / (float)nbPattoun);
                Instantiate(prefab, posLine, Quaternion.identity, Dossier_go.transform);
                index++;
            }

            index = 0;
        }
    }
    public void Clear()
    {
        for (int i = Dossier_go.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(Dossier_go.transform.GetChild(i).gameObject);
        }
    }
    public void AddPoint()
    {

            GameObject pointA = new GameObject("Tangente " + points.Count);
            pointA.transform.position = new Vector3(points[points.Count-2].transform.position.x + 1.5f, 1.5f, 0);
            points.Insert(points.Count, pointA);
            pointA.transform.SetParent(DossierPoints.transform);

            GameObject pointB = new GameObject("Destination " + points.Count);
            pointB.transform.position = new Vector3(points[points.Count - 1].transform.position.x + 3, 0, 0);
            pointB.transform.SetParent(DossierPoints.transform);
            points.Insert(points.Count , pointB);
    }

}
