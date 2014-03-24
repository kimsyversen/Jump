namespace Jump
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Jump game = new Jump())
            {
                game.Run();
            }
        }
    }
#endif
}

