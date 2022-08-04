using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    ColorSetting setting;
    Texture2D texture;
    const int textureResolution = 50;

    public void UpdateSettings(ColorSetting setting)
    {
        this.setting = setting;
        if(texture == null)
        {
            texture = new Texture2D(textureResolution, 1);
        }
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        setting.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public void UpdateColor()
    {
        Color[] colors = new Color[textureResolution];
        for (int i = 0; i < textureResolution; i++)
        {
            colors[i] = setting.gradient.Evaluate(i / (textureResolution - 1f));
        }
        texture.SetPixels(colors);
        texture.Apply();
        setting.planetMaterial.SetTexture("_texture", texture);
    }
}
