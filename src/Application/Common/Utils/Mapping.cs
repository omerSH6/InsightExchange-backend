using Application.Common.DTOs;
using Azure.Core;
using Domain.Entities;

namespace Application.Common.Utils
{
    public static class Mapping
    {
        public static AnswerDTO AnswerToAnswerDTO(Answer answer, int? authenticatedUserId)
        {
            var answerDto = new AnswerDTO()
            {
                Id = answer.Id,
                Content = answer.Content,
                CreatedAt = answer.CreatedAt,
                User = new UserDTO()
                {
                    Id = answer.User.Id,
                    UserName = answer.User.UserName,
                },
                WasReipaiedByCurrentUser = authenticatedUserId.HasValue? answer.UserId == authenticatedUserId.Value : false,
                WasVotedByCurrentUser = authenticatedUserId.HasValue ? answer.Votes.Any(vote => vote.User?.Id == authenticatedUserId.Value) : false,
                TotalVotes = answer.Votes.Where(vote => vote.isPositiveVote).ToList().Count() - answer.Votes.Where(vote => !vote.isPositiveVote).ToList().Count(),
            };

            return answerDto;
        }
        
        public static TagDTO TagToTagDTO(Tag tag)
        {
            var tagDto = new TagDTO()
            {
                Name = tag.Name,
            };

            return tagDto;
        }

        public static QuestionDTO QuestionToQuestionDTO(Question question, int? authenticatedUserId)
        {
            var questionDTO = new QuestionDTO()
            {
                Title = question.Title,
                Id = question.Id,
                Content = question.Content,
                CreatedAt = question.CreatedAt,
                User = new UserDTO()
                {
                    Id = question.User.Id,
                    UserName = question.User.UserName
                },
                WasAskedByCurrentUser = authenticatedUserId.HasValue && question.UserId == authenticatedUserId,
                WasVotedByCurrentUser = question.Votes.Any(vote => authenticatedUserId.HasValue && vote.UserId == authenticatedUserId),
                TotalVotes = question.Votes.Where(vote => vote.isPositiveVote).ToList().Count() - question.Votes.Where(vote => !vote.isPositiveVote).ToList().Count(),
                Answers = question.Answers.Select(answer => AnswerToAnswerDTO(answer, authenticatedUserId)).ToList(),
                Tags = question.Tags.Select(tag => TagToTagDTO(tag)).ToList(),
            };

            return questionDTO;
        } 
        
        public static QuestionPreviewDTO QuestionToQuestionPreviewDTO(Question question, int? authenticatedUserId)
        {
            var questionPreviewDTO = new QuestionPreviewDTO()
            {
                Id = question.Id,
                Title = question.Title,
                PreviewContent = question.Content.Length > 20 ? question.Content.Take(20).ToString() : question.Content,
                CreatedAt = question.CreatedAt,
                User = new UserDTO()
                {
                    Id = question.User.Id,
                    UserName = question.User.UserName
                },
                WasAskedByCurrentUser = authenticatedUserId.HasValue? question.UserId == authenticatedUserId.Value : false,
                WasVotedByCurrentUser = authenticatedUserId.HasValue ? question.Votes.Any(vote => vote.User.Id == authenticatedUserId.Value) : false,
                TotalVotes = question.Votes.Where(vote => vote.isPositiveVote).ToList().Count() - question.Votes.Where(vote => !vote.isPositiveVote).ToList().Count(),
                TotalAnswers = question.Answers.Count(),
                Tags = question.Tags.Select(tag => new TagDTO()
                {
                    Name = tag.Name
                }).ToList()
            };

            return questionPreviewDTO;
        }
    }
}
