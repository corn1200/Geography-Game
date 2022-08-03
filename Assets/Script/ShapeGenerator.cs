using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    ShapeSettings settings;

    // 생성자
    public ShapeGenerator(ShapeSettings settings)
    {
        this.settings = settings;
    }

    // 지정한 크기 값만큼 Planet 크기 키우기
    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        return pointOnUnitSphere * settings.planetRadius;
    }
}
