using System;
using System.Collections.Generic;
using Arkanoid.Game;
using Arkanoid.Utility;

namespace Arkanoid.Services
{
    public class LevelService : SingletonMonoBehaviour<LevelService>
    {
        #region Variables

        private readonly List<Ball> _balls = new();

        private readonly List<Block> _blocks = new();

        #endregion

        #region Events

        public event Action OnAllBlocksDestroyed;

        #endregion

        #region Properties

        public Ball Ball { get; private set; }
        public IReadOnlyList<Block> Blocks => _blocks;
        public List<Ball> Balls => _balls;


        #endregion

        #region Unity lifecycle

        protected override void Awake()
        {
            base.Awake();

            Block.OnCreated += BlockCreatedCallback;
            Block.OnDestroyed += BlockDestroyedCallback;

            Ball.OnCreated += BallCreatedCallback;
            Ball.OnDestroyed += BallDestroyedCallback;
        }

        private void OnDestroy()
        {
            Block.OnCreated -= BlockCreatedCallback;
            Block.OnDestroyed -= BlockDestroyedCallback;

            Ball.OnCreated -= BallCreatedCallback;
            Ball.OnDestroyed -= BallDestroyedCallback;
        }

        #endregion

        #region Public methods

     

        #endregion

        #region Private methods

        private void BallCreatedCallback(Ball ball)
        {
            Ball = ball;
            _balls.Add(ball);
        }

        private void BallDestroyedCallback(Ball ball)
        {
            Ball = null;
            _balls.Remove(ball);
            if (_balls.Count == 0)
            {
                GameService.Instance.CheckGameEnd();
            }
        }

        private void BlockCreatedCallback(Block block)
        {
            _blocks.Add(block);
        }

        private void BlockDestroyedCallback(Block block)
        {
            _blocks.Remove(block);

            if (_blocks.Count == 0)
            {
                OnAllBlocksDestroyed?.Invoke();
            }
        }

        #endregion
    }
}