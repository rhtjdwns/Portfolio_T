using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [Header("�ƾ� ������Ʈ")]
    [SerializeField] public Image[] sprites;

    [Header("�ƾ� UI")]
    [SerializeField] private GameObject sceneUI;

    [Header("���̵� �г�")]
    [SerializeField] private Image FadePanel;

    [Header("�÷��̾�")]
    [SerializeField] private Player _player;

    [Header("Ʃ�丮�� �� UI")]
    [SerializeField] private GameObject MovingUI;
    [SerializeField] private GameObject AttackUI;
    [SerializeField] private GameObject CommandUI;

    [Header("Hit it")]
    [SerializeField] private GameObject Hitit;

    [Header("Ÿ�� ����")]
    [SerializeField] private GameObject timeLine;

    private bool isCutScene;
    private Coroutine cutSceneCoroutine;
    private bool isKey = false;
    private float timer = 0f;

    private void Start()
    {
        SetPlayerControll(true);
        StartCoroutine(FadeOut());
    }

    private void LateUpdate()
    {
        if (isKey)
        {
            if (timer < 5f)
            {
                timer += Time.deltaTime;
                return;
            }

            if (Input.anyKeyDown || PlayerInputManager.Instance.commandValue.Count > 0)
            {
                PlayerInputManager.Instance.ResetCommandKey();
                _player.PlayerSt.IsKnockedBack = false;
                _player.Attack.isAttack = true;
                isKey = false;
                SetCommandUI(false);
            }
        }
    }

    public void SetMovingUI(bool isTurn)
    {
        MovingUI.SetActive(isTurn);
    }

    public void SetAttackUI(bool isTurn)
    {
        AttackUI.SetActive(isTurn);
    }

    public void SetCommandUI(bool isTurn)
    {
        CommandUI.SetActive(isTurn);
    }

    private IEnumerator FadeOut()
    {
        float alpha = 1;
        isCutScene = false;

        //while (FadePanel.color.a > 0)
        //{
        //    alpha -= Time.fixedDeltaTime;
        //    FadePanel.color = new Color(0, 0, 0, alpha);

        //    yield return null;
        //}

        timeLine.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        Hitit.SetActive(true);

        yield return new WaitForSeconds(2f);

        Hitit.SetActive(false);
        SetPlayerControll(false);

        TestSound.Instance.PlaySound("Start");

        yield return null;
    }

    public void SkipCutScene()
    {
        if (cutSceneCoroutine != null)
        {
            StopCoroutine(cutSceneCoroutine);
        }
        sceneUI.SetActive(false);

        StartCoroutine(FadeOut());
    }

    public void SetPlayerControll(bool isKnockBack)
    {
        _player.PlayerSt.IsKnockedBack = isKnockBack;

        //if (isKnockBack)
        //{
        //    _player.Controller.isMove = false;
        //    _player.Controller.isJump = false;
        //    _player.Attack.isAttack = false;
        //}
        //else
        //{
        //    _player.Controller.isMove = true;
        //    _player.Controller.isJump = true;
        //    _player.Attack.isAttack = true;
        //}
    }

    public void SetPlayerControll(bool isKnockBack, bool isKey)
    {
        _player.PlayerSt.IsKnockedBack = isKnockBack;
        _player.Attack.isAttack = false;
        _player.Rb.velocity = Vector3.zero;
        this.isKey = isKey;
    }
}
