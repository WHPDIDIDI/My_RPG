using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class Globe
{
    public static string nextSceneName;
    public static int bg =0;
    public static void GetBG()
    {
        for (int i = 0; i < AsyncLoadScene.bgimage.Length; i++)
        {
            if (i==bg)
            {
                
            }
            else
            {
                AsyncLoadScene.bgimage[i].gameObject.SetActive(false);
            }
        }
        if (bg==AsyncLoadScene.bgimage.Length-1)
        {
            bg = 0;
        }
        bg++;
    }
}

public class AsyncLoadScene : MonoBehaviour
{
    public GameObject bg;
    public static Image[] bgimage;
    public Slider loadingSlider;

    public Text loadingText;

    private float loadingSpeed = 1;

    private float targetValue;

    private AsyncOperation operation;

    // Use this for initialization
      void Start()
    {
        bgimage = Resources.LoadAll<Image>("BGPicture");
        //Globe.GetBG();
        loadingSlider.value = 0.0f;

        if (SceneManager.GetActiveScene().name == "Loading")
        {
            //启动协程
            StartCoroutine(AsyncLoading());
        }
    }

    IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync(Globe.nextSceneName);
        //阻止当加载完成自动切换（等待进度条）
        operation.allowSceneActivation = false;
        //yield return new WaitForSeconds(2);等待2秒
        yield return operation;//正式开启携程
    }

    // Update is called once per frame
    void Update()
    {//更新进度条
        targetValue = operation.progress;//加载进度

        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9
            targetValue = 1.0f;
        }

        if (targetValue != loadingSlider.value)
        {
            //插值运算
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);
            if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
            {
                loadingSlider.value = targetValue;
            }
        }

        loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

        if ((int)(loadingSlider.value * 100) == 100)
        {
            //允许异步加载完毕后自动切换场景
            operation.allowSceneActivation = true;
        }
    }
}
