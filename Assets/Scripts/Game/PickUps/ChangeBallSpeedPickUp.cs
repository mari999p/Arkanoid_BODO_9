using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangeBallSpeedPickUp : PickUp
    {
        [SerializeField] private float _speedChange;
        
        protected override void PerformActions()
        {
            base.PerformActions();
            Ball ball = LevelService.Instance.Ball;
            if (ball != null)
            {
                ball.ChangeSpeed(_speedChange);
            }


        }
    }
}