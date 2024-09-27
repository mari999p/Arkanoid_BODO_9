using Arkanoid.Services;
using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangeScorePickUp: PickUp
    {
        [SerializeField] private int _scoreChange = 1;
        protected override void PerformActions()
        {
            base.PerformActions();

            GameService.Instance.AddScore(_scoreChange);
        }
        
    }
}