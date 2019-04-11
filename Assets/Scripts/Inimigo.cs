﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public GameObject explosaoPrefab;
    public GameObject[] loot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "PlayerShot":
                {
                    Destroy(collision.gameObject); //Destroi o tiro
                    GameObject temp = Instantiate(explosaoPrefab, transform.position, transform.localRotation);
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
}