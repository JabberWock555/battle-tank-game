using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace BattleTank.AchievementSystem
{
    public class AchievementPanel : MonoBehaviour
    {
        [SerializeField] private Text AchievementMessage;
        [SerializeField] private float panelTimeLimit = 3f;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void ShowAchievement(string message)
        {
            gameObject.SetActive(true);
            anim.SetBool("Show", true);
            StartCoroutine(GetPanel(message));
        }

        private IEnumerator GetPanel(string text)
        {
            AchievementMessage.text = text;
            yield return new WaitForSeconds(panelTimeLimit);
            anim.SetBool("Hide", true);
            anim.SetBool("Show", false);
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            anim.SetBool("Hide", false);
            anim.SetBool("Show", false);
        }

    }
}