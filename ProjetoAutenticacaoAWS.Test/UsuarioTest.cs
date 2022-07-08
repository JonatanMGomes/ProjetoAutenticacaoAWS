using Xunit;
using ProjetoAutenticacaoAWS.Lib.Models;
using ProjetoAutenticacaoAWS.Lib.MyExceptions;
using System;

namespace ProjetoAutenticacaoAWS.Test;

public class UnitTest1
{
    [Fact]
    public void TestandoSeUsuarioSalvaIdCorretamente()
    {
        //Arrange
        var usuarioTest = new Usuario(1, "email@email", "11122233344", DateTime.Parse("11/07/1994"), "Fulano", "senha123",
                                      DateTime.Now);
        //Act
        usuarioTest.SetId(2);
        //Assert
        Assert.Equal(2, usuarioTest.Id);
    }
    [Fact]
    public void TestandoSeUsuarioSalvaEmailCorretamente()
    {
        //Arrange
        var usuarioTest = new Usuario(1, "email@email", "11122233344", DateTime.Parse("11/07/1994"), "Fulano", "senha123",
                                      DateTime.Now);
        //Act
        usuarioTest.SetEmail("teste@email");
        //Assert
        Assert.Equal("teste@email", usuarioTest.Email);
    }
    [Fact]
    public void TestandoSeUsuarioNaoSalvaEmailIncorreto()
    {
        //Arrange-Act
        var exceptionTest = Assert.Throws<DadosInvalidosException>(() => new Usuario(1, "emailemail", "11122233344",
                                                                   DateTime.Parse("11/07/1994"), "Fulano", "senha123",
                                                                   DateTime.Now));
        //Assert
        Assert.Equal("Email deve conter @!", exceptionTest.Message);
    }
    [Fact]
    public void TestandoSeUsuarioSalvaCpfCorretamente()
    {
        //Arrange
        var usuarioTest = new Usuario(1, "email@email", "11122233344", DateTime.Parse("11/07/1994"), "Fulano", "senha123",
                                      DateTime.Now);
        //Act
        usuarioTest.SetCpf("11122233377");
        //Assert
        Assert.Equal("11122233377", usuarioTest.Cpf);
    }
    [Fact]
    public void TestandoSeUsuarioNaoSalvaCpfIncorreto()
    {
        //Arrange-Act
        var exceptionTest = Assert.Throws<DadosInvalidosException>(() => new Usuario(1, "email@email", "111222333444",
                                                                   DateTime.Parse("11/07/1994"), "Fulano", "senha123",
                                                                   DateTime.Now));
        //Assert
        Assert.Equal("CPF deve ter 11 digitos, apenas números!", exceptionTest.Message);
    }
    [Fact]
    public void TestandoSeUsuarioSalvaDataNascimentoCorretamente()
    {
        //Arrange
        var usuarioTest = new Usuario(1, "email@email", "11122233344", DateTime.Parse("11/07/1994"), "Fulano", "senha123",
                                      DateTime.Now);
        //Act
        usuarioTest.SetDataNascimento(DateTime.Parse("21/06/1997"));
        //Assert
        Assert.Equal(DateTime.Parse("21/06/1997"), usuarioTest.DataNascimento);
    }
    [Fact]
    public void TestandoSeUsuarioNaoSalvaAnoNascimentoIncorreto()
    {
        //Arrange-Act
        var exceptionTest = Assert.Throws<DadosInvalidosException>(() => new Usuario(1, "email@email", "11122233344",
                                                                   DateTime.Parse("11/07/2011"), "Fulano", "senha123",
                                                                   DateTime.Now));
        //Assert
        Assert.Equal("Ano de nascimento deve ser menor que 2010!", exceptionTest.Message);
    }
    [Fact]
    public void TestandoSeUsuarioSalvaNomeCorretamente()
    {
        //Arrange
        var usuarioTest = new Usuario(1, "email@email", "11122233344", DateTime.Parse("11/07/1994"), "Fulano", "senha123",
                                      DateTime.Now);
        //Act
        usuarioTest.SetNome("Neo");
        //Assert
        Assert.Equal("Neo", usuarioTest.Nome);
    }
    [Fact]
    public void TestandoSeUsuarioSalvaSenhaCorretamente()
    {
        //Arrange
        var usuarioTest = new Usuario(1, "email@email", "11122233344", DateTime.Parse("11/07/1994"), "Fulano", "senha123",
                                      DateTime.Now);
        //Act
        usuarioTest.SetSenha("123456789");
        //Assert
        Assert.Equal("123456789", usuarioTest.Senha);
    }
    [Fact]
    public void TestandoSeUsuarioNaoSalvaSenhaIncorreta()
    {
        //Arrange-Act
        var exceptionTest = Assert.Throws<DadosInvalidosException>(() => new Usuario(1, "email@email", "11122233344",
                                                                   DateTime.Parse("11/07/1994"), "Fulano", "senha",
                                                                   DateTime.Now));
        //Assert
        Assert.Equal("Senha precisa ter no mínimo 8 caracteres!", exceptionTest.Message);
    }
    [Fact]
    public void TestandoSeUsuarioSalvaDataCriacaoCorretamente()
    {
        //Arrange
        var usuarioTest = new Usuario(1, "email@email", "11122233344", DateTime.Parse("11/07/1994"), "Fulano", "senha123",
                                      DateTime.Now);
        //Act
        usuarioTest.SetDataCriacao(DateTime.Parse("05/05/2005"));
        //Assert
        Assert.Equal(DateTime.Parse("05/05/2005"), usuarioTest.DataCriacao);
    }
}