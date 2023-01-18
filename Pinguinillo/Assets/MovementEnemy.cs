using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool esDerecha;
    [SerializeField] private float contadorT;
    [SerializeField] private float tiempoParaCambiar;
    SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        tiempoParaCambiar = 2f;
        contadorT = tiempoParaCambiar;
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(esDerecha){
            transform.position += Vector3.right * speed * Time.deltaTime;
            spr.flipX=false;
        }
        else{
            transform.position += Vector3.left * speed * Time.deltaTime;
            spr.flipX=true;
        }

        contadorT -= Time.deltaTime;
        
        if(contadorT<=0){
            contadorT=tiempoParaCambiar;
            esDerecha=!esDerecha;
        }
    }
}
