using UnityEngine;

/// <summary>
/// �v���C���[�𑀍삷��X�N���v�g�N���X�ł��B
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 3; // �ړ����x
    [SerializeField] private float jumpPower = 0; // �W�����v��
    private CharacterController _characterController; // CharacterController�̃L���b�V��
    private Transform _transform; // Transform�̃L���b�V��
    private Vector3 _moveVelocity; // �L�����̈ړ����x���

    private void Start()
    {
        // ���t���[���A�N�Z�X����̂ŁA���ׂ������邽�߂ɃL���b�V�����Ă���
        // Transform���L���b�V������Ə����������ׂ�������
        _characterController = GetComponent<CharacterController>(); 
        _transform = transform; 
    }

    private void Update()
    {
        // ���͎��ɂ��ړ������i�����𖳎����Ă���̂ŁA�L�r�L�r�����j
        _moveVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
        _moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed;

        // �ړ������Ɍ���
        _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));

        if (_characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                // �W�����v����
                Debug.Log("�W�����v�I");
                _moveVelocity.y = jumpPower; // �W�����v�̍ۂ͏�����Ɉړ�������
            }
        }
        else
        {
            // �d�͂ɂ�����
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        // �I�u�W�F�N�g�𓮂���
        _characterController.Move(_moveVelocity * Time.deltaTime);

        // �ړ��X�s�[�h��animator�ɔ��f
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
    }
}