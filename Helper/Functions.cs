using System.Text.RegularExpressions;
using AlifTestTask.DTOs;
using AlifTestTask.Models;

namespace AlifTestTask.Helper;

public class Functions
{
    private const string phonePattern = "^([0-9]{9})$";
    private const string passportPattern = "[0-9]{7}";
    private static readonly Regex _allowedPhoneRegex = new(phonePattern, RegexOptions.Compiled);
    private static readonly Regex _allowedPassportRegex = new(passportPattern, RegexOptions.Compiled);

    public int VerifyPhonePassportNumber(string phoneNumber, string passportNumber)
    {
        if (_allowedPhoneRegex.IsMatch(phoneNumber) && _allowedPassportRegex.IsMatch(passportNumber)) return 1;

        return 0;
    }

    public User MapUserDtoToUser(UserDTO userDto)
    {
        var user = new User
        {
            UserVerification = userDto.UserVerification,
            Id = userDto.Id,
            Name = userDto.Name,
            Surname = userDto.Surname,
            PhoneNumber = userDto.PhoneNumber,
            PassportNumber = userDto.PassportNumber
        };
        return user;
    }

    public User MapCreateUserToUser(CreateUserModel createUserModel)
    {
        var user = new User
        {
            Name = createUserModel.Name,
            Surname = createUserModel.Surname,
            PhoneNumber = createUserModel.PhoneNumber,
            PassportNumber = createUserModel.PassportNumber
        };
        return user;
    }

    public UserDTO MapUserToUserDto(User user, Wallet wallet)
    {
        var userDto = new UserDTO
        {
            UserVerification = user.UserVerification,
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            PhoneNumber = user.PhoneNumber,
            PassportNumber = user.PassportNumber,
            Wallet = wallet
        };
        return userDto;
    }

    public UserDTO MapToUserDto(User? user)
    {
        return new UserDTO
        {
            Name = user.Name,
            Surname = user.Surname,
            PhoneNumber = user.PhoneNumber,
            PassportNumber = user.PhoneNumber,
            UserVerification = user.UserVerification,
            Wallet = new Wallet
            {
                Transactions = user.Wallet.Transactions.Select(x => new Transaction
                {
                    transactionTime = x.transactionTime,
                    Amount = x.Amount,
                    WalletId = x.WalletId,
                    Wallet = x.Wallet
                }).ToList()
            }
        };
    }

    public bool IsUnverifiedUserWalletLimitReached(decimal amount)
    {
        return amount >= 10000;
    }

    public bool IsVerifiedUserWalletLimitReached(decimal amount)
    {
        return amount >= 100000;
    }

    public ResponseModel UserIsNull()
    {
        return new ResponseModel
        {
            Result = -1,
            Comment = "Invalid data in replenish service!"
        };
    }

    public ResponseModel UnverifiedUserLimitReached()
    {
        return new ResponseModel
        {
            Result = -1,
            Comment = "Can not proceed replenish request, wallet reached max amount for unverified user!" +
                      " Please verify to proceed to verify user"
        };
    }

    public ResponseModel VerifiedUserLimitReached()
    {
        return new ResponseModel
        {
            Result = -1,
            Comment = "Verified user walled limit reached!"
        };
    }
}