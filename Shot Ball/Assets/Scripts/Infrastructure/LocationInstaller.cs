using Zenject;
using UnityEngine;
using EntitySystem.PlayerSystem;
using FactorySystem;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        [Header("----- Player Settings Installer -----")]
        [SerializeField] private Transform _startPoint;
        [SerializeField] private GameObject _playerPrefab;
        [Space(5)]
        [Header("----- Bullet Factory Settings Installer -----")]
        [SerializeField] private BulletFactory _bulletFactory;
        [Space(5)]
        [Header("----- Finish Settings Installer -----")]
        [SerializeField] private Finish _finish;
        public override void InstallBindings()
        {
            BindFinishPos();
            BindPlayer();
            BindBulletFactory();
        }
        private void BindPlayer()
        {
            Player player = Container
                .InstantiatePrefabForComponent<Player>(_playerPrefab, _startPoint.position, Quaternion.identity, null);

            Container
                .Bind<Player>()
                .FromInstance(player)
                .AsSingle();
        }
        private void BindBulletFactory()
        {
            Container
               .Bind<BulletFactory>()
               .FromInstance(_bulletFactory)
               .AsSingle();
        }
        private void BindFinishPos()
        {
            Container
               .Bind<Finish>()
               .FromInstance(_finish)
               .AsSingle();
        }
    }
}