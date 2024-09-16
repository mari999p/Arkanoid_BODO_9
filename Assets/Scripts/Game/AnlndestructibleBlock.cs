using UnityEngine;

namespace Arkanoid.Game
{
    public class AnIndestructibleBlock : MonoBehaviour
    {
        #region Unity lifecycle

        private void Start()
        {
            Block.OnCreated += CreatedBlock;
        }

        private void OnDestroy()
        {
            Block.OnCreated -= CreatedBlock;
        }

        private void OnCollisionEnter2D(Collision2D other) { }

        #endregion

        #region Private methods

        private void CreatedBlock(Block block)
        {
            Debug.Log("Созданный неразрушаемый блок " + block.name);
        }

        #endregion
    }
}