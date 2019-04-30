using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAInimigoExercicio : MonoBehaviour
{
    private float incrementado;
    private float rotacaoZ;
    private bool isCurva;
    public float velocidadeMovimento, pontoInicialCurva, grausCurva, incrementar;

    [Header("Se a nave estiver no Eixo Horizontal marcar essa opção.")]
    public bool isHorizontal;

    [Header("Se estiver indo da esquerda para a direita marcar essa opção.")]
    public bool isEsquerda;

    [Header("Se estiver indo de cima para baixo marcar essa opção.")]
    public bool isEmCima;

    void Start()
    {
        rotacaoZ = transform.eulerAngles.z;
    }

    private void Update()
    {
        if (isHorizontal)
        {

            if (isEsquerda)
            {
                if (transform.position.x >= pontoInicialCurva && isCurva == false)
                {
                    isCurva = true;
                }
            }
            else
            {
                if (transform.position.x <= pontoInicialCurva && isCurva == false)
                {
                    isCurva = true;
                }
            }
            
        }
        else
        {
            if (transform.position.y <= pontoInicialCurva && isCurva == false)
            {
                isCurva = true;
            }
        }

        if (isCurva && incrementado < grausCurva)
        {
            rotacaoZ += incrementar;
            transform.rotation = Quaternion.Euler(0, 0, rotacaoZ);
            if (incrementar < 0)
            {
                incrementado += (incrementar * -1);
            }
            else
            {
                incrementado += incrementar;
            }
        }
        transform.Translate(Vector3.down * velocidadeMovimento * Time.deltaTime);
    }
}