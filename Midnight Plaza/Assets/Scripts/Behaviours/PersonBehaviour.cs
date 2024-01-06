using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PersonBehaviour : MonoBehaviour {
    [SerializeField] private float fearRange;
    [SerializeField] public Person personData;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator anim;
    
    private float speed;
    private Transform playerTransform;
    private Vector3 currentDirection;

    private delegate void Moviment();
    private Moviment currentMoviment;
    private bool safe = true;
    public Person PersonData{ 
        get { return personData; } 
        set{
            this.personData = value;
            this.speed = personData.speed;
            if(spriteRenderer != null){ spriteRenderer.sprite = value.sprite; }
        } 
    }

    private void Start() {
        this.PersonData = this.personData; 
        playerTransform = GameObject.FindWithTag("Player").transform;
        currentMoviment = randomMove;
    }

    private void Update() {        
        if(safe && isSeeingPlayer(playerTransform.position, transform.position))
        {
            safe = false;
            spriteRenderer.sprite = personData.worriedSprite;
            anim.Play("PersonWaddle");
            anim.speed = 3;
            currentMoviment = fearedMove;
        }
        currentMoviment?.Invoke();
    }

    private bool isSeeingPlayer(Vector3 p, Vector3 pp)
    {
        return (p.x - pp.x)*(p.x - pp.x) + (p.z - pp.z)*(p.z - pp.z) <= fearRange;
    }

    public void randomMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + currentDirection, speed*Time.deltaTime);
        if(Random.value < 0.995f) {return;} 
        if(currentDirection != Vector3.zero){
            anim.Play("PersonIdle");
            currentDirection = Vector3.zero; 
            return; 
        }
        anim.Play("PersonWaddle");
        Vector3 d = Random.onUnitSphere;
        currentDirection = new Vector3 (d.x, 0, d.z);
        spriteRenderer.flipX = d.x < 0;
    }

    public void fearedMove()
    {
        Vector3 playerPosition = playerTransform.position;
        Vector3 position = transform.position; 
        spriteRenderer.flipX = playerPosition.x > position.x;
        transform.position += (new Vector3(position.x - playerPosition.x, 0, position.z - playerPosition.z)).normalized*speed*Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            GameDataManager.Instance.onCollectProp?.Invoke(PersonData);
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag.Equals("Player"))
        {
            Destroy(this.gameObject);
        }
    }
    
}