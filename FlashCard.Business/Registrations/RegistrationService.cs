using FlashCard.Core.Entities;
using FlashCard.Core.Repositories.Abstract;
using FlashCard.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Business.Registrations
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _repository;
        private readonly IUnitOfWorkService _unitOfWorkService;
        public RegistrationService(IRegistrationRepository repository, IUnitOfWorkService unitOfWorkService)
        {
            _repository = repository;
            _unitOfWorkService = unitOfWorkService;
        }
        public async Task<bool> CheckActiveAsync(int registrationId)
        {
            Registration? registration = await _repository.FindAsync(registrationId);
            return registration != null && registration.IsActive;
        }
        public async Task<(bool isActive, int? registrationId)> CheckActiveAsync(string mobileNumber, string registrationHash)
        {
            Registration? existedRegistration = await _repository.BuildQuery()
                        .FilterByPhoneNumber(mobileNumber)
                        .FilterActive(true)
                        .FirstOrDefaultAsync();

            if (existedRegistration is null)
            {
                return (false, null);
            }

            string hash = GenerateHash(mobileNumber, existedRegistration.CreatedDate);

            bool isValidHash = hash == registrationHash;

            if (isValidHash)
            {
                return (true, existedRegistration.Id);
            }

            return (false, null);
        }
        private string GenerateHash(string mobileNumber, DateTime dateTime)
        {
            string salt = "65179e11-70a9-430c-b7ac-5160407bdb85";
            return GetHashString($"{mobileNumber}{dateTime.ToString("yyyy-MM-dd")}{salt}");
        }
        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        public async Task<int?> GetRegistrationIdAsync(string mobileNo)
        {
            Registration? existedRegistration = await _repository.BuildQuery()
                        .FilterByPhoneNumber(mobileNo)
                        .FilterActive(true)
                        .FirstOrDefaultAsync();

            return existedRegistration?.Id;
        }
        public async Task<(int registrationId, string registrationHash)> RegisterAsync(string mobileNo)
        {
            Registration? existedRegistration = await _repository.BuildQuery()
                        .FilterByPhoneNumber(mobileNo)
                        .FilterActive(true)
                        .FirstOrDefaultAsync();

            if (existedRegistration is not null)
            {
                existedRegistration.IsActive = false;
                _repository.SoftRemove(existedRegistration);
            }

            Registration newRegistration = new()
            {
                LastSessionTimeStamp = DateTime.Now,
                MobileNo = mobileNo,
                IsActive = true
            };

            await _repository.AddAsync(newRegistration);
            await _unitOfWorkService.SaveChangeAsync();

            return (newRegistration.Id, GenerateHash(mobileNo, newRegistration.CreatedDate));
        }
        public async Task UnRegisterAsync(int registrationId)
        {
            Registration? existedRegistration = await _repository.FindAsync(registrationId);

            if (existedRegistration is null)
            {
                return;
            }

            _repository.SoftRemove(existedRegistration);
            await _unitOfWorkService.SaveChangeAsync();
        }
    }
}
