using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Audio;

public class Saves : MonoBehaviour
{
    [Header ("Сохраняемые преременные")]
    public int _SavePoz; //Переменная в которую записывается место сохранения
    public int _Ansver1; //Сохранение ответа в целях сохранения при перезаходе на чекпоинт

    public float _SoundSave; // Сохранение громкости музыки
    public float _EffectSave; // Сохранение громкости эффектов

    public int _RespFR;
    public int _RespPar;
    public int _RespSc;

    public bool _DisYes;
    

    [Serializable] ////////////////////////Стопудово есть ошибка с бесконечным сохранением респектов
    class SaveData
    {
        public float SoundSave;
        public float EffectSave;
        public int SavePoz;
        public int Ansver1;

        public int RespFR;
        public int RespPar;
        public int RespSc;

        public bool DisYes;
    }

    [Header ("Обьекты с которых берутся сохранения")]
    public SoundEdit _SE;

    void Awake()
    {
        LoadGameAwake();
    }

    public void Save()
    {  
        _SoundSave = _SE._SoundF;
        _EffectSave = _SE._EffF;
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath 
            + "/LifeExam.dat"); 
        SaveData data = new SaveData();


        data.SoundSave = _SoundSave;
        data.EffectSave = _EffectSave;
        data.SavePoz = _SavePoz;
        data.Ansver1 = _Ansver1;
        data.RespFR = _RespFR;
        data.RespPar = _RespPar;
        data.RespSc = _RespSc;
        data.DisYes = _DisYes;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    
    
    void LoadGameAwake()
    {
        if (File.Exists(Application.persistentDataPath 
            + "/LifeExam.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
            File.Open(Application.persistentDataPath 
            + "/LifeExam.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _SoundSave = data.SoundSave;
            _EffectSave = data.EffectSave;
            _SavePoz = data.SavePoz;
            _DisYes = data.DisYes;
            _RespFR = data.RespFR;
            _RespPar = data.RespPar;
            _RespSc = data.RespSc;

            _SE._SoundF = _SoundSave;
            _SE._EffF = _EffectSave;

            _Ansver1 = data.Ansver1;

            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }

    public void LoadGames()
    {
        if (File.Exists(Application.persistentDataPath 
            + "/LifeExam.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
            File.Open(Application.persistentDataPath 
            + "/LifeExam.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

           if ( data.SavePoz != 0)
           {
               _SavePoz = data.SavePoz;
           } 

           _DisYes = data.DisYes;
           _RespFR = data.RespFR;
           _RespPar = data.RespPar;
           _RespSc = data.RespSc;
           
            Debug.Log("Продолжение запущено успешно");
        }
        else
            Debug.LogError("Нет файла сохранения!!!");
    }

    public void ResetSaves()
    {
        if (File.Exists(Application.persistentDataPath 
            + "/LifeExam.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter(); 
            FileStream file = File.Create(Application.persistentDataPath 
                + "/LifeExam.dat"); 
            SaveData data = new SaveData();

            data.SavePoz = 0;
            _SavePoz = 0;
            data.Ansver1 = 0;
            _Ansver1 = 0;
            data.SoundSave = _SoundSave;
            data.EffectSave = _EffectSave;

            bf.Serialize(file, data);
            file.Close();
            Debug.Log("Save reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
    
}
