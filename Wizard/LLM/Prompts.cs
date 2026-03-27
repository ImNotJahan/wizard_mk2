using System.Reflection;

namespace Wizard.LLM
{
    public static class Prompts
    {
        private static readonly Dictionary<string, string> prompts = [];
        
        static Prompts()
        {
            string? possiblePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) 
                                ?? throw new Exception("Somehow the path of the executing assembly is null");
            
            string dataPath = Path.Join(possiblePath, "LLM", "Prompts");
            
            foreach(string filename in Directory.GetFiles(dataPath))
            {
                prompts[Path.GetFileNameWithoutExtension(filename)] = File.ReadAllText(filename);
            }
        }

        public static string GetPrompt(string prompt) => prompts[prompt];
    }
}