# Wizard/Lane (version 2)
Lane is a chatbot designed to feel authentic, following in the spirit of
the Twitch streamer Neuro-sama.

## Current features
- Can be interacted with through either terminal or discord
- Has window, summary, and RAG memory
- Chooses whether to respond to messages or not

## Roadmap
- Support for more interactive mediums
- Support for more models
- Self-prompting during downtime to allow for them to initiate their own conversations
- Sentiment analysis of own messages for creating a longterm run of mood
- Optimization of context used

## Tech stack
Fully written in C#. Using Claude Haiku 4.5 for the main LLM and OpenAI 
text-embedding-3-small for vector embedding. Qdrant is used for the vector
database.