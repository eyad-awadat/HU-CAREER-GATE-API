using System.Text.Json;
using HUCAREERGATE.DTO;
using OpenAI.Chat;

namespace HUCAREERGATE.Services
{
    public class AIServices : IAIServices
    {
        private readonly string _apiKey;

        public AIServices(IConfiguration config)
        {
            _apiKey = config["OpenAI:ApiKey"];
        }

        public async Task<List<QuestionDTO>> GenerateQuestions(string taskDescription)
        {
            var client = new ChatClient("gpt-5", _apiKey);

            var prompt = $@"
            You are a senior technical interviewer creating assessment questions for software developers.
            Based strictly on the programming task description below, generate exactly 7 multiple-choice questions that evaluate the candidate's understanding of the algorithm, logic, complexity, and edge cases involved in the task.
            Task Description:
            {taskDescription}
            Question distribution:
            - 2 algorithm concept
            - 2 algorithm logic
            - 1 complexity
            - 2 edge cases
            Return ONLY valid JSON in this format:
            [
            {{
              ""question"": ""Question text"",
              ""optionA"": ""Option A"",
              ""optionB"": ""Option B"",
              ""optionC"": ""Option C"",
              ""optionD"": ""Option D"",
              ""correctAnswer"": ""A""
            }}
            ]
            ";

            var response = await client.CompleteChatAsync(prompt);

            var json = response.Value.Content[0].Text;

            var questions = JsonSerializer.Deserialize<List<QuestionDTO>>(json,new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

            return questions;
        }
        public async Task<QuestionDTO> RegenerateQuestion(string taskDescription)
        {
            var client = new ChatClient("gpt-5", _apiKey);

            var prompt = $@"
                    You are a senior technical interviewer.

                    Based on this task description generate ONE multiple choice question.

                    Task Description:
                    {taskDescription}

                    Return ONLY valid JSON in this format:

                    {{
                    ""question"": ""Question text"",
                    ""optionA"": ""Option A"",
                    ""optionB"": ""Option B"",
                    ""optionC"": ""Option C"",
                    ""optionD"": ""Option D"",
                    ""correctAnswer"": ""A""
                    }}
                    ";

            var response = await client.CompleteChatAsync(prompt);

            var json = response.Value.Content[0].Text;

            var question = JsonSerializer.Deserialize<QuestionDTO>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return question;
        }
    }
}
