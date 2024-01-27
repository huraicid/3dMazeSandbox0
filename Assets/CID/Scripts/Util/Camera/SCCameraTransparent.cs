/// License
/// 
/// NYSL Version 0.9982
/// 
/// A. �{�\�t�g�E�F�A�� Everyone'sWare �ł��B���̃\�t�g����ɂ�����l��l���A
///    �������̍�������̂������̂Ɠ����悤�ɁA���R�ɗ��p���邱�Ƃ��o���܂��B
/// 
///   A-1. �t���[�E�F�A�ł��B��҂���͎g�p������v�����܂���B
///   A-2. �L��������}�̂̔@�����킸�A���R�ɓ]�ځE�Ĕz�z�ł��܂��B
///   A-3. �����Ȃ��ނ� ���ρE���v���O�����ł̗��p ���s���Ă��\���܂���B
///   A-4. �ύX�������̂╔���I�Ɏg�p�������̂́A���Ȃ��̂��̂ɂȂ�܂��B
///        ���J����ꍇ�́A���Ȃ��̖��O�̉��ōs���ĉ������B
/// 
/// B. ���̃\�t�g�𗘗p���邱�Ƃɂ���Đ��������Q���ɂ��āA��҂�
///    �ӔC�𕉂�Ȃ����̂Ƃ��܂��B�e���̐ӔC�ɂ����Ă����p�������B
/// 
/// C. ����Ґl�i���� SakuraCrowd �ɋA�����܂��B���쌠�͕������܂��B
/// 
/// D. �ȏ�̂R���́A�\�[�X�E���s�o�C�i���̑o���ɓK�p����܂��B

using System.Collections.Generic;
using UnityEngine;
using System.Linq;  // Except

/// <summary>
/// �J�����ƑΏۂƂ̊Ԃ̎Օ���(Cover)�𓧖������܂��B
/// �J�����ɕt�����Ă��������B
/// �����ɂ���Օ����� Renderer �R���|�[�l���g��t�����Ă���K�v������܂��B
/// </summary>
public class SCCameraCoverTransparent : MonoBehaviour
{
    /// <summary>
    /// ��ʑ̂��w�肵�Ă��������B
    /// </summary>
    [SerializeField]
    private Transform subject_;

    /// <summary>
    /// �Օ����̃��C���[���̃��X�g�B
    /// </summary>
    [SerializeField]
    private List<string> coverLayerNameList_;

    /// <summary>
    /// �Օ����Ƃ��郌�C���[�}�X�N�B
    /// </summary>
    private int layerMask_;

    /// <summary>
    /// ����� Update �Ō��o���ꂽ�Օ����� Renderer �R���|�[�l���g�B
    /// </summary>
    public List<Renderer> rendererHitsList_ = new List<Renderer>();

    /// <summary>
    /// �O��� Update �Ō��o���ꂽ�Օ����� Renderer �R���|�[�l���g�B
    /// ����� Update �ŊY�����Ȃ��ꍇ�́A�Օ����ł͂Ȃ��Ȃ����̂� Renderer �R���|�[�l���g��L���ɂ���B
    /// </summary>
    public Renderer[] rendererHitsPrevs_;


    // Use this for initialization
    void Start()
    {
        // �Օ����̃��C���[�}�X�N���A���C���[���̃��X�g���獇������B
        layerMask_ = 0;
        foreach (string _layerName in coverLayerNameList_)
        {
            layerMask_ |= 1 << LayerMask.NameToLayer(_layerName);
        }

    }


    // Update is called once per frame
    void Update()
    {
        // �J�����Ɣ�ʑ̂����� ray ���쐬
        Vector3 _difference = (subject_.transform.position - this.transform.position);
        Vector3 _direction = _difference.normalized;
        Ray _ray = new Ray(this.transform.position, _direction);

        // �O��̌��ʂ�ޔ����Ă���ARaycast ���č���̎Օ����̃��X�g���擾����
        RaycastHit[] _hits = Physics.RaycastAll(_ray, _difference.magnitude, layerMask_);


        rendererHitsPrevs_ = rendererHitsList_.ToArray();
        rendererHitsList_.Clear();
        // �Օ����͈ꎞ�I�ɂ��ׂĕ`��@�\�𖳌��ɂ���B
        foreach (RaycastHit _hit in _hits)
        {
            // �Օ�������ʑ̂̏ꍇ�͗�O�Ƃ���
            if (_hit.collider.gameObject == subject_)
            {
                continue;
            }

            // �Օ����� Renderer �R���|�[�l���g�𖳌��ɂ���
            Renderer _renderer = _hit.collider.gameObject.GetComponent<Renderer>();
            if (_renderer != null)
            {
                rendererHitsList_.Add(_renderer);
                _renderer.enabled = false;
            }
        }

        // �O��܂őΏۂŁA����ΏۂłȂ��Ȃ������̂́A�\�������ɖ߂��B
        foreach (Renderer _renderer in rendererHitsPrevs_.Except<Renderer>(rendererHitsList_))
        {
            // �Օ����łȂ��Ȃ��� Renderer �R���|�[�l���g��L���ɂ���
            if (_renderer != null)
            {
                _renderer.enabled = true;
            }
        }

    }
}
