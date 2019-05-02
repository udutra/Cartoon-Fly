using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController _GameControoler;
    private Rigidbody2D playerRb;
    private SpriteRenderer playerSr;
    public SpriteRenderer fumacaSr;
    public Transform armaPosition;
    public Color corInvensivel;
    public TagBullets tagTiro;
    public int idBullet;
    public float velocidade, velocidadeTiro, delayPiscar;
    

    private void Start()
    {
        _GameControoler = FindObjectOfType(typeof(GameController)) as GameController;
        _GameControoler._PlayerController = this;
        _GameControoler.isAlivePlayer = true;
        playerRb = GetComponent<Rigidbody2D>();
        playerSr = GetComponent<SpriteRenderer>();
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
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocidadeTiro);
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

    public IEnumerator Invencivel()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = false;
        playerSr.color = corInvensivel;
        fumacaSr.color = corInvensivel;
        StartCoroutine("PiscarPlayer");

        yield return new WaitForSeconds(_GameControoler.tempoInvencivel);
        col.enabled = true;
        playerSr.color = Color.white;
        fumacaSr.color = Color.white;
        playerSr.enabled = true;
        fumacaSr.enabled = true;
        StopCoroutine("PiscarPlayer");
    }

    public IEnumerator PiscarPlayer()
    {
        yield return new WaitForSeconds(delayPiscar);
        playerSr.enabled = !playerSr.enabled;
        fumacaSr.enabled = !fumacaSr.enabled;
        StartCoroutine("PiscarPlayer");
    }
}