using Xunit;
using ProjetoAutenticacaoAWS.Lib.Models;
using ProjetoAutenticacaoAWS.Lib.MyExceptions;
using System;

namespace ProjetoAutenticacaoAWS.Test
{
    public class ModelBaseTest
    {
        [Fact]
        public void TestandoSeUsuarioSalvaIdCorretamente()
        {
            //Arrange
            var baseTest = new ModelBase(1, DateTime.Now);
            //Act
            baseTest.SetId(2);
            //Assert
            Assert.Equal(2, baseTest.Id);
        }
        [Fact]
        public void TestandoSeUsuarioSalvaDataCriacaoCorretamente()
        {
            //Arrange
            var baseTest = new ModelBase(1, DateTime.Now);
            //Act
            baseTest.SetDataCriacao(DateTime.Parse("05/05/2005"));
            //Assert
            Assert.Equal(DateTime.Parse("05/05/2005"), baseTest.DataCriacao);
        }
    }
}