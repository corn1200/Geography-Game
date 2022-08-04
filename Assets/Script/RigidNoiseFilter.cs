using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : NoiseFilterInterface
{
    NoiseSettings.RigidNoiseSettings settings;
    Noise noise = new Noise();

    public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
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
        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}
