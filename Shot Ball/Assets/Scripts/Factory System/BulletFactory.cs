using EntitySystem;

namespace FactorySystem
{
    public class BulletFactory : GenericFactory<Bullet>
    {
        public event System.Action<Bullet> On—ontaminatorActiveEvent;

        [UnityEngine.HideInInspector]public Bullet currentBullet;

        public void SetBullet()
        {
            currentBullet = SpawnBullet();
            On—ontaminatorActiveEvent?.Invoke(currentBullet);
        }

        private Bullet SpawnBullet()
        {
            Bullet bullet = GetNewInstance().GetComponent<Bullet>();
            return bullet;
        }
    }
}