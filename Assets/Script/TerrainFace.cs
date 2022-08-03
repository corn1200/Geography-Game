using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    // 생성자
    public TerrainFace(Mesh mesh, int resolution, Vector3 localUp)
    {
        // 필드 초기화
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        // 각 포지션에 맞는 벡터값과 포지션 벡터의 외적을 저장
        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    // 메쉬 지정
    public void ConstructMesh()
    {
        // 총 정점 개수만큼 벡터 배열 생성
        Vector3[] vertices = new Vector3[resolution * resolution];
        // 점을 잇는 정점 총 개수 * 면
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triangleIndex = 0;

        // x, y 범위만큼 반복
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                // 각 정점을 차례로 순회하고 다음 행 순서로 넘어가도록 함
                int i = x + y * resolution;
                // 각 점의 위치를 계산하고 배열에 삽입
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                // 벡터값을 정규화하여 점들이 원을 이루도록함
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = pointOnUnitSphere;

                // 마지막 좌표 외 전부 실행
                if (x != resolution - 1 && y != resolution - 1)
                {
                    // 삼각형들의 위치, 순서 지정
                    triangles[triangleIndex] = i;
                    triangles[triangleIndex + 1] = i + resolution + 1;
                    triangles[triangleIndex + 2] = i + resolution;

                    triangles[triangleIndex + 3] = i;
                    triangles[triangleIndex + 4] = i + 1;
                    triangles[triangleIndex + 5] = i + resolution + 1;
                    triangleIndex += 6;
                }
            }
        }
        // 기존 메쉬 내용을 지우고 정점과 삼각형을 초기화해준다
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        // 메쉬끼리 이어준다
        mesh.RecalculateNormals();
    }
}