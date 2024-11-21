﻿using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace GestorDeClientes
{
    public class Program
    {
        [System.Serializable]
        public struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;

        }
        static List<Cliente> clientes = new List<Cliente>();
        enum Menu { Listagem = 1, Adicionar, Remover, Sair };

        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de clientes - Bem vindo!");
                Console.WriteLine("1 - Listagem\n2 - Adicionar\n3 - Remover\n4 - Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }

        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente: ");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente: ");
            cliente.cpf = Console.ReadLine();
            
            clientes.Add(cliente);
            Salvar();
            Console.WriteLine("Cadastro concluído, aperte enter para sair.");
            Console.ReadLine();
        }

        static void Listagem()
        {
            if (clientes.Count > 0)
            {
                Console.WriteLine("Lista de clientes: ");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email : {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("=====================");
                    i++;
                }
                
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }
            Console.WriteLine("Aperte enter para sair.");
            Console.ReadLine();
        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que você quer remover:");
            int id = int.Parse( Console.ReadLine() );
            if( id  >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();

            }
            else
            {
                Console.WriteLine("ID digitado é inválido, tente novamente!");
                Console.ReadLine();
            }
        }

        public static void Salvar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);
            XmlSerializer encoder = new XmlSerializer(clientes.GetType(), new XmlRootAttribute("clientes"));
            encoder.Serialize(stream, clientes);
            stream.Close();
        }

        public static void Carregar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);
            try
            {

                XmlSerializer encoder = new XmlSerializer(clientes.GetType(), new XmlRootAttribute("clientes"));
                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }
                
            }
            catch (Exception ex)
            {
                clientes = new List<Cliente>();
            }
            stream.Close();
        }
    
    }
}