using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColorSetting : ScriptableObject
{
    // 행성 머테리얼
    public Material planetMaterial;
    // 바이옴 색상 세팅 필드
    public BiomeColorSettings biomeColorSettings;

    // 바이옴 색상 세팅 클래스
    [System.Serializable]
    public class BiomeColorSettings
    {
        // 바이옴 배열
        public Biome[] biomes;
        // 바이옴의 노이즈 설정
        public NoiseSettings noiseSettings;
        public float noiseOffset;
        public float noiseStrength;
        [Range(0, 1)]
        public float blendAmount;

        [System.Serializable]
        // 바이옴 클래스
        public class Biome
        {
            // 그라디언트 색상과 바이옴 영역 표시 색상
            public Gradient gradient;
            public Color tint;
            // 바이옴 영역 시작 높이와 바이옴 영역 표시 색상 투명도
            [Range(0, 1)]
            public float startHeight;
            [Range(0, 1)]
            public float tintPercent;
        }
    }
}
