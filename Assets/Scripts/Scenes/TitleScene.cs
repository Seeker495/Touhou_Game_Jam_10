using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject m_soundManager;
    [SerializeField] private GameObject m_fadeObject;
    [SerializeField] private GameObject m_focusObject;
    private List<GameObject> m_titleItems = new List<GameObject>();
    private List<System.Action> m_titleItemFunctions = new List<System.Action>();
    private int m_index;
    // Start is called before the first frame update
    void OnEnable()
    {
        SoundPlayer.SetUp(Instantiate(m_soundManager, null));
        SoundPlayer.PlayBGM(eBGM.TITLE);
#if !UNITY_EDITOR
        Fade.SetUp(m_fadeObject);
        Fade.FadeIn(2.0f);
#endif
        m_titleItems.AddRange(GameObject.FindGameObjectsWithTag("Button"));
        m_titleItemFunctions.Add(PressStart);
        m_titleItemFunctions.Add(PressOption);
        m_titleItemFunctions.Add(PressQuit);

        PlayerController.CONTROLLER.UI.SelectUp.started += SelectUp;
        PlayerController.CONTROLLER.UI.SelectDown.started += SelectDown;
        PlayerController.CONTROLLER.UI.Enter.started += Enter;
        PlayerController.CONTROLLER.UI.Enable();

    }

    private void OnDisable()
    {
        PlayerController.CONTROLLER.UI.SelectUp.started -= SelectUp;
        PlayerController.CONTROLLER.UI.SelectDown.started -= SelectDown;
        PlayerController.CONTROLLER.UI.Enter.started -= Enter;
        PlayerController.CONTROLLER.UI.Disable();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SelectUp(InputAction.CallbackContext context)
    {
        m_index = (--m_index + m_titleItems.Count) % m_titleItems.Count;
        FocusItem();
    }

    private void SelectDown(InputAction.CallbackContext context)
    {
        m_index = ++m_index % m_titleItems.Count;
        FocusItem();
    }

    private void Enter(InputAction.CallbackContext context)
    {
        m_titleItemFunctions[m_index]();
    }

    private void PressStart()
    {
#if !UNITY_EDITOR
        Fade.FadeOut_with_Scene(this, "Play", 2.0f);
#else
        SceneManager.LoadSceneAsync("Play");
#endif
    }

    private void PressOption()
    {

    }

    private void PressQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void FocusItem()
    {
        m_focusObject.transform.DOLocalMoveY((-120.0f * m_index) + -117.0f, 0.2f);
    }
}
