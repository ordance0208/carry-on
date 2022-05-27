using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapperPlatformController : MonoBehaviour
{
    private List<GameObject> ZapsLeft = new List<GameObject>();
    private List<GameObject> ZapsRight = new List<GameObject>();
    private List<GameObject> ZapsAll = new List<GameObject>();
    private float inputX;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    public void SetZapLists(List<GameObject> listOfLefts = null, List < GameObject> listOfRights = null)
    {
        ZapsLeft.AddRange(listOfLefts);
        ZapsRight.AddRange(listOfRights);
        ZapsAll.AddRange(ZapsRight);
        ZapsAll.AddRange(ZapsLeft);
    }

    private void Update()
    {
        if (ZapsAll == null) return; //Ovoj return e za ako ZapMachineGeneratorot ne ja povika SetZapLists pred prviot Update
        
        inputX = -ButtonsInput.InputValue;

        inputX = !_renderer.isVisible ? 0 : inputX;

        if (inputX < 0) ZapControl(ZapsRight, ZapsLeft);
        else if (inputX > 0) ZapControl(ZapsLeft, ZapsRight);
        else ZapControl(ZapsAll);
    }
    private void ZapControl(List<GameObject> zapToEnable, List<GameObject> zapToDisable = null)
    {
        foreach (GameObject obj in zapToEnable)
        { obj.SetActive(true); }
        if (zapToDisable == null) return; //Ovoj return e za koga zapToDisable e null nema potreba da proveruva ponatamu
        foreach(GameObject obj in zapToDisable)
        { obj.SetActive(false); }
    }
}
