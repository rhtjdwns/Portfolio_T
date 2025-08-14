using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

public class UIController : MonoBehaviour
{
    [Header("User Button")]
    [SerializeField] private Button onLoginUIButton;
    [SerializeField] private Button onRegisterUIButton;
    [SerializeField] private Button onLoginButton;
    [SerializeField] private Button onRegisterButton;
    [SerializeField] private Button onBackButton;

    [Header("User UI")]
    [SerializeField] private TMP_InputField usernameLoginInputField;
    [SerializeField] private TMP_InputField passwordLoginInputField;
    [SerializeField] private TMP_InputField usernameRegisterInputField;
    [SerializeField] private TMP_InputField passwordRegisterInputField;
    [SerializeField] private GameObject loginUI;
    [SerializeField] private GameObject registerUI;
    [SerializeField] private TMP_Text loginStateText;
    [SerializeField] private TMP_Text registerStateText;
    [SerializeField] private GameObject mainUI;

    [Header("Game Button")]
    [SerializeField] private Button onLobbyButton;
    [SerializeField] private Button onItemButton;
    [SerializeField] private Button onBackMainButton;

    [Header("Game UI")]
    [SerializeField] private GameObject mainGameUI;
    [SerializeField] private GameObject lobbyUI;
    [SerializeField] private GameObject itemUI;

    [Header("User Info UI")]
    [SerializeField] private TMP_Text maxHpText;
    [SerializeField] private TMP_Text attackDamageText;
    [SerializeField] private TMP_Text bossGradeText;
    private APIClient.UserInfo userInfo;

    private void Start()
    {
        onLoginUIButton.onClick.AddListener(OnLoginUI);
        onRegisterUIButton.onClick.AddListener(OnRegisterUI);
        onLoginButton.onClick.AddListener(() => OnLoginButton().Forget());
        onRegisterButton.onClick.AddListener(() => OnRegisterButton().Forget());
        onBackButton.onClick.AddListener(OnBackUI);

        onBackMainButton.onClick.AddListener(OnMainGameUI);
        onLobbyButton.onClick.AddListener(OnLobbyUI);
        onItemButton.onClick.AddListener(OnItemUI);
    }

    private void Update()
    {
        if (userInfo != null)
        {
            maxHpText.text = userInfo.maxHp.ToString();
            attackDamageText.text = userInfo.attackDamage.ToString();
            bossGradeText.text = userInfo.bossGrade.ToString();
        }
    }

    private void OnItemUI()
    {
        mainGameUI.SetActive(false);
        itemUI.SetActive(true);
        lobbyUI.SetActive(false);
        onBackMainButton.gameObject.SetActive(true);
    }

    private void OnLobbyUI()
    {
        mainGameUI.SetActive(false);
        itemUI.SetActive(false);
        lobbyUI.SetActive(true);
        onBackMainButton.gameObject.SetActive(true);
    }

    private void OnMainGameUI()
    {
        mainGameUI.SetActive(true);
        itemUI.SetActive(false);
        lobbyUI.SetActive(false);
        onBackMainButton.gameObject.SetActive(false);
    }

    private void OnLoginUI()
    {
        loginUI.SetActive(true);
    }

    private void OnRegisterUI()
    {
        registerUI.SetActive(true);
    }

    private void OnBackUI()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
    }

    private async UniTaskVoid OnLoginButton()
    {
        if (await MatchManager.instance.OnLogin(usernameLoginInputField, passwordLoginInputField, loginStateText))
        {
            mainUI.SetActive(false);

            userInfo = MatchManager.instance.clientUserInfo;
            maxHpText.text = userInfo.maxHp.ToString();
            attackDamageText.text = userInfo.attackDamage.ToString();
            bossGradeText.text = userInfo.bossGrade.ToString();
        }
    }

    private async UniTaskVoid OnRegisterButton()
    {
        if (await MatchManager.instance.OnRegister(usernameRegisterInputField, passwordRegisterInputField, registerStateText))
        {
            mainUI.SetActive(false);
        }
    }
}
