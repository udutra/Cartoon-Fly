using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    private GameController _GameController;
    //private PlayerController _playerController;
    public Transform arma;
    public GameObject explosaoPrefab;
    public GameObject[] loot;
    public float[] delayEntreTiros;
    public int idBullet;
    public TagBullets tagTiro;

    private void Start()
    {
        //_playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        StartCoroutine("Atirar");
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "PlayerShot":
                {
                    Destroy(collision.gameObject); //Destroi o tiro
                    GameObject temp = Instantiate(_GameController.explosaoPrefab, transform.position, transform.localRotation);
                    Destroy(temp, 0.5f); //Destroi a animação da explosão
                    Loot();
                    Destroy(this.gameObject);  //Destroi a nave inimiga
                    break;
                }
        }
    }

    private void Loot()
    {
        int rand = Random.Range(0, 100);
        int idItem = 0;
        if (rand < 50)
        {
            rand = Random.Range(0, 100);
            if (rand > 85)
            {
                idItem = 2; //CaixaBomba 15%
                //print("Bomba - Rand: " + rand + ", IDItem: " + idItem);
            }
            else if (rand > 50 && rand <= 85 )
            {
                idItem = 1; //CaixaSaude 35%
                //print("Saude - Rand: " + rand + ", IDItem: " + idItem);
            }
            else
            {
                idItem = 0; //CaixaMoeda 50%
                //print("Moeda - Rand: " + rand + ", IDItem: " + idItem);
            }
            Instantiate(loot[idItem], transform.position, transform.localRotation);
        }
    }

    private IEnumerator Atirar()
    {
        yield return new WaitForSeconds(Random.Range(delayEntreTiros[0], delayEntreTiros[1]));
        Shot();
        StartCoroutine("Atirar");
    }

    private void Shot()
    {
        arma.right = _GameController._PlayerController.transform.position - transform.position;
        GameObject temp = Instantiate(_GameController.bulletPrefab[idBullet], arma.position, arma.localRotation);
        temp.transform.tag = _GameController.AplicarTag(tagTiro);
        temp.GetComponent<Rigidbody2D>().velocity = arma.right * 3;
    }
}
