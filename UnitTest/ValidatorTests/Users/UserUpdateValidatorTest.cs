using Microsoft.AspNetCore.Http;
using System.Text;
using VidBox.Service.Dtos.Users;
using VidBox.Service.Validators.Dtos.Users;

namespace UnitTest.ValidatorTests.Users;

public class UserUpdateValidatorTest
{
    [Theory]
    [InlineData("1234dsfsdfADASD@#!@#!@#adsasd")]
    [InlineData("!23aADAdsddsds!@#!@#!@3adsdasd")]
    [InlineData("!@#QEad123!@#!@#1243123")]
    [InlineData("A1!@#!#qwesd@#QEsdfsdfsdf")]
    [InlineData("A1!@#!#qwasdaesd@#QEsdfsdfsdf")]
    [InlineData("A1!@#!asdasd#qwesd@#QEsdfsdfsdf")]
    [InlineData("aAdaE!@qeqWE!@#!@#adsadssa1")]
    [InlineData("A0as12312da!@#!@#sdsdfsdfsdf")]
    [InlineData("       dfasdfsdfsdfa12")]
    [InlineData("       2323411!@#!@aadsfASDA")]
    [InlineData("       ASD123!@#!@aadsfASDA")]
    [InlineData("ASD123!@#!@aadsfASDA      asd ")]
    [InlineData("ASD123!@#!@aadsfASDsdfsA   assdasdas")]
    [InlineData("       ASD123!@#!@aadsfASDA      asda12asdsdasd")]
    [InlineData("asd       Aasd0!@#!@aadsfASDA       asasdasd ")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic " +
           "products to our clients")]
    public void ShouldReturnInValidValidation(string name)
    {
        UserUpdateDto userCreateDto = new UserUpdateDto()

        {
            Name = name,
            Password = "AAaa11##"
        };
        var validator = new UserUpdateValidator();
        var result = validator.Validate(userCreateDto);
        Assert.False(result.IsValid);
    }
}
