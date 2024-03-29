using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class PersonSpawner : MonoBehaviour
{
    [SerializeField] private float countdown = 0;
    [SerializeField] private Transform peopleParent;

    private Transform playerTransform;

    [SerializeField] private GameObject personPrefab;
    [SerializeField] private float minTime = 5f;
    [SerializeField] private float randomTime = 3f;
    List<Person> people = new List<Person>();

    private void Awake()
    {
        Object[] olist = Resources.LoadAll("People", typeof(Person));
        //assetNames = AssetDatabase.FindAssets("t:" + typeof(GameObject).Name, new[] { "Assets/Prefabs/Scenary/Buildings" });
        people.Clear();
        foreach (Object o in olist)
        {
            people.Add((Person)o);
        }
    }

    private void Start() {
        setCountDown();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    private void Update() {
        countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
            setCountDown();
            spawnGroups(playerTransform.position);
        }
    }
    private void setCountDown()
    {
        countdown = minTime + Random.value*randomTime;
    }


    public void spawnGroups(Vector3 position)
    {
        int groupAmmout = (int) (Random.value*4);
        for(int i = 0; i < groupAmmout; i++)
        {
            float angle = Random.value*360f;
            spawnPersonGroup(new Vector3(position.x + Mathf.Sin(angle)*15, 1, position.z + Mathf.Cos(angle)*10));
        }
    }

    public void spawnPersonGroup(Vector3 position)
    {
        int groupSize = (int) (Random.value*6);
        for(int i = 0; i < groupSize; i++)
        {
            Vector2 r = Random.insideUnitCircle*8f;
            spawnPerson(new Vector3(position.x + r.x, 1, position.z + r.y));
        }
    }
    public void spawnPerson(Vector3 position)
    {
        if(people.Count == 0) return;

        GameObject person = GameObject.Instantiate(personPrefab, position, new Quaternion(0, 1, 0, 0));
        person.transform.parent = peopleParent;

        PersonBehaviour personScript = person.GetComponent<PersonBehaviour>();
        personScript.personData = people[(int)(Random.value*(float)people.Count) % people.Count];
    }   

   
}