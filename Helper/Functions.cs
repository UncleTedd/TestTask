using System.Text.RegularExpressions;
using AlifTestTask.DTOs;
using AlifTestTask.Models;

namespace AlifTestTask.Helper;

public class Functions
{
    private const string phonePattern = "^([0-9]{9})$";
    private const string passportPattern = ("[0-9]{7}");
    private static readonly Regex _allowedPhoneRegex = new Regex(phonePattern, RegexOptions.Compiled);
    private static readonly Regex _allowedPassportRegex = new Regex(passportPattern, RegexOptions.Compiled);

    public int VerifyUser(ToVerifyUserModel user)
    {
        if (_allowedPhoneRegex.IsMatch(user.PhoneNumber)&& _allowedPassportRegex.IsMatch(user.PassportSeries) )
        {
            return 1;
        }

        return 0;

    }

    public User MapUserDtoToUser(UserDTO userDto)
    {
        var user = new User()
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
}