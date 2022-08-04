using Xunit;
using ProjetoAutenticacaoAWS.Lib.Models;
using ProjetoAutenticacaoAWS.Lib.MyExceptions;
using System;

namespace ProjetoAutenticacaoAWS.Test
{
    public class ModelBaseTest
    {
        [Fact]
        public void TestandoSeUsuarioSalvaDataCriacaoCorretamente()
        {
            //Arrange
            var baseTest = CriarModelTest();
            //Act
            baseTest.SetDataCriacao(DateTime.Parse("05/05/2005"));
            //Assert
            Assert.Equal(DateTime.Parse("05/05/2005"), baseTest.DataCriacao);
        }
        private ModelBase CriarModelTest()
        {
            var modelTest = new ModelBase(Guid.NewGuid() , DateTime.Now);
            return modelTest;
        }
    }
}