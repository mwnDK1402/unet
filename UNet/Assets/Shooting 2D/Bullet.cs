namespace Shooting2D
{
    using UnityEngine;
    using UnityEngine.Networking;

    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class Bullet : NetworkBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField]
        private float speed = 1f, damage = 40f;

        public void Initialize(Vector2 position, Quaternion rotation)
        {
            this.transform.position = position;
            this.transform.rotation = rotation;
            this.rb.rotation = RotationHelper.TransformToPhysicsRotation(this.transform);
            this.rb.position = position;
            this.rb.AddRelativeForce(Vector2.up * this.speed, ForceMode2D.Impulse);
        }

        private void Awake()
        {
            this.rb = this.GetComponent<Rigidbody2D>();
        }

        [ServerCallback]
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<ShootingPlayer>();
            if (player)
            {
                player.Damage(this.damage);
                NetworkServer.Destroy(this.gameObject);
            }
        }
    }
}