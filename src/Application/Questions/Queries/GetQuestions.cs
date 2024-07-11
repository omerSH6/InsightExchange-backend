﻿using Application.Common.DTOs;
using Application.Common.Services.Mediator.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;

namespace Application.Questions.Queries
{
    public class GetQuestionsQuery : IRequest<List<QuestionPreviewDTO>>
    {
        public string? Tag { get; set; }
        public required SortBy SortBy { get; set; }
        public required SortDirection SortDirection { get; set; }
        public required QuestionState QuestionState { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetQuestionsHandler : IRequestHandler<GetQuestionsQuery, List<QuestionPreviewDTO>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public GetQuestionsHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUserService userService)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<List<QuestionPreviewDTO>> Handle(GetQuestionsQuery request)
        {
            var authenticatedUserId = _userService.GetAuthenticatedUserIfExist();
            if (request.QuestionState == QuestionState.Pending)
            {
                if (!authenticatedUserId.HasValue)
                {
                    throw new Exception($"User is not authenticated");
                }

                var user = await _userRepository.GetUserById(authenticatedUserId.Value);
                if (user.Role != UserRole.Admin)
                {
                    throw new Exception($"User NOT authorized");
                }
            }
            var questions = await _questionRepository.GetTaggedSortedQuestionsWithPaginationAsync(request.Tag, request.SortBy, request.SortDirection, request.Page, request.PageSize, request.QuestionState);
            var questionsPreview = questions.Select(question => QuestionToQuestionPreviewDTO(question, authenticatedUserId)).ToList();

            return questionsPreview;
        }

        private static QuestionPreviewDTO QuestionToQuestionPreviewDTO(Question question, int? AuthenticatedUserId)
        {
            var userDto = new UserDTO() 
            {
                Id = question.User.Id,
                UserName = question.User.UserName,
            };

            var questionPreviewDTO = new QuestionPreviewDTO()
            {
                Id = question.Id,
                Title = question.Title,
                PreviewContent = question.Content.Length > 20 ? question.Content.Take(20).ToString() : question.Content,
                CreatedAt = question.CreatedAt,
                User = userDto,
                WasAskedByCurrentUser = question.UserId == AuthenticatedUserId,
                WasVotedByCurrentUser = question.Votes.Any(vote => vote.User.Id == AuthenticatedUserId),
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
