using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController _PlayerController;

    [Header("Limite de Movimento")]
    public Transform limiteSuperior;
    public Transform limiteInferior, limiteEsquerdo, limiteDireito;

    [Header("Limite Lateral Camera")]
    public Camera mainCamera;
    public Transform posFinalCamera, limiteCamEsquerdo, limiteCamDireito;
    public float velocidadeFase, velocidadeLateralCamera;

    private void Start()
    {
        _PlayerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }

    private void Update()
    {
        LimitarMovimentoPlayer();
    }

    private void LateUpdate()
    {
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(mainCamera.transform.position.x, posFinalCamera.position.y, -10), velocidadeFase * Time.deltaTime);
        ControlePosicaoCamera();
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

    private void ControlePosicaoCamera()
    {
        if (mainCamera.transform.position.x > limiteCamEsquerdo.position.x && mainCamera.transform.position.x < limiteCamDireito.position.x)
        {
            MoverCamera();
        }

        else if(mainCamera.transform.position.x <= limiteCamEsquerdo.position.x && _PlayerController.transform.position.x > limiteCamEsquerdo.position.x)
        {
            MoverCamera();
        }

        else if (mainCamera.transform.position.x >= limiteCamDireito.position.x && _PlayerController.transform.position.x < limiteCamDireito.position.x)
        {
            MoverCamera();
        }
    }

    private void MoverCamera()
    {
        Vector3 posicaoDestinoCamera = new Vector3(_PlayerController.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, posicaoDestinoCamera, velocidadeLateralCamera * Time.deltaTime);
    }
}
