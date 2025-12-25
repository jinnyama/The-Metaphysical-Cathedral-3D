using UnityEngine;
using UnityEditor;

public class PrefabPaletteWindow : EditorWindow
{
    private GameObject[] prefabs;
    private Vector2 scrollPos;

    [MenuItem("GameObject/Prefab Palette")]
    public static void ShowWindow()
    {
        GetWindow<PrefabPaletteWindow>("Prefab Palette");
    }

    private void OnEnable()
    {
        LoadPrefabs();
    }

    void LoadPrefabs()
    {
        // 読み込みたいフォルダを指定
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Models" });
        prefabs = new GameObject[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            prefabs[i] = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Reload Prefabs"))
        {
            LoadPrefabs();
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        int columns = 4; // 横並びの数
        int count = 0;

        EditorGUILayout.BeginHorizontal();
        foreach (var prefab in prefabs)
        {
            if (prefab == null) continue;

            Texture2D preview = AssetPreview.GetAssetPreview(prefab);
            if (preview == null)
            {
                preview = AssetPreview.GetMiniThumbnail(prefab);
            }

            if (GUILayout.Button(preview, GUILayout.Width(80), GUILayout.Height(80)))
            {
                PlacePrefab(prefab);
            }

            count++;
            if (count % columns == 0)
            {
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndScrollView();
    }

    void PlacePrefab(GameObject prefab)
    {
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        // 選択オブジェクトの子にする or シーンの原点に置く
        instance.transform.position = Vector3.zero;
        //if (Selection.activeTransform != null)
        //{
        //    instance.transform.SetParent(Selection.activeTransform, false);
        //}
        //else
        //{
        //    instance.transform.position = Vector3.zero;
        //}

        Undo.RegisterCreatedObjectUndo(instance, "Place Prefab");
        Selection.activeObject = instance;
    }
}
