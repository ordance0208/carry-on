using UnityEngine;

public class ShopCamera : MonoBehaviour
{    
    [SerializeField] private Transform firstCamPos, secondCamPos, player;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private float normalMoveDistance = 15f;
    [SerializeField] private float alternatingMoveDistnace = 1f;
    private float moveDistance = 8f;
    private bool alternateBetweenPoints = false;
    private Vector3 targetPos;
    private Vector3 startingPos;

    private void OnEnable()
    {
        Camera.main.orthographic = false;
        targetPos = firstCamPos.position;
        startingPos = transform.position;
    }

    private void OnDisable()
    {
        Camera.main.orthographic = true;               
    }

    private void Update()
    {
        transform.LookAt(player);
        
        if(Vector3.Distance(transform.position, targetPos) <= 1f)
        {
            if (!alternateBetweenPoints && targetPos != startingPos) { alternateBetweenPoints = true; }     
            
            if(targetPos == startingPos)
            {
                uiManager.ToggleSkinSelector();
                this.enabled = false;
                return;
            }

            targetPos = ChangeCamPos().position;
        }

        moveDistance = alternateBetweenPoints ? alternatingMoveDistnace : normalMoveDistance;        
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveDistance * Time.deltaTime);
    }

    public void MoveBack()
    {
        alternateBetweenPoints = false;
        targetPos = startingPos;
    }

    private Transform ChangeCamPos()
    {
        return targetPos == firstCamPos.position ? secondCamPos : firstCamPos;
    }
}
