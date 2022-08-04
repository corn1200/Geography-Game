using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax
{
    // 노이즈 최대값/최소값 필드
    public float Min { get; private set; }
    public float Max { get; private set; }

    // 생성자
    public MinMax()
    {
        Min = float.MaxValue;
        Max = float.MinValue;
    }

    // 최대값/최소값 저장 함수
    public void AddValue(float v)
    {
        if (v > Max)
        {
            Max = v;
        }
        if (v < Min)
        {
            Min = v;
        }
    }
}
