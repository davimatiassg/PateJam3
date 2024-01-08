using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class SectionData : MonoBehaviour
{
    public static SectionData Instance;
    private List<GameObject> sections = new List<GameObject>();
    private List<GameObject> buildings = new List<GameObject>();

    #if UNITY_EDITOR
    void OnEnable()
    {
        if(SectionData.Instance == null){ SectionData.Instance = this; }
        else if(SectionData.Instance != this) { Destroy(this.gameObject);  return; }

        string[] assetNames = AssetDatabase.FindAssets("t:" + typeof(GameObject).Name, new[] { "Assets/Prefabs/Scenary/Sections" });
        sections.Clear();
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var section = AssetDatabase.LoadAssetAtPath<GameObject>(SOpath);
            sections.Add(section);
        }

        assetNames = AssetDatabase.FindAssets("t:" + typeof(GameObject).Name, new[] { "Assets/Prefabs/Scenary/Buildings" });
        buildings.Clear();
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var building = AssetDatabase.LoadAssetAtPath<GameObject>(SOpath);
            buildings.Add(building);
        }
    }
    #endif


    public static GameObject GetRandomSection()
    {
        if(SectionData.Instance.sections.Count == 0) { return null; }
        int i = Random.Range(0, SectionData.Instance.sections.Count);
        return SectionData.Instance.sections[i];
    }

    public static GameObject GetRandomBuilding()
    {
        if(SectionData.Instance.buildings.Count == 0) { return null; }
        int i = Random.Range(0, SectionData.Instance.buildings.Count);
        return SectionData.Instance.buildings[i];
    }
}
