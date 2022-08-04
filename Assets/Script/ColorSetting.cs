using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColorSetting : ScriptableObject
{
    // 그라디언트 색상 값
    public Gradient gradient;
    // 행성 머테리얼
    public Material planetMaterial;
}
