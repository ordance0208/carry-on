using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxSpeed = 15f;
    private Rigidbody rb;
    public Vector2 CurrentSpeed
    {
        get
        {
            return new Vector2(rb.velocity.x, maxSpeed * maxSpeedMultiplier);
        }
    }
    private bool allowedToMove;
    private float speedMultiplier;
    private float maxSpeedMultiplier;

    private void Start()
    {
        LevelDifficulty.Instance.DifficultyChanged += SetUpSpeed;
        rb = GetComponent<Rigidbody>();
    }

    private void SetUpSpeed()
    {
        speedMultiplier = LevelDifficulty.Instance.Speed_m;
        maxSpeedMultiplier = LevelDifficulty.Instance.MaxSpeed_m;
    }

    private void FixedUpdate()
    {
        if (allowedToMove && rb.velocity.x < maxSpeed * maxSpeedMultiplier)
            rb.AddForce(Vector3.right * speed * speedMultiplier * Time.fixedDeltaTime, ForceMode.VelocityChange);
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.layer == 6 || col.gameObject.layer == 7)
        {
            SoundHandler.Instance.PlayAudio(SoundEffect.Bounce);
            allowedToMove = true;
        }
    }
}
