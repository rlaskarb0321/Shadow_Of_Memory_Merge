using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public static string _nextScene;
    public Image _loadingFillImg;
    
    private void Start()
    {
        StartCoroutine(LoadSceneCor());
    }

    // �Ű������� �ҷ��� �� �̸����޴´�, �ε����� �ҷ����� �ε��� �ش罺ũ��Ʈ�� Start�Լ������� �Ű��������� �´� ���� �ҷ��´�.
    public static void LoadScene(string sceneName, int index = -1)
    {
        if (0 < index && index <= ConstData._SAVELISTCOUNT)
        {
            GameDataPackage.SetData(index);
        }

        _nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator LoadSceneCor()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            // ���� ȣ���� �󸶳� �̷��������
            if (op.progress < 0.9f)
            {
                _loadingFillImg.fillAmount = Mathf.Lerp(_loadingFillImg.fillAmount, op.progress, timer);
                if (_loadingFillImg.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                _loadingFillImg.fillAmount = Mathf.Lerp(_loadingFillImg.fillAmount, 1.0f, timer);
                if (_loadingFillImg.fillAmount == 1.0f)
                {
                    yield return new WaitForSeconds(1.5f);

                    // �� Ȱ��ȭ�Ǹ�, ���̵忬���� �� Obj���� anim�� �����Ű�� ������ ������ ���忬���� ����x
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}