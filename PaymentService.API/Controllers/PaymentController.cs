namespace PaymentService.API.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    
    using MicroservicesPOC.Shared.Controllers;

    using PaymentService.Application.Account.Queries;

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ApiController
    {
        [HttpGet("accounts/{policyNumber:Guid}")]
        public async Task<ActionResult> AccountBalance(Guid policyNumber) =>
            this.Ok(await this.Mediator.Send(new GetAccountBalanceQuery(policyNumber)));
    }
}
