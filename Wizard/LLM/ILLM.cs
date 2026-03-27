namespace Wizard.LLM
{
    public interface ILLM
    {
        public Task<MessageContainer> RespondToMessage(List<MessageContainer> context);

        public Task<bool> WantsToRespond(List<MessageContainer> context);

        public Task<MessageContainer> Prompt(List<MessageContainer> context, string systemPrompt);
    }
}