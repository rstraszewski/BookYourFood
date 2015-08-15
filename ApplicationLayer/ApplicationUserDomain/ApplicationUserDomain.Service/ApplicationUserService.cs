﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ApplicationUserBC.Interfaces.DTOs;
using ApplicationUserBC.Service;
using ApplicationUserDomain.Infrastructure;
using ApplicationUserDomain.Model.Repository;
using AutoMapper;
using Common.Service;
//using Database;
using Identity.Model;
using Utility;

namespace ApplicationUserDomain.Service
{
    public class ApplicationUserService : ApplicationService<ApplicationUserDomainDbContext>, IApplicationUserService
    {
        private readonly IApplicationUserRepository applicationUserRepository;
        public ApplicationUserService(ApplicationUserDomainDbContext context, IApplicationUserRepository applicationUserRepository)
            : base(context)
        {
            this.applicationUserRepository = applicationUserRepository;
        }

        public OperationResult AddUserAnswers(List<long> answersIds, string userId)
        {
            var user = applicationUserRepository.GetUser(userId);

            if (user == null)
            {
                return OperationResult.ErrorResult("User not found");
            }

            var answers = answersIds.Select(answer => new UserAnswer {AnswerId = answer}).ToList();
            user.AnswerQuestions(answers);
            applicationUserRepository.UpdateUserGraph(user);

            return OperationResult.Success();
        }

        public List<long> GetUserAnswers(string userId)
        {
            var answers = applicationUserRepository.GetUserAnswers(userId).ToList();
            return answers;
        }

        public void ChangeNameAndSurname(string userId, string name, string surname)
        {
            using (var ts = new TransactionScope())
            {
                var user = applicationUserRepository.GetUser(userId);
                applicationUserRepository.UpdateUser(user);
                ts.Complete();
            }
        }

        public OperationResult AddFavouriteMeal(long mealId, string userId)
        {
            var user = applicationUserRepository.GetUser(userId);

            if(user == null)
            {
                return OperationResult.ErrorResult("User not found.");
            }

            user.LikeMeal(mealId);
            applicationUserRepository.UpdateUserGraph(user);
            
            return OperationResult.Success();
        }

        public List<long> GetUserFavouriteMeals(string userId)
        {
            var user = applicationUserRepository.GetUser(userId);


            if (user != null)
            {
                return applicationUserRepository.GetUserFavouriteMeals(userId);
            }

            return new List<long>();
        }

        public OperationResult RemoveFavouriteMeal(long mealId, string userId)
        {
            var user = _context.Users.Find(userId);
            user.UnlikeMeal(mealId);
            applicationUserRepository.UpdateUserGraph(user);

            return OperationResult.Success();
        }
    }
}