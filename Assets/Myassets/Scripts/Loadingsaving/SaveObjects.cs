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

   private string path = Application.dataPath + Path.AltDirectorySeparatorChar + "saNuveDateTest.json";
   private string persistsenPath;

   public TextAsset jsonFile;

   public void Awake()
   {




      foreach (Saveable2stateObject saveable2StateObject in ChestArray)
      {
         chestDictionary.Add(saveable2StateObject.ID, saveable2StateObject);
         print(saveable2StateObject.ID);
      }
   }

   public void SaveToJSON()
   {
         File.Delete(Application.dataPath + "/savedata.json");
         var Json = JsonConvert.SerializeObject(chestDictionary, Formatting.Indented);
         File.WriteAllText(Application.dataPath + "/savedata.json", Json);
   }

   public void LoadFromJson()
   {
      if (File.Exists(Application.dataPath + "/savedata.json"))
      {
         var json = File.ReadAllText(Application.dataPath + "/savedata.json");
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
