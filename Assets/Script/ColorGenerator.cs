using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    // 필드
    ColorSetting setting;
    Texture2D texture;
    const int textureResolution = 50;
    // 바이옴 필터
    NoiseFilterInterface biomeNoiseFilter;

    // 세팅 업데이트
    public void UpdateSettings(ColorSetting setting)
    {
        this.setting = setting;
        // 텍스쳐가 null이거나 텍스쳐 height와 바이옴 개수가 일치하지 않을 시 실행
        if (texture == null || texture.height != setting.biomeColorSettings.biomes.Length)
        {
            // 텍스쳐 초기화
            texture = new Texture2D(textureResolution, setting.biomeColorSettings.biomes.Length, TextureFormat.RGBA32, false);
        }
        // 바이옴 노이즈 필터 초기화
        biomeNoiseFilter = NoiseFilterFactory.CreateNoiseFilter(setting.biomeColorSettings.noiseSettings);
    }

    // 행성 머테리얼 텍스쳐에 노이즈 최대값/최소값 전달
    public void UpdateElevation(MinMax elevationMinMax)
    {
        setting.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    // 바이옴 비율 반환 함수
    public float BiomePercentFromPoint(Vector3 pointOnUnitSphere)
    {
        float heightPercent = (pointOnUnitSphere.y + 1) / 2f;
        heightPercent += (biomeNoiseFilter.Evaluate(pointOnUnitSphere) - setting.biomeColorSettings.noiseOffset) * setting.biomeColorSettings.noiseStrength;
        float biomeIndex = 0;
        int numBiomes = setting.biomeColorSettings.biomes.Length;
        float blendRange = setting.biomeColorSettings.blendAmount / 2f + .001f;

        for (int i = 0; i < numBiomes; i++)
        {
            float distance = heightPercent - setting.biomeColorSettings.biomes[i].startHeight;
            float weight = Mathf.InverseLerp(-blendRange, blendRange, distance);
            biomeIndex *= (1 - weight);
            biomeIndex += i * weight;
        }

        return biomeIndex / Mathf.Max(1, numBiomes - 1);
    }

    // 색상 업데이트
    public void UpdateColor()
    {
        // 텍스쳐 크기만큼 색상 배열 생성
        Color[] colors = new Color[texture.width * texture.height];
        int colorIndex = 0;
        // 바이옴 배열 순회
        foreach (var biome in setting.biomeColorSettings.biomes)
        {
            // 색상 단계 순회
            for (int i = 0; i < textureResolution; i++)
            {
                // 단계별 색상 추출 후 저장
                Color gradientColor = biome.gradient.Evaluate(i / (textureResolution - 1f));
                Color tintColor = biome.tint;
                colors[colorIndex] = gradientColor * (1 - biome.tintPercent) + tintColor * biome.tintPercent;
                colorIndex++;
            }
        }
        // 텍스쳐에 그라디언트 색상 지정 후 행성 머테리얼에 텍스쳐 삽입
        texture.SetPixels(colors);
        texture.Apply();
        setting.planetMaterial.SetTexture("_texture", texture);
    }
}
