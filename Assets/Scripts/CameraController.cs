using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraOffset = 10;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Transform target;
    [SerializeField] private AddForce sphereSpeed;
    [SerializeField] private float expandThreshold;
    [SerializeField] private float changeAmount;
    [SerializeField] private Vector2 minMaxExpand;
    private float previousGradient;
    private Camera cam;
    public Vector3 cameraViewSize
    {
        get
        {
            if (cam)
            {
                return new Vector3(minMaxExpand.x, minMaxExpand.y, cam.orthographicSize);
            }
            else return Vector3.zero;
        }
    }

    private void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    private void Start()
    {
        LevelDifficulty.Instance.DifficultyChanged += SetUpBackground;
        Vector3 playerPosition = LevelDifficulty.Instance.Player.position;
        Vector3 backwards = -transform.forward;
    }

    private void SetUpBackground()
    {
        StartCoroutine(LerpGradient(previousGradient));
        previousGradient = LevelDifficulty.Instance.BackgroundColor_m;

    }

    private IEnumerator LerpGradient(float previousGradient)
    {
        while (previousGradient <= LevelDifficulty.Instance.BackgroundColor_m)
        {
            yield return new WaitForSeconds(0.1f);
            previousGradient = Mathf.Lerp(previousGradient, LevelDifficulty.Instance.BackgroundColor_m, 0.1f);
            cam.backgroundColor = gradient.Evaluate(previousGradient);
        }


    }


    private void LateUpdate()
    {
        MoveCamera();
        ExpandView();
    }
    
    private void MoveCamera()
    {
        if (GameManager.Instance._GameState == GameState.Play)
        {
            Vector3 playerPosition = target.position;
            Vector3 backwards = -transform.forward;
            transform.position = Vector3.Lerp(transform.position, playerPosition + backwards * cameraOffset, 5f * Time.deltaTime);
        }
    }

    private void ExpandView()
    {
        if (sphereSpeed.CurrentSpeed.x > expandThreshold * sphereSpeed.CurrentSpeed.y)
        {
            if (cam.orthographicSize < minMaxExpand.y)
            cam.orthographicSize += changeAmount * Time.fixedDeltaTime;
        }
        else 
        {
            if (cam.orthographicSize > minMaxExpand.x)
            cam.orthographicSize -= changeAmount * Time.fixedDeltaTime;
        }

    }

    public void UpdateBackgroundColor()
    {
        Color temp = Color.red;
        gameObject.GetComponent<Camera>().backgroundColor = temp; 
    }
}
