using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 노이즈 필터 인터페이스 상속
public class SimpleNoiseFilter : NoiseFilterInterface
{
    // 필드
    NoiseSettings.SimpleNoiseSettings settings;
    Noise noise = new Noise();

    // 생성자
    public SimpleNoiseFilter(NoiseSettings.SimpleNoiseSettings settings)
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

        // 레이어 개수만큼 실행
        for (int i = 0; i < settings.numLayers; i++)
        {
            // 노이즈 값 생성
            float v = noise.Evaluate(point * frequency + settings.centre);
            noiseValue += (v + 1) * .5f * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        // 노이즈 값 리턴
        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}
