using Spine.Unity;
using UnityEngine;

public static partial class ExtensionMethod_Transform
{
    public static void MoveTo(this Transform my, Transform target, float speed)
    {
        // 이동할 최종 목적지를 지정
        Vector3 dest = target.position;

        // 방향벡터 선언 (z축은 0으로 지정)
        Vector3 dir = dest - my.position;
        dir.Normalize();

        Vector3 pos = my.transform.position;
        pos += dir * speed * Time.deltaTime;
        pos.z = 0f;

        // 내 위치 이동
        my.transform.position = pos;
    }

    public static void MoveTo(this Transform my, Transform target, float speed, SkeletonAnimation skeleton)
    {
        // 이동할 최종 목적지를 지정
        Vector3 dest = target.position;

        // 방향벡터 선언 (z축은 0으로 지정)
        Vector3 dir = dest - my.position;
        dir.Normalize();

        if (dir.x < 0 && skeleton.skeleton.ScaleX == 1f)
            skeleton.skeleton.ScaleX = -1f;
        else if (dir.x > 0 && skeleton.skeleton.ScaleX == -1f)
            skeleton.skeleton.ScaleX = 1f;

        Vector3 pos = my.transform.position;
        pos += dir * speed * Time.deltaTime;
        pos.z = 0f;

        // 내 위치 이동
        my.transform.position = pos;
    }

    public static float GetDistance(this Transform my, Transform target)
    {
        return (my.localPosition - target.localPosition).sqrMagnitude;
    }
}
