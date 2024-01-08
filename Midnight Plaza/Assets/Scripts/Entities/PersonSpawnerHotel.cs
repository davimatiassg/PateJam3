using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class PersonSpawnerHotel : MonoBehaviour
{

    [SerializeField] private bool isRandom = false;
    [SerializeField] private float personScale = 0.4f;
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private Transform peopleParent;
    List<Person> people = new List<Person>();

    void Awake() 
    {
        Object[] olist = Resources.LoadAll("People", typeof(Person));
        //assetNames = AssetDatabase.FindAssets("t:" + typeof(GameObject).Name, new[] { "Assets/Prefabs/Scenary/Buildings" });
        people.Clear();
        foreach (Object o in olist)
        {
            people.Add((Person)o);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPersonGroup(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnPersonGroup(Vector3 position)
    {
        int n = isRandom ? 6 : DataTransfer.people.Count;

        for(int i = 0; i < n; i++)
        {
            Vector2 r = Random.insideUnitCircle*1f;
            spawnPerson(new Vector3(position.x + r.x, position.y, position.z + r.y), i);
        }
    }
    public void spawnPerson(Vector3 position, int ind)
    {
        if(people.Count == 0) return;

        GameObject person = GameObject.Instantiate(personPrefab, position, new Quaternion(0, 1, 0, 0));
        person.transform.parent = peopleParent;
        person.transform.localScale = new Vector3(personScale, personScale, personScale);

        person.GetComponent<Collider>().isTrigger = false;
        var rb = person.AddComponent<Rigidbody>();
        rb.freezeRotation = true;

        PersonBehaviour personScript = person.GetComponent<PersonBehaviour>();
        personScript.personData = isRandom ? people[(int)(Random.value*(float)people.Count) % people.Count] : DataTransfer.people[ind];
    }  
}
