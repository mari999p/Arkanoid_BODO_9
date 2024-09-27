using UnityEngine;

namespace Arkanoid.Game.PickUps
{
    public class ChangePlatformSizePickUp : PickUp
    {
        [SerializeField] private float _sizeChange;
        
        protected override void PerformActions()
        {
            base.PerformActions();

            Platform platform = FindObjectOfType<Platform>();
            platform.transform.localScale *= (1 + _sizeChange / 100);


        }
  
    }
}