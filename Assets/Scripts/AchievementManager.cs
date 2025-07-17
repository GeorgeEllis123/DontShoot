using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementManager
{
   // Unlock achievement thru its ID name. 
   public static void UnlockAchievement(string achID)
   {
       if (SteamManager.Initialized)
       {
           SteamUserStats.GetAchievement(achID, out bool isAchCompleted);

           if (!isAchCompleted)
           {
               SteamUserStats.SetAchievement(achID);
               SteamUserStats.StoreStats();
               Debug.Log($"Achievement unlocked: {achID}");
           }
       }
       SteamAPI.RunCallbacks();
   }

   // Call in Lvl Manager 
   public static void CheckForAchievement(int score, int lvlIndex)
   {
       if (SteamManager.Initialized)
       {
           if (score >= 1 && lvlIndex >= 1)
               UnlockAchievement("ACH_SURVIVED");
           if (score >= 3 && lvlIndex >= 2)
               UnlockAchievement("ACH_CANT_SHOOT_THE_MESSENGER");
           if (score >= 5 && lvlIndex >= 4)
               UnlockAchievement("ACH_CRUEL_CULVER");
           if (score >= 8 && lvlIndex >= 7)
               UnlockAchievement("ACH_SQUAB_SQUABBLES");
           if (score >= 10 && lvlIndex > 9)
               UnlockAchievement("ACH_ SALUTE_TO_SPIKE");
       }
       SteamAPI.RunCallbacks();
   }
}
