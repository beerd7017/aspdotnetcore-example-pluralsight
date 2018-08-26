using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Produces("application/json")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IDutchRepository repository, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<OrderItem>> Get(int orderId)
        {
            try
            {
                var order = _repository.GetOrderById(User.Identity.Name, orderId);
                if (order != null)
                {
                    return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
                }
                _logger.LogError($"Failed to get order item for order : {orderId}");
                return BadRequest("Filed to get order item");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get order item: {ex}");
                return BadRequest("Filed to get order item");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int orderId, int id)
        {
            try
            {
                var order = _repository.GetOrderById(User.Identity.Name, orderId);
                if (order != null)
                {
                    var item = order.Items.FirstOrDefault(i => i.Id == id);
                    if (item != null)
                    {
                        return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
                    }
                    _logger.LogError($"Failed to get order item: {id}");
                    return BadRequest("Filed to get order item");
                }
                _logger.LogError($"Failed to get order item: {orderId}");
                return BadRequest("Filed to get order");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get order item: {ex}");
                return BadRequest("Filed to get order item");
            }
        }
    }
}
