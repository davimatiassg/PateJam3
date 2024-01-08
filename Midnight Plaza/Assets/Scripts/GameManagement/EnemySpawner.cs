using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float countdown = 0;
    [SerializeField] private int groupSize = 2;
    [SerializeField] private int groupAmmount = 1;
    [SerializeField] private float groupArea = 8f;

    private Transform playerTransform;

    [SerializeField] private Transform enemiesParent;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float minTime = 5f;
    [SerializeField] private float randomTime = 3f;
    List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        Object[] olist = Resources.LoadAll("Enemies", typeof(Enemy));
        //assetNames = AssetDatabase.FindAssets("t:" + typeof(GameObject).Name, new[] { "Assets/Prefabs/Scenary/Buildings" });
        enemies.Clear();
        foreach (Object o in olist)
        {
            enemies.Add((Enemy)o);
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
        int groupAmmount_ = (int) (Random.value * (1 + groupAmmount));
        for(int i = 0; i < groupAmmount_; i++)
        {
            float angle = Random.value*360f;
            spawnEnemyGroup(new Vector3(position.x + Mathf.Sin(angle)*15, 1, position.z + Mathf.Cos(angle)*10));
        }
    }

    public void spawnEnemyGroup(Vector3 position)
    {
        int groupSize_ = (int) (Random.value * (1 + groupSize));
        for(int i = 0; i < groupSize_; i++)
        {
            Vector2 r = Random.insideUnitCircle * groupArea;
            spawnEnemy(new Vector3(position.x + r.x, 1, position.z + r.y));
        }
    }

    // Creating instance
    public void spawnEnemy(Vector3 position)
    {
        if(enemies.Count == 0) return;

        var enemy = GameObject.Instantiate(enemyPrefab, position, new Quaternion(0, 1, 0, 0));
        enemy.transform.parent = enemiesParent;

        EnemyBehaviour enemyScript = enemy.GetComponent<EnemyBehaviour>();
        enemyScript.enemyData = enemies[(int)(Random.value*(float)enemies.Count) % enemies.Count];
    }
}
