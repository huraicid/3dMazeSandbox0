using UnityEngine;

/// <summary>
/// �K�i�̓���ɂ��ċK�肷��X�N���v�g�N���X�ł��B
/// </summary>
public class StairBehaviour : MonoBehaviour
{
    /// <summary>
    /// �K�i�ɐG�ꂽ�Ƃ��̓�����s���܂��B
    /// </summary>
    /// <param name="other">�G�ꂽ�I�u�W�F�N�g�i���g�p�j</param>
    private void OnTriggerEnter(Collider other)
    {
        // ���̃t���A�����擾����
        string targetSceneName = GetNextFloorName();

        // ���̃t���A�ɐi�ށiGameManager�Ɉڏ�����j
        GameObject gameManagerObject = GameObject.Find("GameManager");
        GameManager script = gameManagerObject.GetComponent<GameManager>();
        script.GoToNextFloor(targetSceneName);
    }

    /// <summary>
    /// ���̃t���A�̃V�[�������擾���܂��B
    /// </summary>
    /// <returns></returns>
    private string GetNextFloorName()
    {
        if(GameVariables.floor == 2)
        {
            return "toyBox";
        }

        return "mazeFloor";
    }
}
