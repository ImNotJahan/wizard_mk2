using Anthropic.Models.Messages;
using Newtonsoft.Json.Linq;

namespace Wizard.LLM
{
    public sealed class MessageContainer
    {
        readonly string   content;
        readonly Author   author;
        readonly DateTime time;

        public MessageContainer(string content, Author author = Author.User)
        {
            this.content = content;
            this.author  = author;  
            this.time    = DateTime.UtcNow; 
        }

        public MessageContainer(JToken data)
        {
            string? content = (string?) data["content"];
            int?    author  = (int?)    data["author"];

            if(content is null) throw new Exception("Content is null");
            if(author  is null) throw new Exception("Author is null");

            DateTime? time = (DateTime?) data["time"];

            if(time is not null) this.time = (DateTime) time;

            if(!Enum.IsDefined(typeof(Author), author)) throw new Exception($"Invalid author type {author}");
            
            this.author  = (Author) author;
            this.content = content;
        }

        public MessageParam Anthropic()
        {
            Role role = author switch
            {
                Author.User => Role.User,
                Author.Bot  => Role.Assistant,
                _           => throw new Exception($"Unexpected author type {author}")
            };

            string formatted = content;

            if(role == Role.User) formatted = "[" + time.ToString("yyyy/MM/dd HH:mm:ss") + "] " + formatted;

            return new()
            {
                Role    = role,
                Content = content
            };
        }

        public string GetContent() => content;

        public Author GetAuthor() => author;

        public JToken Serialize()
        {
            return new JObject()
            {
                ["content"] = content,
                ["author"]  = (int) author,
                ["time"]    = time
            };
        }
    }

    public enum Author
    {
        User,
        Bot
    }
}