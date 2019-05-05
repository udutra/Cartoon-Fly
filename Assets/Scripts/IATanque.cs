using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATanque : MonoBehaviour
{
    private GameController _GameController;
    public Transform arma;
    public TagBullets tagTiro;
    public int idBullet, pontos;
    public float delayTiro, velocidadeTiro;
    
    private void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    private void Update()
    {
        
    }

    private void OnBecameVisible()
    {
        StartCoroutine("ControleTiro");
    }

    private IEnumerator ControleTiro()
    {
        yield return new WaitForSeconds(delayTiro);
        Atirar();
        StartCoroutine("ControleTiro");
    }

    private void Atirar()
    {
        if (_GameController.isAlivePlayer)
        {
            arma.up = _GameController._PlayerController.transform.position - transform.position;
            GameObject temp = Instantiate(_GameController.bulletPrefab[idBullet], arma.position, new Quaternion(0, 0, arma.localRotation.z, arma.localRotation.w));
            temp.transform.tag = _GameController.AplicarTag(tagTiro);
            temp.GetComponent<Rigidbody2D>().velocity = arma.up * velocidadeTiro;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "PlayerShot":
                {
                    
                    GameObject temp = Instantiate(_GameController.explosaoPrefab, transform.position, transform.localRotation);
                    temp.transform.parent = _GameController.cenario;
                    _GameController.AddScore(pontos);
                    Destroy(collision.gameObject); //Destroi o tiro
                    Destroy(this.gameObject);  //Destroi a nave inimiga
                    break;
                }
        }
    }
}
