using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{
    public float spacing = 2f;
    public float density = 0.5f; //0 to 1

    [SerializeField] private GameObject tasukete;
    [SerializeField] private GameObject building;
    [SerializeField] private Transform buildings;

    private SectionData sectionData;
    private int w, h;
    private Transform p1, p2;
    private int[,] grid;
    
    private bool create = true;

    private struct int2 {
        public int w,h;
        public int2(int w, int h) {
            this.w = w;this.h = h;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sectionData = GameObject.FindWithTag("SectionData").GetComponent<SectionData>();

        p1 = transform.Find("p1").transform;
        p2 = transform.Find("p2").transform;

        // calculating amount of buildings allowed horizontally and vertically
        w = (int) (Mathf.Floor((p2.position.x - p1.position.x) / spacing));
        h = (int) (Mathf.Floor((p2.position.z - p1.position.z) / spacing));

        // Generating Grid
        grid = new int[w, h];
        for (int i = 0;i < w;i ++) {for (int j = 0;j < h;j ++) {grid[i, j] = 0;}}
    }

    // Update is called once per frame
    void Update()
    {
        if (create) {
            BuildCity();
            create = false;
        }
    }

    void BuildCity() {

        for (int i = 0;i < w;i ++)
        {
        for (int j = 0;j < h;j ++)
        {
            BuildAt(i, j);
        }
        }

    }

    void BuildAt(int i, int j) {
        if (grid[i, j] >= 0 && Random.Range(0f, 1f) < density) {
            tryToPlaceSection(i, j, getRandomSection());
        }
    }

    void tryToPlaceSection(int i, int j, GameObject sec) {
        int2 sectionArea = getSectionArea(sec);

        if (checkGrid(i, j, sectionArea.w, sectionArea.h)) {
            CreateSection(i, j, sec);
            blockGrid(i, j, sectionArea.w, sectionArea.h);
        }
    }

    // prevents buildings from being built in an area of (x, y) beginning from (i, j)
    void blockGrid(int i, int j, int x, int y) {
        for (int u = 0;u < x;u ++) {
        for (int v = 0;v < y;v ++) {
            if (i + u < w && j + v < h) {grid[i + u, j + v] = -1;}
        }}
    }

    // return whether area of grid is free
    bool checkGrid(int i, int j, int x, int y) {
        for (int u = 0;u < x;u ++) {
        for (int v = 0;v < y;v ++) {
            if (i + u < w && j + v < h && 
                grid[i + u, j + v] < 0) {return false;}
        }}

        return true;
    }

    GameObject getRandomSection() {

        int i = Random.Range(0, sectionData.sections.Count);
        return sectionData.sections[i];
    }

    int2 getSectionArea(GameObject sec) {
        Transform secT = sec.transform;

        Vector2 blockArea = new Vector2(
        secT.Find("p2").position.x - secT.Find("p1").position.x, 
        secT.Find("p2").position.z - secT.Find("p1").position.z);

        return new int2((int) (blockArea.x/spacing), (int) (blockArea.y/spacing));
    }

    // x, z
    void CreateBuilding(int i, int j) {

        Vector3 p1c = p1.position, p2c = p2.position;
        float top = p1c.z, left = p1c.x, bottom = p2c.z, right = p2c.x;

        var b = Instantiate(building, 
            new Vector3(left + (i + 0.5f) * spacing, 0, top + (j + 0.5f) * spacing), Quaternion.identity);

        b.transform.parent = buildings;

    }

    // x, z
    void CreateSection(int i, int j, GameObject sec) {

        Vector3 p1c = p1.position, p2c = p2.position;
        float top = p1c.z, left = p1c.x, bottom = p2c.z, right = p2c.x;

        var b = Instantiate(sec, 
            new Vector3(left + (i + 0f) * spacing, 0, top + (j + 0f) * spacing), Quaternion.identity);

        b.transform.parent = buildings;

    }

    // x, z
    void CreateTasukete(int i, int j) {

        Vector3 p1c = p1.position, p2c = p2.position;
        float top = p1c.z, left = p1c.x, bottom = p2c.z, right = p2c.x;

        var b = Instantiate(tasukete, 
            new Vector3(left + (i + 0f) * spacing, 0, top + (j + 0f) * spacing), Quaternion.identity);

        b.transform.parent = buildings;

    }
}
