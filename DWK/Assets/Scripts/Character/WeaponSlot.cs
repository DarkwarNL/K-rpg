using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WeaponSlot : MonoBehaviour
{
    private MeshFilter _Renderer;

    private void Awake()
    {
        _Renderer = GetComponent<MeshFilter>();
    }

    internal void SetWeapon(Mesh mesh)
    {
        _Renderer.mesh = mesh;
    }
}
