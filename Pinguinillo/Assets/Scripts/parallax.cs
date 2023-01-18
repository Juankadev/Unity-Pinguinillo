using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D jugadorRB;
    [SerializeField] private bool semuevesolo;
    [SerializeField] private bool semuevealsaltar;
    [SerializeField] private int divisionVelocidad;
    private Transform posJugador;

    private void Awake() {
        material = GetComponent<SpriteRenderer>().material;
        jugadorRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        posJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        //jugadorRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if(semuevealsaltar){
            // if(jugadorRB.velocity.y > 0){
            //     this.velocity.y = jugadorRB.velocity.y*0.3f;
            // }
            // else{
            //     this.velocity.y = 0;
            // }

            if(jugadorRB.velocity.y != 0){
                //le sumo el Y del jugador
                Vector3 posNueva = new Vector3(this.transform.position.x,this.transform.position.y + (jugadorRB.velocity.y / divisionVelocidad),this.transform.position.z); 
                this.transform.position = posNueva;
            }

        }

        if(semuevesolo){ 
            offset = velocity * Time.deltaTime;
        }
        //tiene en cuenta la velocidad del jugador
        else{
            offset = ( jugadorRB.velocity.x /2) * velocity * Time.deltaTime; 
        }
        
        material.mainTextureOffset += offset;

    }
}
