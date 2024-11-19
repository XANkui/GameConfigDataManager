using System.Collections.Generic;
using UnityEngine;

public class TestGameConfigDataManager : MonoBehaviour
{
    IConfigData skillConfigData = SkillConfigData.Instance;
    IConfigData petConfigData = PetConfigData.Instance;

    // Start is called before the first frame update
    void Start()
    {
        skillConfigData.InitConfigData();
        List<Dictionary<string, string>> skillDictionaries = skillConfigData.GetConfigDataLines();
        Debug.Log(skillDictionaries[2]["ID"]);

        petConfigData.InitConfigData();
        List<Dictionary<string, string>> petDictionaries = petConfigData.GetConfigDataLines();
        Debug.Log(petDictionaries[2]["Name"]);
    }
    
}
