using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rescorce_Display : MonoBehaviour
{
    [Header("Parametres")]
    public Image human;
    public Image food;
    public Image animal;
    public Image atmosphere;
    public Image soil;

    public Image actionPointsO;
    public Image actionPointsC;

    public GameObject openButton;
    public GameObject closeButton;

    public Animator anim;


    public float ap = 0;

    private void Awake()
    {
        human = human.GetComponent<Image>();
        food = food.GetComponent<Image>();
        animal = animal.GetComponent<Image>();
        atmosphere = atmosphere.GetComponent<Image>();
        soil = soil.GetComponent<Image>();

        actionPointsO = actionPointsO.GetComponent<Image>();
        actionPointsC = actionPointsC.GetComponent<Image>();
    }


    private void Update()
    {

        actionPointsO.fillAmount = GameController.instance.actionPoints / 5;
        actionPointsC.fillAmount = GameController.instance.actionPoints / 5;

        human.fillAmount = GameController.worldValues["humans"] / 100;
        human.fillAmount = GameController.worldValues["food"] / 100;
        human.fillAmount = GameController.worldValues["atmosphere"] / 100;
        human.fillAmount = GameController.worldValues["animals"] / 100;
        human.fillAmount = GameController.worldValues["soil"] / 100;
    }

    public void Open()
    {
        anim.SetBool("Open", true);
        openButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
    }

    public void Close()
    {
        anim.SetBool("Open", false);
        openButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(false);
    }


}
