using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    // 필터 타입 선택 필드
    public enum FilterType { Simple, Rigid };
    public FilterType filterType;

    // 심플/리지드 노이즈 세팅으로 분리, 필터 타입에 따라 선택
    [ConditionalHide("filterType", 0)]
    public SimpleNoiseSettings simpleNoiseSettings;
    [ConditionalHide("filterType", 1)]
    public RigidNoiseSettings rigidNoiseSettings;

    // 심플 노이즈 세팅일 때의 세팅 값
    [System.Serializable]
    public class SimpleNoiseSettings
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

    // 리지드 노이즈 세팅일 때의 세팅 값, 심플 노이즈 세팅을 상속함
    [System.Serializable]
    public class RigidNoiseSettings : SimpleNoiseSettings
    {
        public float weightMultiplier = .8f;
    }
}
