using UnityEngine;
using System.Collections;

public class TransformPosDeConversa
{
    public static Transform tDeConversa(Vector3 dir,Transform transform)
    {
        Transform T = new GameObject().transform;
        T.position = transform.position + 0.5f * dir + 2 * Vector3.up;
        Vector3 cross = Vector3.Cross(Vector3.up, dir);
        if (Vector3.Dot(cross, -Vector3.forward) < 0)
            cross *= -1;
        T.rotation = Quaternion.LookRotation(cross);

        return T;
    }

    public static Transform MeAjude(Transform transform)
    {
        Vector3 dir = GameController.g.Manager.transform.position - transform.position;
        dir = Vector3.ProjectOnPlane(dir, Vector3.up);
        transform.rotation = Quaternion.LookRotation(dir);

        return tDeConversa(dir,transform);
    }
}
