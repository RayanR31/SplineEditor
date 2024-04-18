using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveManager : MonoBehaviour
{
    public GameObject A_go;
    public GameObject B_go;
    public GameObject C_go;

    private Vector3 A;
    private Vector3 B;
    private Vector3 C;

    private GameObject[] pattoun;
    public int nbPattoun;
    public GameObject prefab;

    void Start()
    {
        pattoun = new GameObject[nbPattoun];

        for (int i = 0; i < pattoun.Length; i++)
        {
            pattoun[i] = Instantiate(prefab, Vector3.one * 10000f, Quaternion.identity,transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pattoun.Length; i++)
        {
            Vector3 _AB = Vector3.Lerp(A, B, i / (float)nbPattoun);
            Vector3 _BC = Vector3.Lerp(B, C, i / (float)nbPattoun);
            Vector3 posLine = Vector3.Lerp(_AB, _BC, i / (float)nbPattoun);
            pattoun[i].transform.position = posLine; 
        }
    }
    Vector3 posLineObjectif;
    void OnDrawGizmos()
    {
        for (int i = 0; i < 100; i++)
        {
            Gizmos.color = Color.white;

            A = A_go.transform.position;
            B = B_go.transform.position;
            C = C_go.transform.position;

            Vector3 _AB = Vector3.Lerp(A, B, i / (float)100);
            Vector3 _BC = Vector3.Lerp(B, C, i / (float)100);
            Vector3 posLine = Vector3.Lerp(_AB, _BC, i / (float)100);

            if (i + 1 < 100)
            {
                posLineObjectif = Vector3.Lerp(_AB, _BC, i + 1 / (float)100);
            }

            Gizmos.DrawLine(posLine, posLineObjectif);
        }
    }
    void OnGUI()
    {
        GUI.backgroundColor = Color.yellow;
        GUI.Button(new Rect(45, 45, 100, 100), "A button");
    }
}
