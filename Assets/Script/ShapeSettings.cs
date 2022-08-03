using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    // 행성 크기 값
    public float planetRadius = 1;
    // 노이즈 레이어 배열
    public NoiseLayer[] noiseLayers;

    // 노이즈 레이어 클래스
    [System.Serializable]
    public class NoiseLayer
    {
        // 레이어를 노출 시킬지 여부
        public bool enabled = true;
        public bool useFirstLayerAsMask;
        // 노이즈 세팅 값
        public NoiseSettings noiseSettings;
    }
}
