using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAInimigo : MonoBehaviour
{
    private float incrementado, rotacaoZ;
    public Direcao direcaoMovimento;

    public float velocidadeMovimento, pontoInicialCurva;
    public bool isCurva;
    public float grausCurva, incrementar;

    public GameObject prefabTiro;
    public Transform arma;
    public float velocidadeTiro;

    public float delayTiro;

    private void Start()
    {
        rotacaoZ = transform.eulerAngles.z;
    }

    private void Update()
    {
        ControleCurva();


    }

    private void OnBecameVisible()
    {
        StartCoroutine("ControleTiro");
    }

    private void ControleCurva()
    {
        if (transform.position.y <= pontoInicialCurva && isCurva == false)
        {
            isCurva = true;
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

    private void Atirar()
    {
        GameObject temp = Instantiate(prefabTiro, arma.position, transform.localRotation);
        temp.GetComponent<Rigidbody2D>().velocity = transform.up * -1 * velocidadeTiro;
    }

    private IEnumerator ControleTiro()
    {
        yield return new WaitForSeconds(delayTiro);
        Atirar();
        StartCoroutine("ControleTiro");
    }
}
