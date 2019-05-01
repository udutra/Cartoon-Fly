using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAInimigo : MonoBehaviour
{
    private GameController _GameControoler;
    private float incrementado, rotacaoZ;
    public Direcao direcaoMovimento;

    public float velocidadeMovimento, pontoInicialCurva;
    public bool isCurva;
    public float grausCurva, incrementar;

    public Transform arma;
    public float velocidadeTiro;

    public float delayTiro;

    public int idBullet;
    public TagBullets tagTiro;

    private void Start()
    {
        _GameControoler = FindObjectOfType(typeof(GameController)) as GameController;
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
        GameObject temp = Instantiate(_GameControoler.bulletPrefab[idBullet], arma.position, transform.localRotation);
        temp.transform.tag = _GameControoler.AplicarTag(tagTiro);
        temp.GetComponent<Rigidbody2D>().velocity = transform.up * -1 * velocidadeTiro;
    }

    private IEnumerator ControleTiro()
    {
        yield return new WaitForSeconds(delayTiro);
        Atirar();
        StartCoroutine("ControleTiro");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "PlayerShot":
                {
                    Destroy(collision.gameObject); //Destroi o tiro
                    GameObject temp = Instantiate(_GameControoler.explosaoPrefab, transform.position, _GameControoler.explosaoPrefab.transform.localRotation);
                    Destroy(temp, 0.5f); //Destroi a animação da explosão
                    //Loot();
                    Destroy(this.gameObject);  //Destroi a nave inimiga
                    break;
                }
        }
    }
}
