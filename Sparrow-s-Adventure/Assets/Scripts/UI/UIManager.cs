using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Value")]
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI waveText;

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI attackPowerText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;

    [Header("PromptText")]
    [SerializeField] private TextMeshProUGUI promptText;

    private bool isShowingPrompt = false;

    private void Start()
    {
        UpdateStatText();
        UpdateNextWaveValText();
    }

    public void UpdateStatText()
    {
        attackPowerText.text = CharacterManager.Instance.Player.Data.PlayerAttackData.PlayerAttackPower.ToString();
        healthText.text = CharacterManager.Instance.Player.Condition.nowHealth.ToString();
        attackSpeedText.text = CharacterManager.Instance.Player.Data.PlayerAttackData.AttackSpeed.ToString("F1");
    }

    public void UpdateNextWaveValText()
    {
        waveText.text = GameManager.Instance.Wave.ToString();
        pointText.text = GameManager.Instance.Point.ToString();
    }

    public void OnclickStatBtn(int index)
    {
        if (GameManager.Instance.Point <= 0) 
        {
            StartCoroutine(ShowPromptText());
            return;
        }
        switch(index)
        {
            case 0:
                CharacterManager.Instance.Player.Data.PlayerAttackData.PlayerAttackPower++;
                break;
            case 1:
                CharacterManager.Instance.Player.Condition.Heal(10); 
                break;
            case 2:
                CharacterManager.Instance.Player.Data.PlayerAttackData.AttackSpeed+=0.1f;
                break;
        }
        GameManager.Instance.UsePoint();
        UpdateStatText();
        UpdateNextWaveValText();
    }

    IEnumerator ShowPromptText()
    {
        if (isShowingPrompt) yield break;

        promptText.gameObject.SetActive(true );
        isShowingPrompt = true;
        yield return new WaitForSeconds(1f);
        promptText.gameObject.SetActive(false);
        isShowingPrompt = false;
    }
}
