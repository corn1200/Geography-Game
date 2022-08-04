using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    // 필드
    ColorSetting setting;
    Texture2D texture;
    const int textureResolution = 50;

    // 세팅 업데이트
    public void UpdateSettings(ColorSetting setting)
    {
        this.setting = setting;
        if(texture == null)
        {
            texture = new Texture2D(textureResolution, 1);
        }
    }

    // 행성 머테리얼 텍스쳐에 노이즈 최대값/최소값 전달
    public void UpdateElevation(MinMax elevationMinMax)
    {
        setting.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    // 색상 업데이트
    public void UpdateColor()
    {
        // 색상 단계만큼 배열 생성
        Color[] colors = new Color[textureResolution];
        // 색상 배열만큼 순회
        for (int i = 0; i < textureResolution; i++)
        {
            // 단계별 색상 추출 후 저장
            colors[i] = setting.gradient.Evaluate(i / (textureResolution - 1f));
        }
        // 텍스쳐에 그라디언트 색상 지정 후 행성 머테리얼에 텍스쳐 삽입
        texture.SetPixels(colors);
        texture.Apply();
        setting.planetMaterial.SetTexture("_texture", texture);
    }
}
