using System.Threading.Tasks;
using Assets.Scripts.Common;
using UnityEngine;


public class TutorialPanel : Panel
{
    [SerializeField] private Animator animator;

    private AnimatorStateInfo _animatorStateInfo;

    private void Start()
    {
        SoundController.Instance.PlayOneShotAudio(GameConfig.OPEN_POPUP_AUDIO);
        _animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (GamePlayController.Instance != null)
        {
            animator.enabled = false;
        }
        else
        {
            animator.enabled = true;
            Messenger.Broadcast<float>(EventKey.HIDE_BOTTOM_AND_PUSH_UP_TITLE, _animatorStateInfo.length);
        }
    }

    public async void Back()
    {
        SoundController.Instance.PlayOneShotAudio(GameConfig.TAP_AUDIO);
        if (GamePlayController.Instance != null)
        {
            GamePlayController.Instance.checkContinue = true;
        }
        else
        {
            animator.SetBool("isClose", true);
            Messenger.Broadcast<float>(EventKey.SHOW_BOTTOM_AND_PUSH_UP_TITLE, _animatorStateInfo.length);
            await Task.Delay((int)(_animatorStateInfo.length * 1000));
        }

        PanelManager.Instance.ClosePanel(GameConfig.TUTORIAL_PANEL);
    }
}
