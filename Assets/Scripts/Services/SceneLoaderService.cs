using Arkanoid.Utility;
using UnityEngine.SceneManagement;

namespace Arkanoid.Services
{
    public class SceneLoaderService : SingletonMonoBehaviour<SceneLoaderService>
    {
        #region Public methods

        public static void LoadNextLevelTest(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        #endregion
    }
}