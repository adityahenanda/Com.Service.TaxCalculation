using Com.Service.TaxCalculation.Lib.Utilities;
using Com.Service.TaxCalculation.Lib.Utilities.BaseClass;
using Com.Service.TaxCalculation.Lib.Utilities.Interfaces;
using Com.Service.TaxCalculation.WebApi.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Com.Service.TaxCalculation.Test.WebApi.Utils
{
    public abstract class BaseControllerTest<TController, TModel, TViewModel, IFacade>
    where TController : BaseController<TModel, TViewModel, IFacade>
    where TModel : BaseModel, new()
    where TViewModel : BaseViewModel, IValidatableObject, new()
    where IFacade : class, IBaseFacade<TModel>
    {
        protected TModel Model
        {
            get { return new TModel(); }
        }

        protected TViewModel ViewModel
        {
            get { return new TViewModel(); }
        }

        protected List<TViewModel> Models
        {
            get { return new List<TViewModel>(); }
        }

        protected List<TViewModel> ViewModels
        {
            get { return new List<TViewModel>(); }
        }

        protected ServiceValidationException GetServiceValidationExeption()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            List<ValidationResult> validationResults = new List<ValidationResult>();
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(this.ViewModel, serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        protected (Mock<IValidateService> ValidateService, Mock<IFacade> Facade, Mock<IMapper> Mapper) GetMocks()
        {
            return (ValidateService: new Mock<IValidateService>(), Facade: new Mock<IFacade>(), Mapper: new Mock<IMapper>());
        }


        protected TController GetController((Mock<IValidateService> ValidateService, Mock<IFacade> Facade, Mock<IMapper> Mapper) mocks)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            TController controller = (TController)Activator.CreateInstance(typeof(TController), mocks.ValidateService.Object, mocks.Facade.Object, mocks.Mapper.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
            return controller;
        }

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private int GetStatusCodeGet((Mock<IValidateService> ValidateService, Mock<IFacade> Facade, Mock<IMapper> Mapper) mocks)
        {
            TController controller = this.GetController(mocks);
            IActionResult response = controller.Get();

            return this.GetStatusCode(response);
        }

        [Fact]
        public void Get_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new ReadResponse<TModel>(new List<TModel>(), 0, new Dictionary<string, string>(), new List<string>()));
            mocks.Mapper.Setup(f => f.Map<List<TViewModel>>(It.IsAny<List<TModel>>())).Returns(this.ViewModels);

            int statusCode = this.GetStatusCodeGet(mocks);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Get_ReadThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            int statusCode = this.GetStatusCodeGet(mocks);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        private async Task<int> GetStatusCodePost((Mock<IValidateService> ValidateService, Mock<IFacade> Facade, Mock<IMapper> Mapper) mocks)
        {
            TController controller = this.GetController(mocks);
            IActionResult response = await controller.Post(this.ViewModel);

            return this.GetStatusCode(response);
        }

        [Fact]
        public async Task Post_WithoutException_ReturnCreated()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(s => s.Validate(It.IsAny<TViewModel>())).Verifiable();
            mocks.Facade.Setup(s => s.CreateAsync(It.IsAny<TModel>())).ReturnsAsync(1);

            int statusCode = await this.GetStatusCodePost(mocks);
            Assert.Equal((int)HttpStatusCode.Created, statusCode);
        }

        [Fact]
        public async Task Post_ThrowServiceValidationExeption_ReturnBadRequest()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(s => s.Validate(It.IsAny<TViewModel>())).Throws(this.GetServiceValidationExeption());

            int statusCode = await this.GetStatusCodePost(mocks);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async Task Post_ThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.ValidateService.Setup(s => s.Validate(It.IsAny<TViewModel>())).Verifiable();
            mocks.Facade.Setup(s => s.CreateAsync(It.IsAny<TModel>())).ThrowsAsync(new Exception());

            int statusCode = await this.GetStatusCodePost(mocks);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        private async Task<int> GetStatusCodeGetById((Mock<IValidateService> ValidateService, Mock<IFacade> Facade, Mock<IMapper> Mapper) mocks)
        {
            TController controller = this.GetController(mocks);
            IActionResult response = await controller.GetById(1);

            return this.GetStatusCode(response);
        }

        [Fact]
        public async Task GetById_NotNullModel_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(this.Model);

            int statusCode = await this.GetStatusCodeGetById(mocks);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public async Task GetById_NullModel_ReturnNotFound()
        {
            var mocks = this.GetMocks();
            mocks.Mapper.Setup(f => f.Map<TViewModel>(It.IsAny<TModel>())).Returns(this.ViewModel);
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync((TModel)null);

            int statusCode = await this.GetStatusCodeGetById(mocks);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public async Task GetById_ThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            int statusCode = await this.GetStatusCodeGetById(mocks);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

    }
}
