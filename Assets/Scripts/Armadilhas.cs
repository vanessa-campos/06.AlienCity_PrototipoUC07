using System.Collections;
using UnityEngine;

public class Armadilhas : MonoBehaviour
{  
    [SerializeField] bool Ativada = false;
    [SerializeField] float TimeDano;
    [SerializeField] int ValorDano;
    float time;
    PlayerJoystickController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerJoystickController>();
        time = TimeDano;
    }

    private void Update()
    {
        if(Ativada)
        {
            time -= Time.deltaTime;
            if(time <=0)
            {
                Handheld.Vibrate();
                Dano();
                time = TimeDano;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Ativada = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Ativada = false;
        }
    }

    void Dano()
    {
        //Tira o valor de dano do total de HP e o equivalente a porcentagem na barra
        player.HP -= ValorDano;
        player.barra.value -= ValorDano * 0.01f;
    }
}
