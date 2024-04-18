using Microsoft.AspNetCore.Mvc;
using TrainingProject.Interfaces;
using TrainingProject.Models;

namespace TrainingProject.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("GetOrder")]
        public async Task<ActionResult> GetOrder()
        {
            var order = await _orderRepository.GetOrder();

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("GetType")]
        public async Task<ActionResult> GetType()
        {
            var types = await _orderRepository.GetType();

            if (types == null)
            {
                return NotFound();
            }

            return Ok(types);
        }

        [HttpGet("GetLine")]
        public async Task<ActionResult> GetLine()
        {
            var lines = await _orderRepository.GetLine();

            if (lines == null)
            {
                return NotFound();
            }

            return Ok(lines);
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult> GetUser()
        {
            var users = await _orderRepository.GetUser();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet("GetStatus")]
        public async Task<ActionResult> GetStatus()
        {
            var status = await _orderRepository.GetStatus();

            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult> AddOrder([FromBody] Order order)
        {
            var addedOrder = await _orderRepository.AddOrder(order);

            return Ok(addedOrder);
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult> AddUser([FromBody] User user)
        {
            var addedUser = await _orderRepository.AddUser(user);

            return Ok(addedUser);
        }

        [HttpPut("EditOrder")]
        public async Task<ActionResult> EditOrder([FromBody] Order order)
        {
            var editedOrder = await _orderRepository.EditOrder(order);

            return Ok(editedOrder);
        }

        [HttpDelete("DeleteOrder")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var deletedOrder = await _orderRepository.DeleteOrder(id);

            return Ok(deletedOrder);
        }

        [HttpGet("IsRegistered")]
        public async Task<ActionResult> IsRegistered(string username)
        {
            var isRegistered = await _orderRepository.IsRegistered(username);

            return Ok(isRegistered);
        }

    }
}
