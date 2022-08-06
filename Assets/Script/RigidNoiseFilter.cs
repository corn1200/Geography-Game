using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 노이즈 필터 인터페이스 상속
public class RigidNoiseFilter : NoiseFilterInterface
{
    // 필드
    NoiseSettings.RigidNoiseSettings settings;
    Noise noise = new Noise();

    // 생성자
    public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings settings)
    {
        this.settings = settings;
    }

    // 노이즈 반환 함수
    public float Evaluate(Vector3 point)
    {
        // 노이즈 값
        float noiseValue = 0;
        // 노이즈 빈도
        float frequency = settings.baseRoughness;
        // 노이즈 진폭
        float amplitude = 1;
        // 노이즈 가중치
        float weight = 1;

        // 레이어 개수만큼 실행
        for (int i = 0; i < settings.numLayers; i++)
        {
            // 노이즈 값 생성
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.centre));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp01(v * settings.weightMultiplier);

            noiseValue += v * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        // 노이즈 값 리턴
        noiseValue = noiseValue - settings.minValue;
        return noiseValue * settings.strength;
    }
}
