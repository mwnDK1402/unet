namespace Smoothing2D
{
    using UnityEngine;
    using UnityEngine.Networking;

    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class PhysicsPlayer2D : NetworkBehaviour
    {
        private new Camera camera;
        private Rigidbody2D rb;

        private void Awake()
        {
            this.camera = Camera.main;
            this.rb = this.GetComponent<Rigidbody2D>();
        }

        public override void OnStartLocalPlayer()
        {
            this.enabled = this.isLocalPlayer;
        }

        private void FixedUpdate()
        {
            var input = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"));

            this.rb.AddForce(input);

            var screenPos = this.camera.WorldToScreenPoint(this.rb.position);
            var mousePos = Input.mousePosition;
            var lookDirection = (mousePos - screenPos).normalized;
            this.rb.rotation = (-Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg);
        }
    }
}