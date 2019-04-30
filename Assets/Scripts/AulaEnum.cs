using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AulaEnum : MonoBehaviour
{
    public Direcao movimento;

    void Start()
    {
        if (movimento == Direcao.BAIXO)
        {

        }

        switch (movimento)
        {
            case Direcao.CIMA:
                break;
            case Direcao.BAIXO:
                break;
            case Direcao.ESQUERDA:
                break;
            case Direcao.DIREITA:
                break;
            default:
                break;
        }
    }

    
}
