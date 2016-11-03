using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    /*
    Triggers Assignment - Rotation script mostly for collectables.

    Josh Bellyk - 100526009
    Owen Meier  - 100538643    
    */

    void Update ()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 100);
    }
}
