namespace Shooting2D
{
    using UnityEngine;
    using UnityEngine.Networking;

    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class ShootingPlayer : NetworkBehaviour
    {
        private new Camera camera;
        private Rigidbody2D rb;

        [SerializeField]
        private Bullet bulletPrefab;

        [SerializeField]
        private Transform bulletSpawn;

        [SerializeField]
        private Healthbar healthbar;

        [SerializeField]
        private float speed = 1f, maxHealth = 100f;

        [SyncVar(hook = "OnHealthChanged")]
        private float health;

        private float Health
        {
            get { return this.health; }
            set { this.health = Mathf.Clamp(value, 0f, this.maxHealth); }
        }

        private void Awake()
        {
            this.camera = Camera.main;
            this.rb = this.GetComponent<Rigidbody2D>();
        }

        [ServerCallback]
        private void OnEnable()
        {
            this.health = this.maxHealth;
        }

        [ServerCallback]
        private void OnDisable()
        {
            this.health = 0f;
        }

        private void Start()
        {
            Debug.Log(this.health);
            this.UpdateHealthbar(this.health);
        }

        private void Update()
        {
            if (this.isLocalPlayer)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    this.CmdShoot();
                }
            }
        }

        private void FixedUpdate()
        {
            if (this.isLocalPlayer)
            {
                var input = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"));

                this.rb.AddForce(input * this.speed);

                var screenPos = this.camera.WorldToScreenPoint(this.rb.position);
                var mousePos = Input.mousePosition;
                var lookDirection = (mousePos - screenPos).normalized;
                this.rb.rotation = RotationHelper.DirectionToPhysicsRotation(lookDirection);
            }
        }

        [Server]
        public void Damage(float damage)
        {
            this.Health -= damage;
        }

        [Command]
        private void CmdShoot()
        {
            var bullet = ShootingPlayer.Instantiate(this.bulletPrefab);
            bullet.Initialize(this.bulletSpawn.position, this.transform.rotation);
            NetworkServer.Spawn(bullet.gameObject);
        }

        private void OnHealthChanged(float health)
        {
            this.UpdateHealthbar(health);
        }

        private void UpdateHealthbar(float health)
        {
            this.healthbar.FillAmount = health / this.maxHealth;
        }
    }
}