using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTouchController : MonoBehaviour
{
    [SerializeField] float Dist = 200;
    [SerializeField] GameObject jogoText;
    [SerializeField] Text pontosText;
    [SerializeField] Text recordeText;
    [SerializeField] AudioSource collectSound;
    Animator _animator;
    private int pontos;
    public int Pontos { get { return pontos; } set { pontos = value; pontosText.text = "PONTUAÇÃO: " + pontos; } }

    private int recorde;
    public int Recorde { get { return recorde; } set { recorde = value; recordeText.text = "RECORDE: " + recorde; } }

    void Start()
    {
        _animator = GetComponent<Animator>();
        collectSound = GetComponent<AudioSource>();
        Pontos = 0;
        Recorde = PlayerPrefs.GetInt("Recorde");        
    }

    private void Update()
    {
        if (Pontos > Recorde)
        {
            Recorde = Pontos;
            PlayerPrefs.SetInt("Recorde", Recorde);
        }
    }

    private void OnMouseOver()
    {
        // Mostra o texto 
        jogoText.SetActive(true);
        // Ativa a animação de flutuação
        _animator.SetTrigger("Move");
        // Pega a informação de touch para traçar o movimento do personagem
        Touch t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Moved)
        {
            transform.position += (Vector3)t.deltaPosition / Dist;
        }
    }

    private void OnMouseExit()
    {
        jogoText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("gema"))
        {
            collectSound.Play();
            Pontos += 10;
            Destroy(other.gameObject);
        }
    }
}
