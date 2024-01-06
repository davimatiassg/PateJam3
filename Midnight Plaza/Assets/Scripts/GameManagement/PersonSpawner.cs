using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersonSpawner  
{
    private float countdown = 0;

    private Transform playerTransform;
    [SerializeField] private float minTime = 5f;
    [SerializeField] private float randomTime = 3f;
    Person[] people = Resources.LoadAll<Person>("Assets/Entities/People");

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

    public void spawnGroups(Vector3 position)
    {
        int groupAmmout = (int) (Random.value*4);
        for(int i = 0; i < groupAmmout; i++)
        {
            Vector2 r = Random.insideUnitCircle*15;
            spawnPersonGroup(new Vector3(position.x + r.x, 1, position.z + r.y));
        }
    }

    public void spawnPersonGroup(Vector3 position)
    {
        int groupSize = (int) (Random.value*6);
        for(int i = 0; i < groupSize; i++)
        {
            Vector2 r = Random.insideUnitCircle*2f;
            spawnPerson(new Vector3(position.x + r.x, 1, position.z + r.y));
        }
    }
    public void spawnPerson(Vector3 position)
    {
        //GameObject.Instantiate();
    }   

    private void setCountDown()
    {
        countdown = minTime + Random.value*randomTime;
    }

}