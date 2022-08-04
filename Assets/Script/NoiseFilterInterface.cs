using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 노이즈 필터 다형성 지원을 위한 노이즈 필터 인터페이스
public interface NoiseFilterInterface
{
    float Evaluate(Vector3 point);
}
