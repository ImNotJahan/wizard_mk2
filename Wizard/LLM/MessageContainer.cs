using Anthropic.Models.Messages;
using Newtonsoft.Json.Linq;

namespace Wizard.LLM
{
    public sealed class MessageContainer
    {
        readonly string content;
        readonly Author author;

        public MessageContainer(string content, Author author = Author.User)
        {
            this.content = content;
            this.author  = author;   
        }

        public MessageContainer(JToken data)
        {
            string? content = (string?) data["content"];
            int?    author  = (int?)    data["author"];

            if(content is null) throw new Exception("Content is null");
            if(author  is null) throw new Exception("Author is null");

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

            return new()
            {
                Role    = role,
                Content = content,
            };
        }

        public string GetContent() => content;

        public JToken Serialize()
        {
            return new JObject()
            {
                ["content"] = content,
                ["author"]  = (int) author
            };
        }
    }

    public enum Author
    {
        User,
        Bot
    }
}