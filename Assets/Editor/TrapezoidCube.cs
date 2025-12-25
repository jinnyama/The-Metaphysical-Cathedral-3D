using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TrapezoidCube : MonoBehaviour
{
    [Header("台形サイズ")]
    public float topWidth = 0.5f;
    public float bottomWidth = 1f;
    public float height = 1f;
    public float depth = 1f;

    // EditorやPlayモードで呼べる
    public void ApplyTrapezoid()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf.sharedMesh == null) return; // 元Meshがない場合は処理しない

        // 元のMeshをコピー
        Mesh mesh = Instantiate(mf.sharedMesh);

        // 台形用の頂点に置き換え
        Vector3[] vertices = mesh.vertices;
        float topWidth = 0.5f;
        float bottomWidth = 1f;
        float height = 1f;
        float depth = 1f;

        float hb = bottomWidth / 2f;
        float ht = topWidth / 2f;
        float hd = depth / 2f;

        // 8頂点を台形の形に調整
        vertices[0] = new Vector3(-hb, 0, -hd);
        vertices[1] = new Vector3(hb, 0, -hd);
        vertices[2] = new Vector3(hb, 0, hd);
        vertices[3] = new Vector3(-hb, 0, hd);
        vertices[4] = new Vector3(-ht, height, -hd);
        vertices[5] = new Vector3(ht, height, -hd);
        vertices[6] = new Vector3(ht, height, hd);
        vertices[7] = new Vector3(-ht, height, hd);

        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        mf.sharedMesh = mesh; // コピーをMeshFilterにセット
    }


    private Mesh CreateTrapezoidMesh()
    {
        Mesh mesh = new Mesh();

        float hb = bottomWidth / 2f;
        float ht = topWidth / 2f;
        float hd = depth / 2f;

        Vector3[] vertices = new Vector3[8];
        // 下面
        vertices[0] = new Vector3(-hb, 0, -hd);
        vertices[1] = new Vector3(hb, 0, -hd);
        vertices[2] = new Vector3(hb, 0, hd);
        vertices[3] = new Vector3(-hb, 0, hd);
        // 上面
        vertices[4] = new Vector3(-ht, height, -hd);
        vertices[5] = new Vector3(ht, height, -hd);
        vertices[6] = new Vector3(ht, height, hd);
        vertices[7] = new Vector3(-ht, height, hd);

        int[] triangles = new int[]
        {
            0,2,1, 0,3,2,       // 底面
            4,5,6, 4,6,7,       // 上面
            0,1,5, 0,5,4,       // 側面1
            1,2,6, 1,6,5,       // 側面2
            2,3,7, 2,7,6,       // 側面3
            3,0,4, 3,4,7        // 側面4
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TrapezoidCube))]
    public class TrapezoidCubeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            TrapezoidCube tc = (TrapezoidCube)target;

            if (GUILayout.Button("台形に変形"))
            {
                tc.ApplyTrapezoid();
                EditorUtility.SetDirty(tc.GetComponent<MeshFilter>());
            }
        }
    }
#endif
}
