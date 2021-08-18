# Webhooks-with-Blazor_REST_API_RabbitMQ
.NET Core Project implementing Webhooks using Blazor WASM | EF (MS SQL) | REST API | RabbitMq

<div style="border:1px sold; padding:5px">
 <img src="http://jonathancoombes.com/back.JPG" alt="SpaceX" />
 </div>

 <h4 class="text-white">Webhook and RabbitMQ REST API Demonstration</h4>
                <p class="">Welcome to the .NET project which forms part of this personal portfolio</p>
                <strong class="">Use-Case</strong>
                <p>
                    This application allows for the registration of "webhooks" at the Airline by interested subscribers(Travel Agents) thereby providing them with access to data
                    change updates made available by the publisher(Airline). Once a webhook is registered, the subscribers will be notified of changes to current data (price   changes etc) once such an event has occurred.
                </p>
                <strong class="">Technology</strong>
                <p>
                    .NET Core as the main back-end engine that will host the RESTFull API endpoints and deliver the event-driven information via a message bus application(RabbitMQ).
                    Data will be persisted by a SQL server hosted on a Azure VM.
                    The front end user interfaces are created using Blazor and connected to the .NET Core server application via a SignalR connection.
                </p>
