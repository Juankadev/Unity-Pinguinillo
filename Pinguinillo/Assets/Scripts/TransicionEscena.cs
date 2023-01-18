using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicionEscena : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    [SerializeField] private float espera;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Desactivar());
    }

    IEnumerator Desactivar(){
        yield return new WaitForSeconds(espera);
        this.gameObject.SetActive(false);
    }
}
