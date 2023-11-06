﻿using ApplicationsService.Models;
using ApplicationsService.Models.Dtos;
using ApplicationsService.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IApplicationInterface _applicationInterface;
        private readonly ResponseDto _response;
        public ApplicationsController(IMapper mapper, IApplicationInterface applicationInterface)
        {
            _mapper = mapper;
            _applicationInterface = applicationInterface;
            _response = new ResponseDto();
        }
        //create application
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> makeApplication(ApplicationRequestDto applicationRequest)
        {
            
                var newApplication = _mapper.Map<Application>(applicationRequest);
                var res = await _applicationInterface.makeApplication(newApplication);
                if (string.IsNullOrWhiteSpace(res))
                {
                    _response.Message = "Application not sent";
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                _response.Message = res;
                return Ok(_response);           
        }
        //get all applications
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllApplications()
        {
            var res = await _applicationInterface.getApplications();
            if(res == null)
            {
                _response.Message = "could not fetch applications";
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            _response.obj = res;
            return Ok(_response);
        }
        //get application by Id
        [HttpGet("byId")]
        public async Task<ActionResult<ResponseDto>> GetApplicationById(Guid id)
        {
            var res = await _applicationInterface.getApplication(id);
            if (res == null)
            {
                _response.Message = "could not fetch application";
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            _response.obj = res;
            return Ok(_response);
        }
    }
}
