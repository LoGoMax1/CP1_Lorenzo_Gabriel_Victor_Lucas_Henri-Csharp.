using System;
class Program
{
    // Definindo os produtos disponíveis
    public static string[] produtosDisponiveis = { "X-Burguer", "Refrigerante", "Sorvete" };
    public static decimal[] precos = { 20.0m, 5.0m, 10.0m };   // Preços dos produtos
    public static int[] qtdeProdutos = new int[] { 0, 0, 0 }; // Quantidades de cada produto no pedido
    static void Main()
    {
        int opcao;
        do
        {
            // Exibindo menu
            Console.Clear();
            Console.WriteLine("Bem-vindo à Lanchonete Virtual!");
            Console.WriteLine("1 - Listar produtos disponíveis");
            Console.WriteLine("2 - Adicionar produto ao pedido");
            Console.WriteLine("3 - Remover produto do pedido");
            Console.WriteLine("4 - Visualizar pedido atual");
            Console.WriteLine("5 - Finalizar pedido e sair");
            Console.Write("Escolha uma opção: ");
            // Usando TryParse para evitar exceções se o usuário digitar algo não numérico
            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
                Console.WriteLine("Pressione Enter para continuar...");
                Console.ReadLine();
                continue;
            }
            switch (opcao)
            {
                case 1:
                    ListarProdutos();
                    PausarTela();
                    break;
                case 2:
                    AdicionarProduto();
                    break;
                case 3:
                    RemoverProduto();
                    break;
                case 4:
                    VisualizarPedido();
                    PausarTela();
                    break;
                case 5:
                    FinalizarPedido();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    PausarTela();
                    break;
            }
            if (opcao == 5)
            {
                break;
            }
        } while (true);
    }
    // Método para "pausar" a tela
    private static void PausarTela()
    {
        Console.WriteLine("\nPressione Enter para continuar...");
        Console.ReadLine();
    }
    // Método para listar produtos disponíveis
    static void ListarProdutos()
    {
        Console.Clear();
        Console.WriteLine("Produtos disponíveis:\n");
        for (int i = 0; i < produtosDisponiveis.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {produtosDisponiveis[i]} - R$ {precos[i]:0.00}");
        }
    }
    // Método para adicionar produto ao pedido
    static void AdicionarProduto()
    {
        Console.Clear();
        ListarProdutos();
        Console.Write("\nEscolha o número do produto: ");
        if (!int.TryParse(Console.ReadLine(), out int produtoEscolhido))
        {
            Console.WriteLine("Entrada inválida. Nenhum produto foi adicionado.");
            PausarTela();
            return;
        }
        produtoEscolhido -= 1; // Ajusta para índice do array
        if (produtoEscolhido >= 0 && produtoEscolhido < produtosDisponiveis.Length)
        {
            Console.Write("Informe a quantidade: ");
            if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade <= 0)
            {
                Console.WriteLine("Quantidade inválida. Nenhum produto foi adicionado.");
                PausarTela();
                return;
            }
            qtdeProdutos[produtoEscolhido] += quantidade; // Atualiza a quantidade no pedido
            Console.WriteLine($"\n{quantidade} x {produtosDisponiveis[produtoEscolhido]} adicionado ao pedido.");
        }
        else
        {
            Console.WriteLine("Produto inválido.");
        }
        PausarTela();
    }
    // Método para remover produto do pedido
    static void RemoverProduto()
    {
        Console.Clear();
        if (PedidoVazio())
        {
            Console.WriteLine("Seu pedido está vazio.");
            PausarTela();
            return;
        }
        Console.WriteLine("Produtos no seu pedido:");
        for (int i = 0; i < produtosDisponiveis.Length; i++)
        {
            if (qtdeProdutos[i] != 0)
            {
                Console.WriteLine($"{i} - {qtdeProdutos[i]} x {produtosDisponiveis[i]} = R$ {precos[i] * qtdeProdutos[i]:0.00}");
            }
        }
        Console.Write("\nDigite o código do produto para remover: ");
        if (!int.TryParse(Console.ReadLine(), out int produtoRemover))
        {
            Console.WriteLine("Código inválido.");
            PausarTela();
            return;
        }
        if (produtoRemover >= 0 && produtoRemover < produtosDisponiveis.Length && qtdeProdutos[produtoRemover] > 0)
        {
            Console.Write("Quantas unidades deseja remover? ");
            if (!int.TryParse(Console.ReadLine(), out int quantidadeRemover) || quantidadeRemover <= 0)
            {
                Console.WriteLine("Quantidade inválida.");
                PausarTela();
                return;
            }
            if (quantidadeRemover >= qtdeProdutos[produtoRemover])
            {
                qtdeProdutos[produtoRemover] = 0;
                Console.WriteLine($"Produto {produtosDisponiveis[produtoRemover]} removido do pedido.");
            }
            else
            {
                qtdeProdutos[produtoRemover] -= quantidadeRemover;
                Console.WriteLine($"Quantidade de {produtosDisponiveis[produtoRemover]} ajustada para {qtdeProdutos[produtoRemover]}.");
            }
        }
        else
        {
            Console.WriteLine("Produto não encontrado no pedido.");
        }
        PausarTela();
    }
    // Método para verificar se o pedido está vazio
    static bool PedidoVazio()
    {
        foreach (var quantidade in qtdeProdutos)
        {
            if (quantidade > 0)
            {
                return false;
            }
        }
        return true;
    }
    // Método para visualizar o pedido atual
    static void VisualizarPedido()
    {
        Console.Clear();
        if (PedidoVazio())
        {
            Console.WriteLine("Seu pedido está vazio.");
            return;
        }
        Console.WriteLine("Seu pedido atual:\n");
        decimal totalPedido = 0;
        for (int i = 0; i < produtosDisponiveis.Length; i++)
        {
            if (qtdeProdutos[i] > 0)
            {
                decimal subtotal = precos[i] * qtdeProdutos[i];
                totalPedido += subtotal;
                Console.WriteLine($"{i} - {qtdeProdutos[i]} x {produtosDisponiveis[i]} = R$ {subtotal:0.00}");
            }
        }
        Console.WriteLine($"\nTotal do pedido: R$ {totalPedido:0.00}");
    }
    // Método para finalizar o pedido e aplicar descontos
    static void FinalizarPedido()
    {
        Console.Clear();
        if (PedidoVazio())
        {
            Console.WriteLine("Seu pedido está vazio.");
            PausarTela();
            return;
        }
        decimal valorBruto = 0;
        decimal qtdeFinal = 0;
        Console.WriteLine("Detalhes do pedido:\n");
        for (int i = 0; i < produtosDisponiveis.Length; i++)
        {
            if (qtdeProdutos[i] > 0)
            {
                decimal subtotal = precos[i] * qtdeProdutos[i];
                valorBruto += subtotal;
                qtdeFinal += qtdeProdutos[i];
                Console.WriteLine($"{qtdeProdutos[i]} x {produtosDisponiveis[i]} = R$ {subtotal:0.00}");
            }
        }
        // Calculando desconto
        decimal desconto = (valorBruto > 100) ? valorBruto * 0.10m : 0;
        // Calculando frete
        // Frete é grátis se qtdeFinal >= 5, caso contrário, R$2,00 por item
        decimal valorFrete = (qtdeFinal >= 5) ? 0 : (qtdeFinal * 2);
        // Valor final é o valorBruto - desconto + frete (se houver)
        decimal valorFinal = valorBruto - desconto + valorFrete;
        // Exibindo resumo
        Console.WriteLine("\nResumo do Pedido:");
        Console.WriteLine($"Total de itens: {qtdeFinal}");
        Console.WriteLine($"Valor Bruto: R$ {valorBruto:0.00}");
        Console.WriteLine($"Desconto: R$ {desconto:0.00}");
        Console.WriteLine($"Frete: R$ {valorFrete:0.00}");
        Console.WriteLine($"Valor Final a Pagar: R$ {valorFinal:0.00}");
        Console.WriteLine("\nObrigado pela compra! Até logo.");
        PausarTela();
    }
}
