using System;
    
namespace ERP26._1123hrs
{
            // Remova ou renomeie esta classe Program se já existir outra definição em outro arquivo.
    // Aqui, renomeando para evitar conflito:
    class ProdutoApp
    {
        static ProdutoService produtoService = new ProdutoService();

        // Removido o método Main para evitar múltiplos pontos de entrada.
        // O método Main deve existir apenas em uma classe no projeto.
    }

    public class Produto
    {
        // Adicione as propriedades relevantes para Produto aqui
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public override string ToString()
        {
            return $"Nome: {Nome}, Preço: {Preco:C}";
        }
    }
}
