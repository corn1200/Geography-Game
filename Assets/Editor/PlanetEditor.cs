using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    Planet planet;
    Editor shapeEditor;
    Editor colorEditor;

    // GUI Inspector를 오버라이드 하여 사용
    public override void OnInspectorGUI()
    {
        // 변환된 값이 있을 경우 행성 재생성
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if(check.changed)
            {
                planet.GeneratePlanet();
            }
        }

        // Generate Planet 버튼 클릭 시 행성 재생성
        if (GUILayout.Button("Generate Planet"))
        {
            planet.GeneratePlanet();
        }

        // 세팅 창 폴드
        DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingsFoldout, ref shapeEditor);
        DrawSettingsEditor(planet.colorSetting, planet.OnColorSettingsUpdated, ref planet.colorSettingFoldout, ref colorEditor);
    }

    // 폴딩 되는 세팅 창 만드는 함수
    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if(settings != null)
        {
            // 현재 폴드 상태를 확인
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                // 폴드가 펼쳐졌을 경우 실행
                if (foldout)
                {
                    // 개체의 이전 편집기를 반환하여 새 편집기를 선택
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    // 해당 세팅 값이 변경되면 파라미터로 받은 업데이트 함수 실행
                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }
    }

    // 활성화 될 때마다 현재 타겟 재지정
    private void OnEnable()
    {
        planet = (Planet)target;
    }
}
