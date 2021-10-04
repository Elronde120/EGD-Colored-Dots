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
      Color color = new Color
      {
         r = ColorPallet.instance.Pallet[colorPalletIndex].r,
         g = ColorPallet.instance.Pallet[colorPalletIndex].g,
         b = ColorPallet.instance.Pallet[colorPalletIndex].b,
         a = colorImage.color.a
      };
      colorImage.color = color;
   }

   public void RandomizeColor()
   {
      ColorPallet.instance.RandomizeColor(colorPalletIndex, false);
   }
}
