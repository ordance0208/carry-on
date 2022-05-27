using UnityEngine;

public class ButtonsInput : MonoBehaviour
{
    [SerializeField] private SphereController sphereController;

    [SerializeField] private int leftButtonLayer;
    [SerializeField] private int rightButtonLayer;

    private bool leftButtonPressed;
    private bool rightButtonPressed;

    public static float InputValue { get; private set; }

    public void ButtonPressedDown(GameObject button)
    {
        if (button.layer == leftButtonLayer) { leftButtonPressed = true; }
        else { rightButtonPressed = true; }
        CalculateInput();
    }
    public void ButtonUnpressed(GameObject button)
    {
        if (button.layer == leftButtonLayer) { leftButtonPressed = false; }
        else { rightButtonPressed = false; }
        CalculateInput();
    }

    private void CalculateInput()
    {
        if (sphereController.PlayerDead)
        {
            rightButtonPressed = false;
            leftButtonPressed = false;
        }

        if (leftButtonPressed && !rightButtonPressed) { InputValue = -1; }
        else if (!leftButtonPressed && rightButtonPressed) { InputValue = 1; }
        else { InputValue = 0; }
    }
}
