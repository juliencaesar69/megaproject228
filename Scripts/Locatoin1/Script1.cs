using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class Script1 : MonoBehaviour
{

    [Header ("Звуки")]
    public AudioSource Eff_OBJ; // Обьект из которог будут выходить звуковые эффекты
    public AudioClip[] Effect_ref; // Массив с эффектами
    public AudioSource Sound_OBJ; // Обьект из которог будут выходить Музыка 
    public AudioClip[] Sound_ref; // массив фоновой музыки

    [Header ("Главный Герой")]
    public Sprite[] GG_ref; //Массив с эмоциями ГГ
    public GameObject GG_poz0; //Сам ГГ
    private Image GG_poz; //Подпеременная смены спрайта ГГ

    [Header ("Другие персоонажи")]
    public Sprite[] Other_ref; //Массив с персоонажами
    public GameObject Other_poz0; //Сам персоонаж
    private Image Other_poz; //Подпеременная смены спрайта

    public GameObject TwoOther_poz0; //3 тий человек
    public Sprite[] TwoOther_ref;
    private Image TwoOther_poz;


    [Header ("Локация")]
    public Sprite[] Locations; //Массив с локациями
    public Image Loc_poz; // Сама локация

    [Header ("Интерфейс")]  
    public Animator Fade; // Плавный перелив при входе и выходе
    public GameObject LButton; //Левая кнопка выбора ответа
    public GameObject RButton; // Правая кнопка выбора ответа
    public GameObject UI_Panel; //Панель на которой есть все обьекты кроме локации
    public GameObject Clock; //Будильник
    public GameObject CloudText; //Облачко с текстом
    public GameObject Phone; //Телефон
    public GameObject[] FinalPanel; // Массив Финальной панели
    public GameObject GrandButton; //Кнопка для пропуска диалогов

    
    [Header ("Чат")]
    public Text TextGG; // Реплики ГГ
    public Text TextOther; // Реплики других
    
    [Header ("Сюжетные переменные")]  
    private int Answer = 0; // Переменная для временного хранения ответа
    public int RespectParent = 0; // счётчик влияния родственников
    public int RespectScool = 0; //Счётчик влияния школы
    public int RespectFriends = 0; //Счётчик влияния друзей
    public bool DiscoYes = false; // Переключаетель ветки дискатеки / родительского ДР
    

    // Системные переменные //
    private bool ClickBut = false; // Отлсеживает нажания на экран
    private bool NeedBut = false; //Отслеживает когда нужно нажать кнопку
    private bool CorStart = false; //Системная переменная (Нужна при старте для запуска корутины с игрой)
    private bool SaveStart = true; //Системная переменная позволяющая чинит автосейв на старте (Просто зацита от глюка сохранения)
    public bool YesFinal = false; // Системная переменная позволяющая чинить кнопку выхода в главное меню

    public Saves _Save; //  Файл сохранения
    private int LoadPoz; //Переменная отвекчающаяя за точку загрузки

   public void OnClick() //Клик на экран
   {
       if (NeedBut == true) 
       {
           ClickBut = true;
           NeedBut = false;
       }
   }

    void Start()
    {
        LoadPoz  = _Save._SavePoz;
        Answer = _Save._Ansver1;

        RespectFriends = _Save._RespFR;
        RespectParent = _Save._RespPar;
        RespectScool = _Save._RespSc;
        DiscoYes = _Save._DisYes;

        Other_poz = Other_poz0.GetComponent<Image>();
        GG_poz = GG_poz0.GetComponent<Image>();
        TwoOther_poz = TwoOther_poz0.GetComponent<Image>();
    }

    public void LeftButton ()
    {
        Answer = 1;
    }

    public void RightButton ()
    {
        Answer = -1;
    }

    public void FinalExitBut()
    {
        SceneManager.LoadScene (0);
    }
    

    
    void Update()
    {
        if (CorStart == false) // Запуск корутины с игрой
        {
            StartCoroutine(SaveYes());

            Sound_OBJ.Play();
            if (LoadPoz == 0)
            {
                StartCoroutine(Starting0());
            }
            else if (LoadPoz == 1)
            {
               StartCoroutine(Starting1());
            }
            else if (LoadPoz == 2)
            {
                StartCoroutine(Starting2());
            }
            else if (LoadPoz == 3)
            {
                StartCoroutine(Starting3());
            }
            else if (LoadPoz == 4)
            {
                StartCoroutine(Starting4());
            }
            else if (LoadPoz == 5)
            {
                StartCoroutine(Starting5());
            }
            else if (LoadPoz == 6)
            {
                StartCoroutine(Starting6());
            }
            else if (LoadPoz == 7)
            {
                StartCoroutine(Starting7());
            }
            CorStart = true;  //Включение переменной для того что бы больше не запускалось это условие  
        }

        
        
            
        
         
    }

    public void Checpoint()
    {
        if (SaveStart == false)
        {
            _Save._SavePoz = LoadPoz;
            _Save._Ansver1 = Answer;
            _Save._RespFR = RespectFriends;
            _Save._RespPar = RespectParent;
            _Save._RespSc = RespectScool;
            _Save._DisYes = DiscoYes;
            _Save.Save();
        }
    }

    IEnumerator SaveYes()
    {
        yield return new WaitForSeconds (3f);
        SaveStart = false;
    }


    IEnumerator Starting0() 
    {
        Clock.SetActive(true);
        Loc_poz.sprite = Locations[0];
        GG_poz.sprite = GG_ref[0]; // смена эмоции ГГ
        yield return new WaitForSeconds (1f);
        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (2.2f);
        Fade.SetBool("Fade_IN", false);
        TextGG.text = "-Опять это утро!!!";
        NeedBut = true; 
        while (ClickBut == false) // Пока переменная равна false то цикл запускает задержку
        {
            yield return new WaitForSeconds (0.02f);
            
        }
        ClickBut = false;
        TextGG.text = "-Ладно... \n Надо вставать";
        yield return new WaitForSeconds (1f);
        Clock.SetActive (false);
        GG_poz.sprite = GG_ref[1];
        Loc_poz.sprite = Locations[5];
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);
            
        }
        ClickBut = false;
        TextGG.text = "";
        CloudText.SetActive (true);
        TextOther.text = "Мама: Сынок, ты проснулся?";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);
            
        }
        ClickBut = false;
        CloudText.SetActive (false);
        TextGG.text = "Да мам!!!";
        TextOther.text = "";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);
            
        }
        ClickBut = false;
        CloudText.SetActive (true);
        TextGG.text = "";
        TextOther.text = "Мама: Отлично!!! \n Я все утро готовила твои любимые блинчики. \n Иди кушать. ";
        yield return new WaitForSeconds (0.5f);
        LButton.SetActive (true);
        LButton.transform.GetChild(0).GetComponent<Text>().text = "Хорошо мам";
        RButton.SetActive (true);
        RButton.transform.GetChild(0).GetComponent<Text>().text = "Извини я опаздываю. Cкушай блины сама";
        while (Answer == 0)
        {
            yield return new WaitForSeconds (0.02f);
        }
        LButton.SetActive (false);
        RButton.SetActive (false);
        CloudText.SetActive (false);


        if (Answer == 1) //Хороший ответ
        {
            RespectParent += 1;

            TextGG.text = "Хорошо мам";
            GG_poz.sprite = GG_ref[2];
            TextOther.text = "";
            yield return new WaitForSeconds (0.5f);
            Fade.SetBool("Fade_IN", true);
            yield return new WaitForSeconds (1f);
            Eff_OBJ.clip = Effect_ref[0];
            Eff_OBJ.Play();
            yield return new WaitForSeconds (1f);
            Loc_poz.sprite = Locations[1];
            Other_poz0.SetActive (true);
            Other_poz.sprite = Other_ref[0];

            Fade.SetBool("Fade_IN", false);
            TextGG.text = "Ладно я побежал. \n Целую...";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);
                
            }
            ClickBut = false;
            TextGG.text = "";
            TextOther.text = "Мама: Сынок, надеюсь ты не забыл, что у папы завтра день рождения? \n Купи ему подарок он будет рад!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);
                
            }
            ClickBut = false;
            TextGG.text = "Хорошо мам!!!";
            TextOther.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);
                
            }
            ClickBut = false;
            TextGG.text = "";
            TextOther.text = "Мама: Давай!!!  Успехов!!!";
            yield return new WaitForSeconds (1f);

        }
        else if (Answer == -1) //Плохой ответ
        {
            RespectParent -=1;

            GG_poz.sprite = GG_ref[0];
            TextGG.text = "Извини я опаздываю. Cкушай блины сама";
            TextOther.text = "";
            yield return new WaitForSeconds (1f);   
        }
        StartCoroutine (Starting1());
    }



    IEnumerator Starting1() //Чекпоинт//
    {
        int MicroAnsver;
        LoadPoz = 1; 
        MicroAnsver = Answer;
        Checpoint();
        Answer = 0;
        

        if(MicroAnsver == 1)
            Loc_poz.sprite = Locations[1];
        else 
            Loc_poz.sprite = Locations[5];


        Fade.SetBool("Fade_IN", true);
        Eff_OBJ.clip = Effect_ref[1];
        Eff_OBJ.Play();
        yield return new WaitForSeconds (2.2f);
        Loc_poz.sprite = Locations[2];
        Other_poz0.SetActive (false);
        UI_Panel.SetActive (false);
        Fade.SetBool("Fade_IN", false);
        Eff_OBJ.clip = Effect_ref[2];
        Eff_OBJ.Play();
        yield return new WaitForSeconds (2f);
        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (2.2f);
        Loc_poz.sprite = Locations[3];
        Fade.SetBool("Fade_IN", false);
        yield return new WaitForSeconds (2f);
        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (1.1f);
        Eff_OBJ.Stop();
        yield return new WaitForSeconds (1.1f);
        Eff_OBJ.clip = Effect_ref[1];
        Eff_OBJ.Play();
        Loc_poz.sprite = Locations[6];
        UI_Panel.SetActive (true);
        TextGG.text = "";
        TextOther.text = "";
        GG_poz0.SetActive (false);
        
        
        
        if(MicroAnsver == 1)    //поговорил с мамой ветка 
        {
            Other_poz0.SetActive (true);
            Other_poz.sprite = Other_ref[1];
            GG_poz0.SetActive (true);
            GG_poz.sprite = GG_ref[3];
            Fade.SetBool("Fade_IN", false);
            yield return new WaitForSeconds (1.1f);
            TextOther.text = "Мария Ивановна: Кукушкин! Почему опаздываешь?";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);
            
            }
            ClickBut = false;
            GG_poz.sprite = GG_ref[4];
            TextOther.text = "";
            TextGG.text = "Извините пожаулйста! Я не хотел! \n Честно!!!";

            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);
            
            }
            ClickBut = false;
            TextGG.text = "";
            TextOther.text = "Мария Ивановна: Ладно, садись.";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);
            
            }
            ClickBut = false; 
        }
        else if (MicroAnsver == -1) //Ушел не позавтракав
        {
            Other_poz0.SetActive (true);
            Other_poz.sprite = Other_ref[1];
            GG_poz.sprite = GG_ref[3];
            Fade.SetBool("Fade_IN", false);
            Eff_OBJ.clip = Effect_ref[3];
            Eff_OBJ.Play();  
        }


        //Продолжение линейного сюжета//
        GG_poz0.SetActive (false);
        Answer = 0;
        Loc_poz.sprite = Locations[4]; 
        TextOther.text = "Мария Ивановна: Внимание всем!!!"; 
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        TextOther.text = "Мария Ивановна: Сейчас мы пишем тест!";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        TextOther.text = "Мария Ивановна: Тест не сложный, поэтому, приступаем!";
        yield return new WaitForSeconds (2f);
        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (2f);
        Other_poz0.SetActive  (false);
        TextOther.text = "";
        GG_poz0.SetActive (true);
        GG_poz.sprite = GG_ref[0];
        Fade.SetBool("Fade_IN", false);
        yield return new WaitForSeconds (2.2f);
        TextGG.text = "Блин!!! Как же так? Я ничего не помню.";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        TextGG.text = "";
        Other_poz0.SetActive (true);
        Eff_OBJ.clip = Effect_ref[3];
        Eff_OBJ.Play();
        TextOther.text = "Мария Ивановна: Всё, сдаём теради!!!";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        TextOther.text = "";
        TextGG.text = "Зараза!!!";
        yield return new WaitForSeconds (2f);
        Fade.SetBool("Fade_IN", true);
        Eff_OBJ.clip = Effect_ref[4];
        Eff_OBJ.Play();
        Other_poz0.SetActive(false);
        yield return new WaitForSeconds (2.2f);
        Loc_poz.sprite = Locations[7];
        TextGG.text = "";
        Fade.SetBool("Fade_IN", false); 

        if (MicroAnsver == -1) ////Мама звонит напомнить////
        {
            Phone.SetActive (true);
            Phone.GetComponent<AudioSource>().Play();
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            Phone.GetComponent<AudioSource>().Stop();
            GG_poz.sprite = GG_ref[3];
            TextGG.text = "Привет мам!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextGG.text = "";
            TextOther.text = "Мама: Привет сынок \n Ты так быстро убежал что я не успела тебе напомнить";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextGG.text = "";
            TextOther.text = "Мама: Надеюсь ты не забыл что у папы завтра день рождения? \n Купи ему подарок, от будет рад!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextGG.text = "Хорошо мам. Постараюсь не забыть!!!";
            TextOther.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextGG.text = "";
            TextOther.text = "Давай сынок. Пока!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "";
            Phone.SetActive (false);
        }

        //Продолжение линейного сюжета//
        Other_poz.sprite = Other_ref[2];
        Other_poz0.SetActive(true);
        TextOther.text = "Ваcян: Здорова!!! \n Блин вот это жесть а не контроша ";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        TextOther.text = "";
        TextGG.text = "Привет!!! Ну да, не из лёгких была работка.";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        TextGG.text = "";
        TextOther.text = "Васян: Чё хоть половину то решил?";
        //Вилка с другом на счёт контроши//
        LButton.SetActive (true);
        LButton.transform.GetChild(0).GetComponent<Text>().text = "Соврать";
        RButton.SetActive (true);
        RButton.transform.GetChild(0).GetComponent<Text>().text = "Сказать правду";
        NeedBut = true;
        while (Answer == 0)
        {
            yield return new WaitForSeconds (0.02f);
        }
        LButton.SetActive (false);
        RButton.SetActive (false);

        if (Answer == 1)// Соврал
        {
            RespectFriends -= 1;
            GG_poz.sprite = GG_ref[2];
            TextGG.text = "Да легко! \n Я за пару минут управился";
            TextOther.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            
            TextGG.text = "";
            TextOther.text = "Васян: Эх... Ладно...";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;

            TextGG.text = "";
            TextOther.text = "";
            Other_poz0.SetActive(false);
            Fade.SetBool("Fade_IN", true);
            yield return new WaitForSeconds (2.2f);
            Eff_OBJ.clip = Effect_ref[2];
            Eff_OBJ.Play();
            


            Loc_poz.sprite = Locations[2];
            GG_poz.sprite = GG_ref[3];
            Fade.SetBool("Fade_IN", false);


            
            Phone.SetActive (true);
            Phone.GetComponent<AudioSource>().Play();
            NeedBut = true;
            yield return new WaitForSeconds (1f);
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            Phone.GetComponent<AudioSource>().Stop();
            GG_poz.sprite = GG_ref[3];
            TextGG.text = "Алло";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            Answer = 0;

            TextOther.text = "Васян: Алло. Слушай забыл сказать. \n Мы тут всей группой на дискотеку пойдем. \n Не хочешь с нами?";
            TextGG.text = "";
            LButton.SetActive (true);
            LButton.transform.GetChild(0).GetComponent<Text>().text = "Давай!!!";
            RButton.SetActive (true);
            RButton.transform.GetChild(0).GetComponent<Text>().text = "Учёба важнее!!!";
            NeedBut = true;
            while (Answer == 0)
            {
                yield return new WaitForSeconds (0.02f);
            }
            LButton.SetActive (false);
            RButton.SetActive (false);

            if(Answer == 1) //Пошли на дискач
            {
                RespectFriends += 1;
                DiscoYes = true;
                TextOther.text = "Васян: Тогда замётано!!! \n Завтра к 18:00 пойдём!!! ";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                GG_poz.sprite = GG_ref[2];
                TextGG.text = "Хорошо!!!";
                TextOther.text = "";
            }
            else if (Answer == -1 ) // Учёба важнее
            {
                RespectScool += 1;

                TextOther.text = "Васян: Да ты чё!!! \n Там будет Машка!";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextGG.text = "Ну ладно!!! Погнали!!!";
                TextOther.text = "";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextGG.text = "";
                TextOther.text = "Васян: Тогда замётано!!! \n Завтра к 18:00 пойдём!!! ";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                GG_poz.sprite = GG_ref[2];
                TextGG.text = "Хорошо!!!";
                TextOther.text = "";
            }
            Phone.SetActive (false);

            Fade.SetBool("Fade_IN", true);
            yield return new WaitForSeconds (2.2f);
            TextGG.text = "";
            TextOther.text = "";
            Other_poz0.SetActive (false);
            StartCoroutine (Starting2());
            

        }
        else if (Answer == - 1) //Сказал правду
        {
            RespectFriends += 1;
            GG_poz.sprite = GG_ref[0];
            TextGG.text = "Да блин капец я и 10 вопросов не осилил.";
            TextOther.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            
            TextGG.text = "";
            TextOther.text = "Васян: Эх.. Ну ладно. \n Зато вместе на пересдачу пойдем";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            

            //Кусок с вопросом на счёт дискатеки//
            Answer = 0;
            TextOther.text = "Васян: Кстати!!! Мы тут всей группой на дискотеку пойдем. \n Не хочешь с нами?";
            LButton.SetActive (true);
            LButton.transform.GetChild(0).GetComponent<Text>().text = "Давай!!!";
            RButton.SetActive (true);
            RButton.transform.GetChild(0).GetComponent<Text>().text = "Учёба важнее!!!";
            NeedBut = true;
            while (Answer == 0)
            {
                yield return new WaitForSeconds (0.02f);
            }
            LButton.SetActive (false);
            RButton.SetActive (false);

            if(Answer == 1) //Пошли на дискач
            {
                RespectFriends += 1;
                DiscoYes = true;
                TextOther.text = "Васян: Тогда замётано!!! \n Завтра к 18:00 пойдём!!! ";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                GG_poz.sprite = GG_ref[2];
                TextGG.text = "Хорошо!!!";
                TextOther.text = "";
            }
            else if (Answer == -1 ) // Учёба важнее
            {
                RespectScool += 1;

                TextOther.text = "Васян: Да ты чё!!! \n Там будет Машка!";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextGG.text = "Ну ладно!!! Погнали!!!";
                TextOther.text = "";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextGG.text = "";
                TextOther.text = "Васян: Тогда замётано!!! \n Завтра к 18:00 пойдём!!! ";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                GG_poz.sprite = GG_ref[2];
                TextGG.text = "Хорошо!!!";
                TextOther.text = "";
            }

            Fade.SetBool("Fade_IN", true);
            yield return new WaitForSeconds (2.2f);
            TextGG.text = "";
            TextOther.text = "";
            Other_poz0.SetActive (false);

            StartCoroutine (Starting2());
        }
    }


    IEnumerator Starting2() /////Чекпоинт/////// 
    {
        int MicroAnsver1;
        LoadPoz = 2; 
        MicroAnsver1 = Answer;
        Checpoint();
        Answer = 0;
        

        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (2.2f);
        Loc_poz.sprite = Locations[8];
        Eff_OBJ.clip = Effect_ref[1];
        Eff_OBJ.Play();
        GG_poz.sprite = GG_ref[3];
        Fade.SetBool("Fade_IN", false);
        yield return new WaitForSeconds (0.5f);
        Other_poz.sprite = Other_ref[0];
        Other_poz0.SetActive(true);
        yield return new WaitForSeconds (1f);
        TextOther.text = "Мама: Сынок ты купил подарок папе?";
        LButton.SetActive (true);
        LButton.transform.GetChild(0).GetComponent<Text>().text = "Забыл...";
        RButton.SetActive (true);
        RButton.transform.GetChild(0).GetComponent<Text>().text = "Соврать.";
        NeedBut = true;
        while (Answer == 0)
        {
            yield return new WaitForSeconds (0.02f);
        }
        LButton.SetActive (false);
        RButton.SetActive (false);
        

        if (Answer == -1)
        {
            TextOther.text = "";
            TextGG.text = "Да, конечно!!! \n Извини мам мне надо готовится.";
            GG_poz.sprite = GG_ref[0];
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "Мама: Ну вот и славно. \n Давай иди грызи гранит науки.";
            TextGG.text = "";
            GG_poz.sprite = GG_ref[0];
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            RespectScool +=1;
            Fade.SetBool("Fade_IN", true);
            Eff_OBJ.clip = Effect_ref[1];
            Eff_OBJ.Play();
            yield return new WaitForSeconds (2.2f);
            Loc_poz.sprite = Locations[5];
            Other_poz0.SetActive (false);
            GG_poz.sprite = GG_ref[1];
            TextOther.text = "";
            Fade.SetBool("Fade_IN", false);
            TextGG.text = "Уже поздно. Я не успею купить подарок...";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextGG.text = "Лучше примусь за подготовку...";
            yield return new WaitForSeconds (1);
        }
        else if (Answer == 1) //Блин забыл подарок
        {
            TextOther.text = "";
            TextGG.text = "Блин. Как же так??? \n Совсем из головы вылетело!!!";
            GG_poz.sprite = GG_ref[0];
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "Мама: Ладно. У тебя ёщё есть время. \n День рождения то завтра.";
            TextGG.text = "";
            yield return new WaitForSeconds (1); 
            Answer = 0; 
            LButton.SetActive (true);
            LButton.transform.GetChild(0).GetComponent<Text>().text = "Пойти за подарком";
            RButton.SetActive (true);
            RButton.transform.GetChild(0).GetComponent<Text>().text = "Делать уроки";
            NeedBut = true;
            while (Answer == 0)
            {
                yield return new WaitForSeconds (0.02f);
            }
            LButton.SetActive (false);
            RButton.SetActive (false);
            if(Answer == 1) // Пошёл за подарком
            {
                RespectParent +=1;
                Fade.SetBool("Fade_IN", true);
                Eff_OBJ.clip = Effect_ref[1];
                Eff_OBJ.Play();
                yield return new WaitForSeconds (2.2f);
                Loc_poz.sprite = Locations[9];
                Other_poz0.SetActive (false);
                GG_poz.sprite = GG_ref[1];
                Fade.SetBool("Fade_IN", false);
                yield return new WaitForSeconds (1);
                TextOther.text = "";
                TextGG.text = "Что бы купить???";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextGG.text = "";
                Other_poz.sprite = Other_ref[4];
                Other_poz0.SetActive(true);
                TextOther.text = "Продавец: Вам что-нибудь подсказать?";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextGG.text = "Да! Можете, пожалуйста, посоветовать подарок Папе?";
                TextOther.text = "";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextGG.text = "";
                TextOther.text = "Продавец: Конечно!!! \n Как вам вариант бритва?";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                GG_poz.sprite = GG_ref[2];
                TextGG.text = "Хм... \nА что, неплохая идея?";
                TextOther.text = "";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                GG_poz.sprite = GG_ref[2];
                TextGG.text = "Пожалуй куплю.";
                TextOther.text = "";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                GG_poz.sprite = GG_ref[2];
                TextGG.text = "До свидания!!!";
                TextOther.text = "";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                GG_poz.sprite = GG_ref[2];
                TextGG.text = "";
                TextOther.text = "Продавец: До свидания!!!";
                yield return new WaitForSeconds (1f);
                Fade.SetBool("Fade_IN", true);
                Eff_OBJ.clip = Effect_ref[1];
                Eff_OBJ.Play();
                TextOther.text = "";
                yield return new WaitForSeconds (2.2f);
                Loc_poz.sprite = Locations[5];
                Other_poz0.SetActive (false);
                GG_poz.sprite = GG_ref[0];
                Fade.SetBool("Fade_IN", false);
                TextGG.text = "Как же хочется спать.";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                Answer = 1;
            }
            else
            {
                RespectScool +=1;
                Fade.SetBool("Fade_IN", true);
                Eff_OBJ.clip = Effect_ref[1];
                Eff_OBJ.Play();
                yield return new WaitForSeconds (2.2f);
                Loc_poz.sprite = Locations[5];
                Other_poz0.SetActive (false);
                GG_poz.sprite = GG_ref[1];
                TextOther.text = "";
                Fade.SetBool("Fade_IN", false);
                TextGG.text = "Уже поздно. Я не успею купить подарок...";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextGG.text = "Лучше примусь за подготовку...";
                yield return new WaitForSeconds (1);
                Answer = -1;
            }
        }
        TextGG.text = "";
        TextOther.text = "";
        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (2f);  
        StartCoroutine (Starting3());

        
    }


    IEnumerator Starting3 ()
    {
        int MicroAnsver;
        LoadPoz = 3; 
        MicroAnsver = Answer;
        Checpoint();
        Answer = 0;

        GG_poz.sprite = GG_ref[1];
        Loc_poz.sprite = Locations[5];

        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (1f);
        Clock.SetActive(true);
        yield return new WaitForSeconds (1);
        Fade.SetBool("Fade_IN", false);
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;

        Clock.SetActive(false);
        yield return new WaitForSeconds (0.5f);
        Eff_OBJ.clip = Effect_ref[1];
        Eff_OBJ.Play();
        Other_poz.sprite = Other_ref[5];
        Other_poz0.SetActive(true);
        TextOther.text = "Папа: С добрым утром сынок!!!";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;

        if(MicroAnsver == 1)
        {
            GG_poz.sprite = GG_ref[2];
            TextOther.text = "";
            TextGG.text = "Привет Пап \n С днём рождения тебя!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "";
            TextGG.text = "Держи подарок!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "Папа: Спасибо, сын!!!";
            RespectParent += 1;
            TextGG.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
        }
        else
        {
            GG_poz.sprite = GG_ref[2];
            TextOther.text = "";
            TextGG.text = "Привет Пап \n С днём рождения тебя!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "";
            TextGG.text = "Извини меня пожалуйста... \n Я не успел купить тебе подарок";
            GG_poz.sprite = GG_ref[4];
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "Папа: Ладно. И за поздравление уже спасибо!!!";
            RespectParent -= 1;
            TextGG.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
        }

        TextOther.text = "Сегодня вечером мы будем отмечать мой день рождения \n Ты придёшь?";
        Answer = 0;

        LButton.SetActive (true);
        LButton.transform.GetChild(0).GetComponent<Text>().text = "Нет";
        RButton.SetActive (true);
        RButton.transform.GetChild(0).GetComponent<Text>().text = "Да";
        NeedBut = true;
        while (Answer == 0)
        {
            yield return new WaitForSeconds (0.02f);
        }
        LButton.SetActive (false);
        RButton.SetActive (false);

        if (Answer == 1)
        {
            RespectFriends += 1;
            TextOther.text = "";
            GG_poz.sprite = GG_ref[4];
                
            TextGG.text = "Извини пап я не смогу... \n Мне надо готовится к экзамену...";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "Папа: Ладно. Помни сын, учеба должна быть на первом месте!!!";
            TextGG.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            Eff_OBJ.clip = Effect_ref[1];
            Eff_OBJ.Play();
            yield return new WaitForSeconds (1);
            Other_poz0.SetActive(false);
        }
        else
        {
            ClickBut = false;
            RespectParent +=1;
            TextOther.text = "";
            TextGG.text = "Да конечно!!! Я не могу пропустить наш семейный ужин.";
            GG_poz.sprite = GG_ref[2];
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "Вот и хорошо!!! Жду тебя вечером.";
            TextGG.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            Eff_OBJ.clip = Effect_ref[1];
            Eff_OBJ.Play();
            yield return new WaitForSeconds (1);
            Other_poz0.SetActive(false);
        } 
        
        TextGG.text = "";
        TextOther.text = "";

        yield return new WaitForSeconds (1);
        Fade.SetBool("Fade_IN", true);
        Eff_OBJ.clip = Effect_ref[1];
        Eff_OBJ.Play();
        yield return new WaitForSeconds (2.2f);
        TextOther.text = "";
        TextGG.text = "";
        GG_poz.sprite = GG_ref[3];
        Loc_poz.sprite = Locations[2];
        Other_poz0.SetActive (false);
        UI_Panel.SetActive (false);
        Fade.SetBool("Fade_IN", false);
        Eff_OBJ.clip = Effect_ref[2];
        Eff_OBJ.Play();
        yield return new WaitForSeconds (2f);
        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (2.2f);
        Loc_poz.sprite = Locations[3];
        Fade.SetBool("Fade_IN", false);

        yield return new WaitForSeconds (1f);
        if(Answer == -1)
        {
            UI_Panel.SetActive (true);
            Other_poz.sprite = Other_ref[2];
            Other_poz0.SetActive (true);
            TextOther.text = "Васян: Ну что? Всё в силе?";
            Answer = 0;
            LButton.SetActive (true);
            LButton.transform.GetChild(0).GetComponent<Text>().text = "Соврать";
            RButton.SetActive (true);
            RButton.transform.GetChild(0).GetComponent<Text>().text = "Сказать правду";
            NeedBut = true;
            while (Answer == 0)
            {
                yield return new WaitForSeconds (0.02f);
            }
            LButton.SetActive (false);
            RButton.SetActive (false);

            if (Answer == 1) //Соврать
            {
                RespectFriends -=1;
                TextGG.text = "Извини я должен подготовится к экзамену.";
                TextOther.text = "";
                GG_poz.sprite = GG_ref[0];
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextGG.text = "";
                TextOther.text = "Васян: Да ну тебя.";
                Other_poz.sprite = Other_ref[3];
                GG_poz.sprite = GG_ref[2];
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                Other_poz0.SetActive (false);
                TextOther.text = "";
            }
            else // Сказать правду
            {
                RespectFriends +=1;
                TextGG.text = "Извини я обещал быть на папином дне рождении";
                TextOther.text = "";
                GG_poz.sprite = GG_ref[0];
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextGG.text = "";
                TextOther.text = "Васян: Ладно. Удачи!!!";
                GG_poz.sprite = GG_ref[2];
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                Other_poz0.SetActive (false);
                TextOther.text = "";
            }
            Answer = -1;
        }
        else if (Answer == 1)
        {
            UI_Panel.SetActive (true);
            Other_poz.sprite = Other_ref[2];
            Other_poz0.SetActive (true);
            TextOther.text = "Васян: Ну что? Всё в силе?";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextGG.text = "Да!!! Сегодня оторвемся!!!";
            GG_poz.sprite = GG_ref[2];
            TextOther.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextGG.text = "";
            TextOther.text = "Васян: Круто!!! Тогда поcле уроков на этом месте!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            Other_poz0.SetActive (false);
            TextOther.text = "";
        }

        StartCoroutine (Starting4());
    }

    IEnumerator Starting4()
    {
        int MicroAnsver;
        LoadPoz = 4; 
        MicroAnsver = Answer;
        Checpoint();
        Answer = 0;

        GG_poz.sprite = GG_ref[1];
        
        Loc_poz.sprite = Locations[3];
        yield return new WaitForSeconds (2f);
        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (1.1f);
        Eff_OBJ.Stop();
        GG_poz0.SetActive (false);
        yield return new WaitForSeconds (1.1f);
        Eff_OBJ.clip = Effect_ref[1];
        Eff_OBJ.Play();
        Loc_poz.sprite = Locations[4];
        UI_Panel.SetActive (true);
        TextGG.text = "";
        TextOther.text = "";
        Fade.SetBool("Fade_IN", false);
        yield return new WaitForSeconds (1.1f);

        Other_poz.sprite = Other_ref[1];
        Other_poz0.SetActive(true);
        TextOther.text = "Мария Ивановна: Внимание всем!!!"; 
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        TextOther.text = "Мария Ивановна: Сегодня мы продолжаем изучать косинусы...";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        TextOther.text = "";
        GG_poz.sprite = GG_ref[0];
        GG_poz0.SetActive (true);
        Other_poz0.SetActive (false);
        TextGG.text = "Блин как же хочется спать...";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        TextGG.text = "";
        Answer = 0;
        LButton.SetActive (true);
        LButton.transform.GetChild(0).GetComponent<Text>().text = "Поспать";
        RButton.SetActive (true);
        RButton.transform.GetChild(0).GetComponent<Text>().text = "Пытаться учится";
        NeedBut = true;
        while (Answer == 0)
        {
            yield return new WaitForSeconds (0.02f);
        }
        LButton.SetActive (false);
        RButton.SetActive (false);
        yield return new WaitForSeconds (2f);
        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (2f);
        Other_poz0.SetActive  (false);
        TextOther.text = "";
        GG_poz0.SetActive (true);
        GG_poz.sprite = GG_ref[0];
        Fade.SetBool("Fade_IN", false);
        yield return new WaitForSeconds (2.2f);
        Eff_OBJ.clip = Effect_ref[3];
        Eff_OBJ.Play();

        if(Answer == 1)
        {
            RespectScool -= 1;
            TextGG.text = "Блин. Я проспал всю теорию!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
        }
        else
        {
            RespectScool += 1;
            TextGG.text = "Ура!!! Наконец то звонок!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
        }

        Answer = MicroAnsver;
        
        Fade.SetBool("Fade_IN", true);
        TextGG.text = "";
        StartCoroutine (Starting5());
    }

    IEnumerator Starting5()
    {
        int MicroAnsver;
        LoadPoz = 5; 
        MicroAnsver = Answer;
        Checpoint();
        Answer = 0;
        GG_poz.sprite = GG_ref[0]; 
        Loc_poz.sprite = Locations[4];
        Fade.SetBool("Fade_IN", true);
        
        if ( MicroAnsver == 1) // На Дискач//
        {
            yield return new WaitForSeconds (2.2f);
            Loc_poz.sprite = Locations[3];
            Eff_OBJ.clip = Effect_ref[2];
            Eff_OBJ.Play();
            GG_poz.sprite = GG_ref[1];
            Other_poz.sprite = Other_ref[2];
            Other_poz0.SetActive(true);
            yield return new WaitForSeconds (2.2f);
            TextGG.text = "";
            Fade.SetBool("Fade_IN", false);
            yield return new WaitForSeconds (2f);
            TextOther.text = "Васян: Ну что погнали!!!";
            yield return new WaitForSeconds (1f);
            Fade.SetBool("Fade_IN", true);
            yield return new WaitForSeconds (2.2f);
            TextGG.text = "";
            TextOther.text = "";
            Loc_poz.sprite = Locations[11];
            Sound_OBJ.clip = Sound_ref[1];
            GG_poz.sprite = GG_ref[2];
            Sound_OBJ.Play();
            Eff_OBJ.Stop();
            Fade.SetBool("Fade_IN", false);
            TextOther.text = "Васян: Ну чё не зря же я тебя сюда позвал?";
            TextGG.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "";
            TextGG.text = "Блин. Да тут круто!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "Васян: Ооо смотри кто идёт. Ну ладно оставлю вас наедине.";
            TextGG.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            Other_poz0.SetActive(false);
            TextOther.text = "";
            yield return new WaitForSeconds (0.5f);  
            Other_poz.sprite = Other_ref[6];
            Other_poz0.SetActive(true);
            TextOther.text = "Маша: Миша привет!!!";
            TextGG.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "";
            TextGG.text = "Привет Маша. \nМожет потанцуем?";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "Маша: Давай!!!";
            TextGG.text = "";
            yield return new WaitForSeconds (1f); 
            Fade.SetBool("Fade_IN", true);
            yield return new WaitForSeconds (2.2f); 
            Fade.SetBool("Fade_IN", false);
            TextOther.text = "Маша: Было класно!!! \n Извини мне пора. Меня Папа сейчас заберет. \n Пока!!!";
            TextGG.text = "";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "";
            TextGG.text = "Пока!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            Other_poz0.SetActive(false);
            yield return new WaitForSeconds (0.5f);  
            Other_poz.sprite = Other_ref[2];
            Other_poz0.SetActive(true);
            TextOther.text = "Васян: Ну чё? Как дела?";
            TextGG.text = "";
            LButton.SetActive (true);
            LButton.transform.GetChild(0).GetComponent<Text>().text = "Пойти готовится";
            RButton.SetActive (true);
            RButton.transform.GetChild(0).GetComponent<Text>().text = "Продолжить тусовку";
            NeedBut = true;
            while (Answer == 0)
            {
                yield return new WaitForSeconds (0.02f);
            }
            LButton.SetActive (false);
            RButton.SetActive (false);

            if( Answer == 1) //Надо домой
            {
                RespectScool += 1;
                GG_poz.sprite = GG_ref[1];
                TextOther.text = "";
                TextGG.text = "Слушай уже поздно. Я наверно пойду домой.";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextOther.text = "Васян: Ладно давай. До завтра!!!";
                TextGG.text = "";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
            }
            else
            {
                RespectFriends += 1;

                TextOther.text = "";
                TextGG.text = "Погнали поищем ещё кого нибудь знакомого здесь!";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextOther.text = "Васян: Погнали!!!";
                TextGG.text = "";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                yield return new WaitForSeconds (1f); 
                Fade.SetBool("Fade_IN", true);
                yield return new WaitForSeconds (2.2f); 
                Fade.SetBool("Fade_IN", false);
                TextOther.text = "Васян: Ладно уже поздно погнали домой!!!";
                TextGG.text = "";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextOther.text = "";
                TextGG.text = "Ладно давай!!! Завтра встретимся!!!";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
            }
            yield return new WaitForSeconds (1f); 
            Fade.SetBool("Fade_IN", true);
            yield return new WaitForSeconds (2.2f);
            TextOther.text = "";
            Sound_OBJ.clip = Sound_ref[0];
            Sound_OBJ.Play();
            TextGG.text = "";
            GG_poz.sprite = GG_ref[3];
            Loc_poz.sprite = Locations[8];
            Other_poz.sprite = Other_ref[5];
            Fade.SetBool("Fade_IN", false);
            yield return new WaitForSeconds (1f); 
            TextOther.text = "Верулся... Жалко что тебя за столом не было... \n Иди спать Завтра у тебя экзамен";
            yield return new WaitForSeconds (2f); 
            Fade.SetBool("Fade_IN", true);
        }
        else //К родителям
        {
            yield return new WaitForSeconds (2.2f);
            Loc_poz.sprite = Locations[8];
            Eff_OBJ.clip = Effect_ref[1];
            Eff_OBJ.Play();
            GG_poz.sprite = GG_ref[1];
            Other_poz.sprite = Other_ref[5];
            Other_poz0.SetActive(true);
            TwoOther_poz.sprite = TwoOther_ref[0];
            TwoOther_poz0.SetActive(true);
            yield return new WaitForSeconds (2.2f);
            TextGG.text = "";
            Fade.SetBool("Fade_IN", false);
            yield return new WaitForSeconds (1f);
            TextOther.text = "Папа и Мама: Привет сынок! Давай к столу!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "";
            Answer = 0;
            LButton.SetActive (true);
            LButton.transform.GetChild(0).GetComponent<Text>().text = "К столу";
            RButton.SetActive (true);
            RButton.transform.GetChild(0).GetComponent<Text>().text = "Учить уроки";
            while (Answer == 0)
            {
                yield return new WaitForSeconds (0.02f);
            }
            LButton.SetActive (false);
            RButton.SetActive (false);

            if (Answer == 1) // К столу
            {
                RespectScool -= 1;
                TextOther.text = "";
                TextGG.text = "Сейчас, только руки помою.";
                GG_poz.sprite =GG_ref[2];
                yield return new WaitForSeconds (1f);
                Fade.SetBool("Fade_IN", true);
                yield return new WaitForSeconds (2.2f);
                Loc_poz.sprite = Locations[10];
                Fade.SetBool("Fade_IN", false);
                TextOther.text = "";
                TextGG.text = "Пап, ещё раз поздравляю тебя с днём рождения!!!";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextOther.text = "Папа: Спасибо сын.";
                TextGG.text = "";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                Fade.SetBool("Fade_IN", true);
                yield return new WaitForSeconds (2f);
                Loc_poz.sprite = Locations[10];
                Fade.SetBool("Fade_IN", false);
                TextOther.text = "";
                GG_poz.sprite = GG_ref[4];
                TextGG.text = "Ладно. Прости пап но у меня завтра экзамен...";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextOther.text = "Папа: Давай сын!!! Спасибо что посидел с нами!!!";
                TextGG.text = "";
                yield return new WaitForSeconds (1f);
                Fade.SetBool("Fade_IN", true);
            }
            else //Надо подготовится к экзамену
            {
                RespectScool += 1;
                TextOther.text = "";
                TextGG.text = "Извини пап. Мне надо окончательно подговится";
                GG_poz.sprite =GG_ref[4];
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                TextOther.text = "Папа: Ладно... Иди учись.";
                TextGG.text = "";
                NeedBut = true;
                while (ClickBut == false)
                {
                    yield return new WaitForSeconds (0.02f);  
                }
                ClickBut = false;
                Fade.SetBool("Fade_IN", true);
            }
        }
        yield return new WaitForSeconds (2f);  
        StartCoroutine(Starting6());
       
    }

    IEnumerator Starting6()
    {

        int MicroAnsver;
        TextOther.text = "";
        TextGG.text = "";
        LoadPoz = 6; 
        MicroAnsver = Answer;
        Checpoint();
        Answer = 0;
        GG_poz.sprite = GG_ref[1]; 
        Loc_poz.sprite = Locations[5];
        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (2f); 
        Clock.SetActive(true);
        Other_poz0.SetActive(false);
        TwoOther_poz0.SetActive(false);
        Fade.SetBool("Fade_IN", false);
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        Clock.SetActive(false);
        ClickBut = false;
        TextOther.text = "";
        TextGG.text = "Ну вот и настал день экзамена...";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        Eff_OBJ.clip = Effect_ref[1];
        Eff_OBJ.Play();
        yield return new WaitForSeconds (1f);
        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (1f);
        Eff_OBJ.clip = Effect_ref[2];
        Eff_OBJ.Play();
        GG_poz.sprite = GG_ref[3]; 
        Loc_poz.sprite = Locations[3];
        Other_poz.sprite = Other_ref[2];
        Other_poz0.SetActive(true);
        yield return new WaitForSeconds (0.5f);
        Fade.SetBool("Fade_IN", false);
        TextOther.text = "Васян: Ну что готов к экзамену?";
        TextGG.text = "";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        TextOther.text = "";
        TextGG.text = "Да вроде бы да";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        TextOther.text = "Васян: Эх... А я что то сомневаюсь в своих силах...";
        TextGG.text = "";
        LButton.SetActive (true);
        LButton.transform.GetChild(0).GetComponent<Text>().text = "Поддержать";
        RButton.SetActive (true);
        RButton.transform.GetChild(0).GetComponent<Text>().text = "Наругать";
        NeedBut = true;
        while (Answer == 0)
        {
            yield return new WaitForSeconds (0.02f);
        }
        LButton.SetActive (false);
        RButton.SetActive (false);
        if(Answer == 1)
        {
            RespectFriends += 1; 

            TextOther.text = "";
            TextGG.text = "Ничего страшного. Я уверен, ты справишься!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "Васян: Ладно. Погнали!!!";
            TextGG.text = "";
            yield return new WaitForSeconds (1f);  
        }
        else
        {
            RespectFriends -= 1; 

            TextOther.text = "";
            TextGG.text = "Ну чё ты так. Надо было подготовится!!!";
            NeedBut = true;
            while (ClickBut == false)
            {
                yield return new WaitForSeconds (0.02f);  
            }
            ClickBut = false;
            TextOther.text = "Васян: Ладно...";
            TextGG.text = "";
            yield return new WaitForSeconds (1f);
            Other_poz0.SetActive(false);  
            yield return new WaitForSeconds (1f);
        }

        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (2.2f);
        TextOther.text = "";
        Eff_OBJ.Stop();
        TextGG.text = "";
        Other_poz.sprite = Other_ref[1];
        Other_poz0.SetActive(true); 
        GG_poz0.SetActive(false);
        Loc_poz.sprite = Locations[4];
        yield return new WaitForSeconds (0.5f);
        Fade.SetBool("Fade_IN", false);
        yield return new WaitForSeconds (0.5f);
        TextOther.text = "Мария Ивановна: Ну вот и настал решающий вашу судьбу день...";
        NeedBut = true;
        while (ClickBut == false)
        {
            yield return new WaitForSeconds (0.02f);  
        }
        ClickBut = false;
        TextOther.text = "Мария Ивановна: Вот ваши листы. \n Удачи!!!";
        yield return new WaitForSeconds (4f);  
        Fade.SetBool("Fade_IN", true);
        yield return new WaitForSeconds (2.2f);  
        StartCoroutine (Starting7());
    }

    IEnumerator Starting7()
    {
        YesFinal = true;
        GrandButton.SetActive(false);
        int MicroAnsver;
        TextOther.text = "";
        TextGG.text = "";
        LoadPoz = 7; 
        MicroAnsver = Answer;
        UI_Panel.SetActive(false);
        yield return new WaitForSeconds (1f);  
        Checpoint();
        Answer = 0;
        Sound_OBJ.clip = Sound_ref[1];
        Sound_OBJ.Play();
        Loc_poz.sprite = Locations[12];
        FinalPanel[0].SetActive(true);
        //Условие ответа
        if (RespectScool < 3)
        {
            if(RespectFriends>= 3 && RespectParent>= 3)
            {
                FinalPanel[1].GetComponent<Text>().text = "Вот ваши результаты: \n К сожалению, вы завалили экзамен, потому что уделяли этому недостаточно много времени, но вам удалось остаться в хороших отношениях с семьей и друзьями.";
            }
            else if (RespectFriends < 3 && RespectParent < 3)
            {
                FinalPanel[1].GetComponent<Text>().text = "Вот ваши результаты: К сожалению, вы завалили экзамен, потому что уделяли этому недостаточно много времени, к тому же вы испортили отношения с семьей и друзьями, потому что уделяли им недостаточно времени.";
            }
        }
        else if (RespectScool >= 3)
        {
            if(RespectFriends < 3 )
            {
                FinalPanel[1].GetComponent<Text>().text = "Вот ваши результаты: \n Вы хорошо написали экзамен, потому что усердно к нему готовились, к тому же вам удалось сохранить хорошие отношения с семьей, но друзей вы потеряли, потому что мало с ними общались";
            }
            else if(RespectParent < 3 )
            {
                FinalPanel[1].GetComponent<Text>().text = "Вот ваши результаты: \n Вы хорошо написали экзамен, потому что усердно к нему готовились, к тому же вам удалось сохранить хорошие отношения с друзьями, но вы испортили отношения с семьей, потому что не уделяли им достаточного времени";
            }
        }
        Fade.SetBool("Fade_IN", false);
        FinalPanel[2].SetActive(true);
    }  
}
