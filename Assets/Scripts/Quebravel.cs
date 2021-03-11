using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quebravel : MonoBehaviour
{
    public int resistencia;
    [SerializeField] GameObject pedra;
    [SerializeField] AudioSource explosionSound;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform centerTarget;
    Animator anim;
    Renderer render;
    int qtPedras;
    Color initialColor;

    private void Start()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<Renderer>();
        initialColor = render.material.color;
        explosionSound = GetComponent<AudioSource>();
        resistencia = Random.Range(2, 6);
        qtPedras = resistencia;
    }

    IEnumerator ReturnInitialColor()
    {
        yield return new WaitForSeconds(.2f);
        render.material.color = initialColor;
    }

    private void Update()
    {
        //Verifica quando a resistencia acaba e então libera a quantidade de pedras igual a sua resistencia inicial
        if (resistencia == 0)
        {
            anim.SetTrigger("Fecha");
        }
        if (resistencia < 0)
        {
            resistencia = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Verifica se foi acertado por um projetil para diminuir a resistencia 
        if (other.gameObject.CompareTag("Projetil"))
        {
            explosionSound.Play();
            resistencia -= 1;
            render.material.color = Color.red;
            Destroy(other.gameObject);
            StartCoroutine("ReturnInitialColor");
            GameObject newPedra = Instantiate(pedra, spawnPoint.position, Quaternion.identity);
            newPedra.GetComponent<Rigidbody>().AddForce(Vector3.RotateTowards(transform.position, centerTarget.position, Random.Range(-90, -135), 30), ForceMode.Impulse);
            Destroy(newPedra, Random.Range(10, 15));
        }
    }
}
