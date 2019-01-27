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
    }


    private void Update()
    {
        if (actionPointsO != null)
            actionPointsO.fillAmount = GameController.instance.actionPoints / 5f;
        if (actionPointsC != null)
            actionPointsC.fillAmount = GameController.instance.actionPoints / 5f;

        if (GameController.worldValues.Count < 1)
            return;

        human.fillAmount = GameController.worldValues["humans"] / 100f;
        food.fillAmount = GameController.worldValues["food"] / 100f;
        atmosphere.fillAmount = GameController.worldValues["atmosphere"] / 100f;
        animal.fillAmount = GameController.worldValues["animals"] / 100f;
        soil.fillAmount = GameController.worldValues["soil"] / 100f;
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
