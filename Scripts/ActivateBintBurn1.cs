using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActivateBintBurn1 : MonoBehaviour
{

    public TextMeshProUGUI quest;
    public TextMeshProUGUI help;

    public GameObject Pants;
    public GameObject PantsCut;

    public GameObject Ice;
    public GameObject Obmotka;
    public GameObject Salfetka;

    public int AddResult = 0;
    public int Stage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (MainSceneTest.AddSimulator0 == 0)
        {
            MainSceneTest mainScene = gameObject.AddComponent<MainSceneTest>();
            mainScene.onAddSimulatorResultButtonClick("Ожог 2 степени");
            MainSceneTest.AddSimulator0 = 1;

        }
        Debug.Log("Via" + MainSceneTest.AddSimulator0);

        if (collision.gameObject.tag == "Noznici" && Stage == 1)
        {
            Pants.SetActive(false);
            PantsCut.SetActive(true);

            Destroy(collision.gameObject);

            quest.text = "2. Охладите место ожога с помощью холодного компресса и подождите 5 секунд (в реальности надо ждать около 15 минут)";
            help.text = "1. Поместить место ожога под проточную воду, положить сверху лед или любое другое охлаждающее вещество (1-2 степень ожога).\r\n" +
                    "2. Наложить стерильную повязку, а сверху приложить лед (если из пузырей вытекает жидкость 3 степень ожога).\r\n" +
                    "3. Оптимальное время охлаждения – 10-20 мин. В случае химического ожога промывание нужно проводить от 40 мин. Эта мера эффективна в течение 2 часов после получения ожога и не только уменьшает боль, но и - глубину поражения.\r\n" +
                    "\r\n 4. При ожоге нельзя использовать химические вещества, йод, спирт, масляные мази и другие жиросодержащие продукты.\r\n";

            Stage = 2;
            }

        if (collision.gameObject.tag == "Ice" && Stage == 2)
        {
            Ice.SetActive(true);
            Destroy(collision.gameObject);

            Invoke("TimerEnded", 5f);

        }

        if (collision.gameObject.tag == "Salfetka" && Stage == 3)
        {
            Salfetka.SetActive(true);
            Destroy(collision.gameObject);

            Stage = 4;
        }

        if (collision.gameObject.tag == "Bint" && Stage == 4)
        {
            Obmotka.SetActive(true);
            Destroy(collision.gameObject);

            quest.text = "4. Первая помощь оказана";
            help.text = "1. При этом не следует сдавливать место ожога. Если обожжены конечности, стоит зафиксировать места ожогов, осторожно наложив шины.\r\n" +
                "2. При ожогах 3-4 степени бинтовать пораженную поверхность нельзя.\r\n" +
                "3. Дать пострадавшему любой анальгетик - Парацетамол, Анальгин или жаропонижающее средство. Они позволят предупредить развитие болевого шока и резкий подъем температуры. Затем если ожог довольно сильный (3-4 степень) или поражает довольно большую площадь доставьте пациента в больницу.\r\n";

            Stage = 5;

            Debug.Log("Мы зашли в ожоги и изменили");
            MainSceneTest mainScene = gameObject.AddComponent<MainSceneTest>();
            mainScene.onUpdateSimulatorResultButtonClick();
        }

    }
    void TimerEnded()
    {
        quest.text = "3. Положите на место ожога стерильную салфетку, а затем забинтуйте место поражения с помощью бинта";
        help.text = "1. Вложить под руку, локоть и плечо прокладку из ваты для предупреждения сдавления и развития некроза.\r\n";
        Destroy(Ice);

        Stage = 3;
    }
}
