﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Configurações do PLayer")]
    public PlayerController _PlayerController;
    public Transform spawnPlayer;
    public bool isAlivePlayer;
    public int vidasExtra;
    public float delaySpawnPlayer, tempoInvencivel;

    [Header("Limite de Movimento")]
    public Transform limiteSuperior;
    public Transform limiteInferior, limiteEsquerdo, limiteDireito, cenario, posicaoFinalFase;
    public float velocidadeFase;

    [Header("Prefabs")]
    public GameObject[] bulletPrefab;
    public GameObject explosaoPrefab;
    public GameObject playerPrefab;

    private void Update()
    {
        if (isAlivePlayer)
        {
            LimitarMovimentoPlayer();
        }
    }

    private void LateUpdate()
    {
        cenario.position = Vector3.MoveTowards(cenario.position, new Vector3(cenario.position.x, posicaoFinalFase.position.y, 0), velocidadeFase * Time.deltaTime);
    }

    private void LimitarMovimentoPlayer()
    {
        float posX = _PlayerController.transform.position.x;
        float posY = _PlayerController.transform.position.y;


        if(posY > limiteSuperior.position.y)
        {
            _PlayerController.transform.position = new Vector3(posX, limiteSuperior.position.y, 0);
        }
        else if(posY < limiteInferior.position.y)
        {
            _PlayerController.transform.position = new Vector3(posX, limiteInferior.position.y, 0);
        }

        if(posX > limiteDireito.position.x)
        {
            _PlayerController.transform.position = new Vector3(limiteDireito.position.x, posY, 0);
        }
        else if (posX < limiteEsquerdo.position.x)
        {
            _PlayerController.transform.position = new Vector3(limiteEsquerdo.position.x, posY, 0);
        }
    }

    public string AplicarTag(TagBullets tag)
    {
        string retorno = null;

        switch (tag)
        {
            case TagBullets.Player:
                retorno = "PlayerShot";
                break;
            case TagBullets.Inimigo:
                retorno = "InimigoShot";
                break;
            default:
                break;
        }
        return retorno;
    }

    public void HitPlayer()
    {
        isAlivePlayer = false;
        GameObject temp = Instantiate(explosaoPrefab, _PlayerController.transform.position, explosaoPrefab.transform.localRotation);
        Destroy(_PlayerController.gameObject);
        vidasExtra -= 1;

        if (vidasExtra >= 0)
        {
            StartCoroutine("SpawnPlayer");
        }
        else
        {
            Debug.LogError("Acabou");
        }
    }

    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(delaySpawnPlayer);
        GameObject temp = Instantiate(playerPrefab, spawnPlayer.transform.position, spawnPlayer.transform.localRotation);
        yield return new WaitForEndOfFrame();
        _PlayerController.StartCoroutine("Invencivel");
    }
}
