using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorPallet : MonoBehaviour
{
    private const int C_DEFAULT_INIT_PALLET_SIZE = 5;

    public static ColorPallet instance;
    public Color[] Pallet { get; private set; }

    [SerializeField] private bool randomizeAlpha = false;

    private void Start()
    {
        if(instance != null && instance != this)
            Destroy(this);
        
        DontDestroyOnLoad(this);
        instance = this;
        Init();
    }

    /// <summary>
    /// Initializes the color pallet with random colors.
    /// </summary>
    public void Init()
    {
        Pallet = new Color[C_DEFAULT_INIT_PALLET_SIZE];
        RandomizeColors(randomizeAlpha);
    }

    /// <summary>
    /// Randomizes all color values of all colors within <see cref="Pallet"/>
    /// </summary>
    /// <param name="includeAlpha">should the alpha value be randomized</param>
    public void RandomizeColors(bool includeAlpha = false)
    {
        for (int i = 0; i < Pallet.Length; i++)
        {
            RandomizeColor(i, includeAlpha);
        }
    }

    /// <summary>
    /// Randomizes a specific color within the <see cref="Pallet"/>
    /// </summary>
    /// <param name="colorPalletIndex">The index of the array to randomize.</param>
    /// <param name="includeAlpha">Should the alpha be randomized.</param>
    public void RandomizeColor(int colorPalletIndex, bool includeAlpha)
    {
        if (colorPalletIndex < 0 || colorPalletIndex >= Pallet.Length)
        {
            Debug.LogErrorFormat(this,
                "[ColorPallet][RandomizeColor]: ERROR: attempting to randomize a color at an invalid index {0}",
                colorPalletIndex);
            return;
        }
        
        Pallet[colorPalletIndex] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)); 
        if (includeAlpha)
        {
            Pallet[colorPalletIndex].a = Random.Range(0f, 1f);
        }
    }

    /// <summary>
    /// Returns a random color from the <see cref="Pallet"/>
    /// </summary>
    /// <returns></returns>
    public Color PickColor()
    {
        return Pallet[Random.Range(0, Pallet.Length)];
    }
}
