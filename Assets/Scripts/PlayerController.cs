using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] protected float MoveSpeed;
    [SerializeField] protected float RotationSpeed;
    [SerializeField] protected float JumpSpeed;
    [SerializeField] protected GameObject Projetil;
    [SerializeField] protected Transform cano;
    [SerializeField] public Slider barra;
    [SerializeField] Text barraText;
    [SerializeField] Text pontosText;
    [SerializeField] protected AudioClip jumpSound;
    [SerializeField] AudioClip shootSound = null;
    [SerializeField] AudioClip collectSound = null;
    [SerializeField] AudioClip finishSound = null;
    [SerializeField] GameObject smoke;
    protected Vector3 gravidade = Vector3.zero;
    protected Vector3 move = Vector3.zero;
    protected CharacterController cc;
    protected Animator anim;
    GameManager GM;
    Armadilhas armadilhas;
    protected bool jump = false;

    int pontos;
    public int Pontos { get { return pontos; } set { pontos = value; pontosText.text = "PONTUAÇÃO: " + pontos; } }

    int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value; barraText.text = hp.ToString();
            if (hp < 0)
            {
                hp = 0;
            }
        }
    }

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        GM = FindObjectOfType<GameManager>();
        armadilhas = FindObjectOfType<Armadilhas>();
    }

    void Start()
    {
        Pontos = 0;
        HP = 100;
    }

    public virtual void Update()
    {
        //Movimentação
        move = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * MoveSpeed;
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime, 0));

        //Pulo
        if (!cc.isGrounded)
        {
            gravidade += Physics.gravity * Time.deltaTime;
        }
        else
        {
            gravidade = Vector3.zero;
            if (jump)
            {
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                gravidade.y = JumpSpeed;
                jump = false;
            }
        }
        move += gravidade;
        cc.Move(move * Time.deltaTime);

        //Captura o botão de atirar
        if (Input.GetButtonDown("Fire2"))
        {
            Fire2();
        }

        //Chamar o método para animar
        Anima();
    }

    public virtual void Anima()
    {
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("Pula");
            jump = true;
        }
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            anim.SetTrigger("Corre");
        }
        else
        {
            anim.SetTrigger("Parado");
        }
    }

    public void Fire2()
    {
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        GameObject newProjetil = Instantiate(Projetil, cano.position, cano.rotation);
        Rigidbody rbProjetil = newProjetil.GetComponent<Rigidbody>();
        rbProjetil.AddForce(transform.forward * 7f, ForceMode.Impulse);
        Destroy(newProjetil, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Coleta das pedras
        if (other.gameObject.CompareTag("gema"))
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            Pontos += 10;
            Destroy(other.gameObject);
        }
        //Ativar o efeito de fumaça quando tocar a armadilha lava
        if (other.gameObject.CompareTag("lava"))
        {
            smoke.SetActive(true);
        }
        //Verificar a chegada na porta do final
        if (other.gameObject.CompareTag("Finish"))
        {
            AudioSource.PlayClipAtPoint(finishSound, transform.position);
            Destroy(other.gameObject, 2);
            PlayerPrefs.SetInt("PPAberto", 1);
            GM.Fim();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Desativar o efeito de fumaça quando tocar a armadilha lava
        if (other.gameObject.CompareTag("lava"))
        {
            smoke.SetActive(false);
        }
    }    
}