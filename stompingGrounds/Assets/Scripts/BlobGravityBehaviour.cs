using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BlobGravityBehaviour : MonoBehaviour {
    public bool isFrozen = false;
    private Rigidbody _rigidbody;
    private GameObject _self;
    private Vector3 _transformPosition;

    private void Start() {
        _self = gameObject;
        _rigidbody = _self.GetComponent<Rigidbody>();
        _self.layer = 7;
    }

    private void SnapPosition() {
        _transformPosition = _self.transform.position;
        _transformPosition.y = Mathf.Round(_transformPosition.y);
        _transformPosition.x = Mathf.Round(_transformPosition.x);
        _self.transform.position = _transformPosition;
    }
    
    public void SetFrozen(bool value) {
        SnapPosition();
        isFrozen = value;
        _rigidbody.isKinematic = value;
        _self.layer = 6;
    }

}
