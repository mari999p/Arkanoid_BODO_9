using Arkanoid.Game.PickUps;
using Arkanoid.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arkanoid.Services
{
    public class PickUpService : SingletonMonoBehaviour<PickUpService>
    {
        #region Variables

        [Range(0, 100)]
        [SerializeField] private int _pickUpSpawnProbability;

        #endregion

        #region Public methods

        public void SpawnPickUp(Vector3 position, PickUp[] possiblePickUps)
        {
            int random = Random.Range(0, 101);
            if (random <= _pickUpSpawnProbability && possiblePickUps.Length > 0)
            {
                int pickUpIndex = Random.Range(0, possiblePickUps.Length);
                Instantiate(possiblePickUps[pickUpIndex], position, Quaternion.identity);
            }
        }

        #endregion
    }
}