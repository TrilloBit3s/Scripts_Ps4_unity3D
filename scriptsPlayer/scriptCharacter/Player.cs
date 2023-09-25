using UnityEngine;

public class Player : MonoBehaviour
{
  private CharacterController personagem;   
  public float velocidade = 5f;
  public float velocidadeRotacao = 10f;
  public Camera seguirCamera;
  private Vector3 velocidadeJogador;
  private bool jogadorNoChao;
  public float alturaPulo = 1.0f;
  public float gravidade = -9.81f;

  void Start(){
      personagem = GetComponent<CharacterController>();    
  }

  void Update(){
      Mover();    
  }

  void Mover(){
      jogadorNoChao = personagem.isGrounded;
      if (jogadorNoChao && velocidadeJogador.y < 0){
          velocidadeJogador.y = 0f;
      }

      float hInput = Input.GetAxis("Horizontal");
      float vInput = Input.GetAxis("Vertical");

      Vector3 moveInput = Quaternion.Euler(0, seguirCamera.transform.eulerAngles.y, 0) * new Vector3(hInput, 0, vInput);
      Vector3 movementDirection = moveInput.normalized;

      personagem.Move(movementDirection * velocidade * Time.deltaTime);

      if (movementDirection != Vector3.zero){
          Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
          transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, velocidadeRotacao * Time.deltaTime);
      }
      if (Input.GetButtonDown("Jump") && jogadorNoChao){
          velocidadeJogador.y += Mathf.Sqrt(alturaPulo * -3.0f * gravidade);
      }

      velocidadeJogador.y += gravidade * Time.deltaTime;
      personagem.Move(velocidadeJogador * Time.deltaTime);
  }
}