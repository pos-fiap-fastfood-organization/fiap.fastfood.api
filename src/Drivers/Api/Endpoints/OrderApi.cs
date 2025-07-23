using Adapters.Controllers.Interfaces;
using Adapters.DTOs.Orders;
using Core.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
[Route("order")]
public class OrderApi : ControllerBase
{
    private readonly IOrderController _orderController;

    public OrderApi(IOrderController orderController)
    {
        _orderController = orderController;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllByFilterAsync(
        [FromQuery] int page,
        [FromQuery] int size,
        [FromQuery] OrderStatus? status,
        CancellationToken cancellationToken)
    {
        var orderFilter = new OrderFilter(status, page, size);
        var orders = await _orderController.GetAllByFilterAsync(orderFilter, cancellationToken);

        return Ok(orders);
    }

    [HttpGet("{id}", Name = "GetOrderById")]
    public async Task<IActionResult> GetByIdAsync(
        string id,
        CancellationToken cancellationToken)
    {
        var order = await _orderController.GetByIdAsync(id, cancellationToken);

        return Ok(order);
    }

    [HttpGet("{id}/paymentstatus")]
    public async Task<IActionResult> GetStatusByIdAsync(
    string id,
    CancellationToken cancellationToken)
    {
        var order = await _orderController.GetPaymentStatusAsync(id, cancellationToken);

        return Ok(order);
    }


    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(
        [FromBody] CreateOrderRequest createRequest,
        CancellationToken cancellationToken)
    {
        var order = await _orderController.CreateOrderAsync(
            createRequest,
            cancellationToken);

        return CreatedAtRoute("GetOrderById", new { order.Id }, order);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatusAsync(
        [FromBody] UpdateStatusOrderRequest updateRequest,
        string id,
        CancellationToken cancellationToken)
    {
        var updatedOrder = await _orderController.UpdateStatusAsync(
            id,
            updateRequest,
            cancellationToken);

        return Ok(updatedOrder);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(
        string id,
        CancellationToken cancellationToken)
    {
        await _orderController.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id}/checkout")]
    public async Task<IActionResult> CheckoutAsync(
       [FromBody] OrderCheckoutRequest checkoutRequest,
       string id,
       CancellationToken cancellationToken)
    {
        var checkoutResponse = await _orderController.CheckoutAsync(id, checkoutRequest, cancellationToken);
        return Ok(checkoutResponse);
    }

    [HttpPost("{id}/confirm-payment")]
    public async Task<IActionResult> ConfirmPaymentAsync(
        string id,
        CancellationToken cancellationToken)
    {
        await _orderController.ConfirmPaymentAsync(id, cancellationToken);
        return NoContent();
    }
}