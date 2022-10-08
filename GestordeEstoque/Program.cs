using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace GestordeEstoque
{
    class Program : Produto
    {
        static List<IEstoque> produtos = new List<IEstoque>();

        enum Menu { Listar = 1, Adicionar, Remover, Entrada, Saida, Sair}

        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while (escolheuSair == false)
            {
                Console.WriteLine("Sistema de Estoque");
                Console.WriteLine("1- Listar\n2- Adicionar\n3- Remover\n4- Registrar Entrada\n5- Registrar Saida\n6- Sair");
                string opStr = Console.ReadLine();
                int opInt = int.Parse(opStr);
                Menu escolha = (Menu)opInt;

                if (opInt > 0 && opInt < 7)
                {
                    switch (escolha)
                    {
                        case Menu.Listar:
                            Listagem();
                            break;
                        case Menu.Adicionar:
                            Cadastro();
                            break;
                        case Menu.Remover:
                            Remover();
                            break;
                        case Menu.Entrada:
                            Entrada();
                            break;
                        case Menu.Saida:
                            Saida();
                            break;
                        case Menu.Sair:
                            escolheuSair = true;
                            break;
                    }
                }
                else
                {
                    escolheuSair = true;
                }
                Console.Clear();

            }

        }

        static void Listagem()
        {
            Console.WriteLine("Lista de Produtos");
            int i = 0;
            foreach(IEstoque produto in produtos)
            {
                Console.WriteLine("ID: " + i);
                produto.Exibir();
                i++;
            }
            Console.ReadLine();
        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer remover: ");
            int id = int.Parse(Console.ReadLine());
            if(id >= 0 && id < produtos.Count)
            {
                produtos.RemoveAt(id);
                Salvar();
            }

        }
        static void Entrada()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer dar entrada: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count)
            {
                produtos[id].AdicionarEntrada();
                Salvar();
            }

        }
        static void Saida()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer dar saida: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count)
            {
                produtos[id].AdicionarSaida();
                Salvar();
            }

        }
        static void Cadastro()
        {
            Console.WriteLine("Cadastro de Produto");
            Console.WriteLine("1- Produto Fisico\n2- Ebook\n3- Curso");
            string opStr = Console.ReadLine();
            int escolhaInt = int.Parse(opStr);
            switch (escolhaInt)
            {
                case 1:
                    CadastrarPFisico();
                    break;
                case 2:
                    CadastrarEbook();
                    break;
                case 3:
                    CadastrarCurso();
                    break;
            }
        }

        static void CadastrarPFisico()
        {
            Console.WriteLine("Cadastrando produto fisico: ");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Console.Write("Frete: ");
            float frete = float.Parse(Console.ReadLine());
            ProdutoFisico pf = new ProdutoFisico(nome, preco, frete);
            produtos.Add(pf);
            Salvar();
        }
        static void CadastrarEbook()
        {
            Console.WriteLine("Cadastrando Ebook: ");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Console.Write("Autor : ");
            string autor = Console.ReadLine();

            Ebook eb = new Ebook(nome, preco, autor);
            produtos.Add(eb);
            Salvar();

        }
        static void CadastrarCurso()
        {
            Console.WriteLine("Cadastrando Curso: ");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Console.Write("Autor : ");
            string autor = Console.ReadLine();

            Curso C = new Curso(nome, preco, autor);
            produtos.Add(C);
            Salvar();

        }

        static void Salvar()
        {
            FileStream stream = new FileStream("produtos.dat" ,FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, produtos);

            stream.Close();
        }
        static void Carregar()
        {
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            try
            {
                produtos = (List<IEstoque>)encoder.Deserialize(stream);

                if(produtos == null)
                {
                    produtos = new List<IEstoque>();
                }
            }
            catch(Exception e)
            {
                produtos = new List<IEstoque>();

            }

            stream.Close();

        }

    }
}