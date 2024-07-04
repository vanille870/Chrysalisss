using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public enum TwoStateType { chest }

public class SaveObjects : MonoBehaviour
{
   [SerializeField] ItemPickup[] ChestsGOs;
   [SerializeField] Saveable2stateObject[] ChestArray;
   public Dictionary<int, Saveable2stateObject> chestDictionary = new Dictionary<int, Saveable2stateObject>();

   private string SaveDataPath;
   [SerializeField] string FileName;
   private int saveSlotRoot;

   public TextAsset jsonFile;

   public void Awake()
   {
      SaveDataPath = SaveSlotsManager.currentPath + FileName;

      foreach (Saveable2stateObject saveable2StateObject in ChestArray)
      {
         chestDictionary.Add(saveable2StateObject.ID, saveable2StateObject);
      }
   }

   public void SaveToJSON()
   {
      //File.Delete(Application.dataPath + "/savedata.json");
      var Json = JsonConvert.SerializeObject(chestDictionary, Formatting.Indented);
      File.WriteAllText(SaveDataPath, Json);
   }

   public void LoadFromJson()
   {
      if (File.Exists(SaveDataPath))
      {
         var json = File.ReadAllText(SaveDataPath);
         chestDictionary = JsonConvert.DeserializeObject<Dictionary<int, Saveable2stateObject>>(json);
         LoadChests();
      }
   }

   public void Update2StateObject(TwoStateType ObjectType, int InputID)
   {
      chestDictionary[InputID].Open = true;
   }

   public void LoadChests()
   {
      int iterator = -1;

      foreach (ItemPickup itemPickup in ChestsGOs)
      {
         iterator++;

         if (chestDictionary[iterator].Open == true)
         {
            itemPickup.OpenOnLoad();
         }
      }
   }
}
