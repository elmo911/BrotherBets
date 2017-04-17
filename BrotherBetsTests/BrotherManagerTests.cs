using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrotherBetsLibrary;
using BrotherBetsLibrary.Data;
using BrotherBetsLibrary.Data.Interfaces;
using Moq;
using Xunit;

namespace BrotherBetsTests
{
    public class BrotherManagerTests
    {
        [Fact]
        public void Get_ByName_GetsBrother()
        {
            var repo = new Mock<IBrotherRepository>();
            var manager = new BrotherManager(repo.Object);
            manager.Get("Brother Name");
            repo.Verify(m => m.Get(It.IsAny<string>()));
        }

        [Fact]
        public void GetAll_GetsBrotherList()
        {
            var repo = new Mock<IBrotherRepository>();
            var manager = new BrotherManager(repo.Object);
            manager.GetAll();
            repo.Verify(m => m.GetAll());
        }
    }
}
