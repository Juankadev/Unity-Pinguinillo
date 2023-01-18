using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersincolision : MonoBehaviour
{
    [SerializeField] float speed;
    Animator anim;
    Rigidbody2D rb;
    AudioSource aud;
    [SerializeField] public bool clones;
    [SerializeField] ParticleSystem particleJump;
    bool aumentaSpeed, tocasuelo,salto;
    float aumento, speedInicial, movex;
    [SerializeField] private float fuerzaJump;
    [SerializeField] private AudioSource AudioManager;

    public int saltosmaximos;
    public int saltosrestantes;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        aud = GetComponent<AudioSource>();
        speedInicial = speed;
        aumento = speed * 0.4f;
        saltosrestantes=saltosmaximos;
    }

    // Update is called once per frame
    private void Update()
    {
        movex = Input.GetAxisRaw("Horizontal");


        if (Input.GetKeyDown(KeyCode.UpArrow) && saltosrestantes>0)
        {
            salto=true;
        }
        else{
            salto=false;
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            clones = !clones;
            aumentaSpeed = true;
            speed = speedInicial;
        }

        if (clones && aumentaSpeed)
        {
            speed += aumento;
            aumentaSpeed = false;
        }
        
    }
    
    void FixedUpdate()
    {
        moverse(salto);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "suelo")
        {
            //tocasuelo = true;
            saltosrestantes=saltosmaximos;
            //salto=false;
            particleJump.Play();
        }
    }


    private void moverse(bool salto)
    {
        if (salto) {
            saltosrestantes--;
            //print(saltosrestantes);
            print("IF Salto");
            //tocasuelo = false;
            AudioManager.Play();
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * fuerzaJump * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        rb.velocity = new Vector2(movex * speed * Time.fixedDeltaTime, rb.velocity.y);
        AudioPasos();
        Girar();
        anim.SetInteger("move", (int)movex);
    }


    private void AudioPasos(){
        if (movex != 0 && rb.velocity.y == 0)
        {
            aud.enabled = true;
        }
        else
        {
            aud.enabled = false;
        }
    }

    private void Girar(){
        if (movex < 0)
        {
            transform.localScale = new Vector3(-2.70f, this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (movex > 0)
        {
            transform.localScale = new Vector3(2.70f, this.transform.localScale.y, this.transform.localScale.z);
        }
    }
}
