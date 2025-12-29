using AutoMapper;
using FluentAssertions;
using ComplyX_Businesss.Models;
using Microsoft.Extensions.Logging;
using Xunit;
using Nest;
namespace ComplyX_Tests.Profile
{
    public class CompanyMappingTests
    {
        private readonly IMapper _mapper;

        public CompanyMappingTests()
        {
            var configuration = new MapperConfiguration((IMapperConfigurationExpression cfg) =>
            {
                cfg.AddProfile<AutoMappingProfile>();
            }, LoggerFactory.Create(builder => builder.AddConsole()));
            _mapper = configuration.CreateMapper();
        }

        [Theory]
        [InlineData(null, false, 1, "Single Company")]
        [InlineData("", false, 1, "Single Company")]
        [InlineData("Single", false, 1, "Single Company")]
        [InlineData("Consolidated", false, 2, "Multi-company Child Company")]
        [InlineData("Controlled", false, 3, "Multi-company Child Company")]
        [InlineData("Consolidated", true, 2, "Multi-company Parent Company")]
        [InlineData("Controlled", true, 3, "Multi-company Parent Company")]
        public void UpdateCompanyRequest_Maps_GroupType_And_LicenseType_Correctly(
            string groupType, bool groupParent, int? expected, string expectedLicenseType)
        {
            // Arrange
            var updateRequest = new UpdateCompanyRequest
            {
                UserName = "Test Company",
                GroupType = groupType,
                GroupParent = groupParent
            };

            // Act
            var customer = _mapper.Map<Company>(updateRequest);

            // Assert
            customer.Address.Should().Be(expected.ToString());
            customer.Name.Should().Be(expected.ToString());
        }
        public void BeValidEnum<TEnum>(string propertyName) where TEnum : Enum
        {
            // validation logic
        }
    }
}
