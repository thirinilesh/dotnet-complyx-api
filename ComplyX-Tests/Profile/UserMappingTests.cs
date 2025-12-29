using AutoMapper;
using FluentAssertions;
using ComplyX.Shared.Data;
using ComplyX_Businesss.Models;
using Xunit;
using Microsoft.Extensions.Logging;
using MediaBrowser.Model.Dto;
using UserDto = ComplyX_Businesss.Models.UserDto;

namespace ComplyX_Tests.Profile
{
    public class UserMappingTests
    {
        private readonly IMapper _mapper;

        public UserMappingTests()
        {
            var configuration = new MapperConfiguration(static cfg =>
            {
                cfg.AddProfile<UserProfile>();
            }, LoggerFactory.Create(builder => builder.AddConsole()));
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Should_Map_ApplicationUser_To_UserDto()
        {
            // Arrange
            var applicationUser = new RegisterUser
            {
                
                UserName = "testuser",
                Password = "test@1244",
                Email = "test@example.com",
                Phone = "9855623256",
                Address= "Ahmedabad",
                State = "Gujarat",
                GSTIN = "HJ2314",
                PAN = "JHP142304"
               
            };

            // Act
            var result = _mapper.Map<UserDto>(applicationUser);

            // Assert
            result.UserName.Should().Be(applicationUser.UserName);
            result.Email.Should().Be(applicationUser.Email);
            result.Password.Should().Be(applicationUser.Password);
            result.Address.Should().Be(applicationUser.Address);
            result.State.Should().Be(applicationUser.State);
            result.Phone.Should().Be(applicationUser.Phone);
            result.GSTIN.Should().Be(applicationUser.GSTIN);
            result.PAN.Should().Be(applicationUser.PAN);
        }
    }
}
