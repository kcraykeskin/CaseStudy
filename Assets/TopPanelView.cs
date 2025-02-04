using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopPanelView : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI MoveCountText;
   [SerializeField] private TextMeshProUGUI[] MissionTexts;
   [SerializeField] private Image[] MissionImages;

   public void Initialize()
   {
      MoveCountText.text = GameManager.Instance.levelSettings.MoveCount == 0 ?  "" : GameManager.Instance.levelSettings.MoveCount.ToString();

      for (int i= 0; i < 6; i++)
      {
         if (i < GameManager.Instance.levelSettings.numberOfColors && GameManager.Instance.levelSettings.MissionValues[i] > 0)
         {
            MissionTexts[i].text = GameManager.Instance.levelSettings.MissionValues[i].ToString();
            MissionImages[i].sprite = GameManager.Instance.GridManager.selectedColorSprites[i].sprites[0];
            MissionTexts[i].transform.parent.gameObject.SetActive(true);
         }
         else
         {
            MissionTexts[i].transform.parent.gameObject.SetActive(false);
         }
      }
   }

   public void UpdateView()
   {
      MoveCountText.text = GameManager.Instance.levelSettings.MoveCount == 0 ?  "" : GameManager.Instance.MoveCount.ToString();
      for (int i= 0; i < 6; i++)
      {
         if (i < GameManager.Instance.levelSettings.numberOfColors && GameManager.Instance.levelSettings.MissionValues[i] > 0)
         {
            MissionTexts[i].text = GameManager.Instance.MissionValues[i].ToString();
            MissionImages[i].sprite = GameManager.Instance.GridManager.selectedColorSprites[i].sprites[0];
         }
      }
   }
}
