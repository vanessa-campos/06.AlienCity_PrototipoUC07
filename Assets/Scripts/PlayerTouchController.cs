using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTouchController : MonoBehaviour
{
    [SerializeField] Text pontosText;
    [SerializeField] GameObject jogoText;
    Animator _animator;
    private int pontos;
    public int Pontos { get { return pontos; } set { pontos = value; pontosText.text = "PONTUAÇÃO: " + pontos; } }

    void Start()
    {
        _animator = GetComponent<Animator>();
        Pontos = 0;

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            jogoText.SetActive(true);
            _animator.SetTrigger("Move");
            Touch t = Input.GetTouch(0);
            Vector3 pos = Camera.main.ScreenToWorldPoint(t.position);
            transform.position = pos;
        }else{
            jogoText.SetActive(false);
        }

        // if (touching)
        // {
        //     // Mostrar o texto quando o jogador estiver tocando na tela
        //     jogoText.text = "Capture todas as gemas";
        //     // Permitir a movimentação do personagem somente quando tocadado
        // }
        // else
        // {
        //     jogoText.text = "";
            
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("gema"))
        {
            Pontos += 10;
            Destroy(other.gameObject);
        }
    }
}
