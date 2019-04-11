using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
