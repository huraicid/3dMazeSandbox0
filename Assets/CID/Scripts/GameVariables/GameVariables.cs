/// <summary>
/// �Q�[�����ŋ��ʂŎg�p����f�[�^�N���X�ł��B
/// </summary>
public static class GameVariables
{
    /// <summary>
    /// ���݂̃t���A
    /// </summary>
    public static int floor = 0;

    /// <summary>
    /// �_���W�����̍ŏ�K�̃t���A
    /// </summary>
    public static int maxFloor = 4;
    

    /// <summary>
    /// �^�C�}�[�N���t���O
    /// </summary>
    public static bool isTimerActive = false;

    /// <summary>
    /// ���݂̃^�C�}�[
    /// </summary>
    public static float currentTime = 0f;


    /// <summary>
    /// �f�[�^�����ׂď��������܂��B
    /// </summary>
    public static void Init()
    {
        floor = 0;
        isTimerActive = false;
        currentTime = 0f;
    }
}
