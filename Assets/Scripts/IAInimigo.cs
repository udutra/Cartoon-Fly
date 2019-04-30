using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAInimigo : MonoBehaviour
{
    public float velocidadeMovimento, pontoInicialCurva, grausCurva, incrementar;
    private float incrementado, rotacaoZ;
    public bool isCurva;

    private void Start()
    {
        rotacaoZ = transform.eulerAngles.z;
    }

    private void Update()
    {
        if (transform.position.y <= pontoInicialCurva && isCurva == false)
        {
            isCurva = true;
        }
        if(isCurva && incrementado < grausCurva)
        {
            rotacaoZ += incrementar;
            transform.rotation = Quaternion.Euler(0, 0, rotacaoZ);
            if(incrementar < 0)
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
}
