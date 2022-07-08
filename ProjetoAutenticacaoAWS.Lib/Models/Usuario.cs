namespace ProjetoAutenticacaoAWS.Lib.Models
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Nome { get; private set; }
        public string Senha { get; private set; }
        public string UrlImagemCadastro { get; private set; }
        public DateTime DataCriacao { get; private set; }
        protected Usuario()
        {
            
        }
        public Usuario(int iD, string email, string cpf, DateTime dataNascimento, string nome, string senha, string urlImagemCadastro,
                       DateTime dataCadastro)
        {
            
        }
        public void SetId(int id){
            Id = id;
        }
        public void SetEmail(string email){
            Email = email;
        }
        public void SetCpf(string cpf){
            Cpf = cpf;
        }
        public void SetDataNascimento(DateTime dataNascimento){
            DataNascimento = dataNascimento;
        }
        public void SetNome(string nome){
            Nome = nome;
        }
        public void SetSenha(string senha){
            Senha = senha;
        }
        public void SetUrlImagemCadastro(string urlImagemCadastro){
            UrlImagemCadastro = urlImagemCadastro;
        }
        public void SetDataCriacao(DateTime dataCriacao){
            DataCriacao = dataCriacao;
        }
    }
}