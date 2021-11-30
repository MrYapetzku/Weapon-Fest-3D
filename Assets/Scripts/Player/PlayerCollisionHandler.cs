using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]private Player _player;
    [SerializeField] private ParticleSystem _gunExplosion;

    private void OnTriggerEnter(Collider other)
    {
        var balloon = other.GetComponentInParent<Balloon>();
        var changer = other.GetComponentInParent<GunsCountChanger>();
        var finish = other.GetComponentInParent<FinishTrigger>();

        if (finish)
            _player.OnLevelFinishing();

        if (balloon)
        {
            balloon.TakeOffGun();
            _player.ChangeGunsBy(-balloon.Damage);
        }

        if (changer)
        {
            other.enabled = false;
            var pairCollisionDisabler = other.GetComponentInParent<PairColliderDisabler>();
            if (pairCollisionDisabler)
                pairCollisionDisabler.DisablePairCollider();

            switch (changer.Type)
            {
                case GunsCountChanger.OperationType.Add:
                    _player.ChangeGunsBy(changer.Value);
                    break;

                case GunsCountChanger.OperationType.Subtract:
                    _player.ChangeGunsBy(-changer.Value);
                    _gunExplosion.Play();
                    break;

                case GunsCountChanger.OperationType.Multiply:
                    int increaseValue = _player.Guns * (changer.Value - 1);
                    _player.ChangeGunsBy(increaseValue);
                    break;

                case GunsCountChanger.OperationType.Divide:
                    int decreaseValue = _player.Guns - (_player.Guns / changer.Value);
                    _player.ChangeGunsBy(-decreaseValue);
                    _gunExplosion.Play();
                    break;
            }
        }
    }
}
