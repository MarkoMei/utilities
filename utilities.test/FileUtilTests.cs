using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace utilities.test
{
   [TestClass]
   public class FileUtilTests
   {
      [TestMethod]
      public void GetSafeFilename_GoodFilename_NoChanges()
      {
         var goodFilename = "file.txt";
         Assert.AreEqual(goodFilename, FileUtil.GetSafeFilename(goodFilename));
      }

      [TestMethod]
      public void GetSafeFilename_BadFilename_BadCharactersReplacedWithUnderscore()
      {
         var badFilename = ">fi/l?e.t*t";
         var expectedFilename = "_fi_l_e.t_t";
         Assert.AreEqual(expectedFilename, FileUtil.GetSafeFilename(badFilename));
      }

      [TestMethod]
      public void GetSafeFilename_InputNull_ReturnsNull()
      {
         Assert.IsNull(FileUtil.GetSafeFilename(null));
      }

      [TestMethod]
      public void GetSafeFilename_InputWhiteSpace_ReturnsTextWHITESPACE()
      {
         Assert.AreEqual(FileUtil.WHITESPACE, FileUtil.GetSafeFilename("  \t   "));
         Assert.AreEqual(FileUtil.WHITESPACE, FileUtil.GetSafeFilename(string.Empty));
      }
   }
}
