namespace Smoothing3D
{
    using UnityEngine;
    using UnityEngine.Networking;

    [RequireComponent(typeof(Rigidbody))]
    internal sealed class PhysicsPlayer3D : NetworkBehaviour
    {
        private Rigidbody rb;

        private void Awake()
        {
            this.rb = this.GetComponent<Rigidbody>();
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

            this.rb.AddForce(input.x, 0f, input.y);
        }
    }
}