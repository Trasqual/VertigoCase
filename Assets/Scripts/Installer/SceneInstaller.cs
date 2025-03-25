using RewardSystem;
using ServiceLocatorSystem;
using UnityEngine;

namespace Installer
{
    public static class SceneInstaller
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Install()
        {
            ServiceLocator.Instance.RegisterService(new RewardManager());
        }
    }
}
