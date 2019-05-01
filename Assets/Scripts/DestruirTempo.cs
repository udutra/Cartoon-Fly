using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirTempo : MonoBehaviour
{
    public float tempoDesttruir;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, tempoDesttruir);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
