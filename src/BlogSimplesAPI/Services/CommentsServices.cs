﻿using BlogSimplesAPI.Data.Repositories.Interfaces;
using BlogSimplesAPI.Models;
using BlogSimplesAPI.Services.Interfaces;

namespace BlogSimplesAPI.Services
{
    public class CommentsServices : ICommentServices
    {
        private readonly IRepository<Comments> _repositoryComment;

        public CommentsServices(IRepository<Comments> repositoryComment)
        {
            _repositoryComment = repositoryComment;
        }

        public void Delete(int id)
        {
            _repositoryComment.DeleteById(id);
        }

        public void GetAll()
        {
            _repositoryComment.GetAll();
        }

        public Comments GetById(int id)
        {
            return _repositoryComment.GetById(id.ToString());
        }

        public void Insert(Comments post)
        {
            _repositoryComment.Insert(post);
        }

        public void Update(Comments post)
        {
            _repositoryComment.Update(post);
        }
    }
}
