using System;
using UnityEngine;

public class BattleCalc
{
    public static int GetStatus(int grade, int level, float origin, float levelScale, float gradeScale)
    {
        float value = origin + ((level - 1) * (levelScale + ((grade - 1) * gradeScale)));
        return (int)Math.Ceiling(value);
    }

    public static float GetDistance(Transform a, Transform b)
    {
        return (a.localPosition - b.localPosition).sqrMagnitude;
    }

    public static void MoveTo(Transform my, Transform target, float speed, Spine.Unity.SkeletonAnimation skeleton)
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
}
