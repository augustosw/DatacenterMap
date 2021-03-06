﻿namespace DatacenterMap.Domain.Entidades
{
    public class Usuario : EntidadeBasica
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public Usuario()
        {
        }

        public bool ValidarSenha(string senha)
        {       
            return Criptografia.CriptografarSenha(Email, senha).Equals(Senha);
        }

        public override bool Validar()
        {
            Mensagens.Clear();

            if (string.IsNullOrWhiteSpace(Nome))
                Mensagens.Add("Nome é inválido.");

            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@") || !Email.Contains("."))
                Mensagens.Add("Email é inválido.");

            if (string.IsNullOrWhiteSpace(Senha) || Senha.Length < 8)
                Mensagens.Add("Senha é inválida.");

            return Mensagens.Count == 0;
        }
    }
}
