using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnTriggerEnter(Collider other)
    {
        var balloon = other.GetComponentInParent<Balloon>();
        var changer = other.GetComponentInParent<GunsCountChanger>();
        var finish = other.GetComponentInParent<FinishTrigger>();

        if (finish)
            _player.Finish();

        if (balloon)
        {
            balloon.Deactivate();
            _player.DecreaseGunsCountBy(balloon.Damage);
        }

        if (changer)
        {
            switch (changer.Operation)
            {
                case '+':
                    _player.IncreaseGunsCountBy(changer.Value);
                    break;
                case '-':
                    _player.DecreaseGunsCountBy(changer.Value);
                    break;
                case '*':
                    int increaseValue = _player.GunsCount * (changer.Value - 1);
                    _player.IncreaseGunsCountBy(increaseValue);
                    break;
                case '/':
                    int decreaseValue = _player.GunsCount - (_player.GunsCount / changer.Value);
                    _player.DecreaseGunsCountBy(decreaseValue);
                    break;
            }
        }
    }
}
