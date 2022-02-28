using UnityEngine;

public class PairColliderDisabler : MonoBehaviour
{
    [SerializeField] private Gate _PairGunsCountChanger;

    private Collider _pairCollider;

    private void Awake()
    {
        _pairCollider = _PairGunsCountChanger.GetComponentInChildren<Collider>();
        if (_pairCollider == null)
            throw new System.Exception($"Pair guns count changer doesn't contain {typeof(Collider)}");
    }

    public void DisablePairCollider()
    {
        _pairCollider.enabled = false;
    }
}
