using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using ApplicationUserBC.Interfaces;
using ApplicationUserBC.Interfaces.DTOs;
using ApplicationUserDomain.Model.Repository;
using AutoMapper;
using BookYourFood;
using Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Utility;

namespace ApplicationUserDomain.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly ApplicationSignInManager.ApplicationUserManager applicationUserManager;
        private readonly ApplicationSignInManager applicationSignInManager;
        private readonly IAuthenticationManager authenticationManager;
        private readonly ApplicationSignInManager signInManager;

        public ApplicationUserService(IApplicationUserRepository applicationUserRepository, ApplicationSignInManager.ApplicationUserManager applicationUserManager, ApplicationSignInManager applicationSignInManager, IAuthenticationManager authenticationManager, ApplicationSignInManager signInManager)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.applicationUserManager = applicationUserManager;
            this.applicationSignInManager = applicationSignInManager;
            this.authenticationManager = authenticationManager;
            this.signInManager = signInManager;
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

        public async Task<IdentityResult> CreateUserAccountAndSignIn(UserDto user, string password)
        {
            var userEntity = Mapper.Map<ApplicationUser>(user);
            var result = await applicationUserManager.CreateAsync(userEntity, password);

            if (result.Succeeded)
            {


                await applicationSignInManager.SignInAsync(userEntity, isPersistent: false, rememberBrowser: false);
                
            }

            return result;

        }

        public void LogOff()
        {
            authenticationManager.SignOut();
        }

        public async Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe, bool shouldLockout)
        {
            return await signInManager.PasswordSignInAsync(email, password, rememberMe, shouldLockout: shouldLockout);
        }

        public UserDto GetUserById(string getUserId)
        {
            var user = applicationUserRepository.GetUser(getUserId);
            var dto = Mapper.Map<UserDto>(user);
            return dto;
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
            var user = applicationUserRepository.GetUser(userId);
            user.UnlikeMeal(mealId);
            applicationUserRepository.UpdateUserGraph(user);

            return OperationResult.Success();
        }
    }
}