using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    Color Base;
    [SerializeField]
    Color Offset;

    public void Init(bool offset)
    {
        GetComponent<MeshRenderer>().material.color = offset ? Offset : Base;
    }

}
