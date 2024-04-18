using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;


public class Curve : MonoBehaviour
{


    [SerializeField][Range(0,100)] private int nbPattoun;
    [SerializeField] private GameObject prefab;
    [SerializeField] private bool timeRealCurve;

    public GameObject[] pattoun;
    private Transform A_go;
    private Transform B_go;
    private Transform C_go;
    void OnDrawGizmos()
    {
        for (int i = 0; i < 100; i++)
        {
            Gizmos.color = Color.white;

            InitTransform(ref A_go, Vector3.zero , "A");
            InitTransform(ref B_go, new Vector3(0.5f, 0.5f, 0), "B");
            InitTransform(ref C_go, new Vector3(1, 0, 0), "C");

            Gizmos.DrawSphere(A_go.position, 0.05f);
            Gizmos.DrawSphere(B_go.position, 0.05f);
            Gizmos.DrawSphere(C_go.position, 0.05f);

            Vector3 _AB = Vector3.Lerp(A_go.position, B_go.position, i / (float)100);
            Vector3 _BC = Vector3.Lerp(B_go.position, C_go.position, i / (float)100);
            Vector3 posFinal = Vector3.Lerp(_AB, _BC, i / (float)100);

            if (i + 1 < 100)
            {
                Vector3 posLineMax = Vector3.Lerp(_AB, _BC, i + 1 / (float)100);
                Gizmos.DrawLine(posFinal, posLineMax);
            }


            PattounOnCurve();
        }
    }
    void InitTransform(ref Transform t ,Vector3 pos , string name)
    {
        if(t == null)
        {
            t = new GameObject(name).transform;
            t.position = pos;
        }
    }

    public void GeneratePattoun()
    {
        if(pattoun != null)
        {
            Clear(); 
        }

        pattoun = new GameObject[nbPattoun];

        PattounOnCurve(true);
    }
    public void AddGeneratePattoun()
    {
       // pattoun = new GameObject[nbPattoun + nbPattoun];

       // PattounOnCurve(true);
    }
    public void Clear()
    {
        for (int i = 0; i < pattoun.Length; i++)
        {
            DestroyImmediate(pattoun[i]);
            pattoun[i] = null;
        }

        pattoun = new GameObject[0];

    }
    void PattounOnCurve(bool instantiate = false)
    {
        for (int i = 0; i < pattoun.Length; i++)
        {
            Vector3 _AB = Vector3.Lerp(A_go.position, B_go.position, i / (float)nbPattoun);
            Vector3 _BC = Vector3.Lerp(B_go.position, C_go.position, i / (float)nbPattoun);
            Vector3 posLine = Vector3.Lerp(_AB, _BC, i / (float)nbPattoun);


            if(instantiate == true) pattoun[i] = Instantiate(prefab, posLine, Quaternion.identity, transform);
            
            Gizmos.color = Color.red;

            if (pattoun[i] != null && timeRealCurve) pattoun[i].transform.position = posLine;
        }
    }
}
