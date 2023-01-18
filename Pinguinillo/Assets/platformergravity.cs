using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformergravity : MonoBehaviour
{
    Rigidbody2D rb;
    bool colisiono,bajar;
    [SerializeField] private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //velocity=0.5f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(colisiono){
            rb.velocity = Vector2.down * velocity * Time.deltaTime;
            StartCoroutine(Destruir());
        }

    }
    

    private void OnCollisionEnter2D(Collision2D other) {
        colisiono=true;
    }

    IEnumerator Destruir(){
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
