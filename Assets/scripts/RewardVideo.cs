using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class RewardVideo : MonoBehaviour
{
	[SerializeField] GameObject winCanvas;
	[SerializeField] GameObject player;
	[SerializeField] GameObject cheaterBlock;
	[SerializeField] GameObject ifPlayerNotGrounded;
	private void OnEnable()
	{
		YandexGame.CloseVideoEvent += Rewarded;
		YandexGame.CheaterVideoEvent += ActivateCheaterBlock;
	}
	// Отписываемся от события открытия рекламы в OnDisable
	private void OnDisable()
	{
		YandexGame.CloseVideoEvent -= Rewarded;
		YandexGame.CheaterVideoEvent -= ActivateCheaterBlock;
	}
	//метод вызхова видеорекламы
	public void ShowAdReward(int levelindex) => YandexGame.RewVideoShow(levelindex);

	// Подписанный метод получения награды
	void Rewarded(int id)
	{
		if(id <= 14)
			RewardLevel(id);
	}
	private void ActivateCheaterBlock() => cheaterBlock.SetActive(true);
	
	private void RewardLevel(int id)
    {
		SavePoints.isPointSet = false;

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		winCanvas.SetActive(true);

		PauseController pc = FindObjectOfType<PauseController>();
		pc.enabled = false;
		player.SetActive(false);

		dead.deadCounter = 0;

		//Тут добавить сохранения
		SaveDataYG save = new SaveDataYG();
		save.levelPassed = id;
		save.tutorial = true;
		save.SaveData();

		YandexGame.SaveProgress();
		YandexGame.SaveProgress();
	}
}
