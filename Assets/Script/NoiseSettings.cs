using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    // 노이즈 설정 값들
    // 노이즈 강도
    public float strength = 1;
    // 노이즈 레이어(층, 단계) 개수
    [Range(1, 8)]
    public int numLayers = 1;

    public float baseRoughness = 1;
    public float roughness = 2;
    public float persistence = .5f;
    public Vector3 centre;
    public float minValue;
}
