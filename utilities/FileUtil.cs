namespace utilities
{
   public static class FileUtil
   {
      public static string WHITESPACE => "WHITESPACE";

      /// <summary>
      /// Converts input to a safe filename. Bad characters are replaced with underscores '_'.
      ///
      /// For null returns null.
      /// For whitespace returns "WHITESPACE"
      /// </summary>
      /// <param name="filename">Filename to convert</param>
      /// <returns></returns>
      public static string GetSafeFilename(string filename)
      {
         if (string.IsNullOrWhiteSpace(filename))
         {
            if (filename == null)
            {
               return null;
            }
            else
            {
               // filename that is only whitespace does not make sense
               return WHITESPACE;
            }
         }
         // Convert bad characters to '_'
         return string.Join("_", filename.Split(System.IO.Path.GetInvalidFileNameChars()));
      }
   }
}