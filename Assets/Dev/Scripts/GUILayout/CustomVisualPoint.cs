using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomVisualPoint : MonoBehaviour
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
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
    void DrawSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }

}
