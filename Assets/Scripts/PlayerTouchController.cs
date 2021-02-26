using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTouchController : MonoBehaviour
{
    [SerializeField] float Speed = 1;
    [SerializeField] Text pontosText;
    [SerializeField] Text jogoText;
    Rigidbody2D _rigidibody;
    bool touching = false;
    private int pontos;
    public int Pontos { get { return pontos; } set { pontos = value; pontosText.text = "PONTUAÇÃO: " + pontos; } }

    void Start()
    {
        _rigidibody = GetComponent<Rigidbody2D>();
        Pontos = 0;
    }

    void Update()
    {

        // Mostrar o texto quando o jogador estiver tocando na tela
        if (touching)
        {
            jogoText.text = "Capture todas as gemas";
        }
        else
        {
            jogoText.text = "";
        }

    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * Speed;
        float vertical = Input.GetAxis("Vertical") * Speed;

        if (horizontal != 0 || vertical != 0)
        {
            touching = true;
        }else{
            touching = false;
        }

        //transform.Translate (horizontal, vertical, 0);
        _rigidibody.velocity = new Vector3(horizontal * Speed, vertical * Speed, 0);
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
