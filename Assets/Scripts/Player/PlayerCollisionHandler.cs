using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ParticleSystem _gunExplosion;

    private void OnTriggerEnter(Collider other)
    {
        var balloon = other.GetComponentInParent<Balloon>();
        var modifier = other.GetComponentInParent<Gate>();
        var finish = other.GetComponentInParent<FinishTrigger>();

        if (finish)
            _player.OnLevelFinishing();

        if (balloon)
        {
            balloon.TakeOffGun();
            _player.ChangeGunsTo(_player.Guns - balloon.Damage);
        }

        if (modifier)
        {
            other.enabled = false;
            var pairCollisionDisabler = other.GetComponentInParent<PairColliderDisabler>();
            if (pairCollisionDisabler)
                pairCollisionDisabler.DisablePairCollider();

            switch (modifier.Type)
            {
                case Gate.OperationType.Add:
                    _player.ChangeGunsTo(_player.Guns + modifier.Value);
                    break;

                case Gate.OperationType.Subtract:
                    _player.ChangeGunsTo(_player.Guns - modifier.Value);
                    _gunExplosion.Play();
                    break;

                case Gate.OperationType.Multiply:
                    _player.ChangeGunsTo(_player.Guns * modifier.Value);
                    break;

                case Gate.OperationType.Divide:
                    _player.ChangeGunsTo(_player.Guns / modifier.Value);
                    _gunExplosion.Play();
                    break;
            }
        }
    }
}
