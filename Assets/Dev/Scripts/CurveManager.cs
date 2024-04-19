using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CurveManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> points;
    [SerializeField] private GameObject prefab;
    [Range(0.001f, 0.1f)][SerializeField] private float pas = 0.5f;
    [Range(0, 200)] [SerializeField] private int nbPattoun; 
    [SerializeField] private bool ViewCurve; 
    private int index ;
    private GameObject DossierPoints;
    private GameObject Dossier_go; 

    void OnDrawGizmos()
    {
        Init();

        if (ViewCurve)
        {
            DrawCurve();
        }
    }
    void DrawCurve()
    {
        for (int i = 0; i < points.Count - 2; i += 2)
        {
            for (float t = 0; t <= 1; t += pas)
            {
                Vector3 _AB = Vector3.Lerp(points[i].transform.position, points[i + 1].transform.position, t);
                Vector3 _BC = Vector3.Lerp(points[i + 1].transform.position, points[i + 2].transform.position, t);
                Vector3 posFinal = Vector3.Lerp(_AB, _BC, t);
                Vector3 posLineMax = Vector3.Lerp(_AB, _BC, t + pas);

                Gizmos.DrawLine(posFinal, posLineMax);
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
            GameObject pointA = new GameObject("PosInitial");
            pointA.transform.position = new Vector3(0, 0, 0);
            pointA.transform.SetParent(DossierPoints.transform);
            points.Insert(0, pointA);
            pointA.AddComponent<CustomVisualPoint>();

            GameObject pointB = new GameObject("Tangente 1");
            pointB.transform.position = new Vector3(1, 1.5f, 0);
            pointB.transform.SetParent(DossierPoints.transform);
            points.Insert(1, pointB);
            pointB.AddComponent<CustomVisualTangente>();

            GameObject pointC = new GameObject("Destination 1");
            pointC.transform.position = new Vector3(2, 0, 0);
            pointC.transform.SetParent(DossierPoints.transform);
            points.Insert(2, pointC);
            pointC.AddComponent<CustomVisualPoint>();

        }
        // Security
        if (points[2] == null)
        {
            GameObject pointA = new GameObject("PosInitial");
            pointA.transform.position = new Vector3(0, 0, 0);
            pointA.transform.SetParent(DossierPoints.transform);
            points[0] = pointA;

            GameObject pointB = new GameObject("Tangente 1");
            pointB.transform.position = new Vector3(1, 1.5f, 0);
            pointB.transform.SetParent(DossierPoints.transform);
            points[1] = pointB;

            GameObject pointC = new GameObject("Destination 1"); 
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
    public void AddPattoun()
    {
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
    public void ResetCurve()
    {
        for (int i = DossierPoints.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(DossierPoints.transform.GetChild(i).gameObject);
        }
        points.Clear();
    }
    public void AddPoint()
    {

            GameObject pointA = new GameObject("Tangente " + (points.Count - 1));
            pointA.transform.position = new Vector3(points[points.Count-1].transform.position.x + 2f, 1.5f, 0);
            points.Insert(points.Count, pointA);
            pointA.transform.SetParent(DossierPoints.transform);
            pointA.AddComponent<CustomVisualTangente>();

            GameObject pointB = new GameObject("Destination " + (points.Count - 2));
            pointB.transform.position = new Vector3(points[points.Count - 1].transform.position.x + 2.5f, 0, 0);
            pointB.transform.SetParent(DossierPoints.transform);
            points.Insert(points.Count , pointB);
            pointB.AddComponent<CustomVisualPoint>();

    }

    public void SuppPoint()
    {
        DestroyImmediate(points[points.Count - 2].gameObject);
        DestroyImmediate(points[points.Count - 1].gameObject);
        points.RemoveRange(points.Count - 2, 2);

    }

}
