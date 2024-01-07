using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PersonBehaviour : MonoBehaviour, IHittable, IGrabbable {
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
        currentMoviment = RandomMove;
    }

    private void Update() {       
        if(isFarFromPlayer(playerTransform.position, transform.position)) { Destroy(this.gameObject); }
        if(safe && isSeeingPlayer(playerTransform.position, transform.position))
        {
            safe = false;
            spriteRenderer.sprite = personData.worriedSprite;
            anim.Play("PersonWaddle");
            anim.speed = 3;
            currentMoviment = FearedMove;
        }
        currentMoviment?.Invoke();
    }

    private bool isSeeingPlayer(Vector3 p, Vector3 pp)
    {
        return (p.x - pp.x)*(p.x - pp.x) + (p.z - pp.z)*(p.z - pp.z) <= fearRange;
    }

    private bool isFarFromPlayer(Vector3 p, Vector3 pp)
    {
        return (p.x - pp.x)*(p.x - pp.x) + (p.z - pp.z)*(p.z - pp.z) > 625f;
    }

    public void RandomMove()
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
        spriteRenderer.flipX = d.x > 0;
    }

    public void FearedMove()
    {
        Vector3 playerPosition = playerTransform.position;
        Vector3 position = transform.position; 
        spriteRenderer.flipX = playerPosition.x < position.x;
        transform.position += (new Vector3(position.x - playerPosition.x, 0, position.z - playerPosition.z)).normalized*speed*Time.deltaTime;
    }
    private void GrabedMove(){ return; }
    

    public void TakeDmg(float dmg, Vector3 force, GameObject hitter)
    {
        string otherTag = hitter.tag;
        if(otherTag.Equals("Player"))
        {
            GameDataManager.Instance.onCollectProp?.Invoke(this);
            GameDataManager.Instance.onGainScore?.Invoke(PersonData);
            Destroy(this.gameObject);
        }
        else if(otherTag.Equals("Enemy"))
        {
            Destroy(this.gameObject);
        }   
    }
    public void GetGrabbed()
    {
        currentMoviment = GrabedMove;
    }

    public void OnTriggerEnter(Collider other) 
    {  
        TakeDmg(0, Vector3.zero, other.gameObject);
    }
    
    
}
