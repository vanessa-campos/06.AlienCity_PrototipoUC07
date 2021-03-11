using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystickController : PlayerController
{
    public VariableJoystick variableJoystick;

    //Captura o botão de pular
    public void OnButtonAClicked()
    {
        jump = true;
    }

    //Captura o botão de atirar
    public void OnButtonBClicked()
    {
        Fire2();
    }

    public override void Update()
    {
        //Movimentação
        move = variableJoystick.Vertical * transform.TransformDirection(Vector3.forward) * MoveSpeed;
        transform.Rotate(new Vector3(0, variableJoystick.Horizontal * RotationSpeed * Time.deltaTime, 0));

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
        
        //Chamar o método para animar
        Anima();
    }

    public override void Anima()
    {
        if (jump)
        {
            anim.SetTrigger("Pula");
        }
        if (variableJoystick.Vertical != 0 || variableJoystick.Horizontal != 0)
        {
            anim.SetTrigger("Corre");
        }
        else
        {
            anim.SetTrigger("Parado");
        }
    }
}
