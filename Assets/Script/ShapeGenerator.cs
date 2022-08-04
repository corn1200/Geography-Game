using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSettings settings;
    // 여러 클래스의 노이즈 필터를 만들기 위해 노이즈 필터 인터페이스로 대체
    NoiseFilterInterface[] noiseFilter;
    // 노이즈 최대/최소값
    public MinMax elevationMinMax;

    // 세팅 업데이트 함수
    public void UpdateSettings(ShapeSettings settings)
    {
        this.settings = settings;
        // 노이즈 레이어 개수만큼 노이즈 필터 배열 생성
        noiseFilter = new NoiseFilterInterface[settings.noiseLayers.Length];
        // 노이즈 필터를 노이즈 레이어의 세팅 값으로 초기화
        for (int i = 0; i < noiseFilter.Length; i++)
        {
            // 노이즈 레이어에서 선택된 노이즈 분류에 따라 노이즈 필터 생성
            noiseFilter[i] = NoiseFilterFactory.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
        // 노이즈 최대/최소값 초기화
        elevationMinMax = new MinMax();
    }

    // 지정한 크기 값만큼 Planet 크기 키우기
    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = 0;
        float elevation = 0;

        // 노이즈 필터가 한 개 이상 있을 경우 실행
        if (noiseFilter.Length > 0)
        {
            // 생성된 노이즈 저장
            firstLayerValue = noiseFilter[0].Evaluate(pointOnUnitSphere);
            // 노이즈 레이어 사용 체크 시 실행
            if (settings.noiseLayers[0].enabled)
            {
                // 노이즈 값 저장
                elevation = firstLayerValue;
            }
        }

        // 노이즈 필터 개수만큼 반복
        for (int i = 0; i < noiseFilter.Length; i++)
        {
            // 노이즈 레이어 사용 체크 시 실행
            if (settings.noiseLayers[i].enabled)
            {
                // useFirstLayerAsMask 사용 체크 시 첫번째 레이어 노이즈 값을 다음 노이즈에 곱하여 적용한다
                float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
                elevation += noiseFilter[i].Evaluate(pointOnUnitSphere) * mask;
            }
        }
        // 노이즈 최대/최소값에 계산한 노이즈 값 저장
        elevation = settings.planetRadius * (1 + elevation);
        elevationMinMax.AddValue(elevation);
        return pointOnUnitSphere * elevation;
    }
}
