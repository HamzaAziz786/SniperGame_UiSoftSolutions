using System.Collections;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CompleteAnimator : MonoBehaviour
{
	public float statsVal,statsVal2,statsVal3;
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public float stat1;

		public CompleteAnimator _003C_003E4__this;

		public float stat2;

		public float stat3;

		internal float _003CAnimationSequence_003Eb__0()
		{
			return stat1;
		}

		internal void _003CAnimationSequence_003Eb__1(float x)
		{
			stat1 = x;
		}

		internal void _003CAnimationSequence_003Eb__2()
		{
			_003C_003E4__this.statsText[0].text = stat1.ToString("0");
		}

		internal float _003CAnimationSequence_003Eb__3()
		{
			return stat2;
		}

		internal void _003CAnimationSequence_003Eb__4(float x)
		{
			stat2 = x;
		}

		internal void _003CAnimationSequence_003Eb__5()
		{
			_003C_003E4__this.statsText[1].text = stat2.ToString("0");
		}

		internal float _003CAnimationSequence_003Eb__6()
		{
			return stat3;
		}

		internal void _003CAnimationSequence_003Eb__7(float x)
		{
			stat3 = x;
		}

		internal void _003CAnimationSequence_003Eb__8()
		{
			_003C_003E4__this.statsText[2].text = stat3.ToString("0");
		}

		internal void _003CAnimationSequence_003Eb__9()
		{
			_003C_003E4__this.subStats[0].GetChild(0).GetComponent<Text>().text = "2000";
		}

		internal void _003CAnimationSequence_003Eb__10()
		{
			_003C_003E4__this.subStats[1].GetChild(0).GetComponent<Text>().text = "20";
		}

		internal void _003CAnimationSequence_003Eb__11()
		{
			_003C_003E4__this.subStats[2].GetChild(0).GetComponent<Text>().text = "1050";
		}
	}

	public Text[] statsText;

	public GameObject[] checkImages;

	public Image playerLevelFillbar;

	public GameObject[] gamePlayButtons;

	public Transform[] subStats;

	public Transform[] subStatsInitialPos;

	public Transform[] subStatsTargetPos;

	private void OnEnable()
	{
		StartCoroutine("AnimationSequence");
		playerLevelFillbar.fillAmount=PlayerPrefs.GetFloat("PlayerRankFillBar");
	}
	public bool isLevelCompletePanel;
	private IEnumerator AnimationSequence()
	{
		_003C_003Ec__DisplayClass8_0 _003C_003Ec__DisplayClass8_ = new _003C_003Ec__DisplayClass8_0();
		_003C_003Ec__DisplayClass8_._003C_003E4__this = this;
		GetComponent<CanvasGroup>().DOFade(1f, 1f);
		yield return new WaitForSecondsRealtime(1.2f);
		_003C_003Ec__DisplayClass8_.stat1 = 0f;
		DOTween.To(_003C_003Ec__DisplayClass8_._003CAnimationSequence_003Eb__0, _003C_003Ec__DisplayClass8_._003CAnimationSequence_003Eb__1, statsVal, 1f).OnUpdate(_003C_003Ec__DisplayClass8_._003CAnimationSequence_003Eb__2);
		yield return new WaitForSecondsRealtime(1f);
		checkImages[0].gameObject.SetActive(true);
		statsText[0].transform.DOPunchScale(new Vector3(0.35f, 0.35f, 0.35f), 0.3f, 1);
		yield return new WaitForSecondsRealtime(0.31f);
		yield return new WaitForSecondsRealtime(0.1f);
		_003C_003Ec__DisplayClass8_.stat2 = 0f;
		DOTween.To(_003C_003Ec__DisplayClass8_._003CAnimationSequence_003Eb__3, _003C_003Ec__DisplayClass8_._003CAnimationSequence_003Eb__4, statsVal2, 1f).OnUpdate(_003C_003Ec__DisplayClass8_._003CAnimationSequence_003Eb__5);
		yield return new WaitForSecondsRealtime(1f);
		checkImages[1].gameObject.SetActive(true);
		statsText[1].transform.DOPunchScale(new Vector3(0.35f, 0.35f, 0.35f), 0.3f, 1);
		yield return new WaitForSecondsRealtime(0.31f);
		yield return new WaitForSecondsRealtime(0.2f);
		_003C_003Ec__DisplayClass8_.stat3 = 0f;
		DOTween.To(_003C_003Ec__DisplayClass8_._003CAnimationSequence_003Eb__6, _003C_003Ec__DisplayClass8_._003CAnimationSequence_003Eb__7, statsVal3, 1f).OnUpdate(_003C_003Ec__DisplayClass8_._003CAnimationSequence_003Eb__8);
		yield return new WaitForSecondsRealtime(1f);
		checkImages[2].gameObject.SetActive(true);
		statsText[2].transform.DOPunchScale(new Vector3(0.35f, 0.35f, 0.35f), 0.3f, 1);
		yield return new WaitForSecondsRealtime(0.31f);
		subStats[0].DOLocalMove(subStatsTargetPos[0].localPosition, 0.2f).SetEase(Ease.Linear);
		subStats[0].DOScale(subStatsTargetPos[0].localScale, 0.2f).SetEase(Ease.Linear);
		yield return new WaitForSecondsRealtime(0.05f);
		subStats[1].DOLocalMove(subStatsTargetPos[1].localPosition, 0.2f).SetEase(Ease.Linear);
		subStats[1].DOScale(subStatsTargetPos[1].localScale, 0.2f).SetEase(Ease.Linear);
		yield return new WaitForSecondsRealtime(0.25f);
		subStats[0].DOLocalMove(subStatsInitialPos[0].localPosition, 0.1f).SetEase(Ease.Linear);
		subStats[0].DOScale(subStatsInitialPos[0].localScale, 0.1f).SetEase(Ease.Linear).OnComplete(_003C_003Ec__DisplayClass8_._003CAnimationSequence_003Eb__9);
		subStats[2].DOLocalMove(subStatsTargetPos[2].localPosition, 0.2f).SetEase(Ease.Linear);
		subStats[2].DOScale(subStatsTargetPos[2].localScale, 0.2f).SetEase(Ease.Linear);
		yield return new WaitForSecondsRealtime(0.21f);
		subStats[1].DOLocalMove(subStatsInitialPos[1].localPosition, 0.1f).SetEase(Ease.Linear);
		subStats[1].DOScale(subStatsInitialPos[1].localScale, 0.1f).SetEase(Ease.Linear).OnComplete(_003C_003Ec__DisplayClass8_._003CAnimationSequence_003Eb__10);
		yield return new WaitForSecondsRealtime(0.08f);
		subStats[2].DOLocalMove(subStatsInitialPos[2].localPosition, 0.1f).SetEase(Ease.Linear);
		subStats[2].DOScale(subStatsInitialPos[2].localScale, 0.1f).SetEase(Ease.Flash).OnComplete(_003C_003Ec__DisplayClass8_._003CAnimationSequence_003Eb__11);
		yield return new WaitForSecondsRealtime(0.1f);
		if(isLevelCompletePanel)
		playerLevelFillbar.DOFillAmount(PlayerPrefs.GetFloat("PlayerRankFillBar"), 1.2f);
		yield return new WaitForSecondsRealtime(1.3f);
		gamePlayButtons[0].SetActive(true);
		gamePlayButtons[1].SetActive(true);
		gamePlayButtons[2].SetActive(true);
	}
}
