using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCastDebugger : MonoBehaviour
{
    public Vector3 origin = Vector3.zero;
    public float radius = 0.5f;
    public float maxDistance = 2f;
    public Vector3 direction = Vector3.forward;
    public Color color = Color.green;

    private void OnDrawGizmos()
    {
        DrawSphereCastGizmo(origin, direction.normalized, radius, maxDistance, color);
    }

    public static void DrawSphereCastGizmo(Vector3 origin, Vector3 direction, float radius, float maxDistance, Color color)
    {
        Gizmos.color = color;

        Vector3 endPoint = origin + direction * maxDistance;

        // 시작 지점 구
        Gizmos.DrawWireSphere(origin, radius);

        // 끝 지점 구
        Gizmos.DrawWireSphere(endPoint, radius);

        // 중심선
        Gizmos.DrawLine(origin, endPoint);

        // 양쪽 바깥쪽 테두리를 그려서 원통처럼 표현
        Vector3[] offsets = new Vector3[]
        {
            Vector3.right,
            Vector3.left,
            Vector3.up,
            Vector3.down,
            Vector3.forward,
            Vector3.back
        };

        foreach (var offset in offsets)
        {
            Vector3 from = origin + offset * radius;
            Vector3 to = endPoint + offset * radius;
            Gizmos.DrawLine(from, to);
        }
    }
}
