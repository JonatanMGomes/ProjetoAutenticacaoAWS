using Xunit;
using ProjetoAutenticacaoAWS.Lib.Models;
using ProjetoAutenticacaoAWS.Lib.MyExceptions;
using System;

namespace ProjetoAutenticacaoAWS.Test;

public class usuarioTest
{
    [Fact]
    public void TestandoSeUsuarioSalvaEmailCorretamente()
    {
        //Arrange
        var usuarioTest = CriarUsuarioTest();
        //Act
        usuarioTest.SetEmail("teste@email");
        //Assert
        Assert.Equal("teste@email", usuarioTest.Email);
    }
    [Fact]
    public void TestandoSeUsuarioNaoSalvaEmailIncorreto()
    {
        //Arrange-Act
        var exceptionTest = Assert.Throws<DadosInvalidosException>(() => new Usuario("emailemail", "11122233344",
                                                                   DateTime.Parse("11/07/1994"), "Fulano", "senha123",
                                                                   DateTime.Now));
        //Assert
        Assert.Equal("Email deve conter @!", exceptionTest.Message);
    }
    [Fact]
    public void TestandoSeUsuarioSalvaCpfCorretamente()
    {
        //Arrange
        var usuarioTest = CriarUsuarioTest();
        //Act
        usuarioTest.SetCpf("11122233377");
        //Assert
        Assert.Equal("11122233377", usuarioTest.Cpf);
    }
    [Fact]
    public void TestandoSeUsuarioNaoSalvaCpfIncorreto()
    {
        //Arrange-Act
        var exceptionTest = Assert.Throws<DadosInvalidosException>(() => new Usuario("email@email", "111222333444",
                                                                   DateTime.Parse("11/07/1994"), "Fulano", "senha123",
                                                                   DateTime.Now));
        //Assert
        Assert.Equal("CPF deve ter 11 digitos, apenas números!", exceptionTest.Message);
    }
    [Fact]
    public void TestandoSeUsuarioSalvaDataNascimentoCorretamente()
    {
        //Arrange
        var usuarioTest = CriarUsuarioTest();
        //Act
        usuarioTest.SetDataNascimento(DateTime.Parse("21/06/1997"));
        //Assert
        Assert.Equal(DateTime.Parse("21/06/1997"), usuarioTest.DataNascimento);
    }
    [Fact]
    public void TestandoSeUsuarioNaoSalvaAnoNascimentoIncorreto()
    {
        //Arrange-Act
        var exceptionTest = Assert.Throws<DadosInvalidosException>(() => new Usuario("email@email", "11122233344",
                                                                   DateTime.Parse("11/07/2011"), "Fulano", "senha123",
                                                                   DateTime.Now));
        //Assert
        Assert.Equal("Ano de nascimento deve ser menor que 2010!", exceptionTest.Message);
    }
    [Fact]
    public void TestandoSeUsuarioSalvaNomeCorretamente()
    {
        //Arrange
        var usuarioTest = CriarUsuarioTest();
        //Act
        usuarioTest.SetNome("Neo");
        //Assert
        Assert.Equal("Neo", usuarioTest.Nome);
    }
    [Fact]
    public void TestandoSeUsuarioSalvaSenhaCorretamente()
    {
        //Arrange
        var usuarioTest = CriarUsuarioTest();
        //Act
        usuarioTest.SetSenha("123456789");
        //Assert
        Assert.Equal("123456789", usuarioTest.Senha);
    }
    [Fact]
    public void TestandoSeUsuarioNaoSalvaSenhaIncorreta()
    {
        //Arrange-Act
        var exceptionTest = Assert.Throws<DadosInvalidosException>(() => new Usuario("email@email", "11122233344",
                                                                   DateTime.Parse("11/07/1994"), "Fulano", "senha",
                                                                   DateTime.Now));
        //Assert
        Assert.Equal("Senha precisa ter no mínimo 8 caracteres!", exceptionTest.Message);
    }
    private Usuario CriarUsuarioTest()
    {
        var usuarioTest = new Usuario("email@email", "11122233344", DateTime.Parse("11/07/1994"), "Fulano", "senha123",
                                      DateTime.Now);
        return usuarioTest;
    }
}