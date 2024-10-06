﻿using BlogSimplesMvc.UI.Models;

namespace BlogSimplesMvc.UI.Services.Interfaces
{
    public interface IPostServices
    {
        void Insert(Post post);
        void Update(Post post);
        void Delete(int id);
        IEnumerable<Post> GetAll();
        Post GetById(int id);
    }
}