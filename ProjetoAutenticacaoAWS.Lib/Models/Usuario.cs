using ProjetoAutenticacaoAWS.Lib.MyExceptions;

namespace ProjetoAutenticacaoAWS.Lib.Models
{
    public class Usuario : ModelBase
    {
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Nome { get; private set; }
        public string Senha { get; private set; }
        public string? UrlImagemCadastro { get; private set; }
        
        protected Usuario()
        {

        }
        public Usuario(int id, string email, string cpf, DateTime dataNascimento, string nome, string senha, DateTime dataCriacao) :
                       base(id, dataCriacao)
        {
            SetEmail(email);
            SetCpf(cpf);
            SetDataNascimento(dataNascimento);
            SetNome(nome);
            SetSenha(senha);
        }
        public void SetEmail(string email)
        {
            Email = ValidarEmail(email);
        }
        public void SetCpf(string cpf)
        {
            Cpf = ValidarCpf(cpf);
        }
        public void SetDataNascimento(DateTime dataNascimento)
        {
            DataNascimento = ValidarAnoNascimento(dataNascimento);
        }
        public void SetNome(string nome)
        {
            Nome = nome;
        }
        public void SetSenha(string senha)
        {
            Senha = ValidarSenha(senha);
        }
        public void SetUrlImagemCadastro(string urlImagemCadastro)
        {
            UrlImagemCadastro = urlImagemCadastro;
        }
        public DateTime ValidarAnoNascimento(DateTime data)
        {
            if (data.Year < 2010)
            {
                return data;
            }
            throw new DadosInvalidosException("Ano de nascimento deve ser menor que 2010!");
        }
        public string ValidarEmail(string email)
        {
            if (email.Contains("@"))
            {
                return email;
            }
            throw new DadosInvalidosException("Email deve conter @!");
        }
        public string ValidarCpf(string cpf)
        {
            if (cpf.Length == 11 && cpf.All(char.IsNumber))
            {
                return cpf;
            }
            throw new DadosInvalidosException("CPF deve ter 11 digitos, apenas números!");
        }
        public string ValidarSenha(string senha)
        {
            if (senha.Length >= 8)
            {
                return senha;
            }
            throw new DadosInvalidosException("Senha precisa ter no mínimo 8 caracteres!");
        }
    }
}