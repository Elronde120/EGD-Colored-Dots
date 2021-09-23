using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorBlock : MonoBehaviour
{
   [SerializeField] private Image colorImage;
   [SerializeField] private int colorPalletIndex = 0;
   private void Update()
   {
      colorImage.color = ColorPallet.instance.Pallet[colorPalletIndex];
   }

   public void RandomizeColor()
   {
      ColorPallet.instance.RandomizeColor(colorPalletIndex, false);
   }
}
