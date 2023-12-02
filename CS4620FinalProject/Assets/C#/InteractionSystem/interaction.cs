using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint; 
    [SerializeField] private float _interactionPointRadius = 0.5f; 
    [SerializeField] private LayerMask _interactableMask; 

    private readonly Collider[] _collider = new Collider[3];
    [SerializeField] private int _numfound;

    private void update(){
        _numfound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _collider, _interactableMask);
    }
}
