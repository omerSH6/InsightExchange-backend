﻿using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITagRepository
    {
        public Task CreateTagAsync(string tagName);
        public Task<List<Tag>> GetAllTagsAsync();
        public Task<List<Tag>> GetByNamesAsync(List<string> names);
        public Task<Tag> GetByNameAsync(string name);
    }
}
