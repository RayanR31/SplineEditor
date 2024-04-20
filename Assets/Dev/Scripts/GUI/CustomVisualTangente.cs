using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomVisualTangente : MonoBehaviour
{
    void OnDrawGizmos()
    {
        DrawWire();
    }
    private void OnDrawGizmosSelected()
    {
        DrawSelected();
    }
    void DrawWire()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawCube(transform.position, new Vector3(0.2f, 0.2f, 0.2f));
    }
    void DrawSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(0.2f,0.2f,0.2f));
    }
}
