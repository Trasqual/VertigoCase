using EventSystem;
using PoolingSystem;
using RewardSystem;
using ServiceLocatorSystem;
using UISystem.Core;
using UnityEngine;

namespace Installers
{
    public static class SceneInstaller
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Install()
        {
            ServiceLocator.Instance.RegisterService(new EventManager());
            ServiceLocator.Instance.RegisterService(new ObjectPoolManager());
            ServiceLocator.Instance.RegisterService(new UIManager());
        }
    }
}
