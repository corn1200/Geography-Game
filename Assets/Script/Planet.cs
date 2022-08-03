using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // 값 범위 지정
    [Range(2, 256)]
    public int resolution = 10;

    // 메쉬필터 생성 후 배열에 오브젝트 저장, 인스펙터 창에서 수정은 못하게 설정
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    // 스크립트 또는 인스펙터 상에서 변수의 값이 변경될 때 호출
    private void OnValidate()
    {
        // 초기화 & 메쉬 지정
        Initialize();
        GenerateMesh();
    }

    void Initialize()
    {
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
            terrainFaces[i] = new TerrainFace(meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    // 메쉬 생성 함수
    void GenerateMesh()
    {
        // 저장된 지층 순회 접근
        foreach (TerrainFace face in terrainFaces)
        {
            // 지층면의 메쉬 지정
            face.ConstructMesh();
        }
    }
}