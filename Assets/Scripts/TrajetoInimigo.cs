using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajetoInimigo : MonoBehaviour
{
    private int idCheckPoints;
    private bool movimentar;
    public Transform naveInimiga;
    public Transform[] checkPoints;
    public float velocidadeMovimento, delayParado;

    private void Start()
    {
        StartCoroutine("IniciarMovimento");
    }

    private void Update()
    {
        if (movimentar == true)
        {
            naveInimiga.localPosition = Vector3.MoveTowards(naveInimiga.localPosition, checkPoints[idCheckPoints].position, velocidadeMovimento * Time.deltaTime);
            if (naveInimiga.localPosition == checkPoints[idCheckPoints].position)
            {
                movimentar = false;
                StartCoroutine("IniciarMovimento");
            }
        }
    }

    IEnumerator IniciarMovimento()
    {
        idCheckPoints += 1;
        if (idCheckPoints >= checkPoints.Length)
        {
            idCheckPoints = 0;
        }
        yield return new WaitForSeconds(delayParado);
        movimentar = true;
    }

}
