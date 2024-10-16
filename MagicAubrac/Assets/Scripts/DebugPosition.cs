using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPosition : MonoBehaviour
{
    [SerializeField] private float _radius = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
