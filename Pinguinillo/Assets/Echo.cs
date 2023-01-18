using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour
{
    public float timeSpawn;
    public float startTimeSpawn;
    public GameObject echo;
    Animator anim;
    Rigidbody2D rb;
    private playersincolision ScriptPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ScriptPlayer = GetComponent<playersincolision>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x !=0 && ScriptPlayer.clones==true){
            if (timeSpawn<=0){
                //Vector3 pos = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
                GameObject instance = (GameObject)Instantiate(echo,this.transform.position,Quaternion.identity);
                //instance.transform.SetParent(this.transform);
                //instance.layer =9;
                instance.transform.localScale = this.transform.localScale;
                Destroy(instance, 2.5f);
                timeSpawn = startTimeSpawn;
            } 
            else{
                timeSpawn -= Time.deltaTime;
            }
        }

    }
    
}
