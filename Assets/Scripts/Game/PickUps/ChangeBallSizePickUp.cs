using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangeBallSizePickUp : PickUp
    {
        [SerializeField] private float _sizeChange;

        protected override void PerformActions()
        {
            base.PerformActions();

            Ball ball = LevelService.Instance.Ball;
            if (ball != null)
            {
                ball.transform.localScale *= (1 + _sizeChange / 100);
            }
        }
    }
}
        
    
