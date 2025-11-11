using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Se você precisar desabilitar o botão

public class GerenciadorDeAvanco : MonoBehaviour
{
    // O nome da cena da próxima fase para onde este botão levaria
    public string numeroDaProximaFase = "2";

    // O nome da cena da fase atual
    public string numeroDaFaseAtual = "1";

    // O requisito mínimo de estrelas para liberar esta próxima fase
    public int requisitoMinimoEstrelas = 2; // Para Fase 2

    // Referência ao botão "Próxima Fase" no painel de conclusão
    public Button botaoProximaFase;

    // O número total de fases no jogo
    public int quantidadeDeFases = 3;

    void Start()
    {
        // Certifica-se de que o painel só libera o botão se o requisito for atendido
        VerificarRequisitoEAtualizarBotao();
    }

    // Método que calcula as estrelas e habilita/desabilita o botão
    public void VerificarRequisitoEAtualizarBotao()
    {
        int estrelasSalvas = CalcularTotalEstrelas();

        // Se o botão for público e estiver no painel, ele será desativado se o requisito não for atendido.
        if (botaoProximaFase != null)
        {
            botaoProximaFase.interactable = estrelasSalvas >= requisitoMinimoEstrelas;
        }

        // Você também pode simplesmente não mostrar o botão 'Próxima Fase'
        // se o requisito não for atingido, e mostrar apenas o botão 'Menu Principal'.
    }

    // Função para calcular o total de estrelas (reutilizada do seu EventSystem script)
    private int CalcularTotalEstrelas()
    {
        int estrelasSalvas = 0;
        for (int i = 1; i <= quantidadeDeFases; i++)
        {
            // O nome da chave precisa ser EXATAMENTE igual ao nome usado ao SALVAR as estrelas
            estrelasSalvas += PlayerPrefs.GetInt("Estrelas_Fase" + i, 0);
        }
        return estrelasSalvas;
    }

    // Este método deve ser chamado pelo onClick() do botão 'Próxima Fase'
    public void CarregarProximaFase()
    {
        // Não é estritamente necessário checar novamente aqui,
        // mas é uma boa prática de segurança para evitar exploits ou erros.
        if (CalcularTotalEstrelas() >= requisitoMinimoEstrelas)
        {
            SceneManager.LoadScene(numeroDaProximaFase);
        }
        else
        {
            // O jogador não deveria conseguir clicar no botão se ele estiver desabilitado,
            // mas caso chegue aqui, você pode adicionar um debug ou um feedback visual.
            Debug.LogWarning("Requisito de estrelas não atendido para avançar.");
        }
    }
}