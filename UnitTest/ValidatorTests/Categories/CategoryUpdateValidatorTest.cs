using VidBox.Service.Dtos.Categories;
using VidBox.Service.Validators.Dtos.Categories;

namespace UnitTest.ValidatorTests.Categories;

public class CategoryUpdateValidatorTest
{
    [Theory]
    [InlineData("1234dsfsdfADASD@#!@#!@#adsasd")]
    [InlineData("!23aADAdsddsds!@#!@#!@3adsdasd")]
    [InlineData("!@#QEad123!@#!@#1243123")]
    [InlineData("A1!@#!#qwesd@#QEsdfsdfsdf")]
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
        CategoryUpdateDto categoryCreateDto = new CategoryUpdateDto()
        {
            Name = name,
        };

        var validator = new CategoryUpdateValidator();
        var result = validator.Validate(categoryCreateDto);
        Assert.False(result.IsValid);
    }
}
