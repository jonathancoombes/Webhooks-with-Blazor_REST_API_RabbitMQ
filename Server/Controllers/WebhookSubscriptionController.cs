using SpaceXAirlines.Server.Data;
using SpaceXAirlines.Server.Models;
using SpaceXAirlines.Shared.Dto;
using static SpaceXAirlines.Server.Constants.Constants;
using AutoMapper;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;
using System.Collections.Generic;

namespace SpaceXAirlines.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookSubscriptionController: ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;

        public WebhookSubscriptionController(AirlineDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<WebhookSubscriptionReadDto> CreateSubscription(WebhookSubscriptionCreateDto webhookSubscriptionCreateDto) {

            var subscription = _context.WebhookSubscriptions.FirstOrDefault(sub => sub.WebhookUri == webhookSubscriptionCreateDto.WebhookUri);

            if (subscription == null)
            {
                subscription = _mapper.Map<WebhookSubscription>(webhookSubscriptionCreateDto);
                subscription.Secret = Guid.NewGuid().ToString();
                subscription.WebhookPublisher = AirLine;

                try
                {
                    _context.WebhookSubscriptions.Add(subscription);
                    _context.SaveChanges();
                }
                catch (Exception ex) {

                    return BadRequest(ex.Message);
                }
               
                return Ok (_mapper.Map<WebhookSubscriptionReadDto>(subscription));
            }
            else {

                return NoContent();
            }
        }

        [HttpGet("{secret}", Name = "GetSubscriptionBySecret")]
        public ActionResult<WebhookSubscriptionReadDto> GetSubscriptionBySecret(string secret)
        {
            
            var subscription = _context.WebhookSubscriptions.FirstOrDefault(sub => sub.Secret == secret);
           
            if (subscription != null)
            {
                var webhookSubscriptionReadDto = _mapper.Map<WebhookSubscriptionReadDto>(subscription);
                
                return CreatedAtRoute(nameof(GetSubscriptionBySecret),new { secret = webhookSubscriptionReadDto.Secret}, webhookSubscriptionReadDto );
                
            }
            else {

                return NotFound();
            }
        }

        [HttpGet(Name = "GetAllSubscriptions")]
        public ActionResult<List<WebhookSubscriptionReadDto>> GetAllSubscriptions() {
            
            var subscriptions = _context.WebhookSubscriptions.ToList();

            var listWSReadDto = new List<WebhookSubscriptionReadDto>();

            if (subscriptions != null)
            {
                foreach(var sub in subscriptions){
                    
                    var webhookSubscriptionReadDto = _mapper.Map<WebhookSubscriptionReadDto>(sub);
                    listWSReadDto.Add(webhookSubscriptionReadDto);
                }
                
                return Ok(listWSReadDto);
            }
            else {

                return NotFound();
            }
        }
    }
}
