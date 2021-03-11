using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelCreditos;
    public GameObject panelPause;
    public GameObject levelCompletedText;
    public GameObject gameOverText;
    public GameObject somLigado;
    public GameObject somDesligado;
    [SerializeField] Image[] Hearts;
    PlayerJoystickController player;
    int vida;
    int som = 1;

    public void ButtonFaseI()
    {
        SceneManager.LoadScene("Teste01_ToqueBasico");
    }

    public void ButtonFaseII()
    {
        PlayerPrefs.DeleteKey("PPVida");
        PlayerPrefs.DeleteKey("PPAberto");
        PlayerPrefs.SetInt("PPVida", 2);
        SceneManager.LoadScene("Teste02_MovBasica");
    }

    public void ButtonCredits()
    {
        panelMenu.SetActive(false);
        panelCreditos.SetActive(true);
    }

    public void ButtonSair()
    {
        Application.Quit();
    }

    public void ButtonVoltar()
    {
        panelCreditos.SetActive(false);
        panelMenu.SetActive(true);
    }

    public void ButtonPause()
    {
        Time.timeScale = 0;
        panelPause.SetActive(true);
    }
    public void ButtonVoltarDoPause()
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }

    public void SomLigarDesligar()
    {
        if (som == 0)
        {
            som = 1;
            somDesligado.SetActive(false);
            somLigado.SetActive(true);
        }
        else
        {
            som = 0;
            somLigado.SetActive(false);
            somDesligado.SetActive(true);
        }
        PlayerPrefs.SetInt("PPSom", som);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void FaseII()
    {
        SceneManager.LoadScene("Teste02_MovBasica");
    }

    public void Fim()
    {
        SceneManager.LoadScene("Fim");
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayerPrefs.SetInt("PPSom", som);
            PlayerPrefs.SetInt("Recorde", 0);
            Invoke("Menu", 5f);
        }
        if (SceneManager.GetActiveScene().name == "Teste02_MovBasica")
        {
            player = FindObjectOfType<PlayerJoystickController>();
            vida = PlayerPrefs.GetInt("PPVida");
            for (int i = 0; i < Hearts.Length; i++)
            {
                Hearts[i].color = new Vector4(0, 0, 0, 0);
            }
            for (int i = 0; i < vida + 1; i++)
            {
                Hearts[i].color = new Vector4(1, 1, 1, 1);
            }
        }
        if (SceneManager.GetActiveScene().name == "Fim")
        {
            InvokeRepeating(nameof(Vibrate), .5f, .5f);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Fim")
        {
            if (PlayerPrefs.GetInt("PPAberto") == 1)
            {
                levelCompletedText.SetActive(true);
                StartCoroutine("CorCancelInvoke");
            }
            else
            {
                gameOverText.SetActive(true);
                StartCoroutine("CorCancelInvoke");
            }
        }
        if (SceneManager.GetActiveScene().name == "Teste02_MovBasica")
        {
            LifeSystem();
        }
    }

    void LifeSystem()
    {
        //Tira uma vida quando a barra esvazia, salva o valor em PlayerPrefs e reinicia a fase
        if (player.HP <= 0)
        {
            vida -= 1;
            PlayerPrefs.SetInt("PPVida", vida);
            Hearts[vida + 1].color = new Vector4(0, 0, 0, 0);
            FaseII();
        }
        //Se as vidas acabarem vai pra cena de fim
        if (vida < 0)
        {
            Fim();
        }
    }

    IEnumerator CorCancelInvoke()
    {
        yield return new WaitForSeconds(3);
        CancelInvoke();
    }

    void Vibrate()
    {
        Handheld.Vibrate();
    }

}
