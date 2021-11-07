using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollisionHandler : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

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
            _player.DecreaseGunsCountBy(balloon.Damage);
        }

        if (changer)
        {
            other.enabled = false;
            var pairCollisionDisabler = other.GetComponentInParent<PairColliderDisabler>();
            if (pairCollisionDisabler)
                pairCollisionDisabler.DisablePairCollider();

            switch (changer.Operation)
            {
                case '+':
                    _player.IncreaseGunsCountBy(changer.Value);
                    break;

                case '-':
                    _player.DecreaseGunsCountBy(changer.Value);
                    break;

                case 'x':
                    int increaseValue = _player.GunsCount * (changer.Value - 1);
                    _player.IncreaseGunsCountBy(increaseValue);
                    break;

                case '÷':
                    int decreaseValue = _player.GunsCount - (_player.GunsCount / changer.Value);
                    _player.DecreaseGunsCountBy(decreaseValue);
                    break;
            }
        }
    }
}
