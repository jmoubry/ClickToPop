global using BTD_Mod_Helper.Extensions;
using MelonLoader;
using BTD_Mod_Helper;
using HarmonyLib;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using UnityEngine;
using ClickToPop;

[assembly: MelonInfo(typeof(ClickToPop.ClickToPop), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ClickToPop;

public class ClickToPop : BloonsTD6Mod
{
    private const float ClickPopRadiusPixels = 140f;
    private static bool _loggedNoCamera;

    public override void OnApplicationStart()
    {
        ModHelper.Msg<ClickToPop>("ClickToPop loaded!");
    }

    private static Camera? GetUsableCamera()
    {
        return Camera.main ?? Object.FindObjectOfType<Camera>();
    }

    internal static void TryPopClickedBloon(InGame inGame)
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        if (inGame == null || !inGame.IsInGame())
        {
            return;
        }

        var camera = GetUsableCamera();
        if (camera == null)
        {
            if (!_loggedNoCamera)
            {
                _loggedNoCamera = true;
                ModHelper.Warning<ClickToPop>("No camera found for click pop.");
            }

            return;
        }

        var mousePos = Input.mousePosition;

        Il2CppAssets.Scripts.Simulation.Bloons.Bloon? closestBloon = null;
        var bestDistance = float.MaxValue;
        var candidates = 0;

        foreach (var bloon in inGame.GetBloons())
        {
            if (bloon == null)
            {
                continue;
            }

            var bloonToSim = bloon.GetBloonToSim();
            Vector3 worldPos;
            if (bloonToSim != null)
            {
                worldPos = bloonToSim.position;
            }
            else
            {
                var node = bloon.GetUnityDisplayNode();
                if (node == null)
                {
                    continue;
                }

                worldPos = node.transform.position;
            }

            candidates++;
            var screenPos = camera.WorldToScreenPoint(worldPos);
            if (screenPos.z < 0f)
            {
                continue;
            }

            var dist = Vector2.Distance(new Vector2(mousePos.x, mousePos.y), new Vector2(screenPos.x, screenPos.y));
            if (dist < bestDistance)
            {
                bestDistance = dist;
                closestBloon = bloon;
            }
        }

        if (closestBloon == null || bestDistance > ClickPopRadiusPixels)
        {
            ModHelper.Msg<ClickToPop>($"Click miss. Candidates: {candidates}, closest: {bestDistance:0.0}px");
            return;
        }

        ModHelper.Msg<ClickToPop>($"Popping bloon at {bestDistance:0.0}px (candidates: {candidates})");
        closestBloon.Leaked();
    }
}

[HarmonyPatch(typeof(InGame), nameof(InGame.Update))]
internal static class InGame_Update_Patch
{
    [HarmonyPostfix]
    internal static void Postfix(InGame __instance)
    {
        ClickToPop.TryPopClickedBloon(__instance);
    }
}
