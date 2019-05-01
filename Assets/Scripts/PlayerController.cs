using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController _GameControoler;
    private Rigidbody2D playerRb;
    public float velocidade;
    public Transform armaPosition;
    public float velocidadeTiro;
    public int idBullet;
    public TagBullets tagTiro;
    
    private void Start()
    {
        _GameControoler = FindObjectOfType(typeof(GameController)) as GameController;
        _GameControoler._PlayerController = this;
        _GameControoler.isAlivePlayer = true;
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerRb.velocity = new Vector2(horizontal * velocidade, vertical * velocidade);

        if (Input.GetButtonDown("Fire1"))
        {
            Shot();
        }
    }

    private void Shot()
    {
        GameObject temp = Instantiate(_GameControoler.bulletPrefab[idBullet]);
        temp.transform.tag = _GameControoler.AplicarTag(tagTiro);
        temp.transform.position = armaPosition.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0,velocidadeTiro);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "InimigoShot":
                {
                    Destroy(collision.gameObject); //Destroi o tiro
                    /*GameObject temp = Instantiate(_GameControoler.explosao[0], transform.position, _GameControoler.explosao[0].transform.localRotation);
                    Destroy(temp, 0.5f); //Destroi a animação da explosão
                    //Loot();
                    Destroy(this.gameObject);  //Destroi a nave
                    */
                    _GameControoler.HitPlayer();
                    break;
                }
        }
    }
}