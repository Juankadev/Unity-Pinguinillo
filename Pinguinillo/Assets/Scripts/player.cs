using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    bool powerup;
    [SerializeField] private AudioClip jumpclip,deathclip,winclip;
    private Color colorinicial;
    private Vector3 posinicial,local;
    private AudioSource audsrc;
    [SerializeField] public AudioClip clipsalto,clipcollect,clipslime;
    private Rigidbody2D rb;
    float velocidadInput;
    public float velocidad = 5;
    public float fuerza = 2;
    bool onfloor;
    Vector2 actualizarvl;
    Animator anim;
    SpriteRenderer render;
     public LayerMask capaSuelo;
     private BoxCollider2D boxCollider;
     bool perdio;
    bool tocapuerta;
    bool moe;
    public ParticleSystem nieveparticle;
    float tamaño;
    private TrailRenderer tr;


    [Header("Dash")]
    [SerializeField] private float velocidadDash;
    [SerializeField] private float tiempoDash;
    [SerializeField] private float cooldown;
    private float gravedadInicial;
    private bool puedeHacerDash=true;
    private bool sePuedeMover=true;
    bool isDashing;

    void Awake() {
        posinicial = transform.position;
    }
    void Start()
    {
        tamaño=9f;
        perdio=false;
        powerup=false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        colorinicial=render.color;
        boxCollider = GetComponent<BoxCollider2D>();
        audsrc = GetComponent<AudioSource>();
        local = transform.localScale;
        gravedadInicial = rb.gravityScale;
        tr=GetComponent<TrailRenderer>();
    }


    void Update()
    {
        saltar();
    }


    private void FixedUpdate() {
        if(perdio==false){
            if(sePuedeMover){ // si no esta dasheando, puede moverse
                moverse();
            }


            if(Input.GetKeyDown(KeyCode.Z) && puedeHacerDash){
                StartCoroutine(Dash());
            }
        }
    }


    IEnumerator Dash(){
        sePuedeMover=false;
        puedeHacerDash=false;
        rb.gravityScale = 0;
        tr.emitting = true;
        anim.SetTrigger("Dash");
        rb.velocity = new Vector2(velocidadDash * transform.localScale.x, 0);
        yield return new WaitForSeconds(tiempoDash);
        sePuedeMover=true;
        rb.gravityScale = gravedadInicial;
        tr.emitting = false;
        anim.SetTrigger("NoDash");
        yield return new WaitForSeconds(cooldown);
        puedeHacerDash=true;
    }



    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    private void saltar(){
        if(Input.GetKeyDown(KeyCode.UpArrow) && EstaEnSuelo()){
            rb.AddForce(Vector2.up * fuerza, ForceMode2D.Impulse);
            //onfloor=false;
            AudioManager.Instance.PlaySFX(jumpclip);
        }
    }

    private void moverse(){
            velocidadInput = Input.GetAxisRaw("Horizontal");
            if(velocidadInput < 0){
                flip(-tamaño);
            }
            else if (velocidadInput > 0){
                flip(tamaño);
            }
            rb.velocity = new Vector2(velocidadInput * velocidad,rb.velocity.y);
            anim.SetFloat("Running",Mathf.Abs(velocidadInput));
    }
    

    private void flip(float tam){
        local.x = tam;
        transform.localScale = local;
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.tag == "slime"){

            if(powerup){
                Destroy(other.gameObject);          
                audsrc.PlayOneShot(clipcollect);
            }
            else{
                perdio=true;
                render.color= Color.black;
                Invoke("reset",2f);
            }
        }


        if(other.gameObject.tag == "suelo"){
            nieveparticle.gameObject.SetActive(true);
            //print("suelo");
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "suelo"){
            nieveparticle.gameObject.SetActive(false);
            //print("salio del suelo");
        }
    }

    private void reset(){
        render.color= colorinicial;
        transform.position=posinicial;
        perdio=false;
    }
    

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "coin"){
            audsrc.PlayOneShot(clipcollect);
            Destroy(other.gameObject);
            return;
        }


        if (other.gameObject.tag == "polar" || other.gameObject.tag == "water"){
            AudioManager.Instance.PlaySFX(deathclip);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }


        if (other.gameObject.tag == "iglu"){
            AudioManager.Instance.PlaySFX(winclip);
            StartCoroutine(GameWin());
            return;
        }


        if (other.gameObject.tag == "powerup"){
  
            audsrc.PlayOneShot(clipslime);
            Destroy(other.gameObject);
            powerup=true;
            //render.color= Color.green;
            anim.SetBool("duff",true);
            //StartCoroutine(TimeAnimDuff());

        }
    }

    // IEnumerator TimeAnimDuff(){
    //     yield return new WaitForSeconds(1f);
    //     anim.SetBool("duff",false);
    // }


    IEnumerator GameWin(){
        yield return new WaitForSeconds(0.6f);
        
        if(SceneManager.GetActiveScene().buildIndex+1 == SceneManager.sceneCountInBuildSettings){
            SceneManager.LoadScene(0);           
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

    }
}
