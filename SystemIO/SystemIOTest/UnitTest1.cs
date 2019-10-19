using System;
using System.IO;
using Xunit;
using static SystemIO.Program;

namespace SystemIOTest
{
    public class UnitTest1
    {
        [Fact]
        public void CheckWordExists()
        {
            string path = "../../../test.txt";
            File.Delete(path);
            AddWord(path, "Rachel");
            Assert.Equal("Rachel", ViewWords(path));
        }
    }
}
