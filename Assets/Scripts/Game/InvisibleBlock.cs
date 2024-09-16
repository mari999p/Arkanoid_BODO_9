using UnityEngine;

namespace Arkanoid.Game
{
    public class InvisibleBlock : MonoBehaviour
    {
        #region Variables

        [SerializeField] private SpriteRenderer _spriteRenderer;
        private bool _isVisible;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _spriteRenderer.enabled = false;
            Block.OnCreated += CreatedBlock;
        }

        private void OnDestroy()
        {
            Block.OnCreated -= CreatedBlock;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_isVisible)
            {
                _isVisible = true;
                _spriteRenderer.enabled = true;
            }
        }

        #endregion

        #region Private methods

        private void CreatedBlock(Block block)
        {
            Debug.Log("Созданный невидимый блок " + block.name);
        }

        #endregion
    }
}