using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // 값 범위 지정
    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    // 렌더링할 메쉬 선택 방향
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;

    public ShapeSettings shapeSettings;
    public ColorSetting colorSetting;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingFoldout;

    ShapeGenerator shapeGenerator;

    // 메쉬필터 생성 후 배열에 오브젝트 저장, 인스펙터 창에서 수정은 못하게 설정
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    // 스크립트 또는 인스펙터 상에서 변수의 값이 변경될 때 호출
    private void OnValidate()
    {
        GeneratePlanet();
    }

    void Initialize()
    {
        shapeGenerator = new ShapeGenerator(shapeSettings);

        // 메쉬필터가 없을 경우 새로 생성
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        // 지층면 생성
        terrainFaces = new TerrainFace[6];

        // 정육면체 좌표 배열 생성
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        // 6면의 메쉬에 접근
        for (int i = 0; i < 6; i++)
        {
            // 메쉬가 없을 시 실행
            if (meshFilters[i] == null)
            {
                // 새로운 메쉬 오브젝트 생성
                GameObject meshObj = new GameObject("mesh");
                // 생성한 메쉬 오브젝트의 부모를 Planet 오브젝트로 지정
                meshObj.transform.parent = transform;

                // 스탠다드 머테리얼을 추가
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                // 메쉬 배열에 생성한 메쉬 오브젝트를 추가
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            // 새 지층면을 메쉬와 함께 생성
            // 현재 좌표 범위, 6면 중 한 방향을 지정
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            // 모든 방향 렌더링이거나, 선택한 렌더링 방향일 경우에만 오브젝트 활성화
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
    }

    // 행성 재생성
    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    // 색상 값이 변경되면 행성을 재생성하고 색상을 재지정한다
    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    // 색상 값이 변경되면 행성을 재생성하고 색상을 재지정한다
    public void OnColorSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColors();
        }
    }

    // 메쉬 생성 함수
    void GenerateMesh()
    {
        // 6면 순회
        for(int i = 0; i < 6; i++)
        {
            // 오브젝트 활성화되어 있을 경우에만 메쉬 생성
            if(meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }
    }

    // 메쉬 색상 지정 함수
    void GenerateColors()
    {
        foreach (MeshFilter m in meshFilters)
        {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = colorSetting.planetColor;
        }
    }
}